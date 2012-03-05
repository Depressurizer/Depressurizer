/*
Copyright 2011, 2012 Steve Labbe.

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
using System.Windows.Forms;

namespace Depressurizer {
    public partial class OptionsDlg : Form {
        public OptionsDlg() {
            InitializeComponent();
        }

        private void OptionsForm_Load( object sender, EventArgs e ) {
            FillFieldsFromSettings();
        }

        private void FillFieldsFromSettings() {
            DepSettings settings = DepSettings.Instance();
            txtSteamPath.Text = settings.SteamPath;
            txtDefaultProfile.Text = settings.ProfileToLoad;
            switch( settings.StartupAction ) {
                case StartupAction.Load:
                    radLoad.Checked = true;
                    break;
                case StartupAction.Create:
                    radCreate.Checked = true;
                    break;
                default:
                    radNone.Checked = true;
                    break;
            }
            chkRemoveExtraEntries.Checked = settings.RemoveExtraEntries;
            chkIgnoreDlc.Checked = settings.IgnoreDlc;
            chkFullAutocat.Checked = settings.FullAutocat;
        }

        private void SaveFieldsToSettings() {
            DepSettings settings = DepSettings.Instance();

            settings.SteamPath = txtSteamPath.Text;
            if( radLoad.Checked ) {
                settings.StartupAction = StartupAction.Load;
            } else if( radCreate.Checked ) {
                settings.StartupAction = StartupAction.Create;
            } else {
                settings.StartupAction = StartupAction.None;
            }
            settings.ProfileToLoad = txtDefaultProfile.Text;
            settings.RemoveExtraEntries = chkRemoveExtraEntries.Checked;
            settings.IgnoreDlc = chkIgnoreDlc.Checked;
            settings.FullAutocat = chkFullAutocat.Checked;
            try {
                settings.Save();
            } catch( Exception e ) {
                MessageBox.Show( "Error saving settings file: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        #region Event handlers
        private void cmdCancel_Click( object sender, EventArgs e ) {
            this.Close();
        }

        private void cmdAccept_Click( object sender, EventArgs e ) {
            SaveFieldsToSettings();
            this.Close();
        }

        private void cmdSteamPathBrowse_Click( object sender, EventArgs e ) {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                txtSteamPath.Text = dlg.SelectedPath;
            }
        }

        private void cmdDefaultProfileBrowse_Click( object sender, EventArgs e ) {
            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                txtDefaultProfile.Text = dlg.FileName;
            }
        }
        #endregion
    }
}
