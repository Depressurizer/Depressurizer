using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Depressurizer {
    public partial class AutoCatConfigPanel_Genre : UserControl {
        private Lib.ExtToolTip ttHelp;
        
        public AutoCatConfigPanel_Genre(Lib.ExtToolTip ttHelp) {
            this.ttHelp = ttHelp;
            
            InitializeComponent();

            ttHelp.Ext_SetToolTip( genre_helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix );
            ttHelp.Ext_SetToolTip( genre_helpRemoveExisting, GlobalStrings.DlgAutoCat_Help_Genre_RemoveExisting );

            FillGenreList();
        }

        public void FillGenreList() {
            genre_lstIgnore.Items.Clear();

            if( Program.GameDB != null ) {
                SortedSet<string> genreList = Program.GameDB.GetAllGenres();
            
                foreach( string s in genreList ) {
                    genre_lstIgnore.Items.Add( s );
                }
            }
        }

        public void FillSettings( AutoCatGenre ac ) {
            if( ac == null ) return;
            genre_chkRemoveExisting.Checked = ac.RemoveOtherGenres;
            genre_numMaxCats.Value = ac.MaxCategories;
            genre_txtPrefix.Text = ac.Prefix;

            foreach( ListViewItem item in genre_lstIgnore.Items ) {
                item.Checked = ac.IgnoredGenres.Contains( item.Text );
            }
        }

        public void SaveToAutoCat( AutoCatGenre ac ) {
            ac.Prefix = genre_txtPrefix.Text;
            ac.MaxCategories = (int)genre_numMaxCats.Value;
            ac.RemoveOtherGenres = genre_chkRemoveExisting.Checked;

            ac.IgnoredGenres.Clear();
            foreach( ListViewItem i in genre_lstIgnore.Items ) {
                if( i.Checked ) {
                    ac.IgnoredGenres.Add( i.Text );
                }
            }
        }

        private void SetAllListCheckStates( ListView list, bool to ) {
            foreach( ListViewItem item in list.Items ) {
                item.Checked = to;
            }
        }





        private void genreCmdCheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( genre_lstIgnore, true );
        }

        private void genreCmdUncheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( genre_lstIgnore, false );
        }
    }
}
