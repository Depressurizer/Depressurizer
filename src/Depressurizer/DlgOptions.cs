/*
This file is part of Depressurizer.
Copyright 2011, 2012, 2013 Steve Labbe.

Depressurizer is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Depressurizer is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Depressurizer.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using Depressurizer.Helpers;

namespace Depressurizer
{
    public partial class DlgOptions : Form
    {
        public DlgOptions()
        {
            InitializeComponent();

            // Set up help tooltips
            ttHelp.Ext_SetToolTip(helpIncludeImputedTimes, GlobalStrings.DlgOptions_Help_IncludeImputedTimes);
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            string[] levels = Enum.GetNames(typeof(LogLevel));
            cmbLogLevel.Items.AddRange(levels);

            //UI languages
            List<string> UILanguages = new List<string>();
            foreach (string l in Enum.GetNames(typeof(UILanguage)))
            {
                string name;
                switch (l)
                {
                    case "windows":
                        name = "Default";
                        break;
                    default:
                        name = CultureInfo.GetCultureInfo(l).NativeName;
                        break;
                }
                UILanguages.Add(name);
            }
            cmbUILanguage.Items.AddRange(UILanguages.ToArray());

            //Store Languages
            List<string> storeLanguages = new List<string>();
            foreach (string l in Enum.GetNames(typeof(StoreLanguage)))
            {
                string name;
                switch (l)
                {
                    case "windows":
                        name = "Default";
                        break;
                    case "zh_Hans":
                        name = CultureInfo.GetCultureInfo("zh-Hans").NativeName;
                        break;
                    case "zh_Hant":
                        name = CultureInfo.GetCultureInfo("zh-Hant").NativeName;
                        break;
                    case "pt_BR":
                        name = CultureInfo.GetCultureInfo("pt-BR").NativeName;
                        break;
                    default:
                        name = CultureInfo.GetCultureInfo(l).NativeName;
                        break;
                }
                storeLanguages.Add(name);
            }
            cmbStoreLanguage.Items.AddRange(storeLanguages.ToArray());

            FillFieldsFromSettings();
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
            chkAutosaveDB.Checked = settings.AutosaveDB;
            numScrapePromptDays.Value = settings.ScrapePromptDays;

            chkCheckForDepressurizerUpdates.Checked = settings.CheckForDepressurizerUpdates;

            chkRemoveExtraEntries.Checked = settings.RemoveExtraEntries;

            cmbLogLevel.SelectedIndex = (int) settings.LogLevel;
            numLogBackup.Value = settings.LogBackups;

            //supported languages have an enum value of 1-5 (en, es, ru, uk, nl). 0 is windows language.
            cmbUILanguage.SelectedIndex = (int) settings.UserLang;
            cmbStoreLanguage.SelectedIndex = (int) settings.StoreLang;
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
            settings.AutosaveDB = chkAutosaveDB.Checked;
            settings.ScrapePromptDays = (int) numScrapePromptDays.Value;

            settings.CheckForDepressurizerUpdates = chkCheckForDepressurizerUpdates.Checked;

            settings.RemoveExtraEntries = chkRemoveExtraEntries.Checked;

            settings.LogLevel = (LogLevel) cmbLogLevel.SelectedIndex;
            settings.LogBackups = (int) numLogBackup.Value;

            settings.UserLang = (UILanguage) cmbUILanguage.SelectedIndex;
            settings.StoreLang = (StoreLanguage) cmbStoreLanguage.SelectedIndex;

            try
            {
                settings.Save();
            }
            catch (Exception e)
            {
                MessageBox.Show(GlobalStrings.DlgOptions_ErrorSavingSettingsFile + e.Message,
                    GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Event handlers

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            SaveFieldsToSettings();
            Close();
        }

        private void cmdSteamPathBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                txtSteamPath.Text = dlg.SelectedPath;
            }
        }

        private void cmdDefaultProfileBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                txtDefaultProfile.Text = dlg.FileName;
            }
        }

        #endregion
    }
}