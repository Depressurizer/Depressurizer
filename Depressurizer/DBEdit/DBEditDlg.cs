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

        string currentFilter = string.Empty;
        int currentMinId = 0, currentMaxId = ID_FILTER_MAX;

        GameList ownedList;

        public DBEditDlg( GameList owned = null ) {
            InitializeComponent();
            ownedList = owned;
        }

        const int ID_FILTER_MAX = 1000000;

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
                    UpdateStatusCount();
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
                        UpdateGameAtIndex( lstGames.SelectedIndices[0], true );
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
                        UpdateGameList( true, false );
                    }
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

        void RefreshGameList() {
            Cursor c = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            lstGames.ExtBeginUpdate();
            lstGames.Items.Clear();

            List<ListViewItem> newItems = new List<ListViewItem>( Program.GameDB.Games.Count );
            foreach( GameDBEntry g in Program.GameDB.Games.Values ) {
                if( ShouldDisplayGame( g ) ) {
                    newItems.Add( CreateListViewItem( g ) );
                }
            }

            lstGames.Items.AddRange( newItems.ToArray() );

            lstGames.ExtEndUpdate();
            this.Cursor = c;
            UpdateStatusCount();
        }

        /// <summary>
        /// Updates the game list, without adding new items. Removes games if they no longer exist in the DB or no longer match filters.
        /// </summary>
        /// <param name="selectedOnly">If true, only run on games that the user has selected.</param>
        /// <param name="updateSubItems">If true, update text based on underlying GameDBEntry. If false, just remove filtered items.</param>
        void UpdateGameList( bool selectedOnly, bool updateSubItems ) {
            int index = ( selectedOnly ? lstGames.SelectedIndices.Count : lstGames.Items.Count ) - 1;
            lstGames.ExtBeginUpdate();
            while( index >= 0 ) {
                UpdateGameAtIndex( selectedOnly ? lstGames.SelectedIndices[index] : index, updateSubItems );
                index--;
            }
            lstGames.ExtEndUpdate();
            UpdateStatusCount();
        }

        /// <summary>
        /// Adds the given game entry to the list.
        /// </summary>
        /// <param name="g">Game to add</param>
        void AddGameToList( GameDBEntry g ) {
            if( g != null ) {
                ListViewItem item = CreateListViewItem( g );
                lstGames.Items.Add( item );
            }
        }

        ListViewItem CreateListViewItem( GameDBEntry g ) {
            ListViewItem item = new ListViewItem( new string[] { 
                    g.Name,
                    g.Id.ToString(),
                    ( g.Genres!=null ) ? string.Join(",",g.Genres) : "",
                    g.AppType.ToString(),
                    ( g.LastStoreScrape == 0 ) ? "": "X",
                    ( g.LastAppInfoUpdate == 0 ) ? "" : "X",
                    ( g.ParentId <= 0 ) ? "" : g.ParentId.ToString() } );
            item.Tag = g;
            return item;
        }

        /// <summary>
        /// Updates one ListViewItem, or removes it if it should no longer be displayed
        /// </summary>
        /// <param name="index">The index of the item to update</param>
        /// <param name="updateSubItems">If true, update text to match underlying db object. If false, just remove the game if it doesn't belong.</param>
        /// <returns>True if the game is still in the list, false if it was removed.</returns>
        bool UpdateGameAtIndex( int index, bool updateSubItems ) {
            ListViewItem item = lstGames.Items[index];
            GameDBEntry g = item.Tag as GameDBEntry;
            if( g == null || !Program.GameDB.Games.ContainsKey( g.Id ) || !ShouldDisplayGame( g ) ) {
                lstGames.Items.RemoveAt( index );
                return false;
            }
            if( updateSubItems ) {
                item.SubItems[0].Text = g.Name;
                item.SubItems[1].Text = g.Id.ToString();
                item.SubItems[2].Text = ( g.Genres != null ) ? string.Join( ",", g.Genres ) : "";
                item.SubItems[3].Text = g.AppType.ToString();
                item.SubItems[4].Text = ( g.LastStoreScrape == 0 ) ? "" : "X";
                item.SubItems[5].Text = ( g.LastAppInfoUpdate == 0 ) ? "" : "X";
                item.SubItems[6].Text = ( g.ParentId <= 0 ) ? "" : g.ParentId.ToString();
            }
            return true;
        }

        /// <summary>
        /// Determine whether a particular entry should be displayed based on current filter selections
        /// </summary>
        /// <param name="g">entry to evaluate</param>
        /// <returns>True if the entry should be displayed</returns>
        bool ShouldDisplayGame( GameDBEntry g ) {
            if( g == null ) return false;

            if( chkIdRange.Checked && ( g.Id < currentMinId || g.Id > currentMaxId ) ) return false;

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

            if( currentFilter.Length > 0 && g.Name.IndexOf( currentFilter, StringComparison.CurrentCultureIgnoreCase ) == -1 ) return false;

            return true;
        }

        /// <summary>
        /// Update form elements after the selection status in the game list is modified.
        /// </summary>
        void UpdateStatusCount() {
            statSelected.Text = string.Format( GlobalStrings.DBEditDlg_SelectedDisplayedTotal, lstGames.SelectedItems.Count, lstGames.Items.Count, Program.GameDB.Games.Count );
            cmdDeleteGame.Enabled = cmdEditGame.Enabled = cmdStore.Enabled = cmdUpdateSelected.Enabled = ( lstGames.SelectedItems.Count >= 1 );
        }

        private void ApplyTextFilterChange() {
            string oldFilter = currentFilter;
            currentFilter = txtSearch.Text;

            if( currentFilter.Equals( oldFilter, StringComparison.CurrentCultureIgnoreCase ) ) return;
            else if( currentFilter.IndexOf( oldFilter, StringComparison.CurrentCultureIgnoreCase ) == -1 ) RefreshGameList();
            else UpdateGameList( false, false );
        }

        private void ApplyIdFilterChange() {
            int oldMinId = currentMinId, oldMaxId = currentMaxId;

            if( chkIdRange.Checked ) {
                currentMinId = (int)numIdRangeMin.Value;
                currentMaxId = (int)numIdRangeMax.Value;
            } else {
                currentMinId = 0;
                currentMaxId = ID_FILTER_MAX;
            }

            if( currentMinId == oldMinId && currentMaxId == oldMaxId ) return;
            else if( currentMinId < oldMinId || currentMaxId > oldMaxId ) RefreshGameList();
            else UpdateGameList( false, false );
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

            numIdRangeMax.Maximum = numIdRangeMax.Value = ID_FILTER_MAX;
            numIdRangeMin.Maximum = ID_FILTER_MAX;

            RefreshGameList();
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
            UpdateStatusCount();
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
        }

        #endregion

        #region Filters

        private void cmdSearchClear_Click( object sender, EventArgs e ) {
            txtSearch.Clear();
        }

        private void txtSearch_TextChanged( object sender, EventArgs e ) {
            ApplyTextFilterChange();
        }

        private void IdFilter_Changed( object sender, EventArgs e ) {
            ApplyIdFilterChange();
        }

        private void chkOwned_CheckedChanged( object sender, EventArgs e ) {
            RefreshGameList();
        }

        private void chkAll_CheckedChanged( object sender, EventArgs e ) {
            if( !filterSuspend ) {
                filterSuspend = true;
                if( chkTypeAll.Checked ) {
                    chkTypeDLC.Checked = chkTypeGame.Checked = chkTypeOther.Checked = chkTypeUnknown.Checked = false;
                }
                filterSuspend = false;
                RefreshGameList();
            }
        }

        private void chkType_CheckedChanged( object sender, EventArgs e ) {
            if( !filterSuspend ) {
                filterSuspend = true;

                chkTypeAll.Checked = !( chkTypeDLC.Checked || chkTypeGame.Checked || chkTypeOther.Checked || chkTypeUnknown.Checked );

                filterSuspend = false;
                RefreshGameList();
            }
        }

        private void radWeb_CheckedChanged( object sender, EventArgs e ) {
            if( ( (RadioButton)sender ).Checked == true ) {
                RefreshGameList();
            };
        }

        private void dateWeb_ValueChanged( object sender, EventArgs e ) {
            if( radWebSince.Checked ) {
                RefreshGameList();
            }
        }

        private void radApp_CheckedChanged( object sender, EventArgs e ) {
            if( ( (RadioButton)sender ).Checked == true ) {
                RefreshGameList();
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
