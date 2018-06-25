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
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Models;
using Depressurizer.Dialogs;
using Depressurizer.Enums;
using Depressurizer.Helpers;
using Depressurizer.Models;
using Depressurizer.Properties;
using Newtonsoft.Json.Linq;

namespace Depressurizer
{
	public class Database
	{
		#region Constants

		// Utility
		private const int VERSION = 2;

		private const string XmlName_RootNode = "gamelist", XmlName_Version = "version", XmlName_LastHltbUpdate = "lastHltbUpdate", XmlName_dbLanguage = "dbLanguage", XmlName_Games = "games";

		#endregion

		#region Fields

		public StoreLanguage dbLanguage = StoreLanguage.English;

		// Main Data
		public Dictionary<int, DatabaseEntry> Games = new Dictionary<int, DatabaseEntry>();

		public int LastHltbUpdate;

		private readonly VRSupport allVrSupportFlags = new VRSupport();

		private LanguageSupport allLanguages;

		private SortedSet<string> allStoreDevelopers;

		private SortedSet<string> allStoreFlags;

		// Extra data
		private SortedSet<string> allStoreGenres;

		private SortedSet<string> allStorePublishers;

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
			if (Program.Database == null)
			{
				return;
			}

			if (dbLanguage == language)
			{
				return;
			}

			dbLanguage = language;

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

			Save("GameDB.xml.gz");
		}

		public bool Contains(int appId)
		{
			return Games.ContainsKey(appId);
		}

		public SortedSet<string> GetAllDevelopers()
		{
			allStoreDevelopers = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

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
			allStoreGenres = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

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
			allStorePublishers = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

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
			allStoreFlags = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

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
			Load(path, path.EndsWith(".gz"));
		}

