#region LICENSE

//     This file (DlgSteamPath.cs) is part of Depressurizer.
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
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Depressurizer
{
    public partial class DlgSteamPath : Form
    {
        #region Constructors and Destructors

        public DlgSteamPath()
        {
            InitializeComponent();
            txtPath.Text = GetSteamPath();
        }

        #endregion

        #region Public Properties

        public string Path => txtPath.Text.Trim().TrimEnd('\\');

        #endregion

        #region Methods

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                txtPath.Text = dlg.SelectedPath;
            }
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Path))
            {
                DialogResult res = MessageBox.Show(GlobalStrings.DlgSteamPath_ThatPathDoesNotExist, GlobalStrings.Gen_Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (res == DialogResult.No)
                {
                    return;
                }
            }

            Close();
        }

        private string GetSteamPath()
        {
            try
            {
                string s = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "steamPath", null) as string;
                if (s == null)
                {
                    s = string.Empty;
                }

                return s.Replace('/', '\\');
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion
    }
}
