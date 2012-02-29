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

namespace SteamScrape {
    public partial class MainForm : Form {

        GameDB gameList = new GameDB();
        MultiColumnListViewComparer listSorter = new MultiColumnListViewComparer();

        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load( object sender, EventArgs e ) {
            listSorter.AddIntCol( 1 );
            lstGames.ListViewItemSorter = listSorter;
        }

        void RefreshGameList() {
            lstGames.BeginUpdate();
            lstGames.ListViewItemSorter = null;
            lstGames.Items.Clear();

            foreach( GameDBEntry g in gameList.Games.Values ) {
                AddGameToList( g );
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
            if( game == null || !gameList.Games.ContainsKey( game.Id ) ) {
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

        void SaveGames() {
            SaveFileDialog dlg = new SaveFileDialog();
            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                this.Cursor = Cursors.WaitCursor;
                gameList.SaveToXml( dlg.FileName );
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

        private void cmdFetch_Click( object sender, EventArgs e ) {
            this.Cursor = Cursors.WaitCursor;
            gameList.FetchAppList();
            RefreshGameList();
            this.Cursor = Cursors.Default;
        }

        private void menu_File_Save_Click( object sender, EventArgs e ) {
            SaveGames();

        }

        private void menu_File_Load_Click( object sender, EventArgs e ) {
            LoadGames();
        }

        private void menu_File_Exit_Click( object sender, EventArgs e ) {
            this.Close();
        }

        private void cmdAddGame_Click( object sender, EventArgs e ) {
            GameDBEntryForm dlg = new GameDBEntryForm();
            if( dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK && dlg.Game != null ) {
                if( gameList.Games.ContainsKey( dlg.Game.Id ) ) {
                    MessageBox.Show( "Game with specified ID already exists." );
                } else {
                    gameList.Games.Add( dlg.Game.Id, dlg.Game );
                    AddGameToList( dlg.Game );
                }
            }
        }

        private void cmdEditGame_Click( object sender, EventArgs e ) {
            if( lstGames.SelectedIndices.Count > 0 ) {
                GameDBEntry game = lstGames.SelectedItems[0].Tag as GameDBEntry;
                if( game != null ) {
                    GameDBEntryForm dlg = new GameDBEntryForm( game );
                    DialogResult res = dlg.ShowDialog();
                    if( res == System.Windows.Forms.DialogResult.OK ) {
                        UpdateGameAtIndex( lstGames.SelectedIndices[0] );
                    }
                }
            }
        }

        private void lstGames_ColumnClick( object sender, ColumnClickEventArgs e ) {
            listSorter.ColClick( e.Column );
            lstGames.Sort();
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
            }
        }

        private void cmdUpdateSelected_Click( object sender, EventArgs e ) {
            if( lstGames.SelectedItems.Count > 0 ) {
                Cursor = Cursors.WaitCursor;
                foreach( int index in lstGames.SelectedIndices ) {
                    GameDBEntry game = lstGames.Items[index].Tag as GameDBEntry;
                    if( game != null ) {
                        game.ScrapeStore();
                        UpdateGameAtIndex( index );
                        lstGames.RedrawItems( index, index, false );
                    }
                }
                Cursor = Cursors.Default;
            }
        }

    }
}
