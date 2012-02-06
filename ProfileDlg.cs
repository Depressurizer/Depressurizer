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
using System.IO;
using System.Windows.Forms;

namespace Depressurizer {
    public partial class ProfileDlg : Form {
        public ProfileData Profile;
        private bool editMode = false;

        public bool DownloadNow {
            get {
                return chkActDownload.Checked;
            }
        }

        public bool ImportNow {
            get {
                return chkActImport.Checked;
            }
        }

        public bool SetStartup {
            get {
                return chkSetStartup.Checked;
            }
        }

        #region Init

        public ProfileDlg() {
            InitializeComponent();
        }

        public ProfileDlg( ProfileData profile )
            : this() {
            Profile = profile;
            editMode = true;
        }

        void InitializeEditMode() {
            txtFilePath.Text = Profile.FilePath;
            grpProfInfo.Enabled = false;

            txtCommunityName.Text = Profile.CommunityName;
            cmbAccountID.Text = Profile.AccountID;

            chkActDownload.Checked = false;
            chkActImport.Checked = false;
            chkSetStartup.Checked = false;

            chkAutoDownload.Checked = Profile.AutoDownload;
            chkAutoExport.Checked = Profile.AutoExport;
            chkAutoImport.Checked = Profile.AutoImport;
            chkExportDiscard.Checked = Profile.ExportDiscard;

            this.Text = "Edit Profile";
        }
        #endregion

        #region Event Handlers

        private void ProfileDlg_Load( object sender, EventArgs e ) {

            RefreshIdList();
            if( editMode ) {
                InitializeEditMode();
            } else {
                txtFilePath.Text = System.Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ) + @"\Depressurizer\Default.profile";
            }
        }

        private void cmdBrowse_Click( object sender, EventArgs e ) {
            SaveFileDialog dlg = new SaveFileDialog();

            try {
                FileInfo f = new FileInfo( txtFilePath.Text );
                dlg.InitialDirectory = f.DirectoryName;
                dlg.FileName = f.Name;
            } catch( ArgumentException ) {
            }

            dlg.DefaultExt = "profile";
            dlg.AddExtension = true;
            dlg.Filter = "Profiles (*.profile)|*.profile";
            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                txtFilePath.Text = dlg.FileName;
            }
        }

        private void cmdCancel_Click( object sender, EventArgs e ) {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void cmdOk_Click( object sender, EventArgs e ) {
            if( Apply() ) {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        #endregion

        #region Saving
        private bool Apply() {
            if( editMode ) {
                return SaveProfile();
            } else {
                return CreateProfile();
            }
        }

        private bool SaveProfile() {
            Profile.CommunityName = txtCommunityName.Text;
            Profile.AccountID = cmbAccountID.Text;
            Profile.AutoDownload = chkAutoDownload.Checked;
            Profile.AutoExport = chkAutoExport.Checked;
            Profile.AutoImport = chkAutoImport.Checked;
            Profile.ExportDiscard = chkExportDiscard.Checked;
            return true;
        }

        private bool CreateProfile() {
            FileInfo file;
            try {
                file = new FileInfo( txtFilePath.Text );
            } catch {
                MessageBox.Show( "You must enter a valid profile file path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return false;
            }

            if( !file.Directory.Exists ) {
                try {
                    file.Directory.Create();
                } catch {
                    MessageBox.Show( "Failed to create parent directory of profile file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return false;
                }
            }

            ProfileData profile = new ProfileData();
            profile.AccountID = cmbAccountID.Text;
            profile.CommunityName = txtCommunityName.Text;
            profile.AutoDownload = chkAutoDownload.Checked;
            profile.AutoExport = chkAutoExport.Checked;
            profile.AutoImport = chkAutoImport.Checked;
            profile.ExportDiscard = chkExportDiscard.Checked;
            try {
                profile.Save( file.FullName );
            } catch( ApplicationException e ) {
                MessageBox.Show( e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return false;
            }

            this.Profile = profile;
            return true;
        }
        #endregion

        #region Utility

        /// <summary>
        /// Populates the combo box with all located account IDs
        /// </summary>
        private void RefreshIdList() {
            cmbAccountID.BeginUpdate();
            cmbAccountID.Items.Clear();
            cmbAccountID.ResetText();
            cmbAccountID.Items.AddRange( GetSteamIds() );
            if( cmbAccountID.Items.Count > 0 ) {
                cmbAccountID.SelectedIndex = 0;
            }
            cmbAccountID.EndUpdate();
        }

        /// <summary>
        /// Gets a list of located account ids. Uses settings for the steam path.
        /// </summary>
        /// <returns>An array of located IDs</returns>
        private string[] GetSteamIds() {
            try {
                DirectoryInfo dir = new DirectoryInfo( DepSettings.Instance().SteamPath + "\\userdata" );
                if( dir.Exists ) {
                    DirectoryInfo[] userDirs = dir.GetDirectories();
                    string[] result = new string[userDirs.Length];
                    for( int i = 0; i < userDirs.Length; i++ ) {
                        result[i] = userDirs[i].Name;
                    }
                    return result;
                }
                return new string[0];
            } catch {
                return new string[0];
            }
        }

        #endregion
    }
}
