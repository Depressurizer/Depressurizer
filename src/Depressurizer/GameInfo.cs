using System.Collections.Generic;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Models;

namespace Depressurizer
{
    /// <summary>
    ///     Represents a single game and its categories.
    /// </summary>
    public class GameInfo
    {
        #region Constants

        private const string RunSteam = "steam://rungameid/{0}";

        #endregion

        #region Fields

        public SortedSet<Category> Categories;

        public GameList GameList;

        public int Id; // Positive ID matches to a Steam ID, negative means it's a non-steam game (= -1 - shortcut ID)

        public string Name;

        public GameListingSource Source;

        private string _executable;

        private string _launchStr;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Construct a new GameInfo with no categories set.
        /// </summary>
        /// <param name="id">ID of the new game. Positive means it's the game's Steam ID, negative means it's a non-steam game.</param>
        /// <param name="name">Game title</param>
        public GameInfo(int id, string name, GameList list, string executable = null)
        {
            Id = id;
            Name = name;

            Categories = new SortedSet<Category>();
            GameList = list;
            Executable = executable;
        }

        #endregion

        #region Public Properties

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

        public double HoursPlayed { get; set; } = 0;

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

        #endregion

        #region Properties

        private static Database Database => Database.Instance;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Adds a single category to this game. Does nothing if the category is already attached.
        /// </summary>
        /// <param name="newCat">Category to add</param>
        public void AddCategory(Category newCat)
        {
            if (newCat != null && Categories.Add(newCat) && !IsHidden)
            {
                newCat.Count++;
            }
        }

        /// <summary>
        ///     Adds a list of categories to this game. Skips categories that are already attached.
        /// </summary>
        /// <param name="newCats">A list of categories to add</param>
        public void AddCategory(ICollection<Category> newCats)
        {
            foreach (Category cat in newCats)
            {
                if (!Categories.Contains(cat))
                {
                    AddCategory(cat);
                }
            }
        }

        public void ApplySource(GameListingSource src)
        {
            if (Source < src)
            {
                Source = src;
            }
        }

        /// <summary>
        ///     Removes all categories from this game.
        /// </summary>
        public void ClearCategories()
        {
            ClearCategories(false);
        }

        /// <summary>
        ///     Removes all categories from this game.
        ///     <param name="alsoClearFavorite">If true, removes the favorite category as well.</param>
        /// </summary>
        public void ClearCategories(bool alsoClearFavorite)
        {
            foreach (Category cat in Categories)
            {
                if (!IsHidden)
                {
                    cat.Count--;
                }
            }

            if (alsoClearFavorite)
            {
                Categories.Clear();
            }
            else
            {
                bool restore = IsFavorite();
                Categories.Clear();
                if (restore)
                {
                    Categories.Add(FavoriteCategory);
                    if (!IsHidden)
                    {
                        FavoriteCategory.Count++;
                    }
                }
            }
        }

        /// <summary>
        ///     Check whether the game includes the given category
        /// </summary>
        /// <param name="c">Category to look for</param>
        /// <returns>True if category is found</returns>
        public bool ContainsCategory(Category c)
        {
            return Categories.Contains(c);
        }

        /// <summary>
        ///     Gets a string listing the game's assigned categories.
        /// </summary>
        /// <returns>List of the game's categories, separated by commas.</returns>
        public string GetCatString()
        {
            return GetCatString(null);
        }

        /// <summary>
        ///     Gets a string listing the game's assigned categories.
        /// </summary>
        /// <param name="ifEmpty">Value to return if there are no categories</param>
        /// <returns>List of the game's categories, separated by commas.</returns>
        public string GetCatString(string ifEmpty)
        {
            return GetCatString(ifEmpty, false);
        }

        /// <summary>
        ///     Gets a string listing the game's assigned categories.
        /// </summary>
        /// <param name="ifEmpty">Value to return if there are no categories</param>
        /// <param name="includeFavorite">If true, include the favorite category.</param>
        /// <returns>List of the game's categories, separated by commas.</returns>
        public string GetCatString(string ifEmpty, bool includeFavorite)
        {
            string result = "";
            bool first = true;
            foreach (Category c in Categories)
            {
                if (includeFavorite || c != FavoriteCategory)
                {
                    if (!first)
                    {
                        result += ", ";
                    }

                    result += c.Name;
                    first = false;
                }
            }

            return first ? ifEmpty : result;
        }

        /// <summary>
        ///     Check to see if the game has any categories at all (except the Favorite category)
        /// </summary>
        /// <returns>True if the category set is not empty</returns>
        public bool HasCategories()
        {
            return HasCategories(false);
        }

        /// <summary>
        ///     Check to see if the game has any categories at all (except the Favorite category)
        /// </summary>
        /// <param name="includeFavorite">
        ///     If true, will only return true if the game is not in the favorite category. If false, the
        ///     favorite category is ignored.
        /// </param>
        /// <returns>True if the category set is not empty</returns>
        public bool HasCategories(bool includeFavorite)
        {
            if (Categories.Count == 0)
            {
                return false;
            }

            return !(!includeFavorite && Categories.Count == 1 && Categories.Contains(FavoriteCategory));
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

            bool isVR = false;
            if (f.VR != (int) AdvancedFilterState.None)
            {
                isVR = Database.SupportsVR(Id);
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
                isSoftware = Database.IsType(Id, AppType.Application);
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
                isGame = Database.IsType(Id, AppType.Game);
            }

            switch (f.Game)
            {
                case (int) AdvancedFilterState.Exclude when isGame:
                case (int) AdvancedFilterState.Require when !isGame:
                    return false;
            }

            if (f.Uncategorized == (int) AdvancedFilterState.Allow || f.Hidden == (int) AdvancedFilterState.Allow || f.VR == (int) AdvancedFilterState.Allow || f.Allow.Count > 0)
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

        /// <summary>
        ///     Removes a single category from this game. Does nothing if the category is not attached to this game.
        /// </summary>
        /// <param name="remCat">Category to remove</param>
        public void RemoveCategory(Category remCat)
        {
            if (Categories.Remove(remCat) && !IsHidden)
            {
                remCat.Count--;
            }
        }

        /// <summary>
        ///     Removes a list of categories from this game. Skips categories that are not attached to this game.
        /// </summary>
        /// <param name="remCats">Categories to remove</param>
        public void RemoveCategory(ICollection<Category> remCats)
        {
            foreach (Category cat in remCats)
            {
                if (!Categories.Contains(cat))
                {
                    RemoveCategory(cat);
                }
            }
        }

        /// <summary>
        ///     Sets the categories for this game to exactly match the given list. Missing categories will be added and extra ones
        ///     will be removed.
        /// </summary>
        /// <param name="cats">Set of categories to apply to this game</param>
        public void SetCategories(ICollection<Category> cats, bool preserveFavorite)
        {
            ClearCategories(!preserveFavorite);
            AddCategory(cats);
        }

        public void SetFavorite(bool fav)
        {
            if (fav)
            {
                AddCategory(FavoriteCategory);
            }
            else
            {
                RemoveCategory(FavoriteCategory);
            }
        }

        /// <summary>
        ///     Add or remove the hidden attribute for this game.
        /// </summary>
        /// <param name="hide">Whether the game should be hidden</param>
        public void SetHidden(bool hide)
        {
            if (IsHidden == hide)
            {
                return;
            }

            if (hide)
            {
                foreach (Category cat in Categories)
                {
                    cat.Count--;
                }
            }
            else
            {
                foreach (Category cat in Categories)
                {
                    cat.Count++;
                }
            }

            IsHidden = hide;
        }

        #endregion
    }
}
