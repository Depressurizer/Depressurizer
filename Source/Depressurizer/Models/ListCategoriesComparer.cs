#region LICENSE

//     This file (ListCategoriesComparer.cs) is part of Depressurizer.
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
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using DepressurizerCore.Models;

namespace Depressurizer.Models
{
	public enum CategorySortMode
	{
		Name,
		Count
	}

	/// <summary>
	///     Compares two lstCategories items based on a selected column
	/// </summary>
	public sealed class ListCategoriesComparer : IComparer
	{
		#region Constructors and Destructors

		public ListCategoriesComparer(CategorySortMode sortMode, SortOrder sortOrder)
		{
			SortMode = sortMode;
			SortOrder = sortOrder;
		}

		#endregion

		#region Public Properties

		public CategorySortMode SortMode { get; }

		public SortOrder SortOrder { get; }

		#endregion

		#region Public Methods and Operators

		/// <inheritdoc />
		public int Compare(object x, object y)
		{
			if (x == null)
			{
				return 1;
			}

			if (y == null)
			{
				return -1;
			}

			if (!(x is ListViewItem listViewItemX))
			{
				throw new ArgumentException("Object X is not a ListViewItem");
			}

			if (!(y is ListViewItem listViewItemY))
			{
				throw new ArgumentException("Object Y is not a ListViewItem");
			}

			// Handle special categories
			List<string> specialCategories = new List<string>
			{
				GlobalStrings.MainForm_All,
				GlobalStrings.MainForm_Uncategorized,
				GlobalStrings.MainForm_Hidden,
				GlobalStrings.MainForm_Favorite,
				GlobalStrings.MainForm_VR
			};

			foreach (string category in specialCategories)
			{
				if (listViewItemX.Tag.ToString() == category)
				{
					return -1;
				}

				if (listViewItemY.Tag.ToString() == category)
				{
					return 1;
				}
			}

			if (!(listViewItemX.Tag is Category categoryX))
			{
				throw new ArgumentException("listViewItemX.Tag is not a Category");
			}

			if (!(listViewItemY.Tag is Category categoryY))
			{
				throw new ArgumentException("listViewItemY.Tag is not a Category");
			}

			int result;
			switch (SortMode)
			{
				case CategorySortMode.Name:
					result = string.Compare(categoryX.Name, categoryY.Name, StringComparison.CurrentCultureIgnoreCase);
					break;
				case CategorySortMode.Count:
					result = categoryX.Count.CompareTo(categoryY.Count);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			if (SortOrder == SortOrder.Ascending)
			{
				return result;
			}

			return -result;
		}

		#endregion
	}
}