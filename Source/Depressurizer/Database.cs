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
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using DepressurizerCore;
using DepressurizerCore.Helpers;
using DepressurizerCore.Models;
using Newtonsoft.Json.Linq;

namespace Depressurizer
{
	public sealed class Database
	{
		#region Constants

		private const string XmlName_dbLanguage = "dbLanguage";

		private const string XmlName_Games = "games";

		private const string XmlName_LastHltbUpdate = "lastHltbUpdate";

		private const string XmlName_RootNode = "gamelist";

		#endregion

		#region Static Fields

		private static readonly object SyncRoot = new object();

		private static volatile Database _instance;

		#endregion

		#region Fields

		private StoreLanguage _language = StoreLanguage.Default;

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

		public ConcurrentDictionary<int, DatabaseEntry> Games { get; } = new ConcurrentDictionary<int, DatabaseEntry>();

		public StoreLanguage Language
		{
			get
			{
				lock (Games)
				{
					return _language;
				}
			}
			set
			{
				lock (Games)
				{
					_language = value;

					Parallel.ForEach(Games.Values, entry =>
					{
						if (entry.Id <= 0)
						{
							return;
						}

						entry.Tags = null;
						entry.Flags = null;
						entry.Genres = null;
						entry.SteamReleaseDate = null;
						entry.LastStoreScrape = 0;
						entry.VRSupport = new VRSupport();
						entry.LanguageSupport = new LanguageSupport();
					});
				}
			}
		}

		public long LastHLTBUpdate { get; set; } = 0;

		#endregion

		#region Public Methods and Operators

		public void AddOrUpdate(DatabaseEntry entry)
		{
			Games.AddOrUpdate(entry.Id, entry, (key, oldEntry) => oldEntry.MergeIn(entry));
		}

		public SortedSet<string> AllDevelopers()
		{
			SortedSet<string> developers = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

			lock (Games)
			{
				Parallel.ForEach(Games.Values, entry =>
				{
					if (entry.Developers != null)
					{
						developers.UnionWith(entry.Developers);
					}
				});
			}

			return developers;
		}

		public SortedSet<string> AllGenres()
		{
			SortedSet<string> genres = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

			lock (Games)
			{
				Parallel.ForEach(Games.Values, entry =>
				{
					if (entry.Genres != null)
					{
						genres.UnionWith(entry.Genres);
					}
				});
			}

			return genres;
		}

		public LanguageSupport AllLanguageSupport()
		{
			LanguageSupport languageSupport;

			lock (Games)
			{
				SortedSet<string> sortedFullAudio = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
				SortedSet<string> sortedInterfaces = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
				SortedSet<string> sortedSubtitles = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

				Parallel.ForEach(Games.Values, entry =>
				{
					if (entry.LanguageSupport.FullAudio != null)
					{
						sortedFullAudio.UnionWith(entry.LanguageSupport.FullAudio);
					}

					if (entry.LanguageSupport.Interface != null)
					{
						sortedInterfaces.UnionWith(entry.LanguageSupport.Interface);
					}

					if (entry.LanguageSupport.Subtitles != null)
					{
						sortedSubtitles.UnionWith(entry.LanguageSupport.Subtitles);
					}
				});

				languageSupport = new LanguageSupport
				{
					FullAudio = sortedFullAudio.ToList(),
					Interface = sortedInterfaces.ToList(),
					Subtitles = sortedSubtitles.ToList()
				};
			}

			return languageSupport;
		}

		public SortedSet<string> AllPublishers()
		{
			SortedSet<string> publishers = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

			lock (Games)
			{
				Parallel.ForEach(Games.Values, entry =>
				{
					if (entry.Publishers != null)
					{
						publishers.UnionWith(entry.Publishers);
					}
				});
			}

			return publishers;
		}

