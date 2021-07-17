using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Interfaces;

namespace Depressurizer.Core.Models
{
    /// <summary>
    ///     Represents a single game and its categories.
    /// </summary>
    public class GameInfo
    {
        #region Constants

        private const string RunSteam = "steam://rungameid/{0}";

        private const string XmlNameGame = "game";

        private const string XmlNameGameCategory = "category";

        private const string XmlNameGameCategoryList = "categories";

        private const string XmlNameGameExecutable = "executable";

        private const string XmlNameGameHidden = "hidden";

        private const string XmlNameGameHoursPlayed = "hoursplayed";

        private const string XmlNameGameId = "id";

        private const string XmlNameGameLastPlayed = "lastplayed";

        private const string XmlNameGameName = "name";

        private const string XmlNameGameSource = "source";

        #endregion

        #region Fields

        private SortedSet<Category> _categories;

        private string _executable;

        private string _launchStr;

        #endregion

        #region Constructors and Destructors

        public GameInfo(int id, string name, IGameList list) : this(id, name, list, null) { }

        public GameInfo(int id, string name, IGameList list, string executable)
        {
            Id = id;
            Name = name;
            GameList = list;
            Executable = executable;
        }

        #endregion

        #region Public Properties

        public SortedSet<Category> Categories
        {
            get => _categories ?? (_categories = new SortedSet<Category>());
            set => _categories = value;
        }

        public string Executable
        {
            get
            {
                if (_executable == null)
                {
                    return string.Format(RunSteam, Id);
                }

                return _executable;
            }
            set
            {
                if (value != string.Format(RunSteam, Id))
                {
                    _executable = value;
                }
            }
        }

        public Category FavoriteCategory => GameList?.FavoriteCategory;

        public IGameList GameList { get; }

        public double HoursPlayed { get; set; }

        /// <summary>
        ///     Positive ID matches to a Steam ID, negative means it's a non-steam game (= -1 - shortcut ID)
        /// </summary>
        public int Id { get; }

        public bool IsHidden { get; set; }

        public long LastPlayed { get; set; }

        /// <summary>
        ///     ID String to use to launch this game. Uses the ID for steam games, but non-steam game IDs need to be set.
        /// </summary>
        public string LaunchString
        {
            get
            {
                if (Id > 0)
                {
                    return Id.ToString();
                }

                if (!string.IsNullOrEmpty(_launchStr))
                {
                    return _launchStr;
                }

                return null;
            }
            set => _launchStr = value;
        }

        /// <summary>
        ///     Game title
        /// </summary>
        public string Name { get; set; }

        public GameListingSource Source { get; set; }

        #endregion

        #region Properties

        private static IDatabase Database => SingletonKeeper.Database;

        #endregion

        #region Public Methods and Operators

        public static void AddFromNode(XmlNode node, IProfile profile)
        {
            if (!XmlUtil.TryGetIntFromNode(node[XmlNameGameId], out int id))
            {
                return;
            }

            GameListingSource source = XmlUtil.GetEnumFromNode(node[XmlNameGameSource], GameListingSource.Unknown);

            if (source < GameListingSource.Manual && profile.IgnoreList.Contains(id))
            {
                return;
            }

            string name = XmlUtil.GetStringFromNode(node[XmlNameGameName], null);
            GameInfo game = new GameInfo(id, name, profile.GameData)
            {
                Source = source
            };
            profile.GameData.Games.Add(id, game);

            game.IsHidden = XmlUtil.GetBoolFromNode(node[XmlNameGameHidden], false);
            game.Executable = XmlUtil.GetStringFromNode(node[XmlNameGameExecutable], null);
            game.LastPlayed = XmlUtil.GetIntFromNode(node[XmlNameGameLastPlayed], 0);
            game.HoursPlayed = XmlUtil.GetDoubleFromNode(node[XmlNameGameHoursPlayed], 0);

            XmlNode catListNode = node.SelectSingleNode(XmlNameGameCategoryList);
            XmlNodeList catNodes = catListNode?.SelectNodes(XmlNameGameCategory);
            if (catNodes == null)
            {
                return;
            }

            foreach (XmlNode cNode in catNodes)
            {
                if (XmlUtil.TryGetStringFromNode(cNode, out string cat))
                {
                    game.AddCategory(profile.GameData.GetCategory(cat));
                }
            }
        }

