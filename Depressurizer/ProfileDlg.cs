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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Xml;

namespace Depressurizer {

    public partial class ProfileDlg : Form {
        public Profile Profile;
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

        public ProfileDlg( Profile profile )
            : this() {
            Profile = profile;
            editMode = true;
        }

        void InitializeEditMode() {
            txtFilePath.Text = Profile.FilePath;
            grpProfInfo.Enabled = false;

            txtUserID.Text = Profile.AccountID64.ToString();

            chkActDownload.Checked = false;
            chkActImport.Checked = false;
            chkSetStartup.Checked = false;

            chkAutoDownload.Checked = Profile.AutoDownload;
            chkAutoExport.Checked = Profile.AutoExport;
            chkAutoImport.Checked = Profile.AutoImport;
            chkExportDiscard.Checked = Profile.ExportDiscard;
            chkOverwriteNames.Checked = Profile.OverwriteOnDownload;

            this.Text = "Edit Profile";

            chkAutoIgnore.Checked = Profile.AutoIgnore;
            chkIgnoreDlc.Checked = Profile.IgnoreDlc;
            foreach( int i in Profile.IgnoreList ) {
                lstIgnored.Items.Add( i.ToString() );
            }
            lstIgnored.Sort();
        }
        #endregion

        #region Event Handlers

        private void ProfileDlg_Load( object sender, EventArgs e ) {

            LoadShortIds();
            if( editMode ) {
                InitializeEditMode();
            } else {
                txtFilePath.Text = System.Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ) + @"\Depressurizer\Default.profile";
            }

            lstIgnored.ListViewItemSorter = new IgnoreListViewItemComparer();
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

        private void cmdIgnore_Click( object sender, EventArgs e ) {
            int id;
            if( int.TryParse( txtIgnore.Text, out id ) ) {
                lstIgnored.Items.Add( id.ToString() );
                txtIgnore.ResetText();
                lstIgnored.Sort();
            } else {
                MessageBox.Show( "Game ID must be an integer.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
        }

        private void cmdUnignore_Click( object sender, EventArgs e ) {
            while( lstIgnored.SelectedIndices.Count > 0 ) {
                lstIgnored.Items.RemoveAt( lstIgnored.SelectedIndices[0] );
            }
        }

        private void lstUsers_SelectedIndexChanged( object sender, EventArgs e ) {
            UserRecord u = (UserRecord)( lstUsers.SelectedItem );
            txtUserID.Text = Profile.DirNametoID64( u.DirName ).ToString();
        }

        private void chkManualUser_CheckedChanged( object sender, EventArgs e ) {
            txtUserID.Enabled = chkManualUser.Checked;
        }
        #endregion

        #region Saving
        private bool Apply() {
            if( editMode ) {
                if( ValidateEntries() ) {
                    SaveModifiables( Profile );
                    return true;
                }
                return false;
            } else {
                return CreateProfile();
            }
        }

        private bool CreateProfile() {
            if( !ValidateEntries() ) {
                return false;
            }

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

            Profile profile = new Profile();

            SaveModifiables( profile );

            try {
                profile.Save( file.FullName );
            } catch( ApplicationException e ) {
                MessageBox.Show( e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return false;
            }

            this.Profile = profile;
            return true;
        }

        bool ValidateEntries() {
            Int64 id;
            if( !Int64.TryParse( txtUserID.Text, out id ) ) {
                MessageBox.Show( "Account ID must be a number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return false;
            }
            return true;
        }

        void SaveModifiables( Profile p ) {
            p.AccountID64 = Int64.Parse( txtUserID.Text );

            p.AutoDownload = chkAutoDownload.Checked;
            p.AutoExport = chkAutoExport.Checked;
            p.AutoImport = chkAutoImport.Checked;
            p.ExportDiscard = chkExportDiscard.Checked;
            p.OverwriteOnDownload = chkOverwriteNames.Checked;

            p.AutoIgnore = chkAutoIgnore.Checked;
            p.IgnoreDlc = chkIgnoreDlc.Checked;

            SortedSet<int> ignoreSet = new SortedSet<int>();
            foreach( ListViewItem item in lstIgnored.Items ) {
                int id;
                if( int.TryParse( item.Text, out id ) ) {
                    ignoreSet.Add( id );
                }
            }
            p.IgnoreList = ignoreSet;

        }

        #endregion

        #region Utility

        /// <summary>
        /// Populates the combo box with all located account IDs
        /// </summary>
        private void LoadShortIds() {
            lstUsers.BeginUpdate();

            lstUsers.Items.Clear();

            string[] ids = GetSteamIds();

            if( ids.Length == 0 && !editMode ) {
                MessageBox.Show( "No account configuration information was found in your Steam installation folder. You must enter your 64-bit account ID manually.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                chkManualUser.Checked = true;
            }

            foreach( string id in ids ) {
                lstUsers.Items.Add( new UserRecord( id ) );
            }

            lstUsers.EndUpdate();
        }

        /// <summary>
        /// Gets a list of located account ids. Uses settings for the steam path.
        /// </summary>
        /// <returns>An array of located IDs</returns>
        private string[] GetSteamIds() {
            try {
                DirectoryInfo dir = new DirectoryInfo( Settings.Instance().SteamPath + "\\userdata" );
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

        public string GetDisplayName( Int64 accountId ) {
            try {
                XmlDocument doc = new XmlDocument();
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create( string.Format( "http://www.steamcommunity.com/profiles/{0}?xml=true", accountId ) );
                using( WebResponse resp = req.GetResponse() ) {
                    doc.Load( resp.GetResponseStream() );
                }
                XmlNode nameNode = doc.SelectSingleNode( "profile/steamID" );
                if( nameNode != null ) {
                    return nameNode.InnerText;
                }
            } catch( Exception e ) {
                Program.Logger.Write( Rallion.LoggerLevel.Warning, "Exception raised when trying to scrape profile name for account {0}:", accountId );
                Program.Logger.Write( Rallion.LoggerLevel.Warning, e.Message );
            }
            return null;
        }

        private void cmdUserScrape_Click( object sender, EventArgs e ) {
            for( int i = 0; i < lstUsers.Items.Count; i++ ) {
                UserRecord u = lstUsers.Items[i] as UserRecord;
                if( u != null ) {
                    string name = GetDisplayName( Profile.DirNametoID64( u.DirName ) );
                    if( name == null ) {
                        u.DisplayName = "?";
                    } else {
                        u.DisplayName = name;
                    }
                }
                lstUsers.Items.RemoveAt( i );
                lstUsers.Items.Insert( i, u );
            }
        }
    }

    public class UserRecord {
        public string DirName;
        public string DisplayName;

        public UserRecord( string dir ) {
            DirName = dir;
        }

        public override string ToString() {
            if( DisplayName == null ) {
                return DirName;
            } else {
                return String.Format( "{0} - {1}", DirName, DisplayName );
            }
        }
    }

    class IgnoreListViewItemComparer : IComparer {
        public IgnoreListViewItemComparer() { }

        public int Compare( object x, object y ) {
            int a, b;
            if( int.TryParse( ( (ListViewItem)x ).Text, out a ) && int.TryParse( ( (ListViewItem)y ).Text, out b ) ) {
                return ( a - b );
            }
            return String.Compare( ( (ListViewItem)x ).Text, ( (ListViewItem)y ).Text );
        }
    }
}
