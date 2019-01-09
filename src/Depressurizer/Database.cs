using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;
using Newtonsoft.Json;

namespace Depressurizer
{
    public sealed class Database
    {
        #region Static Fields

        private static readonly object SyncRoot = new object();

        private static volatile Database _instance;

        #endregion

        #region Fields

        public readonly Dictionary<int, DatabaseEntry> Games = new Dictionary<int, DatabaseEntry>();

        private StoreLanguage _language = StoreLanguage.English;

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

        [JsonIgnore]
        public SortedSet<string> AllFlags
        {
            get
            {
                lock (SyncRoot)
                {
                    SortedSet<string> flags = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

                    foreach (DatabaseEntry entry in Values)
                    {
                        flags.UnionWith(entry.Flags);
                    }

                    return flags;
                }
            }
        }

        [JsonIgnore]
        public SortedSet<string> AllGenres
        {
            get
            {
                lock (SyncRoot)
                {
                    SortedSet<string> genres = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

                    foreach (DatabaseEntry entry in Values)
                    {
                        genres.UnionWith(entry.Genres);
                    }

                    return genres;
                }
            }
        }

        [JsonIgnore]
        public LanguageSupport AllLanguages
        {
            get
            {
                lock (SyncRoot)
                {
                    LanguageSupport languageSupport = new LanguageSupport();

                    // ReSharper disable InconsistentNaming
                    SortedSet<string> FullAudio = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
                    SortedSet<string> Interface = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
                    SortedSet<string> Subtitles = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
                    // ReSharper restore InconsistentNaming

                    foreach (DatabaseEntry entry in Values)
                    {
                        FullAudio.UnionWith(entry.LanguageSupport.FullAudio);
                        Interface.UnionWith(entry.LanguageSupport.Interface);
                        Subtitles.UnionWith(entry.LanguageSupport.Subtitles);
                    }

                    languageSupport.FullAudio = FullAudio.ToList();
                    languageSupport.Interface = Interface.ToList();
                    languageSupport.Subtitles = Subtitles.ToList();

                    return languageSupport;
                }
            }
        }

        [JsonIgnore]
        public VRSupport AllVRSupport
        {
            get
            {
                lock (SyncRoot)
                {
                    VRSupport vrSupport = new VRSupport();

                    // ReSharper disable InconsistentNaming
                    SortedSet<string> Headsets = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
                    SortedSet<string> Input = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
                    SortedSet<string> PlayArea = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
                    // ReSharper restore InconsistentNaming

                    foreach (DatabaseEntry entry in Values)
                    {
                        Headsets.UnionWith(entry.VRSupport.Headsets);
                        Input.UnionWith(entry.VRSupport.Input);
                        PlayArea.UnionWith(entry.VRSupport.PlayArea);
                    }

                    vrSupport.Headsets = Headsets.ToList();
                    vrSupport.Input = Input.ToList();
                    vrSupport.PlayArea = PlayArea.ToList();

                    return vrSupport;
                }
            }
        }

        [JsonIgnore]
        public int Count => Games.Count;

        [JsonIgnore]
        public CultureInfo Culture { get; private set; }

        public StoreLanguage Language
        {
            get
            {
                lock (SyncRoot)
                {
                    return _language;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _language = value;
                    Culture = Core.Helpers.Language.GetCultureInfo(_language);
                    LanguageCode = Core.Helpers.Language.LanguageCode(_language);
                }
            }
        }

        [JsonIgnore]
        public string LanguageCode { get; private set; }

        public long LastHLTBUpdate { get; set; }

        [JsonIgnore]
        public Dictionary<int, DatabaseEntry>.ValueCollection Values
        {
            get
            {
                lock (SyncRoot)
                {
                    return Games.Values;
                }
            }
        }

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        public void Add(DatabaseEntry entry)
        {
            if (entry == null)
            {
                return;
            }

            lock (SyncRoot)
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
        }

