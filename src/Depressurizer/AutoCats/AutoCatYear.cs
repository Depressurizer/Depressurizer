using System;
using System.Xml;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;

namespace Depressurizer.AutoCats
{
    public class AutoCatYear : AutoCat
    {
        #region Constants

        public const string TypeIdString = "AutoCatYear";

        public const string XmlName_GroupingMode = "GroupingMode";

        public const string XmlName_IncludeUnknown = "IncludeUnknown";

        public const string XmlName_UnknownText = "UnknownText";

        #endregion

        #region Constructors and Destructors

        public AutoCatYear(string name, string filter = null, string prefix = null, bool includeUnknown = true, string unknownText = null, AutoCatYearGrouping groupMode = AutoCatYearGrouping.None, bool selected = false) : base(name)
        {
            Filter = filter;
            Prefix = prefix;
            IncludeUnknown = includeUnknown;
            UnknownText = unknownText;
            GroupingMode = groupMode;
            Selected = selected;
        }

        protected AutoCatYear(AutoCatYear other) : base(other)
        {
            Filter = other.Filter;
            Prefix = other.Prefix;
            IncludeUnknown = other.IncludeUnknown;
            UnknownText = other.UnknownText;
            GroupingMode = other.GroupingMode;
            Selected = other.Selected;
        }

        //XmlSerializer requires a parameterless constructor
        private AutoCatYear() { }

        #endregion

        #region Public Properties

        /// <inheritdoc />
        public override AutoCatType AutoCatType => AutoCatType.Year;

        public AutoCatYearGrouping GroupingMode { get; set; }

        public bool IncludeUnknown { get; set; }

        public string UnknownText { get; set; }

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        public static AutoCatYear LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[Serialization.Constants.Name], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[Serialization.Constants.Filter], null);
            string prefix = XmlUtil.GetStringFromNode(xElement[Serialization.Constants.Prefix], null);
            bool includeUnknown = XmlUtil.GetBoolFromNode(xElement[XmlName_IncludeUnknown], true);
            string unknownText = XmlUtil.GetStringFromNode(xElement[XmlName_UnknownText], null);
            AutoCatYearGrouping groupMode = XmlUtil.GetEnumFromNode(xElement[XmlName_GroupingMode], AutoCatYearGrouping.None);

            return new AutoCatYear(name, filter, prefix, includeUnknown, unknownText, groupMode);
        }

        /// <inheritdoc />
        public override AutoCatResult CategorizeGame(GameInfo game, Filter filter)
        {
            if (games == null)
            {
                Logger.Error(GlobalStrings.Log_AutoCat_GamelistNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameList);
            }

            if (game == null)
            {
                Logger.Error(GlobalStrings.Log_AutoCat_GameNull);
                return AutoCatResult.Failure;
            }

            if (!Database.Contains(game.Id))
            {
                return AutoCatResult.NotInDatabase;
            }

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            int year = Database.GetReleaseYear(game.Id);
            if (year > 0 || IncludeUnknown)
            {
                game.AddCategory(games.GetCategory(GetProcessedString(year)));
            }

            return AutoCatResult.Success;
        }

        /// <inheritdoc />
        public override AutoCat Clone()
        {
            return new AutoCatYear(this);
        }

        /// <inheritdoc />
        public override void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(Serialization.Constants.Name, Name);
            if (Filter != null)
            {
                writer.WriteElementString(Serialization.Constants.Filter, Filter);
            }

            if (Prefix != null)
            {
                writer.WriteElementString(Serialization.Constants.Prefix, Prefix);
            }

            writer.WriteElementString(XmlName_IncludeUnknown, IncludeUnknown.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlName_UnknownText, UnknownText);
            writer.WriteElementString(XmlName_GroupingMode, GroupingMode.ToString());

            writer.WriteEndElement(); // type ID string
        }

        #endregion

        #region Methods

        private static string GetRangeString(int year, int rangeSize)
        {
            int first = year - year % rangeSize;
            return $"{first}-{first + rangeSize - 1}";
        }

        private string GetProcessedString(int year)
        {
            string result;
            if (year <= 0)
            {
                result = UnknownText;
            }
            else
            {
                switch (GroupingMode)
                {
                    case AutoCatYearGrouping.Decade:
                        result = GetRangeString(year, 10);
                        break;
                    case AutoCatYearGrouping.HalfDecade:
                        result = GetRangeString(year, 5);
                        break;
                    default:
                        result = year.ToString();
                        break;
                }
            }

            if (string.IsNullOrEmpty(Prefix))
            {
                return result;
            }

            return Prefix + result;
        }

        #endregion
    }
}
