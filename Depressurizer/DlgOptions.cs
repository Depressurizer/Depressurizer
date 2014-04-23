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
using System.Windows.Forms;
using Rallion;

namespace Depressurizer {
    public partial class DlgOptions : Form {

        public DlgOptions() {
            InitializeComponent();
        }

        private void OptionsForm_Load( object sender, EventArgs e ) {
            string[] levels = Enum.GetNames( typeof(LoggerLevel) );
            cmbLogLevel.Items.AddRange( levels );
            
            FillFieldsFromSettings();
        }

        private void FillFieldsFromSettings() {
            Settings settings = Settings.Instance();
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
            switch (settings.ListSource) {
                case GameListSource.XmlPreferred:
                    cmbDatSrc.SelectedIndex = 0;
                    break;
                case GameListSource.XmlOnly:
                    cmbDatSrc.SelectedIndex = 1;
                    break;
                case GameListSource.WebsiteOnly:
                    cmbDatSrc.SelectedIndex = 2;
                    break;
            }
            
            chkRemoveExtraEntries.Checked = settings.RemoveExtraEntries;
            chkIgnoreDlc.Checked = settings.IgnoreDlc;
            chkFullAutocat.Checked = settings.FullAutocat;

            //jpodadera. Non-Steam games
            chkIgnoreExternal.Checked = settings.IgnoreExternal;

            cmbLogLevel.SelectedIndex = (int)settings.LogLevel;
            numLogSize.Value = settings.LogSize;
            numLogBackup.Value = settings.LogBackups;

            switch (settings.UserLang)
            {
                case UserLanguage.en:
                    cmbLanguage.SelectedIndex = 1;
                    break;
                case UserLanguage.es:
                    cmbLanguage.SelectedIndex = 2;
                    break;
                default:
                    cmbLanguage.SelectedIndex = 0;
                    break;
            }
        }

        private void SaveFieldsToSettings() {
            Settings settings = Settings.Instance();

            settings.SteamPath = txtSteamPath.Text;
            if( radLoad.Checked ) {
                settings.StartupAction = StartupAction.Load;
            } else if( radCreate.Checked ) {
                settings.StartupAction = StartupAction.Create;
            } else {
                settings.StartupAction = StartupAction.None;
            }

            switch( cmbDatSrc.SelectedIndex ) {
                case 0:
                    settings.ListSource = GameListSource.XmlPreferred;
                    break;
                case 1:
                    settings.ListSource = GameListSource.XmlOnly;
                    break;
                case 2:
                    settings.ListSource = GameListSource.WebsiteOnly;
                    break;
            }

            settings.ProfileToLoad = txtDefaultProfile.Text;
            settings.RemoveExtraEntries = chkRemoveExtraEntries.Checked;
            settings.IgnoreDlc = chkIgnoreDlc.Checked;
            settings.FullAutocat = chkFullAutocat.Checked;
            //jpodadera. Non-Steam games
            settings.IgnoreExternal = chkIgnoreExternal.Checked;

            settings.LogLevel = (LoggerLevel)cmbLogLevel.SelectedIndex;
            settings.LogSize = (int)numLogSize.Value;
            settings.LogBackups = (int)numLogBackup.Value;

            switch (cmbLanguage.SelectedIndex)
            {
                case 0:
                    settings.UserLang = UserLanguage.windows;
                    break;
                case 1:
                    settings.UserLang = UserLanguage.en;
                    break;
                case 2:
                    settings.UserLang = UserLanguage.es;
                    break;
            }

            try {
                settings.Save();
            } catch( Exception e ) {
                MessageBox.Show(GlobalStrings.DlgOptions_ErrorSavingSettingsFile + e.Message, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
