/*
This file is part of Depressurizer.
Copyright 2011 - 2014 Steve Labbe.

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
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Depressurizer.Properties;

namespace Depressurizer
{
    public partial class DlgAbout : Form
    {
        public DlgAbout()
        {
            InitializeComponent();
        }

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
    }
}
