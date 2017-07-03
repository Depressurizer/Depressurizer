/*
    This file is part of Depressurizer.
    Original work Copyright 2011, 2012, 2013 Steve Labbe.
    Modified work Copyright 2017 Martijn Vegter.

    Depressurizer is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Depressurizer is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Depressurizer.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Xml;
using Rallion;

namespace Depressurizer.AutoCat
{
    internal class AutoCatYear : AutoCat
    {
        // Autocat configuration properties
        public string Prefix { get; set; }

        public bool IncludeUnknown { get; set; }
        public string UnknownText { get; set; }
        public AutoCatYearGrouping GroupingMode { get; set; }

        // Meta properies
        public override AutoCatType AutoCatType => AutoCatType.Year;

        // Serialization strings
        public const string TypeIdString = "AutoCatYear";

        public const string XmlNameName = "Name";
        public const string XmlNameFilter = "Filter";
        public const string XmlNamePrefix = "Prefix";
        public const string XmlNameIncludeUnknown = "IncludeUnknown";
        public const string XmlNameUnknownText = "UnknownText";
        public const string XmlNameGroupingMode = "GroupingMode";

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

        public override AutoCat Clone() => new AutoCatYear(this);

        public override AutoCatResult CategorizeGame(GameInfo game, Filter filter)
        {
            if (Games == null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.Log_AutoCat_GamelistNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameList);
            }

            if (Db == null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.Log_AutoCat_DBNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameDB);
            }

            if (game == null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.Log_AutoCat_GameNull);
                return AutoCatResult.Failure;
            }

            if (!Db.Contains(game.Id))
            {
                return AutoCatResult.NotInDatabase;
            }

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            int year = Db.GetReleaseYear(game.Id);
            if ((year > 0) || IncludeUnknown)
            {
                game.AddCategory(Games.GetCategory(GetProcessedString(year)));
            }

            return AutoCatResult.Success;
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
                    case AutoCatYearGrouping.None:
                        result = string.Empty;
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

        private static string GetRangeString(int year, int rangeSize)
        {
            int first = year - (year % rangeSize);
            return $"{first}-{(first + rangeSize) - 1}";
        }

        public override void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(XmlNameName, Name);
            if (Filter != null)
            {
                writer.WriteElementString(XmlNameFilter, Filter);
            }
            if (Prefix != null)
            {
                writer.WriteElementString(XmlNamePrefix, Prefix);
            }
            writer.WriteElementString(XmlNameIncludeUnknown, IncludeUnknown.ToString());
            writer.WriteElementString(XmlNameUnknownText, UnknownText);
            writer.WriteElementString(XmlNameGroupingMode, GroupingMode.ToString());

            writer.WriteEndElement(); // type ID string
        }

        public static AutoCatYear LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlNameName], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlNameFilter], null);
            string prefix = XmlUtil.GetStringFromNode(xElement[XmlNamePrefix], null);
            bool includeUnknown = XmlUtil.GetBoolFromNode(xElement[XmlNameIncludeUnknown], true);
            string unknownText = XmlUtil.GetStringFromNode(xElement[XmlNameUnknownText], null);
            AutoCatYearGrouping groupMode = XmlUtil.GetEnumFromNode(xElement[XmlNameGroupingMode], AutoCatYearGrouping.None);

            return new AutoCatYear(name, filter, prefix, includeUnknown, unknownText, groupMode);
        }
    }
}