        public Dictionary<string, int> CalculateSortedDevList(GameList gameList, int minCount)
        {
            Dictionary<string, int> devCounts = new Dictionary<string, int>();
            if (gameList == null)
            {
                foreach (DatabaseEntry entry in Values)
                {
                    CalculateSortedDevListHelper(devCounts, entry);
                }
            }
            else
            {
                foreach (int appId in gameList.Games.Keys)
                {
                    if (Contains(appId, out DatabaseEntry entry) && !gameList.Games[appId].IsHidden)
                    {
                        CalculateSortedDevListHelper(devCounts, entry);
                    }
                }
            }

            return devCounts.Where(e => e.Value >= minCount).ToDictionary(p => p.Key, p => p.Value);
        }

        public Dictionary<string, int> CalculateSortedPubList(GameList filter, int minCount)
        {
            Dictionary<string, int> pubCounts = new Dictionary<string, int>();
            if (filter == null)
            {
                foreach (DatabaseEntry entry in Values)
                {
                    CalculateSortedPubListHelper(pubCounts, entry);
                }
            }
            else
            {
                foreach (int appId in filter.Games.Keys)
                {
                    if (!Contains(appId, out DatabaseEntry entry) || filter.Games[appId].IsHidden)
                    {
                        continue;
                    }

                    CalculateSortedPubListHelper(pubCounts, entry);
                }
            }

            return pubCounts.Where(e => e.Value >= minCount).ToDictionary(p => p.Key, p => p.Value);
        }

        public Dictionary<string, float> CalculateSortedTagList(GameList filter, float weightFactor, int minScore, int tagsPerGame, bool excludeGenres, bool scoreSort)
        {
            Dictionary<string, float> tagCounts = new Dictionary<string, float>();
            if (filter == null)
            {
                foreach (DatabaseEntry entry in Values)
                {
                    CalculateSortedTagListHelper(tagCounts, entry, weightFactor, tagsPerGame);
                }
            }
            else
            {
                foreach (int appId in filter.Games.Keys)
                {
                    if (Contains(appId, out DatabaseEntry entry) && !filter.Games[appId].IsHidden)
                    {
                        CalculateSortedTagListHelper(tagCounts, entry, weightFactor, tagsPerGame);
                    }
                }
            }

            if (excludeGenres)
            {
                foreach (string genre in AllGenres)
                {
                    tagCounts.Remove(genre);
                }
            }

            IEnumerable<KeyValuePair<string, float>> unsorted = tagCounts.Where(e => e.Value >= minScore);
            if (scoreSort)
            {
                return unsorted.OrderByDescending(e => e.Value).ToDictionary(e => e.Key, e => e.Value);
            }

            return unsorted.OrderBy(e => e.Key).ToDictionary(e => e.Key, e => e.Value);
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
            foreach (DatabaseEntry g in Values)
            {
                if (g.Id <= 0)
                {
                    continue;
                }

                g.Tags = null;
                g.Flags = null;
                g.Genres = null;
                g.SteamReleaseDate = null;
                g.LastStoreScrape = 1; //pretend it is really old data
                g.VRSupport = new VRSupport();
                g.LanguageSupport = new LanguageSupport();
            }

            // Update DB with data in correct language
            List<int> appIds = new List<int>();
            if (FormMain.CurrentProfile != null)
            {
                appIds.AddRange(FormMain.CurrentProfile.GameData.Games.Values.Where(g => g.Id > 0).Select(g => g.Id));
                using (DbScrapeDlg dialog = new DbScrapeDlg(appIds))
                {
                    dialog.ShowDialog();
                }
            }

            Save();
        }

        public void Clear()
        {
            lock (SyncRoot)
            {
                Games.Clear();
            }
        }

        public bool Contains(int appId)
        {
            lock (SyncRoot)
            {
                return Games.ContainsKey(appId);
            }
        }

        public bool Contains(int appId, out DatabaseEntry entry)
        {
            lock (SyncRoot)
            {
                entry = null;

                if (Contains(appId))
                {
                    entry = Games[appId];
                }

                return entry != null;
            }
        }

