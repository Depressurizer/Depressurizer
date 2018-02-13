#region LICENSE

//     This file (OptionsDialog.cs) is part of Depressurizer.
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
using System.Windows.Forms;
using Depressurizer.Dialogs;
using Depressurizer.Properties;
using DepressurizerCore;

namespace Depressurizer
{
	public partial class OptionsDialog : DepressurizerDialog
	{
		#region Constructors and Destructors

		public OptionsDialog() : base(Resources.Options)
		{
			InitializeComponent();
		}

		#endregion

		#region Methods

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void ButtonOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void ButtonProfileBrowse_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				DialogResult result = dialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					TextBoxProfile.Text = dialog.FileName;
				}
			}
		}

		private void ButtonSteamBrowse_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				DialogResult result = dialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					TextBoxSteamDirectory.Text = dialog.SelectedPath;
				}
			}
		}

		private void OptionsDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK)
			{
				return;
			}

			Settings settings = Settings.Instance;

			// General
			settings.CheckForUpdates = CheckDepressurizerUpdates.Checked;

			// Steam Directory
			settings.SteamPath = TextBoxSteamDirectory.Text;

			// Profile
			settings.ProfileToLoad = TextBoxProfile.Text;

			// On Startup
			if (RadioLoadProfile.Checked)
			{
				settings.StartupAction = StartupAction.Load;
			}
			else if (RadioCreateProfile.Checked)
			{
				settings.StartupAction = StartupAction.Create;
			}

			// Database Options
			settings.OnStartUpdateFromAppInfo = CheckUpdateAppInfo.Checked;
			settings.OnStartUpdateFromHLTB = CheckUpdateHLTB.Checked;
			settings.IncludeImputedTimes = CheckIncludeImputed.Checked;
			settings.AutoSaveDatabase = CheckAutoSave.Checked;
			settings.ScrapePromptDays = (int) NumReScrapeDays.Value;

			settings.InterfaceLanguage = (InterfaceLanguage) ListInterfaceLangauge.SelectedIndex;
			settings.StoreLanguage = (StoreLanguage) ListStoreLangauge.SelectedIndex;

			settings.Save();
		}

		private void OptionsDialog_Load(object sender, EventArgs e)
		{
			Settings settings = Settings.Instance;

			// General
			CheckDepressurizerUpdates.Checked = settings.CheckForUpdates;

			// Steam Directory
			TextBoxSteamDirectory.Text = settings.SteamPath;

			// Profile
			TextBoxProfile.Text = settings.ProfileToLoad;

			// On Startup
			switch (settings.StartupAction)
			{
				case StartupAction.Load:
					RadioLoadProfile.Checked = true;
					break;
				case StartupAction.Create:
					RadioLoadProfile.Checked = true;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			// Database Options
			CheckUpdateAppInfo.Checked = settings.OnStartUpdateFromAppInfo;
			CheckUpdateHLTB.Checked = settings.OnStartUpdateFromHLTB;
			CheckIncludeImputed.Checked = settings.IncludeImputedTimes;
			CheckAutoSave.Checked = settings.AutoSaveDatabase;
			NumReScrapeDays.Value = settings.ScrapePromptDays;

			// Interface Langauge
			foreach (string language in Enum.GetNames(typeof(InterfaceLanguage)))
			{
				ListInterfaceLangauge.Items.Add(language);
			}

			ListInterfaceLangauge.SelectedIndex = (int) settings.InterfaceLanguage;

			// Store Langauge
			foreach (string language in Enum.GetNames(typeof(StoreLanguage)))
			{
				ListStoreLangauge.Items.Add(language);
			}

			ListStoreLangauge.SelectedIndex = (int) settings.StoreLanguage;
		}

		#endregion
	}
}