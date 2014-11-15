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
    public partial class AutoCatConfigPanel_Tags : UserControl {

        //TODO: Make sure this gets set
        private GameList ownedGames = null;

        private Lib.ExtToolTip ttHelp;

        public AutoCatConfigPanel_Tags( Lib.ExtToolTip ttHelp, GameList ownedGames ) {
            this.ttHelp = ttHelp;
            this.ownedGames = ownedGames;

            InitializeComponent();

            ttHelp.Ext_SetToolTip( tags_helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix );
            ttHelp.Ext_SetToolTip( tags_list_helpMinScore, GlobalStrings.DlgAutoCat_Help_ListMinScore );
            ttHelp.Ext_SetToolTip( tags_list_helpOwnedOnly, GlobalStrings.DlgAutoCat_Help_ListOwnedOnly );
            ttHelp.Ext_SetToolTip( tags_list_helpTagsPerGame, GlobalStrings.DlgAutoCat_Help_ListTagsPerGame );
            ttHelp.Ext_SetToolTip( tags_list_helpWeightFactor, GlobalStrings.DlgAutoCat_Help_ListWeightFactor );
            ttHelp.Ext_SetToolTip( tags_list_helpScoreSort, GlobalStrings.DlgAutoCat_Help_ListScoreSort );
            ttHelp.Ext_SetToolTip( tags_list_helpExcludeGenres, GlobalStrings.DlgAutoCat_Help_ListExcludeGenres );
        }

        public void FillTagsList( ICollection<string> preChecked = null ) {
            IEnumerable<Tuple<string, float>> tagList =
                Program.GameDB.CalculateSortedTagList(
                    tags_list_chkOwnedOnly.Checked ? ownedGames : null,
                    (float)tags_list_numWeightFactor.Value,
                    (int)tags_list_numMinScore.Value, (int)tags_list_numTagsPerGame.Value, tags_list_chkExcludeGenres.Checked, tags_list_chkScoreSort.Checked );
            tags_lstIncluded.BeginUpdate();
            tags_lstIncluded.Items.Clear();
            foreach( Tuple<string, float> tag in tagList ) {
                ListViewItem newItem = new ListViewItem( string.Format( "{0} [{1:F0}]", tag.Item1, tag.Item2 ) );
                newItem.Tag = tag.Item1;
                if( preChecked != null && preChecked.Contains( tag.Item1 ) ) newItem.Checked = true;
                tags_lstIncluded.Items.Add( newItem );
            }
            tags_lstIncluded.EndUpdate();
        }

        public void FillSettings( AutoCatTags ac ) {
            if( ac == null ) return;
            tags_txtPrefix.Text = ( ac.Prefix == null ) ? string.Empty : ac.Prefix;
            tags_numMaxTags.Value = ac.MaxTags;

            tags_list_numMinScore.Value = ac.ListMinScore;
            tags_list_numTagsPerGame.Value = ac.ListTagsPerGame;
            tags_list_chkOwnedOnly.Checked = ac.ListOwnedOnly;
            tags_list_numWeightFactor.Value = (Decimal)ac.ListWeightFactor;
            tags_list_chkExcludeGenres.Checked = ac.ListExcludeGenres;
            tags_list_chkScoreSort.Checked = ac.ListScoreSort;

            FillTagsList( ac.IncludedTags );
        }

        public void SaveToAutoCat( AutoCatTags ac ) {
            ac.Prefix = tags_txtPrefix.Text;

            ac.MaxTags = (int)tags_numMaxTags.Value;

            ac.IncludedTags = new HashSet<string>();
            foreach( ListViewItem i in tags_lstIncluded.CheckedItems ) {
                ac.IncludedTags.Add( i.Tag as string );
            }

            ac.ListMinScore = (int)tags_list_numMinScore.Value;
            ac.ListOwnedOnly = tags_list_chkOwnedOnly.Checked;
            ac.ListTagsPerGame = (int)tags_list_numTagsPerGame.Value;
            ac.ListWeightFactor = (float)tags_list_numWeightFactor.Value;
            ac.ListExcludeGenres = tags_list_chkExcludeGenres.Checked;
            ac.ListScoreSort = tags_list_chkScoreSort.Checked;
        }

        private void SetAllListCheckStates( ListView list, bool to ) {
            foreach( ListViewItem item in list.Items ) {
                item.Checked = to;
            }
        }





        private void tags_cmdListRebuild_Click( object sender, EventArgs e ) {
            HashSet<string> checkedTags = new HashSet<string>();
            foreach( ListViewItem item in tags_lstIncluded.CheckedItems ) {
                checkedTags.Add( item.Tag as string );
            }
            FillTagsList( checkedTags );
        }

        private void tags_cmdCheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( tags_lstIncluded, true );
        }

        private void tags_cmdUncheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( tags_lstIncluded, false );
        }
    }
}
