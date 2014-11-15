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

        public void FillSettings( AutoCatTags ac ) {
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

        public void SaveToAutoCat( AutoCatTags ac ) {
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