        public void AddCategory(Category category)
        {
            if (category == null)
            {
                return;
            }

            if (Categories.Add(category) && !IsHidden)
            {
                category.Count++;
            }
        }

        public void AddCategory(IEnumerable<Category> categories)
        {
            if (categories == null)
            {
                return;
            }

            foreach (Category category in categories)
            {
                if (Categories.Contains(category))
                {
                    continue;
                }

                AddCategory(category);
            }
        }

        public void ApplySource(GameListingSource src)
        {
            if (Source < src)
            {
                Source = src;
            }
        }

        public void ClearCategories()
        {
            ClearCategories(false);
        }

        public void ClearCategories(bool alsoClearFavorite)
        {
            if (Categories.Count == 0)
            {
                return;
            }

            foreach (Category category in Categories)
            {
                if (IsHidden)
                {
                    continue;
                }

                category.Count--;
            }

            if (alsoClearFavorite)
            {
                Categories.Clear();
            }
            else
            {
                bool restore = IsFavorite();
                Categories.Clear();

                if (!restore)
                {
                    return;
                }

                Categories.Add(FavoriteCategory);
                if (!IsHidden)
                {
                    FavoriteCategory.Count++;
                }
            }
        }

        public bool ContainsCategory(Category category)
        {
            return Categories.Contains(category);
        }

        public string GetCatString()
        {
            return GetCatString(null);
        }

        public string GetCatString(string ifEmpty)
        {
            return GetCatString(ifEmpty, false);
        }

        public string GetCatString(string ifEmpty, bool includeFavorite)
        {
            if (Categories.Count == 0)
            {
                return ifEmpty;
            }

            string result = "";
            bool first = true;
            foreach (Category category in Categories)
            {
                if (!includeFavorite && category == FavoriteCategory)
                {
                    continue;
                }

                if (!first)
                {
                    result += ", ";
                }

                result += category.Name;
                first = false;
            }

            return first ? ifEmpty : result;
        }

        public bool HasCategories()
        {
            return HasCategories(false);
        }

        public bool HasCategories(bool includeFavorite)
        {
            if (Categories.Count == 0)
            {
                return false;
            }

            if (Categories.Count >= 2)
            {
                return true;
            }

            return includeFavorite || !Categories.Contains(FavoriteCategory);
        }

