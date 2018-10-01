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
using System.Drawing;
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class DlgClose : Form
    {
        public bool Export;

        public DlgClose(string message, string title, Image picture, bool cancel, bool exportSteam)
        {
            InitializeComponent();

            lblMessage.Text = message;
            Text = title;
            pictureBox1.Image = picture;
            btnCancel.Visible = cancel;
            chkSaveSteam.Checked = exportSteam;
        }

        private void chkSaveSteam_CheckedChanged(object sender, EventArgs e)
        {
            Export = chkSaveSteam.Checked;
        }
    }
}
