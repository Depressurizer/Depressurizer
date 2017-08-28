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
    /// <summary>
    /// Autocategorization scheme that adds and removes manual categories.
    /// </summary>
    public class AutoCatManual : AutoCat
    {
        public override AutoCatType AutoCatType
        {
            get { return AutoCatType.Manual; }
        }

        // Autocat configuration
        [XmlElement("RemoveAll")]
        public bool RemoveAllCategories { get; set; }

        public string Prefix { get; set; }

        [XmlArray("Remove"), XmlArrayItem("Category")]
        public List<string> RemoveCategories { get; set; }

        [XmlArray("Add"), XmlArrayItem("Category")]
        public List<string> AddCategories { get; set; }

        // Serialization keys
        public const string TypeIdString = "AutoCatManual";

        private const string
            XmlName_Name = "Name",
            XmlName_Filter = "Filter",
            XmlName_RemoveAll = "RemoveAll",
            XmlName_Prefix = "Prefix",
            XmlName_RemoveList = "Remove",
            XmlName_RemoveItem = "Category",
            XmlName_AddList = "Add",
            XmlName_AddItem = "Category";

        private GameList gamelist;

        /// <summary>
        /// Creates a new AutoCatManual object, which removes selected (or all) categories from one list and then, optionally, assigns categories from another list.
        /// </summary>
        public AutoCatManual(string name, string filter = null, string prefix = null, bool removeAll = false,
            List<string> remove = null, List<string> add = null, bool selected = false)
            : base(name)
        {
            Filter = filter;
            Prefix = prefix;
            RemoveAllCategories = removeAll;
            RemoveCategories = (remove == null) ? new List<string>() : remove;
            AddCategories = (add == null) ? new List<string>() : add;
            Selected = selected;
        }

        //XmlSerializer requires a parameterless constructor
        private AutoCatManual() { }

        protected AutoCatManual(AutoCatManual other)
            : base(other)
        {
            Filter = other.Filter;
            Prefix = other.Prefix;
            RemoveAllCategories = other.RemoveAllCategories;
            RemoveCategories = new List<string>(other.RemoveCategories);
            AddCategories = new List<string>(other.AddCategories);
            Selected = other.Selected;
        }

        public override AutoCat Clone()
        {
            return new AutoCatManual(this);
        }

        /// <summary>
        /// Prepares to categorize games. Prepares a list of genre categories to remove. Does nothing if removeothergenres is false.
        /// </summary>
        public override void PreProcess(GameList games, GameDB db)
        {
            base.PreProcess(games, db);
            gamelist = games;
        }

        public override void DeProcess()
        {
            base.DeProcess();
            gamelist = null;
        }

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

            if (!db.Contains(game.Id) || db.Games[game.Id].LastStoreScrape == 0) return AutoCatResult.NotInDatabase;

            if (!game.IncludeGame(filter)) return AutoCatResult.Filtered;

            if (RemoveAllCategories) game.ClearCategories();
            else if (RemoveCategories != null)
            {
                List<Category> removed = new List<Category>();

                foreach (string category in RemoveCategories)
                {
                    Category c = gamelist.GetCategory(category);
                    if (game.ContainsCategory(c))
                    {
                        game.RemoveCategory(c);
                        removed.Add(c);
                    }
                }

                foreach (Category c in removed)
                {
                    if (c.Count == 0)
                    {
                        gamelist.RemoveCategory(c);
                    }
                }
            }

            if (AddCategories != null)
            {
                foreach (string category in AddCategories)
                {
                    // add Category, or create it if it doesn't exist
                    game.AddCategory(gamelist.GetCategory(GetProcessedString(category)));
                }
            }

            return AutoCatResult.Success;
        }

        private string GetProcessedString(string baseString)
        {
            if (string.IsNullOrEmpty(Prefix))
            {
                return baseString;
            }
            return Prefix + baseString;
        }

        public override void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(XmlName_Name, Name);
            if (Filter != null) writer.WriteElementString(XmlName_Filter, Filter);
            if (Prefix != null) writer.WriteElementString(XmlName_Prefix, Prefix);
            writer.WriteElementString(XmlName_RemoveAll, RemoveAllCategories.ToString().ToLowerInvariant());

            writer.WriteStartElement(XmlName_RemoveList);
            foreach (string s in RemoveCategories)
            {
                writer.WriteElementString(XmlName_RemoveItem, s);
            }
            writer.WriteEndElement();

            writer.WriteStartElement(XmlName_AddList);
            foreach (string s in AddCategories)
            {
                writer.WriteElementString(XmlName_AddItem, s);
            }
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        public static AutoCatManual LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlName_Name], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlName_Filter], null);
            bool removeAll = XmlUtil.GetBoolFromNode(xElement[XmlName_RemoveAll], false);
            string prefix = XmlUtil.GetStringFromNode(xElement[XmlName_Prefix], null);

            List<string> remove = new List<string>();

            XmlElement removeListElement = xElement[XmlName_RemoveList];
            if (removeListElement != null)
            {
                XmlNodeList removeNodes = removeListElement.SelectNodes(XmlName_RemoveItem);
                foreach (XmlNode node in removeNodes)
                {
                    string s;
                    if (XmlUtil.TryGetStringFromNode(node, out s))
                    {
                        remove.Add(s);
                    }
                }
            }

            List<string> add = new List<string>();

            XmlElement addListElement = xElement[XmlName_AddList];
            if (addListElement != null)
            {
                XmlNodeList addNodes = addListElement.SelectNodes(XmlName_AddItem);
                foreach (XmlNode node in addNodes)
                {
                    string s;
                    if (XmlUtil.TryGetStringFromNode(node, out s))
                    {
                        add.Add(s);
                    }
                }
            }

            AutoCatManual result = new AutoCatManual(name, filter, prefix, removeAll, remove, add);
            return result;
        }
    }
}