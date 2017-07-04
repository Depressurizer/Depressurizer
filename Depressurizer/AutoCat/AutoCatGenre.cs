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
using Depressurizer.Lib;

namespace Depressurizer.AutoCat
{
    /// <summary>
    ///     Autocategorization scheme that adds genre categories.
    /// </summary>
    public class AutoCatGenre : AutoCat
    {
        public override AutoCatType AutoCatType => AutoCatType.Genre;

        // Autocat configuration
        public int MaxCategories { get; set; }

        public bool RemoveOtherGenres { get; set; }
        public bool TagFallback { get; set; }
        public string Prefix { get; set; }

        public List<string> IgnoredGenres { get; set; }

        // Serialization keys
        public const string TypeIdString = "AutoCatGenre";

        private const string XmlNameName = "Name";
        private const string XmlNameFilter = "Filter";
        private const string XmlNameRemOther = "RemoveOthers";
        private const string XmlNameTagFallback = "TagFallback";
        private const string XmlNameMaxCats = "MaxCategories";
        private const string XmlNamePrefix = "Prefix";
        private const string XmlNameIgnoreList = "Ignored";
        private const string XmlNameIgnoreItem = "Ignore";

        private const int MaxParentDepth = 3;

        private SortedSet<Category> _genreCategories;

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filter"></param>
        /// <param name="prefix"></param>
        /// <param name="maxCategories"></param>
        /// <param name="removeOthers"></param>
        /// <param name="tagFallback"></param>
        /// <param name="ignore"></param>
        /// <param name="selected"></param>
        public AutoCatGenre(string name, string filter = null, string prefix = null, int maxCategories = 0,
            bool removeOthers = false, bool tagFallback = true, List<string> ignore = null,
            bool selected = false) : base(name)
        {
            Filter = filter;
            MaxCategories = maxCategories;
            RemoveOtherGenres = removeOthers;
            TagFallback = tagFallback;
            Prefix = prefix;
            IgnoredGenres = ignore ?? new List<string>();
            Selected = selected;
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        protected AutoCatGenre(AutoCatGenre other) : base(other)
        {
            Filter = other.Filter;
            MaxCategories = other.MaxCategories;
            RemoveOtherGenres = other.RemoveOtherGenres;
            TagFallback = other.TagFallback;
            Prefix = other.Prefix;
            IgnoredGenres = new List<string>(other.IgnoredGenres);
            Selected = other.Selected;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override AutoCat Clone() => new AutoCatGenre(this);

        /// <summary>
        /// </summary>
        /// <param name="games"></param>
        /// <param name="db"></param>
        public override void PreProcess(GameList games, GameDB db)
        {
            base.PreProcess(games, db);
            if (RemoveOtherGenres)
            {
                SortedSet<string> genreStrings = db.GetAllGenres();
                _genreCategories = new SortedSet<Category>();

                foreach (string cStr in genreStrings)
                {
                    if (games.CategoryExists(string.IsNullOrEmpty(Prefix) ? cStr : Prefix + cStr) &&
                        !IgnoredGenres.Contains(cStr))
                    {
                        _genreCategories.Add(games.GetCategory(cStr));
                    }
                }
            }
        }

        /// <summary>
        /// </summary>
        public override void DeProcess()
        {
            base.DeProcess();
            _genreCategories = null;
        }

        /// <summary>
        /// </summary>
        /// <param name="game"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
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

            if (RemoveOtherGenres && (_genreCategories != null))
            {
                game.RemoveCategory(_genreCategories);
            }

            List<string> genreList = Db.GetGenreList(game.Id, MaxParentDepth, TagFallback);
            if ((genreList != null) && (genreList.Count > 0))
            {
                List<Category> categories = new List<Category>();
                int max = MaxCategories;
                for (int i = 0; (i < genreList.Count) && ((MaxCategories == 0) || (i < max)); i++)
                {
                    if (!IgnoredGenres.Contains(genreList[i]))
                    {
                        categories.Add(Games.GetCategory(GetProcessedString(genreList[i])));
                    }
                    else
                    {
                        max++; // ignored genres don't contribute to max
                    }
                }

                game.AddCategory(categories);
            }

            return AutoCatResult.Success;
        }

        /// <summary>
        /// </summary>
        /// <param name="baseString"></param>
        /// <returns></returns>
        private string GetProcessedString(string baseString)
        {
            if (string.IsNullOrEmpty(Prefix))
            {
                return baseString;
            }

            return Prefix + baseString;
        }

        /// <summary>
        /// </summary>
        /// <param name="writer"></param>
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
            writer.WriteElementString(XmlNameMaxCats, MaxCategories.ToString());
            writer.WriteElementString(XmlNameRemOther, RemoveOtherGenres.ToString());
            writer.WriteElementString(XmlNameTagFallback, TagFallback.ToString());

            writer.WriteStartElement(XmlNameIgnoreList);

            foreach (string s in IgnoredGenres)
            {
                writer.WriteElementString(XmlNameIgnoreItem, s);
            }

            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        /// <summary>
        /// </summary>
        /// <param name="xElement"></param>
        /// <returns></returns>
        public static AutoCatGenre LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlNameName], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlNameFilter], null);
            int maxCats = XmlUtil.GetIntFromNode(xElement[XmlNameMaxCats], 0);
            bool remOther = XmlUtil.GetBoolFromNode(xElement[XmlNameRemOther], false);
            bool tagFallback = XmlUtil.GetBoolFromNode(xElement[XmlNameTagFallback], true);
            string prefix = XmlUtil.GetStringFromNode(xElement[XmlNamePrefix], null);

            List<string> ignore = new List<string>();

            XmlElement ignoreListElement = xElement[XmlNameIgnoreList];

            XmlNodeList ignoreNodes = ignoreListElement?.SelectNodes(XmlNameIgnoreItem);
            if (ignoreNodes != null)
            {
                for (int i = 0; i < ignoreNodes.Count; i++)
                {
                    XmlNode node = ignoreNodes[i];
                    if (XmlUtil.TryGetStringFromNode(node, out string s))
                    {
                        ignore.Add(s);
                    }
                }
            }

            AutoCatGenre result = new AutoCatGenre(name, filter, prefix, maxCats, remOther, tagFallback, ignore);
            return result;
        }
    }
}