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
using System.Windows.Forms;

namespace Depressurizer {
    public partial class GameDBEntryDialog : Form {

        public GameDBEntry Game;

        private bool editMode;

        public GameDBEntryDialog()
            : this( null ) {
        }

        public GameDBEntryDialog( GameDBEntry game ) {
            InitializeComponent();
            Game = game;
            editMode = ( game == null ) ? false : true;
        }

        private void GameDBEntryForm_Load( object sender, EventArgs e ) {
            foreach( object o in Enum.GetValues( typeof( AppType ) ) ) {
                cmbType.Items.Add( o );
            }

            if( editMode ) {
                txtId.Text = Game.Id.ToString();
                txtId.Enabled = false;

                txtTitle.Text = Game.Name;
                txtGenre.Text = Game.Genre;
                cmbType.SelectedItem = Game.Type;

            } else {
                cmdSave.Text = "Add";
                cmbType.SelectedIndex = 0;
            }
        }

        private bool SaveToGame() {
            if( Game == null ) {
                Game = new GameDBEntry();
            }
            if( !editMode ) {
                if( !int.TryParse( txtId.Text, out Game.Id ) ) {
                    MessageBox.Show( "App ID must be an integer." );
                    return false;
                }
            }
            Game.Type = (AppType)cmbType.SelectedItem;
            Game.Name = txtTitle.Text;
            Game.Genre = txtGenre.Text;
            return true;
        }

        private void cmdCancel_Click( object sender, EventArgs e ) {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void cmdSave_Click( object sender, EventArgs e ) {
            if( SaveToGame() ) {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }
    }
}
