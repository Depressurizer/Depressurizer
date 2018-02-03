#region LICENSE

//     This file (ListViewComparer.cs) is part of Depressurizer.
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
using System.Windows.Forms;

namespace Depressurizer.Models
{
	/// <summary>
	///     Compares two ListView items based on a selected column.
	/// </summary>
	public class ListViewComparer : IComparer
	{
		#region Constructors and Destructors

		public ListViewComparer(int columnNumber, SortOrder sortOrder)
		{
			ColumnNumber = columnNumber;
			SortOrder = sortOrder;
		}

		#endregion

		#region Public Properties

		public int ColumnNumber { get; }

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

			string valueX = listViewItemX.SubItems.Count <= ColumnNumber ? "" : listViewItemX.SubItems[ColumnNumber].Text;
			string valueY = listViewItemY.SubItems.Count <= ColumnNumber ? "" : listViewItemY.SubItems[ColumnNumber].Text;

			int result;
			if (double.TryParse(valueX, out double doubleX) && double.TryParse(valueY, out double doubleY))
			{
				result = doubleX.CompareTo(doubleY);
			}
			else
			{
				if (DateTime.TryParse(valueX, out DateTime dateX) && DateTime.TryParse(valueY, out DateTime dateY))
				{
					result = DateTime.Compare(dateX, dateY);
				}
				else
				{
					result = string.Compare(valueX, valueY, StringComparison.CurrentCultureIgnoreCase);
				}
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