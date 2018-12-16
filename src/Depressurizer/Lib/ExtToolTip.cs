using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Depressurizer.Lib
{
    /// <summary>
    ///     ToolTip extension that allows you to bypass timers and have the tooltip always show when the mouse is over a
    ///     particular control.
    /// </summary>
    public class ExtToolTip : ToolTip
    {
        #region Fields

        private readonly Dictionary<Control, string> bindings = new Dictionary<Control, string>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Sets the tooltip text associated with a given control. The ToolTip will always show when the mouse is over this
        ///     control.
        /// </summary>
        /// <param name="c">Control to apply the tooltip to</param>
        /// <param name="s">String to show in the tooltip</param>
        public void Ext_SetToolTip(Control c, string s)
        {
            bindings[c] = s;
            c.MouseEnter += Ext_Control_MouseEnter;
            c.MouseLeave += Ext_Control_MouseLeave;
        }

        #endregion

        #region Methods

        private void Ext_Control_MouseEnter(object sender, EventArgs e)
        {
            Control c = sender as Control;
            if (c != null)
            {
                if (bindings.ContainsKey(c))
                {
                    string s = bindings[c];
                    if (s != null)
                    {
                        Show(s, c, c.Width, c.Height);
                    }
                }
            }
        }

        private void Ext_Control_MouseLeave(object sender, EventArgs e)
        {
            Control c = sender as Control;
            if (c != null)
            {
                Hide(c);
            }
        }

        #endregion
    }
}