		public SortedSet<string> AllStoreFlags()
		{
			SortedSet<string> storeFlags = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

			lock (Games)
			{
				Parallel.ForEach(Games.Values, entry =>
				{
					if (entry.Flags != null)
					{
						storeFlags.UnionWith(entry.Flags);
					}
				});
			}

			return storeFlags;
		}

		public VRSupport AllVRSupport()
		{
			VRSupport vrSupport;

			lock (Games)
			{
				SortedSet<string> sortedHeadsets = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
				SortedSet<string> sortedInput = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
				SortedSet<string> sortedPlayArea = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

				Parallel.ForEach(Games.Values, entry =>
				{
					if (entry.VRSupport.Headsets != null)
					{
						sortedHeadsets.UnionWith(entry.VRSupport.Headsets);
					}

					if (entry.VRSupport.Input != null)
					{
						sortedInput.UnionWith(entry.VRSupport.Input);
					}

					if (entry.VRSupport.PlayArea != null)
					{
						sortedPlayArea.UnionWith(entry.VRSupport.PlayArea);
					}
				});

				vrSupport = new VRSupport
				{
					Headsets = sortedHeadsets.ToList(),
					Input = sortedInput.ToList(),
					PlayArea = sortedPlayArea.ToList()
				};
			}

			return vrSupport;
		}

		public IEnumerable<Tuple<string, int>> CalculateSortedDevList(GameList gameList, int minCount)
		{
			IEnumerable<Tuple<string, int>> unsortedList;

			lock (Games)
			{
				Dictionary<string, int> devCounts = new Dictionary<string, int>();

				if (gameList == null)
				{
					Parallel.ForEach(Games.Values, entry => { CalculateSortedDevListHelper(devCounts, entry); });
				}
				else
				{
					Parallel.ForEach(gameList.Games.Keys, appId =>
					{
						if (Contains(appId) && !gameList.Games[appId].Hidden)
						{
							CalculateSortedDevListHelper(devCounts, Games[appId]);
						}
					});
				}

				unsortedList = from entry in devCounts where entry.Value >= minCount select new Tuple<string, int>(entry.Key, entry.Value);
			}

			return unsortedList.ToList();
		}

