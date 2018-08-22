#region License

//     This file (Database.cs) is part of Depressurizer.
//     Copyright (C) 2011  Steve Labbe
//     Copyright (C) 2017  Theodoros Dimos
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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Interfaces;
using Depressurizer.Core.Models;
using Depressurizer.Dialogs;
using Depressurizer.Models;
using Depressurizer.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Depressurizer
{
	public sealed class Database : IRepository<DatabaseEntry>
	{
		#region Static Fields

		private static readonly object SyncRoot = new object();

		private static volatile Database _instance;

		#endregion

		#region Fields

		[JsonProperty]
		private readonly Dictionary<int, DatabaseEntry> _database = new Dictionary<int, DatabaseEntry>();

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
					if (_instance != null)
					{
						return _instance;
					}

					_instance = new Database();
					JsonConvert.DeserializeObject<Database>(JsonConvert.SerializeObject(_instance));
				}

				return _instance;
			}
		}

		[JsonIgnore]
		public int Count
		{
			get
			{
				lock (SyncRoot)
				{
					return _database.Count;
				}
			}
		}

		public StoreLanguage Language { get; set; } = StoreLanguage.English;

		public long LastHLTBUpdate { get; set; }

		#endregion

		#region Properties

		private static Logger Logger => Logger.Instance;

		#endregion

		#region Public Methods and Operators

		public void Add(DatabaseEntry entry)
		{
			lock (SyncRoot)
			{
				_database.Add(entry.Id, entry);
			}
		}

		public IEnumerable<Tuple<string, int>> CalculateSortedDevList(GameList filter, int minCount)
		{
			Dictionary<string, int> devCounts = new Dictionary<string, int>();
			if (filter == null)
			{
				foreach (DatabaseEntry dbEntry in GetAll())
				{
					CalculateSortedDevListHelper(devCounts, dbEntry);
				}
			}
			else
			{
				foreach (int gameId in filter.Games.Keys)
				{
					if (Contains(gameId, out DatabaseEntry entry) && !filter.Games[gameId].Hidden)
					{
						CalculateSortedDevListHelper(devCounts, entry);
					}
				}
			}

			IEnumerable<Tuple<string, int>> unsortedList = from entry in devCounts where entry.Value >= minCount select new Tuple<string, int>(entry.Key, entry.Value);

			return unsortedList.ToList();
		}

		public IEnumerable<Tuple<string, int>> CalculateSortedPubList(GameList filter, int minCount)
		{
			Dictionary<string, int> pubCounts = new Dictionary<string, int>();
			if (filter == null)
			{
				foreach (DatabaseEntry dbEntry in GetAll())
				{
					CalculateSortedPubListHelper(pubCounts, dbEntry);
				}
			}
			else
			{
				foreach (int gameId in filter.Games.Keys)
				{
					if (Contains(gameId, out DatabaseEntry entry) && !filter.Games[gameId].Hidden)
					{
						CalculateSortedPubListHelper(pubCounts, entry);
					}
				}
			}

			IEnumerable<Tuple<string, int>> unsortedList = from entry in pubCounts where entry.Value >= minCount select new Tuple<string, int>(entry.Key, entry.Value);

			return unsortedList.ToList();
		}

		public IEnumerable<Tuple<string, float>> CalculateSortedTagList(GameList filter, float weightFactor, int minScore, int tagsPerGame, bool excludeGenres, bool scoreSort)
		{
			SortedSet<string> genreNames = GetAllGenres();
			Dictionary<string, float> tagCounts = new Dictionary<string, float>();
			if (filter == null)
			{
				foreach (DatabaseEntry dbEntry in GetAll())
				{
					CalculateSortedTagListHelper(tagCounts, dbEntry, weightFactor, tagsPerGame);
				}
			}
			else
			{
				foreach (int gameId in filter.Games.Keys)
				{
					if (Contains(gameId, out DatabaseEntry entry) && !filter.Games[gameId].Hidden)
					{
						CalculateSortedTagListHelper(tagCounts, entry, weightFactor, tagsPerGame);
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
			if (Language == language)
			{
				return;
			}

			Language = language;

			foreach (DatabaseEntry entry in GetAll())
			{
				if (entry.Id <= 0)
				{
					continue;
				}

				entry.Clear();
			}

			List<int> toUpdate = new List<int>();
			if (FormMain.CurrentProfile != null)
			{
				foreach (GameInfo game in FormMain.CurrentProfile.GameData.Games.Values)
				{
					if (game.Id > 0)
					{
						toUpdate.Add(game.Id);
					}
				}

				using (ScrapeDialog dialog = new ScrapeDialog(toUpdate))
				{
					dialog.ShowDialog();
				}
			}

			Save(Location.File.Database);
		}

		public void Clear()
		{
			lock (SyncRoot)
			{
				_database.Clear();
			}
		}

		public bool Contains(int id)
		{
			lock (SyncRoot)
			{
				return _database.ContainsKey(id);
			}
		}

		public bool Contains(int id, out DatabaseEntry entry)
		{
			lock (SyncRoot)
			{
				return _database.TryGetValue(id, out entry);
			}
		}

		public IQueryable<DatabaseEntry> GetAll()
		{
			lock (SyncRoot)
			{
				return _database.Values.AsQueryable();
			}
		}

		public SortedSet<string> GetAllGenres()
		{
			SortedSet<string> allStoreGenres = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

			foreach (DatabaseEntry entry in GetAll())
			{
				if (entry.Genres != null)
				{
					allStoreGenres.UnionWith(entry.Genres);
				}
			}

			return allStoreGenres;
		}

		public LanguageSupport GetAllLanguages()
		{
			LanguageSupport allLanguages = new LanguageSupport();

			SortedSet<string> Interface = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
			SortedSet<string> subtitles = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
			SortedSet<string> fullAudio = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

			foreach (DatabaseEntry entry in GetAll())
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

			allLanguages.Interface = Interface.ToList();
			allLanguages.Subtitles = subtitles.ToList();
			allLanguages.FullAudio = fullAudio.ToList();

			return allLanguages;
		}

		public SortedSet<string> GetAllStoreFlags()
		{
			SortedSet<string> allStoreFlags = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

			foreach (DatabaseEntry entry in GetAll())
			{
				if (entry.Flags != null)
				{
					allStoreFlags.UnionWith(entry.Flags);
				}
			}

			return allStoreFlags;
		}

		public VRSupport GetAllVrSupportFlags()
		{
			VRSupport allVrSupportFlags = new VRSupport();

			SortedSet<string> headsets = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
			SortedSet<string> input = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
			SortedSet<string> playArea = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

			foreach (DatabaseEntry entry in GetAll())
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

		public List<string> GetDevelopers(int gameId, int depth = 3)
		{
			if (!Contains(gameId, out DatabaseEntry entry))
			{
				return null;
			}

			List<string> res = entry.Developers;
			if (((res == null) || (res.Count == 0)) && (depth > 0) && (entry.ParentId > 0))
			{
				res = GetDevelopers(entry.ParentId, depth - 1);
			}

			return res;
		}

		public List<string> GetFlagList(int gameId, int depth = 3)
		{
			if (!Contains(gameId, out DatabaseEntry entry))
			{
				return null;
			}

			List<string> res = entry.Flags;
			if (((res == null) || (res.Count == 0)) && (depth > 0) && (entry.ParentId > 0))
			{
				res = GetFlagList(entry.ParentId, depth - 1);
			}

			return res;
		}

		public List<string> GetGenreList(int gameId, int depth = 3, bool tagFallback = true)
		{
			if (!Contains(gameId, out DatabaseEntry entry))
			{
				return null;
			}

			List<string> res = entry.Genres;
			if (tagFallback && ((res == null) || (res.Count == 0)))
			{
				List<string> tags = GetTagList(gameId, 0);
				if ((tags != null) && (tags.Count > 0))
				{
					res = new List<string>(tags.Intersect(GetAllGenres()));
				}
			}

			if (((res == null) || (res.Count == 0)) && (depth > 0) && (entry.ParentId > 0))
			{
				res = GetGenreList(entry.ParentId, depth - 1, tagFallback);
			}

			return res;
		}

		/// <summary>
		///     Gets and integrates the complete list of public apps.
		/// </summary>
		/// <returns>
		///     The number of new entries.
		/// </returns>
		public int GetIntegrateAppList()
		{
			int added = 0;

			Logger.Info("Database: Downloading list of public apps.");

			string json;
			using (WebClient client = new WebClient())
			{
				client.Headers.Set("User-Agent", "Depressurizer");
				json = client.DownloadString("https://api.steampowered.com/ISteamApps/GetAppList/v2/");
			}

			Logger.Info("Database: Downloaded list of public apps.");
			Logger.Info("Database: Parsing list of public apps.");

			dynamic appList = JObject.Parse(json);
			foreach (dynamic app in appList.applist.apps)
			{
				int appId = app["appid"];
				string name = app["name"];

				if (Contains(appId, out DatabaseEntry entry))
				{
					if (!string.IsNullOrWhiteSpace(entry.Name) && (entry.Name == name))
					{
						continue;
					}

					entry.Name = name;
					entry.AppType = AppType.Unknown;
				}
				else
				{
					entry = new DatabaseEntry(appId)
					{
						Name = name
					};

					Add(entry);
					added++;
				}
			}

			Logger.Info($"Database: Parsed list of public apps, added {added} apps.");

			return added;
		}

		public string GetName(int appId)
		{
			return Contains(appId, out DatabaseEntry entry) ? entry.Name : string.Empty;
		}

		public List<string> GetPublishers(int appId, int depth = 3)
		{
			if (!Contains(appId, out DatabaseEntry entry))
			{
				return null;
			}

			List<string> publishers = entry.Publishers;
			if (((publishers == null) || (publishers.Count == 0)) && (depth > 0) && (entry.ParentId > 0))
			{
				publishers = GetPublishers(entry.ParentId, depth - 1);
			}

			return publishers;
		}

		public int GetReleaseYear(int appId)
		{
			if (!Contains(appId, out DatabaseEntry entry))
			{
				return 0;
			}

			return DateTime.TryParse(entry.SteamReleaseDate, out DateTime releaseDate) ? releaseDate.Year : 0;
		}

		public List<string> GetTagList(int gameId, int depth = 3)
		{
			if (!Contains(gameId, out DatabaseEntry entry))
			{
				return null;
			}

			List<string> res = entry.Tags;
			if (((res == null) || (res.Count == 0)) && (depth > 0) && (entry.ParentId > 0))
			{
				res = GetTagList(entry.ParentId, depth - 1);
			}

			return res;
		}

		public VRSupport GetVrSupport(int gameId, int depth = 3)
		{
			if (!Contains(gameId, out DatabaseEntry entry))
			{
				return new VRSupport();
			}

			VRSupport res = entry.VrSupport;
			if (((res.Headsets == null) || (res.Headsets.Count == 0)) && ((res.Input == null) || (res.Input.Count == 0)) && ((res.PlayArea == null) || (res.PlayArea.Count == 0)) && (depth > 0) && (entry.ParentId > 0))
			{
				res = GetVrSupport(entry.ParentId, depth - 1);
			}

			return res;
		}

		public bool IncludeItemInGameList(int appId)
		{
			if (!Contains(appId, out DatabaseEntry entry))
			{
				return false;
			}

			return (entry.AppType == AppType.Application) || (entry.AppType == AppType.Game);
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
					JsonSerializer serializer = new JsonSerializer();
					_instance = (Database) serializer.Deserialize(file, typeof(Database));
				}

				sw.Stop();
				Logger.Info("Database: Loaded database from '{0}', in {1}ms.", path, sw.ElapsedMilliseconds);
			}
		}

		public void Remove(DatabaseEntry entity)
		{
			lock (SyncRoot)
			{
				_database.Remove(entity.Id);
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

		public void Save(string path)
		{
			lock (SyncRoot)
			{
				Logger.Info("Database: Saving database to '{0}'.", path);

				Stopwatch sw = new Stopwatch();
				sw.Start();

				using (StreamWriter file = File.CreateText(path))
				{
					JsonSerializer serializer = new JsonSerializer();
					serializer.Serialize(file, _instance);
				}

				sw.Stop();
				Logger.Info("Database: Saved database to '{0}', in {1}ms.", path, sw.ElapsedMilliseconds);
			}
		}

		public bool SupportsVR(int appId, int depth = 3)
		{
			if (!Contains(appId, out DatabaseEntry entry))
			{
				return false;
			}

			VRSupport res = entry.VrSupport;
			if (((res.Headsets != null) && (res.Headsets.Count > 0)) || ((res.Input != null) && (res.Input.Count > 0)) || ((res.PlayArea != null) && (res.PlayArea.Count > 0) && (depth > 0) && (entry.ParentId > 0)))
			{
				return true;
			}

			if ((depth > 0) && (entry.ParentId > 0))
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
				if (!Contains(aInf.AppId, out DatabaseEntry entry))
				{
					entry = new DatabaseEntry(aInf.AppId);
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

				if ((entry.Platforms == AppPlatforms.None) || ((entry.LastStoreScrape == 0) && (aInf.Platforms > AppPlatforms.None)))
				{
					entry.Platforms = aInf.Platforms;
				}

				if (aInf.ParentId > 0)
				{
					entry.ParentId = aInf.ParentId;
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

			HttpClient client = new HttpClient();

			using (Stream s = client.GetStreamAsync(Constants.HowLongToBeat).Result)
			using (StreamReader sr = new StreamReader(s))
			using (JsonReader reader = new JsonTextReader(sr))
			{
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

					HltbInfo htlbInfo = steamAppData.HltbInfo;

					if (!includeImputedTimes && htlbInfo.MainTtbImputed)
					{
						entry.HltbMain = 0;
					}
					else
					{
						entry.HltbMain = htlbInfo.MainTtb;
					}

					if (!includeImputedTimes && htlbInfo.ExtrasTtbImputed)
					{
						entry.HltbExtras = 0;
					}
					else
					{
						entry.HltbExtras = htlbInfo.ExtrasTtb;
					}

					if (!includeImputedTimes && htlbInfo.CompletionistTtbImputed)
					{
						entry.HltbCompletionist = 0;
					}
					else
					{
						entry.HltbCompletionist = htlbInfo.CompletionistTtb;
					}

					updated++;
				}
			}

			LastHLTBUpdate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

			return updated;
		}

		#endregion

		#region Methods

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

		private static void CalculateSortedTagListHelper(IDictionary<string, float> counts, DatabaseEntry dbEntry, float weightFactor, int tagsPerGame)
		{
			if (dbEntry.Tags == null)
			{
				return;
			}

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
						score = ((1 - interp) * weightFactor) + interp;
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

		#endregion
	}
}
