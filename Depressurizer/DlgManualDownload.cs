using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Depressurizer {
    public partial class DlgManualDownload : Form {
        public Int64 IdVal;
        public string UrlVal;
        public bool Custom;

        public DlgManualDownload() {
            InitializeComponent();
        }

        private void cmdOk_Click( object sender, EventArgs e ) {
            if( radId.Checked ) {
                if( Int64.TryParse( txtEntry.Text, out IdVal ) ) {
                    Custom = false;
                    DialogResult = DialogResult.OK;
                    Close();
                } else {
                    MessageBox.Show( this, "If ID is selected, entry must be a number." );
                }
            } else {
                Custom = true;
                UrlVal = txtEntry.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

    }
}
