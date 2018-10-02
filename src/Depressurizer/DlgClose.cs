#region LICENSE

//     This file (DlgClose.cs) is part of Depressurizer.
//     Copyright (C) 2011 Steve Labbe
//     Copyright (C) 2017 Theodoros Dimos
//     Copyright (C) 2017 Martijn Vegter
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class DlgClose : Form
    {
        #region Fields

        public bool Export;

        #endregion

        #region Constructors and Destructors

        public DlgClose(string message, string title, Image picture, bool cancel, bool exportSteam)
        {
            InitializeComponent();

            lblMessage.Text = message;
            Text = title;
            pictureBox1.Image = picture;
            btnCancel.Visible = cancel;
            chkSaveSteam.Checked = exportSteam;
        }

        #endregion

        #region Methods

        private void chkSaveSteam_CheckedChanged(object sender, EventArgs e)
        {
            Export = chkSaveSteam.Checked;
        }

        #endregion
    }
}
