#region LICENSE

//     This file (Database.cs) is part of Depressurizer.
//     Copyright (C) 2018  Martijn Vegter
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
//     along with this program.  If not, see <https://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Depressurizer.Forms;
using Depressurizer.Properties;
using DepressurizerCore;
using DepressurizerCore.Helpers;
using DepressurizerCore.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace Depressurizer
{
    public sealed class Database
    {
        #region Static Fields

        private static readonly object SyncRoot = new object();

        private static volatile Database _instance;

        #endregion

        #region Fields

        private SortedSet<string> _allDevelopers;

        private SortedSet<string> _allFlags;

        private SortedSet<string> _allGenres;

        private SortedSet<string> _allPublishers;

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
        public SortedSet<string> AllDevelopers
        {
            get
            {
                lock (SyncRoot)
                {
                    if (_allDevelopers == null)
                    {
                        _allDevelopers = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
                    }

                    _allDevelopers.Clear();

                    foreach (DatabaseEntry entry in Apps.Values)
                    {
                        _allDevelopers.UnionWith(entry.Developers);
                    }

                    return _allDevelopers;
                }
            }
        }
        public SortedSet<string> AllFlags
        {
            get
            {
                lock (SyncRoot)
                {
                    if (_allFlags == null)
                    {
                        _allFlags = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
                    }

                    _allFlags.Clear();

                    foreach (DatabaseEntry entry in Apps.Values)
                    {
                        _allFlags.UnionWith(entry.Flags);
                    }

                    return _allFlags;
                }
            }
        }

        public SortedSet<string> AllGenres
        {
            get
            {
                lock (SyncRoot)
                {
                    if (_allGenres == null)
                    {
                        _allGenres = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
                    }

                    _allGenres.Clear();

                    foreach (DatabaseEntry entry in Apps.Values)
                    {
                        _allGenres.UnionWith(entry.Genres);
                    }

                    return _allGenres;
                }
            }
        }
        public SortedSet<string> AllPublishers
        {
            get
            {
                lock (SyncRoot)
                {
                    if (_allPublishers == null)
                    {
                        _allPublishers = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
                    }

                    _allPublishers.Clear();

                    foreach (DatabaseEntry entry in Apps.Values)
                    {
                        _allPublishers.UnionWith(entry.Publishers);
                    }

                    return _allPublishers;
                }
            }
        }

        // Main Data
        public ConcurrentDictionary<int, DatabaseEntry> Apps { get; } = new ConcurrentDictionary<int, DatabaseEntry>();

        public StoreLanguage Language { get; set; } = StoreLanguage.Default;

        public long LastHLTBUpdate { get; set; }

        #endregion

        #region Properties

        private LanguageSupport AllLanguages { get; set; }

        private VRSupport AllVrSupportFlags { get; set; }

        #endregion

        #region Public Methods and Operators

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

        /// <summary>
        ///     Gets a list of all Game Languages found in the entire database.
        ///     Always recalculates.
        /// </summary>
        /// <returns>A LanguageSupport struct containing the languages</returns>
        public LanguageSupport CalculateAllLanguages()
        {
            AllLanguages = new LanguageSupport();

            SortedSet<string> Interface = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> subtitles = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> fullAudio = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (DatabaseEntry entry in Apps.Values)
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

            AllLanguages.Interface = Interface.ToList();
            AllLanguages.Subtitles = subtitles.ToList();
            AllLanguages.FullAudio = fullAudio.ToList();

            return AllLanguages;
        }

        /// <summary>
        ///     Gets a list of all Steam store VR Support flags found in the entire database.
        ///     Always recalculates.
        /// </summary>
        /// <returns>A VRSupport struct containing the flags</returns>
        public VRSupport CalculateAllVrSupportFlags()
        {
            SortedSet<string> headsets = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> input = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> playArea = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (DatabaseEntry entry in Apps.Values)
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

            AllVrSupportFlags.Headsets = headsets.ToList();
            AllVrSupportFlags.Input = input.ToList();
            AllVrSupportFlags.PlayArea = playArea.ToList();

            return AllVrSupportFlags;
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
            Dictionary<string, int> devCounts = new Dictionary<string, int>();
            if (filter == null)
            {
                foreach (DatabaseEntry dbEntry in Apps.Values)
                {
                    CalculateSortedDevListHelper(devCounts, dbEntry);
                }
            }
            else
            {
                foreach (int gameId in filter.Games.Keys)
                {
                    if (Apps.ContainsKey(gameId) && !filter.Games[gameId].Hidden)
                    {
                        CalculateSortedDevListHelper(devCounts, Apps[gameId]);
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
            Dictionary<string, int> pubCounts = new Dictionary<string, int>();
            if (filter == null)
            {
                foreach (DatabaseEntry dbEntry in Apps.Values)
                {
                    CalculateSortedPubListHelper(pubCounts, dbEntry);
                }
            }
            else
            {
                foreach (int gameId in filter.Games.Keys)
                {
                    if (Apps.ContainsKey(gameId) && !filter.Games[gameId].Hidden)
                    {
                        CalculateSortedPubListHelper(pubCounts, Apps[gameId]);
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
            SortedSet<string> genreNames = AllGenres;
            Dictionary<string, float> tagCounts = new Dictionary<string, float>();
            if (filter == null)
            {
                foreach (DatabaseEntry dbEntry in Apps.Values)
                {
                    CalculateSortedTagListHelper(tagCounts, dbEntry, weightFactor, tagsPerGame);
                }
            }
            else
            {
                foreach (int gameId in filter.Games.Keys)
                {
                    if (Apps.ContainsKey(gameId) && !filter.Games[gameId].Hidden)
                    {
                        CalculateSortedTagListHelper(tagCounts, Apps[gameId], weightFactor, tagsPerGame);
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

        public void ChangeLanguage(StoreLanguage newLanguage)
        {
            lock (SyncRoot)
            {
                if (newLanguage == StoreLanguage.Default)
                {
                    Enum.TryParse(Settings.Instance.InterfaceLanguage.ToString(), out newLanguage);
                }

                if (Language == newLanguage || newLanguage == StoreLanguage.Default)
                {
                    return;
                }

                Language = Settings.Instance.StoreLanguage = newLanguage;

                /* Clean database */
                foreach (DatabaseEntry g in Apps.Values)
                {
                    if (g.Id <= 0)
                    {
                        continue;
                    }

                    g.Tags = null;
                    g.Flags = null;
                    g.Genres = null;
                    g.SteamReleaseDate = null;
                    g.LastStoreScrape = 0;
                    g.VRSupport = null;
                    g.LanguageSupport = null;
                }

                /* Update database */
                List<int> gamesToUpdate = new List<int>();
                if (FormMain.CurrentProfile != null)
                {
                    foreach (GameInfo game in FormMain.CurrentProfile.GameData.Games.Values)
                    {
                        if (game.Id > 0)
                        {
                            gamesToUpdate.Add(game.Id);
                        }
                    }

                    using (ScrapeDialog dialog = new ScrapeDialog(gamesToUpdate))
                    {
                        dialog.ShowDialog();
                    }

                    foreach (GameInfo game in FormMain.CurrentProfile.GameData.Games.Values)
                    {
                        if (Contains(game.Id))
                        {
                            game.Name = Apps[game.Id].Name;
                        }
                    }
                }

                Save();
            }
        }

        public bool Contains(int id)
        {
            return Apps.ContainsKey(id);
        }

        /// <summary>
        ///     Gets a list of all Game Languages found in the entire database.
        ///     Only recalculates if necessary.
        /// </summary>
        /// <returns>A LanguageSupport struct containing the languages</returns>
        public LanguageSupport GetAllLanguages()
        {
            if (AllLanguages == null || AllLanguages.FullAudio == null || AllLanguages.Interface == null || AllLanguages.Subtitles == null)
            {
                return CalculateAllLanguages();
            }

            return AllLanguages;
        }

        /// <summary>
        ///     Gets a list of all Steam store VR Support flags found in the entire database.
        ///     Only recalculates if necessary.
        /// </summary>
        /// <returns>A VRSupport struct containing the flags</returns>
        public VRSupport GetAllVrSupportFlags()
        {
            if (AllVrSupportFlags.Headsets == null || AllVrSupportFlags.Input == null || AllVrSupportFlags.PlayArea == null)
            {
                return CalculateAllVrSupportFlags();
            }

            return AllVrSupportFlags;
        }

        public List<string> GetDevelopers(int gameId, int depth = 3)
        {
            if (Apps.ContainsKey(gameId))
            {
                List<string> res = Apps[gameId].Developers;
                if ((res == null || res.Count == 0) && depth > 0 && Apps[gameId].ParentId > 0)
                {
                    res = GetDevelopers(Apps[gameId].ParentId, depth - 1);
                }

                return res;
            }

            return null;
        }

        public List<string> GetFlagList(int gameId, int depth = 3)
        {
            if (Apps.ContainsKey(gameId))
            {
                List<string> res = Apps[gameId].Flags;
                if ((res == null || res.Count == 0) && depth > 0 && Apps[gameId].ParentId > 0)
                {
                    res = GetFlagList(Apps[gameId].ParentId, depth - 1);
                }

                return res;
            }

            return null;
        }

        public List<string> GetGenreList(int gameId, int depth = 3, bool tagFallback = true)
        {
            if (Apps.ContainsKey(gameId))
            {
                List<string> res = Apps[gameId].Genres;
                if (tagFallback && (res == null || res.Count == 0))
                {
                    List<string> tags = GetTagList(gameId, 0);
                    if (tags != null && tags.Count > 0)
                    {
                        res = new List<string>(tags.Intersect(AllGenres));
                    }
                }

                if ((res == null || res.Count == 0) && depth > 0 && Apps[gameId].ParentId > 0)
                {
                    res = GetGenreList(Apps[gameId].ParentId, depth - 1, tagFallback);
                }

                return res;
            }

            return null;
        }

        public string GetName(int appId)
        {
            return Contains(appId) ? Apps[appId].Name : null;
        }

        public List<string> GetPublishers(int gameId, int depth = 3)
        {
            if (Apps.ContainsKey(gameId))
            {
                List<string> res = Apps[gameId].Publishers;
                if ((res == null || res.Count == 0) && depth > 0 && Apps[gameId].ParentId > 0)
                {
                    res = GetPublishers(Apps[gameId].ParentId, depth - 1);
                }

                return res;
            }

            return null;
        }

        public int GetReleaseYear(int gameId)
        {
            if (Apps.ContainsKey(gameId))
            {
                DatabaseEntry dbEntry = Apps[gameId];
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
            if (Apps.ContainsKey(gameId))
            {
                List<string> res = Apps[gameId].Tags;
                if ((res == null || res.Count == 0) && depth > 0 && Apps[gameId].ParentId > 0)
                {
                    res = GetTagList(Apps[gameId].ParentId, depth - 1);
                }

                return res;
            }

            return null;
        }

        public VRSupport GetVrSupport(int gameId, int depth = 3)
        {
            if (Apps.ContainsKey(gameId))
            {
                VRSupport res = Apps[gameId].VRSupport;
                if ((res.Headsets == null || res.Headsets.Count == 0) && (res.Input == null || res.Input.Count == 0) && (res.PlayArea == null || res.PlayArea.Count == 0) && depth > 0 && Apps[gameId].ParentId > 0)
                {
                    res = GetVrSupport(Apps[gameId].ParentId, depth - 1);
                }

                return res;
            }

            return new VRSupport();
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
                    if (Apps.ContainsKey(appId))
                    {
                        DatabaseEntry g = Apps[appId];
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
                        Apps.TryAdd(appId, g);
                        added++;
                    }
                }
            }

            return added;
        }

        public void Load()
        {
            lock (SyncRoot)
            {
                if (!File.Exists("db.json"))
                {
                    return;
                }

                try
                {
                    string json = File.ReadAllText("db.json");
                    _instance = JsonConvert.DeserializeObject<Database>(json, new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                    throw;
                }
            }
        }

        public void Save()
        {
            lock (SyncRoot)
            {
                try
                {
                    string json = JsonConvert.SerializeObject(_instance, new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        TypeNameHandling = TypeNameHandling.Auto
                    });

                    File.WriteAllText("db.json", json);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                    throw;
                }
            }
        }

        /// <summary>
        ///     Returns whether the game supports VR
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public bool SupportsVR(int appId, int depth = 3)
        {
            if (!Apps.ContainsKey(appId))
            {
                return false;
            }

            VRSupport vrSupport = Apps[appId].VRSupport;
            if (vrSupport.Headsets.Count > 0 || vrSupport.Input.Count > 0 || vrSupport.PlayArea.Count > 0 && depth > 0 && Apps[appId].ParentId > 0)
            {
                return true;
            }

            if (depth > 0 && Apps[appId].ParentId > 0)
            {
                return SupportsVR(Apps[appId].ParentId, depth - 1);
            }

            return false;
        }

        public void UpdateAppListFromWeb()
        {
            XmlDocument doc = FetchAppListFromWeb();
            IntegrateAppList(doc);
        }

        public int UpdateFromAppInfo(string path)
        {
            Logger.Instance.Info("Database: Updating from AppInfo");
            int updated = 0;

            lock (SyncRoot)
            {
                Dictionary<int, AppInfo> appInfos = AppInfo.LoadApps(path);
                long currentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                Parallel.ForEach(appInfos.Values, appInfo =>
                {
                    DatabaseEntry entry;

                    if (Contains(appInfo.Id))
                    {
                        entry = Apps[appInfo.Id];
                    }
                    else
                    {
                        entry = new DatabaseEntry(appInfo.Id);
                        Apps.TryAdd(entry.Id, entry);
                    }

                    entry.LastAppInfoUpdate = currentUnixTime;

                    if (appInfo.AppType != AppType.Unknown)
                    {
                        entry.AppType = appInfo.AppType;
                    }

                    if (!string.IsNullOrEmpty(appInfo.Name))
                    {
                        entry.Name = appInfo.Name;
                    }

                    if (entry.Platforms == AppPlatforms.None || entry.LastStoreScrape == 0 && appInfo.Platforms > AppPlatforms.None)
                    {
                        entry.Platforms = appInfo.Platforms;
                    }

                    if (appInfo.Parent > 0)
                    {
                        entry.ParentId = appInfo.Parent;
                    }

                    updated++;
                });
            }

            Logger.Instance.Info("Database: Updated from AppInfo");

            return updated;
        }

        public int UpdateFromHLTB(bool includeImputedTimes)
        {
            int updated = 0;

            lock (SyncRoot)
            {
                try
                {
                    using (WebClient webClient = new WebClient())
                    {
                        webClient.Headers.Set("User-Agent", "Depressurizer");
                        webClient.Encoding = Encoding.UTF8;

                        string json = webClient.DownloadString(Constant.HowLongToBeatURL);
                        JObject parsedJson = JObject.Parse(json);
                        dynamic games = parsedJson.SelectToken("Games");

                        foreach (dynamic g in games)
                        {
                            dynamic steamAppData = g.SteamAppData;
                            int id = steamAppData.SteamAppId;
                            if (!Contains(id))
                            {
                                continue;
                            }

                            dynamic htlbInfo = steamAppData.HltbInfo;

                            if (!includeImputedTimes && htlbInfo.MainTtbImputed == "True")
                            {
                                Apps[id].HltbMain = 0;
                            }
                            else
                            {
                                Apps[id].HltbMain = htlbInfo.MainTtb;
                            }

                            if (!includeImputedTimes && htlbInfo.ExtrasTtbImputed == "True")
                            {
                                Apps[id].HltbExtras = 0;
                            }
                            else
                            {
                                Apps[id].HltbExtras = htlbInfo.ExtrasTtb;
                            }

                            if (!includeImputedTimes && htlbInfo.CompletionistTtbImputed == "True")
                            {
                                Apps[id].HltbCompletionist = 0;
                            }
                            else
                            {
                                Apps[id].HltbCompletionist = htlbInfo.CompletionistTtb;
                            }

                            updated++;
                        }
                    }

                    LastHLTBUpdate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                }
                catch (Exception e)
                {
                    SentryLogger.LogException(e);

                    throw;
                }
            }

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

        /// <summary>
        ///     Counts games for each publisher.
        /// </summary>
        /// <param name="counts">
        ///     Existing dictionary of publishers and game count. Key is the publisher as a string, value is the
        ///     count
        /// </param>
        /// <param name="dbEntry">Entry to add publishers from</param>
        private void CalculateSortedPubListHelper(Dictionary<string, int> counts, DatabaseEntry dbEntry)
        {
            if (dbEntry.Publishers != null)
            {
                for (int i = 0; i < dbEntry.Publishers.Count; i++)
                {
                    string pub = dbEntry.Publishers[i];
                    if (counts.ContainsKey(pub))
                    {
                        counts[pub] += 1;
                    }
                    else
                    {
                        counts[pub] = 1;
                    }
                }
            }
        }

        /// <summary>
        ///     Adds tags from the given DBEntry to the dictionary. Adds new elements if necessary, and increases values on
        ///     existing elements.
        /// </summary>
        /// <param name="counts">Existing dictionary of tags and scores. Key is the tag as a string, value is the score</param>
        /// <param name="dbEntry">Entry to add tags from</param>
        /// <param name="weightFactor">
        ///     The score value of the first tag in the list.
        ///     The first tag on the game will have this score, and the last tag processed will always have score 1.
        ///     The tags between will have linearly interpolated values between them.
        /// </param>
        /// <param name="tagsPerGame"></param>
        private void CalculateSortedTagListHelper(Dictionary<string, float> counts, DatabaseEntry dbEntry, float weightFactor, int tagsPerGame)
        {
            if (dbEntry.Tags != null)
            {
                int tagsToLoad = tagsPerGame == 0 ? dbEntry.Tags.Count : Math.Min(tagsPerGame, dbEntry.Tags.Count);
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

        #endregion
    }
}