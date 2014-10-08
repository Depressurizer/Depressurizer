using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Rallion;

namespace Depressurizer {
    public partial class DBEditDlg : Form {

        private bool UnsavedChanges { get; set; }

        MultiColumnListViewComparer listSorter = new MultiColumnListViewComparer();

        bool filterSuspend = false;
        StringBuilder statusBuilder = new StringBuilder();

        GameList ownedList;

        public DBEditDlg( GameList owned = null ) {
            InitializeComponent();
            ownedList = owned;
        }

        #region Actions

        /// <summary>
        /// Saves the database to a user-specified file.
        /// </summary>
        void SaveAs() {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "gz";
            dlg.AddExtension = true;
            dlg.CheckFileExists = false;
            dlg.Filter = GlobalStrings.DBEditDlg_DialogFilter;
            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                if( Save( dlg.FileName ) ) {
                    AddStatusMsg( GlobalStrings.DBEditDlg_FileSaved );
                } else {
                    AddStatusMsg( GlobalStrings.DBEditDlg_SaveFailed );
                }
            }
        }

        /// <summary>
        /// Saves the database to the program DB file.
        /// </summary>
        /// <returns></returns>
        bool SaveDB() {
            if( Save( "GameDB.xml.gz" ) ) {
                AddStatusMsg( GlobalStrings.DBEditDlg_DatabaseSaved );
                UnsavedChanges = false;
                return true;
            } else {
                AddStatusMsg( GlobalStrings.DBEditDlg_DatabaseSaveFailed );
                return false;
            }
        }

        /// <summary>
        /// Saves the DB to the given file.
        /// </summary>
        /// <param name="filename">Path to save to</param>
        /// <returns>True if successful</returns>
        bool Save( string filename ) {
            Cursor c = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try {
                Program.GameDB.Save( filename );
            } catch( Exception e ) {
                MessageBox.Show( string.Format( GlobalStrings.DBEditDlg_ErrorSavingFile, e.Message ) );
                this.Cursor = Cursors.Default;
                return false;
            }
            this.Cursor = c;
            return true;
        }

