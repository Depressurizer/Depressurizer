using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Depressurizer.Core;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Interfaces;
using Depressurizer.Core.Models;
using Depressurizer.Dialogs;
using Depressurizer.Properties;

namespace Depressurizer
{
    public partial class DBEditDlg : Form
    {
        #region Fields

        private readonly Dictionary<int, SortModes> _columnSortMap = new Dictionary<int, SortModes>
        {
            {
                0, SortModes.Id
            },
            {
                1, SortModes.Name
            },
            {
                2, SortModes.Genre
            },
            {
                3, SortModes.Type
            },
            {
                4, SortModes.IsScraped
            },
            {
                5, SortModes.HasAppInfo
            },
            {
                6, SortModes.Parent
            }
        };

        private readonly DatabaseEntrySorter _dbEntrySorter = new DatabaseEntrySorter();

        private readonly List<DatabaseEntry> _displayedGames = new List<DatabaseEntry>();

        private readonly IGameList _ownedList;

        private readonly StringBuilder _statusBuilder = new StringBuilder();

        private string _currentFilter = string.Empty;

        private int _currentMaxId = FilterMaxId;

        private int _currentMinId;

        private bool _filterSuspend;

        #endregion

        #region Constructors and Destructors

        public DBEditDlg(IGameList owned = null)
        {
            InitializeComponent();

            updateViewStoreButton();

            _ownedList = owned;
        }

        #endregion

        #region Properties

        private static Database Database => Database.Instance;

        private static int FilterMaxId => 9999999;

        private static Logger Logger => Logger.Instance;

        private static Settings Settings => Settings.Instance;

        private bool UnsavedChanges { get; set; }

        #endregion

        #region Methods

        private static ListViewItem CreateListViewItem(DatabaseEntry entry)
        {
            return new ListViewItem(new[]
            {
                entry.AppId.ToString(CultureInfo.CurrentCulture),
                entry.Name,
                entry.Genres != null ? string.Join(",", entry.Genres) : "",
                entry.AppType.ToString(),
                entry.LastStoreScrape == 0 ? "" : "X",
                entry.LastAppInfoUpdate == 0 ? "" : "X",
                entry.ParentId <= 0 ? "" : entry.ParentId.ToString(CultureInfo.CurrentCulture)
            });
        }

