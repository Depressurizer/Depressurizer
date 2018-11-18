using System;
using System.Collections;
using System.Windows.Forms;

namespace Depressurizer
{
    public class ToolStripItemComparer : IComparer
    {
        #region Public Methods and Operators

        /// <inheritdoc />
        public int Compare(object x, object y)
        {
            ToolStripItem oItem1 = (ToolStripItem) x;
            ToolStripItem oItem2 = (ToolStripItem) y;
            return string.Compare(oItem1.Text, oItem2.Text, StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}
