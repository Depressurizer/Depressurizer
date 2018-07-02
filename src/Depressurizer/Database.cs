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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;
using Depressurizer.Dialogs;
using Depressurizer.Models;
using Depressurizer.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Depressurizer
{
	public sealed class Database
	{
		#region Static Fields

		private static readonly object SyncRoot = new object();

		private static volatile Database _instance;

		#endregion

		#region Fields

		public Dictionary<int, DatabaseEntry> Games = new Dictionary<int, DatabaseEntry>();

		public int LastHltbUpdate;

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
			if (Language == language)
			{
				return;
			}

			Language = language;

			foreach (DatabaseEntry entry in Games.Values)
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

		public bool Contains(int appId)
		{
			return Games.ContainsKey(appId);
		}

		public SortedSet<string> GetAllDevelopers()
		{
			SortedSet<string> allStoreDevelopers = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

			foreach (DatabaseEntry entry in Games.Values)
			{
				if (entry.Developers != null)
				{
					allStoreDevelopers.UnionWith(entry.Developers);
				}
			}

			return allStoreDevelopers;
		}

		public SortedSet<string> GetAllGenres()
		{
			SortedSet<string> allStoreGenres = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

			foreach (DatabaseEntry entry in Games.Values)
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

			allLanguages.Interface = Interface.ToList();
			allLanguages.Subtitles = subtitles.ToList();
			allLanguages.FullAudio = fullAudio.ToList();

			return allLanguages;
		}

		public SortedSet<string> GetAllPublishers()
		{
			SortedSet<string> allStorePublishers = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

			foreach (DatabaseEntry entry in Games.Values)
			{
				if (entry.Publishers != null)
				{
					allStorePublishers.UnionWith(entry.Publishers);
				}
			}

			return allStorePublishers;
		}

		public SortedSet<string> GetAllStoreFlags()
		{
			SortedSet<string> allStoreFlags = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

			foreach (DatabaseEntry entry in Games.Values)
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

		public List<string> GetDevelopers(int gameId, int depth = 3)
		{
			if (Games.ContainsKey(gameId))
			{
				List<string> res = Games[gameId].Developers;
				if (((res == null) || (res.Count == 0)) && (depth > 0) && (Games[gameId].ParentId > 0))
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
				if (((res == null) || (res.Count == 0)) && (depth > 0) && (Games[gameId].ParentId > 0))
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
				if (tagFallback && ((res == null) || (res.Count == 0)))
				{
					List<string> tags = GetTagList(gameId, 0);
					if ((tags != null) && (tags.Count > 0))
					{
						res = new List<string>(tags.Intersect(GetAllGenres()));
					}
				}

				if (((res == null) || (res.Count == 0)) && (depth > 0) && (Games[gameId].ParentId > 0))
				{
					res = GetGenreList(Games[gameId].ParentId, depth - 1, tagFallback);
				}

				return res;
			}

			return null;
		}

		public string GetName(int appId)
		{
			return Contains(appId) ? Games[appId].Name : string.Empty;
		}

		public List<string> GetPublishers(int appId, int depth = 3)
		{
			if (!Contains(appId))
			{
				return null;
			}

			List<string> publishers = Games[appId].Publishers;
			if (((publishers == null) || (publishers.Count == 0)) && (depth > 0) && (Games[appId].ParentId > 0))
			{
				publishers = GetPublishers(Games[appId].ParentId, depth - 1);
			}

			return publishers;
		}

		public int GetReleaseYear(int appId)
		{
			if (!Contains(appId))
			{
				return 0;
			}

			DatabaseEntry entry = Games[appId];

			return DateTime.TryParse(entry.SteamReleaseDate, out DateTime releaseDate) ? releaseDate.Year : 0;
		}

		public List<string> GetTagList(int gameId, int depth = 3)
		{
			if (Games.ContainsKey(gameId))
			{
				List<string> res = Games[gameId].Tags;
				if (((res == null) || (res.Count == 0)) && (depth > 0) && (Games[gameId].ParentId > 0))
				{
					res = GetTagList(Games[gameId].ParentId, depth - 1);
				}

				return res;
			}

			return null;
		}

		public VRSupport GetVrSupport(int gameId, int depth = 3)
		{
			if (Games.ContainsKey(gameId))
			{
				VRSupport res = Games[gameId].VrSupport;
				if (((res.Headsets == null) || (res.Headsets.Count == 0)) && ((res.Input == null) || (res.Input.Count == 0)) && ((res.PlayArea == null) || (res.PlayArea.Count == 0)) && (depth > 0) && (Games[gameId].ParentId > 0))
				{
					res = GetVrSupport(Games[gameId].ParentId, depth - 1);
				}

				return res;
			}

			return new VRSupport();
		}

		public bool IncludeItemInGameList(int appId)
		{
			if (!Contains(appId))
			{
				return false;
			}

			DatabaseEntry entry = Games[appId];

			return (entry.AppType == AppType.Application) || (entry.AppType == AppType.Game);
		}

		public int IntegrateAppList(XmlDocument doc)
		{
			int added = 0;
			foreach (XmlNode node in doc.SelectNodes("/applist/apps/app"))
			{
				if (XmlUtil.TryGetIntFromNode(node["appid"], out int appId))
				{
					string gameName = XmlUtil.GetStringFromNode(node["name"], null);
					if (Games.ContainsKey(appId))
					{
						DatabaseEntry g = Games[appId];
						if (string.IsNullOrEmpty(g.Name) || (g.Name != gameName))
						{
							g.Name = gameName;
							g.AppType = AppType.Unknown;
						}
					}
					else
					{
						DatabaseEntry g = new DatabaseEntry
						{
							Id = appId,
							Name = gameName
						};

						Games.Add(appId, g);
						added++;
					}
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

				string database = File.ReadAllText(path);
				_instance = JsonConvert.DeserializeObject<Database>(database);

				Logger.Info("Database: Loaded database from '{0}'.", path);
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

				string database = JsonConvert.SerializeObject(_instance);
				File.WriteAllText(path, database);

				Logger.Info("Database: Saved database to '{0}'.", path);
			}
		}

		public bool SupportsVR(int appId, int depth = 3)
		{
			if (!Contains(appId))
			{
				return false;
			}

			VRSupport res = Games[appId].VrSupport;
			if (((res.Headsets != null) && (res.Headsets.Count > 0)) || ((res.Input != null) && (res.Input.Count > 0)) || ((res.PlayArea != null) && (res.PlayArea.Count > 0) && (depth > 0) && (Games[appId].ParentId > 0)))
			{
				return true;
			}

			if ((depth > 0) && (Games[appId].ParentId > 0))
			{
				return SupportsVR(Games[appId].ParentId, depth - 1);
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
				if (!Games.ContainsKey(aInf.AppId))
				{
					entry = new DatabaseEntry
					{
						Id = aInf.AppId
					};

					Games.Add(entry.Id, entry);
				}
				else
				{
					entry = Games[aInf.AppId];
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

			using (WebClient wc = new WebClient())
			{
				wc.Encoding = Encoding.UTF8;
				string json = wc.DownloadString(Constants.HowLongToBeat);
				JObject parsedJson = JObject.Parse(json);
				dynamic games = parsedJson.SelectToken("Games");
				foreach (dynamic g in games)
				{
					dynamic steamAppData = g.SteamAppData;
					int id = steamAppData.SteamAppId;
					if (Games.ContainsKey(id))
					{
						dynamic htlbInfo = steamAppData.HltbInfo;

						if (!includeImputedTimes && (htlbInfo.MainTtbImputed == "True"))
						{
							Games[id].HltbMain = 0;
						}
						else
						{
							Games[id].HltbMain = htlbInfo.MainTtb;
						}

						if (!includeImputedTimes && (htlbInfo.ExtrasTtbImputed == "True"))
						{
							Games[id].HltbExtras = 0;
						}
						else
						{
							Games[id].HltbExtras = htlbInfo.ExtrasTtb;
						}

						if (!includeImputedTimes && (htlbInfo.CompletionistTtbImputed == "True"))
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
		}

		#endregion
	}
}