        /// <summary>
        ///     Fetches and integrates the complete list of public apps.
        /// </summary>
        /// <returns>
        ///     The number of new entries.
        /// </returns>
        public int FetchIntegrateAppList()
        {
            int added = 0;
            int updated = 0;

            lock (SyncRoot)
            {
                HttpClient client = null;
                Stream stream = null;
                StreamReader streamReader = null;

                try
                {
                    Logger.Info("Database: Downloading list of public apps.");

                    client = new HttpClient();
                    stream = client.GetStreamAsync(Constants.GetAppList).Result;
                    streamReader = new StreamReader(stream);

                    using (JsonReader reader = new JsonTextReader(streamReader))
                    {
                        streamReader = null;
                        stream = null;
                        client = null;

                        Logger.Info("Database: Downloaded list of public apps.");
                        Logger.Info("Database: Parsing list of public apps.");

                        JsonSerializer serializer = new JsonSerializer();
                        AppList_RawData rawData = serializer.Deserialize<AppList_RawData>(reader);

                        foreach (App app in rawData.Applist.Apps)
                        {
                            if (Contains(app.AppId, out DatabaseEntry entry))
                            {
                                if (!string.IsNullOrWhiteSpace(entry.Name) && entry.Name == app.Name)
                                {
                                    continue;
                                }

                                entry.Name = app.Name;
                                entry.AppType = AppType.Unknown;

                                updated++;
                            }
                            else
                            {
                                entry = new DatabaseEntry(app.AppId)
                                {
                                    Name = app.Name
                                };

                                Add(entry);

                                added++;
                            }
                        }
                    }
                }
                finally
                {
                    streamReader?.Dispose();
                    stream?.Dispose();
                    client?.Dispose();
                }

                Logger.Info("Database: Parsed list of public apps, added {0} apps and updated {1} apps.", added, updated);
            }

            return added;
        }

        public Collection<string> GetDevelopers(int appId)
        {
            return GetDevelopers(appId, 3);
        }

        public Collection<string> GetDevelopers(int appId, int depth)
        {
            if (!Contains(appId, out DatabaseEntry entry))
            {
                return new Collection<string>();
            }

            Collection<string> result = entry.Developers ?? new Collection<string>();
            if (result.Count == 0 && depth > 0 && entry.ParentId > 0)
            {
                result = GetDevelopers(entry.ParentId, depth - 1);
            }

            return result;
        }

        public Collection<string> GetFlagList(int appId)
        {
            return GetFlagList(appId, 3);
        }

        public Collection<string> GetFlagList(int appId, int depth)
        {
            if (!Contains(appId, out DatabaseEntry entry))
            {
                return new Collection<string>();
            }

            Collection<string> result = entry.Flags ?? new Collection<string>();
            if (result.Count == 0 && depth > 0 && entry.ParentId > 0)
            {
                result = GetFlagList(entry.ParentId, depth - 1);
            }

            return result;
        }

        public Collection<string> GetGenreList(int appId, int depth, bool tagFallback)
        {
            if (!Contains(appId, out DatabaseEntry entry))
            {
                return new Collection<string>();
            }

            Collection<string> result = entry.Genres ?? new Collection<string>();
            if (tagFallback && result.Count == 0)
            {
                Collection<string> tags = GetTagList(appId, 0);
                if (tags != null && tags.Count > 0)
                {
                    result = new Collection<string>(tags.Where(tag => AllGenres.Contains(tag)).ToList());
                }
            }

            if (result.Count == 0 && depth > 0 && entry.ParentId > 0)
            {
                result = GetGenreList(entry.ParentId, depth - 1, tagFallback);
            }

            return result;
        }

        public string GetName(int appId)
        {
            if (Contains(appId, out DatabaseEntry entry))
            {
                return entry.Name;
            }

            return string.Empty;
        }

        public Collection<string> GetPublishers(int appId)
        {
            return GetPublishers(appId, 3);
        }