        /// <summary>
        ///     Shows a dialog allowing the user to add a new entry to the database.
        /// </summary>
        private void AddNewGame()
        {
            using (GameDBEntryDialog dlg = new GameDBEntryDialog())
            {
                if (dlg.ShowDialog() != DialogResult.OK || dlg.Game == null)
                {
                    return;
                }

                if (Database.Contains(dlg.Game.AppId))
                {
                    MessageBox.Show(GlobalStrings.DBEditDlg_GameIdAlreadyExists, Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AddStatusMsg(string.Format(CultureInfo.CurrentCulture, GlobalStrings.DBEditDlg_FailedToAddGame, dlg.Game.AppId));
                }
                else
                {
                    Database.Add(dlg.Game);

                    if (ShouldDisplayGame(dlg.Game))
                    {
                        _displayedGames.Add(dlg.Game);
                        lstGames.VirtualListSize += 1;
                        _displayedGames.Sort(_dbEntrySorter);
                        InvalidateAllListViewItems();
                    }

                    AddStatusMsg(string.Format(CultureInfo.CurrentCulture, GlobalStrings.DBEditDlg_AddedGame, dlg.Game.AppId));
                    UnsavedChanges = true;
                    UpdateStatusCount();
                }
            }
        }

        /// <summary>
        ///     Adds a status message to the buffer
        /// </summary>
        /// <param name="s">Message to add</param>
        private void AddStatusMsg(string s)
        {
            _statusBuilder.Append(s);
            _statusBuilder.Append(' ');
        }

        private void ApplyIdFilterChange()
        {
            int oldMinId = _currentMinId, oldMaxId = _currentMaxId;

            if (chkIdRange.Checked)
            {
                _currentMinId = (int) numIdRangeMin.Value;
                _currentMaxId = (int) numIdRangeMax.Value;
            }
            else
            {
                _currentMinId = 0;
                _currentMaxId = FilterMaxId;
            }

            if (_currentMinId == oldMinId && _currentMaxId == oldMaxId)
            {
                return;
            }

            if (_currentMinId < oldMinId || _currentMaxId > oldMaxId)
            {
                RebuildDisplayList();
            }
            else
            {
                RefilterDisplayList();
            }
        }

        private void ApplyTextFilterChange()
        {
            string oldFilter = _currentFilter;
            _currentFilter = txtSearch.Text;

            if (_currentFilter.Equals(oldFilter, StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (_currentFilter.IndexOf(oldFilter, StringComparison.CurrentCultureIgnoreCase) == -1)
            {
                RebuildDisplayList();
            }
            else
            {
                RefilterDisplayList();
            }
        }

        /// <summary>
        ///     If there are any unsaved changes, asks the user if they want to save. Also gives the user the option to cancel the
        ///     calling action.
        /// </summary>
        /// <returns>True if the action should proceed, false otherwise.</returns>
        private bool CheckForUnsaved()
        {
            if (!UnsavedChanges)
            {
                return true;
            }

            DialogResult res = MessageBox.Show(GlobalStrings.DBEditDlg_UnsavedChangesSave, GlobalStrings.DBEditDlg_UnsavedChanges, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (res == DialogResult.No)
            {
                return true;
            }

            if (res == DialogResult.Cancel)
            {
                return false;
            }

            return SaveDatabase();
        }

        private void CheckShowIgnored_CheckedChanged(object sender, EventArgs e)
        {
            RebuildDisplayList();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!_filterSuspend)
            {
                _filterSuspend = true;
                if (chkTypeAll.Checked)
                {
                    chkTypeDLC.Checked = chkTypeGame.Checked = chkTypeOther.Checked = chkTypeUnknown.Checked = false;
                }

                _filterSuspend = false;
                RebuildDisplayList();
            }
        }

        private void ChkCommunityInsteadStore_CheckedChanged(object sender, EventArgs e)
        {
            updateViewStoreButton();
        }

        private void chkOwned_CheckedChanged(object sender, EventArgs e)
        {
            RebuildDisplayList();
        }

        private void chkType_CheckedChanged(object sender, EventArgs e)
        {
            if (!_filterSuspend)
            {
                _filterSuspend = true;

                chkTypeAll.Checked = !(chkTypeDLC.Checked || chkTypeGame.Checked || chkTypeOther.Checked || chkTypeUnknown.Checked);

                _filterSuspend = false;
                RebuildDisplayList();
            }
        }

        private void ClearDatabase()
        {
            if (MessageBox.Show(GlobalStrings.DBEditDlg_AreYouSureToClear, GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
            {
                return;
            }

            if (Database.Count > 0)
            {
                UnsavedChanges = true;
                Database.Clear();
                AddStatusMsg(GlobalStrings.DBEditDlg_ClearedAllData);
            }

            RebuildDisplayList();
        }

        /// <summary>
        ///     Clears the status buffer without displaying it.
        /// </summary>
        private void ClearStatusMsg()
        {
            _statusBuilder.Clear();
        }

        private void cmdAddGame_Click(object sender, EventArgs e)
        {
            ClearStatusMsg();
            AddNewGame();
            FlushStatusMsg();
        }

        private void cmdDeleteGame_Click(object sender, EventArgs e)
        {
            ClearStatusMsg();
            DeleteSelectedGames();
            FlushStatusMsg();
        }

        private void cmdEditGame_Click(object sender, EventArgs e)
        {
            ClearStatusMsg();
            EditSelectedGame();
            FlushStatusMsg();
        }

        private void cmdFetch_Click(object sender, EventArgs e)
        {
            ClearStatusMsg();
            FetchList();
            FlushStatusMsg();
        }

        private void cmdSearchClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
        }

        private void cmdStore_Click(object sender, EventArgs e)
        {
            if (lstGames.SelectedIndices.Count > 0)
            {
                if (!chkCommunityInsteadStore.Checked)
                {
                    Steam.LaunchStorePage(_displayedGames[lstGames.SelectedIndices[0]].AppId);
                }
                else
                {
                    Steam.LaunchSteamCommunityPage(_displayedGames[lstGames.SelectedIndices[0]].AppId);
                }
            }
        }

        private void cmdUpdateAppInfo_Click(object sender, EventArgs e)
        {
            ClearStatusMsg();
            UpdateFromAppInfo();
            FlushStatusMsg();
        }

        private void cmdUpdateHltb_Click(object sender, EventArgs e)
        {
            ClearStatusMsg();
            UpdateFromHltb();
            FlushStatusMsg();
        }

        private void cmdUpdateSelected_Click(object sender, EventArgs e)
        {
            ClearStatusMsg();
            ScrapeSelected();
            FlushStatusMsg();
        }

        private void cmdUpdateUnchecked_Click(object sender, EventArgs e)
        {
            ClearStatusMsg();
            ScrapeNew();
            FlushStatusMsg();
        }

        private void dateWeb_ValueChanged(object sender, EventArgs e)
        {
            if (radWebSince.Checked)
            {
                RebuildDisplayList();
            }
        }

        private void DBEditDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CheckForUnsaved())
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        ///     Removes all selected games from the database.
        /// </summary>
        private void DeleteSelectedGames()
        {
            if (lstGames.SelectedIndices.Count <= 0)
            {
                return;
            }

            DialogResult res = MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.DBEditDlg_AreYouSureDeleteGames, lstGames.SelectedIndices.Count), GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (res != DialogResult.Yes)
            {
                return;
            }

            int deleted = 0;
            foreach (int index in lstGames.SelectedIndices)
            {
                DatabaseEntry game = _displayedGames[index];
                if (game == null)
                {
                    continue;
                }

                Database.Remove(game.AppId);
                deleted++;
            }

            AddStatusMsg(string.Format(CultureInfo.CurrentCulture, GlobalStrings.DBEditDlg_DeletedGames, deleted));
            if (deleted <= 0)
            {
                return;
            }

            UnsavedChanges = true;
            RefilterDisplayList();
            lstGames.SelectedIndices.Clear();
        }

        /// <summary>
        ///     Shows a dialog allowing the user to modify the entry for the first selected game.
        /// </summary>
        private void EditSelectedGame()
        {
            if (lstGames.SelectedIndices.Count <= 0)
            {
                return;
            }

            DatabaseEntry game = _displayedGames[lstGames.SelectedIndices[0]];
            if (game == null)
            {
                return;
            }

            using (GameDBEntryDialog dialog = new GameDBEntryDialog(game))
            {
                DialogResult result = dialog.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }

                lstGames.RedrawItems(lstGames.SelectedIndices[0], lstGames.SelectedIndices[0], true);
                AddStatusMsg(string.Format(CultureInfo.CurrentCulture, GlobalStrings.DBEditDlg_EditedGame, game.AppId));
                UnsavedChanges = true;
            }
        }

        /// <summary>
        ///     Fetches the app list provided by the steam public web API
        /// </summary>
        private void FetchList()
        {
            Cursor = Cursors.WaitCursor;

            int added = 0;
            try
            {
                added = Database.FetchIntegrateAppList();
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.DBEditDlg_ErrorWhileUpdatingGameList, e.Message), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                AddStatusMsg(GlobalStrings.DBEditDlg_ErrorUpdatingGameList);
            }

            AddStatusMsg(string.Format(CultureInfo.CurrentCulture, GlobalStrings.DBEditDlg_UpdatedGameList, added));
            UnsavedChanges = true;

            RebuildDisplayList();
            Cursor = Cursors.Default;
        }

