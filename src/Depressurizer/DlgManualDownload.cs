using System;
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class DlgManualDownload : Form
    {
        #region Fields

        public bool Custom;

        public long IdVal;

        public string UrlVal;

        #endregion

        #region Constructors and Destructors

        public DlgManualDownload()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (radId.Checked)
            {
                if (long.TryParse(txtEntry.Text, out IdVal))
                {
                    Custom = false;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(this, GlobalStrings.DlgManualDownload_IfIDSelectedMustBeNumber);
                }
            }
            else
            {
                Custom = true;
                UrlVal = txtEntry.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        #endregion
    }
}
