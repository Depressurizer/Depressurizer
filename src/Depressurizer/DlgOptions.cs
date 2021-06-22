using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Depressurizer.Core;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Properties;

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

        #region Properties

        private static Database Database => Database.Instance;

        private static Settings Settings => Settings.Instance;

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

        private void cmdDefaultIgnored_Click(object sender, EventArgs e)
        {
            Settings.IgnoreList = new List<int>(Settings.DefaultIgnoreList);
            LoadIgnoreList();
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

        private void cmdIgnore_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtIgnore.Text, out int appId))
            {
                lstIgnored.Items.Add(appId.ToString(CultureInfo.InvariantCulture));
                lstIgnored.Sort();

                txtIgnore.ResetText();
            }
            else
            {
                MessageBox.Show(GlobalStrings.DlgGameDBEntry_IDMustBeInteger, Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void cmdUnignore_Click(object sender, EventArgs e)
        {
            while (lstIgnored.SelectedIndices.Count > 0)
            {
                lstIgnored.Items.RemoveAt(lstIgnored.SelectedIndices[0]);
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

            chkUpdateAppInfoOnStartup.Checked = settings.UpdateAppInfoOnStart;
            chkUpdateHltbOnStartup.Checked = settings.UpdateHltbOnStart;
            chkIncludeImputedTimes.Checked = settings.IncludeImputedTimes;
            chkAutosaveDB.Checked = settings.AutoSaveDatabase;
            numScrapePromptDays.Value = settings.ScrapePromptDays;

            chkCheckForDepressurizerUpdates.Checked = settings.CheckForDepressurizerUpdates;

            textBoxPremiumServer.Text = settings.PremiumServer;

            chkRemoveExtraEntries.Checked = settings.RemoveExtraEntries;

            // Languages
            cmbUILanguage.SelectedIndex = (int) settings.InterfaceLanguage;
            cmbStoreLanguage.SelectedIndex = (int) settings.StoreLanguage;
        }

        private void LoadIgnoreList()
        {
            lstIgnored.Clear();
            foreach (int i in Settings.IgnoreList)
            {
                lstIgnored.Items.Add(i.ToString());
            }

            lstIgnored.Sort();
            lstIgnored.ListViewItemSorter = new IgnoreListViewItemComparer();
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

            LoadIgnoreList();

            FillFieldsFromSettings();
        }

        private void SaveFieldsToSettings()
        {
            Settings.SteamPath = txtSteamPath.Text;
            if (radLoad.Checked)
            {
                Settings.StartupAction = StartupAction.Load;
            }
            else if (radCreate.Checked)
            {
                Settings.StartupAction = StartupAction.Create;
            }
            else
            {
                Settings.StartupAction = StartupAction.None;
            }

            Settings.ProfileToLoad = txtDefaultProfile.Text;

            Settings.UpdateAppInfoOnStart = chkUpdateAppInfoOnStartup.Checked;
            Settings.UpdateHltbOnStart = chkUpdateHltbOnStartup.Checked;
            Settings.IncludeImputedTimes = chkIncludeImputedTimes.Checked;
            Settings.AutoSaveDatabase = chkAutosaveDB.Checked;
            Settings.ScrapePromptDays = (int) numScrapePromptDays.Value;

            Settings.CheckForDepressurizerUpdates = chkCheckForDepressurizerUpdates.Checked;

            Settings.RemoveExtraEntries = chkRemoveExtraEntries.Checked;

            Settings.InterfaceLanguage = (InterfaceLanguage) cmbUILanguage.SelectedIndex;
            Settings.StoreLanguage = (StoreLanguage) cmbStoreLanguage.SelectedIndex;

            Settings.PremiumServer = textBoxPremiumServer.Text;

            Thread.CurrentThread.CurrentUICulture = Language.GetCultureInfo(Settings.InterfaceLanguage);
            Database.ChangeLanguage(Settings.StoreLanguage);

            List<int> ignoreList = new List<int>(lstIgnored.Items.Count);
            foreach (ListViewItem item in lstIgnored.Items)
            {
                if (int.TryParse(item.Text, out int appId))
                {
                    ignoreList.Add(appId);
                }
            }

            Settings.IgnoreList = ignoreList;

            try
            {
                Settings.Save();
            }
            catch (Exception e)
            {
                MessageBox.Show(GlobalStrings.DlgOptions_ErrorSavingSettingsFile + e.Message, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
