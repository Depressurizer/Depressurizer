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
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Depressurizer {
    public partial class DBEditDlg : Form {

        bool _unsavedChanges;
        bool UnsavedChanges {
            get {
                return _unsavedChanges;
            }
            set {
                _unsavedChanges = value;
            }
        }
        MultiColumnListViewComparer listSorter = new MultiColumnListViewComparer();

        bool filterSuspend = false;
        StringBuilder statusBuilder = new StringBuilder();

        public DBEditDlg() {
            InitializeComponent();
            ( (ToolStripDropDownMenu)menu_File.DropDown ).ShowImageMargin = false;
        }

        #region Actions

        void SaveAs() {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "xml";
            dlg.AddExtension = true;
            dlg.CheckFileExists = true;
            dlg.Filter = "XML Files (*.xml)|*.xml|All Files|*.*";
            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                if( Save( dlg.FileName ) ) {
                    AddStatusMsg( "File saved." );
                } else {
                    AddStatusMsg( "Save failed." );
                }
            }
        }

        bool SaveDB() {
            if( Save( "GameDB.xml" ) ) {
                AddStatusMsg( "Database saved." );
                UnsavedChanges = false;
                return true;
            } else {
                AddStatusMsg( "Database save failed." );
                return false;
            }
        }

        bool Save( string filename ) {
            this.Cursor = Cursors.WaitCursor;
            try {
                Program.GameDB.SaveToXml( filename );
            } catch( Exception e ) {
                MessageBox.Show( string.Format( "Error saving file: \n\n{0}", e.Message ) );
                this.Cursor = Cursors.Default;
                return false;
            }
            this.Cursor = Cursors.Default;
            return true;
        }

        void LoadGames() {
            if( CheckForUnsaved() ) {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.DefaultExt = "xml";
                dlg.AddExtension = true;
                dlg.CheckFileExists = true;
                dlg.Filter = "XML Files (*.xml)|*.xml|All Files|*.*";
                DialogResult res = dlg.ShowDialog();
                if( res == System.Windows.Forms.DialogResult.OK ) {
                    this.Cursor = Cursors.WaitCursor;
                    Program.GameDB.LoadFromXml( dlg.FileName );
                    RefreshGameList();
                    AddStatusMsg( "File loaded." );
                    UnsavedChanges = true;
                    this.Cursor = Cursors.Default;
                }
            }
        }

        void ClearList() {
            if( MessageBox.Show( "Are you sure you want to clear all data?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1 )
                == DialogResult.Yes ) {
                if( Program.GameDB.Games.Count > 0 ) {
                    UnsavedChanges = true;
                    Program.GameDB.Games.Clear();
                    AddStatusMsg( "Cleared all data." );
                }
                RefreshGameList();
            }
        }

        void VisitStorePage( GameDBEntry game ) {
            if( game != null ) {
                System.Diagnostics.Process.Start( string.Format( "http://store.steampowered.com/app/{0}/", game.Id ) );
            }
        }

        private void FetchList() {
            this.Cursor = Cursors.WaitCursor;

            FetchPrcDlg dlg = new FetchPrcDlg();
            DialogResult res = dlg.ShowDialog();

            if( dlg.Error != null ) {
                MessageBox.Show( string.Format( "An error occurred while updating the game list:\n\n{0}", dlg.Error.Message ), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                AddStatusMsg( "Error updating game list." );
            } else {
                if( res == System.Windows.Forms.DialogResult.Abort ) {
                    AddStatusMsg( "Aborted list update." );
                } else {
                    AddStatusMsg( string.Format( "Updated game list, added {0} games.", dlg.Added ) );
                    UnsavedChanges = true;
                }
            }

            RefreshGameList();
            UpdateForSelectChange();
            this.Cursor = Cursors.Default;
        }

        void AddNewGame() {
            GameDBEntryDialog dlg = new GameDBEntryDialog();
            if( dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK && dlg.Game != null ) {
                if( Program.GameDB.Games.ContainsKey( dlg.Game.Id ) ) {
                    MessageBox.Show( "Game with specified ID already exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                    AddStatusMsg( string.Format( "Failed to add game with ID {0}.", dlg.Game.Id ) );
                } else {
                    Program.GameDB.Games.Add( dlg.Game.Id, dlg.Game );
                    AddGameToList( dlg.Game );
                    AddStatusMsg( string.Format( "Added game with ID {0}.", dlg.Game.Id ) );
                    UnsavedChanges = true;
                    UpdateForSelectChange();
                }
            }
        }

        void EditSelectedGame() {
            if( lstGames.SelectedIndices.Count > 0 ) {
                GameDBEntry game = lstGames.SelectedItems[0].Tag as GameDBEntry;
                if( game != null ) {
                    GameDBEntryDialog dlg = new GameDBEntryDialog( game );
                    DialogResult res = dlg.ShowDialog();
                    if( res == System.Windows.Forms.DialogResult.OK ) {
                        UpdateGameAtIndex( lstGames.SelectedIndices[0] );
                        AddStatusMsg( string.Format( "Edited game with ID {0}", game.Id ) );
                        UnsavedChanges = true;
                    }
                }
            }
        }

        void DeleteSelectedGames() {
            if( lstGames.SelectedItems.Count > 0 ) {
                DialogResult res = MessageBox.Show( string.Format( "Delete {0} games from the database?", lstGames.SelectedItems.Count ),
                    "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1 );
                if( res == System.Windows.Forms.DialogResult.Yes ) {
                    int deleted = 0;
                    foreach( ListViewItem item in lstGames.SelectedItems ) {
                        GameDBEntry game = item.Tag as GameDBEntry;
                        if( game != null ) {
                            Program.GameDB.Games.Remove( game.Id );
                            deleted++;
                        }
                    }
                    AddStatusMsg( string.Format( "Deleted {0} games.", deleted ) );
                    if( deleted > 0 ) {
                        UnsavedChanges = true;
                        UpdateSelectedGames();
                    }
                    UpdateForSelectChange();
                }
            }
        }

        void ScrapeGamesOfType( AppType type ) {
            Cursor = Cursors.WaitCursor;

            Queue<int> gamesToScrape = new Queue<int>();

            foreach( GameDBEntry g in Program.GameDB.Games.Values ) {
                if( g.Type == type ) {
                    gamesToScrape.Enqueue( g.Id );
                }
            }

            ScrapeGames( gamesToScrape );

            Cursor = Cursors.Default;
        }

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

        private void ScrapeGames( Queue<int> gamesToScrape ) {
            if( gamesToScrape.Count > 0 ) {
                DbScrapeDlg dlg = new DbScrapeDlg( gamesToScrape );
                DialogResult res = dlg.ShowDialog();

                if( res == DialogResult.Abort ) {
                    AddStatusMsg( string.Format( "Aborted. Updated {0} / {1} entries.", dlg.JobsCompleted, dlg.JobsTotal ) );
                } else {
                    AddStatusMsg( string.Format( "Updated {0} entries.", dlg.JobsCompleted ) );
                }
                UnsavedChanges = true;
                RefreshGameList();
            } else {
                AddStatusMsg( "No games to scrape." );
            }
        }

        #endregion

        #region UI Updaters

        void RefreshGameList() {
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

        }

        void AddGameToList( GameDBEntry g ) {
            ListViewItem item = new ListViewItem( new string[] { g.Name, g.Id.ToString(), g.Genre, g.Type.ToString() } );
            item.Tag = g;
            lstGames.Items.Add( item );
        }

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

        bool UpdateGameAtIndex( int index ) {
            ListViewItem item = lstGames.Items[index];
            GameDBEntry game = item.Tag as GameDBEntry;
            if( game == null || !Program.GameDB.Games.ContainsKey( game.Id ) || !ShouldDisplayGame( game ) ) {
                lstGames.Items.RemoveAt( index );
                return false;
            } else {
                item.SubItems[0].Text = game.Name;
                item.SubItems[1].Text = game.Id.ToString();
                item.SubItems[2].Text = game.Genre;
                item.SubItems[3].Text = game.Type.ToString();
                return true;
            }
        }

        bool ShouldDisplayGame( GameDBEntry g ) {
            return
                chkAll.Checked ||
                ( g.Type == AppType.DLC && chkDLC.Checked ) ||
                ( g.Type == AppType.WebError && chkWebError.Checked ) ||
                ( g.Type == AppType.SiteError && chkSiteError.Checked ) ||
                ( g.Type == AppType.Game && chkGame.Checked ) ||
                ( g.Type == AppType.IdRedirect && chkRedirect.Checked ) ||
                ( g.Type == AppType.NonApp && chkNonApp.Checked ) ||
                ( g.Type == AppType.NotFound && chkNotFound.Checked ) ||
                ( g.Type == AppType.Unknown && chkNew.Checked ) ||
                ( g.Type == AppType.New && chkNew.Checked );
        }

        void UpdateForSelectChange() {
            statSelected.Text = string.Format( "{0} selected / {1} displayed / {2} total", lstGames.SelectedItems.Count, lstGames.Items.Count, Program.GameDB.Games.Count );
            cmdDeleteGame.Enabled = cmdEditGame.Enabled = cmdStore.Enabled = cmdUpdateSelected.Enabled = ( lstGames.SelectedItems.Count >= 1 );
        }

        void AddStatusMsg( string s ) {
            statusBuilder.Append( s );
            statusBuilder.Append( ' ' );
        }

        void ClearStatusMsg() {
            statusBuilder.Clear();
        }

        void FlushStatusMsg() {
            statusMsg.Text = statusBuilder.ToString();
        }
        #endregion

        #region Event Handlers

        private void MainForm_Load( object sender, EventArgs e ) {
            listSorter.AddIntCol( 1 );
            lstGames.ListViewItemSorter = listSorter;
            RefreshGameList();
            UpdateForSelectChange();
        }

        private void menu_File_Load_Click( object sender, EventArgs e ) {
            ClearStatusMsg();
            LoadGames();
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
            ClearList();
            UpdateForSelectChange();
            FlushStatusMsg();
        }

        private void menu_File_Exit_Click( object sender, EventArgs e ) {
            this.Close();
        }

        private void lstGames_ColumnClick( object sender, ColumnClickEventArgs e ) {
            listSorter.ColClick( e.Column );
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

        private void cmdFetch_Click( object sender, EventArgs e ) {
            ClearStatusMsg();
            FetchList();
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
            ScrapeGamesOfType( AppType.New );
            FlushStatusMsg();
            UpdateForSelectChange();
        }

        private void chkAll_CheckedChanged( object sender, EventArgs e ) {
            if( !filterSuspend ) {
                filterSuspend = true;
                if( chkAll.Checked ) {
                    chkDLC.Checked = chkSiteError.Checked = chkWebError.Checked = chkGame.Checked = chkNonApp.Checked
                        = chkNotFound.Checked = chkRedirect.Checked = chkNew.Checked = chkUnknown.Checked = false;
                }
                filterSuspend = false;
                RefreshGameList();
                UpdateForSelectChange();
            }
        }

        private void chkAny_CheckedChanged( object sender, EventArgs e ) {
            if( !filterSuspend ) {
                filterSuspend = true;
                if( ( (CheckBox)sender ).Checked ) {
                    chkAll.Checked = false;
                }
                filterSuspend = false;
                RefreshGameList();
                UpdateForSelectChange();
            }
        }

        private void lstGames_SelectedIndexChanged( object sender, EventArgs e ) {
            UpdateForSelectChange();
        }

        private void DBEditDlg_FormClosing( object sender, FormClosingEventArgs e ) {
            if( !CheckForUnsaved() ) {
                e.Cancel = true;
            }
        }
        #endregion

        /// <summary>
        /// If there are any unsaved changes, asks the user if they want to save. Also gives the user the option to cancel the calling action.
        /// </summary>
        /// <returns>True if the action should proceed, false otherwise.</returns>
        bool CheckForUnsaved() {
            if( !UnsavedChanges ) {
                return true;
            }

            DialogResult res = MessageBox.Show( "There are unsaved changes. Save to the game database first?", "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning );
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
