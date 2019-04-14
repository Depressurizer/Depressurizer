using System;
using System.Collections;
using System.Windows.Forms;

namespace Depressurizer.Helpers
{
    public class ToolStripItemComparer : IComparer
    {
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

            ToolStripItem oItem1 = (ToolStripItem) x;
            ToolStripItem oItem2 = (ToolStripItem) y;

            return string.Compare(oItem1.Text, oItem2.Text, StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}
