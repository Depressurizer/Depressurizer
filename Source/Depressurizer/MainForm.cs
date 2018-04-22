#region LICENSE

//     This file (MainForm.cs) is part of Depressurizer.
//     Original Copyright (C) 2011  Steve Labbe
//     Modified Copyright (C) 2018  Martijn Vegter
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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Depressurizer.Dialogs;
using Depressurizer.Helpers;
using Depressurizer.Models;
using Depressurizer.Properties;
using DepressurizerCore;
using DepressurizerCore.Helpers;
using DepressurizerCore.Models;
using MaterialSkin;
using MaterialSkin.Controls;
using Newtonsoft.Json.Linq;
using Rallion;
using Utility = DepressurizerCore.Helpers.Utility;

namespace Depressurizer
{
	public partial class FormMain : MaterialForm
	{
		#region Constants

		private const string AdvancedFilter = "ADVANCED_FILTER";

		private const string EarlyAccess = "Early Access";

		private const int MaxFilterState = 2;

		#endregion

		#region Fields

		private readonly StringBuilder _statusBuilder = new StringBuilder();

		private Filter _advFilter = new Filter(AdvancedFilter);

		// used to prevent moving the filler column in the game list
		private Thread _columnReorderThread;

		// Used to prevent double clicking in Autocat listview from changing checkstate
		private bool _doubleClick;

		private int _dragOldCat;

		// Allow visual feedback when dragging over the cat list
		private bool _isDragging;

		private object _lastSelectedCategory;

		private TypedObjectListView<GameInfo> _tlstGames;

		private bool _unsavedChanges;

		#endregion

		#region Constructors and Destructors

		public FormMain()
		{
			InitializeComponent();
			InitializeFormMain();
		}

		#endregion

		#region Delegates

		private delegate void RemoveItemCallback();

		#endregion

		#region Public Properties

		public static Profile CurrentProfile { get; private set; }

		public static bool ProfileLoaded => CurrentProfile != null;

		#endregion

		#region Properties

		private static Color Accent => Color.FromArgb(255, 0, 145, 234);

		private static Database Database => Database.Instance;

		private static Color FormColor => Color.FromArgb(255, 42, 42, 44);

		private static Color HeaderFontColor => Color.FromArgb(255, 169, 167, 167);

		private static Color ListBackground => Color.FromArgb(255, 22, 22, 22);

		private static Color Primary => Color.FromArgb(255, 55, 71, 79);

		private static Color PrimaryDark => Color.FromArgb(255, 38, 50, 56);

		private static Color PrimaryLight => Color.FromArgb(255, 96, 125, 139);

		private static Settings Settings => Settings.Instance;

		private static Color TextColor => Color.FromArgb(255, 255, 255, 255);

		private bool AdvancedCategoryFilter => mchkAdvancedCategories.Checked;

		#endregion

		#region Public Methods and Operators

		public void AddStatus(string status)
		{
			_statusBuilder.Append(status);
			_statusBuilder.Append(' ');
		}

		public void ChangeDatabaseLanguage(StoreLanguage storeLanguage)
		{
			if (Database.Language == storeLanguage)
			{
				return;
			}

			Database.Language = storeLanguage;

			List<int> appsToUpdate = new List<int>();
			if (CurrentProfile != null)
			{
				foreach (GameInfo game in CurrentProfile.GameData.Games.Values)
				{
					if (game.Id > 0)
					{
						appsToUpdate.Add(game.Id);
					}
				}

				using (ScrapeDialog dialog = new ScrapeDialog(appsToUpdate))
				{
					dialog.ShowDialog();
				}
			}

			SaveDatabase(true);
		}

		public void ClearStatus()
		{
			_statusBuilder.Clear();
		}

		public void FlushStatus()
		{
			mlblStatusMsg.Font = new Font("Arial", 9);
			mlblStatusMsg.Text = _statusBuilder.ToString();
			_statusBuilder.Clear();
		}

		#endregion

		#region Methods

		/// <summary>
		///     Creates the  text for a Category entry.
		/// </summary>
		/// <param name="category">Category to get the display text of</param>
		/// <returns>Display text for the provided Category information</returns>
		private static string CategoryListViewItemText(Category category)
		{
			return CategoryListViewItemText(category.Name, category.Count);
		}

