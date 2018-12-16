using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;
using Depressurizer.Helpers;
using Depressurizer.Models;

namespace Depressurizer
{
    public class AutoCatTags : AutoCat
    {
        #region Constants

        public const string TypeIdString = "AutoCatTags";

        private const string XmlName_Name = "Name", XmlName_Filter = "Filter", XmlName_Prefix = "Prefix", XmlName_TagList = "Tags", XmlName_Tag = "Tag", XmlName_MaxTags = "MaxTags", XmlName_ListOwnedOnly = "List_OwnedOnly", XmlName_ListWeightFactor = "List_WeightedScore", XmlName_ListMinScore = "List_MinScore", XmlName_ListTagsPerGame = "List_TagsPerGame", XmlName_ListExcludeGenres = "List_ExcludeGenres", XmlName_ListScoreSort = "List_ScoreSort";

        #endregion

        #region Constructors and Destructors

        public AutoCatTags(string name, string filter = null, string prefix = null, HashSet<string> tags = null, int maxTags = 0, bool listOwnedOnly = true, float listWeightFactor = 1, int listMinScore = 0, int listTagsPerGame = 0, bool listScoreSort = true, bool listExcludeGenres = false, bool selected = false) : base(name)
        {
            Filter = filter;
            Prefix = prefix;

            if (tags == null)
            {
                IncludedTags = new HashSet<string>();
            }
            else
            {
                IncludedTags = tags;
            }

            MaxTags = maxTags;
            List_OwnedOnly = listOwnedOnly;
            List_WeightFactor = listWeightFactor;
            List_MinScore = listMinScore;
            List_TagsPerGame = listTagsPerGame;
            List_ScoreSort = listScoreSort;
            List_ExcludeGenres = listExcludeGenres;
            Selected = selected;
        }

        protected AutoCatTags(AutoCatTags other) : base(other)
        {
            Filter = other.Filter;
            Prefix = other.Prefix;
            IncludedTags = new HashSet<string>(other.IncludedTags);
            MaxTags = other.MaxTags;

            List_OwnedOnly = other.List_OwnedOnly;
            List_WeightFactor = other.List_WeightFactor;
            List_MinScore = other.List_MinScore;
            List_TagsPerGame = other.List_TagsPerGame;
            List_ScoreSort = other.List_ScoreSort;
            List_ExcludeGenres = other.List_ExcludeGenres;
            Selected = other.Selected;
        }

        //XmlSerializer requires a parameterless constructor
        private AutoCatTags() { }

        #endregion

        #region Public Properties

        public override AutoCatType AutoCatType => AutoCatType.Tags;

        [XmlArray("Tags")]
        [XmlArrayItem("Tag")]
        public HashSet<string> IncludedTags { get; set; }

        public bool List_ExcludeGenres { get; set; }

        public int List_MinScore { get; set; }

        public bool List_OwnedOnly { get; set; }

        public bool List_ScoreSort { get; set; }

        public int List_TagsPerGame { get; set; }

        public float List_WeightFactor { get; set; }

        public int MaxTags { get; set; }

        public string Prefix { get; set; }

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        public static AutoCatTags LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlName_Name], TypeIdString);

            AutoCatTags result = new AutoCatTags(name)
            {
                Filter = XmlUtil.GetStringFromNode(xElement[XmlName_Filter], null)
            };

            if (XmlUtil.TryGetStringFromNode(xElement[XmlName_Prefix], out string prefix))
            {
                result.Prefix = prefix;
            }

            if (XmlUtil.TryGetIntFromNode(xElement[XmlName_MaxTags], out int maxTags))
            {
                result.MaxTags = maxTags;
            }

            if (XmlUtil.TryGetBoolFromNode(xElement[XmlName_ListOwnedOnly], out bool listOwnedOnly))
            {
                result.List_OwnedOnly = listOwnedOnly;
            }

            if (XmlUtil.TryGetFloatFromNode(xElement[XmlName_ListWeightFactor], out float listWeightFactor))
            {
                result.List_WeightFactor = listWeightFactor;
            }

            if (XmlUtil.TryGetIntFromNode(xElement[XmlName_ListMinScore], out int listMinScore))
            {
                result.List_MinScore = listMinScore;
            }

            if (XmlUtil.TryGetIntFromNode(xElement[XmlName_ListTagsPerGame], out int listTagsPerGame))
            {
                result.List_TagsPerGame = listTagsPerGame;
            }

            if (XmlUtil.TryGetBoolFromNode(xElement[XmlName_ListScoreSort], out bool listScoreSort))
            {
                result.List_ScoreSort = listScoreSort;
            }

            if (XmlUtil.TryGetBoolFromNode(xElement[XmlName_ListExcludeGenres], out bool listExcludeGenres))
            {
                result.List_ExcludeGenres = listExcludeGenres;
            }

            List<string> tagList = XmlUtil.GetStringsFromNodeList(xElement.SelectNodes(XmlName_TagList + "/" + XmlName_Tag));
            result.IncludedTags = tagList == null ? new HashSet<string>() : new HashSet<string>(tagList);

            return result;
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

            if (!db.Contains(game.Id, out DatabaseEntry entry) || entry.LastStoreScrape == 0)
            {
                return AutoCatResult.NotInDatabase;
            }

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            Collection<string> gameTags = db.GetTagList(game.Id);

            int added = 0;
            for (int index = 0; index < gameTags.Count && (MaxTags == 0 || added < MaxTags); index++)
            {
                if (!IncludedTags.Contains(gameTags[index]))
                {
                    continue;
                }

                game.AddCategory(games.GetCategory(GetProcessedString(gameTags[index])));
                added++;
            }

            return AutoCatResult.Success;
        }

        public override AutoCat Clone()
        {
            return new AutoCatTags(this);
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

            writer.WriteElementString(XmlName_Name, Name);
            if (Filter != null)
            {
                writer.WriteElementString(XmlName_Filter, Filter);
            }

            if (Prefix != null)
            {
                writer.WriteElementString(XmlName_Prefix, Prefix);
            }

            writer.WriteElementString(XmlName_MaxTags, MaxTags.ToString());

            if (IncludedTags != null && IncludedTags.Count > 0)
            {
                writer.WriteStartElement(XmlName_TagList);
                foreach (string s in IncludedTags)
                {
                    writer.WriteElementString(XmlName_Tag, s);
                }

                writer.WriteEndElement();
            }

            writer.WriteElementString(XmlName_ListOwnedOnly, List_OwnedOnly.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlName_ListWeightFactor, List_WeightFactor.ToString());
            writer.WriteElementString(XmlName_ListMinScore, List_MinScore.ToString());
            writer.WriteElementString(XmlName_ListTagsPerGame, List_TagsPerGame.ToString());
            writer.WriteElementString(XmlName_ListScoreSort, List_ScoreSort.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlName_ListExcludeGenres, List_ExcludeGenres.ToString().ToLowerInvariant());

            writer.WriteEndElement();
        }

        #endregion
    }
}
