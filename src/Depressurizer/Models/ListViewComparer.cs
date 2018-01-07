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
    public sealed class ListViewComparer : IComparer
    {
        #region Fields

        private readonly int _columnNumber;

        private readonly SortOrder _sortOrder;

        #endregion

        #region Constructors and Destructors

        public ListViewComparer(int columnNumber, SortOrder sortOrder)
        {
            _columnNumber = columnNumber;
            _sortOrder = sortOrder;
        }

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
        public int Compare(object x, object y)
        {
            ListViewItem listViewItemX;
            ListViewItem listViewItemY;

            try
            {
                // Cast objects as ListViewItem
                listViewItemX = (ListViewItem) x;
                listViewItemY = (ListViewItem) y;

                if (listViewItemX == null)
                {
                    throw new NullReferenceException("listViewItemX is null");
                }

                if (listViewItemY == null)
                {
                    throw new NullReferenceException("listViewItemY is null");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
            }

            // Get the corresponding sub-item values.
            string valueX = listViewItemX.SubItems.Count <= _columnNumber ? "" : listViewItemX.SubItems[_columnNumber].Text;
            string valueY = listViewItemY.SubItems.Count <= _columnNumber ? "" : listViewItemY.SubItems[_columnNumber].Text;

            // Compare them.
            int result;

            // Treat as a number.
            if (double.TryParse(valueX, out double doubleX) && double.TryParse(valueY, out double doubleY))
            {
                result = doubleX.CompareTo(doubleY);
            }
            else
            {
                // Treat as a date.
                if (DateTime.TryParse(valueX, out DateTime dateX) && DateTime.TryParse(valueY, out DateTime dateY))
                {
                    result = dateX.CompareTo(dateY);
                }

                // Treat as a string.
                else
                {
                    result = string.Compare(valueX, valueY, StringComparison.Ordinal);
                }
            }

            // Return the correct result depending on whether
            // we're sorting ascending or descending.
            if (_sortOrder == SortOrder.Ascending)
            {
                return result;
            }

            return -result;
        }

        #endregion
    }
}