        /// <summary>
        ///     Displays the status message and clears it.
        /// </summary>
        private void FlushStatusMsg()
        {
            statusMsg.Text = _statusBuilder.ToString();
            ClearStatusMsg();
        }

        private void IdFilter_Changed(object sender, EventArgs e)
        {
            ApplyIdFilterChange();
        }

        private void InvalidateAllListViewItems()
        {
            if (lstGames.VirtualListSize > 0)
            {
                lstGames.RedrawItems(0, lstGames.VirtualListSize - 1, true);
            }
        }

        private void LoadDatabase()
        {
            if (!CheckForUnsaved())
            {
                return;
            }

            string path;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.DefaultExt = "json";
                dialog.AddExtension = true;
                dialog.CheckFileExists = true;
                dialog.Filter = Resources.DatabaseSaveFilter;

                DialogResult result = dialog.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }

                path = dialog.FileName;
            }

            Cursor = Cursors.WaitCursor;

            try
            {
                Database.Load(path);
                RebuildDisplayList();
                AddStatusMsg(GlobalStrings.DBEditDlg_FileLoaded);
                UnsavedChanges = true;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void lstGames_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (!_columnSortMap.ContainsKey(e.Column))
            {
                return;
            }

            _dbEntrySorter.SetSortMode(_columnSortMap[e.Column]);
            lstGames.SetSortIcon(e.Column, _dbEntrySorter.SortDirection > 0 ? SortOrder.Ascending : SortOrder.Descending);
            _displayedGames.Sort(_dbEntrySorter);
            InvalidateAllListViewItems();
        }

