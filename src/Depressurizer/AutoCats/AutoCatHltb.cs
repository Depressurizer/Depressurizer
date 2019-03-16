using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Depressurizer.Core.AutoCats;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;

namespace Depressurizer.AutoCats
{
    public class AutoCatHltb : AutoCat
    {
        #region Constants

        public const string TypeIdString = "AutoCatHltb";

        public const string XmlName_Filter = "Filter";

        public const string XmlName_IncludeUnknown = "IncludeUnknown";

        public const string XmlName_Name = "Name";

        public const string XmlName_Prefix = "Prefix";

        public const string XmlName_Rule = "Rule";

        public const string XmlName_Rule_MaxHours = "MaxHours";

        public const string XmlName_Rule_MinHours = "MinHours";

        public const string XmlName_Rule_Text = "Text";

        public const string XmlName_Rule_TimeType = "TimeType";

        public const string XmlName_UnknownText = "UnknownText";

        #endregion

        #region Fields

        [XmlElement("Rule")]
        public List<HowLongToBeatRule> Rules;

        #endregion

        #region Constructors and Destructors

        public AutoCatHltb(string name, string filter = null, string prefix = null, bool includeUnknown = true, string unknownText = "", List<HowLongToBeatRule> rules = null, bool selected = false) : base(name)
        {
            Filter = filter;
            Prefix = prefix;
            IncludeUnknown = includeUnknown;
            UnknownText = unknownText;
            Rules = rules ?? new List<HowLongToBeatRule>();
            Selected = selected;
        }

        public AutoCatHltb(AutoCatHltb other) : base(other)
        {
            Filter = other.Filter;
            Prefix = other.Prefix;
            IncludeUnknown = other.IncludeUnknown;
            UnknownText = other.UnknownText;
            Rules = other.Rules.ConvertAll(rule => new HowLongToBeatRule(rule));
            Selected = other.Selected;
        }

        /// <summary>
        ///     Parameter-less constructor for XmlSerializer.
        /// </summary>
        private AutoCatHltb() { }

        #endregion

        #region Public Properties

        /// <inheritdoc />
        public override AutoCatType AutoCatType => AutoCatType.Hltb;

        public bool IncludeUnknown { get; set; }

        public string UnknownText { get; set; }

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        public static AutoCatHltb LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlName_Name], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlName_Filter], null);
            string prefix = XmlUtil.GetStringFromNode(xElement[XmlName_Prefix], string.Empty);
            bool includeUnknown = XmlUtil.GetBoolFromNode(xElement[XmlName_IncludeUnknown], false);
            string unknownText = XmlUtil.GetStringFromNode(xElement[XmlName_UnknownText], string.Empty);

            XmlNodeList rulesNodeList = xElement.SelectNodes(XmlName_Rule);
            List<HowLongToBeatRule> rules = new List<HowLongToBeatRule>();
            if (rulesNodeList != null)
            {
                foreach (XmlNode node in rulesNodeList)
                {
                    string ruleName = XmlUtil.GetStringFromNode(node[XmlName_Rule_Text], string.Empty);
                    float ruleMin = XmlUtil.GetFloatFromNode(node[XmlName_Rule_MinHours], 0);
                    float ruleMax = XmlUtil.GetFloatFromNode(node[XmlName_Rule_MaxHours], 0);
                    string type = XmlUtil.GetStringFromNode(node[XmlName_Rule_TimeType], string.Empty);

                    TimeType ruleTimeType;
                    switch (type)
                    {
                        case "Extras":
                            ruleTimeType = TimeType.Extras;
                            break;
                        case "Completionist":
                            ruleTimeType = TimeType.Completionist;
                            break;
                        default:
                            ruleTimeType = TimeType.Main;
                            break;
                    }

                    rules.Add(new HowLongToBeatRule(ruleName, ruleMin, ruleMax, ruleTimeType));
                }
            }

            AutoCatHltb result = new AutoCatHltb(name, filter, prefix, includeUnknown, unknownText)
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

            if (!db.Contains(game.Id, out DatabaseEntry entry))
            {
                return AutoCatResult.NotInDatabase;
            }

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            string result = null;

            float hltbMain = entry.HltbMain / 60.0f;
            float hltbExtras = entry.HltbExtras / 60.0f;
            float hltbCompletionist = entry.HltbCompletionists / 60.0f;

            if (IncludeUnknown && hltbMain == 0.0f && hltbExtras == 0.0f && hltbCompletionist == 0.0f)
            {
                result = UnknownText;
            }
            else
            {
                foreach (HowLongToBeatRule rule in Rules)
                {
                    if (!CheckRule(rule, hltbMain, hltbExtras, hltbCompletionist))
                    {
                        continue;
                    }

                    result = rule.Name;
                    break;
                }
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
            return new AutoCatHltb(this);
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
            writer.WriteElementString(XmlName_UnknownText, UnknownText);

            foreach (HowLongToBeatRule rule in Rules)
            {
                writer.WriteStartElement(XmlName_Rule);
                writer.WriteElementString(XmlName_Rule_Text, rule.Name);
                writer.WriteElementString(XmlName_Rule_MinHours, rule.MinHours.ToString());
                writer.WriteElementString(XmlName_Rule_MaxHours, rule.MaxHours.ToString());
                writer.WriteElementString(XmlName_Rule_TimeType, rule.TimeType.ToString());

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        #endregion

        #region Methods

        private static bool CheckRule(HowLongToBeatRule rule, float hltbMain, float hltbExtras, float hltbCompletionist)
        {
            float hours;
            switch (rule.TimeType)
            {
                case TimeType.Main:
                    hours = hltbMain;
                    break;
                case TimeType.Extras:
                    hours = hltbExtras;
                    break;
                case TimeType.Completionist:
                    hours = hltbCompletionist;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (hours == 0.0f)
            {
                return false;
            }

            return hours >= rule.MinHours && (hours <= rule.MaxHours || rule.MaxHours == 0.0f);
        }

        #endregion
    }
}
