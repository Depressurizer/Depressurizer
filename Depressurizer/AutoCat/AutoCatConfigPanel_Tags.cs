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
    public partial class AutoCatConfigPanel_Tags : AutoCatConfigPanel {

        private GameList ownedGames = null;

        public AutoCatConfigPanel_Tags(GameList ownedGames ) {
            this.ownedGames = ownedGames;

            InitializeComponent();

            ttHelp.Ext_SetToolTip( helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix );
            ttHelp.Ext_SetToolTip( list_helpMinScore, GlobalStrings.DlgAutoCat_Help_ListMinScore );
            ttHelp.Ext_SetToolTip( list_helpOwnedOnly, GlobalStrings.DlgAutoCat_Help_ListOwnedOnly );
            ttHelp.Ext_SetToolTip( helpTagsPerGame, GlobalStrings.DlgAutoCat_Help_ListTagsPerGame );
            ttHelp.Ext_SetToolTip( helpWeightFactor, GlobalStrings.DlgAutoCat_Help_ListWeightFactor );
            ttHelp.Ext_SetToolTip( list_helpScoreSort, GlobalStrings.DlgAutoCat_Help_ListScoreSort );
            ttHelp.Ext_SetToolTip( helpExcludeGenres, GlobalStrings.DlgAutoCat_Help_ListExcludeGenres );
        }

        public void FillTagsList( ICollection<string> preChecked = null ) {
            IEnumerable<Tuple<string, float>> tagList =
                Program.GameDB.CalculateSortedTagList(
                    list_chkOwnedOnly.Checked ? ownedGames : null,
                    (float)list_numWeightFactor.Value,
                    (int)list_numMinScore.Value, (int)list_numTagsPerGame.Value, list_chkExcludeGenres.Checked, list_chkScoreSort.Checked );
            lstIncluded.BeginUpdate();
            lstIncluded.Items.Clear();
            foreach( Tuple<string, float> tag in tagList ) {
                ListViewItem newItem = new ListViewItem( string.Format( "{0} [{1:F0}]", tag.Item1, tag.Item2 ) );
                newItem.Tag = tag.Item1;
                if( preChecked != null && preChecked.Contains( tag.Item1 ) ) newItem.Checked = true;
                lstIncluded.Items.Add( newItem );
            }
            lstIncluded.EndUpdate();
        }

        public override void LoadFromAutoCat( AutoCat autocat ) {
            AutoCatTags ac = autocat as AutoCatTags;
            if( ac == null ) return;
            txtPrefix.Text = ( ac.Prefix == null ) ? string.Empty : ac.Prefix;
            numMaxTags.Value = ac.MaxTags;

            list_numMinScore.Value = ac.ListMinScore;
            list_numTagsPerGame.Value = ac.ListTagsPerGame;
            list_chkOwnedOnly.Checked = ac.ListOwnedOnly;
            list_numWeightFactor.Value = (Decimal)ac.ListWeightFactor;
            list_chkExcludeGenres.Checked = ac.ListExcludeGenres;
            list_chkScoreSort.Checked = ac.ListScoreSort;

            FillTagsList( ac.IncludedTags );
        }

        public override void SaveToAutoCat( AutoCat autocat ) {
            AutoCatTags ac = autocat as AutoCatTags;
            if( ac == null ) return;
            ac.Prefix = txtPrefix.Text;

            ac.MaxTags = (int)numMaxTags.Value;

            ac.IncludedTags = new HashSet<string>();
            foreach( ListViewItem i in lstIncluded.CheckedItems ) {
                ac.IncludedTags.Add( i.Tag as string );
            }

            ac.ListMinScore = (int)list_numMinScore.Value;
            ac.ListOwnedOnly = list_chkOwnedOnly.Checked;
            ac.ListTagsPerGame = (int)list_numTagsPerGame.Value;
            ac.ListWeightFactor = (float)list_numWeightFactor.Value;
            ac.ListExcludeGenres = list_chkExcludeGenres.Checked;
            ac.ListScoreSort = list_chkScoreSort.Checked;
        }

        private void SetAllListCheckStates( ListView list, bool to ) {
            foreach( ListViewItem item in list.Items ) {
                item.Checked = to;
            }
        }

        private void cmdListRebuild_Click( object sender, EventArgs e ) {
            HashSet<string> checkedTags = new HashSet<string>();
            foreach( ListViewItem item in lstIncluded.CheckedItems ) {
                checkedTags.Add( item.Tag as string );
            }
            FillTagsList( checkedTags );
        }

        private void cmdCheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( lstIncluded, true );
        }

        private void cmdUncheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( lstIncluded, false );
        }
    }
}
