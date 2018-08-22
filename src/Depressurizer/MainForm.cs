#region License

//     This file (MainForm.cs) is part of Depressurizer.
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
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Depressurizer.Core;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;
using Depressurizer.Dialogs;
using Depressurizer.Helpers;
using Depressurizer.Models;
using Depressurizer.Properties;
using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Rallion;

namespace Depressurizer
{
	public enum AdvancedFilterState
	{
		None = -1,

		Allow = 0,

		Require = 1,

		Exclude = 2
	}

	public partial class FormMain : MaterialForm
	{
		#region Constants

		private const string ADVANCED_FILTER = "ADVANCED_FILTER";

		private const string EARLY_ACCESS = "Early Access";

		private const int MAX_FILTER_STATE = 2;

		#endregion

		#region Static Fields

		public static Profile CurrentProfile;

		#endregion

		#region Fields

		private readonly Color accent = Color.FromArgb(255, 0, 145, 234);

		private readonly Color formColor = Color.FromArgb(255, 42, 42, 44);

		private readonly Color headerFontColor = Color.FromArgb(255, 169, 167, 167);

		private readonly Color listBackground = Color.FromArgb(255, 22, 22, 22);

		private readonly MaterialSkinManager materialSkinManager;

		private readonly Color primary = Color.FromArgb(255, 55, 71, 79);

		private readonly Color primaryDark = Color.FromArgb(255, 38, 50, 56);

		private readonly Color primaryLight = Color.FromArgb(255, 96, 125, 139);

		private readonly StringBuilder statusBuilder = new StringBuilder();

		private readonly Color textColor = Color.FromArgb(255, 255, 255, 255);

		private Filter _advancedFilter = new Filter(ADVANCED_FILTER);

		private Color borderColor = Color.FromArgb(255, 25, 28, 38);

		// used to prevent moving the filler column in the game list
		private Thread columnReorderThread;

		// Used to prevent double clicking in Autocat listview from changing checkstate
		private bool doubleClick;

		private int dragOldCat;

		private Color headerCellColor = Color.FromArgb(255, 58, 58, 58);

		private Color highlightCellColor = Color.FromArgb(255, 25, 55, 84);

		// Allow visual feedback when dragging over the cat list
		private bool isDragging;

		private object lastSelectedCat; // Stores last selected category to minimize game list refreshes

		private Color menuColorDark = Color.FromArgb(255, 38, 50, 56);

		private Color menuColorLight = Color.FromArgb(255, 55, 71, 79);

		private Color menuHighlightText = Color.FromArgb(255, 255, 255, 234);

		private Color menuPrimaryText = Color.FromArgb(255, 168, 173, 175);

		private int originalHeight;

		private int originalSplitDistanceBrowser;

		private int originalSplitDistanceMain;

		private int originalSplitDistanceSecondary;

		// Used to reload resources of main form while switching language
		private int originalWidth;

		private Color primaryCellColor = Color.FromArgb(255, 29, 29, 29);

		private TypedObjectListView<GameInfo> tlstGames;

		private bool unsavedChanges;

		#endregion

		#region Constructors and Destructors

		public FormMain()
		{
			InitializeComponent();

			menuStrip.Renderer = new MyRenderer();
			menu_Tools_Autocat_List.Renderer = new MyRenderer();
			contextCat.Renderer = new MyRenderer();
			contextGame.Renderer = new MyRenderer();
			contextGameFav.Renderer = new MyRenderer();
			contextGameHidden.Renderer = new MyRenderer();
			contextGameAddCat.Renderer = new MyRenderer();
			contextGameRemCat.Renderer = new MyRenderer();
			contextAutoCat.Renderer = new MyRenderer();

			// Initialize MaterialSkinManager
			materialSkinManager = MaterialSkinManager.Instance;
			materialSkinManager.AddFormToManage(this);
			materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
			materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue700, TextShade.WHITE);

			lstCategories.BackColor = formColor;
			lstCategories.ForeColor = textColor;

			InitializeLstGames();
		}

		#endregion

		#region Delegates

		private delegate void RemoveItemCallback();

		#endregion

		#region Public Properties

		/// <summary>
		///     Just checks to see if there is currently a profile loaded
		/// </summary>
		public bool ProfileLoaded => CurrentProfile != null;

		#endregion

		#region Properties

		private static Database Database => Database.Instance;

		private static Logger Logger => Logger.Instance;

		private static Settings Settings => Settings.Instance;

		private bool AdvancedCategoryFilter => mchkAdvancedCategories.Checked;

		#endregion

		#region Public Methods and Operators

		/// <summary>
		///     Adds a string to the status builder
		/// </summary>
		/// <param name="s"></param>
		public void AddStatus(string s)
		{
			statusBuilder.Append(s);
			statusBuilder.Append(' ');
		}

		/// <summary>
		///     Empties the status builder
		/// </summary>
		public void ClearStatus()
		{
			statusBuilder.Clear();
		}

