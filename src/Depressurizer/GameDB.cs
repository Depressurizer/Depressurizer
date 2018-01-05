﻿/*
This file is part of Depressurizer.
Original work Copyright 2011, 2012, 2013 Steve Labbe.
Modified work Copyright 2017 Theodoros Dimos and Martijn Vegter.

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
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Depressurizer.Properties;
using DepressurizerCore.Models;
using Newtonsoft.Json.Linq;
using Rallion;

namespace Depressurizer
{
    public class GameDB
    {
        // Main Data
        public Dictionary<int, GameDBEntry> Games = new Dictionary<int, GameDBEntry>();

        // Extra data
        private SortedSet<string> allStoreGenres;

        private SortedSet<string> allStoreFlags;
        private SortedSet<string> allStoreDevelopers;
        private SortedSet<string> allStorePublishers;
        private VRSupport allVrSupportFlags;
        private LanguageSupport allLanguages;
        public int LastHltbUpdate;

        public StoreLanguage dbLanguage = StoreLanguage.en;

        // Utility
        private const int VERSION = 2;

        private const string
            XmlName_RootNode = "gamelist",
            XmlName_Version = "version",
            XmlName_LastHltbUpdate = "lastHltbUpdate",
            XmlName_dbLanguage = "dbLanguage",
            XmlName_Games = "games";
            

        #region Accessors

        public bool Contains(int id)
        {
            return Games.ContainsKey(id);
        }

        public bool IncludeItemInGameList(int id, AppTypes scheme)
        {
            if (Games.ContainsKey(id))
            {
                return scheme.HasFlag(Games[id].AppType);
            }
            return scheme.HasFlag(AppTypes.Unknown);
        }

        public string GetName(int id)
        {
            if (Games.ContainsKey(id))
            {
                return Games[id].Name;
            }
            return null;
        }

        public List<string> GetGenreList(int gameId, int depth = 3, bool tagFallback = true)
        {
            if (Games.ContainsKey(gameId))
            {
                List<string> res = Games[gameId].Genres;
                if (tagFallback && (res == null || res.Count == 0))
                {
                    List<string> tags = GetTagList(gameId, 0);
                    if (tags != null && tags.Count > 0)
                    {
                        res = new List<string>(tags.Intersect(GetAllGenres()));
                    }
                }
                if ((res == null || res.Count == 0) && depth > 0 && Games[gameId].ParentId > 0)
                {
                    res = GetGenreList(Games[gameId].ParentId, depth - 1, tagFallback);
                }
                return res;
            }
            return null;
        }

        public List<string> GetFlagList(int gameId, int depth = 3)
        {
            if (Games.ContainsKey(gameId))
            {
                List<string> res = Games[gameId].Flags;
                if ((res == null || res.Count == 0) && depth > 0 && Games[gameId].ParentId > 0)
                {
                    res = GetFlagList(Games[gameId].ParentId, depth - 1);
                }
                return res;
            }
            return null;
        }

        public List<string> GetTagList(int gameId, int depth = 3)
        {
            if (Games.ContainsKey(gameId))
            {
                List<string> res = Games[gameId].Tags;
                if ((res == null || res.Count == 0) && depth > 0 && Games[gameId].ParentId > 0)
                {
                    res = GetTagList(Games[gameId].ParentId, depth - 1);
                }
                return res;
            }
            return null;
        }

        /// <summary>
        ///     Returns whether the game supports VR
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public bool SupportsVR(int appId, int depth = 3)
        {
            if (!Games.ContainsKey(appId))
            {
                return false;
            }

            VRSupport vrSupport = Games[appId].VRSupport;
            if (vrSupport.Headsets.Count > 0 || vrSupport.Input.Count > 0 || vrSupport.PlayArea.Count > 0 && depth > 0 && Games[appId].ParentId > 0)
            {
                return true;
            }

            if (depth > 0 && Games[appId].ParentId > 0)
            {
                return SupportsVR(Games[appId].ParentId, depth - 1);
            }

            return false;
        }

        public VRSupport GetVrSupport(int gameId, int depth = 3)
        {
            if (Games.ContainsKey(gameId))
            {
                VRSupport res = Games[gameId].VRSupport;
                if ((res.Headsets == null || res.Headsets.Count == 0) && (res.Input == null || res.Input.Count == 0) &&
                    (res.PlayArea == null || res.PlayArea.Count == 0) && depth > 0 && Games[gameId].ParentId > 0)
                {
                    res = GetVrSupport(Games[gameId].ParentId, depth - 1);
                }
                return res;
            }
            return new VRSupport();
        }


        public List<string> GetDevelopers(int gameId, int depth = 3)
        {
            if (Games.ContainsKey(gameId))
            {
                List<string> res = Games[gameId].Developers;
                if ((res == null || res.Count == 0) && depth > 0 && Games[gameId].ParentId > 0)
                {
                    res = GetDevelopers(Games[gameId].ParentId, depth - 1);
                }
                return res;
            }
            return null;
        }

        public List<string> GetPublishers(int gameId, int depth = 3)
        {
            if (Games.ContainsKey(gameId))
            {
                List<string> res = Games[gameId].Publishers;
                if ((res == null || res.Count == 0) && depth > 0 && Games[gameId].ParentId > 0)
                {
                    res = GetPublishers(Games[gameId].ParentId, depth - 1);
                }
                return res;
            }
            return null;
        }

        public int GetReleaseYear(int gameId)
        {
            if (Games.ContainsKey(gameId))
            {
                GameDBEntry dbEntry = Games[gameId];
                DateTime releaseDate;
                if (DateTime.TryParse(dbEntry.SteamReleaseDate, out releaseDate))
                {
                    return releaseDate.Year;
                }
            }
            return 0;
        }

        #endregion

        #region Aggregate Accessors

        /// <summary>
        /// Gets a list of all Steam store genres found in the entire database.
        /// Only recalculates if necessary.
        /// </summary>
        /// <returns>A set of genres, as strings</returns>
        public SortedSet<string> GetAllGenres()
        {
            if (allStoreGenres == null)
            {
                return CalculateAllGenres();
            }
            return allStoreGenres;
        }

        /// <summary>
        /// Gets a list of all Steam store genres found in the entire database.
        /// Always recalculates.
        /// </summary>
        /// <returns>A set of genres, as strings</returns>
        public SortedSet<string> CalculateAllGenres()
        {
            if (allStoreGenres == null)
            {
                allStoreGenres = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                allStoreGenres.Clear();
            }

            foreach (GameDBEntry entry in Games.Values)
            {
                if (entry.Genres != null)
                {
                    allStoreGenres.UnionWith(entry.Genres);
                }
            }

            return allStoreGenres;
        }

        /// <summary>
        /// Gets a list of all Steam store developers found in the entire database.
        /// Only recalculates if necessary.
        /// </summary>
        /// <returns>A set of developers, as strings</returns>
        public SortedSet<string> GetAllDevelopers()
        {
            if (allStoreDevelopers == null)
            {
                return CalculateAllDevelopers();
            }
            return allStoreDevelopers;
        }

        /// <summary>
        /// Gets a list of all Steam store developers found in the entire database.
        /// Always recalculates.
        /// </summary>
        /// <returns>A set of developers, as strings</returns>
        public SortedSet<string> CalculateAllDevelopers()
        {
            if (allStoreDevelopers == null)
            {
                allStoreDevelopers = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                allStoreDevelopers.Clear();
            }

            foreach (GameDBEntry entry in Games.Values)
            {
                if (entry.Developers != null)
                {
                    allStoreDevelopers.UnionWith(entry.Developers);
                }
            }

            return allStoreDevelopers;
        }

        /// <summary>
        /// Gets a list of all Steam store publishers found in the entire database.
        /// Only recalculates if necessary.
        /// </summary>
        /// <returns>A set of publishers, as strings</returns>
        public SortedSet<string> GetAllPublishers()
        {
            if (allStorePublishers == null)
            {
                return CalculateAllPublishers();
            }
            return allStorePublishers;
        }

        /// <summary>
        /// Gets a list of all Steam store publishers found in the entire database.
        /// Always recalculates.
        /// </summary>
        /// <returns>A set of publishers, as strings</returns>
        public SortedSet<string> CalculateAllPublishers()
        {
            if (allStorePublishers == null)
            {
                allStorePublishers = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                allStorePublishers.Clear();
            }

            foreach (GameDBEntry entry in Games.Values)
            {
                if (entry.Publishers != null)
                {
                    allStorePublishers.UnionWith(entry.Publishers);
                }
            }

            return allStorePublishers;
        }

        /// <summary>
        /// Gets a list of all Steam store flags found in the entire database.
        /// Only recalculates if necessary.
        /// </summary>
        /// <returns>A set of genres, as strings</returns>
        public SortedSet<string> GetAllStoreFlags()
        {
            if (allStoreFlags == null)
            {
                return CalculateAllStoreFlags();
            }
            return allStoreFlags;
        }

        /// <summary>
        /// Gets a list of all Steam store flags found in the entire database.
        /// Always recalculates.
        /// </summary>
        /// <returns>A set of genres, as strings</returns>
        public SortedSet<string> CalculateAllStoreFlags()
        {
            if (allStoreFlags == null)
            {
                allStoreFlags = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                allStoreFlags.Clear();
            }

            foreach (GameDBEntry entry in Games.Values)
            {
                if (entry.Flags != null)
                {
                    allStoreFlags.UnionWith(entry.Flags);
                }
            }
            return allStoreFlags;
        }

        /// <summary>
        /// Gets a list of all Steam store VR Support flags found in the entire database.
        /// Only recalculates if necessary.
        /// </summary>
        /// <returns>A VRSupport struct containing the flags</returns>
        public VRSupport GetAllVrSupportFlags()
        {
            if (allVrSupportFlags.Headsets == null || allVrSupportFlags.Input == null ||
                allVrSupportFlags.PlayArea == null)
            {
                return CalculateAllVrSupportFlags();
            }
            return allVrSupportFlags;
        }

        /// <summary>
        /// Gets a list of all Steam store VR Support flags found in the entire database.
        /// Always recalculates.
        /// </summary>
        /// <returns>A VRSupport struct containing the flags</returns>
        public VRSupport CalculateAllVrSupportFlags()
        {
            SortedSet<string> headsets = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> input = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> playArea = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (GameDBEntry entry in Games.Values)
            {
                if (entry.VRSupport.Headsets != null)
                {
                    headsets.UnionWith(entry.VRSupport.Headsets);
                }
                if (entry.VRSupport.Input != null)
                {
                    input.UnionWith(entry.VRSupport.Input);
                }
                if (entry.VRSupport.PlayArea != null)
                {
                    playArea.UnionWith(entry.VRSupport.PlayArea);
                }
            }
            allVrSupportFlags.Headsets = headsets.ToList();
            allVrSupportFlags.Input = input.ToList();
            allVrSupportFlags.PlayArea = playArea.ToList();
            return allVrSupportFlags;
        }

        /// <summary>
        /// Gets a list of all Game Languages found in the entire database.
        /// Only recalculates if necessary.
        /// </summary>
        /// <returns>A LanguageSupport struct containing the languages</returns>
        public LanguageSupport GetAllLanguages()
        {
            if (allLanguages.FullAudio == null || allLanguages.Interface == null ||
                allLanguages.Subtitles == null)
            {
                return CalculateAllLanguages();
            }
            return allLanguages;
        }

        /// <summary>
        /// Gets a list of all Game Languages found in the entire database.
        /// Always recalculates.
        /// </summary>
        /// <returns>A LanguageSupport struct containing the languages</returns>
        public LanguageSupport CalculateAllLanguages()
        {
            SortedSet<string> Interface = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> Subtitles = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> FullAudio = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (GameDBEntry entry in Games.Values)
            {
                if (entry.LanguageSupport.Interface != null)
                {
                    Interface.UnionWith(entry.LanguageSupport.Interface);
                }
                if (entry.LanguageSupport.Subtitles != null)
                {
                    Subtitles.UnionWith(entry.LanguageSupport.Subtitles);
                }
                if (entry.LanguageSupport.FullAudio != null)
                {
                    FullAudio.UnionWith(entry.LanguageSupport.FullAudio);
                }
            }
            allLanguages.Interface = Interface.ToList();
            allLanguages.Subtitles = Subtitles.ToList();
            allLanguages.FullAudio = FullAudio.ToList();
            return allLanguages;
        }

        /// <summary>
        /// Gets a list of developers found on games with their game count.
        /// </summary>
        /// <param name="filter">GameList including games to include in the search. If null, finds developers for all games in the database.</param>
        /// <param name="minScore">Minimum count of developers games to include in the result list. Developers with lower game counts will be discarded.</param>
        /// <returns>List of developers, as strings with game counts</returns>
        public IEnumerable<Tuple<string, int>> CalculateSortedDevList(GameList filter, int minCount)
        {
            SortedSet<string> developers = GetAllDevelopers();
            Dictionary<string, int> devCounts = new Dictionary<string, int>();
            if (filter == null)
            {
                foreach (GameDBEntry dbEntry in Games.Values)
                {
                    CalculateSortedDevListHelper(devCounts, dbEntry);
                }
            }
            else
            {
                foreach (int gameId in filter.Games.Keys)
                {
                    if (Games.ContainsKey(gameId) && !filter.Games[gameId].Hidden)
                    {
                        CalculateSortedDevListHelper(devCounts, Games[gameId]);
                    }
                }
            }

            var unsortedList = (from entry in devCounts
                where entry.Value >= minCount
                select new Tuple<string, int>(entry.Key, entry.Value));
            return unsortedList.ToList();
        }

        /// <summary>
        /// Counts games for each developer.
        /// </summary>
        /// <param name="counts">Existing dictionary of developers and game count. Key is the developer as a string, value is the count</param>
        /// <param name="dbEntry">Entry to add developers from</param>
        private void CalculateSortedDevListHelper(Dictionary<string, int> counts, GameDBEntry dbEntry)
        {
            if (dbEntry.Developers != null)
            {
                for (int i = 0; i < dbEntry.Developers.Count; i++)
                {
                    string dev = dbEntry.Developers[i];
                    if (counts.ContainsKey(dev))
                    {
                        counts[dev] += 1;
                    }
                    else
                    {
                        counts[dev] = 1;
                    }
                }
            }
        }

        /// <summary>
        /// Gets a list of publishers found on games with their game count.
        /// </summary>
        /// <param name="filter">GameList including games to include in the search. If null, finds publishers for all games in the database.</param>
        /// <param name="minScore">Minimum count of publishers games to include in the result list. publishers with lower game counts will be discarded.</param>
        /// <returns>List of publishers, as strings with game counts</returns>
        public IEnumerable<Tuple<string, int>> CalculateSortedPubList(GameList filter, int minCount)
        {
            SortedSet<string> publishers = GetAllPublishers();
            Dictionary<string, int> PubCounts = new Dictionary<string, int>();
            if (filter == null)
            {
                foreach (GameDBEntry dbEntry in Games.Values)
                {
                    CalculateSortedPubListHelper(PubCounts, dbEntry);
                }
            }
            else
            {
                foreach (int gameId in filter.Games.Keys)
                {
                    if (Games.ContainsKey(gameId) && !filter.Games[gameId].Hidden)
                    {
                        CalculateSortedPubListHelper(PubCounts, Games[gameId]);
                    }
                }
            }

            var unsortedList = (from entry in PubCounts
                where entry.Value >= minCount
                select new Tuple<string, int>(entry.Key, entry.Value));
            return unsortedList.ToList();
        }

        /// <summary>
        /// Counts games for each publisher.
        /// </summary>
        /// <param name="counts">Existing dictionary of publishers and game count. Key is the publisher as a string, value is the count</param>
        /// <param name="dbEntry">Entry to add publishers from</param>
        private void CalculateSortedPubListHelper(Dictionary<string, int> counts, GameDBEntry dbEntry)
        {
            if (dbEntry.Publishers != null)
            {
                for (int i = 0; i < dbEntry.Publishers.Count; i++)
                {
                    string Pub = dbEntry.Publishers[i];
                    if (counts.ContainsKey(Pub))
                    {
                        counts[Pub] += 1;
                    }
                    else
                    {
                        counts[Pub] = 1;
                    }
                }
            }
        }

        /// <summary>
        /// Gets a list of tags found on games, sorted by a popularity score.
        /// </summary>
        /// <param name="filter">GameList including games to include in the search. If null, finds tags for all games in the database.</param>
        /// <param name="weightFactor">Value of the popularity score contributed by the first processed tag for each game. Each subsequent tag contributes less to its own score.
        /// The last tag always contributes 1. Value less than or equal to 1 indicates no weighting.</param>
        /// <param name="minScore">Minimum score of tags to include in the result list. Tags with lower scores will be discarded.</param>
        /// <param name="tagsPerGame">Maximum tags to find per game. If a game has more tags than this, they will be discarded. 0 indicates no limit.</param>
        /// <returns>List of tags, as strings</returns>
        public IEnumerable<Tuple<string, float>> CalculateSortedTagList(GameList filter, float weightFactor,
            int minScore, int tagsPerGame, bool excludeGenres, bool scoreSort)
        {
            SortedSet<string> genreNames = GetAllGenres();
            Dictionary<string, float> tagCounts = new Dictionary<string, float>();
            if (filter == null)
            {
                foreach (GameDBEntry dbEntry in Games.Values)
                {
                    CalculateSortedTagListHelper(tagCounts, dbEntry, weightFactor, tagsPerGame);
                }
            }
            else
            {
                foreach (int gameId in filter.Games.Keys)
                {
                    if (Games.ContainsKey(gameId) && !filter.Games[gameId].Hidden)
                    {
                        CalculateSortedTagListHelper(tagCounts, Games[gameId], weightFactor, tagsPerGame);
                    }
                }
            }

            if (excludeGenres)
            {
                foreach (string genre in genreNames)
                {
                    tagCounts.Remove(genre);
                }
            }

            var unsortedList = (from entry in tagCounts
                where entry.Value >= minScore
                select new Tuple<string, float>(entry.Key, entry.Value));
            var sortedList = scoreSort
                ? from entry in unsortedList orderby entry.Item2 descending select entry
                : from entry in unsortedList orderby entry.Item1 select entry;
            return sortedList.ToList();
        }

        /// <summary>
        /// Adds tags from the given DBEntry to the dictionary. Adds new elements if necessary, and increases values on existing elements.
        /// </summary>
        /// <param name="counts">Existing dictionary of tags and scores. Key is the tag as a string, value is the score</param>
        /// <param name="dbEntry">Entry to add tags from</param>
        /// <param name="weightFactor">The score value of the first tag in the list.
        /// The first tag on the game will have this score, and the last tag processed will always have score 1.
        /// The tags between will have linearly interpolated values between them.</param>
        /// <param name="tagsPerGame"></param>
        private void CalculateSortedTagListHelper(Dictionary<string, float> counts, GameDBEntry dbEntry,
            float weightFactor, int tagsPerGame)
        {
            if (dbEntry.Tags != null)
            {
                int tagsToLoad = (tagsPerGame == 0) ? dbEntry.Tags.Count : Math.Min(tagsPerGame, dbEntry.Tags.Count);
                for (int i = 0; i < tagsToLoad; i++)
                {
                    // Get the score based on the weighting factor
                    float score = 1;
                    if (weightFactor > 1)
                    {
                        if (tagsToLoad <= 1)
                        {
                            score = weightFactor;
                        }
                        else
                        {
                            float interp = i / (float) (tagsToLoad - 1);
                            score = (1 - interp) * weightFactor + interp;
                        }
                    }

                    string tag = dbEntry.Tags[i];
                    if (counts.ContainsKey(tag))
                    {
                        counts[tag] += score;
                    }
                    else
                    {
                        counts[tag] = score;
                    }
                }
            }
        }

        private void ClearAggregates()
        {
            allStoreGenres = null;
            allStoreFlags = null;
            allStoreDevelopers = null;
            allStorePublishers = null;
        }

        #endregion

        #region Operations

        public void UpdateAppListFromWeb()
        {
            XmlDocument doc = FetchAppListFromWeb();
            IntegrateAppList(doc);
        }

        public static XmlDocument FetchAppListFromWeb()
        {
            XmlDocument doc = new XmlDocument();
            
            WebRequest req = WebRequest.Create(@"http://api.steampowered.com/ISteamApps/GetAppList/v0002/?format=xml");
            using (WebResponse resp = req.GetResponse())
            {
                doc.Load(resp.GetResponseStream());
            }
            
            return doc;
        }

        public int IntegrateAppList(XmlDocument doc)
        {
            int added = 0;
            foreach (XmlNode node in doc.SelectNodes("/applist/apps/app"))
            {
                int appId;
                if (XmlUtil.TryGetIntFromNode(node["appid"], out appId))
                {
                    string gameName = XmlUtil.GetStringFromNode(node["name"], null);
                    if (Games.ContainsKey(appId))
                    {
                        GameDBEntry g = Games[appId];
                        if (string.IsNullOrEmpty(g.Name) || g.Name != gameName)
                        {
                            g.Name = gameName;
                            g.AppType = AppTypes.Unknown;
                        }
                    }
                    else
                    {
                        GameDBEntry g = new GameDBEntry();
                        g.Id = appId;
                        g.Name = gameName;
                        Games.Add(appId, g);
                        added++;
                    }
                }
            }
            
            return added;
        }

        /// <summary>
        /// Updated the database with information from the AppInfo cache file.
        /// </summary>
        /// <param name="path">Path to the cache file</param>
        /// <returns>The number of entries integrated into the database.</returns>
        public int UpdateFromAppInfo(string path)
        {
            int updated = 0;

            Dictionary<int, AppInfo> appInfos = AppInfo.LoadApps(path);
            int timestamp = Utility.GetCurrentUTime();

            foreach (AppInfo aInf in appInfos.Values)
            {
                GameDBEntry entry;
                if (!Games.ContainsKey(aInf.Id))
                {
                    entry = new GameDBEntry();
                    entry.Id = aInf.Id;
                    Games.Add(entry.Id, entry);
                }
                else
                {
                    entry = Games[aInf.Id];
                }

                entry.LastAppInfoUpdate = timestamp;
                if (aInf.AppType != AppTypes.Unknown) entry.AppType = aInf.AppType;
                if (!string.IsNullOrEmpty(aInf.Name)) entry.Name = aInf.Name;
                if (entry.Platforms == AppPlatforms.None ||
                    (entry.LastStoreScrape == 0 && aInf.Platforms > AppPlatforms.None))
                    entry.Platforms = aInf.Platforms;
                if (aInf.Parent > 0) entry.ParentId = aInf.Parent;
                updated++;
            }
            return updated;
        }

        /// <summary>
        /// Update the database with information from howlongtobeatsteam.com.
        /// </summary>
        /// <param name="includeImputedTimes">Whether to include imputed hltb times</param>
        /// <returns>The number of entries integrated into the database.</returns>
        public int UpdateFromHltb(bool includeImputedTimes)
        {
            int updated = 0;

            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                string json = wc.DownloadString(Resources.UrlHLTBAll);
                JObject parsedJson = JObject.Parse(json);
                dynamic games = parsedJson.SelectToken("Games");
                foreach (dynamic g in games)
                {
                    dynamic steamAppData = g.SteamAppData;
                    int id = steamAppData.SteamAppId;
                    if (Games.ContainsKey(id))
                    {
                        dynamic htlbInfo = steamAppData.HltbInfo;

                        if (!includeImputedTimes && htlbInfo.MainTtbImputed == "True")
                            Games[id].HltbMain = 0;
                        else Games[id].HltbMain = htlbInfo.MainTtb;

                        if (!includeImputedTimes && htlbInfo.ExtrasTtbImputed == "True")
                            Games[id].HltbExtras = 0;
                        else Games[id].HltbExtras = htlbInfo.ExtrasTtb;

                        if (!includeImputedTimes && htlbInfo.CompletionistTtbImputed == "True")
                            Games[id].HltbCompletionist = 0;
                        else Games[id].HltbCompletionist = htlbInfo.CompletionistTtb;

                        updated++;
                    }
                }
            }
            LastHltbUpdate = Utility.GetCurrentUTime();
            return updated;
        }

        public void ChangeLanguage(StoreLanguage lang)
        {
            if (Program.GameDB == null) return;
            StoreLanguage dbLang = StoreLanguage.en;
            if (lang == StoreLanguage.windows)
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                if (Enum.GetNames(typeof(StoreLanguage)).ToList().Contains(currentCulture.TwoLetterISOLanguageName))
                    dbLang =
                        (StoreLanguage)Enum.Parse(typeof(StoreLanguage), currentCulture.TwoLetterISOLanguageName);
                else
                {
                    if (currentCulture.Name == "zh-Hans" || currentCulture.Parent.Name == "zh-Hans")
                        dbLang = StoreLanguage.zh_Hans;
                    else if (currentCulture.Name == "zh-Hant" || currentCulture.Parent.Name == "zh-Hant")
                        dbLang = StoreLanguage.zh_Hant;
                    else if (currentCulture.Name == "pt-BR" || currentCulture.Parent.Name == "pt-BR")
                        dbLang = StoreLanguage.pt_BR;
                }
            }
            else dbLang = lang;
            if (dbLanguage == dbLang) return;
            dbLanguage = dbLang;
            //clean DB from data in wrong language
            foreach (GameDBEntry g in Games.Values)
            {
                if (g.Id > 0)
                {
                    g.Tags = null;
                    g.Flags = null;
                    g.Genres = null;
                    g.SteamReleaseDate = null;
                    g.LastStoreScrape = 1; //pretend it is really old data
                    g.VRSupport = new VRSupport();
                    g.LanguageSupport = new LanguageSupport();
                }
            }

            //Update DB with data in correct language
            Queue<int> gamesToUpdate = new Queue<int>();
            if (FormMain.CurrentProfile != null)
            {
                foreach (GameInfo game in FormMain.CurrentProfile.GameData.Games.Values)
                {
                    if (game.Id > 0)
                    {
                        gamesToUpdate.Enqueue(game.Id);
                    }
                }
                DbScrapeDlg scrapeDlg = new DbScrapeDlg(gamesToUpdate);
                scrapeDlg.ShowDialog();

            }
            Save("GameDB.xml.gz");
        }

        #endregion

        #region Serialization

        public void Save(string path)
        {
            Save(path, path.EndsWith(".gz"));
        }

        public void Save(string path, bool compress)
        {
            
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.CloseOutput = true;

            Stream stream = null;
            try
            {
                stream = new FileStream(path, FileMode.Create);

                if (compress)
                {
                    stream = new GZipStream(stream, CompressionMode.Compress);
                }

                XmlWriter writer = XmlWriter.Create(stream, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement(XmlName_RootNode);

                writer.WriteElementString(XmlName_Version, VERSION.ToString());

                writer.WriteElementString(XmlName_LastHltbUpdate, LastHltbUpdate.ToString());

                writer.WriteElementString(XmlName_dbLanguage, Enum.GetName(typeof(StoreLanguage), dbLanguage));

                writer.WriteStartElement(XmlName_Games);
                XmlSerializer x = new XmlSerializer(typeof(GameDBEntry));
                var nameSpace = new XmlSerializerNamespaces();
                nameSpace.Add("", "");
                foreach (GameDBEntry g in Games.Values)
                {
                    x.Serialize(writer, g, nameSpace);
                }
                writer.WriteEndElement();   //XmlName_Games

                writer.WriteEndElement();   //XmlName_RootNode
                writer.WriteEndDocument();
                writer.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            
        }

        public void Load(string path)
        {
            Load(path, path.EndsWith(".gz"));
        }

        public void Load(string path, bool compress)
        {
            
            XmlDocument doc = new XmlDocument();

            Stream stream = null;
            try
            {
                stream = new FileStream(path, FileMode.Open);
                if (compress)
                {
                    stream = new GZipStream(stream, CompressionMode.Decompress);
                }

                doc.Load(stream);

                
                Games.Clear();
                ClearAggregates();

                XmlNode gameListNode = doc.SelectSingleNode("/" + XmlName_RootNode);

                int fileVersion = XmlUtil.GetIntFromNode(gameListNode[XmlName_Version], 0);

                LastHltbUpdate = XmlUtil.GetIntFromNode(gameListNode[XmlName_LastHltbUpdate], 0);

                dbLanguage = (StoreLanguage) Enum.Parse(typeof(StoreLanguage),
                    XmlUtil.GetStringFromNode(gameListNode[XmlName_dbLanguage], "en"), true);

                if (fileVersion == 1) LoadGamelistVersion1(gameListNode);
                else
                {
                    XmlSerializer x = new XmlSerializer(typeof(GameDBEntry));
                    foreach (XmlNode gameNode in gameListNode.SelectSingleNode(XmlName_Games).ChildNodes)
                    {
                        XmlReader reader = new XmlNodeReader(gameNode);
                        GameDBEntry entry = (GameDBEntry)x.Deserialize(reader);
                        Games.Add(entry.Id, entry);
                    }
                }
                
            }
            finally
            {
                stream?.Close();
            }
        }

        /// <summary>
        /// Reads GameDbEntry objects from selected node and adds them to GameDb
        /// Legacy method used to read data from version 1 of the database.
        /// </summary>
        /// <param name="gameListNode">Node containing GameDbEntry objects with game as element name</param>
        private void LoadGamelistVersion1(XmlNode gameListNode)
        {
            const string
                XmlName_Game = "game",
                XmlName_Game_Id = "id",
                XmlName_Game_Name = "name",
                XmlName_Game_LastStoreUpdate = "lastStoreUpdate",
                XmlName_Game_LastAppInfoUpdate = "lastAppInfoUpdate",
                XmlName_Game_Type = "type",
                XmlName_Game_Platforms = "platforms",
                XmlName_Game_Parent = "parent",
                XmlName_Game_Genre = "genre",
                XmlName_Game_Tag = "tag",
                XmlName_Game_Achievements = "achievements",
                XmlName_Game_Developer = "developer",
                XmlName_Game_Publisher = "publisher",
                XmlName_Game_Flag = "flag",
                XmlName_Game_ReviewTotal = "reviewTotal",
                XmlName_Game_ReviewPositivePercent = "reviewPositiveP",
                XmlName_Game_MCUrl = "mcUrl",
                XmlName_Game_Date = "steamDate",
                XmlName_Game_HltbMain = "hltbMain",
                XmlName_Game_HltbExtras = "hltbExtras",
                XmlName_Game_HltbCompletionist = "hltbCompletionist",
                XmlName_Game_vrSupport = "vrSupport",
                XmlName_Game_vrSupport_Headsets = "Headset",
                XmlName_Game_vrSupport_Input = "Input",
                XmlName_Game_vrSupport_PlayArea = "PlayArea",
                XmlName_Game_languageSupport = "languageSupport",
                XmlName_Game_languageSupport_Interface = "Headset",
                XmlName_Game_languageSupport_FullAudio = "Input",
                XmlName_Game_languageSupport_Subtitles = "PlayArea";

            foreach (XmlNode gameNode in gameListNode.SelectNodes(XmlName_Game))
            {
                int id;
                if (!XmlUtil.TryGetIntFromNode(gameNode[XmlName_Game_Id], out id) || Games.ContainsKey(id))
                {
                    continue;
                }
                GameDBEntry g = new GameDBEntry();
                g.Id = id;

                g.Name = XmlUtil.GetStringFromNode(gameNode[XmlName_Game_Name], null);

                g.AppType = XmlUtil.GetEnumFromNode(gameNode[XmlName_Game_Type], AppTypes.Unknown);

                g.Platforms = XmlUtil.GetEnumFromNode(gameNode[XmlName_Game_Platforms], AppPlatforms.All);

                g.ParentId = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_Parent], -1);

                g.Genres = XmlUtil.GetStringsFromNodeList(gameNode.SelectNodes(XmlName_Game_Genre));

                g.Tags = XmlUtil.GetStringsFromNodeList(gameNode.SelectNodes(XmlName_Game_Tag));

                foreach (XmlNode vrNode in gameNode.SelectNodes(XmlName_Game_vrSupport))
                {
                    g.VRSupport.Headsets =
                        XmlUtil.GetStringsFromNodeList(vrNode.SelectNodes(XmlName_Game_vrSupport_Headsets));
                    g.VRSupport.Input =
                        XmlUtil.GetStringsFromNodeList(vrNode.SelectNodes(XmlName_Game_vrSupport_Input));
                    g.VRSupport.PlayArea =
                        XmlUtil.GetStringsFromNodeList(vrNode.SelectNodes(XmlName_Game_vrSupport_PlayArea));
                }

                foreach (XmlNode langNode in gameNode.SelectNodes(XmlName_Game_languageSupport))
                {
                    g.LanguageSupport.Interface =
                        XmlUtil.GetStringsFromNodeList(langNode.SelectNodes(XmlName_Game_languageSupport_Interface));
                    g.LanguageSupport.FullAudio =
                        XmlUtil.GetStringsFromNodeList(langNode.SelectNodes(XmlName_Game_languageSupport_FullAudio));
                    g.LanguageSupport.Subtitles =
                        XmlUtil.GetStringsFromNodeList(langNode.SelectNodes(XmlName_Game_languageSupport_Subtitles));
                }

                g.Developers = XmlUtil.GetStringsFromNodeList(gameNode.SelectNodes(XmlName_Game_Developer));

                g.Publishers = XmlUtil.GetStringsFromNodeList(gameNode.SelectNodes(XmlName_Game_Publisher));

                g.SteamReleaseDate = XmlUtil.GetStringFromNode(gameNode[XmlName_Game_Date], null);

                g.Flags = XmlUtil.GetStringsFromNodeList(gameNode.SelectNodes(XmlName_Game_Flag));

                g.TotalAchievements = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_Achievements], 0);

                g.ReviewTotal = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_ReviewTotal], 0);
                g.ReviewPositivePercentage =
                    XmlUtil.GetIntFromNode(gameNode[XmlName_Game_ReviewPositivePercent], 0);

                g.MetacriticUrl = XmlUtil.GetStringFromNode(gameNode[XmlName_Game_MCUrl], null);

                g.LastAppInfoUpdate = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_LastAppInfoUpdate], 0);
                g.LastStoreScrape = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_LastStoreUpdate], 0);

                g.HltbMain = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_HltbMain], 0);
                g.HltbExtras = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_HltbExtras], 0);
                g.HltbCompletionist = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_HltbCompletionist], 0);

                Games.Add(id, g);
            }
        }

        #endregion
    }
}
