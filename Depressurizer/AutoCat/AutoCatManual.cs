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
    /// <summary>
    ///     Autocategorization scheme that adds and removes manual categories.
    /// </summary>
    public class AutoCatManual : AutoCat
    {
        public override AutoCatType AutoCatType => AutoCatType.Manual;

        // Autocat configuration
        public bool RemoveAllCategories { get; set; }

        public string Prefix { get; set; }
        public bool MaxCount { get; set; }
        public int MinCount { get; set; }

        public List<string> RemoveCategories { get; set; }
        public List<string> AddCategories { get; set; }

        // Serialization keys
        public const string TypeIdString = "AutoCatManual";

        private const string XmlNameName = "Name";
        private const string XmlNameFilter = "Filter";
        private const string XmlNameRemoveAll = "RemoveAll";
        private const string XmlNamePrefix = "Prefix";
        private const string XmlNameRemoveList = "Remove";
        private const string XmlNameRemoveItem = "Category";
        private const string XmlNameAddList = "Add";
        private const string XmlNameAddItem = "Category";

        private GameList _gamelist;

        /// <summary>
        ///     Creates a new AutoCatManual object, which removes selected (or all) categories from one list and then, optionally,
        ///     assigns categories from another list.
        /// </summary>
        public AutoCatManual(string name, string filter = null, string prefix = null, bool removeAll = false, List<string> remove = null, List<string> add = null, bool selected = false) : base(name)
        {
            Filter = filter;
            Prefix = prefix;
            RemoveAllCategories = removeAll;
            RemoveCategories = remove ?? new List<string>();
            AddCategories = add ?? new List<string>();
            Selected = selected;
        }

        protected AutoCatManual(AutoCatManual other) : base(other)
        {
            Filter = other.Filter;
            Prefix = other.Prefix;
            RemoveAllCategories = other.RemoveAllCategories;
            RemoveCategories = new List<string>(other.RemoveCategories);
            AddCategories = new List<string>(other.AddCategories);
            Selected = other.Selected;
        }

        public override AutoCat Clone() => new AutoCatManual(this);

        /// <summary>
        ///     Prepares to categorize games. Prepares a list of genre categories to remove. Does nothing if removeothergenres is
        ///     false.
        /// </summary>
        public override void PreProcess(GameList games, GameDB db)
        {
            base.PreProcess(games, db);
            _gamelist = games;
        }

        public override void DeProcess()
        {
            base.DeProcess();
            _gamelist = null;
        }

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

            if (!Db.Contains(game.Id) || (Db.Games[game.Id].LastStoreScrape == 0))
            {
                return AutoCatResult.NotInDatabase;
            }

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            if (RemoveAllCategories)
            {
                game.ClearCategories();
            }
            else if (RemoveCategories != null)
            {
                List<Category> removed = new List<Category>();

                foreach (string category in RemoveCategories)
                {
                    Category c = _gamelist.GetCategory(category);
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
                        _gamelist.RemoveCategory(c);
                    }
                }
            }

            if (AddCategories != null)
            {
                foreach (string category in AddCategories)
                {
                    // add Category, or create it if it doesn't exist
                    game.AddCategory(_gamelist.GetCategory(GetProcessedString(category)));
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

            writer.WriteElementString(XmlNameName, Name);
            if (Filter != null)
            {
                writer.WriteElementString(XmlNameFilter, Filter);
            }
            if (Prefix != null)
            {
                writer.WriteElementString(XmlNamePrefix, Prefix);
            }
            writer.WriteElementString(XmlNameRemoveAll, RemoveAllCategories.ToString());

            writer.WriteStartElement(XmlNameRemoveList);
            foreach (string s in RemoveCategories)
            {
                writer.WriteElementString(XmlNameRemoveItem, s);
            }

            writer.WriteEndElement();

            writer.WriteStartElement(XmlNameAddList);
            foreach (string s in AddCategories)
            {
                writer.WriteElementString(XmlNameAddItem, s);
            }

            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        public static AutoCatManual LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlNameName], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlNameFilter], null);
            bool removeAll = XmlUtil.GetBoolFromNode(xElement[XmlNameRemoveAll], false);
            string prefix = XmlUtil.GetStringFromNode(xElement[XmlNamePrefix], null);

            List<string> remove = new List<string>();

            XmlElement removeListElement = xElement[XmlNameRemoveList];

            XmlNodeList removeNodes = removeListElement?.SelectNodes(XmlNameRemoveItem);
            if (removeNodes != null)
            {
                for (int i = 0; i < removeNodes.Count; i++)
                {
                    XmlNode node = removeNodes[i];
                    if (XmlUtil.TryGetStringFromNode(node, out string s))
                    {
                        remove.Add(s);
                    }
                }
            }

            List<string> add = new List<string>();

            XmlElement addListElement = xElement[XmlNameAddList];

            XmlNodeList addNodes = addListElement?.SelectNodes(XmlNameAddItem);
            if (addNodes != null)
            {
                for (int i = 0; i < addNodes.Count; i++)
                {
                    XmlNode node = addNodes[i];
                    if (XmlUtil.TryGetStringFromNode(node, out string s))
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