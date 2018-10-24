#region LICENSE

//     This file (AutoCatFlags.cs) is part of Depressurizer.
//     Copyright (C) 2011 Steve Labbe
//     Copyright (C) 2017 Theodoros Dimos
//     Copyright (C) 2017 Martijn Vegter
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Rallion;

namespace Depressurizer
{
    public class AutoCatFlags : AutoCat
    {
        #region Constants

        // Serialization constants
        public const string TypeIdString = "AutoCatFlags";

        private const string XmlName_Name = "Name", XmlName_Filter = "Filter", XmlName_Prefix = "Prefix", XmlName_FlagList = "Flags", XmlName_Flag = "Flag";

        #endregion

        #region Constructors and Destructors

        public AutoCatFlags(string name, string filter = null, string prefix = null, List<string> flags = null, bool selected = false) : base(name)
        {
            Filter = filter;
            Prefix = prefix;
            IncludedFlags = flags == null ? new List<string>() : flags;
            Selected = selected;
        }

        protected AutoCatFlags(AutoCatFlags other) : base(other)
        {
            Filter = other.Filter;
            Prefix = other.Prefix;
            IncludedFlags = new List<string>(other.IncludedFlags);
            Selected = other.Selected;
        }

        //XmlSerializer requires a parameterless constructor
        private AutoCatFlags() { }

        #endregion

        #region Public Properties

        public override AutoCatType AutoCatType => AutoCatType.Flags;

        [XmlArray("Flags")]
        [XmlArrayItem("Flag")]
        public List<string> IncludedFlags { get; set; }

        // AutoCat configuration
        public string Prefix { get; set; }

        #endregion

        #region Public Methods and Operators

        public static AutoCatFlags LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlName_Name], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlName_Filter], null);
            string prefix = XmlUtil.GetStringFromNode(xElement[XmlName_Prefix], null);
            List<string> flags = new List<string>();

            XmlElement flagListElement = xElement[XmlName_FlagList];
            if (flagListElement != null)
            {
                XmlNodeList flagElements = flagListElement.SelectNodes(XmlName_Flag);
                foreach (XmlNode n in flagElements)
                {
                    string flag;
                    if (XmlUtil.TryGetStringFromNode(n, out flag))
                    {
                        flags.Add(flag);
                    }
                }
            }

            return new AutoCatFlags(name, filter, prefix, flags);
        }

        public override AutoCatResult CategorizeGame(GameInfo game, Filter filter)
        {
            if (games == null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.Log_AutoCat_GamelistNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameList);
            }

            if (db == null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.Log_AutoCat_DBNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameDB);
            }

            if (game == null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.Log_AutoCat_GameNull);
                return AutoCatResult.Failure;
            }

            if (!db.Contains(game.Id) || db.Games[game.Id].LastStoreScrape == 0)
            {
                return AutoCatResult.NotInDatabase;
            }

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            List<string> gameFlags = db.GetFlagList(game.Id);
            if (gameFlags == null)
            {
                gameFlags = new List<string>();
            }

            IEnumerable<string> categories = gameFlags.Intersect(IncludedFlags);

            foreach (string catString in categories)
            {
                Category c = games.GetCategory(GetProcessedString(catString));
                game.AddCategory(c);
            }

            return AutoCatResult.Success;
        }

        public override AutoCat Clone()
        {
            return new AutoCatFlags(this);
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

            writer.WriteStartElement(XmlName_FlagList);

            foreach (string s in IncludedFlags)
            {
                writer.WriteElementString(XmlName_Flag, s);
            }

            writer.WriteEndElement(); // flag list
            writer.WriteEndElement(); // type ID string
        }

        #endregion

        #region Methods

        private string GetProcessedString(string baseString)
        {
            if (string.IsNullOrEmpty(Prefix))
            {
                return baseString;
            }

            return Prefix + baseString;
        }

        #endregion
    }
}
