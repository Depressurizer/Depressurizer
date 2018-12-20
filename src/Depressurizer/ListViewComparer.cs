using System;
using System.Collections;
using System.Windows.Forms;

namespace Depressurizer
{
    public class ListViewComparer : IComparer
    {
        #region Constructors and Destructors

        public ListViewComparer(int columnNumber, SortOrder sortOrder)
        {
            ColumnNumber = columnNumber;
            SortOrder = sortOrder;
        }

        #endregion

        #region Properties

        private int ColumnNumber { get; }

        private SortOrder SortOrder { get; }

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
        public int Compare(object x, object y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }

                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            ListViewItem listViewItem1 = x as ListViewItem;
            ListViewItem listViewItem2 = y as ListViewItem;

            if (listViewItem1 == null)
            {
                if (listViewItem2 == null)
                {
                    return 0;
                }

                return -1;
            }

            if (listViewItem2 == null)
            {
                return 1;
            }

            string string1 = listViewItem1.SubItems.Count <= ColumnNumber ? "" : listViewItem1.SubItems[ColumnNumber].Text;
            string string2 = listViewItem2.SubItems.Count <= ColumnNumber ? "" : listViewItem2.SubItems[ColumnNumber].Text;

            int result;
            if (double.TryParse(string1, out double double1) && double.TryParse(string2, out double double2))
            {
                result = double1.CompareTo(double2);
            }
            else
            {
                if (DateTime.TryParse(string1, out DateTime date1) && DateTime.TryParse(string2, out DateTime date2))
                {
                    result = date1.CompareTo(date2);
                }
                else
                {
                    result = string.Compare(string1, string2, StringComparison.CurrentCultureIgnoreCase);
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
