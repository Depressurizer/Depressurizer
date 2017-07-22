using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Depressurizer
{
    // Compares two ListView items based on a selected column.
    public class ListViewComparer : System.Collections.IComparer
    {
        private int ColumnNumber;
        private SortOrder SortOrder;

        public ListViewComparer(int column_number,
            SortOrder sort_order)
        {
            ColumnNumber = column_number;
            SortOrder = sort_order;
        }

        // Compare two ListViewItems.
        public int Compare(object object_x, object object_y)
        {
            // Get the objects as ListViewItems.
            ListViewItem item_x = object_x as ListViewItem;
            ListViewItem item_y = object_y as ListViewItem;

            // Get the corresponding sub-item values.
            string string_x;
            string_x = item_x.SubItems.Count <= ColumnNumber ? "" : item_x.SubItems[ColumnNumber].Text;

            string string_y;
            string_y = item_y.SubItems.Count <= ColumnNumber ? "" : item_y.SubItems[ColumnNumber].Text;

            // Compare them.
            int result;
            double double_x, double_y;
            if (double.TryParse(string_x, out double_x) &&
                double.TryParse(string_y, out double_y))
            {
                // Treat as a number.
                result = double_x.CompareTo(double_y);
            }
            else
            {
                DateTime date_x, date_y;
                if (DateTime.TryParse(string_x, out date_x) &&
                    DateTime.TryParse(string_y, out date_y))
                {
                    // Treat as a date.
                    result = date_x.CompareTo(date_y);
                }
                else
                {
                    // Treat as a string.
                    result = string_x.CompareTo(string_y);
                }
            }

            // Return the correct result depending on whether
            // we're sorting ascending or descending.
            if (SortOrder == SortOrder.Ascending)
            {
                return result;
            }
            return -result;
        }
    }

    // Compares two lstCategories items based on a selected column.
    public class lstCategoriesComparer : System.Collections.IComparer
    {
        public enum categorySortMode
        {
            Name,
            Count
        }

        private categorySortMode SortMode;
        private SortOrder SortOrder;

        public lstCategoriesComparer(categorySortMode sortMode,
            SortOrder sortOrder)
        {
            SortMode = sortMode;
            SortOrder = sortOrder;
        }

        // Compare two ListViewItems.
        public int Compare(object object_x, object object_y)
        {
            // Get the objects as ListViewItems.
            ListViewItem item_x = object_x as ListViewItem;
            ListViewItem item_y = object_y as ListViewItem;

            if (item_x == null)
            {
                return 1;
            }
            if (item_y == null)
            {
                return -1;
            }

            // Handle special categories
            List<string> specialCategories = new List<string>();
            specialCategories.Add(GlobalStrings.MainForm_All);
            specialCategories.Add(GlobalStrings.MainForm_Uncategorized);
            specialCategories.Add(GlobalStrings.MainForm_Hidden);
            specialCategories.Add(GlobalStrings.MainForm_Favorite);
            specialCategories.Add(GlobalStrings.MainForm_VR);

            foreach (string s in specialCategories)
            {
                if (item_x.Tag.ToString() == s)
                {
                    return -1;
                }
                if (item_y.Tag.ToString() == s)
                {
                    return 1;
                }
            }

            Category cat_x = item_x.Tag as Category;
            Category cat_y = item_y.Tag as Category;

            // Compare categories.
            int result;

            if (SortMode == categorySortMode.Count)
            {
                result = cat_x.Count.CompareTo(cat_y.Count);
            }
            else
            {
                result = string.Compare(cat_x.Name, cat_y.Name, StringComparison.CurrentCultureIgnoreCase);
            }

            // Return the correct result depending on whether
            // we're sorting ascending or descending.
            if (SortOrder == SortOrder.Ascending)
            {
                return result;
            }
            return -result;
        }
    }
}