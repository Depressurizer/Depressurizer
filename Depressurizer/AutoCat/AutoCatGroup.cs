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
using System.Collections.Generic;
using System.Xml;
using Rallion;

namespace Depressurizer
{
    internal class AutoCatGroup : AutoCat
    {
        // Autocat configuration properties
        public List<string> Autocats { get; set; }

        // Meta properies
        public override AutoCatType AutoCatType => AutoCatType.Group;

        public override string DisplayName
        {
            get
            {
                string displayName = Name + "[" + Autocats.Count + "]";
                if (Filter != null)
                {
                    displayName += "*";
                }
                return displayName;
            }
        }

        // Serialization strings
        public const string TypeIdString = "AutoCatGroup";

        public const string XmlNameName = "Name";
        public const string XmlNameFilter = "Filter";
        public const string XmlNameAutocats = "Autocats";
        public const string XmlNameAutocat = "Autocat";

        public AutoCatGroup(string name, string filter = null, List<string> autocats = null, bool selected = false) : base(name)
        {
            Filter = filter;
            Autocats = autocats ?? new List<string>();
            Selected = selected;
        }

        protected AutoCatGroup(AutoCatGroup other) : base(other)
        {
            Filter = other.Filter;
            Autocats = new List<string>(other.Autocats);
            Selected = other.Selected;
        }

        public override AutoCat Clone() => new AutoCatGroup(this);

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

            return AutoCatResult.Success;
        }

        public override void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(XmlNameName, Name);
            if (Filter != null)
            {
                writer.WriteElementString(XmlNameFilter, Filter);
            }

            if ((Autocats != null) && (Autocats.Count > 0))
            {
                writer.WriteStartElement(XmlNameAutocats);
                foreach (string name in Autocats)
                {
                    writer.WriteElementString(XmlNameAutocat, name);
                }

                writer.WriteEndElement();
            }

            writer.WriteEndElement(); // type ID string
        }

        public static AutoCatGroup LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlNameName], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlNameFilter], null);
            List<string> autocats = XmlUtil.GetStringsFromNodeList(xElement.SelectNodes(XmlNameAutocats + "/" + XmlNameAutocat));

            return new AutoCatGroup(name, filter, autocats);
        }
    }
}
