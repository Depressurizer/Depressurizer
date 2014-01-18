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

    public partial class DlgProfile : Form {
        public Profile Profile;
        private bool editMode = false;

        private ThreadLocker currentThreadLock = new ThreadLocker();
        private int currentThreadCount = 0;

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

        delegate void UpdateDelegate( int i, string s );
        delegate void SimpleDelegate();

        #region Init

        public DlgProfile() {
            InitializeComponent();
        }

        public DlgProfile( Profile profile )
            : this() {
            Profile = profile;
            editMode = true;
        }

        void InitializeEditMode() {
            txtFilePath.Text = Profile.FilePath;
            grpProfInfo.Enabled = false;

            chkActDownload.Checked = false;
            chkActImport.Checked = false;
            chkSetStartup.Checked = false;

            chkAutoDownload.Checked = Profile.AutoDownload;
            chkAutoExport.Checked = Profile.AutoExport;
            chkAutoImport.Checked = Profile.AutoImport;
            chkExportDiscard.Checked = Profile.ExportDiscard;
            chkOverwriteNames.Checked = Profile.OverwriteOnDownload;

            this.Text = GlobalStrings.DlgProfile_EditProfile;

            chkAutoIgnore.Checked = Profile.AutoIgnore;
            chkIgnoreDlc.Checked = Profile.IgnoreDlc;
            foreach( int i in Profile.IgnoreList ) {
                lstIgnored.Items.Add( i.ToString() );
            }
            lstIgnored.Sort();


            bool found = SelectUserInList( Profile.SteamID64 );
            if( found ) {
                radSelUserFromList.Checked = true;
            } else {
                radSelUserByID.Checked = true;
                txtUserID.Text = Profile.SteamID64.ToString();
            }
        }

        private bool SelectUserInList( Int64 accountId ) {
            string profDirName = Profile.ID64toDirName( accountId );

            for( int i = 0; i < lstUsers.Items.Count; i++ ) {
                UserRecord r = lstUsers.Items[i] as UserRecord;
                if( r != null && r.DirName == profDirName ) {
                    lstUsers.SelectedIndex = i;
                    return true;
                }
            }

            return false;
        }

        private bool SelectUserInList( string accountId ) {
            Int64 val;
            if( Int64.TryParse( accountId, out val ) ) {
                return SelectUserInList( val );
            }
            return false;
        }

        #endregion

        #region Event Handlers

        private void ProfileDlg_Load( object sender, EventArgs e ) {

            LoadShortIds();
            if( editMode ) {
                InitializeEditMode();
            } else {
                txtFilePath.Text = System.Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ) + @"\Depressurizer\Default.profile";

                if( lstUsers.Items.Count == 0 ) {
                    MessageBox.Show(GlobalStrings.DlgProfile_NoAccountConfiguration, GlobalStrings.DBEditDlg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    radSelUserByURL.Checked = true;
                } else {
                    radSelUserFromList.Checked = true;
                }
                StartThreadedNameUpdate();
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
            dlg.Filter = GlobalStrings.DlgProfile_Filter;
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
                MessageBox.Show(GlobalStrings.DlgGameDBEntry_IDMustBeInteger, GlobalStrings.DBEditDlg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmdUnignore_Click( object sender, EventArgs e ) {
            while( lstIgnored.SelectedIndices.Count > 0 ) {
                lstIgnored.Items.RemoveAt( lstIgnored.SelectedIndices[0] );
            }
        }

        private void cmdUserUpdate_Click( object sender, EventArgs e ) {
            StartThreadedNameUpdate();
        }

        private void cmdUserUpdateCancel_Click( object sender, EventArgs e ) {
            if( currentThreadCount > 0 ) {
                lock( currentThreadLock ) {
                    currentThreadLock.Aborted = true;
                }
                SetUpdateInterfaceStopping();
            }
        }

        private void lstUsers_SelectedIndexChanged( object sender, EventArgs e ) {
            UserRecord u = lstUsers.SelectedItem as UserRecord;
            if( u != null ) {
                txtUserID.Text = Profile.DirNametoID64( u.DirName ).ToString();
            }
        }

        private void txtUserID_TextChanged( object sender, EventArgs e ) {
            //    if( !skipUserClear ) {
            //        lstUsers.ClearSelected();
            //    }
        }

        private void ProfileDlg_FormClosing( object sender, FormClosingEventArgs e ) {
            lock( currentThreadLock ) {
                currentThreadLock.Aborted = true;
            }
        }

        #endregion

        #region Saving
        private bool Apply() {
            if( radSelUserByURL.Checked ) {
                CDlgGetSteamID dlg = new CDlgGetSteamID( txtUserUrl.Text );
                dlg.ShowDialog();

                if( dlg.DialogResult == System.Windows.Forms.DialogResult.Cancel ) {
                    return false;
                }

                if( dlg.Success == false || dlg.SteamID == 0 ) {
                    MessageBox.Show(this, GlobalStrings.DlgProfile_CouldNotFindSteamProfile, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                txtUserID.Text = dlg.SteamID.ToString();
            }
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
                MessageBox.Show(GlobalStrings.DlgProfile_YouMustEnterValidProfilePath, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if( !file.Directory.Exists ) {
                try {
                    file.Directory.Create();
                } catch {
                    MessageBox.Show(GlobalStrings.DlgProfile_FailedToCreateParentDirectory, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            Profile profile = new Profile();

            SaveModifiables( profile );

            try {
                profile.Save( file.FullName );
            } catch( ApplicationException e ) {
                MessageBox.Show(e.Message, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            this.Profile = profile;
            return true;
        }

        bool ValidateEntries() {
            Int64 id;
            if( !Int64.TryParse( txtUserID.Text, out id ) ) {
                MessageBox.Show(GlobalStrings.DlgProfile_AccountIDMustBeNumber, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        void SaveModifiables( Profile p ) {
            p.SteamID64 = Int64.Parse( txtUserID.Text );

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

        #region Display name update
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
                Program.Logger.Write(Rallion.LoggerLevel.Warning, GlobalStrings.DlgProfile_ExceptionRaisedWhenTryingScrapeProfileName, accountId);
                Program.Logger.Write( Rallion.LoggerLevel.Warning, e.Message );
            }
            return null;
        }

        private void StartThreadedNameUpdate() {
            if( currentThreadCount > 0 ) return;

            int maxThreads = 1;

            Queue<UpdateJob> q = new Queue<UpdateJob>();
            for( int i = 0; i < lstUsers.Items.Count; i++ ) {
                UserRecord r = lstUsers.Items[i] as UserRecord;
                if( r != null ) {
                    q.Enqueue( new UpdateJob( i, r.DirName ) );
                }
            }

            int threads = ( maxThreads > q.Count ) ? maxThreads : q.Count;

            if( threads > 0 ) {
                currentThreadLock = new ThreadLocker();
                SetUpdateInterfaceRunning();
                for( int i = 0; i < threads; i++ ) {
                    Thread t = new Thread( this.NameUpdateThread );
                    currentThreadCount++;
                    t.Start( new UpdateData( q, currentThreadLock ) );
                }
            }
        }

        private void NameUpdateThread( object d ) {
            UpdateData data = (UpdateData)d;
            bool abort = false;
            do {
                UpdateJob job = null;
                lock( data.jobs ) {
                    if( data.jobs.Count > 0 ) {
                        job = data.jobs.Dequeue();
                    } else {
                        abort = true;
                    }
                }
                if( job != null ) {
                    string name = GetDisplayName( Profile.DirNametoID64( job.dir ) );

                    lock( data.tLock ) {
                        if( data.tLock.Aborted ) abort = true;
                        else {
                            UpdateDisplayNameInList( job.index, name );
                        }
                    }
                }
            } while( !abort );
            OnNameUpdateThreadTerminate();
        }

        private void UpdateDisplayNameInList( int index, string name ) {
            if( this.InvokeRequired ) {
                Invoke( new UpdateDelegate( UpdateDisplayNameInList ), new object[] { index, name } );
            } else {
                
                UserRecord u = lstUsers.Items[index] as UserRecord;
                if( u != null ) {
                    bool selected = lstUsers.SelectedIndex == index;
                    if( name == null ) {
                        name = "?";
                    }
                    u.DisplayName = name;

                    lstUsers.Items.RemoveAt( index );
                    lstUsers.Items.Insert( index, u );
                    if( selected ) lstUsers.SelectedIndex = index;
                }
            }
        }

        private void OnNameUpdateThreadTerminate() {
            if( InvokeRequired ) {
                Invoke( new SimpleDelegate( OnNameUpdateThreadTerminate ) );
            } else {
                currentThreadCount--;
                if( currentThreadCount == 0 ) {
                    SetUpdateInterfaceNormal();
                }
            }
        }

        private void SetUpdateInterfaceNormal() {
            cmdUserUpdate.Enabled = true;
            cmdUserUpdateCancel.Enabled = false;
            lblUserStatus.Text = GlobalStrings.DlgProfile_ClickUpdateToDisplayNames;
        }

        private void SetUpdateInterfaceRunning() {
            cmdUserUpdate.Enabled = false;
            cmdUserUpdateCancel.Enabled = true;
            lblUserStatus.Text = GlobalStrings.DlgProfile_UpdatingNames;
        }

        private void SetUpdateInterfaceStopping() {
            cmdUserUpdate.Enabled = false;
            cmdUserUpdateCancel.Enabled = false;
            lblUserStatus.Text = GlobalStrings.DlgProfile_Cancelling;
        }

        /*
        private void NonthreadedNameUpdate() {
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
        */
        #endregion

        #region Utility structures

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

        public class ThreadLocker {
            private bool _abort = false;

            public bool Aborted {
                get {
                    return _abort;
                }
                set {
                    _abort = value;
                }
            }
        }

        public class UpdateJob {
            public int index;
            public string dir;

            public UpdateJob( int i, string d ) {
                index = i; dir = d;
            }
        }

        public class UpdateData {
            public Queue<UpdateJob> jobs;
            public ThreadLocker tLock;

            public UpdateData( Queue<UpdateJob> q, ThreadLocker l ) {
                jobs = q; tLock = l;
            }
        }

        #endregion

        private void radSelUser_CheckedChanged( object sender, EventArgs e ) {
            lstUsers.Enabled = radSelUserFromList.Checked;
            lstUsers.SelectedItem = null;
            txtUserID.Enabled = radSelUserByID.Checked;
            txtUserUrl.Enabled = radSelUserByURL.Checked;

            if( radSelUserFromList.Checked ) {
                SelectUserInList( txtUserID.Text );
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
