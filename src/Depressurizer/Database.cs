#region LICENSE

//     This file (Database.cs) is part of Depressurizer.
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
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using Depressurizer.Enums;
using Depressurizer.Models;
using Depressurizer.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rallion;
using Formatting = Newtonsoft.Json.Formatting;

namespace Depressurizer
{
    public class Database
    {
        #region Static Fields

        private static readonly object SyncRoot = new object();

        private static volatile Database _instance;

        #endregion

        #region Fields

        // Main Data
        public Dictionary<int, DatabaseEntry> Games = new Dictionary<int, DatabaseEntry>();
        public int LastHltbUpdate;

        private LanguageSupport allLanguages;
        private SortedSet<string> allStoreDevelopers;

        private SortedSet<string> allStoreFlags;

        // Extra data
        private SortedSet<string> allStoreGenres;
        private SortedSet<string> allStorePublishers;
        private VrSupport allVrSupportFlags;

        #endregion

        #region Constructors and Destructors

        private Database()
        {
        }

        #endregion

        #region Public Properties

        public static Database Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                lock (SyncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new Database();
                    }
                }

                return _instance;
            }
        }

        public StoreLanguage Language { get; set; } = StoreLanguage.en;

        #endregion

        #region Public Methods and Operators

        public static XmlDocument FetchAppListFromWeb()
        {
            XmlDocument doc = new XmlDocument();
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_DownloadingSteamAppList);
            WebRequest req = WebRequest.Create(@"http://api.steampowered.com/ISteamApps/GetAppList/v0002/?format=xml");
            using (WebResponse resp = req.GetResponse())
            {
                doc.Load(resp.GetResponseStream());
            }

            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_XMLAppListDownloaded);
            return doc;
        }

        /// <summary>
        ///     Gets a list of all Steam store developers found in the entire database.
        ///     Always recalculates.
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

            foreach (DatabaseEntry entry in Games.Values)
            {
                if (entry.Developers != null)
                {
                    allStoreDevelopers.UnionWith(entry.Developers);
                }
            }

            return allStoreDevelopers;
        }

        /// <summary>
        ///     Gets a list of all Steam store genres found in the entire database.
        ///     Always recalculates.
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

            foreach (DatabaseEntry entry in Games.Values)
            {
                if (entry.Genres != null)
                {
                    allStoreGenres.UnionWith(entry.Genres);
                }
            }

            return allStoreGenres;
        }

        /// <summary>
        ///     Gets a list of all Game Languages found in the entire database.
        ///     Always recalculates.
        /// </summary>
        /// <returns>A LanguageSupport struct containing the languages</returns>
        public LanguageSupport CalculateAllLanguages()
        {
            SortedSet<string> Interface = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> Subtitles = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> FullAudio = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (DatabaseEntry entry in Games.Values)
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
        ///     Gets a list of all Steam store publishers found in the entire database.
        ///     Always recalculates.
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

            foreach (DatabaseEntry entry in Games.Values)
            {
                if (entry.Publishers != null)
                {
                    allStorePublishers.UnionWith(entry.Publishers);
                }
            }

            return allStorePublishers;
        }

        /// <summary>
        ///     Gets a list of all Steam store flags found in the entire database.
        ///     Always recalculates.
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

            foreach (DatabaseEntry entry in Games.Values)
            {
                if (entry.Flags != null)
                {
                    allStoreFlags.UnionWith(entry.Flags);
                }
            }

            return allStoreFlags;
        }

        /// <summary>
        ///     Gets a list of all Steam store VR Support flags found in the entire database.
        ///     Always recalculates.
        /// </summary>
        /// <returns>A VrSupport struct containing the flags</returns>
        public VrSupport CalculateAllVrSupportFlags()
        {
            SortedSet<string> headsets = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> input = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> playArea = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (DatabaseEntry entry in Games.Values)
            {
                if (entry.VrSupport.Headsets != null)
                {
                    headsets.UnionWith(entry.VrSupport.Headsets);
                }

                if (entry.VrSupport.Input != null)
                {
                    input.UnionWith(entry.VrSupport.Input);
                }

                if (entry.VrSupport.PlayArea != null)
                {
                    playArea.UnionWith(entry.VrSupport.PlayArea);
                }
            }

            allVrSupportFlags.Headsets = headsets.ToList();
            allVrSupportFlags.Input = input.ToList();
            allVrSupportFlags.PlayArea = playArea.ToList();
            return allVrSupportFlags;
        }

        /// <summary>
        ///     Gets a list of developers found on games with their game count.
        /// </summary>
        /// <param name="filter">
        ///     GameList including games to include in the search. If null, finds developers for all games in the
        ///     database.
        /// </param>
        /// <param name="minScore">
        ///     Minimum count of developers games to include in the result list. Developers with lower game
        ///     counts will be discarded.
        /// </param>
        /// <returns>List of developers, as strings with game counts</returns>
        public IEnumerable<Tuple<string, int>> CalculateSortedDevList(GameList filter, int minCount)
        {
            SortedSet<string> developers = GetAllDevelopers();
            Dictionary<string, int> devCounts = new Dictionary<string, int>();
            if (filter == null)
            {
                foreach (DatabaseEntry dbEntry in Games.Values)
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

            IEnumerable<Tuple<string, int>> unsortedList = from entry in devCounts where entry.Value >= minCount select new Tuple<string, int>(entry.Key, entry.Value);
            return unsortedList.ToList();
        }

        /// <summary>
        ///     Gets a list of publishers found on games with their game count.
        /// </summary>
        /// <param name="filter">
        ///     GameList including games to include in the search. If null, finds publishers for all games in the
        ///     database.
        /// </param>
        /// <param name="minScore">
        ///     Minimum count of publishers games to include in the result list. publishers with lower game
        ///     counts will be discarded.
        /// </param>
        /// <returns>List of publishers, as strings with game counts</returns>
        public IEnumerable<Tuple<string, int>> CalculateSortedPubList(GameList filter, int minCount)
        {
            SortedSet<string> publishers = GetAllPublishers();
            Dictionary<string, int> PubCounts = new Dictionary<string, int>();
            if (filter == null)
            {
                foreach (DatabaseEntry dbEntry in Games.Values)
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

            IEnumerable<Tuple<string, int>> unsortedList = from entry in PubCounts where entry.Value >= minCount select new Tuple<string, int>(entry.Key, entry.Value);
            return unsortedList.ToList();
        }

        /// <summary>
        ///     Gets a list of tags found on games, sorted by a popularity score.
        /// </summary>
        /// <param name="filter">
        ///     GameList including games to include in the search. If null, finds tags for all games in the
        ///     database.
        /// </param>
        /// <param name="weightFactor">
        ///     Value of the popularity score contributed by the first processed tag for each game. Each subsequent tag contributes
        ///     less to its own score.
        ///     The last tag always contributes 1. Value less than or equal to 1 indicates no weighting.
        /// </param>
        /// <param name="minScore">Minimum score of tags to include in the result list. Tags with lower scores will be discarded.</param>
        /// <param name="tagsPerGame">
        ///     Maximum tags to find per game. If a game has more tags than this, they will be discarded. 0
        ///     indicates no limit.
        /// </param>
        /// <returns>List of tags, as strings</returns>
        public IEnumerable<Tuple<string, float>> CalculateSortedTagList(GameList filter, float weightFactor, int minScore, int tagsPerGame, bool excludeGenres, bool scoreSort)
        {
            SortedSet<string> genreNames = GetAllGenres();
            Dictionary<string, float> tagCounts = new Dictionary<string, float>();
            if (filter == null)
            {
                foreach (DatabaseEntry dbEntry in Games.Values)
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

            IEnumerable<Tuple<string, float>> unsortedList = from entry in tagCounts where entry.Value >= minScore select new Tuple<string, float>(entry.Key, entry.Value);
            IOrderedEnumerable<Tuple<string, float>> sortedList = scoreSort ? from entry in unsortedList orderby entry.Item2 descending select entry : from entry in unsortedList orderby entry.Item1 select entry;
            return sortedList.ToList();
        }

        public void ChangeLanguage(StoreLanguage language)
        {
            StoreLanguage dbLang = StoreLanguage.en;
            if (language == StoreLanguage.windows)
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                if (Enum.GetNames(typeof(StoreLanguage)).ToList().Contains(currentCulture.TwoLetterISOLanguageName))
                {
                    dbLang = (StoreLanguage) Enum.Parse(typeof(StoreLanguage), currentCulture.TwoLetterISOLanguageName);
                }
                else
                {
                    if (currentCulture.Name == "zh-Hans" || currentCulture.Parent.Name == "zh-Hans")
                    {
                        dbLang = StoreLanguage.zh_Hans;
                    }
                    else if (currentCulture.Name == "zh-Hant" || currentCulture.Parent.Name == "zh-Hant")
                    {
                        dbLang = StoreLanguage.zh_Hant;
                    }
                    else if (currentCulture.Name == "pt-BR" || currentCulture.Parent.Name == "pt-BR")
                    {
                        dbLang = StoreLanguage.pt_BR;
                    }
                }
            }
            else
            {
                dbLang = language;
            }

            if (Language == dbLang)
            {
                return;
            }

            Language = dbLang;
            //clean DB from data in wrong language
            foreach (DatabaseEntry g in Games.Values)
            {
                if (g.Id > 0)
                {
                    g.Tags = null;
                    g.Flags = null;
                    g.Genres = null;
                    g.SteamReleaseDate = null;
                    g.LastStoreScrape = 1; //pretend it is really old data
                    g.VrSupport = new VrSupport();
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

            Save("database.json");
        }

        public bool Contains(int id)
        {
            return Games.ContainsKey(id);
        }

        /// <summary>
        ///     Gets a list of all Steam store developers found in the entire database.
        ///     Only recalculates if necessary.
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
        ///     Gets a list of all Steam store genres found in the entire database.
        ///     Only recalculates if necessary.
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
        ///     Gets a list of all Game Languages found in the entire database.
        ///     Only recalculates if necessary.
        /// </summary>
        /// <returns>A LanguageSupport struct containing the languages</returns>
        public LanguageSupport GetAllLanguages()
        {
            if (allLanguages.FullAudio == null || allLanguages.Interface == null || allLanguages.Subtitles == null)
            {
                return CalculateAllLanguages();
            }

            return allLanguages;
        }

        /// <summary>
        ///     Gets a list of all Steam store publishers found in the entire database.
        ///     Only recalculates if necessary.
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
        ///     Gets a list of all Steam store flags found in the entire database.
        ///     Only recalculates if necessary.
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
        ///     Gets a list of all Steam store VR Support flags found in the entire database.
        ///     Only recalculates if necessary.
        /// </summary>
        /// <returns>A VrSupport struct containing the flags</returns>
        public VrSupport GetAllVrSupportFlags()
        {
            if (allVrSupportFlags.Headsets == null || allVrSupportFlags.Input == null || allVrSupportFlags.PlayArea == null)
            {
                return CalculateAllVrSupportFlags();
            }

            return allVrSupportFlags;
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

        public string GetName(int id)
        {
            if (Games.ContainsKey(id))
            {
                return Games[id].Name;
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
                DatabaseEntry dbEntry = Games[gameId];
                DateTime releaseDate;
                if (DateTime.TryParse(dbEntry.SteamReleaseDate, out releaseDate))
                {
                    return releaseDate.Year;
                }
            }

            return 0;
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

        public VrSupport GetVrSupport(int gameId, int depth = 3)
        {
            if (Games.ContainsKey(gameId))
            {
                VrSupport res = Games[gameId].VrSupport;
                if ((res.Headsets == null || res.Headsets.Count == 0) && (res.Input == null || res.Input.Count == 0) && (res.PlayArea == null || res.PlayArea.Count == 0) && depth > 0 && Games[gameId].ParentId > 0)
                {
                    res = GetVrSupport(Games[gameId].ParentId, depth - 1);
                }

                return res;
            }

            return new VrSupport();
        }

        public bool IncludeItemInGameList(int appId)
        {
            if (!Games.ContainsKey(appId))
            {
                return false;
            }

            return Games[appId].AppType == AppType.Application || Games[appId].AppType == AppType.Game;
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
                        DatabaseEntry g = Games[appId];
                        if (string.IsNullOrEmpty(g.Name) || g.Name != gameName)
                        {
                            g.Name = gameName;
                            g.AppType = AppType.Unknown;
                        }
                    }
                    else
                    {
                        DatabaseEntry g = new DatabaseEntry();
                        g.Id = appId;
                        g.Name = gameName;
                        Games.Add(appId, g);
                        added++;
                    }
                }
            }

            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_LoadedNewItemsFromAppList, added);
            return added;
        }

        public void Load(string path)
        {
            lock (SyncRoot)
            {
                Program.Logger.Write(LoggerLevel.Info, "Database: Loading database from '{0}'.", path);
                if (!File.Exists(path))
                {
                    Program.Logger.Write(LoggerLevel.Warning, "Database: Database file not found at '{0}'.", path);

                    return;
                }

                Stopwatch sw = new Stopwatch();
                sw.Start();

                using (StreamReader file = File.OpenText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
#if DEBUG
                    serializer.Formatting = Formatting.Indented;
#endif

                    _instance = (Database) serializer.Deserialize(file, typeof(Database));
                }

                sw.Stop();
                Program.Logger.Write(LoggerLevel.Info, "Database: Loaded database from '{0}', in {1}ms.", path, sw.ElapsedMilliseconds);
            }
        }

        public void Reset()
        {
            lock (SyncRoot)
            {
                Program.Logger.Write(LoggerLevel.Info, "Database: Database was reset.");
                _instance = new Database();
            }
        }

        public void Save(string path)
        {
            lock (SyncRoot)
            {
                Program.Logger.Write(LoggerLevel.Info, "Database: Saving database to '{0}'.", path);

                Stopwatch sw = new Stopwatch();
                sw.Start();

                using (StreamWriter file = File.CreateText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
#if DEBUG
                    serializer.Formatting = Formatting.Indented;
#endif

                    serializer.Serialize(file, _instance);
                }

                sw.Stop();
                Program.Logger.Write(LoggerLevel.Info, "Database: Saved database to '{0}', in {1}ms.", path, sw.ElapsedMilliseconds);
            }
        }

        /// <summary>
        ///     Returns whether the game supports VR
        /// </summary>
        public bool SupportsVr(int gameId, int depth = 3)
        {
            if (Games.ContainsKey(gameId))
            {
                VrSupport res = Games[gameId].VrSupport;
                if (res.Headsets != null && res.Headsets.Count > 0 || res.Input != null && res.Input.Count > 0 || res.PlayArea != null && res.PlayArea.Count > 0 && depth > 0 && Games[gameId].ParentId > 0)
                {
                    return true;
                }

                if (depth > 0 && Games[gameId].ParentId > 0)
                {
                    return SupportsVr(Games[gameId].ParentId, depth - 1);
                }
            }

            return false;
        }

        /// <summary>
        ///     Updated the database with information from the AppInfo cache file.
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
                DatabaseEntry entry;
                if (!Games.ContainsKey(aInf.Id))
                {
                    entry = new DatabaseEntry();
                    entry.Id = aInf.Id;
                    Games.Add(entry.Id, entry);
                }
                else
                {
                    entry = Games[aInf.Id];
                }

                entry.LastAppInfoUpdate = timestamp;
                if (aInf.AppType != AppType.Unknown)
                {
                    entry.AppType = aInf.AppType;
                }

                if (!string.IsNullOrEmpty(aInf.Name))
                {
                    entry.Name = aInf.Name;
                }

                if (entry.Platforms == AppPlatforms.None || entry.LastStoreScrape == 0 && aInf.Platforms > AppPlatforms.None)
                {
                    entry.Platforms = aInf.Platforms;
                }

                if (aInf.Parent > 0)
                {
                    entry.ParentId = aInf.Parent;
                }

                updated++;
            }

            return updated;
        }

        /// <summary>
        ///     Update the database with information from howlongtobeatsteam.com.
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
                        {
                            Games[id].HltbMain = 0;
                        }
                        else
                        {
                            Games[id].HltbMain = htlbInfo.MainTtb;
                        }

                        if (!includeImputedTimes && htlbInfo.ExtrasTtbImputed == "True")
                        {
                            Games[id].HltbExtras = 0;
                        }
                        else
                        {
                            Games[id].HltbExtras = htlbInfo.ExtrasTtb;
                        }

                        if (!includeImputedTimes && htlbInfo.CompletionistTtbImputed == "True")
                        {
                            Games[id].HltbCompletionist = 0;
                        }
                        else
                        {
                            Games[id].HltbCompletionist = htlbInfo.CompletionistTtb;
                        }

                        updated++;
                    }
                }
            }

            LastHltbUpdate = Utility.GetCurrentUTime();
            return updated;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Counts games for each publisher.
        /// </summary>
        /// <param name="counts">
        ///     Existing dictionary of publishers and game count. Key is the publisher as a string, value is the
        ///     count
        /// </param>
        /// <param name="entry">Entry to add publishers from</param>
        private static void CalculateSortedPubListHelper(IDictionary<string, int> counts, DatabaseEntry entry)
        {
            if (entry.Publishers == null)
            {
                return;
            }

            foreach (string publisher in entry.Publishers)
            {
                if (counts.ContainsKey(publisher))
                {
                    counts[publisher] += 1;
                }
                else
                {
                    counts[publisher] = 1;
                }
            }
        }

        /// <summary>
        ///     Adds tags from the given DBEntry to the dictionary. Adds new elements if necessary, and increases values on
        ///     existing elements.
        /// </summary>
        /// <param name="counts">Existing dictionary of tags and scores. Key is the tag as a string, value is the score</param>
        /// <param name="entry">Entry to add tags from</param>
        /// <param name="weightFactor">
        ///     The score value of the first tag in the list.
        ///     The first tag on the game will have this score, and the last tag processed will always have score 1.
        ///     The tags between will have linearly interpolated values between them.
        /// </param>
        /// <param name="tagsPerGame"></param>
        private static void CalculateSortedTagListHelper(IDictionary<string, float> counts, DatabaseEntry entry, float weightFactor, int tagsPerGame)
        {
            if (entry.Tags == null)
            {
                return;
            }

            int tagsToLoad = tagsPerGame == 0 ? entry.Tags.Count : Math.Min(tagsPerGame, entry.Tags.Count);
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

                string tag = entry.Tags[i];
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

        /// <summary>
        ///     Counts games for each developer.
        /// </summary>
        /// <param name="counts">
        ///     Existing dictionary of developers and game count. Key is the developer as a string, value is the
        ///     count
        /// </param>
        /// <param name="dbEntry">Entry to add developers from</param>
        private void CalculateSortedDevListHelper(Dictionary<string, int> counts, DatabaseEntry dbEntry)
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

        #endregion
    }
}
