/*
This file is part of Depressurizer.
Copyright 2011, 2012, 2013 Steve Labbe.

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
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Depressurizer
{
    public class AutoCatGroup : AutoCat
    {
        #region Properties

        // Autocat configuration properties
        [XmlArrayItem("Autocat")]
        public List<string> Autocats { get; set; }

        // Meta properies
        public override AutoCatType AutoCatType
        {
            get { return AutoCatType.Group; }
        }

        public override string DisplayName
        {
            get
            {
                string displayName = Name + "[" + Autocats.Count + "]";
                if (Filter != null) displayName += "*";
                return displayName;
            }
        }

        // Serialization strings
        public const string TypeIdString = "AutoCatGroup";

        public const string
            XmlName_Name = "Name",
            XmlName_Filter = "Filter",
            XmlName_Autocats = "Autocats",
            XmlName_Autocat = "Autocat";

        #endregion

        #region Construction

        public AutoCatGroup(string name, string filter = null, List<string> autocats = null, bool selected = false)
            : base(name)
        {
            Filter = filter;
            Autocats = (autocats == null) ? new List<string>() : autocats;
            Selected = selected;
        }

        //XmlSerializer requires a parameterless constructor
        private AutoCatGroup() { }

        protected AutoCatGroup(AutoCatGroup other)
            : base(other)
        {
            Filter = other.Filter;
            Autocats = new List<string>(other.Autocats);
            Selected = other.Selected;
        }

        public override AutoCat Clone()
        {
            return new AutoCatGroup(this);
        }

        #endregion

        #region Autocategorization Methods

        public override AutoCatResult CategorizeGame(GameInfo game, Filter filter)
        {
            if (games == null)
            {
                Program.Logger.WriteError(GlobalStrings.Log_AutoCat_GamelistNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameList);
            }
            if (db == null)
            {
                Program.Logger.WriteError(GlobalStrings.Log_AutoCat_DBNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameDB);
            }
            if (game == null)
            {
                Program.Logger.WriteError(GlobalStrings.Log_AutoCat_GameNull);
                return AutoCatResult.Failure;
            }

            if (!db.Contains(game.Id)) return AutoCatResult.NotInDatabase;

            if (!game.IncludeGame(filter)) return AutoCatResult.Filtered;

            return AutoCatResult.Success;
        }

        #endregion

        #region Serialization methods

        public override void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(XmlName_Name, Name);
            if (Filter != null) writer.WriteElementString(XmlName_Filter, Filter);

            if (Autocats != null && Autocats.Count > 0)
            {
                writer.WriteStartElement(XmlName_Autocats);
                foreach (string name in Autocats)
                {
                    writer.WriteElementString(XmlName_Autocat, name);
                }
                writer.WriteEndElement();
            }

            writer.WriteEndElement(); // type ID string
        }

        public static AutoCatGroup LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlName_Name], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlName_Filter], null);
            List<string> autocats =
                XmlUtil.GetStringsFromNodeList(xElement.SelectNodes(XmlName_Autocats + "/" + XmlName_Autocat));

            return new AutoCatGroup(name, filter, autocats);
        }

        #endregion
    }
}