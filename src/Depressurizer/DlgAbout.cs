#region LICENSE

//     This file (DlgAbout.cs) is part of Depressurizer.
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
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Depressurizer.Properties;

namespace Depressurizer
{
    public partial class DlgAbout : Form
    {
        #region Constructors and Destructors

        public DlgAbout()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void DlgAbout_Load(object sender, EventArgs e)
        {
            lblVersion.Text += Assembly.GetExecutingAssembly().GetName().Version.ToString();

            int oldLen = lnkHomepage.Text.Length;
            lnkHomepage.Text += Resources.DepressurizerHomepage;
            lnkHomepage.LinkArea = new LinkArea(oldLen, lnkHomepage.Text.Length - oldLen);
        }

        private void lnkHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Resources.DepressurizerHomepage);
        }

        private void lnkLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.gnu.org/licenses/");
        }

        private void lnkNDesk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.novell.com");
        }

        #endregion
    }
}
