using System.Collections;
using System.Windows.Forms;

namespace Depressurizer
{
    internal class IgnoreListViewItemComparer : IComparer
    {
        #region Public Methods and Operators

        public int Compare(object x, object y)
        {
            if (int.TryParse(((ListViewItem) x).Text, out int a) && int.TryParse(((ListViewItem) y).Text, out int b))
            {
                return a - b;
            }

            return string.Compare(((ListViewItem) x).Text, ((ListViewItem) y).Text);
        }

        #endregion
    }
}
