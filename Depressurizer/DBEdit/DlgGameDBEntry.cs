using System;
using System.Windows.Forms;
using System.Collections.Generic;

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
            foreach( object o in Enum.GetValues( typeof( AppTypes ) ) ) {
                cmbType.Items.Add( o );
            }

            InitializeFields( Game );
        }

        private void InitializeFields( GameDBEntry entry = null ) {
            if( entry == null ) {
                cmdSave.Text = GlobalStrings.DlgGameDBEntry_Add;
                cmbType.SelectedIndex = 0;
            } else {
                txtId.Text = Game.Id.ToString();
                txtId.Enabled = false;

                txtParent.Text = ( Game.ParentId < 0 ) ? "" : Game.ParentId.ToString();

                cmbType.SelectedItem = Game.AppType;

                this.txtName.Text = Game.Name;
                if( Game.Genres != null ) txtGenres.Text = string.Join( ",", Game.Genres );
                if( Game.Flags != null ) txtFlags.Text = string.Join( ",", Game.Flags );
                if( Game.Tags != null ) txtTags.Text = string.Join( ",", Game.Tags );
                if( Game.Developers != null ) txtDev.Text = string.Join( ",", Game.Developers );
                if( Game.Publishers != null ) txtPub.Text = string.Join( ",", Game.Publishers );
                if( Game.MC_Url != null ) txtMCName.Text = Game.MC_Url;
                if( Game.SteamReleaseDate != null ) txtRelease.Text = Game.SteamReleaseDate;
                chkPlatWin.Checked = Game.Platforms.HasFlag( AppPlatforms.Windows );
                chkPlatMac.Checked = Game.Platforms.HasFlag( AppPlatforms.Mac );
                chkPlatLinux.Checked = Game.Platforms.HasFlag( AppPlatforms.Linux );

                chkWebUpdate.Checked = Game.LastStoreScrape > 0;
                chkAppInfoUpdate.Checked = Game.LastAppInfoUpdate > 0;

                dateWeb.Value = Utility.GetDTFromUTime( Game.LastStoreScrape );
                dateAppInfo.Value = Utility.GetDTFromUTime( Game.LastAppInfoUpdate );
            }
        }

        private bool ValidateEntries( out int id, out int parent ) {
            parent = -1;
            if( !int.TryParse( txtId.Text, out id ) || id <= 0 ) {
                MessageBox.Show( GlobalStrings.DlgGameDBEntry_IDMustBeInteger );
                return false;
            }
            if( !string.IsNullOrEmpty( txtParent.Text ) && !int.TryParse( txtParent.Text, out parent ) ) {
                MessageBox.Show( GlobalStrings.DlgGameDBEntry_ParentMustBeInt );
            }
            return true;
        }

        private bool SaveToGame() {

            int id, parent;
            if( !ValidateEntries( out id, out parent ) ) {
                return false;
            }

            if( Game == null ) {
                Game = new GameDBEntry();
                Game.Id = id;
            }

            Game.ParentId = parent;

            Game.AppType = (AppTypes)cmbType.SelectedItem;
            Game.Name = txtName.Text;

            Game.Genres = new List<string>( txtGenres.Text.Split( ',' ) );
            Game.Flags = new List<string>( txtFlags.Text.Split( ',' ) );
            Game.Tags = new List<string>( txtFlags.Text.Split( ',' ) );
            Game.Developers = new List<string>( txtFlags.Text.Split( ',' ) );
            Game.Publishers = new List<string>( txtFlags.Text.Split( ',' ) );

            Game.MC_Url = txtMCName.Text;
            Game.SteamReleaseDate = txtRelease.Text;

            Game.Platforms = AppPlatforms.None;
            if( chkPlatWin.Checked ) Game.Platforms |= AppPlatforms.Windows;
            if( chkPlatMac.Checked ) Game.Platforms |= AppPlatforms.Mac;
            if( chkPlatLinux.Checked ) Game.Platforms |= AppPlatforms.Linux;

            Game.LastStoreScrape = chkWebUpdate.Checked ? Utility.GetUTime( dateWeb.Value ) : 0;
            Game.LastAppInfoUpdate = chkAppInfoUpdate.Checked ? Utility.GetUTime( dateAppInfo.Value ) : 0;

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
