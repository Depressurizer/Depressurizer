using System;
using System.Drawing;
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class DlgClose : Form
    {
        #region Fields

        public bool Export;

        #endregion

        #region Constructors and Destructors

        public DlgClose(string message, string title, Image picture, bool cancel, bool exportSteam)
        {
            InitializeComponent();

            lblMessage.Text = message;
            Text = title;
            pictureBox1.Image = picture;
            btnCancel.Visible = cancel;
            chkSaveSteam.Checked = exportSteam;
        }

        #endregion

        #region Methods

        private void chkSaveSteam_CheckedChanged(object sender, EventArgs e)
        {
            Export = chkSaveSteam.Checked;
        }

        #endregion
    }
}