		/// <summary>
		///     Creates the  text for a Category entry.
		/// </summary>
		/// <param name="categoryName">Name of the Category</param>
		/// <param name="categoryCount">Count of the Category</param>
		/// <returns>Display text for the provided Category information</returns>
		private static string CategoryListViewItemText(string categoryName, int categoryCount)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} ({1})", categoryName, categoryCount);
		}

		private static void ChangeLanguageControls(Control control, ComponentResourceManager resourceManager, CultureInfo cultureInfo)
		{
			if (control == null)
			{
				return;
			}

			Rectangle currentBounds = control.Bounds;
			if (control.GetType() == typeof(MenuStrip))
			{
				foreach (ToolStripDropDownItem toolStripItem in ((MenuStrip) control).Items)
				{
					ChangeLanguageToolStripItems(toolStripItem, resourceManager, cultureInfo);
				}
			}
			else if (control is ListView)
			{
				foreach (ColumnHeader columnHeader in ((ListView) control).Columns)
				{
					if (columnHeader.Tag != null)
					{
						resourceManager.ApplyResources(columnHeader, columnHeader.Tag.ToString(), cultureInfo);
					}
				}
			}
			else
			{
				foreach (Control childControl in control.Controls)
				{
					ChangeLanguageControls(childControl, resourceManager, cultureInfo);
				}
			}

			resourceManager.ApplyResources(control, control.Name, cultureInfo);
			control.Bounds = currentBounds;
		}

		private static void ChangeLanguageToolStripItems(ToolStripItem item, ComponentResourceManager resources, CultureInfo newCulture)
		{
			if (item == null)
			{
				return;
			}

			if (item is ToolStripDropDownItem toolStripItem)
			{
				foreach (ToolStripItem childItem in toolStripItem.DropDownItems)
				{
					ChangeLanguageToolStripItems(childItem, resources, newCulture);
				}
			}

			resources.ApplyResources(item, item.Name, newCulture);
		}

		private static void CheckForDepressurizerUpdates()
		{
			string rawJson;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					webClient.Headers.Set("User-Agent", "Depressurizer");
					rawJson = webClient.DownloadString(Constants.LatestReleaseURL);
				}

				if (string.IsNullOrWhiteSpace(rawJson))
				{
					throw new InvalidDataException();
				}
			}
			catch (Exception e)
			{
				SentryLogger.Log(e);
				MessageBox.Show(Resources.Error_DepressurizerUpdate + Environment.NewLine + e.Message, Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			Version installedVersion = Assembly.GetExecutingAssembly().GetName().Version;
			try
			{
				JObject parsedJson = JObject.Parse(rawJson);

				Version latestVersion = new Version(((string) parsedJson.SelectToken("tag_name")).Replace("v", ""));
				string url = (string) parsedJson.SelectToken("html_url");

				if (latestVersion <= installedVersion)
				{
					return;
				}

				if (MessageBox.Show(Resources.Message_DepressurizerUpdate, Resources.Message_DepressurizerUpdate_Title, MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					Process.Start(url);
				}
			}
			catch (Exception e)
			{
				SentryLogger.Log(e);
				MessageBox.Show(Resources.Error_DepressurizerUpdate + Environment.NewLine + e.Message, Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private static ListViewItem CreateCategoryListViewItem(Category category)
		{
			return new ListViewItem(CategoryListViewItemText(category))
			{
				Tag = category,
				Name = category.Name
			};
		}

		private static void LaunchGame(GameInfo gameInfo)
		{
			if (gameInfo == null)
			{
				return;
			}

			gameInfo.LastPlayed = Utility.CurrentUnixTime();
			Process.Start(gameInfo.Executable);
		}

		private static void ResortToolStripItemCollection(ToolStripItemCollection coll)
		{
			ArrayList oAList = new ArrayList(coll);
			oAList.Sort(new ToolStripItemComparer());
			coll.Clear();

			foreach (ToolStripItem oItem in oAList)
			{
				coll.Add(oItem);
			}
		}

		private static void SetDragDropEffect(DragEventArgs e)
		{
			if (Settings.SingleCatMode /*|| (e.KeyState & 4) == 4*/)
			{
				// Commented segment: SHIFT
				e.Effect = DragDropEffects.Move;
			}
			else if ((e.KeyState & 8) == 8)
			{
				// CTRL
				e.Effect = DragDropEffects.Link;
			}
			else
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private static bool ValidateCategoryName(string name)
		{
			if (!string.IsNullOrWhiteSpace(name))
			{
				return true;
			}

			MessageBox.Show(GlobalStrings.MainForm_CategoryNamesNotEmpty, GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

			return false;
		}

		private static bool ValidateFilterName(string name)
		{
			if (!string.IsNullOrWhiteSpace(name))
			{
				return true;
			}

			MessageBox.Show(GlobalStrings.MainForm_FilterNamesNotEmpty, GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

			return false;
		}

		private void AddCategoryToSelectedGames(Category category, bool forceClearOthers)
		{
			if (lstGames.SelectedObjects.Count <= 0)
			{
				return;
			}

			Cursor.Current = Cursors.WaitCursor;

			foreach (GameInfo gameInfo in _tlstGames.SelectedObjects)
			{
				if (gameInfo == null)
				{
					continue;
				}

				if (forceClearOthers || Settings.SingleCatMode)
				{
					gameInfo.ClearCategories();
					if (category != null)
					{
						gameInfo.AddCategory(category);
					}
				}
				else
				{
					gameInfo.AddCategory(category);
				}
			}

			FillAllCategoryLists();

			if (forceClearOthers)
			{
				FilterGamelist(false);
			}

			if (lstCategories.SelectedItems[0].Tag.ToString() == Resources.Category_Uncategorized)
			{
				FilterGamelist(false);
			}
			else
			{
				RebuildGamelist();
			}

			MakeChange(true);

			Cursor.Current = Cursors.Default;
		}

		private void AddGame()
		{
			using (DlgGame dialog = new DlgGame(CurrentProfile.GameData))
			{
				if (dialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}

				Cursor.Current = Cursors.WaitCursor;

				if (ProfileLoaded)
				{
					if (CurrentProfile.IgnoreList.Remove(dialog.Game.Id))
					{
						AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_UnignoredGame, dialog.Game.Id));
					}
				}

				FullListRefresh();
				MakeChange(true);
				AddStatus(GlobalStrings.MainForm_AddedGame);

				Cursor.Current = Cursors.Default;
			}
		}

		private void AddGameToMultiCatCheckStates(GameInfo gameInfo, bool first)
		{
			foreach (ListViewItem listViewItem in lstMultiCat.Items)
			{
				if (listViewItem.StateImageIndex == 2)
				{
					continue;
				}

				if (!(listViewItem.Tag is Category category))
				{
					continue;
				}

				if (first)
				{
					listViewItem.StateImageIndex = gameInfo.ContainsCategory(category) ? 1 : 0;
				}
				else
				{
					if (gameInfo.ContainsCategory(category))
					{
						if (listViewItem.StateImageIndex == 0)
						{
							listViewItem.StateImageIndex = 2;
						}
					}
					else
					{
						if (listViewItem.StateImageIndex == 1)
						{
							listViewItem.StateImageIndex = 2;
						}
					}
				}
			}
		}

		/// <summary>
		///     Ensures the context menu contains an entry to remove any category the given game is assigned to.
		///     <para />
		///     Inserted entries will be placed at the end.
		/// </summary>
		private void AddRemoveCategoryContextMenu(GameInfo game)
		{
			if ((game == null) || (game.Categories == null))
			{
				return;
			}

			foreach (Category category in game.Categories)
			{
				AddRemoveCategoryContextMenu(category);
			}
		}

		/// <summary>
		///     Append an entry to the end of the context menu to remove the given category from the selected games.
		///     <para />
		///     No-op if there is already a button for the category.
		/// </summary>
		/// <param name="category">Category to add item for</param>
		private void AddRemoveCategoryContextMenu(Category category)
		{
			if (category == null)
			{
				return;
			}

			if (contextGameRemCat.Items.ContainsKey(category.Name))
			{
				return;
			}

			ToolStripItem item = contextGameRemCat.Items.Add(category.Name);
			item.Tag = category;
			item.Name = category.Name;
			item.Click += contextGameRemCat_Category_Click;
		}

		private void ApplyFilter(Filter f)
		{
			if (!AdvancedCategoryFilter)
			{
				return;
			}

			// reset Advanced settings
			_advFilter = new Filter(AdvancedFilter);

			// load new Advanced settings
			foreach (ListViewItem i in lstCategories.Items)
			{
				if (i.Tag.ToString() == Resources.Category_Games)
				{
					i.StateImageIndex = f.Game;
					_advFilter.Game = f.Game;
				}
				else if (i.Tag.ToString() == Resources.Category_Software)
				{
					i.StateImageIndex = f.Software;
					_advFilter.Software = f.Software;
				}
				else if (i.Tag.ToString() == Resources.Category_Uncategorized)
				{
					i.StateImageIndex = f.Uncategorized;
					_advFilter.Uncategorized = f.Uncategorized;
				}
				else if (i.Tag.ToString() == Resources.Category_Hidden)
				{
					i.StateImageIndex = f.Hidden;
					_advFilter.Hidden = f.Hidden;
				}
				else if (i.Tag.ToString() == Resources.Category_VR)
				{
					i.StateImageIndex = f.VR;
					_advFilter.VR = f.VR;
				}
				else
				{
					if (f.Allow.Contains((Category) i.Tag))
					{
						i.StateImageIndex = (int) AdvancedFilterState.Allow;
						_advFilter.Allow.Add((Category) i.Tag);
					}
					else if (f.Require.Contains((Category) i.Tag))
					{
						i.StateImageIndex = (int) AdvancedFilterState.Require;
						_advFilter.Require.Add((Category) i.Tag);
					}
					else if (f.Exclude.Contains((Category) i.Tag))
					{
						i.StateImageIndex = (int) AdvancedFilterState.Exclude;
						_advFilter.Exclude.Add((Category) i.Tag);
					}
					else
					{
						i.StateImageIndex = (int) AdvancedFilterState.None;
					}
				}
			}

			OnViewChange();
		}

		private void AssignFavoriteToSelectedGames(bool isFavorite)
		{
			if (lstGames.SelectedObjects.Count <= 0)
			{
				return;
			}

			Cursor.Current = Cursors.WaitCursor;

			foreach (GameInfo gameInfo in _tlstGames.SelectedObjects)
			{
				gameInfo.SetFavorite(isFavorite);
			}

			FillCategoryList();
			RebuildGamelist();
			MakeChange(true);

			Cursor.Current = Cursors.Default;
		}

		private void AssignHiddenToSelectedGames(bool hidden)
		{
			if (lstGames.SelectedObjects.Count <= 0)
			{
				return;
			}

			Cursor.Current = Cursors.WaitCursor;

			foreach (GameInfo gameInfo in _tlstGames.SelectedObjects)
			{
				gameInfo.SetHidden(hidden);
			}

			FillCategoryList();
			FilterGamelist(false);
			MakeChange(true);

			Cursor.Current = Cursors.Default;
		}

		private void Autocategorize(bool selectedOnly, AutoCat autoCat, bool scrape = true, bool refresh = true)
		{
			if (autoCat == null)
			{
				return;
			}

			Cursor.Current = Cursors.WaitCursor;

			// Get a list of games to update
			List<GameInfo> gamesToUpdate = new List<GameInfo>();

			if (selectedOnly && (autoCat.Filter == null))
			{
				gamesToUpdate.AddRange(_tlstGames.SelectedObjects.Where(g => (g != null) && (g.Id > 0)));
			}
			else if ((_tlstGames.Objects.Count > 0) && (autoCat.Filter == null))
			{
				gamesToUpdate.AddRange(_tlstGames.Objects.Where(g => (g != null) && (g.Id > 0)));
			}
			else
			{
				gamesToUpdate.AddRange(CurrentProfile.GameData.Games.Values.Where(g => (g != null) && (g.Id > 0)));
			}

			int updated = 0;

			// List of games not found in database or that have old data, so we can try to scrape data for them
			List<int> notInDbOrOldData = new List<int>();
			int oldDbDataCount = 0;
			int notInDbCount = 0;
			foreach (GameInfo game in gamesToUpdate)
			{
				if ((game.Id > 0) && (!Database.Contains(game.Id) || (Database.Games[game.Id].LastStoreScrape == 0)))
				{
					notInDbOrOldData.Add(game.Id);
					notInDbCount++;
				}
				else if ((game.Id > 0) && (Utility.CurrentUnixTime() > (Database.Games[game.Id].LastStoreScrape + (Settings.ScrapePromptDays * 86400)))) //86400 seconds in a day
				{
					notInDbOrOldData.Add(game.Id);
					oldDbDataCount++;
				}
			}

			if (((notInDbCount > 0) || (oldDbDataCount > 0)) && scrape)
			{
				Cursor.Current = Cursors.Default;
				string message = "";
				message += notInDbCount > 0 ? string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_GamesNotFoundInGameDB, notInDbCount) : "";
				if ((notInDbCount > 0) && (oldDbDataCount > 0))
				{
					message += " " + GlobalStrings.Text_And + " ";
				}

				message += oldDbDataCount > 0 ? string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_GamesHaveOldDataInGameDB, oldDbDataCount, Settings.ScrapePromptDays) : "";
				message += ". " + GlobalStrings.MainForm_ScrapeNow;

				if (MessageBox.Show(message, GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					using (ScrapeDialog dialog = new ScrapeDialog(notInDbOrOldData))
					{
						DialogResult result = dialog.ShowDialog();

						if (result == DialogResult.Cancel)
						{
							AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_CanceledDatabaseUpdate));
						}
						else
						{
							AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_UpdatedDatabaseEntries, dialog.TotalJobs));
							if (dialog.TotalJobs > 0)
							{
								SaveDatabase();
							}
						}
					}
				}

				Cursor.Current = Cursors.WaitCursor;
			}

			autoCat.PreProcess(CurrentProfile.GameData, Database);

			foreach (GameInfo g in gamesToUpdate)
			{
				AutoCatResult res = autoCat.CategorizeGame(g, CurrentProfile.GameData.GetFilter(autoCat.Filter));
				if (res == AutoCatResult.Success)
				{
					updated++;
				}
			}

			autoCat.DeProcess();
			AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_UpdatedCategories, updated));
			if (gamesToUpdate.Count > updated)
			{
				AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_FailedToUpdate, gamesToUpdate.Count - updated));
			}

			if (updated > 0)
			{
				MakeChange(true);
			}

			if (refresh)
			{
				FillAllCategoryLists();
				FilterGamelist(true);
			}

			Cursor.Current = Cursors.Default;
		}

		private int AutoCatGameCount()
		{
			// Get a count of games to update
			int count = 0;

			if (mchkAutoCatSelected.Checked)
			{
				count += _tlstGames.SelectedObjects.Count(g => (g != null) && (g.Id > 0));
			}
			else if (_tlstGames.Objects.Count > 0)
			{
				count += _tlstGames.Objects.Count(g => (g != null) && (g.Id > 0));
			}
			else
			{
				if (CurrentProfile == null)
				{
					return count;
				}

				count += CurrentProfile.GameData.Games.Values.Count(g => (g != null) && (g.Id > 0));
			}

			return count;
		}

		private void AutonameAll()
		{
			DialogResult res = MessageBox.Show(GlobalStrings.MainForm_OverwriteExistingNames, GlobalStrings.MainForm_Overwrite, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			bool overwrite = false;

			if (res == DialogResult.Cancel)
			{
				AddStatus(GlobalStrings.MainForm_AutonameCanceled);

				return;
			}

			if (res == DialogResult.Yes)
			{
				overwrite = true;
			}

			Cursor.Current = Cursors.WaitCursor;

			int named = 0;
			foreach (GameInfo g in CurrentProfile.GameData.Games.Values)
			{
				if (overwrite || string.IsNullOrEmpty(g.Name))
				{
					g.Name = Database.GetName(g.Id);
					named++;
				}
			}

			AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_AutonamedGames, named));
			if (named > 0)
			{
				MakeChange(true);
			}

			RebuildGamelist();

			Cursor.Current = Cursors.Default;
		}

		private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((cboFilter.SelectedItem != null) && AdvancedCategoryFilter)
			{
				ApplyFilter((Filter) cboFilter.SelectedItem);
			}
		}

		private bool CheckForUnsaved()
		{
			if (!ProfileLoaded || !_unsavedChanges)
			{
				return true;
			}

			using (CloseDialog dialog = new CloseDialog(GlobalStrings.MainForm_UnsavedChangesWillBeLost, GlobalStrings.MainForm_UnsavedChanges, SystemIcons.Warning.ToBitmap(), true, CurrentProfile.AutoExport))
			{
				DialogResult result = dialog.ShowDialog();

				if (result == DialogResult.No)
				{
					return true;
				}

				if (result == DialogResult.Cancel)
				{
					return false;
				}

				CurrentProfile.AutoExport = dialog.Export;
			}

			return SaveProfile();
		}

		private void cmdAddCatAndAssign_Click(object sender, EventArgs e)
		{
			if (!ValidateCategoryName(txtAddCatAndAssign.Text))
			{
				return;
			}

			Category category = CurrentProfile.GameData.GetCategory(txtAddCatAndAssign.Text);
			AddCategoryToSelectedGames(category, false);
			txtAddCatAndAssign.Clear();
		}

		private void cmdGameAdd_Click(object sender, EventArgs e)
		{
			ClearStatus();
			AddGame();
			FlushStatus();
		}

		private void cmdGameEdit_Click(object sender, EventArgs e)
		{
			ClearStatus();
			EditGame();
			FlushStatus();
		}

		private void cmdGameLaunch_Click(object sender, EventArgs e)
		{
			ClearStatus();
			if (lstGames.SelectedObjects.Count > 0)
			{
				LaunchGame(_tlstGames.SelectedObjects[0]);
			}

			FlushStatus();
		}

		private void cmdGameRemove_Click(object sender, EventArgs e)
		{
			ClearStatus();
			RemoveGames();
			FlushStatus();
		}

		private void ColumnReorderWorker()
		{
			ReorderFillerColumn();
		}

		private void contectCat_RemoveEmpty_Click(object sender, EventArgs e)
		{
			ClearStatus();
			RemoveEmptyCats();
			FlushStatus();
		}

		private void contextAutoCat_Edit_Click(object sender, EventArgs e)
		{
			ClearStatus();
			AutoCat selected = null;
			if (lvAutoCatType.SelectedItems.Count > 0)
			{
				selected = (AutoCat) lvAutoCatType.SelectedItems[0].Tag;
			}
			else if (lvAutoCatType.CheckedItems.Count > 0)
			{
				selected = (AutoCat) lvAutoCatType.CheckedItems[0].Tag;
			}
			else
			{
				if (lvAutoCatType.Items.Count > 0)
				{
					selected = (AutoCat) lvAutoCatType.Items[0].Tag;
				}
			}

			EditAutoCats(selected);
			FlushStatus();
		}

		private void contextCat_Opening(object sender, CancelEventArgs e)
		{
			bool selectedCat = (lstCategories.SelectedItems.Count > 0) && (lstCategories.SelectedItems[0].Tag != null);
			contextCat_Delete.Enabled = contextCat_Rename.Enabled = selectedCat;
		}

		private void contextCat_SetAdvanced_Allow_Click(object sender, EventArgs e)
		{
			if (lstCategories.SelectedItems.Count <= 0)
			{
				return;
			}

			foreach (ListViewItem i in lstCategories.SelectedItems)
			{
				SetItemState(i, (int) AdvancedFilterState.Allow);
			}

			OnViewChange();
		}

		private void contextCat_SetAdvanced_Exclude_Click(object sender, EventArgs e)
		{
			if (lstCategories.SelectedItems.Count <= 0)
			{
				return;
			}

			foreach (ListViewItem i in lstCategories.SelectedItems)
			{
				SetItemState(i, (int) AdvancedFilterState.Exclude);
			}

			OnViewChange();
		}

		private void contextCat_SetAdvanced_None_Click(object sender, EventArgs e)
		{
			if (lstCategories.SelectedItems.Count <= 0)
			{
				return;
			}

			foreach (ListViewItem i in lstCategories.SelectedItems)
			{
				SetItemState(i, (int) AdvancedFilterState.None);
			}

			OnViewChange();
		}

		private void contextCat_SetAdvanced_Require_Click(object sender, EventArgs e)
		{
			if (lstCategories.SelectedItems.Count > 0)
			{
				foreach (ListViewItem i in lstCategories.SelectedItems)
				{
					SetItemState(i, (int) AdvancedFilterState.Require);
				}

				OnViewChange();
			}
		}

		private void contextGame_Opening(object sender, CancelEventArgs e)
		{
			bool selectedGames = lstGames.SelectedObjects.Count > 0;
			contextGame_Edit.Enabled = selectedGames;
			contextGame_Remove.Enabled = selectedGames;
			contextGame_AddCat.Enabled = selectedGames;
			contextGame_RemCat.Enabled = selectedGames && (contextGameRemCat.Items.Count > 0);
			contextGame_SetFav.Enabled = selectedGames;
			contextGame_VisitStore.Enabled = selectedGames;
			contextGame_LaunchGame.Enabled = selectedGames;
		}

		private void contextGame_SetFav_No_Click(object sender, EventArgs e)
		{
			ClearStatus();
			AssignFavoriteToSelectedGames(false);
			FlushStatus();
		}

		private void contextGame_SetFav_Yes_Click(object sender, EventArgs e)
		{
			ClearStatus();
			AssignFavoriteToSelectedGames(true);
			FlushStatus();
		}

		private void contextGame_VisitStore_Click(object sender, EventArgs e)
		{
			if (lstGames.SelectedObjects.Count > 0)
			{
				Steam.LaunchStorePage(_tlstGames.SelectedObjects[0].Id);
			}
		}

		private void contextGameAddCat_Category_Click(object sender, EventArgs e)
		{
			ToolStripItem menuItem = sender as ToolStripItem;
			if (menuItem != null)
			{
				ClearStatus();
				Category c = menuItem.Tag as Category;
				QuickAddCategoryToSelectedGames(c);
				FlushStatus();
			}
		}

		private void contextGameAddCat_Create_Click(object sender, EventArgs e)
		{
			Category c = CreateCategory();
			if (c != null)
			{
				ClearStatus();
				QuickAddCategoryToSelectedGames(c);
				FlushStatus();
			}
		}

		private void contextGameHidden_No_Click(object sender, EventArgs e)
		{
			ClearStatus();
			AssignHiddenToSelectedGames(false);
			FlushStatus();
		}

		private void contextGameHidden_Yes_Click(object sender, EventArgs e)
		{
			ClearStatus();
			AssignHiddenToSelectedGames(true);
			FlushStatus();
		}

		private void contextGameRemCat_Category_Click(object sender, EventArgs e)
		{
			ToolStripItem menuItem = sender as ToolStripItem;
			if (menuItem != null)
			{
				ClearStatus();
				Category c = menuItem.Tag as Category;
				QuickRemoveCategoryFromSelectedGames(c);
				FlushStatus();
			}
		}

		private void countascendingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			lstCategories.ListViewItemSorter = new ListCategoriesComparer(CategorySortMode.Count, SortOrder.Ascending);
			lstCategories.Sort();
		}

		private void countdescendingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			lstCategories.ListViewItemSorter = new ListCategoriesComparer(CategorySortMode.Count, SortOrder.Descending);
			lstCategories.Sort();
		}

		private Category CreateCategory()
		{
			if (!ProfileLoaded)
			{
				return null;
			}

			using (GetStringDlg dialog = new GetStringDlg(string.Empty, GlobalStrings.MainForm_CreateCategory, GlobalStrings.MainForm_EnterNewCategoryName, GlobalStrings.MainForm_Create))
			{
				if ((dialog.ShowDialog() != DialogResult.OK) || !ValidateCategoryName(dialog.Value))
				{
					return null;
				}

				Category newCategory = CurrentProfile.GameData.AddCategory(dialog.Value);
				if (newCategory != null)
				{
					FillAllCategoryLists();
					MakeChange(true);
					AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_CategoryAdded, newCategory.Name));

					return newCategory;
				}

				MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_CouldNotAddCategory, dialog.Value), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			return null;
		}

		private void CreateProfile()
		{
			using (DlgProfile dialog = new DlgProfile())
			{
				DialogResult result = dialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					Cursor = Cursors.WaitCursor;

					CurrentProfile = dialog.Profile;
					AddStatus(GlobalStrings.MainForm_ProfileCreated);

					if (dialog.DownloadNow)
					{
						UpdateLibrary();
					}

					if (dialog.ImportNow)
					{
						ImportConfig();
					}

					if (dialog.SetStartup)
					{
						Settings.StartupAction = StartupAction.LoadProfile;
						Settings.ProfileToLoad = CurrentProfile.FilePath;
						Settings.Save();
					}

					FullListRefresh();

					Cursor = Cursors.Default;
				}
			}

			OnProfileChange();
		}

		private void DeleteCategory()
		{
			List<Category> toDelete = new List<Category>();
			foreach (ListViewItem item in lstCategories.SelectedItems)
			{
				if (item.Tag is Category c && (c != CurrentProfile.GameData.FavoriteCategory))
				{
					toDelete.Add(c);
				}
			}

			if (toDelete.Count <= 0)
			{
				return;
			}

			{
				DialogResult res = MessageBox.Show(toDelete.Count == 1 ? string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_DeleteCategory, toDelete[0].Name) : string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_DeleteCategoryMulti, toDelete.Count), GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (res != DialogResult.Yes)
				{
					return;
				}

				int deleted = 0;
				foreach (Category c in toDelete)
				{
					if (CurrentProfile.GameData.RemoveCategory(c))
					{
						deleted++;
					}
				}

				if (deleted > 0)
				{
					FillAllCategoryLists();
					RebuildGamelist();
					MakeChange(true);
					AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_CategoryDeleted, deleted));
				}
				else
				{
					MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_CouldNotDeleteCategory), GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}

		private void DeleteFilter(Filter f)
		{
			if (!ProfileLoaded || !AdvancedCategoryFilter)
			{
				return;
			}

			DialogResult res = MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_DeleteFilter, f.Name), GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if (res != DialogResult.Yes)
			{
				return;
			}

			try
			{
				CurrentProfile.GameData.Filters.Remove(f);
				AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_FilterDeleted, f.Name));
				RefreshFilters();
			}
			catch
			{
				MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_CouldNotDeleteFilter), GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void EditAutoCats(AutoCat selected)
		{
			if (!ProfileLoaded)
			{
				return;
			}

			using (DlgAutoCat dialog = new DlgAutoCat(CurrentProfile.AutoCats, CurrentProfile.GameData, selected, CurrentProfile.FilePath))
			{
				DialogResult result = dialog.ShowDialog();

				if (result != DialogResult.OK)
				{
					return;
				}

				CurrentProfile.AutoCats = dialog.AutoCatList;
				MakeChange(true);
				FillAutoCatLists();
			}
		}

		private void EditGame()
		{
			if (lstGames.SelectedObjects.Count <= 0)
			{
				return;
			}

			GameInfo gameInfo = _tlstGames.SelectedObjects[0];
			using (DlgGame dialog = new DlgGame(CurrentProfile.GameData, gameInfo))
			{
				if (dialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}

				Cursor.Current = Cursors.WaitCursor;

				FilterGamelist(false);
				MakeChange(true);
				AddStatus(GlobalStrings.MainForm_EditedGame);

				Cursor.Current = Cursors.Default;
			}
		}

		private void EditProfile()
		{
			if (!ProfileLoaded)
			{
				if (MessageBox.Show(GlobalStrings.MainForm_NoProfileLoaded, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				{
					CreateProfile();
				}

				return;
			}

			using (DlgProfile dialog = new DlgProfile(CurrentProfile))
			{
				if (dialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}

				Cursor = Cursors.WaitCursor;

				AddStatus(GlobalStrings.MainForm_ProfileEdited);
				MakeChange(true);

				bool refresh = false;
				if (dialog.DownloadNow)
				{
					UpdateLibrary();
					refresh = true;
				}

				if (dialog.ImportNow)
				{
					ImportConfig();
					refresh = true;
				}

				if (dialog.SetStartup)
				{
					Settings.StartupAction = StartupAction.LoadProfile;
					Settings.ProfileToLoad = CurrentProfile.FilePath;
					Settings.Save();
				}

				Cursor = Cursors.Default;

				if (refresh)
				{
					FullListRefresh();
				}
			}
		}

		private void ExportConfig()
		{
			if (CurrentProfile == null)
			{
				return;
			}

			try
			{
				CurrentProfile.ExportSteamData();
				AddStatus(GlobalStrings.MainForm_ExportedCategories);
			}
			catch (Exception e)
			{
				MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Msg_ErrorExportingToSteam, e.Message), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Logger.Instance.Exception(GlobalStrings.MainForm_Log_ExceptionExport, e);
				AddStatus(GlobalStrings.MainForm_ExportFailed);
			}
		}

		private void FillAllCategoryLists()
		{
			Cursor = Cursors.WaitCursor;
			contextGameAddCat.Items.Clear();
			contextGameAddCat.Items.Add(contextGameAddCat_Create);
			contextGameRemCat.Items.Clear();
			lstMultiCat.Items.Clear();

			if (!ProfileLoaded)
			{
				lstCategories.Items.Clear();

				return;
			}

			CurrentProfile.GameData.Categories.Sort();

			FillCategoryList();

			lstMultiCat.BeginUpdate();
			foreach (Category c in CurrentProfile.GameData.Categories)
			{
				if (c != CurrentProfile.GameData.FavoriteCategory)
				{
					ToolStripItem item = contextGame_AddCat.DropDownItems.Add(c.Name);
					item.Tag = c;
					item.Click += contextGameAddCat_Category_Click;

					//item = contextGameRemCat.Items.Add( c.Name );
					//item.Tag = c;
					//item.Click += contextGameRemCat_Category_Click;

					ListViewItem listItem = new ListViewItem(c.Name);
					listItem.Tag = c;
					listItem.Name = c.Name;
					listItem.StateImageIndex = 0;
					lstMultiCat.Items.Add(listItem);
				}
			}

			UpdateGameCheckStates();
			lstMultiCat.EndUpdate();
			mlblCategoryCount.Font = new Font("Arial", 8);
			mlblCategoryCount.Text = lstCategories.Items.Count + " Categories";
			Cursor = Cursors.Default;
		}

		private void FillAutoCatLists()
		{
			lvAutoCatType.Items.Clear();

			// Prepare main menu list
			menu_Tools_Autocat_List.Items.Clear();

			if (CurrentProfile != null)
			{
				foreach (AutoCat autoCat in CurrentProfile.AutoCats)
				{
					if (autoCat == null)
					{
						continue;
					}

					// Fill main screen dropdown
					ListViewItem listViewItem = new ListViewItem(autoCat.DisplayName)
					{
						Tag = autoCat,
						Name = autoCat.Name,
						Checked = autoCat.Selected
					};

					lvAutoCatType.Items.Add(listViewItem);

					//SelectAutoCats();

					// Fill main menu list
					ToolStripItem item = menu_Tools_Autocat_List.Items.Add(autoCat.DisplayName);
					item.Tag = autoCat;
					item.Name = autoCat.Name;
					item.Click += menuToolsAutocat_Item_Click;
				}
			}

			// Finish main menu list
			menu_Tools_AutocatAll.Enabled = menu_Tools_Autocat_List.Items.Count > 0;
		}

		private void FillCategoryList()
		{
			object selected = lstCategories.SelectedItems.Count > 0 ? lstCategories.SelectedItems[0].Tag : null;
			int selectedIndex = lstCategories.SelectedItems.Count > 0 ? lstCategories.SelectedIndices[0] : -1;

			lstCategories.Items.Clear();

			if (!ProfileLoaded)
			{
				return;
			}

			CurrentProfile.GameData.Categories.Sort();

			lstCategories.BeginUpdate();
			lstCategories.Items.Clear();

			int hidden = 0;
			int uncategorized = 0;
			int vr = 0;
			int games = 0;
			int software = 0;
			foreach (GameInfo g in CurrentProfile.GameData.Games.Values)
			{
				if (!Database.Games.ContainsKey(g.Id))
				{
					continue;
				}

				DatabaseEntry entry = Database.Games[g.Id];
				if (g.Hidden)
				{
					hidden++;
				}
				else if (!g.HasCategories())
				{
					uncategorized++;
				}

				if (Database.SupportsVR(g.Id) && !g.Hidden)
				{
					vr++;
				}

				if (entry.AppType == AppType.Game)
				{
					games++;
				}

				if (entry.AppType == AppType.Application)
				{
					software++;
				}
			}

			ListViewItem listViewItem;
			if (!AdvancedCategoryFilter)
			{
				// <All>
				listViewItem = new ListViewItem(CategoryListViewItemText(Resources.Category_All, games + software))
				{
					Tag = Resources.Category_All,
					Name = Resources.Category_All
				};

				lstCategories.Items.Add(listViewItem);
			}

			// <Games>
			listViewItem = new ListViewItem(CategoryListViewItemText(Resources.Category_Games, games))
			{
				Tag = Resources.Category_Games,
				Name = Resources.Category_Games
			};

			lstCategories.Items.Add(listViewItem);

			// <Software>
			listViewItem = new ListViewItem(CategoryListViewItemText(Resources.Category_Software, software))
			{
				Tag = Resources.Category_Software,
				Name = Resources.Category_Software
			};

			lstCategories.Items.Add(listViewItem);

			// <Uncategorized>
			listViewItem = new ListViewItem(CategoryListViewItemText(Resources.Category_Uncategorized, uncategorized))
			{
				Tag = Resources.Category_Uncategorized,
				Name = Resources.Category_Uncategorized
			};

			lstCategories.Items.Add(listViewItem);

			// <Hidden>
			listViewItem = new ListViewItem(CategoryListViewItemText(Resources.Category_Hidden, hidden))
			{
				Tag = Resources.Category_Hidden,
				Name = Resources.Category_Hidden
			};

			lstCategories.Items.Add(listViewItem);

			// <VR>
			listViewItem = new ListViewItem(CategoryListViewItemText(Resources.Category_VR, vr))
			{
				Tag = Resources.Category_VR,
				Name = Resources.Category_VR
			};

			lstCategories.Items.Add(listViewItem);

			foreach (Category c in CurrentProfile.GameData.Categories)
			{
				ListViewItem l = CreateCategoryListViewItem(c);
				lstCategories.Items.Add(l);
			}

			if (selected == null)
			{
				lstCategories.SelectedIndices.Add(selectedIndex >= 0 ? selectedIndex : 0);
			}
			else
			{
				for (int i = 0; i < lstCategories.Items.Count; i++)
				{
					if (lstCategories.Items[i].Tag == selected)
					{
						lstCategories.SelectedIndices.Add(i);

						break;
					}
				}
			}

			// if (sort)
			if (lstCategories.ListViewItemSorter == null)
			{
				lstCategories.ListViewItemSorter = new ListCategoriesComparer(CategorySortMode.Name, SortOrder.Ascending);
			}

			lstCategories.Sort();
			lstCategories.EndUpdate();
		}

		private void FillGameList()
		{
			Cursor = Cursors.WaitCursor;

			List<GameInfo> gamelist = new List<GameInfo>();
			List<int> appIds = new List<int>();

			if (CurrentProfile != null)
			{
				foreach (GameInfo g in CurrentProfile.GameData.Games.Values)
				{
					if ((g.Id < 0) && !CurrentProfile.IncludeShortcuts)
					{
						continue;
					}

					if (string.IsNullOrWhiteSpace(g.Name))
					{
						g.Name = string.Empty;
					}

					gamelist.Add(g);
					appIds.Add(g.Id);
				}
			}

			Steam.GrabBanners(appIds);

			lstGames.SetObjects(gamelist);
			lstGames.BuildList();

			mbtnAutoCategorize.Text = string.Format(CultureInfo.CurrentCulture, "Auto-Categorize ({0} Games)", AutoCatGameCount());

			Cursor = Cursors.Default;
		}

		private void FilterGamelist(bool preserveSelection)
		{
			Cursor = Cursors.WaitCursor;

			lstGames.BeginUpdate();
			if (!preserveSelection)
			{
				lstGames.DeselectAll();
			}

			lstGames.UpdateColumnFiltering();
			lstGames.BuildList();
			lstGames.EndUpdate();

			Cursor = Cursors.Default;
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.SelectedFilter = AdvancedCategoryFilter ? cboFilter.Text : string.Empty;

			if (lstCategories.SelectedItems.Count > 0)
			{
				Settings.SelectedCategory = lstCategories.SelectedItems[0].Name;
			}

			SaveSelectedAutoCats();

			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = !CheckForUnsaved();
			}
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			// allow mousewheel scrolling context menus, e.g. for Add Category submenu.
			DropdownMenuScrollWheelHandler.Enable();

			ttHelp.Ext_SetToolTip(mchkAdvancedCategories, GlobalStrings.MainForm_Help_AdvancedCategories);

			InitializeObjectListView();

			ClearStatus();

			/* */

			Database.Load();

			if (Settings.CheckForUpdates)
			{
				CheckForDepressurizerUpdates();
			}

			if (string.IsNullOrWhiteSpace(Settings.SteamPath))
			{
				using (SteamPathDialog dialog = new SteamPathDialog())
				{
					dialog.ShowDialog();

					Settings.SteamPath = dialog.Path;
					Settings.Save();
				}
			}

			if (Settings.OnStartUpdateFromAppInfo)
			{
				UpdateDatabaseFromAppInfo();
			}

			const int aWeekInSecs = 7 * 24 * 60 * 60;
			if (Settings.OnStartUpdateFromHLTB && (Utility.CurrentUnixTime() > (Database.LastHLTBUpdate + aWeekInSecs)))
			{
				UpdateDatabaseFromHLTB();
			}

			switch (Settings.StartupAction)
			{
				case StartupAction.LoadProfile:
					LoadProfile(Settings.ProfileToLoad, false);
					break;
				case StartupAction.CreateProfile:
					CreateProfile();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			ChangeDatabaseLanguage(Settings.StoreLanguage);

			UpdateUiForSingleCat();
			UpdateEnabledStatesForGames();
			UpdateEnabledStatesForCategories();

			if (CurrentProfile != null)
			{
				SelectCategory();
				SelectFilter();
				SelectAutoCats();
			}

			FlushStatus();
		}

		private void FullListRefresh()
		{
			FillAllCategoryLists();
			FillGameList();
		}

		private ListViewItem GetCategoryItemAtPoint(int x, int y)
		{
			Point clientPoint = lstCategories.PointToClient(new Point(x, y));

			return lstCategories.GetItemAt(clientPoint.X, clientPoint.Y);
		}

		private void HandleAdvancedCategoryItemActivation(ListViewItem i, bool reverse, bool updateView = true)
		{
			int oldState = i.StateImageIndex;

			if ((i.StateImageIndex == -1) && reverse)
			{
				i.StateImageIndex = MaxFilterState;
			}
			else if ((i.StateImageIndex == MaxFilterState) && !reverse)
			{
				i.StateImageIndex = -1;
			}
			else
			{
				i.StateImageIndex += reverse ? -1 : 1;
			}

			if (i.Tag.ToString() == Resources.Category_Games)
			{
				_advFilter.Game = i.StateImageIndex;
			}
			else if (i.Tag.ToString() == Resources.Category_Software)
			{
				_advFilter.Software = i.StateImageIndex;
			}
			else if (i.Tag.ToString() == Resources.Category_Uncategorized)
			{
				_advFilter.Uncategorized = i.StateImageIndex;
			}
			else if (i.Tag.ToString() == Resources.Category_Hidden)
			{
				_advFilter.Hidden = i.StateImageIndex;
			}
			else if (i.Tag.ToString() == Resources.Category_VR)
			{
				_advFilter.VR = i.StateImageIndex;
			}
			else
			{
				if (i.Tag is Category category)
				{
					switch (oldState)
					{
						case (int) AdvancedFilterState.None:
							break;
						case (int) AdvancedFilterState.Allow:
							_advFilter.Allow.Remove(category);

							break;
						case (int) AdvancedFilterState.Require:
							_advFilter.Require.Remove(category);

							break;
						case (int) AdvancedFilterState.Exclude:
							_advFilter.Exclude.Remove(category);

							break;
						default:
							throw new ArgumentOutOfRangeException(nameof(oldState), oldState, null);
					}

					switch (i.StateImageIndex)
					{
						case (int) AdvancedFilterState.None:
							break;
						case (int) AdvancedFilterState.Allow:
							_advFilter.Allow.Add(category);

							break;
						case (int) AdvancedFilterState.Require:
							_advFilter.Require.Add(category);

							break;
						case (int) AdvancedFilterState.Exclude:
							_advFilter.Exclude.Add(category);

							break;
						default:
							throw new ArgumentOutOfRangeException(nameof(i.StateImageIndex), i.StateImageIndex, null);
					}
				}
			}

			if (updateView)
			{
				OnViewChange();
			}
		}

		private void HandleMultiCatItemActivation(ListViewItem item, bool modKey)
		{
			if (item != null)
			{
				if ((item.StateImageIndex == 0) || ((item.StateImageIndex == 2) && modKey))
				{
					item.StateImageIndex = 1;
					Category cat = item.Tag as Category;
					if (cat != null)
					{
						QuickAddCategoryToSelectedGames(cat);
					}
				}
				else if ((item.StateImageIndex == 1) || ((item.StateImageIndex == 2) && !modKey))
				{
					item.StateImageIndex = 0;
					Category cat = item.Tag as Category;
					if (cat != null)
					{
						QuickRemoveCategoryFromSelectedGames(cat);
					}
				}
			}
		}

		private void ImportConfig()
		{
			if (!ProfileLoaded)
			{
				return;
			}

			Cursor = Cursors.WaitCursor;
			try
			{
				int count = CurrentProfile.ImportSteamData();
				AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_ImportedItems, count));
				if (count > 0)
				{
					MakeChange(true);
					FullListRefresh();
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_ErrorImportingSteamDataList, e.Message), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				Logger.Instance.Exception("Exception encountered while importing the remoteconfig file.", e);
				AddStatus(GlobalStrings.MainForm_ImportFailed);
			}

			Cursor = Cursors.Default;
		}

		private void InitializeFormMain()
		{
			Settings.Load();

			InitializeRenderers();
			InitializeMaterialSkin();

			lstCategories.BackColor = FormColor;
			lstCategories.ForeColor = TextColor;

			InitializeLstGames();
		}

		private void InitializeLstGames()
		{
			_tlstGames = new TypedObjectListView<GameInfo>(lstGames);

			//Aspect Getters
			_tlstGames.GenerateAspectGetters();
			colGameID.AspectToStringConverter = delegate { return string.Empty; };

			//colGameID.AspectToStringConverter = delegate(object obj)
			//{
			//    int id = (int)obj;
			//    return (id < 0) ? GlobalStrings.MainForm_External : id.ToString();
			//};
			//colTitle.AspectGetter = delegate (Object g) { return String.Empty; };
			colCategories.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return string.Empty;
				}

				return ((GameInfo) g).GetCatString(Resources.Category_Uncategorized);
			};

			colFavorite.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return string.Empty;
				}

				return ((GameInfo) g).IsFavorite() ? "X" : string.Empty;
			};

			colHidden.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return string.Empty;
				}

				return ((GameInfo) g).Hidden ? "X" : string.Empty;
			};

			colGenres.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return GlobalStrings.MainForm_NoGenres;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Games.ContainsKey(id) && (Database.Games[id].Genres != null))
				{
					return string.Join(", ", Database.Games[id].Genres);
				}

				return GlobalStrings.MainForm_NoGenres;
			};

			colFlags.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return GlobalStrings.MainForm_NoFlags;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Games.ContainsKey(id) && (Database.Games[id].Flags != null))
				{
					return string.Join(", ", Database.Games[id].Flags);
				}

				return GlobalStrings.MainForm_NoFlags;
			};

			colTags.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return GlobalStrings.MainForm_NoTags;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Games.ContainsKey(id) && (Database.Games[id].Tags != null))
				{
					return string.Join(", ", Database.Games[id].Tags);
				}

				return GlobalStrings.MainForm_NoTags;
			};

			colVRHeadsets.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return string.Empty;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Games.ContainsKey(id) && (Database.Games[id].VRSupport.Headsets != null))
				{
					return string.Join(", ", Database.Games[id].VRSupport.Headsets);
				}

				return string.Empty;
			};

			colVRInput.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return string.Empty;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Games.ContainsKey(id) && (Database.Games[id].VRSupport.Input != null))
				{
					return string.Join(", ", Database.Games[id].VRSupport.Input);
				}

				return string.Empty;
			};

			colVRPlayArea.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return string.Empty;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Games.ContainsKey(id) && (Database.Games[id].VRSupport.PlayArea != null))
				{
					return string.Join(", ", Database.Games[id].VRSupport.PlayArea);
				}

				return string.Empty;
			};

			colLanguageInterface.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return string.Empty;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Games.ContainsKey(id) && (Database.Games[id].LanguageSupport.Interface != null))
				{
					return string.Join(", ", Database.Games[id].LanguageSupport.Interface);
				}

				return string.Empty;
			};

			colLanguageSubtitles.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return string.Empty;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Games.ContainsKey(id) && (Database.Games[id].LanguageSupport.Subtitles != null))
				{
					return string.Join(", ", Database.Games[id].LanguageSupport.Subtitles);
				}

				return string.Empty;
			};

			colLanguageFullAudio.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return string.Empty;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Games.ContainsKey(id) && (Database.Games[id].LanguageSupport.FullAudio != null))
				{
					return string.Join(", ", Database.Games[id].LanguageSupport.FullAudio);
				}

				return string.Empty;
			};

			colYear.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return GlobalStrings.MainForm_Unknown;
				}

				int id = ((GameInfo) g).Id;
				CultureInfo culture = Utility.GetCultureInfoFromStoreLanguage(Database.Language);
				if (Database.Games.ContainsKey(id) && DateTime.TryParse(Database.Games[id].SteamReleaseDate, culture, DateTimeStyles.None, out DateTime releaseDate))
				{
					return releaseDate.Year.ToString();
				}

				return GlobalStrings.MainForm_Unknown;
			};

			colLastPlayed.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return DateTime.MinValue;
				}

				GameInfo gameInfo = g as GameInfo;
				if (gameInfo == null)
				{
					return DateTime.MinValue;
				}

				if (gameInfo.LastPlayed <= 0)
				{
					return DateTime.MinValue;
				}

				return DateTimeOffset.FromUnixTimeSeconds(gameInfo.LastPlayed).Date;
			};

			colAchievements.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return 0;
				}

				int id = ((GameInfo) g).Id;

				return Database.Games.ContainsKey(id) ? Database.Games[id].TotalAchievements : 0;
			};

			colPlatforms.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return "";
				}

				AppPlatforms platforms = Database.Games[((GameInfo) g).Id].Platforms;

				return ((platforms & AppPlatforms.Linux) != 0) && (platforms != AppPlatforms.All) ? platforms + ", SteamOS" : platforms.ToString();
			};

			colDevelopers.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return GlobalStrings.MainForm_Unknown;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Games.ContainsKey(id) && (Database.Games[id].Developers != null))
				{
					return string.Join(", ", Database.Games[id].Developers);
				}

				return GlobalStrings.MainForm_Unknown;
			};

			colPublishers.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return GlobalStrings.MainForm_Unknown;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Games.ContainsKey(id) && (Database.Games[id].Publishers != null))
				{
					return string.Join(", ", Database.Games[id].Publishers);
				}

				return GlobalStrings.MainForm_Unknown;
			};

			colNumberOfReviews.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return 0;
				}

				int id = ((GameInfo) g).Id;

				return Database.Games.ContainsKey(id) ? Database.Games[id].ReviewTotal : 0;
			};

			colReviewScore.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return 0;
				}

				int id = ((GameInfo) g).Id;

				return Database.Games.ContainsKey(id) ? Database.Games[id].ReviewPositivePercentage : 0;
			};

			colReviewLabel.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return 0;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Games.ContainsKey(id))
				{
					int reviewTotal = Database.Games[id].ReviewTotal;
					int reviewPositivePercentage = Database.Games[id].ReviewPositivePercentage;
					if (reviewTotal <= 0)
					{
						return -1;
					}

					if ((reviewPositivePercentage >= 95) && (reviewTotal >= 500))
					{
						return 9;
					}

					if ((reviewPositivePercentage >= 85) && (reviewTotal >= 50))
					{
						return 8;
					}

					if (reviewPositivePercentage >= 80)
					{
						return 7;
					}

					if (reviewPositivePercentage >= 70)
					{
						return 6;
					}

					if (reviewPositivePercentage >= 40)
					{
						return 5;
					}

					if (reviewPositivePercentage >= 20)
					{
						return 4;
					}

					if (reviewTotal >= 500)
					{
						return 3;
					}

					if (reviewTotal >= 50)
					{
						return 2;
					}

					return 1;
				}

				return 0;
			};

			colHltbMain.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return 0;
				}

				int id = ((GameInfo) g).Id;

				return Database.Games.ContainsKey(id) ? Database.Games[id].HltbMain : 0;
			};

			colHltbExtras.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return 0;
				}

				int id = ((GameInfo) g).Id;

				return Database.Games.ContainsKey(id) ? Database.Games[id].HltbExtras : 0;
			};

			colHltbCompletionist.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return 0;
				}

				int id = ((GameInfo) g).Id;

				return Database.Games.ContainsKey(id) ? Database.Games[id].HltbCompletionist : 0;
			};

			//Aspect to String Converters
			colNumberOfReviews.AspectToStringConverter = delegate(object obj)
			{
				int reviewTotal = (int) obj;

				return reviewTotal <= 0 ? "0" : reviewTotal.ToString(CultureInfo.InvariantCulture);
			};

			colReviewScore.AspectToStringConverter = delegate(object obj)
			{
				int reviewScore = (int) obj;

				return reviewScore <= 0 ? GlobalStrings.MainForm_Unknown : reviewScore.ToString(CultureInfo.InvariantCulture) + '%';
			};

			colReviewLabel.AspectToStringConverter = delegate(object obj)
			{
				int index = (int) obj;
				Dictionary<int, string> reviewLabels = new Dictionary<int, string>
				{
					{
						9, "Overwhelmingly Positive"
					},
					{
						8, "Very Positive"
					},
					{
						7, "Positive"
					},
					{
						6, "Mostly Positive"
					},
					{
						5, "Mixed"
					},
					{
						4, "Mostly Negative"
					},
					{
						3, "Negative"
					},
					{
						2, "Very Negative"
					},
					{
						1, "Overwhelmingly Negative"
					}
				};

				return reviewLabels.ContainsKey(index) ? reviewLabels[index] : GlobalStrings.MainForm_Unknown;
			};

			AspectToStringConverterDelegate hltb = delegate(object obj)
			{
				int time = (int) obj;
				if (time <= 0)
				{
					return GlobalStrings.MainForm_NoHltbTime;
				}

				if (time < 60)
				{
					return time + "m";
				}

				int hours = time / 60;
				int mins = time % 60;
				if (mins == 0)
				{
					return hours + "h";
				}

				return hours + "h " + mins + "m";
			};

			colHltbMain.AspectToStringConverter = delegate(object obj)
			{
				int time = (int) obj;
				if (time <= 0)
				{
					return GlobalStrings.MainForm_NoHltbTime;
				}

				if (time < 60)
				{
					return time + "m";
				}

				int hours = time / 60;
				int mins = time % 60;
				if (mins == 0)
				{
					return hours + "h";
				}

				return hours + "h " + mins + "m";
			};

			colHltbExtras.AspectToStringConverter = hltb;
			colHltbCompletionist.AspectToStringConverter = hltb;
			colLastPlayed.AspectToStringConverter = delegate(object obj)
			{
				DateTime lastPlayed = (DateTime) obj;
				Thread threadForCulture = new Thread(delegate() { });
				string format = threadForCulture.CurrentCulture.DateTimeFormat.ShortDatePattern;

				return lastPlayed == DateTime.MinValue ? null : lastPlayed.ToString(format);
			};

			//Filtering
			colCategories.ClusteringStrategy = new CommaClusterStrategy();
			colGenres.ClusteringStrategy = new CommaClusterStrategy();
			colFlags.ClusteringStrategy = new CommaClusterStrategy();
			colTags.ClusteringStrategy = new CommaClusterStrategy();
			colVRHeadsets.ClusteringStrategy = new CommaClusterStrategy();
			colVRInput.ClusteringStrategy = new CommaClusterStrategy();
			colVRPlayArea.ClusteringStrategy = new CommaClusterStrategy();
			colLanguageInterface.ClusteringStrategy = new CommaClusterStrategy();
			colLanguageSubtitles.ClusteringStrategy = new CommaClusterStrategy();
			colLanguageFullAudio.ClusteringStrategy = new CommaClusterStrategy();
			colPlatforms.ClusteringStrategy = new CommaClusterStrategy();
			lstGames.AdditionalFilter = new ModelFilter(delegate(object g)
			{
				if (g == null)
				{
					return false;
				}

				return ShouldDisplayGame((GameInfo) g);
			});

			//Formating
			lstGames.RowFormatter = delegate(OLVListItem lvi)
			{
				if ((lvi.RowObject != null) && (((GameInfo) lvi.RowObject).Id < 0))
				{
					lvi.Font = new Font(lvi.Font, lvi.Font.Style | FontStyle.Italic);
				}
			};

			lstGames.PrimarySortColumn = colTitle;

			if (!string.IsNullOrWhiteSpace(Settings.ListGamesState))
			{
				lstGames.RestoreState(Convert.FromBase64String(Settings.ListGamesState));
			}
		}

		private void InitializeMaterialSkin()
		{
			MaterialSkinManager.Instance.AddFormToManage(this);
			MaterialSkinManager.Instance.Theme = MaterialSkinManager.Themes.DARK;
			MaterialSkinManager.Instance.ColorScheme = new ColorScheme(MaterialSkin.Primary.BlueGrey800, MaterialSkin.Primary.BlueGrey900, MaterialSkin.Primary.BlueGrey500, MaterialSkin.Accent.LightBlue700, TextShade.WHITE);
		}

		private void InitializeObjectListView()
		{
			// Skin the Game List
			lstGames.HeaderFormatStyle = new HeaderFormatStyle();
			lstGames.HeaderFormatStyle.SetBackColor(PrimaryDark);
			lstGames.HeaderFormatStyle.SetForeColor(HeaderFontColor);
			lstGames.HeaderFormatStyle.SetFont(new Font("Arial", 10, FontStyle.Bold));
			lstGames.HeaderFormatStyle.Hot.BackColor = PrimaryLight;
			lstGames.ForeColor = TextColor;
			lstGames.BackColor = FormColor;
			lstGames.SelectedForeColor = TextColor;
			lstGames.SelectedBackColor = Accent;
			lstGames.UnfocusedSelectedForeColor = TextColor;
			lstGames.UnfocusedSelectedBackColor = Primary;
			lstGames.Font = new Font("Arial", 10);
		}

		private void InitializeRenderers()
		{
			menuStrip.Renderer = new MyRenderer();
			menu_Tools_Autocat_List.Renderer = new MyRenderer();
			contextCat.Renderer = new MyRenderer();
			contextGame.Renderer = new MyRenderer();
			contextGameFav.Renderer = new MyRenderer();
			contextGameHidden.Renderer = new MyRenderer();
			contextGameAddCat.Renderer = new MyRenderer();
			contextGameRemCat.Renderer = new MyRenderer();
			contextAutoCat.Renderer = new MyRenderer();
		}

		private void LoadProfile()
		{
			if (!CheckForUnsaved())
			{
				return;
			}

			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				dialog.DefaultExt = "profile";
				dialog.AddExtension = true;
				dialog.CheckFileExists = true;
				dialog.Filter = GlobalStrings.DlgProfile_Filter;
				dialog.InitialDirectory = Path.GetDirectoryName(CurrentProfile == null ? Assembly.GetExecutingAssembly().CodeBase : CurrentProfile.FilePath);

				if (dialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}

				LoadProfile(dialog.FileName, false);
			}
		}

		private void LoadProfile(string path, bool checkForChanges = true)
		{
			Cursor = Cursors.WaitCursor;
			if (checkForChanges && !CheckForUnsaved())
			{
				return;
			}

			try
			{
				CurrentProfile = Profile.Load(path);
				AddStatus(GlobalStrings.MainForm_ProfileLoaded);
			}
			catch (ApplicationException e)
			{
				MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Msg_ErrorLoadingProfile, e.Message), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				Logger.Instance.Exception(GlobalStrings.MainForm_Log_ExceptionLoadingProfile, e);
				OnProfileChange();
				AddStatus(GlobalStrings.MainForm_FailedLoadProfile);

				return;
			}

			if (CurrentProfile.AutoUpdate)
			{
				UpdateLibrary();
			}

			if (CurrentProfile.AutoImport)
			{
				ImportConfig();
			}

			Cursor = Cursors.Default;

			FullListRefresh();

			OnProfileChange();
		}

		private void lstCategories_DragDrop(object sender, DragEventArgs e)
		{
			if (!e.Data.GetDataPresent(typeof(int[])))
			{
				return;
			}

			lstCategories.SelectedIndices.Clear();
			if (_dragOldCat >= 0)
			{
				lstCategories.SelectedIndices.Add(_dragOldCat);
			}

			_isDragging = false;
			ClearStatus();
			ListViewItem dropItem = GetCategoryItemAtPoint(e.X, e.Y);

			SetDragDropEffect(e);

			if ((dropItem.Tag != null) && dropItem.Tag is Category)
			{
				Category dropCat = (Category) dropItem.Tag;
				if (e.Effect == DragDropEffects.Move)
				{
					if (dropCat == CurrentProfile.GameData.FavoriteCategory)
					{
						CurrentProfile.GameData.AddGameCategory((int[]) e.Data.GetData(typeof(int[])), dropCat);
					}
					else
					{
						CurrentProfile.GameData.SetGameCategories((int[]) e.Data.GetData(typeof(int[])), dropCat, true);
					}
				}
				else if (e.Effect == DragDropEffects.Link)
				{
					CurrentProfile.GameData.RemoveGameCategory((int[]) e.Data.GetData(typeof(int[])), dropCat);
				}
				else if (e.Effect == DragDropEffects.Copy)
				{
					CurrentProfile.GameData.AddGameCategory((int[]) e.Data.GetData(typeof(int[])), dropCat);
				}

				FillAllCategoryLists();
				FilterGamelist(false);
				MakeChange(true);
			}
			else if ((string) dropItem.Tag == Resources.Category_Uncategorized)
			{
				CurrentProfile.GameData.ClearGameCategories((int[]) e.Data.GetData(typeof(int[])), true);
				FillCategoryList();
				FilterGamelist(false);
				MakeChange(true);
			}
			else if ((string) dropItem.Tag == Resources.Category_Hidden)
			{
				CurrentProfile.GameData.HideGames((int[]) e.Data.GetData(typeof(int[])), true);
				FillCategoryList();
				FilterGamelist(false);
				MakeChange(true);
			}

			FlushStatus();
		}

		private void lstCategories_DragEnter(object sender, DragEventArgs e)
		{
			_isDragging = true;
			_dragOldCat = lstCategories.SelectedIndices.Count > 0 ? lstCategories.SelectedIndices[0] : -1;

			SetDragDropEffect(e);
		}

		private void lstCategories_DragLeave(object sender, EventArgs e)
		{
			_isDragging = false;
			lstCategories.SelectedIndices.Clear();
			if (_dragOldCat >= 0)
			{
				lstCategories.SelectedIndices.Add(_dragOldCat);
			}
		}

		private void lstCategories_DragOver(object sender, DragEventArgs e)
		{
			if (_isDragging)
			{
				// This shouldn't get called if this is false, but the OnSelectChange method is tied to this variable so do the check
				lstCategories.SelectedIndices.Clear();
				ListViewItem overItem = GetCategoryItemAtPoint(e.X, e.Y);
				if (overItem != null)
				{
					overItem.Selected = true;
				}
			}

			SetDragDropEffect(e);
		}

		private void lstCategories_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Delete:
					ClearStatus();
					DeleteCategory();
					FlushStatus();

					break;
				case Keys.N:
					ClearStatus();
					if (e.Modifiers == Keys.Control)
					{
						CreateCategory();
					}

					FlushStatus();

					break;
				case Keys.F2:
					ClearStatus();
					RenameCategory();
					FlushStatus();

					break;
				case Keys.Return:
				case Keys.Space:
					if (AdvancedCategoryFilter)
					{
						bool reverse = ModifierKeys == Keys.Shift;
						foreach (ListViewItem i in lstCategories.SelectedItems)
						{
							HandleAdvancedCategoryItemActivation(i, reverse, false);
						}

						OnViewChange();
					}

					break;
			}
		}

		private void lstCategories_Layout(object sender, LayoutEventArgs e)
		{
			lstCategories.Columns[0].Width = lstCategories.DisplayRectangle.Width;
		}

		private void lstCategories_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				ListViewItem overItem = lstCategories.GetItemAt(e.X, e.Y);
				if (overItem != null)
				{
					overItem.Selected = true;
				}
			}
			else if (e.Button == MouseButtons.Left)
			{
				if (AdvancedCategoryFilter)
				{
					ListViewItem i = lstCategories.GetItemAt(e.X, e.Y);
					if (lstCategories.SelectedItems.Contains(i) && !(ModifierKeys == Keys.Control))
					{
						HandleAdvancedCategoryItemActivation(i, ModifierKeys == Keys.Shift);
					}
				}
			}
		}

		private void lstCategories_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_isDragging)
			{
				return;
			}

			object selectedCategory = null;
			if (lstCategories.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = lstCategories.SelectedItems[0];
				selectedCategory = listViewItem.Tag ?? listViewItem.Text;
			}

			if (selectedCategory != _lastSelectedCategory)
			{
				OnViewChange();
				_lastSelectedCategory = selectedCategory;
			}

			UpdateEnabledStatesForCategories();
		}

		private void lstGames_ColumnReordered(object sender, ColumnReorderedEventArgs e)
		{
			_columnReorderThread = new Thread(ColumnReorderWorker);
			_columnReorderThread.Start();
		}

		private void lstGames_DoubleClick(object sender, EventArgs e)
		{
			ClearStatus();
			EditGame();
			FlushStatus();
		}

		private void lstGames_FormatCell(object sender, FormatCellEventArgs e)
		{
			if (e.ColumnIndex != 0)
			{
				return;
			}

			if (e.Model == null)
			{
				return;
			}

			// Add game banner to ID column
			GameInfo g = (GameInfo) e.Model;

			string bannerFile = DepressurizerCore.Helpers.Location.File.Banner(g.Id);
			if (!File.Exists(bannerFile))
			{
				return;
			}

			try
			{
				ImageDecoration decoration = new ImageDecoration(Image.FromFile(bannerFile));
				decoration.ShrinkToWidth = true;
				decoration.AdornmentCorner = ContentAlignment.TopLeft;
				decoration.ReferenceCorner = ContentAlignment.TopLeft;
				decoration.Transparency = 255;

				//e.SubItem.Decoration = decoration;
				e.SubItem.Decorations.Add(decoration);
			}
			catch
			{
				// Some images used to get saved in a corrupted state, and would throw an exception when loaded here.
				// Just in case there are still images like that around, drop exceptions from them.
			}

			// Add Early Access banner
			if (Database.Games.ContainsKey(g.Id) && (Database.Games[g.Id].Tags != null))
			{
				if (Database.Games[g.Id].Tags.Contains(EarlyAccess))
				{
					ImageDecoration earlyAccessDecoration = new ImageDecoration(imglistEarlyAccess.Images[0])
					{
						AdornmentCorner = ContentAlignment.TopLeft,
						ReferenceCorner = ContentAlignment.TopLeft,
						Transparency = 200
					};

					e.SubItem.Decorations.Add(earlyAccessDecoration);
				}
			}

			TextDecoration td = new TextDecoration(g.Id.ToString(CultureInfo.InvariantCulture), ContentAlignment.BottomLeft)
			{
				Font = new Font(lstGames.Font.Name, 8),
				Wrap = false,
				TextColor = TextColor,
				BackColor = ListBackground,
				CornerRounding = 4,
				Transparency = 200
			};

			e.SubItem.Decorations.Add(td);
		}

		private void lstGames_FormatRow(object sender, FormatRowEventArgs e)
		{
			if (e.Model == null)
			{
				return;
			}

			GameInfo g = (GameInfo) e.Model;
			if (g.IsFavorite())
			{
				e.Item.BackColor = ListBackground;
			}

			if (g.Hidden)
			{
				e.Item.BackColor = PrimaryLight;
			}
		}

		private void lstGames_ItemDrag(object sender, ItemDragEventArgs e)
		{
			int[] selectedGames = new int[lstGames.SelectedObjects.Count];
			for (int i = 0; i < lstGames.SelectedObjects.Count; i++)
			{
				selectedGames[i] = _tlstGames.SelectedObjects[i].Id;
			}

			lstGames.DoDragDrop(selectedGames, DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Link);
		}

		private void lstGames_ItemsChanged(object sender, ItemsChangedEventArgs e)
		{
			UpdateSelectedStatusText();
			UpdateEnabledStatesForGames();
			UpdateGameCheckStates();
		}

		private void lstGames_KeyDown(object sender, KeyEventArgs e)
		{
			ClearStatus();
			switch (e.KeyCode)
			{
				case Keys.Delete:
					RemoveGames();

					break;
				case Keys.N:
					if (e.Control)
					{
						AddGame();
					}

					break;
				case Keys.Enter:
					EditGame();

					break;
			}

			FlushStatus();
		}

		private void lstGames_SelectedIndexChanged(object sender, EventArgs e)
		{
			string storeLanguage = Settings.StoreLanguage.ToString();
			contextGameFav_Yes.Checked = false;
			contextGameFav_No.Checked = false;
			contextGameHidden_Yes.Checked = false;
			contextGameHidden_No.Checked = false;

			if (lstGames.SelectedObjects.Count > 0)
			{
				GameInfo g = _tlstGames.SelectedObjects[0];

				if ((_tlstGames.SelectedObjects.Count == 1) && g.IsFavorite())
				{
					contextGameFav_Yes.Checked = true;
				}
				else if (_tlstGames.SelectedObjects.Count == 1)
				{
					contextGameFav_No.Checked = true;
				}

				if ((_tlstGames.SelectedObjects.Count == 1) && g.Hidden)
				{
					contextGameHidden_Yes.Checked = true;
				}
				else if (_tlstGames.SelectedObjects.Count == 1)
				{
					contextGameHidden_No.Checked = true;
				}

				if (webBrowser1.Visible)
				{
					webBrowser1.ScriptErrorsSuppressed = true;
					webBrowser1.Navigate(string.Format(CultureInfo.InvariantCulture, Constants.SteamStoreAppURL + "?l=" + storeLanguage, g.Id));
				}
			}
			else if (webBrowser1.Visible)
			{
				try
				{
					if (_tlstGames.Objects.Count > 0)
					{
						GameInfo g = _tlstGames.Objects[0];
						webBrowser1.ScriptErrorsSuppressed = true;
						webBrowser1.Navigate(string.Format(CultureInfo.InvariantCulture, Constants.SteamStoreAppURL + "?l=" + storeLanguage, g.Id));
					}
					else
					{
						webBrowser1.ScriptErrorsSuppressed = true;
						webBrowser1.Navigate(Constants.SteamStoreURL + "?l=" + storeLanguage);
					}
				}
				catch (Exception exception)
				{
					SentryLogger.Log(exception);
				}
			}
		}

		private void lstGames_SelectionChanged(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			UpdateSelectedStatusText();
			UpdateEnabledStatesForGames();
			UpdateGameCheckStates();
			UpdateAutoCatSelected_StatusMessage();
			mbtnAutoCategorize.Text = string.Format(CultureInfo.CurrentCulture, "Auto-Categorize ({0} Games)", AutoCatGameCount());
			Cursor.Current = Cursors.Default;
		}

		private void lstMultiCat_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar != (char) Keys.Return) && (e.KeyChar != (char) Keys.Space))
			{
				return;
			}

			if (lstMultiCat.SelectedItems.Count == 0)
			{
				return;
			}

			ListViewItem listViewItem = lstMultiCat.SelectedItems[0];
			HandleMultiCatItemActivation(listViewItem, ModifierKeys == Keys.Shift);
		}

		private void lstMultiCat_MouseDown(object sender, MouseEventArgs e)
		{
			ListViewItem i = lstMultiCat.GetItemAt(e.X, e.Y);
			HandleMultiCatItemActivation(i, ModifierKeys == Keys.Shift);
		}

		private void lvAutoCatType_DoubleClick(object sender, EventArgs e)
		{
			ClearStatus();
			AutoCat selected = null;
			if (lvAutoCatType.SelectedItems.Count > 0)
			{
				selected = (AutoCat) lvAutoCatType.SelectedItems[0].Tag;
			}
			else if (lvAutoCatType.CheckedItems.Count > 0)
			{
				selected = (AutoCat) lvAutoCatType.CheckedItems[0].Tag;
			}
			else
			{
				if (lvAutoCatType.Items.Count > 0)
				{
					selected = (AutoCat) lvAutoCatType.Items[0].Tag;
				}
			}

			EditAutoCats(selected);
			FlushStatus();
		}

		private void lvAutoCatType_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (_doubleClick)
			{
				// prevent double click from changing checked value.  Double click opens edit dialog.
				_doubleClick = false;
				e.NewValue = e.CurrentValue;
			}
		}

		private void lvAutoCatType_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			((AutoCat) e.Item.Tag).Selected = e.Item.Checked;
		}

		private void lvAutoCatType_Layout(object sender, LayoutEventArgs e)
		{
			lvAutoCatType.Columns[0].Width = lvAutoCatType.DisplayRectangle.Width;
		}

		private void lvAutoCatType_MouseDown(object sender, MouseEventArgs e)
		{
			if (lvAutoCatType.GetItemAt(e.X, e.Y) != null)
			{
				if (e.Clicks > 1)
				{
					_doubleClick = true;
				}
			}
		}

		private void MakeChange(bool changes)
		{
			_unsavedChanges = changes;
			UpdateTitle();
		}

		private void ManualExportConfig()
		{
			if (CurrentProfile == null)
			{
				return;
			}

			using (SaveFileDialog dialog = new SaveFileDialog())
			{
				DialogResult result = dialog.ShowDialog();
				if (result != DialogResult.OK)
				{
					return;
				}

				UseWaitCursor = true;

				try
				{
					CurrentProfile.GameData.ExportSteamConfigFile(dialog.FileName, Settings.RemoveExtraEntries);
					AddStatus(GlobalStrings.MainForm_DataExported);
				}
				catch (Exception e)
				{
					MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Msg_ErrorManualExport, e.Message), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
					Logger.Instance.Exception(GlobalStrings.MainForm_Log_ExceptionExport, e);
					AddStatus(GlobalStrings.MainForm_ExportFailed);
				}

				UseWaitCursor = false;
			}
		}

		private void mbtnAutoCategorize_Click(object sender, EventArgs e)
		{
			//AutoCat ac = cmbAutoCatType.SelectedItem as AutoCat;
			if (lvAutoCatType.CheckedItems.Count == 0)
			{
				ClearStatus();
				AddStatus(GlobalStrings.AutoCat_NothingSelected);
				FlushStatus();
			}
			else
			{
				if ((_tlstGames.SelectedObjects.Count == 0) && mchkAutoCatSelected.Checked)
				{
					ClearStatus();
					AddStatus(GlobalStrings.AutoCatSelected_NothingSelected);
					FlushStatus();
				}
				else
				{
					List<AutoCat> autocats = new List<AutoCat>();
					foreach (ListViewItem item in lvAutoCatType.CheckedItems)
					{
						AutoCat ac = (AutoCat) item.Tag;
						autocats.Add(ac);
					}

					//RunAutoCats(currentProfile.AutoCats);  WILL THIS WORK?  ARE AUTOCATS SELECTED VALUES SET CORRECTLY
					RunAutoCats(autocats, true);
					RemoveEmptyCats();
					FilterGamelist(true);
				}
			}
		}

		private void mbtnCatAdd_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			ClearStatus();
			CreateCategory();
			FlushStatus();
			Cursor.Current = Cursors.Default;
		}

		private void mbtnCatDelete_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			ClearStatus();
			DeleteCategory();
			FlushStatus();
			Cursor.Current = Cursors.Default;
		}

		private void mbtnCategories_Click(object sender, EventArgs e)
		{
			if (splitContainer.Panel1Collapsed)
			{
				splitContainer.Panel1Collapsed = false;
				mbtnCategories.Text = "<";
			}
			else
			{
				splitContainer.Panel1Collapsed = true;
				mbtnCategories.Text = ">";
			}
		}

		private void mbtnCatRename_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			ClearStatus();
			RenameCategory();
			FlushStatus();
			Cursor.Current = Cursors.Default;
		}

		private void mbtnClearFilters_Click(object sender, EventArgs e)
		{
			ApplyFilter(new Filter(string.Empty));
			cboFilter.Text = string.Empty;
		}

		private void mbtnEditAutocats_Click(object sender, EventArgs e)
		{
			ClearStatus();
			AutoCat selected = null;
			if (lvAutoCatType.SelectedItems.Count > 0)
			{
				selected = (AutoCat) lvAutoCatType.SelectedItems[0].Tag;
			}
			else if (lvAutoCatType.CheckedItems.Count > 0)
			{
				selected = (AutoCat) lvAutoCatType.CheckedItems[0].Tag;
			}
			else if (lvAutoCatType.Items.Count > 0)
			{
				selected = (AutoCat) lvAutoCatType.Items[0].Tag;
			}

			EditAutoCats(selected);
			FlushStatus();
		}

		private void mbtnFilterDelete_Click(object sender, EventArgs e)
		{
			if (AdvancedCategoryFilter)
			{
				DeleteFilter((Filter) cboFilter.SelectedItem);
			}
		}

		private void mbtnFilterRename_Click(object sender, EventArgs e)
		{
			if ((cboFilter.SelectedItem != null) && AdvancedCategoryFilter)
			{
				RenameFilter((Filter) cboFilter.SelectedItem);
			}
		}

		private void mbtnSaveFilter_Click(object sender, EventArgs e)
		{
			if (AdvancedCategoryFilter)
			{
				SaveFilter();
			}
		}

		private void mbtnSearchClear_Click(object sender, EventArgs e)
		{
			mtxtSearch.Clear();
		}

		private void mchkAdvancedCategories_CheckedChanged(object sender, EventArgs e)
		{
			SetAdvancedMode(mchkAdvancedCategories.Checked);
		}

		private void mchkAutoCatSelected_CheckedChanged(object sender, EventArgs e)
		{
			UpdateAutoCatSelected_StatusMessage();
			mbtnAutoCategorize.Text = string.Format(CultureInfo.CurrentCulture, "Auto-Categorize ({0} Games)", AutoCatGameCount());
		}

		private void menu_File_Close_Click(object sender, EventArgs e)
		{
			ClearStatus();
			Unload();
			FlushStatus();
		}

		private void menu_File_Exit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void menu_File_LoadProfile_Click(object sender, EventArgs e)
		{
			ClearStatus();
			LoadProfile();
			FlushStatus();
		}

		private void menu_File_Manual_Export_Click(object sender, EventArgs e)
		{
			ClearStatus();
			ManualExportConfig();
			FlushStatus();
		}

		private void menu_File_NewProfile_Click(object sender, EventArgs e)
		{
			ClearStatus();
			CreateProfile();
			FlushStatus();
		}

		private void menu_File_SaveProfile_Click(object sender, EventArgs e)
		{
			ClearStatus();

			using (CloseDialog dialog = new CloseDialog(GlobalStrings.MainForm_SaveProfileConfirm, GlobalStrings.MainForm_SaveProfile, SystemIcons.Question.ToBitmap(), false, CurrentProfile.AutoExport))
			{
				DialogResult result = dialog.ShowDialog();

				if (result == DialogResult.Yes)
				{
					CurrentProfile.AutoExport = dialog.Export;
					SaveProfile();
				}
			}

			FlushStatus();
		}

		private void menu_File_SaveProfileAs_Click(object sender, EventArgs e)
		{
			ClearStatus();
			SaveProfileAs();
			FlushStatus();
		}

		private void menu_Profile_Edit_Click(object sender, EventArgs e)
		{
			ClearStatus();
			EditProfile();
			FlushStatus();
		}

		private void menu_Profile_EditAutoCats_Click(object sender, EventArgs e)
		{
			ClearStatus();
			AutoCat selected = null;
			if (lvAutoCatType.Items.Count > 0)
			{
				selected = (AutoCat) lvAutoCatType.Items[0].Tag;
			}

			EditAutoCats(selected);
			FlushStatus();
		}

		private void menu_Profile_Export_Click(object sender, EventArgs e)
		{
			ClearStatus();
			ExportConfig();
			FlushStatus();
		}

		private void menu_Profile_Import_Click(object sender, EventArgs e)
		{
			ClearStatus();
			ImportConfig();
			FlushStatus();
		}

		private void menu_Profile_Restore_Config_Click(object sender, EventArgs e)
		{
			string sharedconfigPath = Path.GetDirectoryName(string.Format(CultureInfo.InvariantCulture, Constants.ConfigFilePath, Settings.SteamPath, Profile.ID64toDirName(CurrentProfile.SteamID64)));

			using (DlgRestore dialog = new DlgRestore(sharedconfigPath))
			{
				dialog.ShowDialog();
				if (!dialog.Restored)
				{
					return;
				}

				ClearStatus();
				ImportConfig();
				FlushStatus();
			}
		}

		private void menu_Profile_Restore_Profile_Click(object sender, EventArgs e)
		{
			string profilePath = Path.GetDirectoryName(CurrentProfile.FilePath);

			using (DlgRestore dialog = new DlgRestore(profilePath))
			{
				dialog.ShowDialog();
				if (!dialog.Restored)
				{
					return;
				}

				ClearStatus();
				LoadProfile(CurrentProfile.FilePath, false);
				FlushStatus();
			}
		}

		private void menu_Profile_Update_Click(object sender, EventArgs e)
		{
			ClearStatus();
			UpdateLibrary();
			FlushStatus();
		}

		private void menu_Tools_AutonameAll_Click(object sender, EventArgs e)
		{
			ClearStatus();
			AutonameAll();
			FlushStatus();
		}

		private void menu_Tools_DBEdit_Click(object sender, EventArgs e)
		{
			using (DBEditDlg dialog = new DBEditDlg(CurrentProfile != null ? CurrentProfile.GameData : null))
			{
				dialog.ShowDialog();
			}
		}

		private void menu_Tools_RemoveEmpty_Click(object sender, EventArgs e)
		{
			ClearStatus();
			RemoveEmptyCats();
			FlushStatus();
		}

		private void menu_Tools_Settings_Click(object sender, EventArgs e)
		{
			ClearStatus();

			CultureInfo previousCulture = Thread.CurrentThread.CurrentUICulture;

			using (OptionsDialog dialog = new OptionsDialog())
			{
				DialogResult result = dialog.ShowDialog();

				if (result == DialogResult.OK)
				{
					if (previousCulture.Name != Thread.CurrentThread.CurrentUICulture.Name)
					{
						ComponentResourceManager resources = new ComponentResourceManager(typeof(FormMain));
						resources.ApplyResources(this, Name, Thread.CurrentThread.CurrentUICulture);

						// jpodadera. Save actual size and recover original size before reload resources of controls
						int actualSplitDistanceMain = splitContainer.SplitterDistance;
						int actualSplitDistanceSecondary = splitGame.SplitterDistance;
						int actualSplitDistanceCategories = splitCategories.SplitterDistance;
						int actualSplitDistanceBrowser = splitBrowser.SplitterDistance;

						ChangeLanguageControls(this, resources, Thread.CurrentThread.CurrentUICulture);

						// jpodadera. Recover previous size
						splitContainer.SplitterDistance = actualSplitDistanceMain;
						splitGame.SplitterDistance = actualSplitDistanceSecondary;
						splitCategories.SplitterDistance = actualSplitDistanceCategories;
						splitBrowser.SplitterDistance = actualSplitDistanceBrowser;

						FullListRefresh();
					}
				}
			}

			FlushStatus();
		}

		private void menu_Tools_SingleCat_Click(object sender, EventArgs e)
		{
			Settings.SingleCatMode = !Settings.SingleCatMode;
			UpdateUiForSingleCat();
		}

		private void menuToolsAutocat_Item_Click(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			if (item != null)
			{
				AutoCat autoCat = item.Tag as AutoCat;
				if (autoCat != null)
				{
					ClearStatus();
					Autocategorize(false, autoCat);
					FlushStatus();
				}
			}
		}

		private void mtxtSearch_TextChanged(object sender, EventArgs e)
		{
			FilterGamelist(false);
		}

		private void nameascendingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			lstCategories.ListViewItemSorter = new ListCategoriesComparer(CategorySortMode.Name, SortOrder.Ascending);

			lstCategories.Sort();
		}

		private void namedescendingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			lstCategories.ListViewItemSorter = new ListCategoriesComparer(CategorySortMode.Name, SortOrder.Descending);
			lstCategories.Sort();
		}

		private void OnProfileChange()
		{
			bool enable = ProfileLoaded;
			menu_File_SaveProfile.Enabled = enable;
			menu_File_SaveProfileAs.Enabled = enable;
			menu_File_Close.Enabled = enable;

			menu_Profile_Update.Enabled = enable;
			menu_Profile_Export.Enabled = enable;
			menu_Profile_Import.Enabled = enable;
			menu_Profile_Edit.Enabled = enable;
			menu_Profile_AutoCats.Enabled = enable;

			mbtnCatAdd.Enabled = enable;
			mbtnCatDelete.Enabled = enable;
			mbtnCatRename.Enabled = enable;

			contextGame_Add.Enabled = enable;

			RefreshFilters();
			UpdateEnabledStatesForGames();
			FillAutoCatLists();

			UpdateTitle();
		}

		private void OnViewChange()
		{
			FilterGamelist(false);
		}

		/// <summary>
		///     Removes the category from the selected games and updates the UI in place.
		///     <para />
		///     In particular,
		///     it repaints the changed games,
		///     it updates the category count in the left-hand pane,
		///     sets checks the category in the category checkbox list at the bottom,
		///     and it inserts the "remove category" context menu and sorts said menu.
		///     <para />
		///     The games will remain in the currently displayed list. Selection is preserved.
		/// </summary>
		/// <param name="category">Category to add to the selected games.</param>
		private void QuickAddCategoryToSelectedGames(Category category)
		{
			if (lstGames.SelectedObjects.Count <= 0)
			{
				return;
			}

			Cursor.Current = Cursors.WaitCursor;

			foreach (GameInfo gameInfo in _tlstGames.SelectedObjects)
			{
				if (gameInfo == null)
				{
					continue;
				}

				gameInfo.AddCategory(category);
			}

			lstGames.RefreshSelectedObjects();

			UpdateCategoryCountInCategoryList(category);

			AddRemoveCategoryContextMenu(category);
			ResortToolStripItemCollection(contextGameRemCat.Items);

			lstMultiCat.Items[category.Name].Checked = true;

			MakeChange(true);

			Cursor.Current = Cursors.Default;
		}

		/// <summary>
		///     Removes the category from the selected games and updates the UI in place.
		///     <para />
		///     In particular,
		///     it repaints the changed games,
		///     it updates the category count in the left-hand pane,
		///     sets checks the category in the category checkbox list at the bottom,
		///     and it inserts the "remove category" context menu and sorts said menu.
		///     <para />
		///     The games will remain in the currently displayed list. Selection is preserved.
		/// </summary>
		/// <param name="category">Category to remove from the selected games.</param>
		private void QuickRemoveCategoryFromSelectedGames(Category category)
		{
			if (lstGames.SelectedObjects.Count <= 0)
			{
				return;
			}

			Cursor.Current = Cursors.WaitCursor;

			foreach (GameInfo gameInfo in _tlstGames.SelectedObjects)
			{
				if (gameInfo == null)
				{
					continue;
				}

				gameInfo.RemoveCategory(category);
			}

			lstGames.RefreshSelectedObjects();

			UpdateCategoryCountInCategoryList(category);

			contextGameRemCat.Items.RemoveByKey(category.Name);
			lstMultiCat.Items[category.Name].Checked = false;

			MakeChange(true);

			Cursor.Current = Cursors.Default;
		}

		private void RebuildGamelist()
		{
			lstGames.BuildList();
		}

		private void RefreshFilters()
		{
			if (CurrentProfile != null)
			{
				cboFilter.DataSource = null;
				cboFilter.DataSource = CurrentProfile.GameData.Filters;
				cboFilter.ValueMember = null;
				cboFilter.DisplayMember = "Name";
				cboFilter.Text = "";
			}
		}

		private void RemoveEmptyCats()
		{
			int count = CurrentProfile.GameData.RemoveEmptyCategories();
			AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_RemovedEmptyCategories, count));
			FillAllCategoryLists();
		}

		private void RemoveGames()
		{
			int selectCount = lstGames.SelectedObjects.Count;
			if (selectCount > 0)
			{
				if (MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_RemoveGame, selectCount, selectCount == 1 ? "" : "s"), GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					Cursor.Current = Cursors.WaitCursor;
					int ignored = 0;
					int removed = 0;
					foreach (GameInfo g in _tlstGames.SelectedObjects)
					{
						g.ClearCategories(true);
						if (CurrentProfile.GameData.Games.Remove(g.Id))
						{
							removed++;
						}

						if (ProfileLoaded && CurrentProfile.AutoIgnore)
						{
							if (CurrentProfile.IgnoreList.Add(g.Id))
							{
								ignored++;
							}
						}
					}

					if (removed > 0)
					{
						AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_RemovedGame, removed, removed == 1 ? "" : "s"));
						MakeChange(true);
					}

					if (ignored > 0)
					{
						AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_IgnoredGame, ignored, ignored == 1 ? "" : "s"));
						MakeChange(true);
					}

					FillGameList();
					Cursor.Current = Cursors.Default;
				}
			}
		}

		private void RenameCategory()
		{
			if (lstCategories.SelectedItems.Count <= 0)
			{
				return;
			}

			if (!(lstCategories.SelectedItems[0].Tag is Category category))
			{
				return;
			}

			if (category == CurrentProfile.GameData.FavoriteCategory)
			{
				return;
			}

			using (GetStringDlg dlg = new GetStringDlg(category.Name, string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_RenameCategory, category.Name), GlobalStrings.MainForm_EnterNewName, GlobalStrings.MainForm_Rename))
			{
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}

				string newName = dlg.Value;
				if (newName == category.Name)
				{
					return;
				}

				if (ValidateCategoryName(newName))
				{
					Category newCategory = CurrentProfile.GameData.RenameCategory(category, newName);
					if (newCategory != null)
					{
						FillAllCategoryLists();
						RebuildGamelist();
						MakeChange(true);

						for (int index = 2; index < lstCategories.Items.Count; index++)
						{
							if (lstCategories.Items[index].Tag != newCategory)
							{
								continue;
							}

							lstCategories.SelectedIndices.Add(index);

							break;
						}

						AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_CategoryRenamed, category.Name));

						return;
					}
				}

				MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_NameIsInUse, newName), GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void RenameFilter(Filter f)
		{
			if (!AdvancedCategoryFilter)
			{
				return;
			}

			using (GetStringDlg dlg = new GetStringDlg(f.Name, string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_RenameFilter, f.Name), GlobalStrings.MainForm_EnterNewName, GlobalStrings.MainForm_Rename))
			{
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}

				if (f.Name == dlg.Value)
				{
					return;
				}

				if (CurrentProfile.GameData.FilterExists(dlg.Value))
				{
					MessageBox.Show(GlobalStrings.MainForm_FilterExists, GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

					return;
				}

				f.Name = dlg.Value;
				RefreshFilters();
				cboFilter.SelectedItem = f;
				cboFilter.Text = f.Name;
			}
		}

		private void ReorderFillerColumn()
		{
			if (lstGames.InvokeRequired)
			{
				RemoveItemCallback callback = ReorderFillerColumn;
				Invoke(callback);
			}
			else
			{
				// filler column should always be last column
				colFiller.DisplayIndex = lstGames.ColumnsInDisplayOrder.Count - 1;
			}
		}

		private void RunAutoCats(List<AutoCat> autoCats, bool first, bool group = false)
		{
			foreach (AutoCat autoCat in autoCats)
			{
				if (autoCat == null)
				{
					continue;
				}

				if (autoCat.AutoCatType == AutoCatType.Group)
				{
					AutoCatGroup acg = (AutoCatGroup) autoCat;
					RunAutoCats(CurrentProfile.CloneAutoCatList(acg.Autocats, CurrentProfile.GameData.GetFilter(acg.Filter)), first, true);
				}
				else
				{
					if (!autoCat.Selected && !group)
					{
						continue;
					}

					ClearStatus();
					Autocategorize(mchkAutoCatSelected.Checked, autoCat, first, false);
					first = false;
					FlushStatus();
				}
			}
		}

		private void SaveDatabase(bool force = false)
		{
			if (!Settings.AutoSaveDatabase && !force)
			{
				return;
			}

			try
			{
				Database.Save();
				AddStatus(GlobalStrings.MainForm_Status_SavedDB);
			}
			catch (Exception e)
			{
				Logger.Instance.Exception(GlobalStrings.MainForm_Log_ExceptionAutosavingDB, e);
				MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Msg_ErrorAutosavingDB, e.Message), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void SaveFilter()
		{
			if (!ProfileLoaded || !AdvancedCategoryFilter)
			{
				return;
			}

			GetStringDlg dlg = new GetStringDlg(cboFilter.Text, GlobalStrings.MainForm_SaveFilter, GlobalStrings.MainForm_EnterNewFilterName, GlobalStrings.MainForm_Save);
			if ((dlg.ShowDialog() == DialogResult.OK) && ValidateFilterName(dlg.Value))
			{
				Filter f;
				bool refresh = true;
				if (CurrentProfile.GameData.FilterExists(dlg.Value))
				{
					DialogResult res = MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_OverwriteFilterName, dlg.Value), GlobalStrings.MainForm_Overwrite, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

					if (res == DialogResult.Yes)
					{
						f = CurrentProfile.GameData.GetFilter(dlg.Value);
						refresh = false;
					}
					else
					{
						return;
					}
				}
				else
				{
					f = CurrentProfile.GameData.AddFilter(dlg.Value);
				}

				if (f != null)
				{
					f.Uncategorized = _advFilter.Uncategorized;
					f.Hidden = _advFilter.Hidden;
					f.VR = _advFilter.VR;
					f.Allow = _advFilter.Allow;
					f.Require = _advFilter.Require;
					f.Exclude = _advFilter.Exclude;
					if (refresh)
					{
						AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_FilterAdded, f.Name));
						RefreshFilters();
						cboFilter.SelectedItem = f;
					}
				}
				else
				{
					MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_CouldNotAddFilter, dlg.Value), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}

		private bool SaveProfile(string path = null)
		{
			if (!ProfileLoaded)
			{
				return false;
			}

			if (CurrentProfile.AutoExport)
			{
				ExportConfig();
			}

			Settings.ListGamesState = Convert.ToBase64String(lstGames.SaveState());

			try
			{
				if (path == null)
				{
					CurrentProfile.Save();
				}
				else
				{
					CurrentProfile.Save(path);
				}

				AddStatus(GlobalStrings.MainForm_ProfileSaved);
				MakeChange(false);

				return true;
			}
			catch (ApplicationException e)
			{
				MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Msg_ErrorSavingProfile, e.Message), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Logger.Instance.Exception(GlobalStrings.MainForm_Log_ExceptionSavingProfile, e);
				AddStatus(GlobalStrings.MainForm_FailedSaveProfile);

				return false;
			}
		}

		private void SaveProfileAs()
		{
			if (!ProfileLoaded)
			{
				return;
			}

			SaveFileDialog dlg = new SaveFileDialog();
			dlg.DefaultExt = "profile";
			dlg.AddExtension = true;
			dlg.CheckPathExists = true;
			dlg.Filter = GlobalStrings.DlgProfile_Filter;
			dlg.InitialDirectory = Path.GetDirectoryName(CurrentProfile.FilePath);
			DialogResult res = dlg.ShowDialog();
			if (res == DialogResult.OK)
			{
				SaveProfile(dlg.FileName);
			}
		}

		private void SaveSelectedAutoCats()
		{
			string autoCats = string.Empty;

			for (int i = 0; i < lvAutoCatType.CheckedItems.Count; i++)
			{
				ListViewItem listViewItem = lvAutoCatType.CheckedItems[i];
				autoCats += string.IsNullOrWhiteSpace(autoCats) ? listViewItem.Name : string.Format(CultureInfo.CurrentCulture, ",{0}", listViewItem.Name);
			}

			Settings.SelectedAutoCats = autoCats;
		}

		private void SelectAutoCats()
		{
			if (string.IsNullOrWhiteSpace(Settings.SelectedAutoCats))
			{
				return;
			}

			List<string> autoCats = Settings.SelectedAutoCats.Split(',').ToList();
			foreach (string autoCat in autoCats)
			{
				for (int i = 0; i < lvAutoCatType.Items.Count; i++)
				{
					ListViewItem listViewItem = lvAutoCatType.Items[i];
					if (listViewItem.Name == autoCat)
					{
						listViewItem.Checked = true;
					}
				}
			}
		}

		private void SelectCategory()
		{
			if (string.IsNullOrWhiteSpace(Settings.SelectedCategory))
			{
				return;
			}

			lstCategories.SelectedIndices.Clear();

			for (int i = 0; i < lstCategories.Items.Count; i++)
			{
				ListViewItem listViewItem = lstCategories.Items[i];
				if (listViewItem.Name != Settings.SelectedCategory)
				{
					continue;
				}

				lstCategories.SelectedIndices.Add(i);
				break;
			}
		}

		private void SelectFilter()
		{
			RefreshFilters();

			if (string.IsNullOrWhiteSpace(Settings.SelectedFilter))
			{
				return;
			}

			for (int i = 0; i < cboFilter.Items.Count; i++)
			{
				string name = cboFilter.GetItemText(cboFilter.Items[i]);
				if (name != Settings.SelectedFilter)
				{
					continue;
				}

				mchkAdvancedCategories.Checked = true;

				cboFilter.Text = name;
				cboFilter.SelectedIndex = i;
				ApplyFilter((Filter) cboFilter.SelectedItem);
			}
		}

		private void SetAdvancedMode(bool enabled)
		{
			Cursor.Current = Cursors.WaitCursor;
			if (enabled)
			{
				splitCategories.Panel1Collapsed = false;
				lstCategories.StateImageList = imglistFilter;
				_advFilter = new Filter(AdvancedFilter);
				cboFilter.Text = string.Empty;
				mbtnClearFilters.Visible = true;
				contextCat_SetAdvanced.Visible = true;
			}
			else
			{
				splitCategories.Panel1Collapsed = true;
				lstCategories.StateImageList = null;
				mbtnClearFilters.Visible = false;
				contextCat_SetAdvanced.Visible = false;
			}

			// allow the form to refresh before the time-consuming stuff happens
			Application.DoEvents();
			FillCategoryList();
			OnViewChange();
			Cursor.Current = Cursors.Default;
		}

		private void SetItemState(ListViewItem i, int state)
		{
			i.StateImageIndex = state;

			if (i.Tag.ToString() == Resources.Category_Games)
			{
				_advFilter.Game = state;
			}
			else if (i.Tag.ToString() == Resources.Category_Software)
			{
				_advFilter.Software = state;
			}
			else if (i.Tag.ToString() == Resources.Category_Uncategorized)
			{
				_advFilter.Uncategorized = state;
			}
			else if (i.Tag.ToString() == Resources.Category_Hidden)
			{
				_advFilter.Hidden = state;
			}
			else if (i.Tag.ToString() == Resources.Category_VR)
			{
				_advFilter.VR = state;
			}
			else
			{
				if (!(i.Tag is Category category))
				{
					return;
				}

				switch ((AdvancedFilterState) state)
				{
					case AdvancedFilterState.Allow:
						_advFilter.Allow.Add(category);
						_advFilter.Require.Remove(category);
						_advFilter.Exclude.Remove(category);

						break;
					case AdvancedFilterState.Require:
						_advFilter.Allow.Remove(category);
						_advFilter.Require.Add(category);
						_advFilter.Exclude.Remove(category);

						break;
					case AdvancedFilterState.Exclude:
						_advFilter.Allow.Remove(category);
						_advFilter.Require.Remove(category);
						_advFilter.Exclude.Add(category);

						break;
					case AdvancedFilterState.None:
						_advFilter.Allow.Remove(category);
						_advFilter.Require.Remove(category);
						_advFilter.Exclude.Remove(category);

						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(state), state, null);
				}
			}
		}

		private bool ShouldDisplayGame(GameInfo g)
		{
			if (CurrentProfile == null)
			{
				return false;
			}

			if (!string.IsNullOrWhiteSpace(mtxtSearch.Text) && (g.Name.IndexOf(mtxtSearch.Text, StringComparison.CurrentCultureIgnoreCase) == -1))
			{
				return false;
			}

			if (!CurrentProfile.GameData.Games.ContainsKey(g.Id))
			{
				return false;
			}

			if ((g.Id < 0) && !CurrentProfile.IncludeShortcuts)
			{
				return false;
			}

			if (lstCategories.SelectedItems.Count == 0)
			{
				return false;
			}

			if (AdvancedCategoryFilter)
			{
				return g.IncludeGame(_advFilter);
			}

			if (g.Hidden)
			{
				return lstCategories.SelectedItems[0].Tag.ToString() == Resources.Category_Hidden;
			}

			// <All>
			if (lstCategories.SelectedItems[0].Tag.ToString() == Resources.Category_All)
			{
				return true;
			}

			// <Games>
			if (lstCategories.SelectedItems[0].Tag.ToString() == Resources.Category_Games)
			{
				return Database.Games.ContainsKey(g.Id) && (Database.Games.First(a => a.Key == g.Id).Value.AppType == AppType.Game);
			}

			// <Software>
			if (lstCategories.SelectedItems[0].Tag.ToString() == Resources.Category_Software)
			{
				return Database.Games.ContainsKey(g.Id) && (Database.Games.First(a => a.Key == g.Id).Value.AppType == AppType.Application);
			}

			// <Uncategorized>
			if (lstCategories.SelectedItems[0].Tag.ToString() == Resources.Category_Uncategorized)
			{
				return g.Categories.Count == 0;
			}

			// <VR>
			if (lstCategories.SelectedItems[0].Tag.ToString() == Resources.Category_VR)
			{
				return Database.SupportsVR(g.Id);
			}

			if (!(lstCategories.SelectedItems[0].Tag is Category category))
			{
				return false;
			}

			// <Favorite>
			if (category.Name == Resources.Category_Favorite)
			{
				return g.IsFavorite();
			}

			return g.ContainsCategory(category);
		}

		private void Unload()
		{
			if (!CheckForUnsaved())
			{
				return;
			}

			Cursor.Current = Cursors.WaitCursor;
			AddStatus(GlobalStrings.MainForm_ClearedData);
			CurrentProfile = null;
			MakeChange(false);
			OnProfileChange();
			FullListRefresh();
			Cursor.Current = Cursors.Default;
		}

		private void UpdateAutoCatSelected_StatusMessage()
		{
			if ((_tlstGames.SelectedObjects.Count == 0) && mchkAutoCatSelected.Checked)
			{
				ClearStatus();
				AddStatus(GlobalStrings.AutoCatSelected_NothingSelected);
				FlushStatus();
			}
			else
			{
				if (mlblStatusMsg.Text.Contains(GlobalStrings.AutoCatSelected_NothingSelected))
				{
					ClearStatus();
					FlushStatus();
				}
			}
		}

		/// <summary>
		///     Updates the category's count in-place in the left-hand pane.
		/// </summary>
		/// <param name="category">Category to update in left-hand pane</param>
		private void UpdateCategoryCountInCategoryList(Category category)
		{
			if (category == null)
			{
				return;
			}

			foreach (ListViewItem listViewItem in lstCategories.Items)
			{
				if (ReferenceEquals(listViewItem.Tag, category))
				{
					listViewItem.Text = CategoryListViewItemText(category);
				}
			}
		}

		private void UpdateDatabaseFromAppInfo()
		{
			Logger.Instance.Info("MainForm: Updating database from appinfo.");

			try
			{
				int num = Database.UpdateFromAppInfo(string.Format(CultureInfo.InvariantCulture, Constants.AppInfoPath, Settings.SteamPath));
				AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Status_AppInfoAutoupdate, num));
				if (num > 0)
				{
					SaveDatabase();
				}
			}
			catch (Exception e)
			{
				Logger.Instance.Exception(GlobalStrings.MainForm_Log_ExceptionAppInfo, e);
				MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Msg_ErrorAppInfo, e.Message), GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void UpdateDatabaseFromHLTB()
		{
			Cursor = Cursors.WaitCursor;

			using (HLTBDialog dialog = new HLTBDialog())
			{
				DialogResult result = dialog.ShowDialog();

				if (dialog.Error != null)
				{
					Logger.Instance.Error(GlobalStrings.DBEditDlg_Log_ExceptionHltb, dialog.Error.Message);
					MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Msg_ErrorHltb, dialog.Error.Message), GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					AddStatus(GlobalStrings.DBEditDlg_ErrorUpdatingHltb);
				}
				else
				{
					if ((result == DialogResult.Cancel) || (result == DialogResult.Abort))
					{
						AddStatus(GlobalStrings.DBEditDlg_CanceledHltbUpdate);
					}
					else
					{
						AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Status_HltbAutoupdate, dialog.Updated));
						if (dialog.Updated > 0)
						{
							SaveDatabase();
						}
					}
				}
			}

			FullListRefresh();
			Cursor = Cursors.Default;
		}

		private void UpdateEnabledStatesForCategories()
		{
			Category category = null;

			foreach (ListViewItem listViewItem in lstCategories.SelectedItems)
			{
				category = listViewItem.Tag as Category;
				if ((category != null) && !((CurrentProfile != null) && (category == CurrentProfile.GameData.FavoriteCategory)))
				{
					break;
				}

				category = null;
			}

			mbtnCatDelete.Enabled = category != null;
			category = lstCategories.SelectedItems.Count > 0 ? lstCategories.SelectedItems[0].Tag as Category : null;
			mbtnCatRename.Enabled = (category != null) && !((CurrentProfile != null) && (category == CurrentProfile.GameData.FavoriteCategory));
		}

		private void UpdateEnabledStatesForGames()
		{
			bool gamesSelected = lstGames.SelectedObjects.Count > 0;

			Cursor = Cursors.WaitCursor;
			foreach (Control c in splitGame.Panel2.Controls)
			{
				if (!(c == cmbAutoCatType))
				{
					c.Enabled = gamesSelected;
				}
			}

			Cursor = Cursors.Default;
		}

		private void UpdateGameCheckStates()
		{
			lstMultiCat.BeginUpdate();
			bool first = true;
			foreach (ListViewItem item in lstMultiCat.Items)
			{
				item.StateImageIndex = 0;
			}

			if (lstGames.SelectedObjects.Count == 0)
			{
				splitGame.Panel2Collapsed = true;
			}
			else
			{
				splitGame.Panel2Collapsed = false;
				contextGameRemCat.Items.Clear();
				foreach (GameInfo game in _tlstGames.SelectedObjects)
				{
					if (game != null)
					{
						AddGameToMultiCatCheckStates(game, first);
						AddRemoveCategoryContextMenu(game);

						//AddGameToCheckboxStates(game, first);
						first = false;
					}
				}

				ResortToolStripItemCollection(contextGameRemCat.Items);
			}

			lstMultiCat.EndUpdate();
		}

		private void UpdateLibrary()
		{
			if (CurrentProfile == null)
			{
				return;
			}

			Cursor = Cursors.WaitCursor;

			bool success = false;

			// First, try to update via local config files, if they're enabled
			if (CurrentProfile.LocalUpdate)
			{
				try
				{
					int totalApps = CurrentProfile.GameData.UpdateGameListFromOwnedPackageInfo(CurrentProfile.SteamID64, CurrentProfile.IgnoreList, out int newApps);
					AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Status_LocalUpdate, totalApps, newApps));
					success = true;
				}
				catch (Exception e)
				{
					MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Msg_LocalUpdateError, e.Message), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					Logger.Instance.Exception(GlobalStrings.MainForm_Log_ExceptionLocalUpdate, e);
					AddStatus(GlobalStrings.MainForm_Status_LocalUpdateFailed);
					success = false;
				}
			}

			if (success)
			{
				MakeChange(true);
				FullListRefresh();
			}
			else if (CurrentProfile.WebUpdate)
			{
				try
				{
					CDlgUpdateProfile updateDlg = new CDlgUpdateProfile(CurrentProfile.GameData, CurrentProfile.SteamID64, CurrentProfile.OverwriteOnDownload, CurrentProfile.IgnoreList);
					DialogResult res = updateDlg.ShowDialog();

					if (updateDlg.Error != null)
					{
						Logger.Instance.Exception(GlobalStrings.MainForm_Log_ExceptionWebUpdateDialog, updateDlg.Error);
						AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_ErrorDownloadingProfileData, updateDlg.UseHtml ? "HTML" : "XML"));
						MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_ErrorDowloadingProfile, updateDlg.Error.Message), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						if ((res == DialogResult.Abort) || (res == DialogResult.Cancel))
						{
							AddStatus(GlobalStrings.MainForm_DownloadAborted);
						}
						else
						{
							if (updateDlg.Failover)
							{
								AddStatus(GlobalStrings.MainForm_XMLDownloadFailed);
							}

							if (updateDlg.Fetched == 0)
							{
								MessageBox.Show(GlobalStrings.MainForm_NoGameDataFound, GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								AddStatus(GlobalStrings.MainForm_NoGamesInDownload);
							}
							else
							{
								MakeChange(true);
								AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_DownloadedGames, updateDlg.Fetched, updateDlg.Added, updateDlg.UseHtml ? "HTML" : "XML"));
								FullListRefresh();
							}
						}
					}
				}
				catch (Exception e)
				{
					Logger.Instance.Exception(GlobalStrings.MainForm_Log_ExceptionWebUpdate, e);
					MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_ErrorDowloadingProfile, e.Message), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
					AddStatus(GlobalStrings.MainForm_DownloadFailed);
				}
			}

			Cursor = Cursors.Default;
		}

		private void UpdateSelectedStatusText()
		{
			mlblStatusSelection.Font = new Font("Arial", 9);
			mlblStatusSelection.Text = string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_SelectedDisplayed, lstGames.SelectedObjects.Count, lstGames.GetItemCount());
		}

		private void UpdateTitle()
		{
			StringBuilder sb = new StringBuilder("Depressurizer");
			if (ProfileLoaded)
			{
				sb.Append(" - ");
				sb.Append(Path.GetFileName(CurrentProfile.FilePath));
			}

			if (Settings.SingleCatMode)
			{
				sb.Append(" [");
				sb.Append(GlobalStrings.MainForm_SingleCategoryMode);
				sb.Append("]");
			}

			if (_unsavedChanges)
			{
				sb.Append(" *");
			}

			Text = sb.ToString();

			//update Avatar picture for new profile
			if (CurrentProfile != null)
			{
				picAvatar.Image = CurrentProfile.GetAvatar();
			}
		}

		private void UpdateUiForSingleCat()
		{
			bool sCat = Settings.SingleCatMode;
			menu_Tools_SingleCat.Checked = sCat;
			UpdateTitle();
		}

		#endregion
	}
}
