/*
This file is part of Depressurizer.
Copyright 2011, 2012, 2013 Steve Labbe.

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
using System.Windows.Forms;

namespace Depressurizer {
    public partial class AutoCatConfigPanel_Genre : AutoCatConfigPanel {
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

        public override void LoadFromAutoCat( AutoCat autocat ) {
            AutoCatGenre ac = autocat as AutoCatGenre;
            if( ac == null ) return;
            chkRemoveExisting.Checked = ac.RemoveOtherGenres;
            numMaxCats.Value = ac.MaxCategories;
            txtPrefix.Text = ac.Prefix;

            foreach( ListViewItem item in lstIgnore.Items ) {
                item.Checked = ac.IgnoredGenres.Contains( item.Text );
            }
        }

        public override void SaveToAutoCat( AutoCat autocat ) {
            AutoCatGenre ac = autocat as AutoCatGenre;
            if( ac == null ) return;
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
