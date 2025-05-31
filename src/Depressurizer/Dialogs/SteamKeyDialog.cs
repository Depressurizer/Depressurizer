using System;
using System.Diagnostics;
using System.Windows.Forms;
using Depressurizer.Core.Helpers;

namespace Depressurizer.Dialogs
{
    public partial class SteamKeyDialog : Form
    {
        #region Constructors and Destructors

        public SteamKeyDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Methods

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonGetKey_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(Constants.SteamWebApiKey)
            {
                UseShellExecute = true,
            };
            Process.Start(startInfo);
        }

        private void ButtonSaveKey_Click(object sender, EventArgs e)
        {
            if (!FormMain.ProfileLoaded)
            {
                // TODO: MessageBox
                Logger.Warn("Not saving Steam Web API key due to no active profile!");
                return;
            }

            string key = TextSteamKey.Text;
            if (key == null)
            {
                // TODO: MessageBox
                Logger.Warn("Not saving Steam Web API key as the TextBox returned null!");
                return;
            }

            FormMain.CurrentProfile.SteamWebApiKey = key;

            Close();
        }

        #endregion
    }
}
