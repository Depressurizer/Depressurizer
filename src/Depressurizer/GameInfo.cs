#region License

//     This file (GameInfo.cs) is part of Depressurizer.
//     Copyright (C) 2011  Steve Labbe
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

using System.Collections.Generic;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Models;
using Depressurizer.Models;

namespace Depressurizer
{
	/// <summary>
	///     Represents a single game and its categories.
	/// </summary>
	public class GameInfo
	{
		#region Constants

		private const string runSteam = "steam://rungameid/{0}";

		#endregion

		#region Fields

		public SortedSet<Category> Categories;

		public GameList GameList;

		public bool Hidden;

		public int Id; // Positive ID matches to a Steam ID, negative means it's a non-steam game (= -1 - shortcut ID)

		public int LastPlayed;

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
			Hidden = false;
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
					return string.Format(runSteam, Id);
				}

				return _executable;
			}
			set
			{
				if (value != string.Format(runSteam, Id))
				{
					_executable = value;
				}
			}
		}

		public Category FavoriteCategory
		{
			get
			{
				if (GameList == null)
				{
					return null;
				}

				return GameList.FavoriteCategory;
			}
		}

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
			if ((newCat != null) && Categories.Add(newCat) && !Hidden)
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
		///     <param name="alsoClearFavorite">If true, removes the favorite category as well.</param>
		/// </summary>
		public void ClearCategories(bool alsoClearFavorite = false)
		{
			foreach (Category cat in Categories)
			{
				if (!Hidden)
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
					if (!Hidden)
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
		/// <param name="ifEmpty">Value to return if there are no categories</param>
		/// <param name="includeFavorite">If true, include the favorite category.</param>
		/// <returns>List of the game's categories, separated by commas.</returns>
		public string GetCatString(string ifEmpty = "", bool includeFavorite = false)
		{
			string result = "";
			bool first = true;
			foreach (Category c in Categories)
			{
				if (includeFavorite || (c != FavoriteCategory))
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
		/// <param name="includeFavorite">
		///     If true, will only return true if the game is not in the favorite category. If false, the
		///     favorite category is ignored.
		/// </param>
		/// <returns>True if the category set is not empty</returns>
		public bool HasCategories(bool includeFavorite = false)
		{
			if (Categories.Count == 0)
			{
				return false;
			}

			return !(!includeFavorite && (Categories.Count == 1) && Categories.Contains(FavoriteCategory));
		}

		/// <summary>
		///     Check to see if the game has any categories set that do not exist in the given list
		/// </summary>
		/// <param name="except">List of games to exclude from the  check</param>
		/// <returns>True if the game has any categories that do not exist in the list</returns>
		public bool HasCategoriesExcept(ICollection<Category> except)
		{
			if (Categories.Count == 0)
			{
				return false;
			}

			foreach (Category c in Categories)
			{
				if (!except.Contains(c))
				{
					return true;
				}
			}

			return false;
		}

		public bool IncludeGame(Filter f)
		{
			if (f == null)
			{
				return true;
			}

			bool isCategorized = false;
			bool isHidden = false;
			bool isVR = false;
			if (f.Uncategorized != (int) AdvancedFilterState.None)
			{
				isCategorized = HasCategories();
			}

			if (f.Hidden != (int) AdvancedFilterState.None)
			{
				isHidden = Hidden;
			}

			if (f.VR != (int) AdvancedFilterState.None)
			{
				isVR = Database.SupportsVR(Id);
			}

			if ((f.Uncategorized == (int) AdvancedFilterState.Require) && isCategorized)
			{
				return false;
			}

			if ((f.Hidden == (int) AdvancedFilterState.Require) && !isHidden)
			{
				return false;
			}

			if ((f.VR == (int) AdvancedFilterState.Require) && !isVR)
			{
				return false;
			}

			if ((f.Uncategorized == (int) AdvancedFilterState.Exclude) && !isCategorized)
			{
				return false;
			}

			if ((f.Hidden == (int) AdvancedFilterState.Exclude) && isHidden)
			{
				return false;
			}

			if ((f.VR == (int) AdvancedFilterState.Exclude) && isVR)
			{
				return false;
			}

			if ((f.Uncategorized == (int) AdvancedFilterState.Allow) || (f.Hidden == (int) AdvancedFilterState.Allow) || (f.VR == (int) AdvancedFilterState.Allow) || (f.Allow.Count > 0))
			{
				if ((f.Uncategorized != (int) AdvancedFilterState.Allow) || isCategorized)
				{
					if ((f.Hidden != (int) AdvancedFilterState.Allow) || !isHidden)
					{
						if ((f.VR != (int) AdvancedFilterState.Allow) || !isVR)
						{
							if (!Categories.Overlaps(f.Allow))
							{
								return false;
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
			if (Categories.Remove(remCat) && !Hidden)
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
			if (Hidden == hide)
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

			Hidden = hide;
		}

		#endregion
	}
}