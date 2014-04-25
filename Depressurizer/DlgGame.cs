/*
Copyright 2011, 2012, 2013 Steve Labbe

This file is part of Depressurizer.

Depressurizer is free software: you can redistribute it and/or modify it under the terms of the GNU
General Public License as published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

Depressurizer is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even
the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with Depressurizer.  If not, see
<http://www.gnu.org/licenses/>.
*/
using System;
using System.Windows.Forms;

namespace Depressurizer {
    public partial class DlgGame : Form {
        GameData Data;
        public Game Game;

        bool editMode;

        private DlgGame() {
            InitializeComponent();
        }

        public DlgGame( GameData data, Game game = null )
            : this() {
            this.Data = data;
            Game = game;
            editMode = Game != null;
        }

        private void GameDlg_Load( object sender, EventArgs e ) {
            cmbCategory.Items.Add( GlobalStrings.MainForm_Uncategorized );
            foreach( Category cat in Data.Categories ) {
                cmbCategory.Items.Add( cat.Name );
            }

            if( editMode ) {
                Text = GlobalStrings.DlgGame_EditGame;
                txtId.Text = Game.Id.ToString();
                txtName.Text = Game.Name;
                if( Game.Category != null ) {
                    cmbCategory.Text = Game.Category.Name;
                }
                chkFavorite.Checked = Game.Favorite;
                txtId.Enabled = false;
            } else {
                Text = GlobalStrings.DlgGame_CreateGame;
                cmbCategory.SelectedIndex = 0;
            }
        }

        private void cmdCancel_Click( object sender, EventArgs e ) {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void cmdOk_Click( object sender, EventArgs e ) {
            Category setCat = null;
            if( !CatUtil.StringToCategory( cmbCategory.Text, Data, out setCat ) ) {
                return;
            }

            if( editMode ) {
                Game.Name = txtName.Text;
            } else {
                int id;
                if( !int.TryParse( txtId.Text, out id ) ) {
                    MessageBox.Show(GlobalStrings.DlgGameDBEntry_IDMustBeInteger, GlobalStrings.DBEditDlg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // jpodadera. Control if Game ID already exists to avoid exceptions
                if (Data.Games.ContainsKey(id))
                {
                    MessageBox.Show(GlobalStrings.DBEditDlg_GameIdAlreadyExists, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    Game = new Game(id, txtName.Text);
                    Data.Games.Add(id, Game);
                }
            }
            Game.Category = setCat;
            Game.Favorite = chkFavorite.Checked;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
