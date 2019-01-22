using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;
using Depressurizer.Properties;
using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.Win32;
using Rallion;

namespace Depressurizer
{
    public partial class FormMain : MaterialForm
    {
        #region Constants

        private const string ADVANCED_FILTER = "ADVANCED_FILTER";

        private const string BIG_DOWN = "{DOWN},{DOWN},{DOWN},{DOWN},{DOWN},{DOWN},{DOWN},{DOWN},{DOWN},{DOWN}";

        private const string BIG_UP = "{UP},{UP},{UP},{UP},{UP},{UP},{UP},{UP},{UP},{UP}";

        private const int MAX_FILTER_STATE = 2;

        #endregion

        #region Fields

        private readonly StringBuilder _statusBuilder = new StringBuilder();

        private Filter _advFilter = new Filter(ADVANCED_FILTER);

        private Regex _currentFilterRegex;

        /// <summary>
        ///     Stores the last selected category to minimize the number of list refreshes.
        /// </summary>
        private object _lastSelectedCategory;

        private TypedObjectListView<GameInfo> _typedListGames;

        private bool _unsavedChanges;

        // used to prevent moving the filler column in the game list
        private Thread columnReorderThread;

        // Used to prevent double clicking in Autocat listview from changing checkstate
        private bool doubleClick;

        private int dragOldCat;

        // Allow visual feedback when dragging over the cat list
        private bool isDragging;

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
            MaterialSkinManager.AddFormToManage(this);
            MaterialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            MaterialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue700, TextShade.WHITE);

            lstCategories.BackColor = FormColor;
            lstCategories.ForeColor = TextColor;

            InitializeLstGames();
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

        private static Color AccentColor => Color.FromArgb(255, 0, 145, 234);

        private static Database Database => Database.Instance;

        private static string EarlyAccessTag => "Early Access";

        private static Color FormColor => Color.FromArgb(255, 42, 42, 44);

        private static Color HeaderFontColor => Color.FromArgb(255, 169, 167, 167);

        /// <summary>
        ///     Color indicating that the given search Regex is invalid.
        /// </summary>
        private static Color InvalidSearchRegexColor => Color.PaleVioletRed;

        private static Color ListBackgroundColor => Color.FromArgb(255, 22, 22, 22);

        private static Logger Logger => Logger.Instance;

        private static MaterialSkinManager MaterialSkinManager => MaterialSkinManager.Instance;

        private static Color PrimaryColor => Color.FromArgb(255, 55, 71, 79);

        private static Color PrimaryDarkColor => Color.FromArgb(255, 38, 50, 56);

        private static Color PrimaryLightColor => Color.FromArgb(255, 96, 125, 139);

        private static Settings Settings => Settings.Instance;

        private static Color TextColor => Color.FromArgb(255, 255, 255, 255);

        private bool AdvancedCategoryFilter => mchkAdvancedCategories.Checked;

        #endregion

        #region Public Methods and Operators

        public void AddStatus(string message)
        {
            _statusBuilder.Append(message);
            _statusBuilder.Append(' ');
        }

        /// <summary>
        ///     Empties the status builder
        /// </summary>
        public void ClearStatus()
        {
            _statusBuilder.Clear();
        }

        /// <summary>
        ///     Sets the status text to the builder text, and clear the builder text.
        /// </summary>
        public void FlushStatus()
        {
            mlblStatusMsg.Font = new Font("Arial", 9);
            mlblStatusMsg.Text = _statusBuilder.ToString();
            _statusBuilder.Clear();
        }

        #endregion

        #region Methods

        private static string CategoryListViewItemText(Category category)
        {
            return CategoryListViewItemText(category.Name, category.Count);
        }