        public bool IncludeGame(Filter f)
        {
            if (f == null)
            {
                return true;
            }

            bool isHidden = false;
            if (f.Hidden != (int) AdvancedFilterState.None)
            {
                isHidden = IsHidden;
            }

            switch (f.Hidden)
            {
                case (int) AdvancedFilterState.Require when !isHidden:
                case (int) AdvancedFilterState.Exclude when isHidden:
                    return false;
            }

            bool isCategorized = false;
            if (f.Uncategorized != (int) AdvancedFilterState.None)
            {
                isCategorized = HasCategories();
            }

            switch (f.Uncategorized)
            {
                case (int) AdvancedFilterState.Require when isCategorized:
                case (int) AdvancedFilterState.Exclude when !isCategorized:
                    return false;
            }

            bool inDatabase = Database.Contains(Id, out DatabaseEntry entry);

            bool isVR = false;
            if (f.VR != (int) AdvancedFilterState.None)
            {
                isVR = inDatabase && Database.SupportsVR(Id);
            }

            switch (f.VR)
            {
                case (int) AdvancedFilterState.Require when !isVR:
                case (int) AdvancedFilterState.Exclude when isVR:
                    return false;
            }

            bool isSoftware = false;
            if (f.Software != (int) AdvancedFilterState.None)
            {
                isSoftware = inDatabase && entry.AppType == AppType.Application;
            }

            switch (f.Software)
            {
                case (int) AdvancedFilterState.Require when !isSoftware:
                case (int) AdvancedFilterState.Exclude when isSoftware:
                    return false;
            }

            bool isGame = false;
            if (f.Game != (int) AdvancedFilterState.None)
            {
                isGame = inDatabase && entry.AppType == AppType.Game;
            }

            switch (f.Game)
            {
                case (int) AdvancedFilterState.Exclude when isGame:
                case (int) AdvancedFilterState.Require when !isGame:
                    return false;
            }


            bool isMod = false;
            if (f.Mod != (int)AdvancedFilterState.None)
            {
                isMod = inDatabase && entry.AppType == AppType.Mod;
            }

            switch (f.Mod)
            {
                case (int)AdvancedFilterState.Exclude when isMod:
                case (int)AdvancedFilterState.Require when !isMod:
                    return false;
            }

            if (IsAnySpecialCategoryAllowed(f) || f.Allow.Count > 0)
            {
                if (f.Uncategorized != (int) AdvancedFilterState.Allow || isCategorized)
                {
                    if (f.Hidden != (int) AdvancedFilterState.Allow || !isHidden)
                    {
                        if (f.VR != (int) AdvancedFilterState.Allow || !isVR)
                        {
                            if (f.Software != (int) AdvancedFilterState.Allow || !isSoftware)
                            {
                                if (f.Game != (int) AdvancedFilterState.Allow || !isGame)
                                {
                                    if (f.Mod != (int)AdvancedFilterState.Allow || !isMod)
                                    {
                                        if (!Categories.Overlaps(f.Allow))
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (!Categories.IsSupersetOf(f.Require))
            {
                return false;
            }

            if (Categories.Overlaps(f.Exclude))
            {
                return false;
            }

            return true;
        }

        public bool IsFavorite()
        {
            return ContainsCategory(FavoriteCategory);
        }

        public void RemoveCategory(Category category)
        {
            if (category == null)
            {
                return;
            }

            if (Categories.Remove(category) && !IsHidden)
            {
                category.Count--;
            }
        }

        public void RemoveCategory(IEnumerable<Category> categories)
        {
            if (categories == null)
            {
                return;
            }

            foreach (Category category in categories)
            {
                if (Categories.Contains(category))
                {
                    continue;
                }

                RemoveCategory(category);
            }
        }

        public void SetCategories(IEnumerable<Category> categories, bool preserveFavorite)
        {
            ClearCategories(!preserveFavorite);
            AddCategory(categories);
        }

        public void SetFavorite(bool isFavorite)
        {
            if (isFavorite)
            {
                AddCategory(FavoriteCategory);
            }
            else
            {
                RemoveCategory(FavoriteCategory);
            }
        }

        public void SetHidden(bool isHidden)
        {
            if (IsHidden == isHidden)
            {
                return;
            }

            foreach (Category cat in Categories)
            {
                if (isHidden)
                {
                    cat.Count--;
                }
                else
                {
                    cat.Count++;
                }
            }

            IsHidden = isHidden;
        }

        public void WriteToXml(XmlWriter writer)
        {
            // Don't save shortcuts if we aren't including them
            writer.WriteStartElement(XmlNameGame);

            writer.WriteElementString(XmlNameGameId, Id.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString(XmlNameGameSource, Source.ToString());

            if (Name != null)
            {
                writer.WriteElementString(XmlNameGameName, Name);
            }

            writer.WriteElementString(XmlNameGameHidden, IsHidden.ToString().ToLowerInvariant());

            if (LastPlayed != 0)
            {
                writer.WriteElementString(XmlNameGameLastPlayed, LastPlayed.ToString(CultureInfo.InvariantCulture));
            }

            if (HoursPlayed > 0)
            {
                writer.WriteElementString(XmlNameGameHoursPlayed, HoursPlayed.ToString(CultureInfo.InvariantCulture));
            }

            if (!Executable.Contains("steam://"))
            {
                writer.WriteElementString(XmlNameGameExecutable, Executable);
            }

            writer.WriteStartElement(XmlNameGameCategoryList);
            foreach (Category category in Categories)
            {
                string categoryName = category.Name;
                if (category.Name == GameList.FavoriteNewConfigValue)
                {
                    categoryName = GameList.FavoriteConfigValue;
                }

                writer.WriteElementString(XmlNameGameCategory, categoryName);
            }

            writer.WriteEndElement(); // categories

            writer.WriteEndElement(); // game
        }

        #endregion

        #region Methods

        private static bool IsAnySpecialCategoryAllowed(Filter filter)
        {
            if (filter == null)
            {
                return false;
            }

            if (filter.Uncategorized == (int) AdvancedFilterState.Allow)
            {
                return true;
            }

            if (filter.Hidden == (int) AdvancedFilterState.Allow)
            {
                return true;
            }

            if (filter.VR == (int) AdvancedFilterState.Allow)
            {
                return true;
            }

            if (filter.Software == (int) AdvancedFilterState.Allow)
            {
                return true;
            }

            if (filter.Game == (int) AdvancedFilterState.Allow)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