        private void lstGames_DoubleClick(object sender, EventArgs e)
        {
            if (lstGames.SelectedIndices.Count > 0)
            {
                ClearStatusMsg();
                EditSelectedGame();
                FlushStatusMsg();
            }
        }

        private void lstGames_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    if (e.Modifiers == Keys.Control)
                    {
                        for (int i = 0; i < lstGames.VirtualListSize; i++)
                        {
                            lstGames.SelectedIndices.Add(i);
                        }
                    }

                    break;
                case Keys.Enter:
                    if (lstGames.SelectedIndices.Count > 0)
                    {
                        ClearStatusMsg();
                        EditSelectedGame();
                        FlushStatusMsg();
                    }

                    break;
                case Keys.N:
                    if (e.Modifiers == Keys.Control)
                    {
                        ClearStatusMsg();
                        AddNewGame();
                        FlushStatusMsg();
                    }

                    break;
                case Keys.Delete:
                    if (lstGames.SelectedIndices.Count > 0)
                    {
                        ClearStatusMsg();
                        DeleteSelectedGames();
                        FlushStatusMsg();
                    }

                    break;
            }
        }

        private void lstGames_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex < 0)
            {
                return;
            }

            e.Item = CreateListViewItem(_displayedGames[e.ItemIndex]);
        }

        private void lstGames_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
        {
            for (int i = e.StartIndex; i < _displayedGames.Count; i++)
            {
                if (_displayedGames[i].Name.StartsWith(e.Text, StringComparison.CurrentCultureIgnoreCase))
                {
                    e.Index = i;
                    return;
                }
            }

            for (int i = 0; i < e.StartIndex; i++)
            {
                if (_displayedGames[i].Name.StartsWith(e.Text, StringComparison.CurrentCultureIgnoreCase))
                {
                    e.Index = i;
                    return;
                }
            }
        }

        private void lstGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStatusCount();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Initialize list sorting
            int initialSortCol = 0;
            _dbEntrySorter.SetSortMode(_columnSortMap[initialSortCol], 1);
            lstGames.SetSortIcon(initialSortCol, SortOrder.Ascending);

            if (_ownedList == null)
            {
                chkOwned.Checked = false;
                chkOwned.Enabled = false;
            }

            numIdRangeMax.Maximum = numIdRangeMax.Value = FilterMaxId;
            numIdRangeMin.Maximum = FilterMaxId;

            RebuildDisplayList();
        }

        private void menu_File_Clear_Click(object sender, EventArgs e)
        {
            ClearStatusMsg();
            ClearDatabase();
            FlushStatusMsg();
        }

        private void menu_File_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menu_File_Load_Click(object sender, EventArgs e)
        {
            ClearStatusMsg();
            LoadDatabase();
            FlushStatusMsg();
        }

        private void menu_File_Save_Click(object sender, EventArgs e)
        {
            ClearStatusMsg();
            SaveDatabase();
            FlushStatusMsg();
        }

        private void menu_File_SaveAs_Click(object sender, EventArgs e)
        {
            ClearStatusMsg();
            SaveAs();
            FlushStatusMsg();
        }

        private void radApp_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton) sender).Checked)
            {
                RebuildDisplayList();
            }
        }

        private void radWeb_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton) sender).Checked)
            {
                RebuildDisplayList();
            }
        }

        private void RebuildDisplayList()
        {
            lstGames.SelectedIndices.Clear();
            _displayedGames.Clear();
            foreach (DatabaseEntry g in Database.Values)
            {
                if (ShouldDisplayGame(g))
                {
                    _displayedGames.Add(g);
                }
            }

            _displayedGames.Sort(_dbEntrySorter);
            lstGames.VirtualListSize = _displayedGames.Count;
            InvalidateAllListViewItems();
            UpdateStatusCount();
        }

        private void RefilterDisplayList()
        {
            lstGames.SelectedIndices.Clear();
            _displayedGames.RemoveAll(ShouldHideGame);
            lstGames.VirtualListSize = _displayedGames.Count;
            InvalidateAllListViewItems();
            UpdateStatusCount();
        }

        private bool Save(string path)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                Database.Save(path);
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.DBEditDlg_ErrorSavingFile, e.Message));
                Cursor = Cursors.Default;
                return false;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            return true;
        }

        private void SaveAs()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.FileName = "database";
                dialog.DefaultExt = "json";
                dialog.AddExtension = true;
                dialog.CheckFileExists = false;
                dialog.Filter = Resources.DatabaseSaveFilter;

                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    AddStatusMsg(Save(dialog.FileName) ? GlobalStrings.DBEditDlg_FileSaved : GlobalStrings.DBEditDlg_SaveFailed);
                }
            }
        }

        /// <summary>
        ///     Saves the database to the program DB file.
        /// </summary>
        /// <returns></returns>
        private bool SaveDatabase()
        {
            if (Save(Locations.File.Database))
            {
                AddStatusMsg(GlobalStrings.DBEditDlg_DatabaseSaved);
                UnsavedChanges = false;
                return true;
            }

            AddStatusMsg(GlobalStrings.DBEditDlg_DatabaseSaveFailed);
            return false;
        }

        private void ScrapeGames(ICollection<ScrapeJob> scrapeJobs)
        {
            if (scrapeJobs.Count <= 0)
            {
                AddStatusMsg(GlobalStrings.DBEditDlg_NoGamesToScrape);
                return;
            }

            using (ScrapeDialog dialog = new ScrapeDialog(scrapeJobs))
            {
                DialogResult result = dialog.ShowDialog();

                if (dialog.Error != null)
                {
                    AddStatusMsg(GlobalStrings.DBEditDlg_ErrorUpdatingGames);
                    MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.DBEditDlg_ErrorWhileUpdatingGames, dialog.Error.Message), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                switch (result)
                {
                    case DialogResult.Cancel:
                        AddStatusMsg(GlobalStrings.DBEditDlg_UpdateCanceled);
                        break;
                    case DialogResult.Abort:
                        AddStatusMsg(string.Format(CultureInfo.CurrentCulture, GlobalStrings.DBEditDlg_AbortedUpdate, dialog.JobsCompleted, dialog.TotalJobs));
                        break;
                    default:
                        AddStatusMsg(string.Format(CultureInfo.CurrentCulture, GlobalStrings.DBEditDlg_UpdatedEntries, dialog.JobsCompleted));
                        break;
                }

                if (dialog.JobsCompleted <= 0)
                {
                    return;
                }

                UnsavedChanges = true;
                RebuildDisplayList();
            }
        }

        /// <summary>
        ///     Performs a web scrape on all games without a last scrape date set.
        /// </summary>
        private void ScrapeNew()
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                List<ScrapeJob> scrapeJobs = Database.Values.Where(e => e.LastStoreScrape == 0 && ShouldDisplayGame(e)).Select(entry => new ScrapeJob(entry.Id, entry.AppId)).ToList();
                ScrapeGames(scrapeJobs);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ScrapeSelected()
        {
            if (lstGames.SelectedIndices.Count <= 0)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            List<ScrapeJob> appIds = new List<ScrapeJob>();
            foreach (int index in lstGames.SelectedIndices)
            {
                appIds.Add(new ScrapeJob(_displayedGames[index].Id, _displayedGames[index].AppId));
            }

            try
            {
                ScrapeGames(appIds);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private bool ShouldDisplayGame(DatabaseEntry entry)
        {
            if (entry == null)
            {
                return false;
            }

            if (Settings.IgnoreList.Contains(entry.AppId) && !CheckShowIgnored.Checked)
            {
                return false;
            }

            if (chkIdRange.Checked && (entry.AppId < _currentMinId || entry.AppId > _currentMaxId))
            {
                return false;
            }

            if (!Database.Contains(entry.AppId))
            {
                return false;
            }

            if (_ownedList != null && chkOwned.Checked && !_ownedList.Games.ContainsKey(entry.AppId))
            {
                return false;
            }

            if (chkTypeAll.Checked == false)
            {
                switch (entry.AppType)
                {
                    case AppType.Game:
                        if (chkTypeGame.Checked == false)
                        {
                            return false;
                        }

                        break;
                    case AppType.DLC:
                        if (chkTypeDLC.Checked == false)
                        {
                            return false;
                        }

                        break;
                    case AppType.Unknown:
                        if (chkTypeUnknown.Checked == false)
                        {
                            return false;
                        }

                        break;
                    default:
                        if (chkTypeOther.Checked == false)
                        {
                            return false;
                        }

                        break;
                }
            }

            if (radWebAll.Checked == false)
            {
                if (radWebNo.Checked && entry.LastStoreScrape > 0)
                {
                    return false;
                }

                if (radWebYes.Checked && entry.LastStoreScrape <= 0)
                {
                    return false;
                }

                if (radWebSince.Checked && entry.LastStoreScrape > ((DateTimeOffset) dateWeb.Value).ToUnixTimeSeconds())
                {
                    return false;
                }
            }

            if (radAppAll.Checked == false)
            {
                if (radAppNo.Checked && entry.LastAppInfoUpdate > 0)
                {
                    return false;
                }

                if (radAppYes.Checked && entry.LastAppInfoUpdate <= 0)
                {
                    return false;
                }
            }

            if (_currentFilter.Length > 0 && entry.Name.IndexOf(_currentFilter, StringComparison.CurrentCultureIgnoreCase) == -1)
            {
                return false;
            }

            return true;
        }

        private bool ShouldHideGame(DatabaseEntry entry)
        {
            if (entry == null)
            {
                return true;
            }

            return !ShouldDisplayGame(entry);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyTextFilterChange();
        }

        private void UpdateFromAppInfo()
        {
            try
            {
                string path = string.Format(CultureInfo.InvariantCulture, Constants.AppInfo, Settings.Instance.SteamPath);
                int updated = Database.UpdateFromAppInfo(path);
                if (updated > 0)
                {
                    UnsavedChanges = true;
                }

                RebuildDisplayList();
                AddStatusMsg(string.Format(CultureInfo.CurrentCulture, GlobalStrings.DBEditDlg_Status_UpdatedAppInfo, updated));
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.DBEditDlg_AppInfoUpdateFailed, e.Message), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error(GlobalStrings.DBEditDlg_Log_ExceptionAppInfo, e.ToString());
            }
        }

        private void UpdateFromHltb()
        {
            Cursor = Cursors.WaitCursor;

            using (HltbPrcDlg dialog = new HltbPrcDlg())
            {
                DialogResult result = dialog.ShowDialog();

                if (dialog.Error != null)
                {
                    MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.DBEditDlg_ErrorWhileUpdatingHltb, dialog.Error.Message), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.Error(GlobalStrings.DBEditDlg_Log_ExceptionHltb, dialog.Error.Message);
                    AddStatusMsg(GlobalStrings.DBEditDlg_ErrorUpdatingHltb);
                }
                else
                {
                    if (result == DialogResult.Cancel || result == DialogResult.Abort)
                    {
                        AddStatusMsg(GlobalStrings.DBEditDlg_CanceledHltbUpdate);
                    }
                    else
                    {
                        AddStatusMsg(string.Format(CultureInfo.CurrentCulture, GlobalStrings.DBEditDlg_Status_UpdatedHltb, dialog.Updated));
                        UnsavedChanges = true;
                    }
                }
            }

            RebuildDisplayList();
            Cursor = Cursors.Default;
        }

        /// <summary>
        ///     Update form elements after the selection status in the game list is modified.
        /// </summary>
        private void UpdateStatusCount()
        {
            statSelected.Text = string.Format(CultureInfo.CurrentCulture, GlobalStrings.DBEditDlg_SelectedDisplayedTotal, lstGames.SelectedIndices.Count, lstGames.VirtualListSize, Database.Count);
            cmdDeleteGame.Enabled = cmdEditGame.Enabled = cmdStore.Enabled = cmdUpdateSelected.Enabled = lstGames.SelectedIndices.Count >= 1;
        }

        private void updateViewStoreButton()
        {
            if (!chkCommunityInsteadStore.Checked)
            {
                cmdStore.Text = Resources.cmdStore_StoreText;
            }
            else
            {
                cmdStore.Text = Resources.cmdStore_CommunityText;
            }
        }

        #endregion
    }
}
