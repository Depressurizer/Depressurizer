using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Depressurizer {
    public partial class AutoLoadDlg : Form {
        #region Help constants
        const string PATH_HELP = "This is the path to your Steam installation.\nThe Program will try to set this automatically.\nIf it does not, type in the correct path, or click the Browse button.";
        const string ID_HELP = "This is your numeric Steam user ID.\nThe program will try to automatically fill the box with found options.\nIf you change the Steam path, click Refresh to reload the list.";
        const string PROF_HELP = "This is your Steam profile ID.\nIn order for the program to get your information,\nyou must be connected to the internet, and your profile must not be private.";
        #endregion

        #region Fields
        bool modified = false;
        GameData gameData;
        #endregion

        public AutoLoadDlg( GameData d ) {
            gameData = d;

            InitializeComponent();
            toolTip.SetToolTip( lnkHelpPath, PATH_HELP );
            toolTip.SetToolTip( lnkHelpId, ID_HELP );
            toolTip.SetToolTip( lnkHelpProfile, PROF_HELP );
        }

        #region Event handlers
        private void AutoLoadDlg_Load( object sender, EventArgs e ) {

            //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            txtSteamPath.Text = GetSteamPath();

            RefreshIdList();

            //txtProfileName.Focus();
            txtProfileName.Select();
        }

        private void cmdBrowse_Click( object sender, EventArgs e ) {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                txtSteamPath.Text = dlg.SelectedPath;
                RefreshIdList();
            }
        }

        private void cmdRefreshIdList_Click( object sender, EventArgs e ) {
            RefreshIdList();
        }

        private void cmdCancel_Click( object sender, EventArgs e ) {
            DialogResult = modified ? DialogResult.OK : DialogResult.Cancel;
        }

        private void cmdLoad_Click( object sender, EventArgs e ) {
            Cursor = Cursors.WaitCursor;
            if( LoadData() ) {
                DialogResult = modified ? DialogResult.OK : DialogResult.Cancel;
            }
            Cursor = Cursors.Default;
        }
        #endregion

        private void RefreshIdList() {
            combUserId.BeginUpdate();
            combUserId.Items.Clear();
            combUserId.ResetText();
            combUserId.Items.AddRange( GetSteamIds() );
            if( combUserId.Items.Count > 0 ) {
                combUserId.SelectedIndex = 0;
            }
            combUserId.EndUpdate();
        }

        private string GetSteamPath() {
            try {
                string s = Registry.GetValue( @"HKEY_CURRENT_USER\Software\Valve\Steam", "steamPath", null ) as string;
                if( s == null ) s = string.Empty;
                return s.Replace( '/', '\\' );;
            } catch {
                return string.Empty;
            }
        }

        private string[] GetSteamIds() {
            try {
                DirectoryInfo dir = new DirectoryInfo( GetFixedSteamPath() + "\\userdata" );
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

        private string GetFixedSteamPath() {
            return txtSteamPath.Text.Trim().TrimEnd( new char[] { '\\' } );
        }

        private bool LoadData() {
            string steamPath = GetFixedSteamPath();
            if( !Directory.Exists( steamPath ) ) {
                MessageBox.Show( "Steam directory does not exist. Aborting.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                return false;
            }

            bool loadFile = true;
            string userPath = string.Format( "{0}\\userdata\\{1}", steamPath, combUserId.Text );
            string fullConfigFilePath = string.Format( "{0}\\7\\remote\\sharedconfig.vdf", userPath );

            if( !Directory.Exists( userPath ) ) {
                if( DialogResult.No == MessageBox.Show( string.Format( "User config directory does not exist for user {0}.\nLoad profile anyway?", combUserId.Text ), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation ) ) {
                    return false;
                }
                loadFile = false;
            } else if( !File.Exists( fullConfigFilePath ) ) {
                if( DialogResult.No == MessageBox.Show( string.Format( "Shared config file does not exist for user {0}.\nLoad profile anyway?", combUserId.Text ), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation ) ) {
                    return false;
                }
                loadFile = false;
            }

            gameData.Clear();
            gameData.SetAutoload( steamPath, combUserId.Text );
            modified = true;

            if( loadFile ) {
                try {
                    gameData.LoadSteamFile( fullConfigFilePath );
                } catch( ParseException e ) {
                    if( DialogResult.No == MessageBox.Show( string.Format( "Error parsing file: {0}\nLoad profile anyway?", e.Message ), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation ) ) {
                        return false;
                    }
                } catch( IOException e ) {
                    if( DialogResult.No == MessageBox.Show( string.Format( "Error reading file: {0}\nLoad profile anyway?", e.Message ), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation ) ) {
                        return false;
                    }
                }
            }

            try {
                int loadedGames = gameData.LoadProfile( txtProfileName.Text );
                if( loadedGames == 0 ) {
                    MessageBox.Show( "Warning: No game data found. Please make sure the profile name is spelled correctly, and that the profile is public.", "No data found", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                }
            } catch( System.Net.WebException e ) {
                MessageBox.Show( string.Format( "Error connecting to profile page: {0}\nNo profile data loaded.", e.Message ), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

            return true;
        }
    }
}
