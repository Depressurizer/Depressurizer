using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Depressurizer.Core.Helpers;

namespace Depressurizer
{
    public partial class DlgAbout : Form
    {
        #region Constructors and Destructors

        public DlgAbout()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void DlgAbout_Load(object sender, EventArgs e)
        {
            lblVersion.Text += Assembly.GetExecutingAssembly().GetName().Version.ToString();

            int oldLen = lnkHomepage.Text.Length;
            lnkHomepage.Text += Constants.DepressurizerHomepage;
            lnkHomepage.LinkArea = new LinkArea(oldLen, lnkHomepage.Text.Length - oldLen);
        }

        private void lnkHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Utils.RunProcess(Constants.DepressurizerHomepage);
        }

        private void lnkLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Utils.RunProcess("http://www.gnu.org/licenses/");
        }

        private void lnkNDesk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Utils.RunProcess("http://www.novell.com");
        }

        #endregion
    }
}
