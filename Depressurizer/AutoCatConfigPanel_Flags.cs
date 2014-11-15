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
    public partial class AutoCatConfigPanel_Flags : UserControl {

        private Lib.ExtToolTip ttHelp;

        public AutoCatConfigPanel_Flags(Lib.ExtToolTip ttHelp) {
            this.ttHelp = ttHelp;
            InitializeComponent();

            ttHelp.Ext_SetToolTip( helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix );

            FillFlagsList();
        }

        public void FillFlagsList() {
            lstIncluded.Items.Clear();

            if( Program.GameDB != null ) {
                SortedSet<string> flagsList = Program.GameDB.GetAllStoreFlags();

                foreach( string s in flagsList ) {
                    lstIncluded.Items.Add( s );
                }
            }
        }

        public void FillSettings( AutoCatFlags ac ) {
            if( ac == null ) return;

            txtPrefix.Text = ac.Prefix;

            foreach( ListViewItem item in lstIncluded.Items ) {
                item.Checked = ac.IncludedFlags.Contains( item.Text );
            }
        }

        public void SaveToAutoCat( AutoCatFlags ac ) {
            ac.Prefix = txtPrefix.Text;

            ac.IncludedFlags.Clear();
            foreach( ListViewItem i in lstIncluded.Items ) {
                if( i.Checked ) {
                    ac.IncludedFlags.Add( i.Text );
                }
            }
        }

        private void SetAllListCheckStates( ListView list, bool to ) {
            foreach( ListViewItem item in list.Items ) {
                item.Checked = to;
            }
        }

        private void cmdCheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( lstIncluded, true );
        }

        private void cmdUncheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( lstIncluded, false );
        }

    }
}