		public void Load(string path, bool compress)
		{
			Logger.Info(GlobalStrings.GameDB_LoadingGameDBFrom, path);
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

				Logger.Info(GlobalStrings.GameDB_GameDBXMLParsed);
				Games.Clear();
				ClearAggregates();

				XmlNode gameListNode = doc.SelectSingleNode("/" + XmlName_RootNode);

				int fileVersion = XmlUtil.GetIntFromNode(gameListNode[XmlName_Version], 0);

				LastHltbUpdate = XmlUtil.GetIntFromNode(gameListNode[XmlName_LastHltbUpdate], 0);

				dbLanguage = (StoreLanguage) Enum.Parse(typeof(StoreLanguage), XmlUtil.GetStringFromNode(gameListNode[XmlName_dbLanguage], "en"), true);

				if (fileVersion == 1)
				{
					LoadGamelistVersion1(gameListNode);
				}
				else
				{
					XmlSerializer x = new XmlSerializer(typeof(DatabaseEntry));
					foreach (XmlNode gameNode in gameListNode.SelectSingleNode(XmlName_Games).ChildNodes)
					{
						XmlReader reader = new XmlNodeReader(gameNode);
						DatabaseEntry entry = (DatabaseEntry) x.Deserialize(reader);
						Games.Add(entry.Id, entry);
					}
				}

				Logger.Info("GameDB XML processed, load complete. Db Language: " + dbLanguage);
			}
			finally
			{
				stream?.Close();
			}
		}

		public void Save(string path)
		{
			Save(path, path.EndsWith(".gz"));
		}

		public void Save(string path, bool compress)
		{
			Logger.Info(GlobalStrings.GameDB_SavingGameDBTo, path);
			XmlWriterSettings settings = new XmlWriterSettings
			{
				Indent = true,
				CloseOutput = true
			};

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
				XmlSerializer x = new XmlSerializer(typeof(DatabaseEntry));
				XmlSerializerNamespaces nameSpace = new XmlSerializerNamespaces();
				nameSpace.Add("", "");
				foreach (DatabaseEntry g in Games.Values)
				{
					x.Serialize(writer, g, nameSpace);
				}

				writer.WriteEndElement(); //XmlName_Games

				writer.WriteEndElement(); //XmlName_RootNode
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

			Logger.Info(GlobalStrings.GameDB_GameDBSaved);
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

		public void UpdateAppListFromWeb()
		{
			XmlDocument doc = FetchAppListFromWeb();
			IntegrateAppList(doc);
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

		private void ClearAggregates()
		{
			allStoreGenres = null;
			allStoreFlags = null;
			allStoreDevelopers = null;
			allStorePublishers = null;
		}

		/// <summary>
		///     Reads GameDbEntry objects from selected node and adds them to GameDb
		///     Legacy method used to read data from version 1 of the database.
		/// </summary>
		/// <param name="gameListNode">Node containing GameDbEntry objects with game as element name</param>
		private void LoadGamelistVersion1(XmlNode gameListNode)
		{
			const string XmlName_Game = "game", XmlName_Game_Id = "id", XmlName_Game_Name = "name", XmlName_Game_LastStoreUpdate = "lastStoreUpdate", XmlName_Game_LastAppInfoUpdate = "lastAppInfoUpdate", XmlName_Game_Type = "type", XmlName_Game_Platforms = "platforms", XmlName_Game_Parent = "parent", XmlName_Game_Genre = "genre", XmlName_Game_Tag = "tag", XmlName_Game_Achievements = "achievements", XmlName_Game_Developer = "developer", XmlName_Game_Publisher = "publisher", XmlName_Game_Flag = "flag", XmlName_Game_ReviewTotal = "reviewTotal", XmlName_Game_ReviewPositivePercent = "reviewPositiveP", XmlName_Game_MCUrl = "mcUrl", XmlName_Game_Date = "steamDate", XmlName_Game_HltbMain = "hltbMain", XmlName_Game_HltbExtras = "hltbExtras", XmlName_Game_HltbCompletionist = "hltbCompletionist", XmlName_Game_vrSupport = "vrSupport", XmlName_Game_vrSupport_Headsets = "Headset", XmlName_Game_vrSupport_Input = "Input", XmlName_Game_vrSupport_PlayArea = "PlayArea", XmlName_Game_languageSupport = "languageSupport", XmlName_Game_languageSupport_Interface = "Headset", XmlName_Game_languageSupport_FullAudio = "Input", XmlName_Game_languageSupport_Subtitles = "PlayArea";

			foreach (XmlNode gameNode in gameListNode.SelectNodes(XmlName_Game))
			{
				if (!XmlUtil.TryGetIntFromNode(gameNode[XmlName_Game_Id], out int id) || Games.ContainsKey(id))
				{
					continue;
				}

				DatabaseEntry g = new DatabaseEntry
				{
					Id = id,

					Name = XmlUtil.GetStringFromNode(gameNode[XmlName_Game_Name], null),

					AppType = XmlUtil.GetEnumFromNode(gameNode[XmlName_Game_Type], AppType.Unknown),

					Platforms = XmlUtil.GetEnumFromNode(gameNode[XmlName_Game_Platforms], AppPlatforms.All),

					ParentId = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_Parent], -1),

					Genres = XmlUtil.GetStringsFromNodeList(gameNode.SelectNodes(XmlName_Game_Genre)),

					Tags = XmlUtil.GetStringsFromNodeList(gameNode.SelectNodes(XmlName_Game_Tag))
				};

				foreach (XmlNode vrNode in gameNode.SelectNodes(XmlName_Game_vrSupport))
				{
					g.VrSupport.Headsets = XmlUtil.GetStringsFromNodeList(vrNode.SelectNodes(XmlName_Game_vrSupport_Headsets));
					g.VrSupport.Input = XmlUtil.GetStringsFromNodeList(vrNode.SelectNodes(XmlName_Game_vrSupport_Input));
					g.VrSupport.PlayArea = XmlUtil.GetStringsFromNodeList(vrNode.SelectNodes(XmlName_Game_vrSupport_PlayArea));
				}

				foreach (XmlNode langNode in gameNode.SelectNodes(XmlName_Game_languageSupport))
				{
					g.LanguageSupport.Interface = XmlUtil.GetStringsFromNodeList(langNode.SelectNodes(XmlName_Game_languageSupport_Interface));
					g.LanguageSupport.FullAudio = XmlUtil.GetStringsFromNodeList(langNode.SelectNodes(XmlName_Game_languageSupport_FullAudio));
					g.LanguageSupport.Subtitles = XmlUtil.GetStringsFromNodeList(langNode.SelectNodes(XmlName_Game_languageSupport_Subtitles));
				}

				g.Developers = XmlUtil.GetStringsFromNodeList(gameNode.SelectNodes(XmlName_Game_Developer));

				g.Publishers = XmlUtil.GetStringsFromNodeList(gameNode.SelectNodes(XmlName_Game_Publisher));

				g.SteamReleaseDate = XmlUtil.GetStringFromNode(gameNode[XmlName_Game_Date], null);

				g.Flags = XmlUtil.GetStringsFromNodeList(gameNode.SelectNodes(XmlName_Game_Flag));

				g.TotalAchievements = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_Achievements], 0);

				g.ReviewTotal = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_ReviewTotal], 0);
				g.ReviewPositivePercentage = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_ReviewPositivePercent], 0);

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