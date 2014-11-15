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

            ttHelp.Ext_SetToolTip( helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix );
            ttHelp.Ext_SetToolTip( helpRemoveExisting, GlobalStrings.DlgAutoCat_Help_Genre_RemoveExisting );

            FillGenreList();
        }

        public void FillGenreList() {
            lstIgnore.Items.Clear();

            if( Program.GameDB != null ) {
                SortedSet<string> genreList = Program.GameDB.GetAllGenres();
            
                foreach( string s in genreList ) {
                    lstIgnore.Items.Add( s );
                }
            }
        }

        public void FillSettings( AutoCatGenre ac ) {
            if( ac == null ) return;
            chkRemoveExisting.Checked = ac.RemoveOtherGenres;
            numMaxCats.Value = ac.MaxCategories;
            txtPrefix.Text = ac.Prefix;

            foreach( ListViewItem item in lstIgnore.Items ) {
                item.Checked = ac.IgnoredGenres.Contains( item.Text );
            }
        }

        public void SaveToAutoCat( AutoCatGenre ac ) {
            ac.Prefix = txtPrefix.Text;
            ac.MaxCategories = (int)numMaxCats.Value;
            ac.RemoveOtherGenres = chkRemoveExisting.Checked;

            ac.IgnoredGenres.Clear();
            foreach( ListViewItem i in lstIgnore.Items ) {
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

        private void cmdCheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( lstIgnore, true );
        }

        private void cmdUncheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( lstIgnore, false );
        }
    }
}
