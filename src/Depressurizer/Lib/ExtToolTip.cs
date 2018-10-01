/*
This file is part of Depressurizer.
Copyright 2011, 2012, 2013 Steve Labbe.

Depressurizer is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Depressurizer is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Depressurizer.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Depressurizer.Lib
{
    /// <summary>
    /// ToolTip extension that allows you to bypass timers and have the tooltip always show when the mouse is over a particular control.
    /// </summary>
    public class ExtToolTip : ToolTip
    {
        private Dictionary<Control, string> bindings = new Dictionary<Control, string>();

        /// <summary>
        /// Sets the tooltip text associated with a given control. The ToolTip will always show when the mouse is over this control.
        /// </summary>
        /// <param name="c">Control to apply the tooltip to</param>
        /// <param name="s">String to show in the tooltip</param>
        public void Ext_SetToolTip(Control c, string s)
        {
            bindings[c] = s;
            c.MouseEnter += Ext_Control_MouseEnter;
            c.MouseLeave += Ext_Control_MouseLeave;
        }

        private void Ext_Control_MouseLeave(object sender, EventArgs e)
        {
            Control c = sender as Control;
            if (c != null)
            {
                Hide(c);
            }
        }

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
    }
}