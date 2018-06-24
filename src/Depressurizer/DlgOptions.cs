#region License

//     This file (DlgOptions.cs) is part of Depressurizer.
//     Copyright (C) 2011  Steve Labbe
//     Copyright (C) 2018  Martijn Vegter
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <https://www.gnu.org/licenses/>.

#endregion

using System;
using System.Globalization;
using System.Windows.Forms;
using Depressurizer.Core.Enums;
using Depressurizer.Enums;

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
			txtSteamPath.Text = Settings.SteamPath;
			txtDefaultProfile.Text = Settings.ProfileToLoad;

			switch (Settings.StartupAction)
			{
				case StartupAction.Create:
					radCreate.Checked = true;

					break;
				case StartupAction.Load:
					radLoad.Checked = true;

					break;
				default:

					throw new ArgumentOutOfRangeException();
			}

			switch (Settings.ListSource)
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
				default:

					throw new ArgumentOutOfRangeException();
			}

			chkUpdateAppInfoOnStartup.Checked = Settings.UpdateAppInfoOnStart;
			chkUpdateHltbOnStartup.Checked = Settings.UpdateHltbOnStart;
			chkIncludeImputedTimes.Checked = Settings.IncludeImputedTimes;
			chkAutosaveDB.Checked = Settings.AutoSaveDatabase;
			numScrapePromptDays.Value = Settings.ScrapePromptDays;

			chkCheckForDepressurizerUpdates.Checked = Settings.CheckForDepressurizerUpdates;

			chkRemoveExtraEntries.Checked = Settings.RemoveExtraEntries;

			//supported languages have an enum value of 1-5 (en, es, ru, uk, nl). 0 is windows language.
			cmbUILanguage.SelectedIndex = (int) Settings.InterfaceLanguage;
			cmbStoreLanguage.SelectedIndex = (int) Settings.StoreLanguage;
		}

		private void OptionsForm_Load(object sender, EventArgs e)
		{
			// Interface languages
			cmbUILanguage.Items.Clear();
			foreach (InterfaceLanguage language in Enum.GetValues(typeof(InterfaceLanguage)))
			{
				CultureInfo culture = Utility.GetCulture(language);
				cmbUILanguage.Items.Add(culture.NativeName);
			}

			// Store languages
			cmbStoreLanguage.Items.Clear();
			foreach (StoreLanguage language in Enum.GetValues(typeof(StoreLanguage)))
			{
				CultureInfo culture = Utility.GetCulture(language);
				cmbStoreLanguage.Items.Add(culture.NativeName);
			}

			FillFieldsFromSettings();
		}

		private void SaveFieldsToSettings()
		{
			Settings.SteamPath = txtSteamPath.Text;

			if (radLoad.Checked)
			{
				Settings.StartupAction = StartupAction.Load;
			}

			if (radCreate.Checked)
			{
				Settings.StartupAction = StartupAction.Create;
			}

			switch (cmbDatSrc.SelectedIndex)
			{
				case 0:
					Settings.ListSource = GameListSource.XmlPreferred;

					break;
				case 1:
					Settings.ListSource = GameListSource.XmlOnly;

					break;
				case 2:
					Settings.ListSource = GameListSource.WebsiteOnly;

					break;
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