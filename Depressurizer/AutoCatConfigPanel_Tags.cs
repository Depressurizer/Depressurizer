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

        public AutoCatConfigPanel_Tags() {
            InitializeComponent();
        }

        public void FillTagList( ICollection<string> preChecked = null ) {
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
    }
}
