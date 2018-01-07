using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DepressurizerCore.Models;

namespace Depressurizer
{
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

            if (item_x == null) return 1;
            if (item_y == null) return -1;

            // Handle special categories
            List<string> specialCategories = new List<string>();
            specialCategories.Add(GlobalStrings.MainForm_All);
            specialCategories.Add(GlobalStrings.MainForm_Uncategorized);
            specialCategories.Add(GlobalStrings.MainForm_Hidden);
            specialCategories.Add(GlobalStrings.MainForm_Favorite);
            specialCategories.Add(GlobalStrings.MainForm_VR);

            foreach (string s in specialCategories)
            {
                if (item_x.Tag.ToString() == s) return -1;
                if (item_y.Tag.ToString() == s) return 1;
            }

            Category cat_x = item_x.Tag as Category;
            Category cat_y = item_y.Tag as Category;

            // Compare categories.
            int result;

            if (SortMode == categorySortMode.Count)
            {
                result = cat_x.Count.CompareTo(cat_y.Count);
            }
            else result = string.Compare(cat_x.Name, cat_y.Name, StringComparison.CurrentCultureIgnoreCase);

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