		/// <summary>
		///     Sets the status text to the builder text, and clear the builder text.
		/// </summary>
		public void FlushStatus()
		{
			mlblStatusMsg.Font = new Font("Arial", 9);
			mlblStatusMsg.Text = statusBuilder.ToString();
			statusBuilder.Clear();
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

		private static void CheckForDepressurizerUpdates()
		{
			Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

			try
			{
				Version githubVersion;
				string url;

				using (WebClient client = new WebClient())
				{
					client.Headers.Set("User-Agent", "Depressurizer");

					string json = client.DownloadString(Constants.DepressurizerLatestRelease);
					JObject parsedJson = JObject.Parse(json);
					githubVersion = new Version(((string) parsedJson.SelectToken("tag_name")).Replace("v", ""));
					url = (string) parsedJson.SelectToken("html_url");
				}

				if (githubVersion <= currentVersion)
				{
					return;
				}

				if (MessageBox.Show(GlobalStrings.MainForm_Msg_UpdateFound, GlobalStrings.MainForm_Msg_UpdateFoundTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					Process.Start(url);
				}
			}
			catch (Exception e)
			{
				Logger.Exception("MainForm: Exception while checking for new updates for Depressurizer.", e);
				MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_ErrorDepressurizerUpdate, e.Message), GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private static void LoadDatabase()
		{
			Cursor.Current = Cursors.WaitCursor;

			try
			{
				if (File.Exists(Core.Helpers.Location.File.Database))
				{
					Database.Load(Core.Helpers.Location.File.Database);
				}
				else
				{
					MessageBox.Show(GlobalStrings.MainForm_ErrorLoadingGameDB + GlobalStrings.MainForm_GameDBFileNotExist);
					Logger.Warn("MainForm: Database file doesn't exist.");
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(GlobalStrings.MainForm_ErrorLoadingGameDB + e.Message);

				Logger.Exception("MainForm: Exception during initial database load.", e);
				Database.Reset();
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void AddCategoryToSelectedGames(Category category, bool forceClearOthers)
		{
			if (lstGames.SelectedObjects.Count <= 0)
			{
				return;
			}

			Cursor.Current = Cursors.WaitCursor;

			foreach (GameInfo gameInfo in tlstGames.SelectedObjects)
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
				DialogResult result = dialog.ShowDialog();
				if (result != DialogResult.OK)
				{
					return;
				}

				Cursor.Current = Cursors.WaitCursor;

				if (ProfileLoaded)
				{
					if (CurrentProfile.IgnoreList.Remove(dialog.Game.Id))
					{
						AddStatus(string.Format(GlobalStrings.MainForm_UnignoredGame, dialog.Game.Id));
					}
				}

				FullListRefresh();
				MakeChange(true);
				AddStatus(GlobalStrings.MainForm_AddedGame);

				Cursor.Current = Cursors.Default;
			}
		}

		private void AddGameToMultiCatCheckStates(GameInfo game, bool first)
		{
			foreach (ListViewItem catItem in lstMultiCat.Items)
			{
				if (catItem.StateImageIndex == 2)
				{
					continue;
				}

				if (!(catItem.Tag is Category cat))
				{
					continue;
				}

				if (first)
				{
					catItem.StateImageIndex = game.ContainsCategory(cat) ? 1 : 0;
				}
				else
				{
					if (game.ContainsCategory(cat))
					{
						if (catItem.StateImageIndex == 0)
						{
							catItem.StateImageIndex = 2;
						}
					}
					else
					{
						if (catItem.StateImageIndex == 1)
						{
							catItem.StateImageIndex = 2;
						}
					}
				}
			}
		}

		private void AddRemoveCategoryContextMenu(GameInfo game)
		{
			foreach (Category c in game.Categories)
			{
				bool found = false;
				foreach (ToolStripItem i in contextGameRemCat.Items)
				{
					if (i.Text == c.Name)
					{
						found = true;
					}
				}

				if (found)
				{
					continue;
				}

				ToolStripItem item = contextGameRemCat.Items.Add(c.Name);
				item.Tag = c;
				item.Click += contextGameRemCat_Category_Click;
			}
		}

		private void ApplyFilter(Filter filter)
		{
			if ((filter == null) || !AdvancedCategoryFilter)
			{
				return;
			}

			// reset Advanced settings
			_advancedFilter = new Filter(ADVANCED_FILTER);

			// load new Advanced settings
			foreach (ListViewItem item in lstCategories.Items)
			{
				string filterName = item.Tag.ToString();
				if (filterName.Equals(Resources.Category_Games))
				{
					item.StateImageIndex = filter.Game;
					_advancedFilter.Game = filter.Game;
				}
				else if (filterName.Equals(Resources.Category_Software))
				{
					item.StateImageIndex = filter.Software;
					_advancedFilter.Software = filter.Software;
				}
				else if (filterName.Equals(Resources.Category_Uncategorized))
				{
					item.StateImageIndex = filter.Uncategorized;
					_advancedFilter.Uncategorized = filter.Uncategorized;
				}
				else if (filterName.Equals(Resources.Category_Hidden))
				{
					item.StateImageIndex = filter.Hidden;
					_advancedFilter.Hidden = filter.Hidden;
				}
				else if (filterName.Equals(Resources.Category_VR))
				{
					item.StateImageIndex = filter.VR;
					_advancedFilter.VR = filter.VR;
				}
				else
				{
					if (!(item.Tag is Category category))
					{
						return;
					}

					if (filter.Allow.Contains(category))
					{
						item.StateImageIndex = (int) AdvancedFilterState.Allow;
						_advancedFilter.Allow.Add(category);
					}
					else if (filter.Require.Contains(category))
					{
						item.StateImageIndex = (int) AdvancedFilterState.Require;
						_advancedFilter.Require.Add(category);
					}
					else if (filter.Exclude.Contains(category))
					{
						item.StateImageIndex = (int) AdvancedFilterState.Exclude;
						_advancedFilter.Exclude.Add(category);
					}
					else
					{
						item.StateImageIndex = (int) AdvancedFilterState.None;
					}
				}
			}

			OnViewChange();
		}

		/// <summary>
		///     Assigns the given favorite state to all selected items in the game list.
		/// </summary>
		/// <param name="fav">True to turn fav on, false to turn it off.</param>
		private void AssignFavoriteToSelectedGames(bool fav)
		{
			if (lstGames.SelectedObjects.Count > 0)
			{
				Cursor.Current = Cursors.WaitCursor;
				foreach (GameInfo g in tlstGames.SelectedObjects)
				{
					g.SetFavorite(fav);
				}

				FillCategoryList();
				RebuildGamelist();
				MakeChange(true);
				Cursor.Current = Cursors.Default;
			}
		}

		/// <summary>
		///     Add or remove the hidden attribute to the selected games
		/// </summary>
		/// <param name="hidden">Whether the games should be hidden</param>
		private void AssignHiddenToSelectedGames(bool hidden)
		{
			if (lstGames.SelectedObjects.Count > 0)
			{
				Cursor.Current = Cursors.WaitCursor;
				foreach (GameInfo g in tlstGames.SelectedObjects)
				{
					g.SetHidden(hidden);
				}

				FillCategoryList();
				FilterGamelist(false);
				MakeChange(true);
				Cursor.Current = Cursors.Default;
			}
		}

		/// <summary>
		///     Autocategorizes a set of games.
		/// </summary>
		/// <param name="selectedOnly">If true, runs on the selected games, otherwise, runs on all games.</param>
		/// <param name="autoCat">The autocat object to use.</param>
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
				foreach (GameInfo g in tlstGames.SelectedObjects)
				{
					if (g.Id > 0)
					{
						gamesToUpdate.Add(g);
					}
				}
			}
			else if ((tlstGames.Objects.Count > 0) && (autoCat.Filter == null))
			{
				foreach (GameInfo g in tlstGames.Objects)
				{
					if (g.Id > 0)
					{
						gamesToUpdate.Add(g);
					}
				}
			}
			else
			{
				foreach (GameInfo g in CurrentProfile.GameData.Games.Values)
				{
					if ((g != null) && (g.Id > 0))
					{
						gamesToUpdate.Add(g);
					}
				}
			}

			int updated = 0;

			// List of games not found in database or that have old data, so we can try to scrape data for them
			List<int> notInDbOrOldData = new List<int>();
			int oldDbDataCount = 0;
			int notInDbCount = 0;
			foreach (GameInfo game in gamesToUpdate)
			{
				if ((game.Id > 0) && (!Database.Contains(game.Id, out DatabaseEntry entry1) || (entry1.LastStoreScrape == 0)))
				{
					notInDbOrOldData.Add(game.Id);
					notInDbCount++;
				}
				else if ((game.Id > 0) && Database.Contains(game.Id, out DatabaseEntry entry2) && (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > (entry2.LastStoreScrape + (Settings.Instance.ScrapePromptDays * 86400)))) // 86400 seconds in a day
				{
					notInDbOrOldData.Add(game.Id);
					oldDbDataCount++;
				}
			}

			if (((notInDbCount > 0) || (oldDbDataCount > 0)) && scrape)
			{
				Cursor.Current = Cursors.Default;
				string message = "";
				message += notInDbCount > 0 ? string.Format(GlobalStrings.MainForm_GamesNotFoundInGameDB, notInDbCount) : "";
				if ((notInDbCount > 0) && (oldDbDataCount > 0))
				{
					message += " " + GlobalStrings.Text_And + " ";
				}

				message += oldDbDataCount > 0 ? string.Format(GlobalStrings.MainForm_GamesHaveOldDataInGameDB, oldDbDataCount, Settings.Instance.ScrapePromptDays) : "";
				message += ". " + GlobalStrings.MainForm_ScrapeNow;
				if (MessageBox.Show(message, GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					using (ScrapeDialog dialog = new ScrapeDialog(notInDbOrOldData))
					{
						DialogResult result = dialog.ShowDialog();

						if (result == DialogResult.Cancel)
						{
							AddStatus(string.Format(GlobalStrings.MainForm_CanceledDatabaseUpdate));
						}
						else
						{
							AddStatus(string.Format(GlobalStrings.MainForm_UpdatedDatabaseEntries, dialog.CompletedJobs));
							if ((dialog.CompletedJobs > 0) && Settings.Instance.AutoSaveDatabase)
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
			AddStatus(string.Format(GlobalStrings.MainForm_UpdatedCategories, updated));
			if (gamesToUpdate.Count > updated)
			{
				AddStatus(string.Format(GlobalStrings.MainForm_FailedToUpdate, gamesToUpdate.Count - updated));
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
				foreach (GameInfo g in tlstGames.SelectedObjects)
				{
					if (g.Id > 0)
					{
						count += 1;
					}
				}
			}
			else if (tlstGames.Objects.Count > 0)
			{
				foreach (GameInfo g in tlstGames.Objects)
				{
					if (g.Id > 0)
					{
						count += 1;
					}
				}
			}
			else
			{
				if (CurrentProfile != null)
				{
					foreach (GameInfo g in CurrentProfile.GameData.Games.Values)
					{
						if ((g != null) && (g.Id > 0))
						{
							count += 1;
						}
					}
				}
			}

			return count;
		}

		/// <summary>
		///     Renames all games with names from the database.
		/// </summary>
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

			AddStatus(string.Format(GlobalStrings.MainForm_AutonamedGames, named));
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

		/// <summary>
		///     jpodadera. Recursive function to reload resources of new language for a control and its childs
		/// </summary>
		/// <param name="c"></param>
		/// Control to reload resources
		/// <param name="resources"></param>
		/// Resource manager
		/// <param name="newCulture"></param>
		/// Culture of language to load
		private void changeLanguageControls(Control c, ComponentResourceManager resources, CultureInfo newCulture)
		{
			if (c != null)
			{
				Rectangle currentBounds = c.Bounds;
				if (c.GetType() == typeof(MenuStrip))
				{
					foreach (ToolStripDropDownItem mItem in (c as MenuStrip).Items)
					{
						changeLanguageToolStripItems(mItem, resources, newCulture);
					}
				}
				else if (c is ListView)
				{
					// jpodadera. Because a framework bug, names of ColumnHeader objects are empty. 
					// Resolved by saving names to Tag property.
					foreach (ColumnHeader cHeader in (c as ListView).Columns)
					{
						if (cHeader.Tag != null)
						{
							resources.ApplyResources(cHeader, cHeader.Tag.ToString(), newCulture);
						}
					}
				}
				else
				{
					foreach (Control childControl in c.Controls)
					{
						changeLanguageControls(childControl, resources, newCulture);
					}
				}

				resources.ApplyResources(c, c.Name, newCulture);
				c.Bounds = currentBounds;
			}
		}

		/// <summary>
		///     jpodadera. Recursive function to reload resources of new language for a menu item and its childs
		/// </summary>
		/// <param name="item"></param>
		/// Item menu to reload resources
		/// <param name="resources"></param>
		/// Resource manager
		/// <param name="newCulture"></param>
		/// Culture of language to load
		private void changeLanguageToolStripItems(ToolStripItem item, ComponentResourceManager resources, CultureInfo newCulture)
		{
			if (item != null)
			{
				if (item is ToolStripDropDownItem)
				{
					foreach (ToolStripItem childItem in (item as ToolStripDropDownItem).DropDownItems)
					{
						changeLanguageToolStripItems(childItem, resources, newCulture);
					}
				}

				resources.ApplyResources(item, item.Name, newCulture);
			}
		}

		/// <summary>
		///     If there are any unsaved changes, asks the user if they want to save. Also gives the user the option to cancel the
		///     calling action.
		/// </summary>
		/// <returns>True if the action should proceed, false otherwise.</returns>
		private bool CheckForUnsaved()
		{
			if (!ProfileLoaded || !unsavedChanges)
			{
				return true;
			}

			DlgClose close = new DlgClose(GlobalStrings.MainForm_UnsavedChangesWillBeLost, GlobalStrings.MainForm_UnsavedChanges, SystemIcons.Warning.ToBitmap(), true, CurrentProfile.AutoExport);

			DialogResult res = close.ShowDialog();

			//DialogResult res = MessageBox.Show( GlobalStrings.MainForm_UnsavedChangesWillBeLost, GlobalStrings.MainForm_UnsavedChanges, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning );
			if (res == DialogResult.No)
			{
				return true;
			}

			if (res == DialogResult.Cancel)
			{
				return false;
			}

			CurrentProfile.AutoExport = close.Export;

			return SaveProfile();
		}

		private void cmdAddCatAndAssign_Click(object sender, EventArgs e)
		{
			if (ValidateCategoryName(txtAddCatAndAssign.Text))
			{
				Category cat = CurrentProfile.GameData.GetCategory(txtAddCatAndAssign.Text);
				AddCategoryToSelectedGames(cat, false);
				txtAddCatAndAssign.Clear();
			}
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
				LaunchGame(tlstGames.SelectedObjects[0]);
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
			reorderFillerColumn();
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
			if (lstCategories.SelectedItems.Count > 0)
			{
				foreach (ListViewItem i in lstCategories.SelectedItems)
				{
					SetItemState(i, (int) AdvancedFilterState.Allow);
				}

				OnViewChange();
			}
		}

		private void contextCat_SetAdvanced_Exclude_Click(object sender, EventArgs e)
		{
			if (lstCategories.SelectedItems.Count > 0)
			{
				foreach (ListViewItem i in lstCategories.SelectedItems)
				{
					SetItemState(i, (int) AdvancedFilterState.Exclude);
				}

				OnViewChange();
			}
		}

		private void contextCat_SetAdvanced_None_Click(object sender, EventArgs e)
		{
			if (lstCategories.SelectedItems.Count > 0)
			{
				foreach (ListViewItem i in lstCategories.SelectedItems)
				{
					SetItemState(i, (int) AdvancedFilterState.None);
				}

				OnViewChange();
			}
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
				Utility.LaunchStorePage(tlstGames.SelectedObjects[0].Id);
			}
		}

		private void contextGameAddCat_Category_Click(object sender, EventArgs e)
		{
			ToolStripItem menuItem = sender as ToolStripItem;
			if (menuItem != null)
			{
				ClearStatus();
				Category c = menuItem.Tag as Category;
				AddCategoryToSelectedGames(c, false);
				FlushStatus();
			}
		}

		private void contextGameAddCat_Create_Click(object sender, EventArgs e)
		{
			Category c = CreateCategory();
			if (c != null)
			{
				ClearStatus();
				AddCategoryToSelectedGames(c, false);
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
				RemoveCategoryFromSelectedGames(c);
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

		/// <summary>
		///     Creates a new category, first prompting the user for the name to use. If the name is not valid or in use, displays
		///     a notification.
		/// </summary>
		/// <returns>The category that was added, or null if the operation was canceled or failed.</returns>
		private Category CreateCategory()
		{
			if (!ProfileLoaded)
			{
				return null;
			}

			GetStringDlg dlg = new GetStringDlg(string.Empty, GlobalStrings.MainForm_CreateCategory, GlobalStrings.MainForm_EnterNewCategoryName, GlobalStrings.MainForm_Create);
			if ((dlg.ShowDialog() == DialogResult.OK) && ValidateCategoryName(dlg.Value))
			{
				Category newCat = CurrentProfile.GameData.AddCategory(dlg.Value);
				if (newCat != null)
				{
					FillAllCategoryLists();
					MakeChange(true);
					AddStatus(string.Format(GlobalStrings.MainForm_CategoryAdded, newCat.Name));

					return newCat;
				}

				MessageBox.Show(string.Format(GlobalStrings.MainForm_CouldNotAddCategory, dlg.Value), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			return null;
		}

		private ListViewItem CreateCategoryListViewItem(Category c)
		{
			ListViewItem i = new ListViewItem(c.Name + " (" + c.Count + ")")
			{
				Tag = c,
				Name = c.Name
			};

			return i;
		}

		/// <summary>
		///     Prompts user to create a new profile.
		/// </summary>
		private void CreateProfile()
		{
			DlgProfile dlg = new DlgProfile();
			DialogResult res = dlg.ShowDialog();
			if (res == DialogResult.OK)
			{
				Cursor = Cursors.WaitCursor;
				CurrentProfile = dlg.Profile;
				AddStatus(GlobalStrings.MainForm_ProfileCreated);
				if (dlg.DownloadNow)
				{
					UpdateLibrary();
				}

				if (dlg.ImportNow)
				{
					ImportConfig();
				}

				if (dlg.SetStartup)
				{
					Settings.Instance.StartupAction = StartupAction.Load;
					Settings.Instance.ProfileToLoad = CurrentProfile.FilePath;
					Settings.Instance.Save();
				}

				FullListRefresh();

				Cursor = Cursors.Default;
			}

			OnProfileChange();
		}

		/// <summary>
		///     Deletes the selected categories and updates the UI. Prompts user for confirmation.
		/// </summary>
		private void DeleteCategory()
		{
			List<Category> toDelete = new List<Category>();
			foreach (ListViewItem item in lstCategories.SelectedItems)
			{
				Category c = item.Tag as Category;
				if ((c != null) && (c != CurrentProfile.GameData.FavoriteCategory))
				{
					toDelete.Add(c);
				}
			}

			if (toDelete.Count > 0)
			{
				DialogResult res;
				res = MessageBox.Show(toDelete.Count == 1 ? string.Format(GlobalStrings.MainForm_DeleteCategory, toDelete[0].Name) : string.Format(GlobalStrings.MainForm_DeleteCategoryMulti, toDelete.Count), GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (res == DialogResult.Yes)
				{
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
						AddStatus(string.Format(GlobalStrings.MainForm_CategoryDeleted, deleted));
					}
					else
					{
						MessageBox.Show(string.Format(GlobalStrings.MainForm_CouldNotDeleteCategory), GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
			}
		}

		private void DeleteFilter(Filter f)
		{
			if (!ProfileLoaded || !AdvancedCategoryFilter)
			{
				return;
			}

			DialogResult res;
			res = MessageBox.Show(string.Format(GlobalStrings.MainForm_DeleteFilter, f.Name), GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if (res == DialogResult.Yes)
			{
				try
				{
					CurrentProfile.GameData.Filters.Remove(f);
					AddStatus(string.Format(GlobalStrings.MainForm_FilterDeleted, f.Name));
					RefreshFilters();
				}
				catch
				{
					MessageBox.Show(string.Format(GlobalStrings.MainForm_CouldNotDeleteFilter), GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}

		/// <summary>
		///     Creates an Edit AutoCats dialog for the user
		/// </summary>
		private void EditAutoCats(AutoCat selected)
		{
			if (!ProfileLoaded)
			{
				return;
			}

			DlgAutoCat dlg = new DlgAutoCat(CurrentProfile.AutoCats, CurrentProfile.GameData, selected, CurrentProfile.FilePath);

			DialogResult res = dlg.ShowDialog();

			if (res == DialogResult.OK)
			{
				CurrentProfile.AutoCats = dlg.AutoCatList;
				MakeChange(true);
				FillAutoCatLists();
			}
		}

		/// <summary>
		///     Edits the first selected game. Displays game dialog.
		/// </summary>
		private void EditGame()
		{
			if (lstGames.SelectedObjects.Count > 0)
			{
				GameInfo g = tlstGames.SelectedObjects[0];
				DlgGame dlg = new DlgGame(CurrentProfile.GameData, g);
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					Cursor.Current = Cursors.WaitCursor;
					FilterGamelist(false);
					MakeChange(true);
					AddStatus(GlobalStrings.MainForm_EditedGame);
					Cursor.Current = Cursors.Default;
				}
			}
		}

		/// <summary>
		///     Prompts the user to modify the currently loaded profile. If there isn't one, asks if the user would like to create
		///     one.
		/// </summary>
		private void EditProfile()
		{
			if (ProfileLoaded)
			{
				DlgProfile dlg = new DlgProfile(CurrentProfile);
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					AddStatus(GlobalStrings.MainForm_ProfileEdited);
					MakeChange(true);
					Cursor = Cursors.WaitCursor;
					bool refresh = false;
					if (dlg.DownloadNow)
					{
						UpdateLibrary();
						refresh = true;
					}

					if (dlg.ImportNow)
					{
						ImportConfig();
						refresh = true;
					}

					if (dlg.SetStartup)
					{
						Settings.Instance.StartupAction = StartupAction.Load;
						Settings.Instance.ProfileToLoad = CurrentProfile.FilePath;
						Settings.Instance.Save();
					}

					Cursor = Cursors.Default;
					if (refresh)
					{
						FullListRefresh();
					}
				}
			}
			else
			{
				if (MessageBox.Show(GlobalStrings.MainForm_NoProfileLoaded, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				{
					CreateProfile();
				}
			}
		}

		/// <summary>
		///     Attempts to export steam categories
		/// </summary>
		private void ExportConfig()
		{
			if (CurrentProfile != null)
			{
				try
				{
					CurrentProfile.ExportSteamData();
					AddStatus(GlobalStrings.MainForm_ExportedCategories);
				}
				catch (Exception e)
				{
					MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_ErrorExportingToSteam, e.Message), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
					Logger.Exception(GlobalStrings.MainForm_Log_ExceptionExport, e);
					AddStatus(GlobalStrings.MainForm_ExportFailed);
				}
			}
		}

		/// <summary>
		///     Completely repopulates the category list and combobox. Maintains selection on both.
		/// </summary>
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

					ListViewItem listItem = new ListViewItem(c.Name)
					{
						Tag = c,
						StateImageIndex = 0
					};

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
			// Prepare main screen AutoCat dropdown
			object selected = cmbAutoCatType.SelectedItem;
			//cmbAutoCatType.Items.Clear();

			lvAutoCatType.Items.Clear();

			// Prepare main menu list
			menu_Tools_Autocat_List.Items.Clear();

			//if( currentProfile != null ) {
			//    foreach( AutoCat ac in currentProfile.AutoCats ) {
			//        if( ac != null ) {
			//            // Fill main screen dropdown
			//            cmbAutoCatType.Items.Add( ac );

			//            //// Fill main menu list
			//            //ToolStripItem item = menu_Tools_Autocat_List.Items.Add( ac.Name );
			//            //item.Tag = ac;
			//            //item.Click += menuToolsAutocat_Item_Click;
			//        }
			//    }
			//}

			if (CurrentProfile != null)
			{
				foreach (AutoCat ac in CurrentProfile.AutoCats)
				{
					if (ac != null)
					{
						// Fill main screen dropdown
						ListViewItem listItem = new ListViewItem(ac.DisplayName)
						{
							Tag = ac,
							Name = ac.Name,
							Checked = ac.Selected
						};

						lvAutoCatType.Items.Add(listItem);
						//SelectAutoCats();

						// Fill main menu list
						ToolStripItem item = menu_Tools_Autocat_List.Items.Add(ac.DisplayName);
						item.Tag = ac;
						item.Name = ac.Name;
						item.Click += menuToolsAutocat_Item_Click;
					}
				}
			}

			//// Finish main screen dropdown
			//if ( selected != null && cmbAutoCatType.Items.Contains( selected ) ) {
			//    cmbAutoCatType.SelectedItem = selected;
			//} else if( cmbAutoCatType.Items.Count > 0 ) {
			//    cmbAutoCatType.SelectedIndex = 0;
			//}

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
				if (!Database.Contains(g.Id, out DatabaseEntry entry))
				{
					continue;
				}

				if (g.Hidden)
				{
					hidden++;
				}

				if (!g.HasCategories() && !g.Hidden)
				{
					uncategorized++;
				}

				if (Database.SupportsVR(g.Id) && !g.Hidden)
				{
					vr++;
				}

				if ((entry.AppType == AppType.Game) && !g.Hidden)
				{
					games++;
				}

				if ((entry.AppType == AppType.Application) && !g.Hidden)
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

		/// <summary>
		///     Completely re-populates the game list.
		/// </summary>
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

					if (g.Name == null)
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

			mbtnAutoCategorize.Text = string.Format("Auto-Categorize ({0} Games)", AutoCatGameCount());

			Cursor = Cursors.Default;
		}

		/// <summary>
		///     Filters game list based on based on the current category selection and advanced filters
		/// </summary>
		/// <param name="preserveSelection">If true, will try to preserve game selection</param>
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

		private void FixWebBrowserRegistry()
		{
			const string installKey = @"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
			string entryLabel = GetType().Assembly.GetName().Name + ".exe";

			int version = new WebBrowser().Version.Major;
			if ((version < 8) || (version > 11))
			{
				return;
			}

			int value = version * 1000;

			RegistryKey existingSubKey = Registry.LocalMachine.OpenSubKey(installKey, false); // readonly key
			if ((existingSubKey == null) || ((existingSubKey.GetValue(entryLabel) != null) && (Convert.ToInt32(existingSubKey.GetValue(entryLabel)) == value)))
			{
				return;
			}

			new RegistryPermission(PermissionState.Unrestricted).Assert();
			try
			{
				existingSubKey = Registry.LocalMachine.OpenSubKey(installKey, RegistryKeyPermissionCheck.ReadWriteSubTree); // writable key
				if (existingSubKey == null)
				{
					return;
				}

				existingSubKey.SetValue(entryLabel, value, RegistryValueKind.DWord);
			}
			catch
			{
				MessageBox.Show(GlobalStrings.MainForm_AdminRights, GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.SplitContainer = splitContainer.SplitterDistance;
			Settings.SplitGame = splitGame.SplitterDistance;
			Settings.SplitBrowser = splitBrowser.SplitterDistance;

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
			// allow mousewheel scrolling for Add Category submenu.  Send 10 UP/DOWN per wheel click.
			DropdownMenuScrollWheelHandler.Enable();

			// Load saved forms settings
			Settings settings = Settings.Instance;

			splitContainer.SplitterDistance = settings.SplitContainer;
			settings.SplitGameContainerHeight = splitGame.Height;
			splitGame.SplitterDistance = settings.SplitGame;
			settings.SplitBrowserContainerWidth = splitBrowser.Width;
			splitBrowser.SplitterDistance = settings.SplitBrowser;

			ttHelp.Ext_SetToolTip(mchkAdvancedCategories, GlobalStrings.MainForm_Help_AdvancedCategories);

			InitializeObjectListView();

			LoadDatabase();

			// Save original width and height
			originalHeight = Height;
			originalWidth = Width;
			originalSplitDistanceMain = splitContainer.SplitterDistance;
			originalSplitDistanceSecondary = splitGame.SplitterDistance;
			originalSplitDistanceBrowser = splitBrowser.SplitterDistance;

			ClearStatus();

			if (Settings.Instance.UpdateAppInfoOnStart)
			{
				UpdateDatabaseFromAppInfo();
			}

			const int aWeekInSecs = 7 * 24 * 60 * 60;
			if (Settings.Instance.UpdateHltbOnStart && (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > (Database.LastHLTBUpdate + aWeekInSecs)))
			{
				UpdateGameDBFromHltb();
			}

			if (Settings.Instance.CheckForDepressurizerUpdates)
			{
				CheckForDepressurizerUpdates();
			}

			switch (Settings.Instance.StartupAction)
			{
				case StartupAction.Load:
					LoadProfile(Settings.Instance.ProfileToLoad, false);

					break;
				case StartupAction.Create:
					CreateProfile();

					break;
				default:
					OnProfileChange();

					break;
			}

			UpdateUIForSingleCat();
			UpdateEnabledStatesForGames();
			UpdateEnabledStatesForCategories();

			FlushStatus();

			if (CurrentProfile == null)
			{
				return;
			}

			// restore previous session
			SelectCategory();
			SelectFilter();
			SelectAutoCats();
		}

		/// <summary>
		///     Completely regenerates both the category and game lists
		/// </summary>
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
				i.StateImageIndex = MAX_FILTER_STATE;
			}
			else if ((i.StateImageIndex == MAX_FILTER_STATE) && !reverse)
			{
				i.StateImageIndex = -1;
			}
			else
			{
				i.StateImageIndex += reverse ? -1 : 1;
			}

			if (i.Tag.ToString() == Resources.Category_Games)
			{
				_advancedFilter.Game = i.StateImageIndex;
			}
			else if (i.Tag.ToString() == Resources.Category_Software)
			{
				_advancedFilter.Software = i.StateImageIndex;
			}
			else if (i.Tag.ToString() == Resources.Category_Uncategorized)
			{
				_advancedFilter.Uncategorized = i.StateImageIndex;
			}
			else if (i.Tag.ToString() == Resources.Category_Hidden)
			{
				_advancedFilter.Hidden = i.StateImageIndex;
			}
			else if (i.Tag.ToString() == Resources.Category_VR)
			{
				_advancedFilter.VR = i.StateImageIndex;
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
							_advancedFilter.Allow.Remove(category);

							break;
						case (int) AdvancedFilterState.Require:
							_advancedFilter.Require.Remove(category);

							break;
						case (int) AdvancedFilterState.Exclude:
							_advancedFilter.Exclude.Remove(category);

							break;
						default:

							throw new ArgumentOutOfRangeException(nameof(oldState), oldState, null);
					}

					switch (i.StateImageIndex)
					{
						case (int) AdvancedFilterState.None:

							break;
						case (int) AdvancedFilterState.Allow:
							_advancedFilter.Allow.Add(category);

							break;
						case (int) AdvancedFilterState.Require:
							_advancedFilter.Require.Add(category);

							break;
						case (int) AdvancedFilterState.Exclude:
							_advancedFilter.Exclude.Add(category);

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
						AddCategoryToSelectedGames(cat, false);
					}
				}
				else if ((item.StateImageIndex == 1) || ((item.StateImageIndex == 2) && !modKey))
				{
					item.StateImageIndex = 0;
					Category cat = item.Tag as Category;
					if (cat != null)
					{
						RemoveCategoryFromSelectedGames(cat);
					}
				}
			}
		}

		/// <summary>
		///     Attempts to import steam categories
		/// </summary>
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
				AddStatus(string.Format(GlobalStrings.MainForm_ImportedItems, count));
				if (count > 0)
				{
					MakeChange(true);
					FullListRefresh();
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(string.Format(GlobalStrings.MainForm_ErrorImportingSteamDataList, e.Message), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				Logger.Exception("Exception encountered while importing the remoteconfig file.", e);
				AddStatus(GlobalStrings.MainForm_ImportFailed);
			}

			Cursor = Cursors.Default;
		}

		private void InitializeLstGames()
		{
			tlstGames = new TypedObjectListView<GameInfo>(lstGames);

			//Aspect Getters
			tlstGames.GenerateAspectGetters();
			colGameID.AspectToStringConverter = delegate
			{
				return string.Empty;
			};

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
					return Resources.Category_NoGenres;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Contains(id, out DatabaseEntry entry) && (entry.Genres != null))
				{
					return string.Join(", ", entry.Genres);
				}

				return Resources.Category_NoGenres;
			};

			colFlags.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return Resources.Category_NoFlags;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Contains(id, out DatabaseEntry entry) && (entry.Flags != null))
				{
					return string.Join(", ", entry.Flags);
				}

				return Resources.Category_NoFlags;
			};

			colTags.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return Resources.Category_NoTags;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Contains(id, out DatabaseEntry entry) && (entry.Tags != null))
				{
					return string.Join(", ", entry.Tags);
				}

				return Resources.Category_NoTags;
			};

			colVRHeadsets.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return string.Empty;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Contains(id, out DatabaseEntry entry) && (entry.VrSupport.Headsets != null))
				{
					return string.Join(", ", entry.VrSupport.Headsets);
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
				if (Database.Contains(id, out DatabaseEntry entry) && (entry.VrSupport.Input != null))
				{
					return string.Join(", ", entry.VrSupport.Input);
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
				if (Database.Contains(id, out DatabaseEntry entry) && (entry.VrSupport.PlayArea != null))
				{
					return string.Join(", ", entry.VrSupport.PlayArea);
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
				if (Database.Contains(id, out DatabaseEntry entry) && (entry.LanguageSupport.Interface != null))
				{
					return string.Join(", ", entry.LanguageSupport.Interface);
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
				if (Database.Contains(id, out DatabaseEntry entry) && (entry.LanguageSupport.Subtitles != null))
				{
					return string.Join(", ", entry.LanguageSupport.Subtitles);
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
				if (Database.Contains(id, out DatabaseEntry entry) && (entry.LanguageSupport.FullAudio != null))
				{
					return string.Join(", ", entry.LanguageSupport.FullAudio);
				}

				return string.Empty;
			};

			colYear.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return Resources.Category_Unknown;
				}

				int id = ((GameInfo) g).Id;
				CultureInfo culture = Language.GetCultureInfo(Database.Language);
				if (Database.Contains(id, out DatabaseEntry entry) && DateTime.TryParse(entry.SteamReleaseDate, culture, DateTimeStyles.None, out DateTime releaseDate))
				{
					return releaseDate.Year.ToString();
				}

				return Resources.Category_Unknown;
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

				return Database.Contains(id, out DatabaseEntry entry) ? entry.TotalAchievements : 0;
			};

			colPlatforms.AspectGetter = delegate(object g)
			{
				if ((g == null) || !Database.Contains(((GameInfo) g).Id, out DatabaseEntry entry))
				{
					return "";
				}

				AppPlatforms platforms = entry.Platforms;

				return ((platforms & AppPlatforms.Linux) != 0) && (platforms != AppPlatforms.All) ? platforms + ", SteamOS" : platforms.ToString();
			};

			colDevelopers.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return Resources.Category_Unknown;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Contains(id, out DatabaseEntry entry) && (entry.Developers != null))
				{
					return string.Join(", ", entry.Developers);
				}

				return Resources.Category_Unknown;
			};

			colPublishers.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return Resources.Category_Unknown;
				}

				int id = ((GameInfo) g).Id;
				if (Database.Contains(id, out DatabaseEntry entry) && (entry.Publishers != null))
				{
					return string.Join(", ", entry.Publishers);
				}

				return Resources.Category_Unknown;
			};

			colNumberOfReviews.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return 0;
				}

				int id = ((GameInfo) g).Id;

				return Database.Contains(id, out DatabaseEntry entry) ? entry.ReviewTotal : 0;
			};

			colReviewScore.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return 0;
				}

				int id = ((GameInfo) g).Id;

				return Database.Contains(id, out DatabaseEntry entry) ? entry.ReviewPositivePercentage : 0;
			};

			colReviewLabel.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return 0;
				}

				int id = ((GameInfo) g).Id;
				if (!Database.Contains(id, out DatabaseEntry entry))
				{
					return 0;
				}

				int reviewTotal = entry.ReviewTotal;
				int reviewPositivePercentage = entry.ReviewPositivePercentage;
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
			};

