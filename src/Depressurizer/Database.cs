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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Depressurizer.Core.Enums;
using Depressurizer.Helpers;
using Depressurizer.Models;
using Depressurizer.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public readonly Dictionary<int, DatabaseEntry> Games = new Dictionary<int, DatabaseEntry>();

        public int LastHltbUpdate;

        private LanguageSupport _allLanguages;

        private SortedSet<string> _allStoreDevelopers;

        private SortedSet<string> _allStoreFlags;

        private SortedSet<string> _allStoreGenres;

        private SortedSet<string> _allStorePublishers;

        private VrSupport _allVrSupportFlags;

        #endregion

        #region Constructors and Destructors

        private Database() { }

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

        public int Count => Games.Count;

        public StoreLanguage Language { get; set; } = StoreLanguage.English;

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        public static XmlDocument FetchAppListFromWeb()
        {
            XmlDocument doc = new XmlDocument();
            Logger.Info(GlobalStrings.GameDB_DownloadingSteamAppList);
            WebRequest req = WebRequest.Create(@"http://api.steampowered.com/ISteamApps/GetAppList/v0002/?format=xml");
            using (WebResponse resp = req.GetResponse())
            {
                doc.Load(resp.GetResponseStream());
            }

            Logger.Info(GlobalStrings.GameDB_XMLAppListDownloaded);
            return doc;
        }

        public void Add(DatabaseEntry entry)
        {
            if (Contains(entry.Id, out DatabaseEntry databaseEntry))
            {
                databaseEntry.MergeIn(entry);
            }
            else
            {
                Games.Add(entry.Id, entry);
            }
        }

        /// <summary>
        ///     Gets a list of all Steam store developers found in the entire database.
        ///     Always recalculates.
        /// </summary>
        /// <returns>A set of developers, as strings</returns>
        public SortedSet<string> CalculateAllDevelopers()
        {
            if (_allStoreDevelopers == null)
            {
                _allStoreDevelopers = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                _allStoreDevelopers.Clear();
            }

            foreach (DatabaseEntry entry in Games.Values)
            {
                if (entry.Developers != null)
                {
                    _allStoreDevelopers.UnionWith(entry.Developers);
                }
            }

            return _allStoreDevelopers;
        }

        /// <summary>
        ///     Gets a list of all Steam store genres found in the entire database.
        ///     Always recalculates.
        /// </summary>
        /// <returns>A set of genres, as strings</returns>
        public SortedSet<string> CalculateAllGenres()
        {
            if (_allStoreGenres == null)
            {
                _allStoreGenres = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                _allStoreGenres.Clear();
            }

            foreach (DatabaseEntry entry in Games.Values)
            {
                if (entry.Genres != null)
                {
                    _allStoreGenres.UnionWith(entry.Genres);
                }
            }

            return _allStoreGenres;
        }

        /// <summary>
        ///     Gets a list of all Game Languages found in the entire database.
        ///     Always recalculates.
        /// </summary>
        /// <returns>A LanguageSupport struct containing the languages</returns>
        public LanguageSupport CalculateAllLanguages()
        {
            SortedSet<string> Interface = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> subtitles = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> fullAudio = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (DatabaseEntry entry in Games.Values)
            {
                if (entry.LanguageSupport.Interface != null)
                {
                    Interface.UnionWith(entry.LanguageSupport.Interface);
                }

                if (entry.LanguageSupport.Subtitles != null)
                {
                    subtitles.UnionWith(entry.LanguageSupport.Subtitles);
                }

                if (entry.LanguageSupport.FullAudio != null)
                {
                    fullAudio.UnionWith(entry.LanguageSupport.FullAudio);
                }
            }

            _allLanguages.Interface = Interface.ToList();
            _allLanguages.Subtitles = subtitles.ToList();
            _allLanguages.FullAudio = fullAudio.ToList();
            return _allLanguages;
        }

        /// <summary>
        ///     Gets a list of all Steam store publishers found in the entire database.
        ///     Always recalculates.
        /// </summary>
        /// <returns>A set of publishers, as strings</returns>
        public SortedSet<string> CalculateAllPublishers()
        {
            if (_allStorePublishers == null)
            {
                _allStorePublishers = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                _allStorePublishers.Clear();
            }

            foreach (DatabaseEntry entry in Games.Values)
            {
                if (entry.Publishers != null)
                {
                    _allStorePublishers.UnionWith(entry.Publishers);
                }
            }

            return _allStorePublishers;
        }

        /// <summary>
        ///     Gets a list of all Steam store flags found in the entire database.
        ///     Always recalculates.
        /// </summary>
        /// <returns>A set of genres, as strings</returns>
        public SortedSet<string> CalculateAllStoreFlags()
        {
            if (_allStoreFlags == null)
            {
                _allStoreFlags = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                _allStoreFlags.Clear();
            }

            foreach (DatabaseEntry entry in Games.Values)
            {
                if (entry.Flags != null)
                {
                    _allStoreFlags.UnionWith(entry.Flags);
                }
            }

            return _allStoreFlags;
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

            _allVrSupportFlags.Headsets = headsets.ToList();
            _allVrSupportFlags.Input = input.ToList();
            _allVrSupportFlags.PlayArea = playArea.ToList();
            return _allVrSupportFlags;
        }

        /// <summary>
        ///     Gets a list of developers found on games with their game count.
        /// </summary>
        /// <param name="filter">
        ///     GameList including games to include in the search. If null, finds developers for all games in the
        ///     database.
        /// </param>
        /// <param name="minCount">
        ///     Minimum count of developers games to include in the result list. Developers with lower game
        ///     counts will be discarded.
        /// </param>
        /// <returns>List of developers, as strings with game counts</returns>
        public IEnumerable<Tuple<string, int>> CalculateSortedDevList(GameList filter, int minCount)
        {
            GetAllDevelopers();
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
        /// <param name="minCount">
        ///     Minimum count of publishers games to include in the result list. publishers with lower game
        ///     counts will be discarded.
        /// </param>
        /// <returns>List of publishers, as strings with game counts</returns>
        public IEnumerable<Tuple<string, int>> CalculateSortedPubList(GameList filter, int minCount)
        {
            GetAllPublishers();
            Dictionary<string, int> pubCounts = new Dictionary<string, int>();
            if (filter == null)
            {
                foreach (DatabaseEntry dbEntry in Games.Values)
                {
                    CalculateSortedPubListHelper(pubCounts, dbEntry);
                }
            }
            else
            {
                foreach (int gameId in filter.Games.Keys)
                {
                    if (Games.ContainsKey(gameId) && !filter.Games[gameId].Hidden)
                    {
                        CalculateSortedPubListHelper(pubCounts, Games[gameId]);
                    }
                }
            }

            IEnumerable<Tuple<string, int>> unsortedList = from entry in pubCounts where entry.Value >= minCount select new Tuple<string, int>(entry.Key, entry.Value);
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
            StoreLanguage dbLang = language;
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

        public void Clear()
        {
            Games.Clear();
        }

        public bool Contains(int appId)
        {
            return Games.ContainsKey(appId);
        }

        public bool Contains(int appId, out DatabaseEntry entry)
        {
            entry = null;

            if (Contains(appId))
            {
                entry = Games[appId];
            }

            return entry != null;
        }

        /// <summary>
        ///     Gets a list of all Steam store developers found in the entire database.
        ///     Only recalculates if necessary.
        /// </summary>
        /// <returns>A set of developers, as strings</returns>
        public SortedSet<string> GetAllDevelopers()
        {
            if (_allStoreDevelopers == null)
            {
                return CalculateAllDevelopers();
            }

            return _allStoreDevelopers;
        }

        /// <summary>
        ///     Gets a list of all Steam store genres found in the entire database.
        ///     Only recalculates if necessary.
        /// </summary>
        /// <returns>A set of genres, as strings</returns>
        public SortedSet<string> GetAllGenres()
        {
            if (_allStoreGenres == null)
            {
                return CalculateAllGenres();
            }

            return _allStoreGenres;
        }

        /// <summary>
        ///     Gets a list of all Game Languages found in the entire database.
        ///     Only recalculates if necessary.
        /// </summary>
        /// <returns>A LanguageSupport struct containing the languages</returns>
        public LanguageSupport GetAllLanguages()
        {
            if (_allLanguages.FullAudio == null || _allLanguages.Interface == null || _allLanguages.Subtitles == null)
            {
                return CalculateAllLanguages();
            }

            return _allLanguages;
        }

        /// <summary>
        ///     Gets a list of all Steam store publishers found in the entire database.
        ///     Only recalculates if necessary.
        /// </summary>
        /// <returns>A set of publishers, as strings</returns>
        public SortedSet<string> GetAllPublishers()
        {
            if (_allStorePublishers == null)
            {
                return CalculateAllPublishers();
            }

            return _allStorePublishers;
        }

        /// <summary>
        ///     Gets a list of all Steam store flags found in the entire database.
        ///     Only recalculates if necessary.
        /// </summary>
        /// <returns>A set of genres, as strings</returns>
        public SortedSet<string> GetAllStoreFlags()
        {
            if (_allStoreFlags == null)
            {
                return CalculateAllStoreFlags();
            }

            return _allStoreFlags;
        }

        /// <summary>
        ///     Gets a list of all Steam store VR Support flags found in the entire database.
        ///     Only recalculates if necessary.
        /// </summary>
        /// <returns>A VrSupport struct containing the flags</returns>
        public VrSupport GetAllVrSupportFlags()
        {
            if (_allVrSupportFlags.Headsets == null || _allVrSupportFlags.Input == null || _allVrSupportFlags.PlayArea == null)
            {
                return CalculateAllVrSupportFlags();
            }

            return _allVrSupportFlags;
        }

        public List<string> GetDevelopers(int appId)
        {
            return GetDevelopers(appId, 3);
        }

        public List<string> GetDevelopers(int appId, int depth)
        {
            if (!Contains(appId, out DatabaseEntry entry))
            {
                return new List<string>();
            }

            List<string> result = entry.Developers ?? new List<string>();
            if (result.Count == 0 && depth > 0 && entry.ParentId > 0)
            {
                result = GetDevelopers(entry.ParentId, depth - 1);
            }

            return result;
        }

        public List<string> GetFlagList(int appId)
        {
            return GetFlagList(appId, 3);
        }

        public List<string> GetFlagList(int appId, int depth)
        {
            if (!Contains(appId, out DatabaseEntry entry))
            {
                return new List<string>();
            }

            List<string> result = entry.Flags ?? new List<string>();
            if (result.Count == 0 && depth > 0 && entry.ParentId > 0)
            {
                result = GetFlagList(entry.ParentId, depth - 1);
            }

            return result;
        }

        public List<string> GetGenreList(int appId, int depth, bool tagFallback = true)
        {
            if (!Contains(appId, out DatabaseEntry entry))
            {
                return new List<string>();
            }

            List<string> result = entry.Genres ?? new List<string>();
            if (tagFallback && result.Count == 0)
            {
                List<string> tags = GetTagList(appId, 0);
                if (tags != null && tags.Count > 0)
                {
                    result = new List<string>(tags.Intersect(GetAllGenres()));
                }
            }

            if ((result.Count == 0) && depth > 0 && entry.ParentId > 0)
            {
                result = GetGenreList(entry.ParentId, depth - 1, tagFallback);
            }

            return result;
        }

        public string GetName(int id)
        {
            if (Games.ContainsKey(id))
            {
                return Games[id].Name;
            }

            return null;
        }

        public List<string> GetPublishers(int appId)
        {
            return GetPublishers(appId, 3);
        }

        public List<string> GetPublishers(int appId, int depth)
        {
            if (!Contains(appId, out DatabaseEntry entry))
            {
                return new List<string>();
            }

            List<string> result = entry.Publishers ?? new List<string>();
            if (result.Count == 0 && depth > 0 && entry.ParentId > 0)
            {
                result = GetPublishers(entry.ParentId, depth - 1);
            }

            return result;
        }

        public int GetReleaseYear(int appId)
        {
            if (!Contains(appId, out DatabaseEntry entry))
            {
                return 0;
            }

            if (DateTime.TryParse(entry.SteamReleaseDate, out DateTime releaseDate))
            {
                return releaseDate.Year;
            }

            return 0;
        }

        public List<string> GetTagList(int appId)
        {
            return GetTagList(appId, 3);
        }

        public List<string> GetTagList(int appId, int depth)
        {
            if (!Contains(appId, out DatabaseEntry entry))
            {
                return new List<string>();
            }

            List<string> tags = entry.Tags ?? new List<string>();
            if (tags.Count == 0 && depth > 0 && entry.ParentId > 0)
            {
                tags = GetTagList(entry.ParentId, depth - 1);
            }

            return tags;
        }

        public VrSupport GetVrSupport(int gameId, int depth = 3)
        {
            if (!Games.ContainsKey(gameId))
            {
                return new VrSupport();
            }

            VrSupport vrSupport = Games[gameId].VrSupport;
            if ((vrSupport.Headsets == null || vrSupport.Headsets.Count == 0) && (vrSupport.Input == null || vrSupport.Input.Count == 0) && (vrSupport.PlayArea == null || vrSupport.PlayArea.Count == 0) && depth > 0 && Games[gameId].ParentId > 0)
            {
                vrSupport = GetVrSupport(Games[gameId].ParentId, depth - 1);
            }

            return vrSupport;
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
            XmlNodeList nodeList = doc?.SelectNodes("/applist/apps/app");
            if (nodeList == null)
            {
                return 0;
            }

            int added = 0;
            foreach (XmlNode node in nodeList)
            {
                if (!XmlUtil.TryGetIntFromNode(node["appid"], out int appId))
                {
                    continue;
                }

                string gameName = XmlUtil.GetStringFromNode(node["name"], null);
                if (Games.ContainsKey(appId))
                {
                    DatabaseEntry entry = Games[appId];
                    if (!string.IsNullOrWhiteSpace(entry.Name) && entry.Name.Equals(gameName))
                    {
                        continue;
                    }

                    entry.Name = gameName;
                    entry.AppType = AppType.Unknown;
                }
                else
                {
                    DatabaseEntry entry = new DatabaseEntry(appId)
                    {
                        Name = gameName
                    };

                    Games.Add(appId, entry);
                    added++;
                }
            }

            Logger.Info(GlobalStrings.GameDB_LoadedNewItemsFromAppList, added);
            return added;
        }

        public void Load(string path)
        {
            lock (SyncRoot)
            {
                Logger.Info("Database: Loading database from '{0}'.", path);
                if (!File.Exists(path))
                {
                    Logger.Warn("Database: Database file not found at '{0}'.", path);

                    return;
                }

                Stopwatch sw = new Stopwatch();
                sw.Start();

                using (StreamReader file = File.OpenText(path))
                {
                    JsonSerializer serializer = new JsonSerializer
                    {
#if DEBUG
                        Formatting = Formatting.Indented
#endif
                    };

                    _instance = (Database) serializer.Deserialize(file, typeof(Database));
                }

                sw.Stop();
                Logger.Info("Database: Loaded database from '{0}', in {1}ms.", path, sw.ElapsedMilliseconds);
            }
        }

        public void Remove(int appId)
        {
            Games.Remove(appId);
        }

        public void Reset()
        {
            lock (SyncRoot)
            {
                Logger.Info("Database: Database was reset.");
                _instance = new Database();
            }
        }

        public void Save(string path)
        {
            lock (SyncRoot)
            {
                Logger.Info("Database: Saving database to '{0}'.", path);

                Stopwatch sw = new Stopwatch();
                sw.Start();

                using (StreamWriter file = File.CreateText(path))
                {
                    JsonSerializer serializer = new JsonSerializer
                    {
#if DEBUG
                        Formatting = Formatting.Indented
#endif
                    };

                    serializer.Serialize(file, _instance);
                }

                sw.Stop();
                Logger.Info("Database: Saved database to '{0}', in {1}ms.", path, sw.ElapsedMilliseconds);
            }
        }

        /// <summary>
        ///     Returns whether the game supports VR
        /// </summary>
        public bool SupportsVr(int gameId, int depth = 3)
        {
            if (!Games.ContainsKey(gameId))
            {
                return false;
            }

            VrSupport vrSupport = Games[gameId].VrSupport;
            if (vrSupport.Headsets != null && vrSupport.Headsets.Count > 0 || vrSupport.Input != null && vrSupport.Input.Count > 0 || vrSupport.PlayArea != null && vrSupport.PlayArea.Count > 0 && depth > 0 && Games[gameId].ParentId > 0)
            {
                return true;
            }

            if (depth > 0 && Games[gameId].ParentId > 0)
            {
                return SupportsVr(Games[gameId].ParentId, depth - 1);
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
                    entry = new DatabaseEntry(aInf.Id);
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
        ///     Counts games for each developer.
        /// </summary>
        /// <param name="counts">
        ///     Existing dictionary of developers and game count. Key is the developer as a string, value is the
        ///     count
        /// </param>
        /// <param name="entry">Entry to add developers from</param>
        private static void CalculateSortedDevListHelper(IDictionary<string, int> counts, DatabaseEntry entry)
        {
            if (entry.Developers == null)
            {
                return;
            }

            foreach (string developer in entry.Developers)
            {
                if (counts.ContainsKey(developer))
                {
                    counts[developer] += 1;
                }
                else
                {
                    counts[developer] = 1;
                }
            }
        }

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

        #endregion
    }
}
