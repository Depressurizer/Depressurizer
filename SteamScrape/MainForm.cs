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
using DPLib;
using System.Collections.Generic;

namespace SteamScrape {
    public partial class MainForm : Form {

        GameDB gameList = new GameDB();
        MultiColumnListViewComparer listSorter = new MultiColumnListViewComparer();

        bool filterSuspend = false;

        public MainForm() {
            InitializeComponent();
        }

        void SaveGames( bool raw ) {
            SaveFileDialog dlg = new SaveFileDialog();
            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                this.Cursor = Cursors.WaitCursor;
                gameList.SaveToXml( dlg.FileName, raw );
                this.Cursor = Cursors.Default;
            }
        }

        void LoadGames() {
            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                this.Cursor = Cursors.WaitCursor;
                gameList.LoadFromXml( dlg.FileName );
                RefreshGameList();
                this.Cursor = Cursors.Default;
            }
        }

        void ClearList() {
            gameList.Games.Clear();
            RefreshGameList();
        }

        void ScrapeGamesOfType( AppType type ) {
            Cursor = Cursors.WaitCursor;

            Queue<int> gamesToUpdate = new Queue<int>();

            foreach( GameDBEntry g in gameList.Games.Values ) {
                if( g.Type == type ) {
                    gamesToUpdate.Enqueue( g.Id );
                }
            }
            if( gamesToUpdate.Count > 0 ) {
                ScrapeProcDlg dlg = new ScrapeProcDlg( gameList, gamesToUpdate );
                dlg.ShowDialog();
                RefreshGameList();
            }

            Cursor = Cursors.Default;
        }

        #region UI Updaters

        void RefreshGameList() {
            lstGames.BeginUpdate();
            lstGames.ListViewItemSorter = null;
            lstGames.Items.Clear();

            foreach( GameDBEntry g in gameList.Games.Values ) {
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
            if( game == null || !gameList.Games.ContainsKey( game.Id ) || !ShouldDisplayGame( game ) ) {
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

        #endregion

        #region Event Handlers

        private void MainForm_Load( object sender, EventArgs e ) {
            listSorter.AddIntCol( 1 );
            lstGames.ListViewItemSorter = listSorter;
            UpdateSelectedStatus();
        }

        private void menu_File_Load_Click( object sender, EventArgs e ) {
            LoadGames();
            UpdateSelectedStatus();
        }

        private void menu_File_SaveRaw_Click( object sender, EventArgs e ) {
            SaveGames( true );
        }

        private void menu_File_SavePruned_Click( object sender, EventArgs e ) {
            SaveGames( false );
        }

        private void menu_File_Clear_Click( object sender, EventArgs e ) {
            ClearList();
            UpdateSelectedStatus();
        }

        private void menu_File_Exit_Click( object sender, EventArgs e ) {
            this.Close();
        }

        private void lstGames_ColumnClick( object sender, ColumnClickEventArgs e ) {
            listSorter.ColClick( e.Column );
            lstGames.Sort();
        }

        private void cmdFetch_Click( object sender, EventArgs e ) {
            this.Cursor = Cursors.WaitCursor;
            // gameList.UpdateAppList();

            FetchPrcDlg dlg = new FetchPrcDlg( gameList );
            DialogResult res = dlg.ShowDialog();

            MessageBox.Show( res.ToString() );

            RefreshGameList();
            UpdateSelectedStatus();
            this.Cursor = Cursors.Default;
        }

        private void cmdStore_Click( object sender, EventArgs e ) {
            if( lstGames.SelectedItems.Count > 0 ) {
                GameDBEntry g = lstGames.SelectedItems[0].Tag as GameDBEntry;
                if( g != null ) {
                    System.Diagnostics.Process.Start( string.Format( "http://store.steampowered.com/app/{0}/", g.Id ) );
                }
            }
        }

        private void cmdAddGame_Click( object sender, EventArgs e ) {
            GameDBEntryDialog dlg = new GameDBEntryDialog();
            if( dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK && dlg.Game != null ) {
                if( gameList.Games.ContainsKey( dlg.Game.Id ) ) {
                    MessageBox.Show( "Game with specified ID already exists." );
                } else {
                    gameList.Games.Add( dlg.Game.Id, dlg.Game );
                    AddGameToList( dlg.Game );
                    UpdateSelectedStatus();
                }
            }
        }

        private void cmdEditGame_Click( object sender, EventArgs e ) {
            if( lstGames.SelectedIndices.Count > 0 ) {
                GameDBEntry game = lstGames.SelectedItems[0].Tag as GameDBEntry;
                if( game != null ) {
                    GameDBEntryDialog dlg = new GameDBEntryDialog( game );
                    DialogResult res = dlg.ShowDialog();
                    if( res == System.Windows.Forms.DialogResult.OK ) {
                        UpdateGameAtIndex( lstGames.SelectedIndices[0] );
                    }
                }
            }
        }

        private void cmdDeleteGame_Click( object sender, EventArgs e ) {
            if( lstGames.SelectedItems.Count > 0 ) {
                foreach( ListViewItem item in lstGames.SelectedItems ) {
                    GameDBEntry game = item.Tag as GameDBEntry;
                    if( game != null ) {
                        gameList.Games.Remove( game.Id );
                    }
                }
                UpdateSelectedGames();
                UpdateSelectedStatus();
            }
        }

        private void cmdUpdateSelected_Click( object sender, EventArgs e ) {
            if( lstGames.SelectedItems.Count > 0 ) {
                Cursor = Cursors.WaitCursor;

                Queue<int> gamesToUpdate = new Queue<int>();

                foreach( int index in lstGames.SelectedIndices ) {
                    GameDBEntry game = lstGames.Items[index].Tag as GameDBEntry;
                    if( game != null ) {
                        gamesToUpdate.Enqueue( game.Id );
                    }
                }
                ScrapeProcDlg dlg = new ScrapeProcDlg( gameList, gamesToUpdate );
                DialogResult res = dlg.ShowDialog();

                MessageBox.Show( res.ToString() );

                RefreshGameList();
                UpdateSelectedStatus();

                Cursor = Cursors.Default;
            }
        }


        private void cmdUpdateUnchecked_Click( object sender, EventArgs e ) {
            ScrapeGamesOfType( AppType.New );
            UpdateSelectedStatus();
        }

        private void cmdUpdateRedirect_Click( object sender, EventArgs e ) {
            ScrapeGamesOfType( AppType.IdRedirect );
            UpdateSelectedStatus();
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
                UpdateSelectedStatus();
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
                UpdateSelectedStatus();
            }
        }


        #endregion

        private void lstGames_SelectedIndexChanged( object sender, EventArgs e ) {
            UpdateSelectedStatus();
        }

        void UpdateSelectedStatus() {
            statSelected.Text = string.Format( "{0} selected / {1} displayed / {2} total", lstGames.SelectedItems.Count, lstGames.Items.Count, gameList.Games.Count );
        }
    }
}