        private static string CategoryListViewItemText(string categoryName, int categoryCount)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0} ({1})", categoryName, categoryCount);
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
        private static void ChangeLanguageControls(Control c, ComponentResourceManager resources, CultureInfo newCulture)
        {
            if (c == null)
            {
                return;
            }

            Rectangle currentBounds = c.Bounds;

            switch (c)
            {
                case MenuStrip menuStrip:
                {
                    foreach (ToolStripDropDownItem mItem in menuStrip.Items)
                    {
                        ChangeLanguageToolStripItems(mItem, resources, newCulture);
                    }

                    break;
                }
                case ListView listView:
                {
                    foreach (ColumnHeader columnHeader in listView.Columns)
                    {
                        if (columnHeader.Tag != null)
                        {
                            resources.ApplyResources(columnHeader, columnHeader.Tag.ToString(), newCulture);
                        }
                        else
                        {
                            foreach (Control childControl in listView.Controls)
                            {
                                ChangeLanguageControls(childControl, resources, newCulture);
                            }
                        }
                    }

                    break;
                }
            }

            resources.ApplyResources(c, c.Name, newCulture);
            c.Bounds = currentBounds;
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
        private static void ChangeLanguageToolStripItems(ToolStripItem item, ComponentResourceManager resources, CultureInfo newCulture)
        {
            if (item == null)
            {
                return;
            }

            if (item is ToolStripDropDownItem downItem)
            {
                foreach (ToolStripItem childItem in downItem.DropDownItems)
                {
                    ChangeLanguageToolStripItems(childItem, resources, newCulture);
                }
            }

            resources.ApplyResources(item, item.Name, newCulture);
        }

        private static ListViewItem CreateCategoryListViewItem(Category category)
        {
            return new ListViewItem(CategoryListViewItemText(category))
            {
                Tag = category,
                Name = category.Name
            };
        }

        /// <summary>
        ///     Launches the selected game.
        ///     <param name="gameInfo">
        ///         Game to launch.
        ///     </param>
        /// </summary>
        private static void LaunchGame(GameInfo gameInfo)
        {
            if (gameInfo == null)
            {
                return;
            }

            gameInfo.LastPlayed = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            Process.Start(gameInfo.Executable);
        }

        /// <summary>
        ///     Loads the database from disk. If the load fails, displays a message box and creates an empty DB.
        /// </summary>
        private static void LoadDatabase()
        {
            try
            {
                if (File.Exists(Locations.File.Database))
                {
                    Database.Load(Locations.File.Database);
                }
                else
                {
                    MessageBox.Show(GlobalStrings.MainForm_ErrorLoadingGameDB + GlobalStrings.MainForm_GameDBFileNotExist);
                    Logger.Warn(GlobalStrings.MainForm_GameDBFileNotExist);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(GlobalStrings.MainForm_ErrorLoadingGameDB + ex.Message);
                Logger.Exception(GlobalStrings.MainForm_Log_ExceptionOnDBLoad, ex);
                Database.Reset();
            }
        }

        private static void ResortToolStripItemCollection(ToolStripItemCollection toolStripItemCollection)
        {
            ArrayList toolStripItems = new ArrayList(toolStripItemCollection);
            toolStripItems.Sort(new ToolStripItemComparer());
            toolStripItemCollection.Clear();

            foreach (ToolStripItem toolStripItem in toolStripItems)
            {
                toolStripItemCollection.Add(toolStripItem);
            }
        }

        private static void SetDragDropEffect(DragEventArgs e)
        {
            if (Settings.SingleCatMode /*|| (e.KeyState & 4) == 4*/)
            {
                e.Effect = DragDropEffects.Move;
            }
            else if ((e.KeyState & 8) == 8)
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        /// <summary>
        ///     Checks to see if a category name is valid. Does not make sure it isn't already in use. If the name is not valid,
        ///     displays a warning.
        /// </summary>
        /// <param name="name">Name to check</param>
        /// <returns>True if valid, false otherwise</returns>
        private static bool ValidateCategoryName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return true;
            }

            MessageBox.Show(GlobalStrings.MainForm_CategoryNamesNotEmpty, Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }

        private static bool ValidateFilterName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return true;
            }

            MessageBox.Show(GlobalStrings.MainForm_FilterNamesNotEmpty, Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }

        /// <summary>
        ///     Adds the given category to all selected games.
        /// </summary>
        /// <param name="cat">Category to add</param>
        /// <param name="forceClearOthers">If true, remove other categories from the affected games.</param>
        private void AddCategoryToSelectedGames(Category cat, bool forceClearOthers)
        {
            if (lstGames.SelectedObjects.Count <= 0)
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            foreach (GameInfo gameInfo in _typedListGames.SelectedObjects)
            {
                if (gameInfo == null)
                {
                    continue;
                }

                if (forceClearOthers || Settings.SingleCatMode)
                {
                    gameInfo.ClearCategories();
                    if (cat != null)
                    {
                        gameInfo.AddCategory(cat);
                    }
                }
                else
                {
                    gameInfo.AddCategory(cat);
                }
            }

            FillAllCategoryLists();
            if (forceClearOthers)
            {
                FilterGameList(false);
            }

            if (lstCategories.SelectedItems[0].Tag.ToString() == Resources.SpecialCategoryUncategorized)
            {
                FilterGameList(false);
            }
            else
            {
                RebuildGameList();
            }

            MakeChange(true);
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        ///     Adds a new game. Displays the game dialog to the user.
        /// </summary>
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
            }

            FullListRefresh();
            MakeChange(true);
            AddStatus(GlobalStrings.MainForm_AddedGame);
            Cursor.Current = Cursors.Default;
        }

        private void AddGameToMultiCatCheckStates(GameInfo game, bool first)
        {
            foreach (ListViewItem catItem in lstMultiCat.Items)
            {
                if (catItem.StateImageIndex == 2)
                {
                    continue;
                }

                if (!(catItem.Tag is Category category))
                {
                    continue;
                }

                if (first)
                {
                    catItem.StateImageIndex = game.ContainsCategory(category) ? 1 : 0;
                }
                else
                {
                    if (game.ContainsCategory(category))
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
            if (filter == null || !AdvancedCategoryFilter)
            {
                return;
            }

            // reset Advanced settings
            _advFilter = new Filter(ADVANCED_FILTER);

            // load new Advanced settings
            foreach (ListViewItem item in lstCategories.Items)
            {
                string filterName = item.Tag.ToString();
                if (filterName.Equals(Resources.SpecialCategoryGames))
                {
                    item.StateImageIndex = filter.Game;
                    _advFilter.Game = filter.Game;
                }
                else if (filterName.Equals(Resources.SpecialCategorySoftware))
                {
                    item.StateImageIndex = filter.Software;
                    _advFilter.Software = filter.Software;
                }
                else if (filterName.Equals(Resources.SpecialCategoryUncategorized))
                {
                    item.StateImageIndex = filter.Uncategorized;
                    _advFilter.Uncategorized = filter.Uncategorized;
                }
                else if (filterName.Equals(Resources.SpecialCategoryHidden))
                {
                    item.StateImageIndex = filter.Hidden;
                    _advFilter.Hidden = filter.Hidden;
                }
                else if (filterName.Equals(Resources.SpecialCategoryVR))
                {
                    item.StateImageIndex = filter.VR;
                    _advFilter.VR = filter.VR;
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
                        _advFilter.Allow.Add(category);
                    }
                    else if (filter.Require.Contains(category))
                    {
                        item.StateImageIndex = (int) AdvancedFilterState.Require;
                        _advFilter.Require.Add(category);
                    }
                    else if (filter.Exclude.Contains(category))
                    {
                        item.StateImageIndex = (int) AdvancedFilterState.Exclude;
                        _advFilter.Exclude.Add(category);
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
                foreach (GameInfo g in _typedListGames.SelectedObjects)
                {
                    g.SetFavorite(fav);
                }

                FillCategoryList();
                RebuildGameList();
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
                foreach (GameInfo g in _typedListGames.SelectedObjects)
                {
                    g.SetHidden(hidden);
                }

                FillCategoryList();
                FilterGameList(false);
                MakeChange(true);
                Cursor.Current = Cursors.Default;
            }
        }

        private void AutoCategorize(bool selectedOnly, AutoCat autoCat, bool scrape = true, bool refresh = true)
        {
            if (autoCat == null)
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            // Get a list of games to update
            List<GameInfo> gamesToUpdate = new List<GameInfo>();

            if (selectedOnly && autoCat.Filter == null)
            {
                gamesToUpdate.AddRange(_typedListGames.SelectedObjects.Where(g => g.Id > 0));
            }
            else if (_typedListGames.Objects.Count > 0 && autoCat.Filter == null)
            {
                gamesToUpdate.AddRange(_typedListGames.Objects.Where(g => g.Id > 0));
            }
            else
            {
                foreach (GameInfo g in CurrentProfile.GameData.Games.Values)
                {
                    if (g != null && g.Id > 0)
                    {
                        gamesToUpdate.Add(g);
                    }
                }
            }

            int updated = 0;

            // List of games not found in database or that have old data, so we can try to scrape data for them
            List<int> appIds = new List<int>();
            int oldDbDataCount = 0;
            int notInDbCount = 0;
            foreach (GameInfo game in gamesToUpdate)
            {
                if (game.Id > 0 && (!Database.Contains(game.Id, out DatabaseEntry entry) || entry.LastStoreScrape == 0))
                {
                    appIds.Add(game.Id);
                    notInDbCount++;
                }
                else if (Database.Contains(game.Id, out DatabaseEntry entry2) && game.Id > 0 && DateTimeOffset.UtcNow.ToUnixTimeSeconds() > entry2.LastStoreScrape + Settings.ScrapePromptDays * 86400) //86400 seconds in a day
                {
                    appIds.Add(game.Id);
                    oldDbDataCount++;
                }
            }

            if ((notInDbCount > 0 || oldDbDataCount > 0) && scrape)
            {
                Cursor.Current = Cursors.Default;
                string message = "";
                message += notInDbCount > 0 ? string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_GamesNotFoundInGameDB, notInDbCount) : "";
                if (notInDbCount > 0 && oldDbDataCount > 0)
                {
                    message += " " + GlobalStrings.Text_And + " ";
                }

                if (oldDbDataCount > 0)
                {
                    message += string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_GamesHaveOldDataInGameDB, oldDbDataCount, Settings.ScrapePromptDays);
                }

                message += string.Format(CultureInfo.CurrentCulture, ". {0}", GlobalStrings.MainForm_ScrapeNow);
                if (MessageBox.Show(message, GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    using (DbScrapeDlg dialog = new DbScrapeDlg(appIds))
                    {
                        DialogResult result = dialog.ShowDialog();

                        if (result == DialogResult.Cancel)
                        {
                            AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_CanceledDatabaseUpdate));
                        }
                        else
                        {
                            AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_UpdatedDatabaseEntries, dialog.JobsCompleted));
                            if (dialog.JobsCompleted > 0 && Settings.AutoSaveDatabase)
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
                FilterGameList(true);
            }

            Cursor.Current = Cursors.Default;
        }

        private int AutoCatGameCount()
        {
            // Get a count of games to update
            int count = 0;

            if (mchkAutoCatSelected.Checked)
            {
                count += _typedListGames.SelectedObjects.Count(g => g.Id > 0);
            }
            else if (_typedListGames.Objects.Count > 0)
            {
                count += _typedListGames.Objects.Count(g => g.Id > 0);
            }
            else
            {
                if (CurrentProfile == null)
                {
                    return count;
                }

                foreach (GameInfo g in CurrentProfile.GameData.Games.Values)
                {
                    if (g != null && g.Id > 0)
                    {
                        count += 1;
                    }
                }
            }

            return count;
        }

        private void autoModeHelperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (DlgAutomaticModeHelper dialog = new DlgAutomaticModeHelper(CurrentProfile))
            {
                dialog.ShowDialog();
            }
        }

        /// <summary>
        ///     Renames all games with names from the database.
        /// </summary>
        private void AutoNameAll()
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

            RebuildGameList();

            Cursor.Current = Cursors.Default;
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFilter.SelectedItem != null && AdvancedCategoryFilter)
            {
                ApplyFilter((Filter) cboFilter.SelectedItem);
            }
        }

        /// <summary>
        ///     If there are any unsaved changes, asks the user if they want to save. Also gives the user the option to cancel the
        ///     calling action.
        /// </summary>
        /// <returns>True if the action should proceed, false otherwise.</returns>
        private bool CheckForUnsaved()
        {
            if (!ProfileLoaded || !_unsavedChanges)
            {
                return true;
            }

            using (DlgClose dialog = new DlgClose(GlobalStrings.MainForm_UnsavedChangesWillBeLost, GlobalStrings.MainForm_UnsavedChanges, SystemIcons.Warning.ToBitmap(), true, CurrentProfile.AutoExport))
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

        private void chkRegex_CheckedChanged(object sender, EventArgs e)
        {
            FilterGameList(false);
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
                LaunchGame(_typedListGames.SelectedObjects[0]);
            }

            FlushStatus();
        }

        private void cmdGameRemove_Click(object sender, EventArgs e)
        {
            ClearStatus();
            RemoveSelectedApps();
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
            bool selectedCat = lstCategories.SelectedItems.Count > 0 && lstCategories.SelectedItems[0].Tag != null;
            contextCat_Delete.Enabled = contextCat_Rename.Enabled = selectedCat;
        }

        private void contextCat_SetAdvanced_Allow_Click(object sender, EventArgs e)
        {
            if (lstCategories.SelectedItems.Count <= 0)
            {
                return;
            }

            foreach (ListViewItem listViewItem in lstCategories.SelectedItems)
            {
                SetItemState(listViewItem, (int) AdvancedFilterState.Allow);
            }

            OnViewChange();
        }

        private void contextCat_SetAdvanced_Exclude_Click(object sender, EventArgs e)
        {
            if (lstCategories.SelectedItems.Count <= 0)
            {
                return;
            }

            foreach (ListViewItem listViewItem in lstCategories.SelectedItems)
            {
                SetItemState(listViewItem, (int) AdvancedFilterState.Exclude);
            }

            OnViewChange();
        }

        private void contextCat_SetAdvanced_None_Click(object sender, EventArgs e)
        {
            if (lstCategories.SelectedItems.Count <= 0)
            {
                return;
            }

            foreach (ListViewItem listViewItem in lstCategories.SelectedItems)
            {
                SetItemState(listViewItem, (int) AdvancedFilterState.None);
            }

            OnViewChange();
        }

        private void contextCat_SetAdvanced_Require_Click(object sender, EventArgs e)
        {
            if (lstCategories.SelectedItems.Count <= 0)
            {
                return;
            }

            foreach (ListViewItem listViewItem in lstCategories.SelectedItems)
            {
                SetItemState(listViewItem, (int) AdvancedFilterState.Require);
            }

            OnViewChange();
        }

        private void contextGame_Opening(object sender, CancelEventArgs e)
        {
            bool selectedGames = lstGames.SelectedObjects.Count > 0;
            contextGame_Edit.Enabled = selectedGames;
            contextGame_Remove.Enabled = selectedGames;
            contextGame_AddCat.Enabled = selectedGames;
            contextGame_RemCat.Enabled = selectedGames && contextGameRemCat.Items.Count > 0;
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
                Utility.LaunchStorePage(_typedListGames.SelectedObjects[0].Id);
            }
        }

        private void contextGameAddCat_Category_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripItem toolStripItem))
            {
                return;
            }

            ClearStatus();
            Category category = toolStripItem.Tag as Category;
            AddCategoryToSelectedGames(category, false);
            FlushStatus();
        }

        private void contextGameAddCat_Create_Click(object sender, EventArgs e)
        {
            Category category = CreateCategory();
            if (category == null)
            {
                return;
            }

            ClearStatus();
            AddCategoryToSelectedGames(category, false);
            FlushStatus();
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
            if (!(sender is ToolStripItem menuItem))
            {
                return;
            }

            ClearStatus();
            Category category = menuItem.Tag as Category;
            RemoveCategoryFromSelectedGames(category);
            FlushStatus();
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

            using (GetStringDlg dialog = new GetStringDlg(string.Empty, GlobalStrings.MainForm_CreateCategory, GlobalStrings.MainForm_EnterNewCategoryName, GlobalStrings.MainForm_Create))
            {
                DialogResult result = dialog.ShowDialog();
                if (result != DialogResult.OK || !ValidateCategoryName(dialog.Value))
                {
                    return null;
                }

                Category newCat = CurrentProfile.GameData.AddCategory(dialog.Value);
                if (newCat != null)
                {
                    FillAllCategoryLists();
                    MakeChange(true);
                    AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_CategoryAdded, newCat.Name));
                    return newCat;
                }

                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_CouldNotAddCategory, dialog.Value), Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return null;
        }

        /// <summary>
        ///     Prompts user to create a new profile.
        /// </summary>
        private void CreateProfile()
        {
            using (DlgProfile dialog = new DlgProfile())
            {
                DialogResult result = dialog.ShowDialog();
                if (result != DialogResult.OK)
                {
                    OnProfileChange();
                    return;
                }

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
                    Settings.StartupAction = StartupAction.Load;
                    Settings.ProfileToLoad = CurrentProfile.FilePath;
                    Settings.Save();
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
                if (item.Tag is Category c && c != CurrentProfile.GameData.FavoriteCategory)
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
                    RebuildGameList();
                    MakeChange(true);
                    AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_CategoryDeleted, deleted));
                }
                else
                {
                    MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_CouldNotDeleteCategory), Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void DeleteFilter(Filter filter)
        {
            if (filter == null)
            {
                Logger.Info("MainForm:DeleteFilter | Tried to delete a filter but given object was null.");
                return;
            }

            if (!ProfileLoaded)
            {
                Logger.Info("MainForm:DeleteFilter | Tried to delete filter '{0}', but there is no profile loaded.", filter.Name);
                return;
            }

            if (!AdvancedCategoryFilter)
            {
                Logger.Info("MainForm:DeleteFilter | Tried to delete filter '{0}', but AdvancedCategoryFilter is not enabled.", filter.Name);
                return;
            }

            DialogResult result = MessageBox.Show(string.Format(CultureInfo.CurrentCulture, Resources.ConfirmDeleteFilter, filter.Name), Resources.Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
            {
                Logger.Info("MainForm:DeleteFilter | User canceled the deletion of filter '{0}'.", filter.Name);
                return;
            }

            try
            {
                if (!CurrentProfile.GameData.Filters.Remove(filter))
                {
                    throw new InvalidOperationException();
                }

                AddStatus(string.Format(CultureInfo.CurrentCulture, Resources.DeletedFilter, filter.Name));
            }
            catch (Exception e)
            {
                Logger.Warn("MainForm:DeleteFilter | Tried to delete filter '{0}', but an exception was thrown: {1}.", filter.Name, e);
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, Resources.FailedDeletingFilter, filter.Name), Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            RefreshFilters();
        }

        /// <summary>
        ///     Creates an Edit AutoCats dialog for the user
        /// </summary>
        private void EditAutoCats(AutoCat autoCat)
        {
            if (!ProfileLoaded)
            {
                return;
            }

            using (DlgAutoCat dialog = new DlgAutoCat(CurrentProfile.AutoCats, CurrentProfile.GameData, autoCat, CurrentProfile.FilePath))
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

        /// <summary>
        ///     Edits the first selected game. Displays game dialog.
        /// </summary>
        private void EditGame()
        {
            if (lstGames.SelectedObjects.Count <= 0)
            {
                return;
            }

            GameInfo gameInfo = _typedListGames.SelectedObjects[0];
            using (DlgGame dialog = new DlgGame(CurrentProfile.GameData, gameInfo))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            Cursor.Current = Cursors.WaitCursor;

            FilterGameList(false);
            MakeChange(true);
            AddStatus(GlobalStrings.MainForm_EditedGame);

            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        ///     Prompts the user to modify the currently loaded profile. If there isn't one, asks if the user would like to create
        ///     one.
        /// </summary>
        private void EditProfile()
        {
            if (ProfileLoaded)
            {
                using (DlgProfile dialog = new DlgProfile(CurrentProfile))
                {
                    DialogResult result = dialog.ShowDialog();
                    if (result != DialogResult.OK)
                    {
                        return;
                    }

                    AddStatus(GlobalStrings.MainForm_ProfileEdited);
                    MakeChange(true);
                    Cursor = Cursors.WaitCursor;
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
                        Settings.StartupAction = StartupAction.Load;
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
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Msg_ErrorExportingToSteam, e.Message), Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Exception(GlobalStrings.MainForm_Log_ExceptionExport, e);
                AddStatus(GlobalStrings.MainForm_ExportFailed);
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
            foreach (Category category in CurrentProfile.GameData.Categories)
            {
                if (category == CurrentProfile.GameData.FavoriteCategory)
                {
                    continue;
                }

                ToolStripItem item = contextGame_AddCat.DropDownItems.Add(category.Name);
                item.Tag = category;
                item.Click += contextGameAddCat_Category_Click;

                ListViewItem listItem = new ListViewItem(category.Name)
                {
                    Tag = category,
                    StateImageIndex = 0
                };
                lstMultiCat.Items.Add(listItem);
            }

            UpdateGameCheckStates();
            lstMultiCat.EndUpdate();
            mlblCategoryCount.Font = new Font("Arial", 8);
            mlblCategoryCount.Text = string.Format(CultureInfo.CurrentCulture, "{0} {1}", lstCategories.Items.Count, Resources.Categories);

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
                    ListViewItem listItem = new ListViewItem(autoCat.DisplayName)
                    {
                        Tag = autoCat,
                        Name = autoCat.Name,
                        Checked = autoCat.Selected
                    };
                    lvAutoCatType.Items.Add(listItem);

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

        /// <summary>
        ///     Completely repopulates the category list. Maintains selection.
        /// </summary>
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

            //calculate number of hidden, VR and uncategorized games
            int all = 0;
            int hidden = 0;
            int uncategorized = 0;
            int vr = 0;
            int software = 0;
            int games = 0;
            foreach (GameInfo g in CurrentProfile.GameData.Games.Values)
            {
                if (g.Id < 0 && !CurrentProfile.IncludeShortcuts)
                {
                    continue;
                }

                if (g.IsHidden)
                {
                    hidden++;
                    continue;
                }

                all++;

                if (!g.HasCategories())
                {
                    uncategorized++;
                }

                if (g.Id <= 0 || !Database.Contains(g.Id, out DatabaseEntry entry))
                {
                    continue;
                }

                if (Database.SupportsVR(g.Id))
                {
                    vr++;
                }

                switch (entry.AppType)
                {
                    case AppType.Game:
                        games++;
                        break;
                    case AppType.Application:
                        software++;
                        break;
                }
            }

            ListViewItem listViewItem;
            if (!AdvancedCategoryFilter)
            {
                listViewItem = new ListViewItem(CategoryListViewItemText(Resources.SpecialCategoryAll, all))
                {
                    Tag = Resources.SpecialCategoryAll,
                    Name = Resources.SpecialCategoryAll
                };
                lstCategories.Items.Add(listViewItem);
            }

            // <Games>
            listViewItem = new ListViewItem(CategoryListViewItemText(Resources.SpecialCategoryGames, games))
            {
                Tag = Resources.SpecialCategoryGames,
                Name = Resources.SpecialCategoryGames
            };
            lstCategories.Items.Add(listViewItem);

            // <Software>
            listViewItem = new ListViewItem(CategoryListViewItemText(Resources.SpecialCategorySoftware, software))
            {
                Tag = Resources.SpecialCategorySoftware,
                Name = Resources.SpecialCategorySoftware
            };
            lstCategories.Items.Add(listViewItem);

            // <Uncategorized>
            listViewItem = new ListViewItem(CategoryListViewItemText(Resources.SpecialCategoryUncategorized, uncategorized))
            {
                Tag = Resources.SpecialCategoryUncategorized,
                Name = Resources.SpecialCategoryUncategorized
            };

            lstCategories.Items.Add(listViewItem);

            // <Hidden>
            listViewItem = new ListViewItem(CategoryListViewItemText(Resources.SpecialCategoryHidden, hidden))
            {
                Tag = Resources.SpecialCategoryHidden,
                Name = Resources.SpecialCategoryHidden
            };

            lstCategories.Items.Add(listViewItem);

            // <VR>
            listViewItem = new ListViewItem(CategoryListViewItemText(Resources.SpecialCategoryVR, vr))
            {
                Tag = Resources.SpecialCategoryVR,
                Name = Resources.SpecialCategoryVR
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

            //if (sort)
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
            List<GameInfo> gameInfos = new List<GameInfo>();
            Cursor = Cursors.WaitCursor;
            if (CurrentProfile != null)
            {
                foreach (GameInfo g in CurrentProfile.GameData.Games.Values)
                {
                    if (g.Id < 0 && !CurrentProfile.IncludeShortcuts)
                    {
                        continue;
                    }

                    gameInfos.Add(g);
                    if (g.Name != null)
                    {
                        continue;
                    }

                    g.Name = string.Empty;
                    gameInfos.Add(g);
                }
            }

            Steam.GrabBanners(gameInfos.Select(game => game.Id));

            lstGames.SetObjects(gameInfos);
            lstGames.BuildList();

            mbtnAutoCategorize.Text = string.Format(CultureInfo.CurrentCulture, Resources.AutoCategorizeNumApps, AutoCatGameCount());

            Cursor = Cursors.Default;
        }

        /// <summary>
        ///     Filters game list based on based on the current category selection and advanced filters
        /// </summary>
        /// <param name="preserveSelection">If true, will try to preserve game selection</param>
        private void FilterGameList(bool preserveSelection)
        {
            if (chkRegex.CheckState == CheckState.Checked)
            {
                try
                {
                    _currentFilterRegex = new Regex(mtxtSearch.Text, RegexOptions.IgnoreCase);

                    // Resets the search box color if a valid Regex is provided.
                    mtxtSearch.BackColor = Color.Empty;
                }
                catch
                {
                    // We are creating the Regex as it is typed, the exception thrown is because the Regex is invalid.
                    // Change color to of the search box to indicate that the current Regex is invalid.
                    mtxtSearch.BackColor = InvalidSearchRegexColor;
                }
            }
            else
            {
                _currentFilterRegex = null;
                mtxtSearch.BackColor = Color.Empty;
            }

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

            int version;
            using (WebBrowser browser = new WebBrowser())
            {
                version = browser.Version.Major;
            }

            int value;
            if (version >= 8 && version <= 11)
            {
                value = version * 1000;
            }
            else
            {
                return;
            }

            RegistryKey existingSubKey = Registry.LocalMachine.OpenSubKey(installKey, false);

            if (existingSubKey?.GetValue(entryLabel) != null && Convert.ToInt32(existingSubKey.GetValue(entryLabel)) == value)
            {
                return;
            }

            new RegistryPermission(PermissionState.Unrestricted).Assert();
            try
            {
                existingSubKey = Registry.LocalMachine.OpenSubKey(installKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
                existingSubKey?.SetValue(entryLabel, value, RegistryValueKind.DWord);
            }
            catch
            {
                MessageBox.Show(GlobalStrings.MainForm_AdminRights, Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.X = Left;
            Settings.Y = Top;
            Settings.Height = Height;
            Settings.Width = Width;
            Settings.SplitContainer = splitContainer.SplitterDistance;
            Settings.SplitGame = splitGame.SplitterDistance;
            Settings.SplitBrowser = splitBrowser.SplitterDistance;

            Settings.Filter = AdvancedCategoryFilter ? cboFilter.Text : string.Empty;

            if (lstCategories.SelectedItems.Count > 0)
            {
                Settings.Category = lstCategories.SelectedItems[0].Name;
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
            contextGame.MouseWheel += HandleMouseWheel;

            // Load saved forms settings
            Location = new Point(Settings.X, Settings.Y);
            if (!Utility.IsOnScreen(this))
            {
                Location = new Point(0, 0);
            }

            Size = new Size(Settings.Width, Settings.Height);
            splitContainer.SplitterDistance = Settings.SplitContainer;
            Settings.SplitGameContainerHeight = splitGame.Height;
            splitGame.SplitterDistance = Settings.SplitGame;
            Settings.SplitBrowserContainerWidth = splitBrowser.Width;
            splitBrowser.SplitterDistance = Settings.SplitBrowser;

            ttHelp.Ext_SetToolTip(mchkAdvancedCategories, GlobalStrings.MainForm_Help_AdvancedCategories);

            InitializeObjectListView();

            LoadDatabase();

            ClearStatus();
            if (Settings.SteamPath == null)
            {
                using (DlgSteamPath dialog = new DlgSteamPath())
                {
                    dialog.ShowDialog();
                    Settings.SteamPath = dialog.Path;
                }

                Settings.Save();
            }

            if (Settings.UpdateAppInfoOnStart)
            {
                UpdateDatabaseFromAppInfo();
            }

            const int aWeekInSecs = 7 * 24 * 60 * 60;
            if (Settings.UpdateHltbOnStart && DateTimeOffset.UtcNow.ToUnixTimeSeconds() > Database.LastHLTBUpdate + aWeekInSecs)
            {
                UpdateDatabaseFromHLTB();
            }

            switch (Settings.StartupAction)
            {
                case StartupAction.Load:
                    LoadProfile(Settings.ProfileToLoad, false);
                    break;
                case StartupAction.Create:
                    CreateProfile();
                    break;
                default:
                    OnProfileChange();
                    break;
            }

            Database.ChangeLanguage(Settings.StoreLanguage);

            UpdateInterfaceForSingleCategory();
            UpdateEnabledStatesForGames();
            UpdateEnabledStatesForCategories();

            FlushStatus();

            if (CurrentProfile == null)
            {
                return;
            }

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

            if (i.StateImageIndex == -1 && reverse)
            {
                i.StateImageIndex = MAX_FILTER_STATE;
            }
            else if (i.StateImageIndex == MAX_FILTER_STATE && !reverse)
            {
                i.StateImageIndex = -1;
            }
            else
            {
                i.StateImageIndex += reverse ? -1 : 1;
            }

            if (i.Tag.ToString() == Resources.SpecialCategoryGames)
            {
                _advFilter.Game = i.StateImageIndex;
            }
            else if (i.Tag.ToString() == Resources.SpecialCategorySoftware)
            {
                _advFilter.Software = i.StateImageIndex;
            }
            else if (i.Tag.ToString() == Resources.SpecialCategoryUncategorized)
            {
                _advFilter.Uncategorized = i.StateImageIndex;
            }
            else if (i.Tag.ToString() == Resources.SpecialCategoryHidden)
            {
                _advFilter.Hidden = i.StateImageIndex;
            }
            else if (i.Tag.ToString() == Resources.SpecialCategoryVR)
            {
                _advFilter.VR = i.StateImageIndex;
            }
            else
            {
                if (!(i.Tag is Category category))
                {
                    return;
                }

                switch (oldState)
                {
                    case (int) AdvancedFilterState.None:
                        // ignored
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
                        // ignored
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

            if (updateView)
            {
                OnViewChange();
            }
        }

        private void HandleMouseWheel(object sender, MouseEventArgs e)
        {
            if (contextGame.IsDropDown)
            {
                SendKeys.SendWait(e.Delta > 0 ? BIG_UP : BIG_DOWN);
            }
        }

        private void HandleMultiCatItemActivation(ListViewItem listViewItem, bool modKey)
        {
            if (listViewItem == null)
            {
                return;
            }

            if (listViewItem.StateImageIndex == 0 || listViewItem.StateImageIndex == 2 && modKey)
            {
                listViewItem.StateImageIndex = 1;
                if (listViewItem.Tag is Category category)
                {
                    AddCategoryToSelectedGames(category, false);
                }
            }
            else if (listViewItem.StateImageIndex == 1 || listViewItem.StateImageIndex == 2 && !modKey)
            {
                listViewItem.StateImageIndex = 0;
                if (listViewItem.Tag is Category category)
                {
                    RemoveCategoryFromSelectedGames(category);
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
                AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_ImportedItems, count));
                if (count > 0)
                {
                    MakeChange(true);
                    FullListRefresh();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_ErrorImportingSteamDataList, e.Message), Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Logger.Exception("Exception encountered while importing the remote config file.", e);
                AddStatus(GlobalStrings.MainForm_ImportFailed);
            }

            Cursor = Cursors.Default;
        }

        private void InitializeLstGames()
        {
            _typedListGames = new TypedObjectListView<GameInfo>(lstGames);
            _typedListGames.GenerateAspectGetters();

            colGameID.AspectToStringConverter = delegate { return string.Empty; };
            colCategories.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return string.Empty;
                }

                return gameInfo.GetCatString(Resources.SpecialCategoryUncategorized);
            };
            colFavorite.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return string.Empty;
                }

                return gameInfo.IsFavorite() ? "X" : string.Empty;
            };
            colHidden.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return string.Empty;
                }

                return gameInfo.IsHidden ? "X" : string.Empty;
            };
            colGenres.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return Resources.SpecialCategoryNoGenres;
                }

                if (Database.Contains(gameInfo.Id, out DatabaseEntry entry) && entry.Genres.Count > 0)
                {
                    return string.Join(", ", entry.Genres);
                }

                return Resources.SpecialCategoryNoGenres;
            };
            colFlags.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return Resources.SpecialCategoryNoFlags;
                }

                if (Database.Contains(gameInfo.Id, out DatabaseEntry entry) && entry.Flags.Count > 0)
                {
                    return string.Join(", ", entry.Flags);
                }

                return Resources.SpecialCategoryNoFlags;
            };
            colTags.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return Resources.SpecialCategoryNoTags;
                }

                if (Database.Contains(gameInfo.Id, out DatabaseEntry entry) && entry.Tags.Count > 0)
                {
                    return string.Join(", ", entry.Tags);
                }

                return Resources.SpecialCategoryNoTags;
            };
            colVRHeadsets.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return string.Empty;
                }

                if (Database.Contains(gameInfo.Id, out DatabaseEntry entry) && entry.VRSupport.Headsets.Count > 0)
                {
                    return string.Join(", ", entry.VRSupport.Headsets);
                }

                return string.Empty;
            };
            colVRInput.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return string.Empty;
                }

                if (Database.Contains(gameInfo.Id, out DatabaseEntry entry) && entry.VRSupport.Input.Count > 0)
                {
                    return string.Join(", ", entry.VRSupport.Input);
                }

                return string.Empty;
            };
            colVRPlayArea.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return string.Empty;
                }

                if (Database.Contains(gameInfo.Id, out DatabaseEntry entry) && entry.VRSupport.PlayArea.Count > 0)
                {
                    return string.Join(", ", entry.VRSupport.PlayArea);
                }

                return string.Empty;
            };
            colLanguageInterface.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return string.Empty;
                }

                if (Database.Contains(gameInfo.Id, out DatabaseEntry entry) && entry.LanguageSupport.Interface.Count > 0)
                {
                    return string.Join(", ", entry.LanguageSupport.Interface);
                }

                return string.Empty;
            };
            colLanguageSubtitles.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return string.Empty;
                }

                if (Database.Contains(gameInfo.Id, out DatabaseEntry entry) && entry.LanguageSupport.Subtitles.Count > 0)
                {
                    return string.Join(", ", entry.LanguageSupport.Subtitles);
                }

                return string.Empty;
            };
            colLanguageFullAudio.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return string.Empty;
                }

                if (Database.Contains(gameInfo.Id, out DatabaseEntry entry) && entry.LanguageSupport.FullAudio.Count > 0)
                {
                    return string.Join(", ", entry.LanguageSupport.FullAudio);
                }

                return string.Empty;
            };
            colYear.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return Resources.SpecialCategoryUnknown;
                }

                if (!Database.Contains(gameInfo.Id, out DatabaseEntry entry))
                {
                    return Resources.SpecialCategoryUnknown;
                }

                if (DateTime.TryParse(entry.SteamReleaseDate, Database.Culture, DateTimeStyles.None, out DateTime releaseDate))
                {
                    return releaseDate.Year.ToString(CultureInfo.CurrentCulture);
                }

                return Resources.SpecialCategoryUnknown;
            };
            colLastPlayed.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return DateTime.MinValue;
                }

                if (gameInfo.LastPlayed <= 0)
                {
                    return DateTime.MinValue;
                }

                return DateTimeOffset.FromUnixTimeSeconds(gameInfo.LastPlayed).Date;
            };
            colHoursPlayed.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return 0;
                }

                return gameInfo.HoursPlayed;
            };
            colAchievements.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return 0;
                }

                return Database.Contains(gameInfo.Id, out DatabaseEntry entry) ? entry.TotalAchievements : 0;
            };
            colPlatforms.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo) || Database.Contains(gameInfo.Id, out DatabaseEntry entry))
                {
                    return string.Empty;
                }

                AppPlatforms platforms = entry.Platforms;
                return (platforms & AppPlatforms.Linux) != 0 && platforms != AppPlatforms.All ? platforms + ", SteamOS" : platforms.ToString();
            };
            colDevelopers.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return Resources.SpecialCategoryUnknown;
                }

                if (Database.Contains(gameInfo.Id, out DatabaseEntry entry) && entry.Developers.Count > 0)
                {
                    return string.Join(", ", entry.Developers);
                }

                return Resources.SpecialCategoryUnknown;
            };
            colPublishers.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return Resources.SpecialCategoryUnknown;
                }

                if (Database.Contains(gameInfo.Id, out DatabaseEntry entry) && entry.Publishers.Count > 0)
                {
                    return string.Join(", ", entry.Publishers);
                }

                return Resources.SpecialCategoryUnknown;
            };
            colNumberOfReviews.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return 0;
                }

                return Database.Contains(gameInfo.Id, out DatabaseEntry entry) ? entry.ReviewTotal : 0;
            };
            colReviewScore.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return 0;
                }

                return Database.Contains(gameInfo.Id, out DatabaseEntry entry) ? entry.ReviewPositivePercentage : 0;
            };
            colReviewLabel.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return 0;
                }

                if (!Database.Contains(gameInfo.Id, out DatabaseEntry entry))
                {
                    return 0;
                }

                int reviewTotal = entry.ReviewTotal;
                int reviewPositivePercentage = entry.ReviewPositivePercentage;
                if (reviewTotal <= 0)
                {
                    return -1;
                }

                if (reviewPositivePercentage >= 95 && reviewTotal >= 500)
                {
                    return 9;
                }

                if (reviewPositivePercentage >= 85 && reviewTotal >= 50)
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
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return 0;
                }

                return Database.Contains(gameInfo.Id, out DatabaseEntry entry) ? entry.HltbMain : 0;
            };
            colHltbExtras.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return 0;
                }

                return Database.Contains(gameInfo.Id, out DatabaseEntry entry) ? entry.HltbExtras : 0;
            };
            colHltbCompletionist.AspectGetter = delegate(object g)
            {
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return 0;
                }

                return Database.Contains(gameInfo.Id, out DatabaseEntry entry) ? entry.HltbCompletionists : 0;
            };

            //Aspect to String Converters
            colNumberOfReviews.AspectToStringConverter = delegate(object obj)
            {
                int reviewTotal = (int) obj;
                return reviewTotal <= 0 ? "0" : reviewTotal.ToString(CultureInfo.CurrentCulture);
            };
            colReviewScore.AspectToStringConverter = delegate(object obj)
            {
                int reviewScore = (int) obj;
                return reviewScore <= 0 ? Resources.SpecialCategoryUnknown : reviewScore.ToString(CultureInfo.CurrentCulture) + '%';
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
                return reviewLabels.ContainsKey(index) ? reviewLabels[index] : Resources.SpecialCategoryUnknown;
            };
            AspectToStringConverterDelegate hltb = delegate(object obj)
            {
                int time = (int) obj;
                if (time <= 0)
                {
                    return "-";
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
                    return "-";
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
                return lastPlayed == DateTime.MinValue ? null : lastPlayed.ToString(format, CultureInfo.CurrentCulture);
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
                if (g == null || !(g is GameInfo gameInfo))
                {
                    return false;
                }

                return ShouldDisplayGame(gameInfo);
            });

            //Formating
            lstGames.RowFormatter = delegate(OLVListItem lvi)
            {
                if (lvi.RowObject != null && ((GameInfo) lvi.RowObject).Id < 0)
                {
                    lvi.Font = new Font(lvi.Font, lvi.Font.Style | FontStyle.Italic);
                }
            };

            lstGames.PrimarySortColumn = colTitle;
            lstGames.RestoreState(Convert.FromBase64String(Settings.LstGamesState));
        }

        private void InitializeObjectListView()
        {
            // Skin the Game List
            lstGames.HeaderFormatStyle = new HeaderFormatStyle();
            lstGames.HeaderFormatStyle.SetBackColor(PrimaryDarkColor);
            lstGames.HeaderFormatStyle.SetForeColor(HeaderFontColor);
            lstGames.HeaderFormatStyle.SetFont(new Font("Arial", 10, FontStyle.Bold));
            lstGames.HeaderFormatStyle.Hot.BackColor = PrimaryLightColor;
            lstGames.ForeColor = TextColor;
            lstGames.BackColor = FormColor;
            lstGames.SelectedForeColor = TextColor;
            lstGames.SelectedBackColor = AccentColor;
            lstGames.UnfocusedSelectedForeColor = TextColor;
            lstGames.UnfocusedSelectedBackColor = PrimaryColor;
            lstGames.Font = new Font("Arial", 10);
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

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.DefaultExt = "profile";
                dialog.AddExtension = true;
                dialog.CheckFileExists = true;
                dialog.Filter = GlobalStrings.DlgProfile_Filter;
                dialog.InitialDirectory = Path.GetDirectoryName(CurrentProfile == null ? Assembly.GetExecutingAssembly().CodeBase : CurrentProfile.FilePath);

                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    LoadProfile(dialog.FileName, false);
                }
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
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Msg_ErrorLoadingProfile, e.Message), Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            if (dropItem.Tag != null && dropItem.Tag is Category dropCat)
            {
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
                FilterGameList(false);
                MakeChange(true);
            }
            else if ((string) dropItem.Tag == Resources.SpecialCategoryUncategorized)
            {
                CurrentProfile.GameData.ClearGameCategories((int[]) e.Data.GetData(typeof(int[])), true);
                FillCategoryList();
                FilterGameList(false);
                MakeChange(true);
            }
            else if ((string) dropItem.Tag == Resources.SpecialCategoryHidden)
            {
                CurrentProfile.GameData.HideGames((int[]) e.Data.GetData(typeof(int[])), true);
                FillCategoryList();
                FilterGameList(false);
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
                    if (lstCategories.SelectedItems.Contains(i) && ModifierKeys != Keys.Control)
                    {
                        HandleAdvancedCategoryItemActivation(i, ModifierKeys == Keys.Shift);
                    }
                }
            }
        }

        private void lstCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isDragging)
            {
                return;
            }

            object nowSelected = null;
            if (lstCategories.SelectedItems.Count > 0)
            {
                ListViewItem selItem = lstCategories.SelectedItems[0];
                nowSelected = selItem.Tag ?? selItem.Text;
            }

            if (nowSelected != _lastSelectedCategory)
            {
                OnViewChange();
                _lastSelectedCategory = nowSelected;
            }

            UpdateEnabledStatesForCategories();
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
            string bannerFile = Locations.File.Banner(g.Id);
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
            if (Database.Contains(g.Id, out DatabaseEntry entry))
            {
                if (entry.Tags.Contains(EarlyAccessTag))
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

            TextDecoration td = new TextDecoration(g.Id.ToString(CultureInfo.CurrentCulture), ContentAlignment.BottomLeft)
            {
                Font = new Font(lstGames.Font.Name, 8),
                Wrap = false,
                TextColor = TextColor,
                BackColor = ListBackgroundColor,
                CornerRounding = 4,
                Transparency = 200
            };

            e.SubItem.Decorations.Add(td);
        }

        private void lstGames_FormatRow(object sender, FormatRowEventArgs e)
        {
            if (e.Model == null || !(e.Model is GameInfo g))
            {
                return;
            }

            if (g.IsFavorite())
            {
                e.Item.BackColor = ListBackgroundColor;
            }

            if (g.IsHidden)
            {
                e.Item.BackColor = PrimaryLightColor;
            }
        }

        private void lstGames_ItemDrag(object sender, ItemDragEventArgs e)
        {
            int[] selectedGames = new int[lstGames.SelectedObjects.Count];
            for (int i = 0; i < lstGames.SelectedObjects.Count; i++)
            {
                selectedGames[i] = _typedListGames.SelectedObjects[i].Id;
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
                    RemoveSelectedApps();
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
            contextGameFav_Yes.Checked = false;
            contextGameFav_No.Checked = false;
            contextGameHidden_Yes.Checked = false;
            contextGameHidden_No.Checked = false;

            string storeLanguage = Database.LanguageCode;

            if (lstGames.SelectedObjects.Count > 0)
            {
                GameInfo g = _typedListGames.SelectedObjects[0];

                if (_typedListGames.SelectedObjects.Count == 1 && g.IsFavorite())
                {
                    contextGameFav_Yes.Checked = true;
                }
                else if (_typedListGames.SelectedObjects.Count == 1)
                {
                    contextGameFav_No.Checked = true;
                }

                if (_typedListGames.SelectedObjects.Count == 1 && g.IsHidden)
                {
                    contextGameHidden_Yes.Checked = true;
                }
                else if (_typedListGames.SelectedObjects.Count == 1)
                {
                    contextGameHidden_No.Checked = true;
                }

                if (webBrowser1.Visible)
                {
                    webBrowser1.ScriptErrorsSuppressed = true;
                    webBrowser1.Navigate(string.Format(CultureInfo.InvariantCulture, Constants.SteamStoreApp + "?l=" + storeLanguage, g.Id));
                }
            }
            else if (webBrowser1.Visible)
            {
                try
                {
                    if (_typedListGames.Objects.Count > 0)
                    {
                        GameInfo g = _typedListGames.Objects[0];
                        webBrowser1.ScriptErrorsSuppressed = true;
                        webBrowser1.Navigate(string.Format(CultureInfo.InvariantCulture, Constants.SteamStoreApp + "?l=" + storeLanguage, g.Id));
                    }
                    else
                    {
                        webBrowser1.ScriptErrorsSuppressed = true;
                        webBrowser1.Navigate(Constants.SteamStore + "?l=" + storeLanguage);
                    }
                }
                catch
                {
                    // ignored
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
            mbtnAutoCategorize.Text = string.Format(CultureInfo.CurrentCulture, Resources.AutoCategorizeNumApps, AutoCatGameCount());
            Cursor.Current = Cursors.Default;
        }

        private void lstMultiCat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char) Keys.Return && e.KeyChar != (char) Keys.Space)
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
            if (lvAutoCatType.GetItemAt(e.X, e.Y) == null)
            {
                return;
            }

            if (e.Clicks > 1)
            {
                doubleClick = true;
            }
        }

        /// <summary>
        ///     Sets the unsaved changes flag to the given value and takes the requisite UI updating action
        /// </summary>
        /// <param name="changes"></param>
        private void MakeChange(bool changes)
        {
            _unsavedChanges = changes;
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

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }

                Cursor = Cursors.WaitCursor;
                try
                {
                    CurrentProfile.GameData.ExportSteamConfigFile(dialog.FileName, Settings.RemoveExtraEntries);
                    AddStatus(GlobalStrings.MainForm_DataExported);
                }
                catch (Exception e)
                {
                    MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Msg_ErrorManualExport, e.Message), Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (_typedListGames.SelectedObjects.Count == 0 && mchkAutoCatSelected.Checked)
                {
                    ClearStatus();
                    AddStatus(GlobalStrings.AutoCatSelected_NothingSelected);
                    FlushStatus();
                }
                else
                {
                    List<AutoCat> autoCats = new List<AutoCat>();
                    foreach (ListViewItem item in lvAutoCatType.CheckedItems)
                    {
                        AutoCat ac = (AutoCat) item.Tag;
                        autoCats.Add(ac);
                    }

                    //RunAutoCats(currentProfile.AutoCats);  WILL THIS WORK?  ARE AUTOCATS SELECTED VALUES SET CORRECTLY
                    RunAutoCats(autoCats, true);
                    RemoveEmptyCats();
                    FilterGameList(true);
                }
            }
        }

        private void mbtnCatAdd_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                ClearStatus();
                CreateCategory();
                FlushStatus();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void mbtnCatDelete_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                ClearStatus();
                DeleteCategory();
                FlushStatus();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
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

            try
            {
                ClearStatus();
                RenameCategory();
                FlushStatus();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
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
            if (!AdvancedCategoryFilter || cboFilter.SelectedItem == null)
            {
                return;
            }

            if (!(cboFilter.SelectedItem is Filter filter))
            {
                MessageBox.Show(Resources.ItemIsNotAFilter, Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DeleteFilter(filter);
        }

        private void mbtnFilterRename_Click(object sender, EventArgs e)
        {
            if (!AdvancedCategoryFilter || cboFilter.SelectedItem == null)
            {
                return;
            }

            if (!(cboFilter.SelectedItem is Filter filter))
            {
                MessageBox.Show(Resources.ItemIsNotAFilter, Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            RenameFilter(filter);
        }

        private void mbtnSaveFilter_Click(object sender, EventArgs e)
        {
            if (!AdvancedCategoryFilter)
            {
                return;
            }

            SaveFilter();
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
            mbtnAutoCategorize.Text = string.Format(CultureInfo.CurrentCulture, Resources.AutoCategorizeNumApps, AutoCatGameCount());
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
            using (DlgAbout dialog = new DlgAbout())
            {
                dialog.ShowDialog();
            }
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

            using (DlgClose dialog = new DlgClose(GlobalStrings.MainForm_SaveProfileConfirm, GlobalStrings.MainForm_SaveProfile, SystemIcons.Question.ToBitmap(), false, CurrentProfile.AutoExport))
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
            string sharedConfigPath = Path.GetDirectoryName(string.Format(CultureInfo.InvariantCulture, Constants.SharedConfig, Settings.SteamPath, Profile.ToSteam3Id(CurrentProfile.SteamID64)));
            using (DlgRestore dialog = new DlgRestore(sharedConfigPath))
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
            AutoNameAll();
            FlushStatus();
        }

        private void menu_Tools_DBEdit_Click(object sender, EventArgs e)
        {
            using (DBEditDlg dialog = new DBEditDlg(CurrentProfile?.GameData))
            {
                dialog.ShowDialog();
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
            using (DlgOptions dialog = new DlgOptions())
            {
                // jpodadera. Save culture of actual language
                CultureInfo actualCulture = Thread.CurrentThread.CurrentUICulture;

                dialog.ShowDialog();

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

                    ChangeLanguageControls(this, resources, Thread.CurrentThread.CurrentUICulture);

                    // jpodadera. Recover previous size
                    splitContainer.SplitterDistance = actualSplitDistanceMain;
                    splitGame.SplitterDistance = actualSplitDistanceSecondary;
                    splitCategories.SplitterDistance = actualSplitDistanceCategories;
                    splitBrowser.SplitterDistance = actualSplitDistanceBrowser;

                    FullListRefresh();
                }

                FlushStatus();
            }
        }

        private void menu_Tools_SingleCat_Click(object sender, EventArgs e)
        {
            Settings.SingleCatMode = !Settings.SingleCatMode;
            UpdateInterfaceForSingleCategory();
        }

        private void menuToolsAutocat_Item_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripItem item))
            {
                return;
            }

            if (!(item.Tag is AutoCat autoCat))
            {
                return;
            }

            ClearStatus();
            AutoCategorize(false, autoCat);
            FlushStatus();
        }

        private void mtxtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterGameList(false);
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
            FilterGameList(false);
        }

        /// <summary>
        ///     Rebuild all the list view items in the gameList, preserving as much state as is possible
        /// </summary>
        private void RebuildGameList()
        {
            lstGames.BuildList();
        }

        private void RefreshFilters()
        {
            if (CurrentProfile == null)
            {
                return;
            }

            cboFilter.DataSource = null;
            cboFilter.DataSource = CurrentProfile.GameData.Filters;
            cboFilter.ValueMember = null;
            cboFilter.DisplayMember = "Name";
            cboFilter.Text = "";
        }

        /// <summary>
        ///     Removes the given category from all selected games.
        /// </summary>
        /// <param name="category">Category to remove.</param>
        private void RemoveCategoryFromSelectedGames(Category category)
        {
            if (lstGames.SelectedObjects.Count <= 0)
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            foreach (GameInfo gameInfo in _typedListGames.SelectedObjects)
            {
                gameInfo.RemoveCategory(category);
            }

            FillAllCategoryLists();
            if (lstCategories.SelectedItems[0].Tag is Category selectedCategory && selectedCategory == category)
            {
                FilterGameList(false);
            }
            else
            {
                FilterGameList(true);
            }

            MakeChange(true);
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        ///     Removes any categories with no games assigned.
        /// </summary>
        private void RemoveEmptyCats()
        {
            int count = CurrentProfile.GameData.RemoveEmptyCategories();
            AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_RemovedEmptyCategories, count));
            FillAllCategoryLists();
        }

        private void RemoveSelectedApps()
        {
            int selectCount = lstGames.SelectedObjects.Count;
            if (selectCount <= 0)
            {
                return;
            }

            if (MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_RemoveGame, selectCount, selectCount == 1 ? "" : "s"), GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            int ignored = 0;
            int removed = 0;
            foreach (GameInfo gameInfo in _typedListGames.SelectedObjects)
            {
                gameInfo.ClearCategories(true);
                if (CurrentProfile.GameData.Games.Remove(gameInfo.Id))
                {
                    removed++;
                }

                if (!ProfileLoaded || !CurrentProfile.AutoIgnore)
                {
                    continue;
                }

                if (CurrentProfile.IgnoreList.Add(gameInfo.Id))
                {
                    ignored++;
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

            Logger.Info("MainForm:RemoveSelectedApps | Removed {0} app(s) and ignored {1} app(s).", removed, ignored);

            FillGameList();
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        ///     Renames the given category. Prompts user for a new name. Updates UI. Will display an error if the rename fails.
        /// </summary>
        private void RenameCategory()
        {
            if (lstCategories.SelectedItems.Count <= 0)
            {
                return;
            }

            if (!(lstCategories.SelectedItems[0].Tag is Category c) || c == CurrentProfile.GameData.FavoriteCategory)
            {
                return;
            }

            using (GetStringDlg dialog = new GetStringDlg(c.Name, string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_RenameCategory, c.Name), GlobalStrings.MainForm_EnterNewName, GlobalStrings.MainForm_Rename))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                string newName = dialog.Value;
                if (newName == c.Name)
                {
                    return;
                }

                if (ValidateCategoryName(newName))
                {
                    Category newCategory = CurrentProfile.GameData.RenameCategory(c, newName);
                    if (newCategory != null)
                    {
                        FillAllCategoryLists();
                        RebuildGameList();
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

                        AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_CategoryRenamed, c.Name));
                        return;
                    }
                }

                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_NameIsInUse, newName), Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RenameFilter(Filter filter)
        {
            if (filter == null)
            {
                Logger.Info("MainForm:RenameFilter | Tried to rename a filter but given object was null.");
                return;
            }

            if (!ProfileLoaded)
            {
                Logger.Info("MainForm:RenameFilter | Tried to rename filter '{0}', but there is no profile loaded.", filter.Name);
                return;
            }

            if (!AdvancedCategoryFilter)
            {
                Logger.Info("MainForm:RenameFilter | Tried to rename filter '{0}', but AdvancedCategoryFilter is not enabled.", filter.Name);
                return;
            }

            using (GetStringDlg dialog = new GetStringDlg(filter.Name, string.Format(CultureInfo.CurrentCulture, Resources.RenameFilter, filter.Name), Resources.EnterNewName, Resources.Rename))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    Logger.Info("MainForm:RenameFilter | User canceled the renaming of filter '{0}'.", filter.Name);
                    return;
                }

                if (filter.Name == dialog.Value)
                {
                    Logger.Info("MainForm:RenameFilter | Canceled the renaming of filter '{0}' because the new name is the same as the old name.", filter.Name);
                    return;
                }

                if (CurrentProfile.GameData.FilterExists(dialog.Value))
                {
                    Logger.Info("MainForm:RenameFilter | Canceled the renaming of filter '{0}' because the new name, '{1}', already exists.", filter.Name, dialog.Value);
                    MessageBox.Show(string.Format(CultureInfo.CurrentCulture, Resources.FilterAlreadyExists, dialog.Value), Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                filter.Name = dialog.Value;

                RefreshFilters();

                cboFilter.SelectedItem = filter;
                cboFilter.Text = filter.Name;
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

        private void RunAutoCats(IEnumerable<AutoCat> autoCats, bool first, bool group = false)
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
                    AutoCategorize(mchkAutoCatSelected.Checked, autoCat, first, false);
                    first = false;
                    FlushStatus();
                }
            }
        }

        /// <summary>
        ///     Saves the current database to disk. Displays a message box on failure.
        /// </summary>
        private void SaveDatabase()
        {
            try
            {
                Database.Save(Locations.File.Database);
                AddStatus(GlobalStrings.MainForm_Status_SavedDB);
            }
            catch (Exception e)
            {
                Logger.Exception(GlobalStrings.MainForm_Log_ExceptionAutosavingDB, e);
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Msg_ErrorAutosavingDB, e.Message), Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveFilter()
        {
            if (!ProfileLoaded || !AdvancedCategoryFilter)
            {
                return;
            }

            using (GetStringDlg dialog = new GetStringDlg(cboFilter.Text, GlobalStrings.MainForm_SaveFilter, GlobalStrings.MainForm_EnterNewFilterName, GlobalStrings.MainForm_Save))
            {
                if (dialog.ShowDialog() != DialogResult.OK || !ValidateFilterName(dialog.Value))
                {
                    return;
                }

                Filter f;
                bool refresh = true;
                if (CurrentProfile.GameData.FilterExists(dialog.Value))
                {
                    DialogResult res = MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_OverwriteFilterName, dialog.Value), GlobalStrings.MainForm_Overwrite, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (res == DialogResult.Yes)
                    {
                        f = CurrentProfile.GameData.GetFilter(dialog.Value);
                        refresh = false;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    f = CurrentProfile.GameData.AddFilter(dialog.Value);
                }

                if (f != null)
                {
                    f.Uncategorized = _advFilter.Uncategorized;
                    f.Hidden = _advFilter.Hidden;
                    f.VR = _advFilter.VR;
                    f.Allow = _advFilter.Allow;
                    f.Require = _advFilter.Require;
                    f.Exclude = _advFilter.Exclude;

                    if (!refresh)
                    {
                        return;
                    }

                    AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_FilterAdded, f.Name));
                    RefreshFilters();
                    cboFilter.SelectedItem = f;
                }
                else
                {
                    MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_CouldNotAddFilter, dialog.Value), Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Msg_ErrorSavingProfile, e.Message), Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.DefaultExt = "profile";
                dialog.AddExtension = true;
                dialog.CheckPathExists = true;
                dialog.Filter = GlobalStrings.DlgProfile_Filter;
                dialog.InitialDirectory = Path.GetDirectoryName(CurrentProfile.FilePath);

                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    SaveProfile(dialog.FileName);
                }
            }
        }

        private void SaveSelectedAutoCats()
        {
            string autoCats = string.Empty;
            for (int i = 0; i < lvAutoCatType.CheckedItems.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(autoCats))
                {
                    autoCats += lvAutoCatType.CheckedItems[i].Name;
                }
                else
                {
                    autoCats += "," + lvAutoCatType.CheckedItems[i].Name;
                }
            }

            Settings.AutoCats = autoCats;
        }

        private void SelectAutoCats()
        {
            if (Settings.AutoCats == null)
            {
                return;
            }

            List<string> autoCats = Settings.AutoCats.Split(',').ToList();
            foreach (string autoCat in autoCats)
            {
                foreach (ListViewItem listViewItem in lvAutoCatType.Items)
                {
                    if (listViewItem.Name != autoCat)
                    {
                        continue;
                    }

                    listViewItem.Checked = true;
                }
            }
        }

        private void SelectCategory()
        {
            if (string.IsNullOrWhiteSpace(Settings.Category))
            {
                return;
            }

            lstCategories.SelectedIndices.Clear();
            for (int i = 0; i < lstCategories.Items.Count; i++)
            {
                if (lstCategories.Items[i].Name != Settings.Category)
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

            if (string.IsNullOrWhiteSpace(Settings.Filter))
            {
                return;
            }

            for (int i = 0; i < cboFilter.Items.Count; i++)
            {
                string name = cboFilter.GetItemText(cboFilter.Items[i]);
                if (name != Settings.Filter)
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
                _advFilter = new Filter(ADVANCED_FILTER);
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

            if (i.Tag.ToString() == Resources.SpecialCategoryGames)
            {
                _advFilter.Game = state;
            }
            else if (i.Tag.ToString() == Resources.SpecialCategorySoftware)
            {
                _advFilter.Software = state;
            }
            else if (i.Tag.ToString() == Resources.SpecialCategoryUncategorized)
            {
                _advFilter.Uncategorized = state;
            }
            else if (i.Tag.ToString() == Resources.SpecialCategoryHidden)
            {
                _advFilter.Hidden = state;
            }
            else if (i.Tag.ToString() == Resources.SpecialCategoryVR)
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

        /// <summary>
        ///     Checks to see if a game should currently be displayed, based on the state of the category list.
        /// </summary>
        /// <param name="g">Game to check</param>
        /// <returns>True if it should be displayed, false otherwise</returns>
        private bool ShouldDisplayGame(GameInfo g)
        {
            if (CurrentProfile == null || g == null)
            {
                return false;
            }

            if (_currentFilterRegex != null)
            {
                if (!_currentFilterRegex.IsMatch(g.Name))
                {
                    return false;
                }
            }
            else if (!string.IsNullOrWhiteSpace(mtxtSearch.Text) && g.Name.IndexOf(mtxtSearch.Text, StringComparison.CurrentCultureIgnoreCase) == -1)
            {
                return false;
            }

            if (!CurrentProfile.GameData.Games.ContainsKey(g.Id))
            {
                return false;
            }

            if (g.Id < 0 && !CurrentProfile.IncludeShortcuts)
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

            if (g.IsHidden)
            {
                return lstCategories.SelectedItems[0].Tag.ToString() == Resources.SpecialCategoryHidden;
            }

            // <All>
            if (lstCategories.SelectedItems[0].Tag.ToString() == Resources.SpecialCategoryAll)
            {
                return true;
            }

            // <Games>
            if (lstCategories.SelectedItems[0].Tag.ToString() == Resources.SpecialCategoryGames)
            {
                return Database.Contains(g.Id, out DatabaseEntry entry) && entry.AppType == AppType.Game;
            }

            // <Software>
            if (lstCategories.SelectedItems[0].Tag.ToString() == Resources.SpecialCategorySoftware)
            {
                return Database.Contains(g.Id, out DatabaseEntry entry) && entry.AppType == AppType.Application;
            }

            // <Uncategorized>
            if (lstCategories.SelectedItems[0].Tag.ToString() == Resources.SpecialCategoryUncategorized)
            {
                return g.Categories.Count == 0;
            }

            // <VR>
            if (lstCategories.SelectedItems[0].Tag.ToString() == Resources.SpecialCategoryVR)
            {
                return Database.SupportsVR(g.Id);
            }

            if (!(lstCategories.SelectedItems[0].Tag is Category category))
            {
                return false;
            }

            // <Favorite>
            if (category.Name == Resources.SpecialCategoryFavorite)
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
            if (_typedListGames.SelectedObjects.Count == 0 && mchkAutoCatSelected.Checked)
            {
                ClearStatus();
                AddStatus(GlobalStrings.AutoCatSelected_NothingSelected);
                FlushStatus();
            }
            else
            {
                if (!mlblStatusMsg.Text.Contains(GlobalStrings.AutoCatSelected_NothingSelected))
                {
                    return;
                }

                ClearStatus();
                FlushStatus();
            }
        }

        /// <summary>
        ///     Updates the database using AppInfo cache. Displays an error message on failure. Saves the DB afterwards if
        ///     AutoSaveDatabase is set.
        /// </summary>
        private void UpdateDatabaseFromAppInfo()
        {
            try
            {
                int num = Database.UpdateFromAppInfo(string.Format(CultureInfo.InvariantCulture, Constants.AppInfo, Settings.SteamPath));
                AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Status_AppInfoAutoupdate, num));
                if (num > 0 && Settings.AutoSaveDatabase)
                {
                    SaveDatabase();
                }
            }
            catch (Exception e)
            {
                Logger.Exception(GlobalStrings.MainForm_Log_ExceptionAppInfo, e);
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Msg_ErrorAppInfo, e.Message), Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        ///     Updates the database using data from howlongtobeatsteam.com. Displays an error message on failure. Saves the DB
        ///     afterwards if AutoSaveDatabase is set.
        /// </summary>
        private void UpdateDatabaseFromHLTB()
        {
            Cursor = Cursors.WaitCursor;

            using (HltbPrcDlg dialog = new HltbPrcDlg())
            {
                DialogResult result = dialog.ShowDialog();

                if (dialog.Error != null)
                {
                    Logger.Error(GlobalStrings.DBEditDlg_Log_ExceptionHltb, dialog.Error.Message);
                    MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Msg_ErrorHltb, dialog.Error.Message), Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AddStatus(GlobalStrings.DBEditDlg_ErrorUpdatingHltb);
                }
                else
                {
                    if (result == DialogResult.Cancel || result == DialogResult.Abort)
                    {
                        AddStatus(GlobalStrings.DBEditDlg_CanceledHltbUpdate);
                    }
                    else
                    {
                        AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Status_HltbAutoupdate, dialog.Updated));
                        if (dialog.Updated > 0 && Settings.AutoSaveDatabase)
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
            Category c = null;
            foreach (ListViewItem item in lstCategories.SelectedItems)
            {
                c = item.Tag as Category;
                if (c != null && !(CurrentProfile != null && c == CurrentProfile.GameData.FavoriteCategory))
                {
                    break;
                }

                c = null;
            }

            mbtnCatDelete.Enabled = c != null;
            c = lstCategories.SelectedItems.Count > 0 ? lstCategories.SelectedItems[0].Tag as Category : null;
            mbtnCatRename.Enabled = c != null && !(CurrentProfile != null && c == CurrentProfile.GameData.FavoriteCategory);
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
                if (c != cmbAutoCatType)
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
                foreach (GameInfo gameInfo in _typedListGames.SelectedObjects)
                {
                    if (gameInfo == null)
                    {
                        continue;
                    }

                    AddGameToMultiCatCheckStates(gameInfo, first);
                    AddRemoveCategoryContextMenu(gameInfo);
                    //AddGameToCheckboxStates(game, first);
                    first = false;
                }

                ResortToolStripItemCollection(contextGameRemCat.Items);
            }

            lstMultiCat.EndUpdate();
        }

        private void UpdateInterfaceForSingleCategory()
        {
            menu_Tools_SingleCat.Checked = Settings.SingleCatMode;
            UpdateTitle();
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
                    AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Status_LocalUpdate, totalApps, newApps));
                    success = true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Msg_LocalUpdateError, e.Message), Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Logger.Exception(GlobalStrings.MainForm_Log_ExceptionLocalUpdate, e);
                    AddStatus(GlobalStrings.MainForm_Status_LocalUpdateFailed);
                    success = false;
                }
            }

            if (CurrentProfile.WebUpdate)
            {
                try
                {
                    using (CDlgUpdateProfile dialog = new CDlgUpdateProfile(CurrentProfile.GameData, CurrentProfile.SteamID64, CurrentProfile.OverwriteOnDownload, CurrentProfile.IgnoreList))
                    {
                        DialogResult result = dialog.ShowDialog();

                        if (dialog.Error != null)
                        {
                            Logger.Exception(GlobalStrings.MainForm_Log_ExceptionWebUpdateDialog, dialog.Error);
                            AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_ErrorDownloadingProfileData, "XML"));
                            MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_ErrorDowloadingProfile, dialog.Error.Message), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (result == DialogResult.Abort || result == DialogResult.Cancel)
                            {
                                AddStatus(GlobalStrings.MainForm_DownloadAborted);
                            }
                            else
                            {
                                if (dialog.Fetched == 0)
                                {
                                    MessageBox.Show(GlobalStrings.MainForm_NoGameDataFound, Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    AddStatus(GlobalStrings.MainForm_NoGamesInDownload);
                                }
                                else
                                {
                                    AddStatus(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_DownloadedGames, dialog.Fetched, dialog.Added, "XML"));
                                    success = true;
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Exception(GlobalStrings.MainForm_Log_ExceptionWebUpdate, e);
                    MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_ErrorDowloadingProfile, e.Message), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AddStatus(GlobalStrings.MainForm_DownloadFailed);
                }
            }

            if (success)
            {
                MakeChange(true);
                FullListRefresh();
            }

            Cursor = Cursors.Default;
        }

        /// <summary>
        ///     Updates the text displaying the number of items in the game list
        /// </summary>
        private void UpdateSelectedStatusText()
        {
            mlblStatusSelection.Font = new Font("Arial", 9);
            mlblStatusSelection.Text = string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_SelectedDisplayed, lstGames.SelectedObjects.Count, lstGames.GetItemCount());
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
                picAvatar.Image = CurrentProfile.Avatar;
            }
        }

        #endregion
    }
}
