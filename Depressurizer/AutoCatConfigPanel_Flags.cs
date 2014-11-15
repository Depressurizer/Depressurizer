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
        public AutoCatConfigPanel_Flags() {
            InitializeComponent();
        }

        public void FillFlagsList() {
            flags_lstIncluded.Items.Clear();

            if( Program.GameDB != null ) {
                SortedSet<string> flagList = Program.GameDB.GetAllStoreFlags();

                foreach( string s in flagsList ) {
                    flags_lstIncluded.Items.Add( s );
                }
            }
        }
    }
}
