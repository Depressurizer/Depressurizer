using System;
using System.Collections;
using System.Windows.Forms;

namespace Depressurizer
{
	// Compares two ListView items based on a selected column.
	public class ListViewComparer : IComparer
	{
		#region Fields

		private readonly int ColumnNumber;

		private readonly SortOrder SortOrder;

		#endregion

		#region Constructors and Destructors

		public ListViewComparer(int column_number, SortOrder sort_order)
		{
			ColumnNumber = column_number;
			SortOrder = sort_order;
		}

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

	// Compares two lstCategories items based on a selected column.
}