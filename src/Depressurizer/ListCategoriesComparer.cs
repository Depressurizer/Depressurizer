using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Models;
using Depressurizer.Properties;

namespace Depressurizer
{
    public class ListCategoriesComparer : IComparer
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

            List<string> specialCategories = new List<string>
            {
                Resources.SpecialCategoryAll,
                Resources.SpecialCategoryGames,
                Resources.SpecialCategorySoftware,
                Resources.SpecialCategoryUncategorized,
                Resources.SpecialCategoryHidden,
                Resources.SpecialCategoryFavorite,
                Resources.SpecialCategoryVR
            };

            foreach (string category in specialCategories)
            {
                if (listViewItem1.Tag.ToString() == category)
                {
                    return -1;
                }

                if (listViewItem2.Tag.ToString() == category)
                {
                    return 1;
                }
            }

            Category category1 = listViewItem1.Tag as Category;
            Category category2 = listViewItem2.Tag as Category;

            if (category1 == null)
            {
                if (category2 == null)
                {
                    return 0;
                }

                return -1;
            }

            if (category2 == null)
            {
                return 1;
            }

            int result = SortMode == CategorySortMode.Count ? category1.Count.CompareTo(category2.Count) : string.Compare(category1.Name, category2.Name, StringComparison.OrdinalIgnoreCase);
            if (SortOrder == SortOrder.Ascending)
            {
                return result;
            }

            return -result;
        }

        #endregion
    }
}
