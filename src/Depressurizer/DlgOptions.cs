using System;
using System.Globalization;
using System.Windows.Forms;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;

namespace Depressurizer
{
    public partial class DlgOptions : Form
    {
        #region Constructors and Destructors

        public DlgOptions()
        {
            InitializeComponent();

            // Set up help tooltips
            ttHelp.Ext_SetToolTip(helpIncludeImputedTimes, GlobalStrings.DlgOptions_Help_IncludeImputedTimes);
        }

        #endregion

        #region Methods

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            SaveFieldsToSettings();
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdDefaultProfileBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtDefaultProfile.Text = dialog.FileName;
                }
            }
        }

        private void cmdSteamPathBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtSteamPath.Text = dialog.SelectedPath;
                }
            }
        }

        private void FillFieldsFromSettings()
        {
            Settings settings = Settings.Instance;
            txtSteamPath.Text = settings.SteamPath;
            txtDefaultProfile.Text = settings.ProfileToLoad;
            switch (settings.StartupAction)
            {
                case StartupAction.Load:
                    radLoad.Checked = true;
                    break;
                case StartupAction.Create:
                    radCreate.Checked = true;
                    break;
                default:
                    radNone.Checked = true;
                    break;
            }

            switch (settings.ListSource)
            {
                case GameListSource.XmlPreferred:
                    cmbDatSrc.SelectedIndex = 0;
                    break;
                case GameListSource.XmlOnly:
                    cmbDatSrc.SelectedIndex = 1;
                    break;
                case GameListSource.WebsiteOnly:
                    cmbDatSrc.SelectedIndex = 2;
                    break;
            }

            chkUpdateAppInfoOnStartup.Checked = settings.UpdateAppInfoOnStart;
            chkUpdateHltbOnStartup.Checked = settings.UpdateHltbOnStart;
            chkIncludeImputedTimes.Checked = settings.IncludeImputedTimes;
            chkAutosaveDB.Checked = settings.AutoSaveDatabase;
            numScrapePromptDays.Value = settings.ScrapePromptDays;

            chkCheckForDepressurizerUpdates.Checked = settings.CheckForDepressurizerUpdates;

            chkRemoveExtraEntries.Checked = settings.RemoveExtraEntries;

            // Languages
            cmbUILanguage.SelectedIndex = (int) settings.InterfaceLanguage;
            cmbStoreLanguage.SelectedIndex = (int) settings.StoreLanguage;
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            // Interface languages
            foreach (InterfaceLanguage language in Enum.GetValues(typeof(InterfaceLanguage)))
            {
                CultureInfo cultureInfo = Language.GetCultureInfo(language);
                cmbUILanguage.Items.Add(cultureInfo.NativeName);
            }

            // Store languages
            foreach (StoreLanguage language in Enum.GetValues(typeof(StoreLanguage)))
            {
                CultureInfo cultureInfo = Language.GetCultureInfo(language);
                cmbStoreLanguage.Items.Add(cultureInfo.NativeName);
            }

            FillFieldsFromSettings();
        }

        private void SaveFieldsToSettings()
        {
            Settings settings = Settings.Instance;

            settings.SteamPath = txtSteamPath.Text;
            if (radLoad.Checked)
            {
                settings.StartupAction = StartupAction.Load;
            }
            else if (radCreate.Checked)
            {
                settings.StartupAction = StartupAction.Create;
            }
            else
            {
                settings.StartupAction = StartupAction.None;
            }

            switch (cmbDatSrc.SelectedIndex)
            {
                case 0:
                    settings.ListSource = GameListSource.XmlPreferred;
                    break;
                case 1:
                    settings.ListSource = GameListSource.XmlOnly;
                    break;
                case 2:
                    settings.ListSource = GameListSource.WebsiteOnly;
                    break;
            }

            settings.ProfileToLoad = txtDefaultProfile.Text;

            settings.UpdateAppInfoOnStart = chkUpdateAppInfoOnStartup.Checked;
            settings.UpdateHltbOnStart = chkUpdateHltbOnStartup.Checked;
            settings.IncludeImputedTimes = chkIncludeImputedTimes.Checked;
            settings.AutoSaveDatabase = chkAutosaveDB.Checked;
            settings.ScrapePromptDays = (int) numScrapePromptDays.Value;

            settings.CheckForDepressurizerUpdates = chkCheckForDepressurizerUpdates.Checked;

            settings.RemoveExtraEntries = chkRemoveExtraEntries.Checked;

            settings.InterfaceLanguage = (InterfaceLanguage) cmbUILanguage.SelectedIndex;
            settings.StoreLanguage = (StoreLanguage) cmbStoreLanguage.SelectedIndex;

            try
            {
                settings.Save();
            }
            catch (Exception e)
            {
                MessageBox.Show(GlobalStrings.DlgOptions_ErrorSavingSettingsFile + e.Message, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