        /// <summary>
        /// Loads a db from a user-specified file.
        /// </summary>
        void LoadDB() {
            if( CheckForUnsaved() ) {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.DefaultExt = "gz";
                dlg.AddExtension = true;
                dlg.CheckFileExists = true;
                dlg.Filter = GlobalStrings.DBEditDlg_DialogFilter;
                DialogResult res = dlg.ShowDialog();
                if( res == System.Windows.Forms.DialogResult.OK ) {
                    this.Cursor = Cursors.WaitCursor;
                    Program.GameDB.Load( dlg.FileName );
                    RefreshGameList();
                    AddStatusMsg( GlobalStrings.DBEditDlg_FileLoaded );
                    UnsavedChanges = true;
                    this.Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Empties the entire database of all entries.
        /// </summary>
        void ClearDB() {
            if( MessageBox.Show( GlobalStrings.DBEditDlg_AreYouSureToClear, GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1 )
                == DialogResult.Yes ) {
                if( Program.GameDB.Games.Count > 0 ) {
                    UnsavedChanges = true;
                    Program.GameDB.Games.Clear();
                    AddStatusMsg( GlobalStrings.DBEditDlg_ClearedAllData );
                }
                RefreshGameList();
            }
        }

        /// <summary>
        /// Opens the steampowered page for the given game entry
        /// </summary>
        /// <param name="game">Entry to open</param>
        void VisitStorePage( GameDBEntry game ) {
            if( game != null ) {
                System.Diagnostics.Process.Start( string.Format( Properties.Resources.UrlSteamStore, game.Id ) );
            }
        }

        /// <summary>
        /// Fetches the app list provided by the steam public web API
        /// </summary>
        private void FetchList() {
            this.Cursor = Cursors.WaitCursor;

            FetchPrcDlg dlg = new FetchPrcDlg();
            DialogResult res = dlg.ShowDialog();

            if( dlg.Error != null ) {
                MessageBox.Show( string.Format( GlobalStrings.DBEditDlg_ErrorWhileUpdatingGameList, dlg.Error.Message ), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error );
                AddStatusMsg( GlobalStrings.DBEditDlg_ErrorUpdatingGameList );
            } else {
                if( res == DialogResult.Cancel || res == DialogResult.Abort ) {
                    AddStatusMsg( GlobalStrings.DBEditDlg_CanceledListUpdate );
                } else {
                    AddStatusMsg( string.Format( GlobalStrings.DBEditDlg_UpdatedGameList, dlg.Added ) );
                    UnsavedChanges = true;
                }
            }

            RefreshGameList();
            UpdateForSelectChange();
            this.Cursor = Cursors.Default;
        }

        private void UpdateFromAppInfo() {
            try {
                string path = string.Format( Properties.Resources.AppInfoPath, Settings.Instance.SteamPath );
                int updated = Program.GameDB.UpdateFromAppInfo( path );
                RefreshGameList();
                AddStatusMsg( string.Format( GlobalStrings.DBEditDlg_Status_UpdatedAppInfo, updated ) );
            } catch( Exception e ) {
                MessageBox.Show( string.Format( GlobalStrings.DBEditDlg_AppInfoUpdateFailed, e.Message ), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error );
                Program.Logger.Write( LoggerLevel.Error, GlobalStrings.DBEditDlg_Log_ExceptionAppInfo, e.ToString() );
            }
        }

        /// <summary>
        /// Shows a dialog allowing the user to add a new entry to the database.
        /// </summary>
        void AddNewGame() {
            GameDBEntryDialog dlg = new GameDBEntryDialog();
            if( dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK && dlg.Game != null ) {
                if( Program.GameDB.Games.ContainsKey( dlg.Game.Id ) ) {
                    MessageBox.Show( GlobalStrings.DBEditDlg_GameIdAlreadyExists, GlobalStrings.DBEditDlg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning );
                    AddStatusMsg( string.Format( GlobalStrings.DBEditDlg_FailedToAddGame, dlg.Game.Id ) );
                } else {
                    Program.GameDB.Games.Add( dlg.Game.Id, dlg.Game );
                    AddGameToList( dlg.Game );
                    AddStatusMsg( string.Format( GlobalStrings.DBEditDlg_AddedGame, dlg.Game.Id ) );
                    UnsavedChanges = true;
                    UpdateForSelectChange();
                }
            }
        }

        /// <summary>
        /// Shows a dialog allowing the user to modify the entry for the first selected game.
        /// </summary>
        void EditSelectedGame() {
            if( lstGames.SelectedIndices.Count > 0 ) {
                GameDBEntry game = lstGames.SelectedItems[0].Tag as GameDBEntry;
                if( game != null ) {
                    GameDBEntryDialog dlg = new GameDBEntryDialog( game );
                    DialogResult res = dlg.ShowDialog();
                    if( res == System.Windows.Forms.DialogResult.OK ) {
                        UpdateGameAtIndex( lstGames.SelectedIndices[0] );
                        AddStatusMsg( string.Format( GlobalStrings.DBEditDlg_EditedGame, game.Id ) );
                        UnsavedChanges = true;
                    }
                }
            }
        }

        /// <summary>
        /// Removes all selected games from the database.
        /// </summary>
        void DeleteSelectedGames() {
            if( lstGames.SelectedItems.Count > 0 ) {
                DialogResult res = MessageBox.Show( string.Format( GlobalStrings.DBEditDlg_AreYouSureDeleteGames, lstGames.SelectedItems.Count ),
                    GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1 );
                if( res == System.Windows.Forms.DialogResult.Yes ) {
                    int deleted = 0;
                    foreach( ListViewItem item in lstGames.SelectedItems ) {
                        GameDBEntry game = item.Tag as GameDBEntry;
                        if( game != null ) {
                            Program.GameDB.Games.Remove( game.Id );
                            deleted++;
                        }
                    }
                    AddStatusMsg( string.Format( GlobalStrings.DBEditDlg_DeletedGames, deleted ) );
                    if( deleted > 0 ) {
                        UnsavedChanges = true;
                        UpdateSelectedGames();
                    }
                    UpdateForSelectChange();
                }
            }
        }

        /// <summary>
        /// Performs a web scrape on all games without a last scrape date set.
        /// </summary>
        void ScrapeNew() {
            Cursor = Cursors.WaitCursor;

            Queue<int> gamesToScrape = new Queue<int>();

            foreach( GameDBEntry g in Program.GameDB.Games.Values ) {
                if( g.LastStoreScrape == 0 ) {
                    gamesToScrape.Enqueue( g.Id );
                }
            }

            ScrapeGames( gamesToScrape );

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Performs a web scrape on all games that are selected in the list.
        /// </summary>
        private void ScrapeSelected() {
            if( lstGames.SelectedItems.Count > 0 ) {
                Cursor = Cursors.WaitCursor;

                Queue<int> gamesToScrape = new Queue<int>();

                foreach( int index in lstGames.SelectedIndices ) {
                    GameDBEntry game = lstGames.Items[index].Tag as GameDBEntry;
                    if( game != null ) {
                        gamesToScrape.Enqueue( game.Id );
                    }
                }

                ScrapeGames( gamesToScrape );

                UpdateForSelectChange();

                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Performs a web scrape on the given games.
        /// </summary>
        /// <param name="gamesToScrape">Queue of games to scrape</param>
        private void ScrapeGames( Queue<int> gamesToScrape ) {
            if( gamesToScrape.Count > 0 ) {
                DbScrapeDlg dlg = new DbScrapeDlg( gamesToScrape );
                DialogResult res = dlg.ShowDialog();

                if( dlg.Error != null ) {
                    AddStatusMsg( GlobalStrings.DBEditDlg_ErrorUpdatingGames );
                    MessageBox.Show( string.Format( GlobalStrings.DBEditDlg_ErrorWhileUpdatingGames, dlg.Error.Message ), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error );
                }

                if( res == DialogResult.Cancel ) {
                    AddStatusMsg( GlobalStrings.DBEditDlg_UpdateCanceled );
                } else if( res == DialogResult.Abort ) {
                    AddStatusMsg( string.Format( GlobalStrings.DBEditDlg_AbortedUpdate, dlg.JobsCompleted, dlg.JobsTotal ) );
                } else {
                    AddStatusMsg( string.Format( GlobalStrings.DBEditDlg_UpdatedEntries, dlg.JobsCompleted ) );
                }
                if( dlg.JobsCompleted > 0 ) {
                    UnsavedChanges = true;
                    RefreshGameList();
                }
            } else {
                AddStatusMsg( GlobalStrings.DBEditDlg_NoGamesToScrape );
            }
        }

        #endregion

        #region UI Updaters

        /// <summary>
        /// Clears and re-fills the game list, displaying all games that should be displayed according to the current filters.
        /// </summary>
        void RefreshGameList() {
            Cursor c = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            lstGames.BeginUpdate();
            lstGames.ListViewItemSorter = null;
            lstGames.Items.Clear();

            foreach( GameDBEntry g in Program.GameDB.Games.Values ) {
                if( ShouldDisplayGame( g ) ) {
                    AddGameToList( g );
                }
            }
            lstGames.ListViewItemSorter = listSorter;
            lstGames.EndUpdate();
            this.Cursor = c;
        }

        /// <summary>
        /// Adds the given game entry to the list.
        /// </summary>
        /// <param name="g">Game to add</param>
        void AddGameToList( GameDBEntry g ) {
            if( g != null ) {
                ListViewItem item = new ListViewItem( new string[] { 
                    g.Name,
                    g.Id.ToString(),
                    ( g.Genres!=null ) ? string.Join(",",g.Genres) : "",
                    g.AppType.ToString(),
                    ( g.LastStoreScrape == 0 ) ? "": "X",
                    ( g.LastAppInfoUpdate == 0 ) ? "" : "X",
                    ( g.ParentId <= 0 ) ? "" : g.ParentId.ToString() } );
                item.Tag = g;
                lstGames.Items.Add( item );
            }
        }

        /// <summary>
        /// Updates the currently-selected ListViewItems. Removes any games that are no longer in the DB or no longer match filters.
        /// </summary>
        void UpdateSelectedGames() {
            int selIndex = 0;
            lstGames.BeginUpdate();
            lstGames.ListViewItemSorter = null;
            while( selIndex < lstGames.SelectedItems.Count ) {
                if( UpdateGameAtIndex( lstGames.SelectedIndices[selIndex] ) ) {
                    selIndex++;
                }
            }
            lstGames.ListViewItemSorter = listSorter;
            lstGames.EndUpdate();
        }

        /// <summary>
        /// Updates one ListViewItem, or removes it if it should no longer be displayed
        /// </summary>
        /// <param name="index">The index of the item to update</param>
        /// <returns>True if the game is still in the list, false if it was removed.</returns>
        bool UpdateGameAtIndex( int index ) {
            ListViewItem item = lstGames.Items[index];
            GameDBEntry g = item.Tag as GameDBEntry;
            if( g == null || !Program.GameDB.Games.ContainsKey( g.Id ) || !ShouldDisplayGame( g ) ) {
                lstGames.Items.RemoveAt( index );
                return false;
            } else {
                item.SubItems[0].Text = g.Name;
                item.SubItems[1].Text = g.Id.ToString();
                item.SubItems[2].Text = string.Join( ",", g.Genres );
                item.SubItems[3].Text = g.AppType.ToString();
                item.SubItems[4].Text = ( g.LastStoreScrape == 0 ) ? "" : "X";
                item.SubItems[5].Text = ( g.LastAppInfoUpdate == 0 ) ? "" : "X";
                item.SubItems[6].Text = ( g.ParentId <= 0 ) ? "" : g.ParentId.ToString();
                return true;
            }
        }

        /// <summary>
        /// Determine whether a particular entry should be displayed based on current filter selections
        /// </summary>
        /// <param name="g">entry to evaluate</param>
        /// <returns>True if the entry should be displayed</returns>
        bool ShouldDisplayGame( GameDBEntry g ) {
            if( g == null ) return false;

            if( ownedList != null && chkOwned.Checked == true && !ownedList.Games.ContainsKey( g.Id ) ) return false;

            if( chkTypeAll.Checked == false ) {
                switch( g.AppType ) {
                    case AppTypes.Game:
                        if( chkTypeGame.Checked == false ) return false;
                        break;
                    case AppTypes.DLC:
                        if( chkTypeDLC.Checked == false ) return false;
                        break;
                    case AppTypes.Unknown:
                        if( chkTypeUnknown.Checked == false ) return false;
                        break;
                    default:
                        if( chkTypeOther.Checked == false ) return false;
                        break;
                }
            }

            if( radWebAll.Checked == false ) {
                if( radWebNo.Checked == true && g.LastStoreScrape > 0 ) return false;
                if( radWebYes.Checked == true && g.LastStoreScrape <= 0 ) return false;
                if( radWebSince.Checked == true && g.LastStoreScrape > Utility.GetUTime( dateWeb.Value ) ) return false;
            }

            if( radAppAll.Checked == false ) {
                if( radAppNo.Checked == true && g.LastAppInfoUpdate > 0 ) return false;
                if( radAppYes.Checked == true && g.LastAppInfoUpdate <= 0 ) return false;
            }

            return true;
        }

        /// <summary>
        /// Update form elements after the selection status in the game list is modified.
        /// </summary>
        void UpdateForSelectChange() {
            statSelected.Text = string.Format( GlobalStrings.DBEditDlg_SelectedDisplayedTotal, lstGames.SelectedItems.Count, lstGames.Items.Count, Program.GameDB.Games.Count );
            cmdDeleteGame.Enabled = cmdEditGame.Enabled = cmdStore.Enabled = cmdUpdateSelected.Enabled = ( lstGames.SelectedItems.Count >= 1 );
        }

        #region Status Text
        /// <summary>
        /// Adds a status message to the buffer
        /// </summary>
        /// <param name="s">Message to add</param>
        void AddStatusMsg( string s ) {
            statusBuilder.Append( s );
            statusBuilder.Append( ' ' );
        }
        /// <summary>
        /// Clears the status buffer without displaying it.
        /// </summary>
        void ClearStatusMsg() {
            statusBuilder.Clear();
        }
        /// <summary>
        ///  Displays the status message and clears it.
        /// </summary>
        void FlushStatusMsg() {
            statusMsg.Text = statusBuilder.ToString();
            ClearStatusMsg();
        }
        #endregion

        #endregion

        #region Event Handlers

        private void MainForm_Load( object sender, EventArgs e ) {
            // Initialize list sorting
            listSorter.AddIntCol( 1 );
            listSorter.AddIntCol( 6 );
            lstGames.ListViewItemSorter = listSorter;
            lstGames.SetSortIcon( listSorter.GetSortCol(), ( listSorter.GetSortDir() == 1 ) ? SortOrder.Ascending : SortOrder.Descending );

            if( ownedList == null ) {
                chkOwned.Checked = false;
                chkOwned.Enabled = false;
            }

            RefreshGameList();
            UpdateForSelectChange();
        }

        private void DBEditDlg_FormClosing( object sender, FormClosingEventArgs e ) {
            if( !CheckForUnsaved() ) {
                e.Cancel = true;
            }
        }

        #region Menu

        private void menu_File_Load_Click( object sender, EventArgs e ) {
            ClearStatusMsg();
            LoadDB();
            UpdateForSelectChange();
            FlushStatusMsg();
        }

        private void menu_File_Save_Click( object sender, EventArgs e ) {
            ClearStatusMsg();
            SaveDB();
            FlushStatusMsg();
        }

        private void menu_File_SaveAs_Click( object sender, EventArgs e ) {
            ClearStatusMsg();
            SaveAs();
            FlushStatusMsg();
        }

        private void menu_File_Clear_Click( object sender, EventArgs e ) {
            ClearStatusMsg();
            ClearDB();
            UpdateForSelectChange();
            FlushStatusMsg();
        }

        private void menu_File_Exit_Click( object sender, EventArgs e ) {
            this.Close();
        }

        #endregion

        #region List Events

        private void lstGames_ColumnClick( object sender, ColumnClickEventArgs e ) {
            listSorter.SetSortCol( e.Column );
            lstGames.SetSortIcon( listSorter.GetSortCol(), ( listSorter.GetSortDir() == 1 ) ? SortOrder.Ascending : SortOrder.Descending );
            lstGames.Sort();
        }

        private void lstGames_KeyDown( object sender, KeyEventArgs e ) {
            switch( e.KeyCode ) {
                case Keys.A:
                    if( e.Modifiers == Keys.Control ) {
                        foreach( ListViewItem item in lstGames.Items ) {
                            item.Selected = true;
                        }
                    }
                    break;
                case Keys.Enter:
                    if( lstGames.SelectedItems.Count > 0 ) {
                        ClearStatusMsg();
                        EditSelectedGame();
                        FlushStatusMsg();
                    }
                    break;
                case Keys.N:
                    if( e.Modifiers == Keys.Control ) {
                        ClearStatusMsg();
                        AddNewGame();
                        FlushStatusMsg();
                    }
                    break;
                case Keys.Delete:
                    if( lstGames.SelectedItems.Count > 0 ) {
                        ClearStatusMsg();
                        DeleteSelectedGames();
                        FlushStatusMsg();
                    }
                    break;
            }
        }

        private void lstGames_SelectedIndexChanged( object sender, EventArgs e ) {
            UpdateForSelectChange();
        }

        #endregion

        #region Buttons

        private void cmdFetch_Click( object sender, EventArgs e ) {
            ClearStatusMsg();
            FetchList();
            FlushStatusMsg();
        }

        private void cmdUpdateAppInfo_Click( object sender, EventArgs e ) {
            ClearStatusMsg();
            UpdateFromAppInfo();
            FlushStatusMsg();
        }

        private void cmdStore_Click( object sender, EventArgs e ) {
            if( lstGames.SelectedItems.Count > 0 ) {
                VisitStorePage( lstGames.SelectedItems[0].Tag as GameDBEntry );
            }
        }

        private void cmdAddGame_Click( object sender, EventArgs e ) {
            ClearStatusMsg();
            AddNewGame();
            FlushStatusMsg();
        }

        private void cmdEditGame_Click( object sender, EventArgs e ) {
            ClearStatusMsg();
            EditSelectedGame();
            FlushStatusMsg();
        }

        private void cmdDeleteGame_Click( object sender, EventArgs e ) {
            ClearStatusMsg();
            DeleteSelectedGames();
            FlushStatusMsg();
        }

        private void cmdUpdateSelected_Click( object sender, EventArgs e ) {
            ClearStatusMsg();
            ScrapeSelected();
            FlushStatusMsg();
        }

        private void cmdUpdateUnchecked_Click( object sender, EventArgs e ) {
            ClearStatusMsg();
            ScrapeNew();
            FlushStatusMsg();
            UpdateForSelectChange();
        }

        #endregion

        #region Filters

        private void chkOwned_CheckedChanged( object sender, EventArgs e ) {
            RefreshGameList();
            UpdateForSelectChange();
        }

        private void chkAll_CheckedChanged( object sender, EventArgs e ) {
            if( !filterSuspend ) {
                filterSuspend = true;
                if( chkTypeAll.Checked ) {
                    chkTypeDLC.Checked = chkTypeGame.Checked = chkTypeOther.Checked = chkTypeUnknown.Checked = false;
                }
                filterSuspend = false;
                RefreshGameList();
                UpdateForSelectChange();
            }
        }

        private void chkType_CheckedChanged( object sender, EventArgs e ) {
            if( !filterSuspend ) {
                filterSuspend = true;

                chkTypeAll.Checked = !( chkTypeDLC.Checked || chkTypeGame.Checked || chkTypeOther.Checked || chkTypeUnknown.Checked );

                filterSuspend = false;
                RefreshGameList();
                UpdateForSelectChange();
            }
        }

        private void radWeb_CheckedChanged( object sender, EventArgs e ) {
            if( ( (RadioButton)sender ).Checked == true ) {
                RefreshGameList();
                UpdateForSelectChange();
            };
        }

        private void dateWeb_ValueChanged( object sender, EventArgs e ) {
            if( radWebSince.Checked ) {
                RefreshGameList();
                UpdateForSelectChange();
            }
        }

        private void radApp_CheckedChanged( object sender, EventArgs e ) {
            if( ( (RadioButton)sender ).Checked == true ) {
                RefreshGameList();
                UpdateForSelectChange();
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// If there are any unsaved changes, asks the user if they want to save. Also gives the user the option to cancel the calling action.
        /// </summary>
        /// <returns>True if the action should proceed, false otherwise.</returns>
        bool CheckForUnsaved() {
            if( !UnsavedChanges ) {
                return true;
            }

            DialogResult res = MessageBox.Show( GlobalStrings.DBEditDlg_UnsavedChangesSave, GlobalStrings.DBEditDlg_UnsavedChanges, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning );
            if( res == System.Windows.Forms.DialogResult.No ) {
                // Don't save, just continue
                return true;
            }
            if( res == System.Windows.Forms.DialogResult.Cancel ) {
                // Don't save, don't continue
                return false;
            }
            return SaveDB();
        }
    }
}
