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
    public class AutoCatTags : AutoCat
    {
        public override AutoCatType AutoCatType => AutoCatType.Tags;

        public string Prefix { get; set; }
        public int MaxTags { get; set; }
        public HashSet<string> IncludedTags { get; set; }

        public bool ListOwnedOnly { get; set; }
        public float ListWeightFactor { get; set; }
        public int ListMinScore { get; set; }
        public int ListTagsPerGame { get; set; }
        public bool ListScoreSort { get; set; }
        public bool ListExcludeGenres { get; set; }

        public const string TypeIdString = "AutoCatTags";

        private const string XmlNameName = "Name";
        private const string XmlNameFilter = "Filter";
        private const string XmlNamePrefix = "Prefix";
        private const string XmlNameTagList = "Tags";
        private const string XmlNameTag = "Tag";
        private const string XmlNameMaxTags = "MaxTags";
        private const string XmlNameListOwnedOnly = "List_OwnedOnly";
        private const string XmlNameListWeightFactor = "List_WeightedScore";
        private const string XmlNameListMinScore = "List_MinScore";
        private const string XmlNameListTagsPerGame = "List_TagsPerGame";
        private const string XmlNameListExcludeGenres = "List_ExcludeGenres";
        private const string XmlNameListScoreSort = "List_ScoreSort";

        public AutoCatTags(string name, string filter = null, string prefix = null, HashSet<string> tags = null, int maxTags = 0, bool listOwnedOnly = true, float listWeightFactor = 1, int listMinScore = 0, int listTagsPerGame = 0, bool listScoreSort = true, bool listExcludeGenres = true, bool selected = false) : base(name)
        {
            Filter = filter;
            Prefix = prefix;

            IncludedTags = tags ?? new HashSet<string>();

            MaxTags = maxTags;
            ListOwnedOnly = listOwnedOnly;
            ListWeightFactor = listWeightFactor;
            ListMinScore = listMinScore;
            ListTagsPerGame = listTagsPerGame;
            ListScoreSort = listScoreSort;
            ListExcludeGenres = listExcludeGenres;
            Selected = selected;
        }

        protected AutoCatTags(AutoCatTags other) : base(other)
        {
            Filter = other.Filter;
            Prefix = other.Prefix;
            IncludedTags = new HashSet<string>(other.IncludedTags);
            MaxTags = other.MaxTags;

            ListOwnedOnly = other.ListOwnedOnly;
            ListWeightFactor = other.ListWeightFactor;
            ListMinScore = other.ListMinScore;
            ListTagsPerGame = other.ListTagsPerGame;
            ListScoreSort = other.ListScoreSort;
            ListExcludeGenres = other.ListExcludeGenres;
            Selected = other.Selected;
        }

        public override AutoCat Clone() => new AutoCatTags(this);

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

            List<string> gameTags = Db.GetTagList(game.Id);

            if (gameTags != null)
            {
                int added = 0;
                for (int index = 0; (index < gameTags.Count) && ((MaxTags == 0) || (added < MaxTags)); index++)
                {
                    if (IncludedTags.Contains(gameTags[index]))
                    {
                        game.AddCategory(Games.GetCategory(GetProcessedString(gameTags[index])));
                        added++;
                    }
                }
            }

            return AutoCatResult.Success;
        }

        public string GetProcessedString(string s)
        {
            if (string.IsNullOrEmpty(Prefix))
            {
                return s;
            }

            return Prefix + s;
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
            writer.WriteElementString(XmlNameMaxTags, MaxTags.ToString());

            if ((IncludedTags != null) && (IncludedTags.Count > 0))
            {
                writer.WriteStartElement(XmlNameTagList);
                foreach (string s in IncludedTags)
                {
                    writer.WriteElementString(XmlNameTag, s);
                }

                writer.WriteEndElement();
            }

            writer.WriteElementString(XmlNameListOwnedOnly, ListOwnedOnly.ToString());
            writer.WriteElementString(XmlNameListWeightFactor, ListWeightFactor.ToString());
            writer.WriteElementString(XmlNameListMinScore, ListMinScore.ToString());
            writer.WriteElementString(XmlNameListTagsPerGame, ListTagsPerGame.ToString());
            writer.WriteElementString(XmlNameListScoreSort, ListScoreSort.ToString());
            writer.WriteElementString(XmlNameListExcludeGenres, ListExcludeGenres.ToString());

            writer.WriteEndElement();
        }

        public static AutoCatTags LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlNameName], TypeIdString);

            AutoCatTags result = new AutoCatTags(name);

            result.Filter = XmlUtil.GetStringFromNode(xElement[XmlNameFilter], null);

            if (XmlUtil.TryGetStringFromNode(xElement[XmlNamePrefix], out string prefix))
            {
                result.Prefix = prefix;
            }

            if (XmlUtil.TryGetIntFromNode(xElement[XmlNameMaxTags], out int maxTags))
            {
                result.MaxTags = maxTags;
            }

            if (XmlUtil.TryGetBoolFromNode(xElement[XmlNameListOwnedOnly], out bool listOwnedOnly))
            {
                result.ListOwnedOnly = listOwnedOnly;
            }

            if (XmlUtil.TryGetFloatFromNode(xElement[XmlNameListWeightFactor], out float listWeightFactor))
            {
                result.ListWeightFactor = listWeightFactor;
            }

            if (XmlUtil.TryGetIntFromNode(xElement[XmlNameListMinScore], out int listMinScore))
            {
                result.ListMinScore = listMinScore;
            }

            if (XmlUtil.TryGetIntFromNode(xElement[XmlNameListTagsPerGame], out int listTagsPerGame))
            {
                result.ListTagsPerGame = listTagsPerGame;
            }

            if (XmlUtil.TryGetBoolFromNode(xElement[XmlNameListScoreSort], out bool listScoreSort))
            {
                result.ListScoreSort = listScoreSort;
            }

            if (XmlUtil.TryGetBoolFromNode(xElement[XmlNameListExcludeGenres], out bool listExcludeGenres))
            {
                result.ListExcludeGenres = listExcludeGenres;
            }

            List<string> tagList = XmlUtil.GetStringsFromNodeList(xElement.SelectNodes(XmlNameTagList + "/" + XmlNameTag));
            result.IncludedTags = tagList == null ? new HashSet<string>() : new HashSet<string>(tagList);

            return result;
        }
    }
}