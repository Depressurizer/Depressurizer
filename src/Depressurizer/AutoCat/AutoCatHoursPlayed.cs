using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using Depressurizer.Core.AutoCats;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;

namespace Depressurizer
{
    public class AutoCatHoursPlayed : AutoCat
    {
        #region Constants

        public const string TypeIdString = "AutoCatHoursPlayed";

        public const string XmlName_Filter = "Filter";

        public const string XmlName_IncludeUnknown = "IncludeUnknown";

        public const string XmlName_Name = "Name";

        public const string XmlName_Prefix = "Prefix";

        public const string XmlName_Rule = "Rule";

        public const string XmlName_Rule_MaxHours = "MaxHours";

        public const string XmlName_Rule_MinHours = "MinHours";

        public const string XmlName_Rule_Text = "Text";

        #endregion

        #region Fields

        [XmlElement("Rule")]
        public List<HoursPlayedRule> Rules;

        #endregion

        #region Constructors and Destructors

        public AutoCatHoursPlayed(string name, string filter = null, string prefix = null, bool includeUnknown = true, List<HoursPlayedRule> rules = null, bool selected = false) : base(name)
        {
            Filter = filter;
            Prefix = prefix;
            IncludeUnknown = includeUnknown;
            Rules = rules ?? new List<HoursPlayedRule>();
            Selected = selected;
        }

        public AutoCatHoursPlayed(AutoCatHoursPlayed other) : base(other)
        {
            Filter = other.Filter;
            Prefix = other.Prefix;
            IncludeUnknown = other.IncludeUnknown;
            Rules = other.Rules.ConvertAll(rule => new HoursPlayedRule(rule));
            Selected = other.Selected;
        }

        /// <summary>
        ///     Parameter-less constructor for XmlSerializer.
        /// </summary>
        private AutoCatHoursPlayed() { }

        #endregion

        #region Public Properties

        /// <inheritdoc />
        public override AutoCatType AutoCatType => AutoCatType.HoursPlayed;

        public bool IncludeUnknown { get; set; }

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        public static AutoCatHoursPlayed LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlName_Name], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlName_Filter], null);
            string prefix = XmlUtil.GetStringFromNode(xElement[XmlName_Prefix], string.Empty);
            bool includeUnknown = XmlUtil.GetBoolFromNode(xElement[XmlName_IncludeUnknown], false);

            XmlNodeList rulesNodeList = xElement.SelectNodes(XmlName_Rule);
            List<HoursPlayedRule> rules = new List<HoursPlayedRule>();
            if (rulesNodeList != null)
            {
                foreach (XmlNode node in rulesNodeList)
                {
                    string ruleName = XmlUtil.GetStringFromNode(node[XmlName_Rule_Text], string.Empty);
                    double ruleMin = XmlUtil.GetDoubleFromNode(node[XmlName_Rule_MinHours], 0);
                    double ruleMax = XmlUtil.GetDoubleFromNode(node[XmlName_Rule_MaxHours], 0);

                    rules.Add(new HoursPlayedRule(ruleName, ruleMin, ruleMax));
                }
            }

            AutoCatHoursPlayed result = new AutoCatHoursPlayed(name, filter, prefix, includeUnknown)
            {
                Rules = rules
            };

            return result;
        }

        /// <inheritdoc />
        public override AutoCatResult CategorizeGame(GameInfo game, Filter filter)
        {
            if (games == null)
            {
                Logger.Error(GlobalStrings.Log_AutoCat_GamelistNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameList);
            }

            if (db == null)
            {
                Logger.Error(GlobalStrings.Log_AutoCat_DBNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameDB);
            }

            if (game == null)
            {
                Logger.Error(GlobalStrings.Log_AutoCat_GameNull);
                return AutoCatResult.Failure;
            }

            if (!db.Contains(game.Id))
            {
                return AutoCatResult.NotInDatabase;
            }

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            string result = null;

            foreach (HoursPlayedRule rule in Rules)
            {
                if (!CheckRule(rule, game.HoursPlayed))
                {
                    continue;
                }

                result = rule.Name;
                break;
            }

            if (result == null)
            {
                return AutoCatResult.Success;
            }

            result = GetCategoryName(result);
            game.AddCategory(games.GetCategory(result));

            return AutoCatResult.Success;
        }

        /// <inheritdoc />
        public override AutoCat Clone()
        {
            return new AutoCatHoursPlayed(this);
        }

        /// <inheritdoc />
        public override void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(XmlName_Name, Name);
            if (Filter != null)
            {
                writer.WriteElementString(XmlName_Filter, Filter);
            }

            if (Prefix != null)
            {
                writer.WriteElementString(XmlName_Prefix, Prefix);
            }

            writer.WriteElementString(XmlName_IncludeUnknown, IncludeUnknown.ToString().ToLowerInvariant());

            foreach (HoursPlayedRule rule in Rules)
            {
                writer.WriteStartElement(XmlName_Rule);
                writer.WriteElementString(XmlName_Rule_Text, rule.Name);
                writer.WriteElementString(XmlName_Rule_MinHours, rule.MinHours.ToString(CultureInfo.InvariantCulture));
                writer.WriteElementString(XmlName_Rule_MaxHours, rule.MaxHours.ToString(CultureInfo.InvariantCulture));

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        #endregion

        #region Methods

        private static bool CheckRule(HoursPlayedRule rule, double hours)
        {
            if (!(hours >= rule.MinHours))
            {
                return false;
            }

            if (hours < rule.MaxHours)
            {
                return true;
            }

            return Math.Abs(rule.MaxHours) < 0.01;
        }

        private string GetCategoryName(string name)
        {
            if (string.IsNullOrEmpty(Prefix))
            {
                return name;
            }

            return Prefix + name;
        }

        #endregion
    }
}
