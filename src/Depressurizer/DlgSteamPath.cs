using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Depressurizer
{
    public partial class DlgSteamPath : Form
    {
        #region Constructors and Destructors

        public DlgSteamPath()
        {
            InitializeComponent();
            txtPath.Text = GetSteamPath();
        }

        #endregion

        #region Public Properties

        public string Path => txtPath.Text.Trim().TrimEnd('\\');

        #endregion

        #region Methods

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtPath.Text = dialog.SelectedPath;
                }
            }
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Path))
            {
                DialogResult res = MessageBox.Show(GlobalStrings.DlgSteamPath_ThatPathDoesNotExist, GlobalStrings.Gen_Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (res == DialogResult.No)
                {
                    return;
                }
            }

            Close();
        }

        private string GetSteamPath()
        {
            try
            {
                string s = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "steamPath", null) as string;
                if (s == null)
                {
                    s = string.Empty;
                }

                return s.Replace('/', '\\');
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion
    }
}