        public Collection<string> GetPublishers(int appId, int depth)
        {
            if (!Contains(appId, out DatabaseEntry entry))
            {
                return new Collection<string>();
            }

            Collection<string> result = entry.Publishers ?? new Collection<string>();
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

        public Collection<string> GetTagList(int appId)
        {
            return GetTagList(appId, 3);
        }

        public Collection<string> GetTagList(int appId, int depth)
        {
            if (!Contains(appId, out DatabaseEntry entry))
            {
                return new Collection<string>();
            }

            Collection<string> tags = entry.Tags ?? new Collection<string>();
            if (tags.Count == 0 && depth > 0 && entry.ParentId > 0)
            {
                tags = GetTagList(entry.ParentId, depth - 1);
            }

            return tags;
        }

        public bool IncludeItemInGameList(int appId)
        {
            if (!Contains(appId, out DatabaseEntry entry))
            {
                return false;
            }

            return entry.AppType == AppType.Application || entry.AppType == AppType.Game;
        }

        public bool IsType(int appId, AppType appType)
        {
            if (!Contains(appId, out DatabaseEntry entry))
            {
                return false;
            }

            return entry.AppType == appType;
        }

        public void Load()
        {
            Load(Locations.File.Database);
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
            lock (SyncRoot)
            {
                Games.Remove(appId);
            }
        }

        public void Reset()
        {
            lock (SyncRoot)
            {
                Logger.Info("Database: Database was reset.");
                _instance = new Database();
            }
        }

        public void Save()
        {
            Save(Locations.File.Database);
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

        public bool SupportsVR(int appId)
        {
            return SupportsVR(appId, 3);
        }

        public bool SupportsVR(int appId, int depth)
        {
            if (!Contains(appId, out DatabaseEntry entry))
            {
                return false;
            }

            VRSupport vrSupport = entry.VRSupport;
            if (vrSupport.Headsets != null && vrSupport.Headsets.Count > 0 || vrSupport.Input != null && vrSupport.Input.Count > 0 || vrSupport.PlayArea != null && vrSupport.PlayArea.Count > 0 && depth > 0 && entry.ParentId > 0)
            {
                return true;
            }

            if (depth > 0 && entry.ParentId > 0)
            {
                return SupportsVR(entry.ParentId, depth - 1);
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
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            foreach (AppInfo aInf in appInfos.Values)
            {
                if (!Contains(aInf.Id, out DatabaseEntry entry))
                {
                    entry = new DatabaseEntry(aInf.Id);
                    Add(entry);
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

        public int UpdateFromHLTB(bool includeImputedTimes)
        {
            int updated = 0;

            lock (SyncRoot)
            {
                HttpClient client = null;
                Stream stream = null;
                StreamReader streamReader = null;

                try
                {
                    client = new HttpClient();
                    stream = client.GetStreamAsync(Constants.HowLongToBeat).Result;
                    streamReader = new StreamReader(stream);

                    using (JsonReader reader = new JsonTextReader(streamReader))
                    {
                        streamReader = null;
                        stream = null;
                        client = null;

                        JsonSerializer serializer = new JsonSerializer();
                        HLTB_RawData rawData = serializer.Deserialize<HLTB_RawData>(reader);

                        foreach (Game game in rawData.Games)
                        {
                            SteamAppData steamAppData = game.SteamAppData;
                            int id = steamAppData.SteamAppId;
                            if (!Contains(id, out DatabaseEntry entry))
                            {
                                continue;
                            }

                            HltbInfo info = steamAppData.HltbInfo;

                            if (!includeImputedTimes && info.MainTtbImputed)
                            {
                                entry.HltbMain = 0;
                            }
                            else
                            {
                                entry.HltbMain = info.MainTtb;
                            }

                            if (!includeImputedTimes && info.ExtrasTtbImputed)
                            {
                                entry.HltbExtras = 0;
                            }
                            else
                            {
                                entry.HltbExtras = info.ExtrasTtb;
                            }

                            if (!includeImputedTimes && info.CompletionistTtbImputed)
                            {
                                entry.HltbCompletionists = 0;
                            }
                            else
                            {
                                entry.HltbCompletionists = info.CompletionistTtb;
                            }

                            updated++;
                        }
                    }
                }
                finally
                {
                    streamReader?.Dispose();
                    stream?.Dispose();
                    client?.Dispose();
                }

                LastHLTBUpdate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
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
                        float inter = i / (float) (tagsToLoad - 1);
                        score = (1 - inter) * weightFactor + inter;
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
