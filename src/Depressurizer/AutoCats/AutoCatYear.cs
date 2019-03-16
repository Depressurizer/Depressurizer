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

        // Serialization strings
        public const string TypeIdString = "AutoCatYear";

        public const string XmlName_Name = "Name", XmlName_Filter = "Filter", XmlName_Prefix = "Prefix", XmlName_IncludeUnknown = "IncludeUnknown", XmlName_UnknownText = "UnknownText", XmlName_GroupingMode = "GroupingMode";

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

        // Meta properies
        public override AutoCatType AutoCatType => AutoCatType.Year;

        public AutoCatYearGrouping GroupingMode { get; set; }

        public bool IncludeUnknown { get; set; }

        // Autocat configuration properties
        public string Prefix { get; set; }

        public string UnknownText { get; set; }

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        public static AutoCatYear LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlName_Name], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlName_Filter], null);
            string prefix = XmlUtil.GetStringFromNode(xElement[XmlName_Prefix], null);
            bool includeUnknown = XmlUtil.GetBoolFromNode(xElement[XmlName_IncludeUnknown], true);
            string unknownText = XmlUtil.GetStringFromNode(xElement[XmlName_UnknownText], null);
            AutoCatYearGrouping groupMode = XmlUtil.GetEnumFromNode(xElement[XmlName_GroupingMode], AutoCatYearGrouping.None);

            return new AutoCatYear(name, filter, prefix, includeUnknown, unknownText, groupMode);
        }

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

            int year = db.GetReleaseYear(game.Id);
            if (year > 0 || IncludeUnknown)
            {
                game.AddCategory(games.GetCategory(GetProcessedString(year)));
            }

            return AutoCatResult.Success;
        }

        public override AutoCat Clone()
        {
            return new AutoCatYear(this);
        }

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
            writer.WriteElementString(XmlName_GroupingMode, GroupingMode.ToString());

            writer.WriteEndElement(); // type ID string
        }

        #endregion

        #region Methods

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

        private string GetRangeString(int year, int rangeSize)
        {
            int first = year - year % rangeSize;
            return $"{first}-{first + rangeSize - 1}";
        }

        #endregion
    }
}