			colHltbMain.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return 0;
				}

				int id = ((GameInfo) g).Id;

				return Database.Contains(id, out DatabaseEntry entry) ? entry.HltbMain : 0;
			};

			colHltbExtras.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return 0;
				}

				int id = ((GameInfo) g).Id;

				return Database.Contains(id, out DatabaseEntry entry) ? entry.HltbExtras : 0;
			};

			colHltbCompletionist.AspectGetter = delegate(object g)
			{
				if (g == null)
				{
					return 0;
				}

				int id = ((GameInfo) g).Id;

				return Database.Contains(id, out DatabaseEntry entry) ? entry.HltbCompletionist : 0;
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

				return reviewScore <= 0 ? Resources.Category_Unknown : reviewScore.ToString(CultureInfo.InvariantCulture) + '%';
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

				return reviewLabels.ContainsKey(index) ? reviewLabels[index] : Resources.Category_Unknown;
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
			colCategories.ClusteringStrategy = new CommaClusteringStrategy();
			colGenres.ClusteringStrategy = new CommaClusteringStrategy();
			colFlags.ClusteringStrategy = new CommaClusteringStrategy();
			colTags.ClusteringStrategy = new CommaClusteringStrategy();
			colVRHeadsets.ClusteringStrategy = new CommaClusteringStrategy();
			colVRInput.ClusteringStrategy = new CommaClusteringStrategy();
			colVRPlayArea.ClusteringStrategy = new CommaClusteringStrategy();
			colLanguageInterface.ClusteringStrategy = new CommaClusteringStrategy();
			colLanguageSubtitles.ClusteringStrategy = new CommaClusteringStrategy();
			colLanguageFullAudio.ClusteringStrategy = new CommaClusteringStrategy();
			colPlatforms.ClusteringStrategy = new CommaClusteringStrategy();
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

			if (!string.IsNullOrWhiteSpace(Settings.LstGamesState))
			{
				lstGames.RestoreState(Convert.FromBase64String(Settings.LstGamesState));
			}
		}

		private void InitializeObjectListView()
		{
			// Skin the Game List
			lstGames.HeaderFormatStyle = new HeaderFormatStyle();
			lstGames.HeaderFormatStyle.SetBackColor(primaryDark);
			lstGames.HeaderFormatStyle.SetForeColor(headerFontColor);
			lstGames.HeaderFormatStyle.SetFont(new Font("Arial", 10, FontStyle.Bold));
			lstGames.HeaderFormatStyle.Hot.BackColor = primaryLight;
			lstGames.ForeColor = textColor;
			lstGames.BackColor = formColor;
			lstGames.SelectedForeColor = textColor;
			lstGames.SelectedBackColor = accent;
			lstGames.UnfocusedSelectedForeColor = textColor;
			lstGames.UnfocusedSelectedBackColor = primary;
			lstGames.Font = new Font("Arial", 10);
		}

		/// <summary>
		///     Launchs selected game
		///     <param name="g">Game to launch</param>
		/// </summary>
		private void LaunchGame(GameInfo g)
		{
			if (g == null)
			{
				return;
			}

			g.LastPlayed = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			Process.Start(g.Executable);
		}

		/// <summary>
		///     Prompts user for a profile file to load, then loads it.
		/// </summary>
		private void LoadProfile()
		{
			if (!CheckForUnsaved())
			{
				return;
			}

			OpenFileDialog dlg = new OpenFileDialog
			{
				DefaultExt = "profile",
				AddExtension = true,
				CheckFileExists = true,
				Filter = GlobalStrings.DlgProfile_Filter,
				InitialDirectory = Path.GetDirectoryName(CurrentProfile == null ? Assembly.GetExecutingAssembly().CodeBase : CurrentProfile.FilePath)
			};

			DialogResult res = dlg.ShowDialog();
			if (res == DialogResult.OK)
			{
				LoadProfile(dlg.FileName, false);
			}
		}

		/// <summary>
		///     Loads the given profile file.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="checkForChanges"></param>
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
				MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_ErrorLoadingProfile, e.Message), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				Logger.Exception(GlobalStrings.MainForm_Log_ExceptionLoadingProfile, e);
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
			if (dragOldCat >= 0)
			{
				lstCategories.SelectedIndices.Add(dragOldCat);
			}

			isDragging = false;
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
			isDragging = true;
			dragOldCat = lstCategories.SelectedIndices.Count > 0 ? lstCategories.SelectedIndices[0] : -1;

			SetDragDropEffect(e);
		}

		private void lstCategories_DragLeave(object sender, EventArgs e)
		{
			isDragging = false;
			lstCategories.SelectedIndices.Clear();
			if (dragOldCat >= 0)
			{
				lstCategories.SelectedIndices.Add(dragOldCat);
			}
		}

		private void lstCategories_DragOver(object sender, DragEventArgs e)
		{
			if (isDragging)
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
			if (!isDragging)
			{
				object nowSelected = null;
				if (lstCategories.SelectedItems.Count > 0)
				{
					ListViewItem selItem = lstCategories.SelectedItems[0];
					nowSelected = selItem.Tag == null ? selItem.Text : selItem.Tag;
				}

				if (nowSelected != lastSelectedCat)
				{
					OnViewChange();
					lastSelectedCat = nowSelected;
				}

				UpdateEnabledStatesForCategories();
			}
		}

		private void lstGames_ColumnReordered(object sender, ColumnReorderedEventArgs e)
		{
			columnReorderThread = new Thread(ColumnReorderWorker);
			columnReorderThread.Start();
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
			string bannerFile = Core.Helpers.Location.File.Banner(g.Id);
			if (!File.Exists(bannerFile))
			{
				return;
			}

			try
			{
				ImageDecoration decoration = new ImageDecoration(Image.FromFile(bannerFile))
				{
					ShrinkToWidth = true,
					AdornmentCorner = ContentAlignment.TopLeft,
					ReferenceCorner = ContentAlignment.TopLeft,
					Transparency = 255
				};

				//e.SubItem.Decoration = decoration;
				e.SubItem.Decorations.Add(decoration);
			}
			catch
			{
				// Some images used to get saved in a corrupted state, and would throw an exception when loaded here.
				// Just in case there are still images like that around, drop exceptions from them.
			}

			// Add Early Access banner
			if (Database.Contains(g.Id, out DatabaseEntry entry) && (entry.Tags != null))
			{
				if (entry.Tags.Contains(EARLY_ACCESS))
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

			TextDecoration td = new TextDecoration(g.Id.ToString(), ContentAlignment.BottomLeft)
			{
				Font = new Font(lstGames.Font.Name, 8),
				Wrap = false,
				TextColor = textColor,
				BackColor = listBackground,
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
				e.Item.BackColor = listBackground;
			}

			if (g.Hidden)
			{
				e.Item.BackColor = primaryLight;
			}
		}

		private void lstGames_ItemDrag(object sender, ItemDragEventArgs e)
		{
			int[] selectedGames = new int[lstGames.SelectedObjects.Count];
			for (int i = 0; i < lstGames.SelectedObjects.Count; i++)
			{
				selectedGames[i] = tlstGames.SelectedObjects[i].Id;
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
			string storeLanguage = Steam.GetStoreLanguage(Database.Language);

			contextGameFav_Yes.Checked = false;
			contextGameFav_No.Checked = false;
			contextGameHidden_Yes.Checked = false;
			contextGameHidden_No.Checked = false;

			if (lstGames.SelectedObjects.Count > 0)
			{
				GameInfo g = tlstGames.SelectedObjects[0];

				if ((tlstGames.SelectedObjects.Count == 1) && g.IsFavorite())
				{
					contextGameFav_Yes.Checked = true;
				}
				else if (tlstGames.SelectedObjects.Count == 1)
				{
					contextGameFav_No.Checked = true;
				}

				if ((tlstGames.SelectedObjects.Count == 1) && g.Hidden)
				{
					contextGameHidden_Yes.Checked = true;
				}
				else if (tlstGames.SelectedObjects.Count == 1)
				{
					contextGameHidden_No.Checked = true;
				}

				if (webBrowser1.Visible)
				{
					webBrowser1.ScriptErrorsSuppressed = true;
					webBrowser1.Navigate(string.Format(Constants.SteamStoreApp + "?l=" + storeLanguage, g.Id));
				}
			}
			else if (webBrowser1.Visible)
			{
				try
				{
					if (tlstGames.Objects.Count > 0)
					{
						GameInfo g = tlstGames.Objects[0];
						webBrowser1.ScriptErrorsSuppressed = true;
						webBrowser1.Navigate(string.Format(Constants.SteamStoreApp + "?l=" + storeLanguage, g.Id));
					}
					else
					{
						webBrowser1.ScriptErrorsSuppressed = true;
						webBrowser1.Navigate(Constants.SteamStore + "?l=" + storeLanguage);
					}
				}
				catch { }
			}
		}

		private void lstGames_SelectionChanged(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			UpdateSelectedStatusText();
			UpdateEnabledStatesForGames();
			UpdateGameCheckStates();
			UpdateAutoCatSelected_StatusMessage();
			mbtnAutoCategorize.Text = string.Format("Auto-Categorize ({0} Games)", AutoCatGameCount());
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

			ListViewItem item = lstMultiCat.SelectedItems[0];
			HandleMultiCatItemActivation(item, ModifierKeys == Keys.Shift);
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
			if (doubleClick)
			{
				// prevent double click from changing checked value.  Double click opens edit dialog.
				doubleClick = false;
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
					doubleClick = true;
				}
			}
		}

		/// <summary>
		///     Sets the unsaved changes flag to the given value and takes the requisite UI updating action
		/// </summary>
		/// <param name="changes"></param>
		private void MakeChange(bool changes)
		{
			unsavedChanges = changes;
			UpdateTitle();
		}

		/// <summary>
		///     Saves a Steam configuration file. Asks the user to select the file to save as.
		/// </summary>
		/// <returns>True if save was completed, false otherwise</returns>
		private void ManualExportConfig()
		{
			if (CurrentProfile == null)
			{
				return;
			}

			SaveFileDialog dlg = new SaveFileDialog();
			DialogResult res = dlg.ShowDialog();
			if (res == DialogResult.OK)
			{
				Cursor = Cursors.WaitCursor;
				try
				{
					CurrentProfile.GameData.ExportSteamConfigFile(dlg.FileName, Settings.Instance.RemoveExtraEntries);
					AddStatus(GlobalStrings.MainForm_DataExported);
				}
				catch (Exception e)
				{
					MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_ErrorManualExport, e.Message), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
					Logger.Exception(GlobalStrings.MainForm_Log_ExceptionExport, e);
					AddStatus(GlobalStrings.MainForm_ExportFailed);
				}

				Cursor = Cursors.Default;
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
				if ((tlstGames.SelectedObjects.Count == 0) && mchkAutoCatSelected.Checked)
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
			mbtnAutoCategorize.Text = string.Format("Auto-Categorize ({0} Games)", AutoCatGameCount());
		}

		private void mchkBrowser_CheckedChanged(object sender, EventArgs e)
		{
			if (mchkBrowser.CheckState == CheckState.Checked)
			{
				FixWebBrowserRegistry();
				splitBrowser.Panel2Collapsed = false;
				webBrowser1.Visible = true;
				lstGames_SelectedIndexChanged(null, null);
			}
			else if (mchkBrowser.CheckState == CheckState.Unchecked)
			{
				splitBrowser.Panel2Collapsed = true;
				webBrowser1.Visible = false;
			}
		}

		private void menu_About_Click(object sender, EventArgs e)
		{
			new DlgAbout().ShowDialog();
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
			DlgClose close = new DlgClose(GlobalStrings.MainForm_SaveProfileConfirm, GlobalStrings.MainForm_SaveProfile, SystemIcons.Question.ToBitmap(), false, CurrentProfile.AutoExport);
			DialogResult res = close.ShowDialog();
			if (res == DialogResult.Yes)
			{
				CurrentProfile.AutoExport = close.Export;
				SaveProfile();
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
			string sharedconfigPath = Path.GetDirectoryName(string.Format(Constants.ConfigFilePath, Settings.Instance.SteamPath, Profile.ID64toDirName(CurrentProfile.SteamID64)));
			DlgRestore restore = new DlgRestore(sharedconfigPath);

			DialogResult res = restore.ShowDialog();

			if (restore.Restored)
			{
				ClearStatus();
				ImportConfig();
				FlushStatus();
			}
		}

		private void menu_Profile_Restore_Profile_Click(object sender, EventArgs e)
		{
			string profilePath = Path.GetDirectoryName(CurrentProfile.FilePath);
			DlgRestore restore = new DlgRestore(profilePath);

			DialogResult res = restore.ShowDialog();

			if (restore.Restored)
			{
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
			using (DBEditDlg dlg = new DBEditDlg(CurrentProfile != null ? CurrentProfile.GameData : null))
			{
				dlg.ShowDialog();
			}

			LoadDatabase();
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
			DlgOptions dlg = new DlgOptions();

			// jpodadera. Save culture of actual language
			CultureInfo actualCulture = Thread.CurrentThread.CurrentUICulture;

			dlg.ShowDialog();

			// jpodadera. If language has been changed, reload resources of main window
			if (actualCulture.Name != Thread.CurrentThread.CurrentUICulture.Name)
			{
				ComponentResourceManager resources = new ComponentResourceManager(typeof(FormMain));
				resources.ApplyResources(this, Name, Thread.CurrentThread.CurrentUICulture);

				// jpodadera. Save actual size and recover original size before reload resources of controls
				int actualSplitDistanceMain = splitContainer.SplitterDistance;
				int actualSplitDistanceSecondary = splitGame.SplitterDistance;
				int actualSplitDistanceCategories = splitCategories.SplitterDistance;
				int actualSplitDistanceBrowser = splitBrowser.SplitterDistance;

				changeLanguageControls(this, resources, Thread.CurrentThread.CurrentUICulture);

				// jpodadera. Recover previous size
				splitContainer.SplitterDistance = actualSplitDistanceMain;
				splitGame.SplitterDistance = actualSplitDistanceSecondary;
				splitCategories.SplitterDistance = actualSplitDistanceCategories;
				splitBrowser.SplitterDistance = actualSplitDistanceBrowser;

				FullListRefresh();
			}

			FlushStatus();
		}

		private void menu_Tools_SingleCat_Click(object sender, EventArgs e)
		{
			Settings.Instance.SingleCatMode = !Settings.Instance.SingleCatMode;
			UpdateUIForSingleCat();
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

		/// <summary>
		///     Updates UI after a profile is created, loaded, modified or closed.
		/// </summary>
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

		/// <summary>
		///     Does all list updating that's required if the filter changes (category selection changes).
		/// </summary>
		private void OnViewChange()
		{
			FilterGamelist(false);
		}

		/// <summary>
		///     Rebuild all the list view items in the gamelist, preserving as much state as is possible
		/// </summary>
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

		/// <summary>
		///     Removes the given category from all selected games.
		/// </summary>
		/// <param name="cat">Category to remove.</param>
		private void RemoveCategoryFromSelectedGames(Category cat)
		{
			if (lstGames.SelectedObjects.Count > 0)
			{
				Cursor.Current = Cursors.WaitCursor;
				foreach (GameInfo g in tlstGames.SelectedObjects)
				{
					g.RemoveCategory(cat);
				}

				FillAllCategoryLists();
				if (lstCategories.SelectedItems[0].Tag is Category && ((Category) lstCategories.SelectedItems[0].Tag == cat))
				{
					FilterGamelist(false);
				}
				else
				{
					FilterGamelist(true);
				}

				MakeChange(true);
				Cursor.Current = Cursors.Default;
			}
		}

		/// <summary>
		///     Removes any categories with no games assigned.
		/// </summary>
		private void RemoveEmptyCats()
		{
			int count = CurrentProfile.GameData.RemoveEmptyCategories();
			AddStatus(string.Format(GlobalStrings.MainForm_RemovedEmptyCategories, count));
			FillAllCategoryLists();
		}

		/// <summary>
		///     Removes all selected games. Prompts for confirmation.
		/// </summary>
		private void RemoveGames()
		{
			int selectCount = lstGames.SelectedObjects.Count;
			if (selectCount > 0)
			{
				if (MessageBox.Show(string.Format(GlobalStrings.MainForm_RemoveGame, selectCount, selectCount == 1 ? "" : "s"), GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					Cursor.Current = Cursors.WaitCursor;
					int ignored = 0;
					int removed = 0;
					foreach (GameInfo g in tlstGames.SelectedObjects)
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
						AddStatus(string.Format(GlobalStrings.MainForm_RemovedGame, removed, removed == 1 ? "" : "s"));
						MakeChange(true);
					}

					if (ignored > 0)
					{
						AddStatus(string.Format(GlobalStrings.MainForm_IgnoredGame, ignored, ignored == 1 ? "" : "s"));
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

			Category c = lstCategories.SelectedItems[0].Tag as Category;
			if ((c == null) || (c == CurrentProfile.GameData.FavoriteCategory))
			{
				return;
			}

			GetStringDlg dlg = new GetStringDlg(c.Name, string.Format(GlobalStrings.MainForm_RenameCategory, c.Name), GlobalStrings.MainForm_EnterNewName, GlobalStrings.MainForm_Rename);
			if (dlg.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			string newName = dlg.Value;
			if (newName == c.Name)
			{
				return;
			}

			if (ValidateCategoryName(newName))
			{
				Category newCat = CurrentProfile.GameData.RenameCategory(c, newName);
				if (newCat != null)
				{
					FillAllCategoryLists();
					RebuildGamelist();
					MakeChange(true);
					for (int index = 2; index < lstCategories.Items.Count; index++)
					{
						if (lstCategories.Items[index].Tag != newCat)
						{
							continue;
						}

						lstCategories.SelectedIndices.Add(index);

						break;
					}

					AddStatus(string.Format(GlobalStrings.MainForm_CategoryRenamed, c.Name));

					return;
				}
			}

			MessageBox.Show(string.Format(GlobalStrings.MainForm_NameIsInUse, newName), GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void RenameFilter(Filter f)
		{
			if (!AdvancedCategoryFilter)
			{
				return;
			}

			GetStringDlg dlg = new GetStringDlg(f.Name, string.Format(GlobalStrings.MainForm_RenameFilter, f.Name), GlobalStrings.MainForm_EnterNewName, GlobalStrings.MainForm_Rename);
			if ((dlg.ShowDialog() == DialogResult.OK) && (f.Name != dlg.Value))
			{
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

		private void reorderFillerColumn()
		{
			if (lstGames.InvokeRequired)
			{
				RemoveItemCallback callback = reorderFillerColumn;
				Invoke(callback);
			}
			else
			{
				// filler column should always be last column
				colFiller.DisplayIndex = lstGames.ColumnsInDisplayOrder.Count - 1;
			}
		}

		private void ResortToolStripItemCollection(ToolStripItemCollection coll)
		{
			ArrayList oAList = new ArrayList(coll);
			oAList.Sort(new ToolStripItemComparer());
			coll.Clear();

			foreach (ToolStripItem oItem in oAList)
			{
				coll.Add(oItem);
			}
		}

		private void RunAutoCats(List<AutoCat> autocats, bool first, bool group = false)
		{
			foreach (AutoCat ac in autocats)
			{
				if (ac == null)
				{
					continue;
				}

				if (ac.AutoCatType == AutoCatType.Group)
				{
					AutoCatGroup acg = (AutoCatGroup) ac;
					RunAutoCats(CurrentProfile.CloneAutoCatList(acg.Autocats, CurrentProfile.GameData.GetFilter(acg.Filter)), first, true);
				}
				else
				{
					if (!ac.Selected && !group)
					{
						continue;
					}

					ClearStatus();
					Autocategorize(mchkAutoCatSelected.Checked, ac, first, false);
					first = false;
					FlushStatus();
				}
			}
		}

		private void SaveDatabase()
		{
			try
			{
				Database.Save(Core.Helpers.Location.File.Database);
				AddStatus(GlobalStrings.MainForm_Status_SavedDB);
			}
			catch (Exception e)
			{
				Logger.Exception(GlobalStrings.MainForm_Log_ExceptionAutosavingDB, e);
				MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_ErrorAutosavingDB, e.Message), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
					DialogResult res = MessageBox.Show(string.Format(GlobalStrings.MainForm_OverwriteFilterName, dlg.Value), GlobalStrings.MainForm_Overwrite, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

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
					f.Uncategorized = _advancedFilter.Uncategorized;
					f.Hidden = _advancedFilter.Hidden;
					f.VR = _advancedFilter.VR;
					f.Allow = _advancedFilter.Allow;
					f.Require = _advancedFilter.Require;
					f.Exclude = _advancedFilter.Exclude;
					if (refresh)
					{
						AddStatus(string.Format(GlobalStrings.MainForm_FilterAdded, f.Name));
						RefreshFilters();
						cboFilter.SelectedItem = f;
					}
				}
				else
				{
					MessageBox.Show(string.Format(GlobalStrings.MainForm_CouldNotAddFilter, dlg.Value), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}

		/// <summary>
		///     Saves profile data to a file and performs any related tasks. This is the main saving function, all saves go through
		///     this function.
		/// </summary>
		/// <param name="path">Path to save to. If null, just saves profile to its current path.</param>
		/// <returns>True if successful, false if there is a failure</returns>
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

			Settings.LstGamesState = Convert.ToBase64String(lstGames.SaveState());

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
				MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_ErrorSavingProfile, e.Message), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Logger.Exception(GlobalStrings.MainForm_Log_ExceptionSavingProfile, e);
				AddStatus(GlobalStrings.MainForm_FailedSaveProfile);

				return false;
			}
		}

		/// <summary>
		///     Prompts user for a file location and saves profile
		/// </summary>
		private void SaveProfileAs()
		{
			if (!ProfileLoaded)
			{
				return;
			}

			SaveFileDialog dlg = new SaveFileDialog
			{
				DefaultExt = "profile",
				AddExtension = true,
				CheckPathExists = true,
				Filter = GlobalStrings.DlgProfile_Filter,
				InitialDirectory = Path.GetDirectoryName(CurrentProfile.FilePath)
			};

			DialogResult res = dlg.ShowDialog();
			if (res == DialogResult.OK)
			{
				SaveProfile(dlg.FileName);
			}
		}

		private void SaveSelectedAutoCats()
		{
			string autoCats = string.Empty;
			foreach (ListViewItem item in lvAutoCatType.SelectedItems)
			{
				if (string.IsNullOrWhiteSpace(item.Name))
				{
					continue;
				}

				autoCats += $"{item.Name},";
			}

			autoCats = autoCats.TrimEnd(',');
			Settings.SelectedAutoCats = autoCats;
		}

		private void SelectAutoCats()
		{
			if (string.IsNullOrWhiteSpace(Settings.SelectedAutoCats))
			{
				return;
			}

			string[] autoCats = Settings.SelectedAutoCats.Split(',').ToArray();
			foreach (string autoCat in autoCats)
			{
				if (string.IsNullOrWhiteSpace(autoCat))
				{
					continue;
				}

				foreach (ListViewItem item in lvAutoCatType.Items)
				{
					if (!item.Name.Equals(autoCat))
					{
						continue;
					}

					item.Checked = true;
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
				ListViewItem item = lstCategories.Items[i];
				if (!item.Name.Equals(Settings.SelectedCategory))
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
				if (!name.Equals(Settings.SelectedFilter))
				{
					continue;
				}

				mchkAdvancedCategories.Checked = true;
				cboFilter.SelectedIndex = i;
				cboFilter.Text = name;
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
				_advancedFilter = new Filter(ADVANCED_FILTER);
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

		private void SetDragDropEffect(DragEventArgs e)
		{
			if (Settings.Instance.SingleCatMode /*|| (e.KeyState & 4) == 4*/)
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

		private void SetItemState(ListViewItem i, int state)
		{
			i.StateImageIndex = state;

			if (i.Tag.ToString() == Resources.Category_Games)
			{
				_advancedFilter.Game = state;
			}
			else if (i.Tag.ToString() == Resources.Category_Software)
			{
				_advancedFilter.Software = state;
			}
			else if (i.Tag.ToString() == Resources.Category_Uncategorized)
			{
				_advancedFilter.Uncategorized = state;
			}
			else if (i.Tag.ToString() == Resources.Category_Hidden)
			{
				_advancedFilter.Hidden = state;
			}
			else if (i.Tag.ToString() == Resources.Category_VR)
			{
				_advancedFilter.VR = state;
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
						_advancedFilter.Allow.Add(category);
						_advancedFilter.Require.Remove(category);
						_advancedFilter.Exclude.Remove(category);

						break;
					case AdvancedFilterState.Require:
						_advancedFilter.Allow.Remove(category);
						_advancedFilter.Require.Add(category);
						_advancedFilter.Exclude.Remove(category);

						break;
					case AdvancedFilterState.Exclude:
						_advancedFilter.Allow.Remove(category);
						_advancedFilter.Require.Remove(category);
						_advancedFilter.Exclude.Add(category);

						break;
					case AdvancedFilterState.None:
						_advancedFilter.Allow.Remove(category);
						_advancedFilter.Require.Remove(category);
						_advancedFilter.Exclude.Remove(category);

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
				return g.IncludeGame(_advancedFilter);
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
				return Database.Contains(g.Id, out DatabaseEntry entry) && (entry.AppType == AppType.Game);
			}

			// <Software>
			if (lstCategories.SelectedItems[0].Tag.ToString() == Resources.Category_Software)
			{
				return Database.Contains(g.Id, out DatabaseEntry entry) && (entry.AppType == AppType.Application);
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

		/// <summary>
		///     Unloads the current profile or game list, making sure the user gets the option to save any changes.
		/// </summary>
		/// <returns>True if there is now no loaded profile, false otherwise.</returns>
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
			if ((tlstGames.SelectedObjects.Count == 0) && mchkAutoCatSelected.Checked)
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

		private void UpdateDatabaseFromAppInfo()
		{
			string path = string.Format(Constants.AppInfoPath, Settings.Instance.SteamPath);
			if (!File.Exists(path))
			{
				MessageBox.Show(Resources.AppInfo_NotFound, Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);

				return;
			}

			int num = 0;
			try
			{
				num = Database.UpdateFromAppInfo(path);
			}
			catch (Exception e)
			{
				Logger.Exception("MainForm: Error while updating database from appinfo:", e);
				MessageBox.Show(string.Format(Resources.AppInfo_Exception, e.Message), Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			finally
			{
				if (num > 0)
				{
					AddStatus(string.Format(GlobalStrings.MainForm_Status_AppInfoAutoupdate, num));

					if (Settings.Instance.AutoSaveDatabase)
					{
						SaveDatabase();
					}
				}
			}
		}

		private void UpdateEnabledStatesForCategories()
		{
			Category c = null;
			foreach (ListViewItem item in lstCategories.SelectedItems)
			{
				c = item.Tag as Category;
				if ((c != null) && !((CurrentProfile != null) && (c == CurrentProfile.GameData.FavoriteCategory)))
				{
					break;
				}

				c = null;
			}

			mbtnCatDelete.Enabled = c != null;
			c = lstCategories.SelectedItems.Count > 0 ? lstCategories.SelectedItems[0].Tag as Category : null;
			mbtnCatRename.Enabled = (c != null) && !((CurrentProfile != null) && (c == CurrentProfile.GameData.FavoriteCategory));
		}

		/// <summary>
		///     Updates enabled states for all game and category buttons
		/// </summary>
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
				foreach (GameInfo game in tlstGames.SelectedObjects)
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

		/// <summary>
		///     Updates the database using data from howlongtobeatsteam.com. Displays an error message on failure. Saves the DB
		///     afterwards if AutosaveDB is set.
		/// </summary>
		private void UpdateGameDBFromHltb()
		{
			Cursor = Cursors.WaitCursor;

			HltbPrcDlg dlg = new HltbPrcDlg();
			DialogResult res = dlg.ShowDialog();

			if (dlg.Error != null)
			{
				Logger.Error(GlobalStrings.DBEditDlg_Log_ExceptionHltb, dlg.Error.Message);
				MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_ErrorHltb, dlg.Error.Message), GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				AddStatus(GlobalStrings.DBEditDlg_ErrorUpdatingHltb);
			}
			else
			{
				if ((res == DialogResult.Cancel) || (res == DialogResult.Abort))
				{
					AddStatus(GlobalStrings.DBEditDlg_CanceledHltbUpdate);
				}
				else
				{
					AddStatus(string.Format(GlobalStrings.MainForm_Status_HltbAutoupdate, dlg.Updated));
					if ((dlg.Updated > 0) && Settings.Instance.AutoSaveDatabase)
					{
						SaveDatabase();
					}
				}
			}

			FullListRefresh();
			Cursor = Cursors.Default;
		}

		/// <summary>
		///     Updates the game list for the loaded profile.
		/// </summary>
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
					AddStatus(string.Format(GlobalStrings.MainForm_Status_LocalUpdate, totalApps, newApps));
					success = true;
				}
				catch (Exception e)
				{
					MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_LocalUpdateError, e.Message), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					Logger.Exception(GlobalStrings.MainForm_Log_ExceptionLocalUpdate, e);
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
						Logger.Exception(GlobalStrings.MainForm_Log_ExceptionWebUpdateDialog, updateDlg.Error);
						AddStatus(string.Format(GlobalStrings.MainForm_ErrorDownloadingProfileData, updateDlg.UseHtml ? "HTML" : "XML"));
						MessageBox.Show(string.Format(GlobalStrings.MainForm_ErrorDowloadingProfile, updateDlg.Error.Message), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
								AddStatus(string.Format(GlobalStrings.MainForm_DownloadedGames, updateDlg.Fetched, updateDlg.Added, updateDlg.UseHtml ? "HTML" : "XML"));
								FullListRefresh();
							}
						}
					}
				}
				catch (Exception e)
				{
					Logger.Exception(GlobalStrings.MainForm_Log_ExceptionWebUpdate, e);
					MessageBox.Show(string.Format(GlobalStrings.MainForm_ErrorDowloadingProfile, e.Message), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
					AddStatus(GlobalStrings.MainForm_DownloadFailed);
				}
			}

			Cursor = Cursors.Default;
		}

		/// <summary>
		///     Updates the text displaying the number of items in the game list
		/// </summary>
		private void UpdateSelectedStatusText()
		{
			mlblStatusSelection.Font = new Font("Arial", 9);
			mlblStatusSelection.Text = string.Format(GlobalStrings.MainForm_SelectedDisplayed, lstGames.SelectedObjects.Count, lstGames.GetItemCount());
		}

		/// <summary>
		///     Updates the window title.
		/// </summary>
		private void UpdateTitle()
		{
			StringBuilder sb = new StringBuilder("Depressurizer");
			if (ProfileLoaded)
			{
				sb.Append(" - ");
				sb.Append(Path.GetFileName(CurrentProfile.FilePath));
			}

			if (Settings.Instance.SingleCatMode)
			{
				sb.Append(" [");
				sb.Append(GlobalStrings.MainForm_SingleCategoryMode);
				sb.Append("]");
			}

			if (unsavedChanges)
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

		/// <summary>
		///     Update UI to match current state of the SingleCatMode setting
		/// </summary>
		private void UpdateUIForSingleCat()
		{
			bool sCat = Settings.Instance.SingleCatMode;
			menu_Tools_SingleCat.Checked = sCat;
			UpdateTitle();
		}

		/// <summary>
		///     Checks to see if a category name is valid. Does not make sure it isn't already in use. If the name is not valid,
		///     displays a warning.
		/// </summary>
		/// <param name="name">Name to check</param>
		/// <returns>True if valid, false otherwise</returns>
		private bool ValidateCategoryName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				MessageBox.Show(GlobalStrings.MainForm_CategoryNamesNotEmpty, GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				return false;
			}

			return true;
		}

		private bool ValidateFilterName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				MessageBox.Show(GlobalStrings.MainForm_FilterNamesNotEmpty, GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				return false;
			}

			return true;
		}

		#endregion
	}
}
