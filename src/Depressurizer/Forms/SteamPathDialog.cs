#region LICENSE

//     This file (SteamPathDialog.cs) is part of Depressurizer.
//     Copyright (C) 2018  Martijn Vegter
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
//     along with this program.  If not, see <https://www.gnu.org/licenses/>.

#endregion

using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Depressurizer.Forms
{
    public partial class SteamPathDialog : DepressurizerDialog
    {
        #region Constructors and Destructors

        public SteamPathDialog()
        {
            InitializeComponent();

            LabelPath.Text = GetSteamPath();
        }

        #endregion

        #region Public Properties

        public string Path => LabelPath.Text;

        #endregion

        #region Methods

        private static string GetSteamPath()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Valve\Steam"))
                {
                    if (key != null)
                    {
                        object obj = key.GetValue("InstallPath");
                        if (obj is string installationPath)
                        {
                            return installationPath;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
            }

            return "";
        }

        private void ButtonBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = Path;

                DialogResult response = dialog.ShowDialog();
                if (response == DialogResult.OK)
                {
                    LabelPath.Text = dialog.SelectedPath;
                }
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Path))
            {
                DialogResult response = MessageBox.Show("The given path does not exist! Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (response == DialogResult.No)
                {
                    return;
                }
            }

            if (!File.Exists(System.IO.Path.Combine(Path, "Steam.exe")))
            {
                DialogResult response = MessageBox.Show("The Steam executable (Steam.exe) couldn't be found in the given path! Do you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (response == DialogResult.No)
                {
                    return;
                }
            }

            Close();
        }

        #endregion
    }
}