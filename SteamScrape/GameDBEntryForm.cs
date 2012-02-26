using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SteamScrape {
    public partial class GameDBEntryForm : Form {

        public GameDBEntry Game;

        private bool editMode;

        public GameDBEntryForm()
            : this( null ) {
        }

        public GameDBEntryForm( GameDBEntry game ) {
            InitializeComponent();
            Game = game;
            editMode = ( game == null ) ? false : true;
        }

        private void GameDBEntryForm_Load( object sender, EventArgs e ) {
            foreach( object o in Enum.GetValues( typeof(AppType) ) ) {
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
