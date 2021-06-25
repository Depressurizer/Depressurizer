using System;
using System.Drawing;
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class DlgClose : Form
    {
        #region Fields

        public bool Export => chkSaveSteam.Checked;

        #endregion

        #region Constructors and Destructors

        public DlgClose(string message, string title, Image picture, bool cancel)
        {
            InitializeComponent();

            lblMessage.Text = message;
            Text = title;
            pictureBox1.Image = picture;
            btnCancel.Visible = cancel;
        }

        #endregion

    }
}
