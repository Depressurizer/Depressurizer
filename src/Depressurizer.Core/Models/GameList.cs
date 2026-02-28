using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.XPath;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Interfaces;
using static Depressurizer.Core.Models.SteamLevelDB;
using ValueType = Depressurizer.Core.Enums.ValueType;

namespace Depressurizer.Core.Models
{
    /// <summary>
    ///     Represents a complete collection of games and categories.
    /// </summary>
    public class GameList : IGameList
    {
        #region Constructors and Destructors

        public GameList()
        {
            Games = new Dictionary<int, GameInfo>();
            Categories = new List<Category>();
            Filters = new List<Filter>();
            FavoriteCategory = new Category(FavoriteNewConfigValue);
            Categories.Add(FavoriteCategory);
        }

        #endregion

        #region Public Properties

        public List<Category> Categories { get; set; }

        public Category FavoriteCategory { get; }

        public string FavoriteConfigValue => "favorite";

        public string FavoriteNewConfigValue => "<Favorite>";

        public List<Filter> Filters { get; set; }

        public Dictionary<int, GameInfo> Games { get; set; }

        #endregion

        #region Properties

        private static IDatabase Database => SingletonKeeper.Database;

        private static Logger Logger => Logger.Instance;

        private static Settings Settings => Settings.Instance;

        #endregion

        #region Public Methods and Operators

        public static IXPathNavigable FetchGameList(long steamId)
        {
            return FetchGameListFromUri(new Uri(string.Format(CultureInfo.InvariantCulture, Constants.GameList, steamId)));
        }

        public static IXPathNavigable FetchGameListFromUri(Uri uri)
        {
            Logger.Info("FetchGameListFromUri | Downloading game list from URI: {0}.", uri.ToString());

            XmlDocument doc = new XmlDocument();
            try
            {
                WebRequest req = WebRequest.Create(uri);
                WebResponse response = req.GetResponse();
                if (response.ResponseUri.Segments.Length < 4)
                {
                    throw new SteamProfileAccessException("The specified profile is not public.");
                }

                doc.Load(response.GetResponseStream());
                response.Close();
                if (doc.InnerText.Contains("This profile is private."))
                {
                    throw new SteamProfileAccessException("The specified profile is not public.");
                }

                Logger.Info("FetchGameListFromUri | Successfully downloaded XML game list.", uri);
                return doc;
            }
            catch (SteamProfileAccessException)
            {
                Logger.Warn("FetchGameListFromUri | Found a private profile...");
                throw;
            }
            catch (Exception e)
            {
                Logger.Warn("FetchGameListFromUri | Exception thrown while downloading game list from URI, error: {0}.", e.Message);
                throw new ApplicationException(e.Message, e);
            }
        }

        /// <summary>
        ///     Adds a new category to the list.
        /// </summary>
        /// <param name="name">Name of the category to add</param>
        /// <param name="forRename">If true, allow case to be updated</param>
        /// <returns>The added category. Returns null if the category already exists.</returns>
        public Category AddCategory(string name, bool forRename = false)
        {
            StringComparison stringComparison = forRename ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            if (string.IsNullOrEmpty(name) || CategoryExists(name, stringComparison))
            {
                return null;
            }

            Category newCat = new Category(name);
            Categories.Add(newCat);
            return newCat;
        }

        /// <summary>
        ///     Adds a new Filter to the list.
        /// </summary>
        /// <param name="name">Name of the Filter to add</param>
        /// <returns>The added Filter. Returns null if the Filter already exists.</returns>
        public Filter AddFilter(string name)
        {
            if (string.IsNullOrEmpty(name) || FilterExists(name))
            {
                return null;
            }

            Filter newFilter = new Filter(name);
            Filters.Add(newFilter);
            return newFilter;
        }

        /// <summary>
        ///     Adds a single category to a single game
        /// </summary>
        /// <param name="appId">Game ID to add category to</param>
        /// <param name="category">Category to add</param>
        public void AddGameCategory(int appId, Category category)
        {
            Games[appId].AddCategory(category);
        }

        /// <summary>
        ///     Adds a single category to each member of a list of games
        /// </summary>
        /// <param name="appIds">List of game IDs to add to</param>
        /// <param name="category">Category to add</param>
        public void AddGameCategory(int[] appIds, Category category)
        {
            if (appIds == null || category == null)
            {
                return;
            }

            foreach (int appId in appIds)
            {
                AddGameCategory(appId, category);
            }
        }

