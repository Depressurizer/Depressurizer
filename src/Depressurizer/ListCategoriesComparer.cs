using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Models;
using Depressurizer.Properties;

namespace Depressurizer
{
    public class ListCategoriesComparer : IComparer
    {
        #region Static Fields

        private static readonly ReadOnlyCollection<string> SpecialCategories = new ReadOnlyCollection<string>(new List<string>
        {
            Resources.SpecialCategoryAll,
            Resources.SpecialCategoryGames,
            Resources.SpecialCategorySoftware,
            Resources.SpecialCategoryUncategorized,
            Resources.SpecialCategoryHidden,
            Resources.SpecialCategoryFavorite,
            Resources.SpecialCategoryVR
        });

        #endregion

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

            bool isSpecial1 = SpecialCategories.Contains(listViewItem1.Tag.ToString());
            bool isSpecial2 = SpecialCategories.Contains(listViewItem2.Tag.ToString());

            if (isSpecial1 && isSpecial2)
            {
                return string.CompareOrdinal(listViewItem1.Tag.ToString(), listViewItem2.Tag.ToString());
            }

            if (isSpecial1 || isSpecial2)
            {
                if (isSpecial1)
                {
                    return -1;
                }

                return 1;
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