		public IEnumerable<Tuple<string, int>> CalculateSortedPubList(GameList gameList, int minCount)
		{
			IEnumerable<Tuple<string, int>> unsortedList;

			lock (Games)
			{
				Dictionary<string, int> pubCounts = new Dictionary<string, int>();

				if (gameList == null)
				{
					Parallel.ForEach(Games.Values, entry => { CalculateSortedPubListHelper(pubCounts, entry); });
				}
				else
				{
					Parallel.ForEach(gameList.Games.Keys, appId =>
					{
						if (Contains(appId) && !gameList.Games[appId].Hidden)
						{
							CalculateSortedPubListHelper(pubCounts, Games[appId]);
						}
					});
				}

				unsortedList = from entry in pubCounts where entry.Value >= minCount select new Tuple<string, int>(entry.Key, entry.Value);
			}

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
			SortedSet<string> genreNames = AllGenres();
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

		public bool Contains(int appId)
		{
			return Games.ContainsKey(appId);
		}

		public XmlDocument FetchAppListFromWeb()
		{
			XmlDocument document = new XmlDocument();

			lock (Games)
			{
				Logger.Instance.Info("Downloading Steam app list");

				Stream responseStream = null;
				try
				{
					WebRequest req = WebRequest.Create(@"http://api.steampowered.com/ISteamApps/GetAppList/v0002/?format=xml");
					using (WebResponse resp = req.GetResponse())
					{
						responseStream = resp.GetResponseStream();
						if (responseStream == null)
						{
							return document;
						}

						document.Load(responseStream);
					}
				}
				catch (Exception e)
				{
					SentryLogger.Log(e);
					throw;
				}
				finally
				{
					if (responseStream != null)
					{
						responseStream.Dispose();
					}
				}

				Logger.Instance.Info("XML App list downloaded");
			}

			return document;
		}

		public List<string> GetDevelopers(int appId)
		{
			return GetDevelopers(appId, 3);
		}

		public List<string> GetDevelopers(int appId, int depth)
		{
			if (!Contains(appId))
			{
				return null;
			}

			List<string> developers = Games[appId].Developers;
			if (((developers == null) || (developers.Count == 0)) && (depth > 0) && (Games[appId].ParentId > 0))
			{
				developers = GetDevelopers(Games[appId].ParentId, depth - 1);
			}

			return developers;
		}

		public List<string> GetFlagList(int appId)
		{
			return GetFlagList(appId, 3);
		}

		public List<string> GetFlagList(int appId, int depth)
		{
			if (!Contains(appId))
			{
				return null;
			}

			List<string> flags = Games[appId].Flags;
			if (((flags == null) || (flags.Count == 0)) && (depth > 0) && (Games[appId].ParentId > 0))
			{
				flags = GetFlagList(Games[appId].ParentId, depth - 1);
			}

			return flags;
		}

		public List<string> GetGenreList(int appId, int depth = 3, bool tagFallback = true)
		{
			if (!Contains(appId))
			{
				return null;
			}

			List<string> genres = Games[appId].Genres;
			if (tagFallback && ((genres == null) || (genres.Count == 0)))
			{
				List<string> tags = GetTagList(appId, 0);
				if ((tags != null) && (tags.Count > 0))
				{
					genres = new List<string>(tags.Intersect(AllGenres()));
				}
			}

			if (((genres == null) || (genres.Count == 0)) && (depth > 0) && (Games[appId].ParentId > 0))
			{
				genres = GetGenreList(Games[appId].ParentId, depth - 1, tagFallback);
			}

			return genres;
		}

		public string GetName(int appId)
		{
			return !Contains(appId) ? null : Games[appId].Name;
		}

		public List<string> GetPublishers(int appId)
		{
			return GetPublishers(appId, 3);
		}

		public List<string> GetPublishers(int appId, int depth)
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

		public List<string> GetTagList(int appId)
		{
			return GetTagList(appId, 3);
		}

		public List<string> GetTagList(int appId, int depth)
		{
			if (!Contains(appId))
			{
				return null;
			}

			List<string> tags = Games[appId].Tags;
			if (((tags == null) || (tags.Count == 0)) && (depth > 0) && (Games[appId].ParentId > 0))
			{
				tags = GetTagList(Games[appId].ParentId, depth - 1);
			}

			return tags;
		}

		public VRSupport GetVRSupport(int appId)
		{
			return GetVRSupport(appId, 3);
		}

		public VRSupport GetVRSupport(int appId, int depth)
		{
			if (!Contains(appId))
			{
				return new VRSupport();
			}

			VRSupport vrSupport = Games[appId].VRSupport;
			if (((vrSupport.Headsets == null) || (vrSupport.Headsets.Count == 0)) && ((vrSupport.Input == null) || (vrSupport.Input.Count == 0)) && ((vrSupport.PlayArea == null) || (vrSupport.PlayArea.Count == 0)) && (depth > 0) && (Games[appId].ParentId > 0))
			{
				vrSupport = GetVRSupport(Games[appId].ParentId, depth - 1);
			}

			return vrSupport;
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

		public int IntegrateAppList(XmlDocument document)
		{
			int added = 0;

			lock (Games)
			{
				XmlNodeList appNodes = document.SelectNodes("/applist/apps/app");
				if (appNodes == null)
				{
					return added;
				}

				foreach (XmlNode node in appNodes)
				{
					if (!XmlUtil.TryGetIntFromNode(node["appid"], out int appId))
					{
						continue;
					}

					string appName = XmlUtil.GetStringFromNode(node["name"], null);

					if (Contains(appId))
					{
						DatabaseEntry entry = Games[appId];
						if (!string.IsNullOrWhiteSpace(entry.Name) && (entry.Name == appName))
						{
							continue;
						}

						entry.Name = appName;
						entry.AppType = AppType.Unknown;
					}
					else
					{
						DatabaseEntry entry = new DatabaseEntry(appId, appName);
						AddOrUpdate(entry);
						added++;
					}
				}

				Logger.Instance.Info("Loaded {0} new items from the app list.", added);
			}

			return added;
		}

		public void Load()
		{
			Load("database.xml");
		}

		public DatabaseEntry Remove(int appId)
		{
			Games.TryRemove(appId, out DatabaseEntry removedEntry);
			return removedEntry;
		}

		public bool Save()
		{
			return Save("database.xml");
		}

		public bool SupportsVR(int appId)
		{
			return SupportsVR(appId, 3);
		}

		public bool SupportsVR(int appId, int depth)
		{
			if (!Contains(appId))
			{
				return false;
			}

			VRSupport vrSupport = Games[appId].VRSupport;
			if (vrSupport == null)
			{
				return false;
			}

			if ((vrSupport.Headsets != null) && (vrSupport.Headsets.Count > 0))
			{
				return true;
			}

			if ((vrSupport.Input != null) && (vrSupport.Input.Count > 0))
			{
				return true;
			}

			if ((vrSupport.PlayArea != null) && (vrSupport.PlayArea.Count > 0))
			{
				return true;
			}

			DatabaseEntry entry = Games[appId];
			if ((depth > 0) && (entry.ParentId > 0))
			{
				return SupportsVR(entry.ParentId, depth - 1);
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
			int updated = 0;

			lock (Games)
			{
				Dictionary<int, AppInfo> appInfos = AppInfo.LoadApps(path);
				long currentUnixTime = Utility.CurrentUnixTime();

				Parallel.ForEach(appInfos.Values, appInfo =>
				{
					try
					{
						DatabaseEntry entry;
						if (!Contains(appInfo.AppId))
						{
							entry = new DatabaseEntry(appInfo.AppId);
							AddOrUpdate(entry);
						}
						else
						{
							entry = Games[appInfo.AppId];
						}

						entry.LastAppInfoUpdate = currentUnixTime;
						if (appInfo.AppType != AppType.Unknown)
						{
							entry.AppType = appInfo.AppType;
						}

						if (!string.IsNullOrWhiteSpace(appInfo.Name))
						{
							entry.Name = appInfo.Name;
						}

						if ((entry.Platforms == AppPlatforms.None) || ((entry.LastStoreScrape == 0) && (appInfo.Platforms > AppPlatforms.None)))
						{
							entry.Platforms = appInfo.Platforms;
						}

						if (appInfo.ParentId > 0)
						{
							entry.ParentId = appInfo.ParentId;
						}
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
						throw;
					}

					updated++;
				});
			}

			return updated;
		}

		public int UpdateFromHLTB(bool includeImputedTimes)
		{
			int updated = 0;

			lock (Games)
			{
				using (WebClient webClient = new WebClient())
				{
					webClient.Headers.Set("User-Agent", "Depressurizer");
					webClient.Encoding = Encoding.UTF8;

					string json = webClient.DownloadString(@"https://www.howlongtobeatsteam.com/api/games/library/cached/all");
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

				LastHLTBUpdate = Utility.CurrentUnixTime();
			}

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

			Parallel.ForEach(entry.Developers, dev =>
			{
				if (counts.ContainsKey(dev))
				{
					counts[dev] += 1;
				}
				else
				{
					counts[dev] = 1;
				}
			});
		}

		private static void CalculateSortedPubListHelper(IDictionary<string, int> counts, DatabaseEntry entry)
		{
			if (entry.Publishers == null)
			{
				return;
			}

			Parallel.ForEach(entry.Publishers, publisher =>
			{
				if (counts.ContainsKey(publisher))
				{
					counts[publisher] += 1;
				}
				else
				{
					counts[publisher] = 1;
				}
			});
		}

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
						score = ((1 - interp) * weightFactor) + interp;
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

		private void Load(string path)
		{
			lock (Games)
			{
				if (!File.Exists(path))
				{
					return;
				}

				Logger.Instance.Info("Database: Loading a database instance from '{0}'", path);

				XmlDocument document = new XmlDocument();

				Stream stream = null;
				try
				{
					stream = new FileStream(path, FileMode.Open);
					stream = new GZipStream(stream, CompressionMode.Decompress);
					document.Load(stream);
				}
				catch (Exception e)
				{
					Logger.Instance.Error("Database: Error while reading database file from '{0}'", path);
					SentryLogger.Log(e);
					throw;
				}
				finally
				{
					if (stream != null)
					{
						stream.Dispose();
					}
				}

				try
				{
					Games.Clear();

					XmlNode gameListNode = document.SelectSingleNode("/" + XmlName_RootNode);
					if (gameListNode == null)
					{
						throw new InvalidDataException();
					}

					Language = (StoreLanguage) Enum.Parse(typeof(StoreLanguage), XmlUtil.GetStringFromNode(gameListNode[XmlName_dbLanguage], "english"), true);
					LastHLTBUpdate = XmlUtil.GetIntFromNode(gameListNode[XmlName_LastHltbUpdate], 0);

					XmlNode dictonaryNode = gameListNode.SelectSingleNode(XmlName_Games);
					if (dictonaryNode == null)
					{
						throw new InvalidDataException();
					}

					XmlSerializer xmlSerializer = new XmlSerializer(typeof(DatabaseEntry));
					foreach (XmlNode appNode in dictonaryNode.ChildNodes)
					{
						using (XmlReader reader = new XmlNodeReader(appNode))
						{
							DatabaseEntry entry = (DatabaseEntry) xmlSerializer.Deserialize(reader);
							AddOrUpdate(entry);
						}
					}
				}
				catch (Exception e)
				{
					Logger.Instance.Error("Database: Error while parsing database file from '{0}'", path);
					SentryLogger.Log(e);
					throw;
				}

				Logger.Instance.Info("Database: Loaded current instance from '{0}'", path);
			}
		}

		private bool Save(string path)
		{
			lock (Games)
			{
				Logger.Instance.Info("Database: Saving current instance to '{0}'", path);

				XmlWriter writer = null;
				Stream stream = null;
				try
				{
					stream = new FileStream(path, FileMode.Create);
					stream = new GZipStream(stream, CompressionMode.Compress);

					XmlWriterSettings settings = new XmlWriterSettings
					{
						Indent = true,
						CloseOutput = true
					};

					writer = XmlWriter.Create(stream, settings);

					writer.WriteStartDocument();
					writer.WriteStartElement(XmlName_RootNode);

					writer.WriteElementString(XmlName_LastHltbUpdate, LastHLTBUpdate.ToString(CultureInfo.InvariantCulture));
					writer.WriteElementString(XmlName_dbLanguage, Enum.GetName(typeof(StoreLanguage), Language));

					writer.WriteStartElement(XmlName_Games);
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(DatabaseEntry));
					XmlSerializerNamespaces nameSpace = new XmlSerializerNamespaces();
					nameSpace.Add("", "");
					foreach (DatabaseEntry entry in Games.Values)
					{
						xmlSerializer.Serialize(writer, entry, nameSpace);
					}

					writer.WriteEndElement();

					writer.WriteEndElement();
					writer.WriteEndDocument();
				}
				catch (Exception e)
				{
					Logger.Instance.Error("Database: Error while trying to save current instance to '{0}'", path);
					SentryLogger.Log(e);
					throw;
				}
				finally
				{
					if (writer != null)
					{
						writer.Dispose();
					}

					if (stream != null)
					{
						stream.Dispose();
					}
				}

				Logger.Instance.Info("Database: Saved current instance to '{0}'", path);
			}

			return true;
		}

		#endregion
	}
}