        /// <summary>
        ///     Checks to see if a category with the given name exists
        /// </summary>
        /// <param name="name">Name of the category to look for</param>
        /// <param name="stringComparison">Specifies the case comparison to be used</param>
        /// <returns>True if the name is found, false otherwise</returns>
        public bool CategoryExists(string name, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            // Favorite category always exists
            if (name == FavoriteNewConfigValue || name == FavoriteConfigValue)
            {
                return true;
            }

            foreach (Category c in Categories)
            {
                if (string.Equals(c.Name, name, stringComparison))
                {
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            Games.Clear();
            Categories.Clear();
        }

        /// <summary>
        ///     Clears all categories from a single game
        /// </summary>
        /// <param name="appId">Game ID to clear categories from</param>
        /// <param name="preserveFavorite">If true, preserves the favorite category.</param>
        public void ClearGameCategories(int appId, bool preserveFavorite)
        {
            Games[appId].ClearCategories(!preserveFavorite);
        }

        /// <summary>
        ///     Clears all categories from a set of games
        /// </summary>
        /// <param name="appIds">List of game IDs to clear categories from</param>
        /// <param name="preserveFavorite">If true, preserves the favorite category.</param>
        public void ClearGameCategories(int[] appIds, bool preserveFavorite)
        {
            if (appIds == null)
            {
                return;
            }

            foreach (int appId in appIds)
            {
                ClearGameCategories(appId, preserveFavorite);
            }
        }

        /// <summary>
        ///     Writes Steam game category information to Steam user config file.
        /// </summary>
        /// <param name="steamId">Steam ID of user to save the config file for</param>
        /// <param name="discardMissing">
        ///     If true, any pre-existing game entries in the file that do not have corresponding entries
        ///     in the GameList are removed
        /// </param>
        /// <param name="includeShortcuts">If true, also saves the Steam shortcut category data</param>
        public void ExportSteamConfig(long steamId, bool discardMissing, bool includeShortcuts)
        {
            string filePath = string.Format(CultureInfo.InvariantCulture, Constants.SharedConfig, Settings.Instance.SteamPath, Steam.ToSteam3Id(steamId));
            ExportSteamConfigFile(filePath, discardMissing);
            if (includeShortcuts)
            {
                ExportSteamShortcuts(steamId);
            }
            Process.Start("steam://resetcollections");
        }

        /// <summary>
        ///     Writes Steam game category information to Steam user config file.
        /// </summary>
        /// <param name="filePath">Full path of the steam config file to save</param>
        /// <param name="discardMissing">
        ///     If true, any pre-existing game entries in the file that do not have corresponding entries
        ///     in the GameList are removed
        /// </param>
        public void ExportSteamConfigFile(string filePath, bool discardMissing)
        {
            Logger.Info("Saving Steam config file: {0}.", filePath);

            VDFNode fileData = new VDFNode();
            try
            {
                using (StreamReader reader = new StreamReader(filePath, false))
                {
                    fileData = VDFNode.LoadFromText(reader, true);
                }
            }
            catch (Exception e)
            {
                Logger.Warn("Loading existing Steam config failed: {0}", e.Message);
            }

            VDFNode appListNode = fileData.GetNodeAt(new[]
            {
                "Software",
                "Valve",
                "Steam",
                "apps"
            }, true);

            // Run through all Delete category data for any games not found in the GameList
            if (discardMissing)
            {
                Dictionary<string, VDFNode> gameNodeArray = appListNode.NodeArray;
                if (gameNodeArray != null)
                {
                    foreach (KeyValuePair<string, VDFNode> pair in gameNodeArray)
                    {
                        if (int.TryParse(pair.Key, out int gameId) && Games.ContainsKey(gameId))
                        {
                            continue;
                        }

                        Logger.Verbose("Removing game {0} category info from Steam config file.", gameId);
                        pair.Value.RemoveSubNode("tags");
                    }
                }
            }

            // Force appListNode to be an array, we can't do anything if it's a value
            appListNode.MakeArray();

            foreach (GameInfo game in Games.Values)
            {
                if (game.Id <= 0)
                {
                    continue;
                }

                // External games have negative identifier
                Logger.Verbose("Adding game {0} to config file.", game.Id);
                VDFNode gameNode = appListNode[game.Id.ToString(CultureInfo.InvariantCulture)];
                gameNode.MakeArray();

                VDFNode tagsNode = gameNode["tags"];
                tagsNode.MakeArray();

                Dictionary<string, VDFNode> tags = tagsNode.NodeArray;
                if (tags != null)
                {
                    tags.Clear();
                }

                int key = 0;
                foreach (Category c in game.Categories)
                {
                    string name = c.Name;
                    if (name == FavoriteNewConfigValue)
                    {
                        name = FavoriteConfigValue;
                    }

                    tagsNode[key.ToString(CultureInfo.InvariantCulture)] = new VDFNode(name);
                    key++;
                }

                if (game.IsHidden)
                {
                    gameNode["hidden"] = new VDFNode("1");
                }
                else
                {
                    gameNode.RemoveSubNode("hidden");
                }
            }

            Logger.Verbose("Cleaning up steam config tree before writing to disk.");
            appListNode.CleanTree();

            Logger.Info("Writing to disk...");
            VDFNode fullFile = new VDFNode();
            fullFile["UserLocalConfigStore"] = fileData;
            try
            {
                Locations.File.Backup(filePath, Settings.Instance.ConfigBackupCount);
            }
            catch (Exception e)
            {
                Logger.Error("Steam config file backup failed: {0}", e.Message);
            }

            try
            {
                string filePathTmp = filePath + ".tmp";
                FileInfo f = new FileInfo(filePathTmp);
                f.Directory.Create();
                FileStream fStream = f.Open(FileMode.Create, FileAccess.Write, FileShare.None);
                using (StreamWriter writer = new StreamWriter(fStream))
                {
                    fullFile.SaveAsText(writer);
                }

                fStream.Close();
                File.Delete(filePath);
                File.Move(filePathTmp, filePath);
            }
            catch (ArgumentException e)
            {
                Logger.Error("Error saving steam config file: {0}", e.ToString());
                throw new ApplicationException("Failed to save Steam config file: Invalid path specified.", e);
            }
            catch (IOException e)
            {
                Logger.Error("Error saving steam config file: {0}", e.ToString());
                throw new ApplicationException("Failed to save Steam config file:" + e.Message, e);
            }
            catch (UnauthorizedAccessException e)
            {
                Logger.Error("Error saving steam config file: {0}", e.ToString());
                throw new ApplicationException("Access denied on Steam config file:" + e.Message, e);
            }
        }

        public void ExportSteamShortcuts(long steamId)
        {
            string filePath = string.Format(CultureInfo.InvariantCulture, Constants.Shortcuts, Settings.Instance.SteamPath, Steam.ToSteam3Id(steamId));
            Logger.Info("GameList:ExportSteamShortcuts | Saving shortcuts.vdf to: {0}.", filePath);

            FileStream fStream = null;
            BinaryReader binReader = null;
            VDFNode dataRoot = null;

            try
            {
                fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                binReader = new BinaryReader(fStream);

                dataRoot = VDFNode.LoadFromBinary(binReader);
            }
            catch (FileNotFoundException e)
            {
                Logger.Error("Error opening Steam config file: {0}", e.ToString());
            }
            catch (IOException e)
            {
                Logger.Error("Loading existing Steam config failed: {0}", e.ToString());
            }
            finally
            {
                binReader?.Close();
                fStream?.Close();
            }

            if (dataRoot == null)
            {
                return;
            }

            List<GameInfo> gamesToSave = Games.Keys.Select(key => Games[key]).ToList();
            LoadShortcutLaunchIds(steamId, out StringDictionary launchIds);
            VDFNode shortcutsNode = dataRoot.GetNodeAt(new[]
            {
                "shortcuts"
            }, false);

            List<string> shortcutsToRemove = new List<string>();
            foreach (KeyValuePair<string, VDFNode> shortcutPair in shortcutsNode.NodeArray)
            {
                if (!int.TryParse(shortcutPair.Key, out int gameNodeId))
                {
                    continue;
                }

                VDFNode shortcutNode = shortcutPair.Value;
                int matchingIndex = FindMatchingShortcut(gameNodeId, shortcutNode, gamesToSave, launchIds);
                if (matchingIndex < 0)
                {
                    shortcutsToRemove.Add(shortcutPair.Key);
                    continue;
                }

                GameInfo gameInfo = gamesToSave[matchingIndex];
                gamesToSave.RemoveAt(matchingIndex);

                Logger.Verbose("Adding game {0} to config file.", gameInfo.Id);
                VDFNode tagsNode = shortcutNode.GetNodeAt(new[]
                {
                    "tags"
                }, true);
                tagsNode.NodeArray?.Clear();

                for (int i = 0; i < gameInfo.Categories.Count; i++)
                {
                    string categoryName = gameInfo.Categories.ElementAt(i).Name;
                    categoryName = categoryName == FavoriteNewConfigValue ? FavoriteConfigValue : categoryName;
                    tagsNode[i.ToString(CultureInfo.InvariantCulture)] = new VDFNode(categoryName);
                }

                shortcutNode["hidden"] = new VDFNode(gameInfo.IsHidden ? 1 : 0);
            }

            foreach (string key in shortcutsToRemove)
            {
                shortcutsNode.RemoveSubNode(key);
            }

            if (dataRoot.NodeType != ValueType.Array)
            {
                return;
            }

            Logger.Info("Saving Steam shortcuts config file: {0}.", filePath);

            try
            {
                Locations.File.Backup(filePath, Settings.Instance.ConfigBackupCount);
            }
            catch (Exception e)
            {
                Logger.Error("Steam shortcut file backup failed: {0}", e.Message);
            }

            try
            {
                string filePathTmp = filePath + ".tmp";
                fStream = new FileStream(filePathTmp, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryWriter binWriter = new BinaryWriter(fStream);
                dataRoot.SaveAsBinary(binWriter);
                binWriter.Close();
                fStream.Close();
                File.Delete(filePath);
                File.Move(filePathTmp, filePath);
            }
            catch (ArgumentException e)
            {
                Logger.Error("Error saving steam config file: {0}", e.ToString());
                throw new ApplicationException("Failed to save Steam config file: Invalid path specified.", e);
            }
            catch (IOException e)
            {
                Logger.Error("Error saving steam config file: {0}", e.ToString());
                throw new ApplicationException("Failed to save Steam config file:" + e.Message, e);
            }
            catch (UnauthorizedAccessException e)
            {
                Logger.Error("Error saving steam config file: {0}", e.ToString());
                throw new ApplicationException("Access denied on Steam config file:" + e.Message, e);
            }
        }

        /// <summary>
        ///     Checks to see if a Filter with the given name exists
        /// </summary>
        /// <param name="name">Name of the Filter to look for</param>
        /// <returns>True if the name is found, false otherwise</returns>
        public bool FilterExists(string name)
        {
            foreach (Filter filter in Filters)
            {
                if (string.Equals(filter.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Gets the category with the given name. If the category does not exist, creates it.
        /// </summary>
        /// <param name="name">Name to get the category for</param>
        /// <returns>A category with the given name. Null if any error is encountered.</returns>
        public Category GetCategory(string name)
        {
            // Categories must have a name
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            // Check for Favorite category
            if (name == FavoriteNewConfigValue || name == FavoriteConfigValue)
            {
                return FavoriteCategory;
            }

            // Look for a matching category in the list and return if found
            foreach (Category c in Categories)
            {
                if (string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return c;
                }
            }

            // Create a new category and return it
            return AddCategory(name);
        }

        /// <summary>
        ///     Gets the Filter with the given name. If the Filter does not exist, creates it.
        /// </summary>
        /// <param name="name">Name to get the Filter for</param>
        /// <returns>A Filter with the given name. Null if any error is encountered.</returns>
        public Filter GetFilter(string name)
        {
            // Filters must have a name
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            // Look for a matching Filter in the list and return if found
            foreach (Filter f in Filters)
            {
                if (string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return f;
                }
            }

            // Create a new Filter and return it
            Filter newFilter = new Filter(name);
            Filters.Add(newFilter);
            return newFilter;
        }

        /// <summary>
        ///     Add or Remove the hidden attribute of a single game
        /// </summary>
        /// <param name="appId">Game ID to hide/unhide</param>
        /// <param name="isHidden">Whether the game should be hidden.</param>
        public void HideGames(int appId, bool isHidden)
        {
            Games[appId].SetHidden(isHidden);
        }

        /// <summary>
        ///     Add or Remove the hidden attribute from a set of games
        /// </summary>
        /// <param name="appIds">List of game IDs to hide/unhide</param>
        /// <param name="isHidden">Whether the games should be hidden.</param>
        public void HideGames(int[] appIds, bool isHidden)
        {
            if (appIds == null)
            {
                return;
            }

            foreach (int appId in appIds)
            {
                HideGames(appId, isHidden);
            }
        }

        /// <summary>
        ///     Loads category info from the steam config file for the given Steam user.
        /// </summary>
        /// <param name="steamId">Identifier of Steam user</param>
        /// <param name="ignore">Set of games to ignore</param>
        /// <param name="includeShortcuts">If true, also import shortcut data</param>
        /// <returns>The number of game entries found</returns>
        public int ImportSteamConfig(long steamId, SortedSet<int> ignore, bool includeShortcuts)
        {
            int result = 0;
            if (Settings.ReadFromLevelDB)
            {
                ImportSteamLevelDB(steamId);
            }
            else
            {
                string filePath = string.Format(CultureInfo.InvariantCulture, Constants.SharedConfig, Settings.Instance.SteamPath, Steam.ToSteam3Id(steamId));
                result = ImportSteamConfigFile(filePath, ignore);
            }

            if (includeShortcuts)
            {
                result += ImportSteamShortcuts(steamId);
            }

            return result;
        }

        private void ImportSteamLevelDB(long steamId) {
            Logger.Info("Importing from Steam LevelDB: {0}", steamId);

            SteamLevelDB levelDB = new SteamLevelDB(Steam.ToSteam3Id(steamId));

            List<CloudStorageNamespace.Element.SteamCollectionValue> collections = levelDB.getSteamCollections();
            Logger.Info("Found {0} Steam Collections", collections.Count);
            foreach (var game in Games.Values)
            {
                SetGameCategories(game.Id, new List<Category>(), false);
            }

            foreach(var collection in collections)
            {
                foreach (var appId in collection.added)
                {
                    Games[appId].AddCategory(GetCategory(collection.name));
                }
            }
        }

        /// <summary>
        ///     Loads category info from the given steam config file.
        /// </summary>
        /// <param name="filePath">The path of the file to open</param>
        /// <param name="ignore">Set of game IDs to ignore</param>
        /// <returns>The number of game entries found</returns>
        public int ImportSteamConfigFile(string filePath, SortedSet<int> ignore)
        {
            Logger.Info("Opening Steam config file: {0}", filePath);
            VDFNode dataRoot;

            try
            {
                using (StreamReader reader = new StreamReader(filePath, false))
                {
                    dataRoot = VDFNode.LoadFromText(reader, true);
                }
            }
            catch (InvalidDataException e)
            {
                Logger.Error("Error parsing Steam config file: {0}", e.Message);
                throw new ApplicationException("Error parsing Steam config file:" + e.Message, e);
            }
            catch (IOException e)
            {
                Logger.Error("Error opening Steam config file: {0}", e.Message);
                throw new ApplicationException("Error opening Steam config file:" + e.Message, e);
            }

            VDFNode appsNode = dataRoot.GetNodeAt(new[]
            {
                "Software",
                "Valve",
                "Steam",
                "apps"
            }, true);
            int count = IntegrateFromNode(appsNode, ignore);
            Logger.Info("Steam config file loaded. {0} items found.", count);
            return count;
        }

        public int ImportSteamShortcuts(long steamId)
        {
            if (steamId <= 0)
            {
                return 0;
            }

            int loadedGames = 0;

            string filePath = string.Format(CultureInfo.InvariantCulture, Constants.Shortcuts, Settings.Instance.SteamPath, Steam.ToSteam3Id(steamId));
            FileStream fStream = null;
            BinaryReader binReader = null;

            try
            {
                fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                binReader = new BinaryReader(fStream);

                VDFNode dataRoot = VDFNode.LoadFromBinary(binReader);

                VDFNode shortcutsNode = dataRoot.GetNodeAt(new[]
                {
                    "shortcuts"
                }, false);

                if (shortcutsNode != null)
                {
                    // Remove existing shortcuts
                    List<int> oldShortcutIds = new List<int>();
                    foreach (int id in Games.Keys)
                    {
                        if (id < 0)
                        {
                            oldShortcutIds.Add(id);
                        }
                    }

                    foreach (int g in oldShortcutIds)
                    {
                        Games.Remove(g);
                    }

                    RemoveEmptyCategories();

                    // Load launch IDs
                    LoadShortcutLaunchIds(steamId, out StringDictionary launchIds);

                    // Load shortcuts
                    foreach (KeyValuePair<string, VDFNode> shortcutPair in shortcutsNode.NodeArray)
                    {
                        VDFNode nodeGame = shortcutPair.Value;

                        if (!int.TryParse(shortcutPair.Key, out int appId))
                        {
                            continue;
                        }

                        if (IntegrateShortcut(appId, nodeGame, launchIds))
                        {
                            loadedGames++;
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Logger.Error("Error opening Steam config file: {0}", e.ToString());
            }
            catch (IOException e)
            {
                Logger.Error("Loading existing Steam config failed: {0}", e.ToString());
            }
            catch (InvalidDataException e)
            {
                Logger.Error(e.ToString());
            }
            finally
            {
                if (binReader != null)
                {
                    binReader.Close();
                }

                if (fStream != null)
                {
                    fStream.Close();
                }
            }

            Logger.Info("Integrated external games into game list. {0} total items.", loadedGames);

            return loadedGames;
        }

        public int IntegrateGameList(GetOwnedGamesObject doc, bool overwrite, SortedSet<int> ignore, out int newItems)
        {
            newItems = 0;
            if (doc == null)
            {
                return 0;
            }

            int loadedGames = 0;
            foreach (GetOwnedGamesObject.Game game in doc.response.games)
            {
                GameInfo integratedGame = IntegrateGame(game.appid, game.name, overwrite, ignore, GameListingSource.WebProfile, out bool isNew);
                if (integratedGame == null)
                {
                    continue;
                }

                // playtime_forever The total number of minutes played "on record", since Steam began tracking total playtime in early 2009.
                integratedGame.HoursPlayed = Math.Round(game.playtime_forever / 60D, 1);

                loadedGames++;
                if (isNew)
                {
                    newItems++;
                }
            }

            Logger.Info("Integrated Web API data into game list. {0} total items, {1} new.", loadedGames, newItems);
            return loadedGames;
        }

        public int IntegrateGameList(IXPathNavigable doc, bool overwrite, SortedSet<int> ignore, out int newItems)
        {
            newItems = 0;
            if (doc == null || !(doc is XmlNode node))
            {
                return 0;
            }

            int loadedGames = 0;
            XmlNodeList gameNodes = node.SelectNodes("/gamesList/games/game");
            if (gameNodes == null)
            {
                Logger.Warn("GameList: Failed integrating XML data, gameNodes is null.");
                return 0;
            }

            foreach (XmlNode gameNode in gameNodes)
            {
                XmlNode appIdNode = gameNode["appID"];
                if (appIdNode == null || !int.TryParse(appIdNode.InnerText, out int appId))
                {
                    continue;
                }

                XmlNode nameNode = gameNode["name"];
                if (nameNode == null)
                {
                    continue;
                }

                GameInfo integratedGame = IntegrateGame(appId, nameNode.InnerText, overwrite, ignore, GameListingSource.WebProfile, out bool isNew);
                if (integratedGame == null)
                {
                    continue;
                }

                XmlNode hoursNode = gameNode["hoursOnRecord"];
                if (hoursNode != null)
                {
                    if (!string.IsNullOrWhiteSpace(hoursNode.InnerText))
                    {
                        if (double.TryParse(hoursNode.InnerText, NumberStyles.Any, CultureInfo.InvariantCulture, out double hoursPlayed))
                        {
                            integratedGame.HoursPlayed = hoursPlayed;
                        }
                    }
                }

                loadedGames++;
                if (isNew)
                {
                    newItems++;
                }
            }

            Logger.Info("Integrated XML data into game list. {0} total items, {1} new.", loadedGames, newItems);
            return loadedGames;
        }

        /// <summary>
        ///     Removes the given category.
        /// </summary>
        /// <param name="category">Category to remove.</param>
        /// <returns>True if removal was successful, false if it was not in the list anyway</returns>
        public bool RemoveCategory(Category category)
        {
            // Can't remove favorite category
            if (category == FavoriteCategory)
            {
                return false;
            }

            if (!Categories.Remove(category))
            {
                return false;
            }

            foreach (GameInfo gameInfo in Games.Values)
            {
                gameInfo.RemoveCategory(category);
            }

            return true;
        }

        /// <summary>
        ///     Remove all empty categories from the category list.
        /// </summary>
        /// <returns>Number of categories removed</returns>
        public int RemoveEmptyCategories()
        {
            Dictionary<Category, int> counts = new Dictionary<Category, int>();
            foreach (Category c in Categories)
            {
                if (c != FavoriteCategory)
                {
                    counts.Add(c, 0);
                }
            }

            foreach (GameInfo g in Games.Values)
            foreach (Category c in g.Categories)
            {
                if (counts.ContainsKey(c))
                {
                    counts[c]++;
                }
            }

            int removed = 0;
            foreach (KeyValuePair<Category, int> pair in counts)
            {
                if (pair.Value != 0)
                {
                    continue;
                }

                if (Categories.Remove(pair.Key))
                {
                    removed++;
                }
            }

            return removed;
        }

        /// <summary>
        ///     Removes a single category from a single game.
        /// </summary>
        /// <param name="appId">Game ID to remove from</param>
        /// <param name="category">Category to remove</param>
        public void RemoveGameCategory(int appId, Category category)
        {
            Games[appId].RemoveCategory(category);
        }

        /// <summary>
        ///     Removes a single category from each member of a list of games
        /// </summary>
        /// <param name="appIds">List of game IDs to remove from</param>
        /// <param name="category">Category to remove</param>
        public void RemoveGameCategory(int[] appIds, Category category)
        {
            foreach (int appId in appIds)
            {
                RemoveGameCategory(appId, category);
            }
        }

        /// <summary>
        ///     Renames the given category.
        /// </summary>
        /// <param name="category">Category to rename.</param>
        /// <param name="newName">Name to assign to the new category.</param>
        /// <returns>The new category, if the operation succeeds. Null otherwise.</returns>
        public Category RenameCategory(Category category, string newName)
        {
            if (category == FavoriteCategory)
            {
                return null;
            }

            Category newCategory = AddCategory(newName, forRename: true);
            if (newCategory == null)
            {
                return null;
            }

            Categories.Sort();
            foreach (GameInfo game in Games.Values)
            {
                if (!game.ContainsCategory(category))
                {
                    continue;
                }

                game.RemoveCategory(category);
                game.AddCategory(newCategory);
            }

            RemoveCategory(category);
            return newCategory;
        }

        public void SetGameCategories(int[] appIds, Category cat, bool preserveFavorites)
        {
            SetGameCategories(appIds, new List<Category>
            {
                cat
            }, preserveFavorites);
        }

        /// <summary>
        ///     Sets a game's categories to a particular set
        /// </summary>
        /// <param name="appId">Game ID to modify</param>
        /// <param name="catSet">Set of categories to apply</param>
        /// <param name="preserveFavorites">If true, will not remove "favorite" category</param>
        public void SetGameCategories(int appId, ICollection<Category> catSet, bool preserveFavorites)
        {
            Games[appId].SetCategories(catSet, preserveFavorites);
        }

        /// <summary>
        ///     Sets multiple games' categories to a particular set
        /// </summary>
        /// <param name="appIds">Game IDs to modify</param>
        /// <param name="catSet">Set of categories to apply</param>
        /// <param name="preserveFavorites">If true, will not remove "favorite" category</param>
        public void SetGameCategories(int[] appIds, ICollection<Category> catSet, bool preserveFavorites)
        {
            foreach (int appId in appIds)
            {
                SetGameCategories(appId, catSet, preserveFavorites);
            }
        }

        public int UpdateGameListFromOwnedPackageInfo(long accountId, SortedSet<int> ignored, out int newApps)
        {
            newApps = 0;
            int totalApps = 0;

            Dictionary<int, PackageInfo> allPackages = PackageInfo.LoadPackages(string.Format(CultureInfo.InvariantCulture, Constants.PackageInfo, Settings.Instance.SteamPath));

            Dictionary<int, GameListingSource> ownedApps = new Dictionary<int, GameListingSource>();

            string localConfigPath = string.Format(CultureInfo.InvariantCulture, Constants.LocalConfig, Settings.Instance.SteamPath, Steam.ToSteam3Id(accountId));

            VDFNode vdfFile;
            using (StreamReader streamReader = new StreamReader(localConfigPath))
            {
                vdfFile = VDFNode.LoadFromText(streamReader);
            }

            if (vdfFile != null)
            {
                VDFNode licensesNode = vdfFile.GetNodeAt(new[]
                {
                    "UserLocalConfigStore",
                    "Licenses"
                }, false);
                if (licensesNode != null && licensesNode.NodeType == ValueType.Array)
                {
                    foreach (string key in licensesNode.NodeArray.Keys)
                    {
                        if (!int.TryParse(key, out int ownedPackageId))
                        {
                            continue;
                        }

                        PackageInfo ownedPackage = allPackages[ownedPackageId];
                        if (ownedPackageId == 0)
                        {
                            continue;
                        }

                        GameListingSource src = ownedPackage.BillingType == PackageBillingType.FreeOnDemand || ownedPackage.BillingType == PackageBillingType.AutoGrant ? GameListingSource.PackageFree : GameListingSource.PackageNormal;
                        foreach (int ownedAppId in ownedPackage.AppIds)
                        {
                            if (!ownedApps.ContainsKey(ownedAppId) || src == GameListingSource.PackageNormal && ownedApps[ownedAppId] == GameListingSource.PackageFree)
                            {
                                ownedApps[ownedAppId] = src;
                            }
                        }
                    }
                }

                // update LastPlayed
                VDFNode appsNode = vdfFile.GetNodeAt(new[]
                {
                    "UserLocalConfigStore",
                    "Software",
                    "Valve",
                    "Steam",
                    "apps"
                }, false);
                GetLastPlayedFromVdf(appsNode, ignored);
            }

            foreach (KeyValuePair<int, GameListingSource> kv in ownedApps)
            {
                string name = Database.GetName(kv.Key);
                GameInfo newGame = IntegrateGame(kv.Key, name, false, ignored, kv.Value, out bool isNew);
                if (newGame != null)
                {
                    totalApps++;
                }

                if (isNew)
                {
                    newApps++;
                }
            }

            return totalApps;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Searches a list of games, looking for the one that matches the information in the shortcut node.
        ///     Checks launch ID first, then checks a combination of name and ID, then just checks name.
        /// </summary>
        /// <param name="shortcutId">ID of the shortcut node</param>
        /// <param name="shortcutNode">Shotcut node itself</param>
        /// <param name="gamesToMatchAgainst">List of game objects to match against</param>
        /// <param name="shortcutLaunchIds">List of launch IDs referenced by name</param>
        /// <returns>The index of the matching game if found, -1 otherwise.</returns>
        private static int FindMatchingShortcut(int shortcutId, VDFNode shortcutNode, List<GameInfo> gamesToMatchAgainst, StringDictionary shortcutLaunchIds)
        {
            VDFNode nodeName = shortcutNode.GetNodeAt(new[]
            {
                "appname"
            }, false);
            string gameName = nodeName?.NodeString;
            string launchId = shortcutLaunchIds[gameName];

            // First, look for games with matching launch IDs.
            if (!string.IsNullOrEmpty(launchId))
            {
                for (int i = 0; i < gamesToMatchAgainst.Count; i++)
                {
                    if (gamesToMatchAgainst[i].LaunchString == launchId)
                    {
                        return i;
                    }
                }
            }

            // Second, look for games with matching names AND matching shortcut IDs.
            for (int i = 0; i < gamesToMatchAgainst.Count; i++)
            {
                if (gamesToMatchAgainst[i].Id == -(shortcutId + 1) && gamesToMatchAgainst[i].Name == gameName)
                {
                    return i;
                }
            }

            // Third, just look for name matches
            for (int i = 0; i < gamesToMatchAgainst.Count; i++)
            {
                if (gamesToMatchAgainst[i].Name == gameName)
                {
                    return i;
                }
            }

            return -1;
        }

        private static void LoadShortcutLaunchIds(long steamId, out StringDictionary shortcutLaunchIds)
        {
            shortcutLaunchIds = new StringDictionary();

            string filePath = string.Format(CultureInfo.InvariantCulture, Constants.Screenshots, Settings.Instance.SteamPath, Steam.ToSteam3Id(steamId));
            if (!File.Exists(filePath))
            {
                Logger.Warn("LoadShortcutLaunchIds: Could not find screenshots.vdf at the specified location.");
                return;
            }

            StreamReader reader = null;
            try
            {
                reader = new StreamReader(filePath, false);
                VDFNode dataRoot = VDFNode.LoadFromText(reader, true);

                VDFNode appsNode = dataRoot.GetNodeAt(new[]
                {
                    "shortcutnames"
                }, false);

                foreach (KeyValuePair<string, VDFNode> shortcutPair in appsNode.NodeArray)
                {
                    string launchId = shortcutPair.Key;
                    string gameName = shortcutPair.Value.NodeString;
                    if (!shortcutLaunchIds.ContainsKey(gameName))
                    {
                        shortcutLaunchIds.Add(gameName, launchId);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Logger.Error("Error opening Steam config file: {0}", e.ToString());
            }
            catch (IOException e)
            {
                Logger.Error("Loading existing Steam config failed: {0}", e.ToString());
            }
            finally
            {
                reader?.Close();
            }
        }

        private void GetLastPlayedFromVdf(VDFNode appsNode, ICollection<int> ignore)
        {
            Dictionary<string, VDFNode> gameNodeArray = appsNode?.NodeArray;
            if (gameNodeArray == null)
            {
                return;
            }

            foreach (KeyValuePair<string, VDFNode> gameNodePair in gameNodeArray)
            {
                if (!int.TryParse(gameNodePair.Key, out int gameId))
                {
                    continue;
                }

                if (ignore != null && ignore.Contains(gameId) || !Database.IncludeItemInGameList(gameId))
                {
                    Logger.Verbose("Skipped processing game {0} from Steam config.", gameId);
                }
                else if (gameNodePair.Value != null && gameNodePair.Value.NodeType == ValueType.Array)
                {
                    GameInfo game;

                    // Add the game to the list if it doesn't exist already
                    if (!Games.ContainsKey(gameId))
                    {
                        game = new GameInfo(gameId, Database.GetName(gameId), this);
                        Games.Add(gameId, game);
                        Logger.Verbose("Added new game found in Steam config: {0} - {1}", gameId, game.Name);
                    }
                    else
                    {
                        game = Games[gameId];
                    }

                    if (!gameNodePair.Value.ContainsKey("LastPlayed") || gameNodePair.Value["LastPlayed"].NodeInt == 0)
                    {
                        continue;
                    }

                    game.LastPlayed = gameNodePair.Value["LastPlayed"].NodeInt;
                    Logger.Verbose("Processed game from Steam config: {0}, Cat: {1}", gameId, DateTimeOffset.FromUnixTimeSeconds(game.LastPlayed).Date);
                }
            }
        }

        private int IntegrateFromNode(VDFNode appsNode, ICollection<int> ignore)
        {
            int loadedGames = 0;

            // Validate appsNode
            if (appsNode?.NodeArray == null || !(appsNode.NodeArray is Dictionary<string, VDFNode> gameNodeArray))
            {
                return loadedGames;
            }

            // Make sure ignore list is not null
            ignore = ignore ?? new List<int>();

            foreach (KeyValuePair<string, VDFNode> gameNodePair in gameNodeArray)
            {
                if (!int.TryParse(gameNodePair.Key, out int gameId))
                {
                    continue;
                }

                if (gameNodePair.Value == null || gameNodePair.Value.NodeType != ValueType.Array)
                {
                    Logger.Verbose("Skipped processing game {0} from Steam config, value was null or not of type Array.", gameId);
                    continue;
                }

                if (ignore.Contains(gameId) || !Database.IncludeItemInGameList(gameId))
                {
                    Logger.Verbose("Skipped processing game {0} from Steam config.", gameId);
                    continue;
                }

                GameInfo game;

                // Add the game to the list if it doesn't exist already
                if (!Games.ContainsKey(gameId))
                {
                    game = new GameInfo(gameId, Database.GetName(gameId), this);
                    Games.Add(gameId, game);
                    Logger.Verbose("Added new game found in Steam config: {0} - {1}", gameId, game.Name);
                }
                else
                {
                    game = Games[gameId];
                }

                loadedGames++;

                game.ApplySource(GameListingSource.SteamConfig);

                game.IsHidden = gameNodePair.Value.ContainsKey("hidden") && gameNodePair.Value["hidden"].NodeInt != 0;

                VDFNode tagsNode = gameNodePair.Value["tags"];
                if (tagsNode?.NodeArray != null && tagsNode.NodeArray is Dictionary<string, VDFNode> tagArray)
                {
                    List<Category> categories = new List<Category>(tagArray.Count);
                    foreach (VDFNode tag in tagArray.Values)
                    {
                        string tagName = tag.NodeString;
                        if (string.IsNullOrWhiteSpace(tagName))
                        {
                            continue;
                        }

                        Category category = GetCategory(tagName);
                        if (category != null)
                        {
                            categories.Add(category);
                        }
                    }

                    if (categories.Count > 0)
                    {
                        SetGameCategories(gameId, categories, false);
                    }
                }

                Logger.Verbose("Processed game from Steam config: {0}, Cat: {1}", gameId, string.Join(",", game.Categories));
            }

            return loadedGames;
        }

        /// <summary>
        ///     Adds a new game to the database, or updates an existing game with new information.
        /// </summary>
        /// <param name="appId">App ID to add or update</param>
        /// <param name="appName">Name of app to add, or update to</param>
        /// <param name="overwriteName">If true, will overwrite any existing games. If false, will fail if the game already exists.</param>
        /// <param name="ignore">Set of games to ignore. Can be null. If the game is in this list, no action will be taken.</param>
        /// <param name="src">The listing source that this request came from.</param>
        /// <param name="isNew">If true, a new game was added. If false, an existing game was updated, or the operation failed.</param>
        /// <returns>True if the game was integrated, false otherwise.</returns>
        private GameInfo IntegrateGame(int appId, string appName, bool overwriteName, SortedSet<int> ignore, GameListingSource src, out bool isNew)
        {
            isNew = false;
            if (ignore != null && ignore.Contains(appId) || !Database.IncludeItemInGameList(appId))
            {
                Logger.Verbose("Skipped integrating game: {0} - {1}.", appId, appName);
                return null;
            }

            GameInfo result;
            if (!Games.ContainsKey(appId))
            {
                result = new GameInfo(appId, appName, this);
                Games.Add(appId, result);
                isNew = true;
            }
            else
            {
                result = Games[appId];
                if (overwriteName && !string.IsNullOrWhiteSpace(appName))
                {
                    result.Name = appName;
                }
            }

            result.ApplySource(src);

            Logger.Verbose("Integrated game into game list: {0} - {1}. New: {2}", appId, appName, isNew);
            return result;
        }

        private bool IntegrateShortcut(int gameId, VDFNode gameNode, StringDictionary launchIds)
        {
            // The ID of the created game must be negative
            int newId = -(gameId + 1);

            // This should never happen, but just in case
            if (Games.ContainsKey(newId))
            {
                return false;
            }

            string gameName = gameNode.GetNodeAt(new[]
            {
                "appname"
            }, false)?.NodeString ?? string.Empty;

            // Create the new GameInfo
            GameInfo game = new GameInfo(newId, gameName, this);
            Games.Add(newId, game);

            // Fill in the LaunchString
            game.LaunchString = launchIds[gameName];

            // Fill in the Executable
            game.Executable = gameNode.GetNodeAt(new[]
            {
                "exe"
            }, false)?.NodeString ?? game.Executable;

            // Fill in the LastPlayed
            game.LastPlayed = gameNode.GetNodeAt(new[]
            {
                "LastPlayTime"
            }, false)?.NodeInt ?? game.LastPlayed;

            // Fill in categories
            VDFNode tagsNode = gameNode.GetNodeAt(new[]
            {
                "tags"
            }, false);

            foreach (KeyValuePair<string, VDFNode> tag in tagsNode?.NodeArray ?? new Dictionary<string, VDFNode>())
            {
                string tagName = tag.Value.NodeString;
                game.AddCategory(GetCategory(tagName));
            }

            // Fill in Hidden
            game.IsHidden = false;
            if (!gameNode.ContainsKey("IsHidden"))
            {
                return true;
            }

            VDFNode hiddenNode = gameNode["IsHidden"];
            if (hiddenNode == null)
            {
                return true;
            }

            game.IsHidden = hiddenNode.NodeString == "1" || hiddenNode.NodeInt == 1;

            return true;
        }

        #endregion
    }
}
