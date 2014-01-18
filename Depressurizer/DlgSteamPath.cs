/*
Copyright 2011, 2012, 2013 Steve Labbe.

This file is part of Depressurizer.

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
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Depressurizer {
    public partial class DlgSteamPath : Form {
        public string Path {
            get {
                return txtPath.Text.Trim().TrimEnd( new char[] { '\\' } );
            }
        }
        
        public DlgSteamPath() {
            InitializeComponent();
            txtPath.Text = GetSteamPath();
        }

        private void cmdOk_Click( object sender, EventArgs e ) {
            if( !Directory.Exists( Path ) ) {
                DialogResult res = MessageBox.Show(GlobalStrings.DlgSteamPath_ThatPathDoesNotExist, GlobalStrings.DBEditDlg_Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if( res == System.Windows.Forms.DialogResult.No ) {
                    return;
                }
            }
            this.Close();
        }

        private void cmdBrowse_Click( object sender, EventArgs e ) {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                txtPath.Text = dlg.SelectedPath;
            }
        }

        private string GetSteamPath() {
            try {
                string s = Registry.GetValue( @"HKEY_CURRENT_USER\Software\Valve\Steam", "steamPath", null ) as string;
                if( s == null ) s = string.Empty;
                return s.Replace( '/', '\\' );
            } catch {
                return string.Empty;
            }
        }
    }
}
