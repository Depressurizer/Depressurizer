#region LICENSE

//     This file (DBEditDlg.cs) is part of Depressurizer.
//     Copyright (C) 2011 Steve Labbe
//     Copyright (C) 2017 Theodoros Dimos
//     Copyright (C) 2017 Martijn Vegter
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
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Depressurizer.Enums;
using Depressurizer.Models;
using Depressurizer.Properties;
using Rallion;

namespace Depressurizer
{
    public partial class DBEditDlg : Form
    {
        #region Constants

        private const int ID_FILTER_MAX = 1000000;

        #endregion

        #region Fields

        private readonly Dictionary<int, DatabaseEntrySorter.SortModes> columnSortMap = new Dictionary<int, DatabaseEntrySorter.SortModes>
        {
            {
                0, DatabaseEntrySorter.SortModes.Id
            },
            {
                1, DatabaseEntrySorter.SortModes.Name
            },
            {
                2, DatabaseEntrySorter.SortModes.Genre
            },
            {
                3, DatabaseEntrySorter.SortModes.Type
            },
            {
                4, DatabaseEntrySorter.SortModes.IsScraped
            },
            {
                5, DatabaseEntrySorter.SortModes.HasAppInfo
            },
            {
                6, DatabaseEntrySorter.SortModes.Parent
            }
        };

        private readonly DatabaseEntrySorter dbEntrySorter = new DatabaseEntrySorter();

        private readonly List<DatabaseEntry> displayedGames = new List<DatabaseEntry>();

        private readonly GameList ownedList;

        private readonly StringBuilder statusBuilder = new StringBuilder();

        private string currentFilter = string.Empty;

        private int currentMinId, currentMaxId = ID_FILTER_MAX;

        private bool filterSuspend;

        #endregion

        #region Constructors and Destructors

        public DBEditDlg(GameList owned = null)
        {
            InitializeComponent();
            ownedList = owned;
        }

        #endregion

        #region Properties

        private static Database Database => Database.Instance;

        private bool UnsavedChanges { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Shows a dialog allowing the user to add a new entry to the database.
        /// </summary>
        private void AddNewGame()
        {
            GameDBEntryDialog dlg = new GameDBEntryDialog();
            if (dlg.ShowDialog() == DialogResult.OK && dlg.Game != null)
            {
                if (Database.Games.ContainsKey(dlg.Game.Id))
                {
                    MessageBox.Show(GlobalStrings.DBEditDlg_GameIdAlreadyExists, GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AddStatusMsg(string.Format(GlobalStrings.DBEditDlg_FailedToAddGame, dlg.Game.Id));
                }
                else
                {
                    Database.Games.Add(dlg.Game.Id, dlg.Game);

                    if (ShouldDisplayGame(dlg.Game))
                    {
                        displayedGames.Add(dlg.Game);
                        lstGames.VirtualListSize += 1;
                        displayedGames.Sort(dbEntrySorter);
                        InvalidateAllListViewItems();
                    }

                    AddStatusMsg(string.Format(GlobalStrings.DBEditDlg_AddedGame, dlg.Game.Id));
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
            statusBuilder.Append(s);
            statusBuilder.Append(' ');
        }

        private void ApplyIdFilterChange()
        {
            int oldMinId = currentMinId, oldMaxId = currentMaxId;

            if (chkIdRange.Checked)
            {
                currentMinId = (int) numIdRangeMin.Value;
                currentMaxId = (int) numIdRangeMax.Value;
            }
            else
            {
                currentMinId = 0;
                currentMaxId = ID_FILTER_MAX;
            }

            if (currentMinId == oldMinId && currentMaxId == oldMaxId)
            {
                return;
            }

            if (currentMinId < oldMinId || currentMaxId > oldMaxId)
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
            string oldFilter = currentFilter;
            currentFilter = txtSearch.Text;

            if (currentFilter.Equals(oldFilter, StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (currentFilter.IndexOf(oldFilter, StringComparison.CurrentCultureIgnoreCase) == -1)
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

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!filterSuspend)
            {
                filterSuspend = true;
                if (chkTypeAll.Checked)
                {
                    chkTypeDLC.Checked = chkTypeGame.Checked = chkTypeOther.Checked = chkTypeUnknown.Checked = false;
                }

                filterSuspend = false;
                RebuildDisplayList();
            }
        }

        private void chkOwned_CheckedChanged(object sender, EventArgs e)
        {
            RebuildDisplayList();
        }

        private void chkType_CheckedChanged(object sender, EventArgs e)
        {
            if (!filterSuspend)
            {
                filterSuspend = true;

                chkTypeAll.Checked = !(chkTypeDLC.Checked || chkTypeGame.Checked || chkTypeOther.Checked || chkTypeUnknown.Checked);

                filterSuspend = false;
                RebuildDisplayList();
            }
        }

        /// <summary>
        ///     Empties the entire database of all entries.
        /// </summary>
        private void ClearDB()
        {
            if (MessageBox.Show(GlobalStrings.DBEditDlg_AreYouSureToClear, GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                if (Database.Games.Count > 0)
                {
                    UnsavedChanges = true;
                    Database.Games.Clear();
                    AddStatusMsg(GlobalStrings.DBEditDlg_ClearedAllData);
                }

                RebuildDisplayList();
            }
        }

        /// <summary>
        ///     Clears the status buffer without displaying it.
        /// </summary>
        private void ClearStatusMsg()
        {
            statusBuilder.Clear();
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
                Utility.LaunchStorePage(displayedGames[lstGames.SelectedIndices[0]].Id);
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

        private ListViewItem CreateListViewItem(DatabaseEntry g)
        {
            return new ListViewItem(new[]
            {
                g.Id.ToString(),
                g.Name,
                g.Genres != null ? string.Join(",", g.Genres) : "",
                g.AppType.ToString(),
                g.LastStoreScrape == 0 ? "" : "X",
                g.LastAppInfoUpdate == 0 ? "" : "X",
                g.ParentId <= 0 ? "" : g.ParentId.ToString()
            });
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
            if (lstGames.SelectedIndices.Count > 0)
            {
                DialogResult res = MessageBox.Show(string.Format(GlobalStrings.DBEditDlg_AreYouSureDeleteGames, lstGames.SelectedIndices.Count), GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (res == DialogResult.Yes)
                {
                    int deleted = 0;
                    foreach (int index in lstGames.SelectedIndices)
                    {
                        DatabaseEntry game = displayedGames[index];
                        if (game != null)
                        {
                            Database.Games.Remove(game.Id);
                            deleted++;
                        }
                    }

                    AddStatusMsg(string.Format(GlobalStrings.DBEditDlg_DeletedGames, deleted));
                    if (deleted > 0)
                    {
                        UnsavedChanges = true;
                        RefilterDisplayList();
                        lstGames.SelectedIndices.Clear();
                    }
                }
            }
        }

        /// <summary>
        ///     Shows a dialog allowing the user to modify the entry for the first selected game.
        /// </summary>
        private void EditSelectedGame()
        {
            if (lstGames.SelectedIndices.Count > 0)
            {
                DatabaseEntry game = displayedGames[lstGames.SelectedIndices[0]];
                if (game != null)
                {
                    GameDBEntryDialog dlg = new GameDBEntryDialog(game);
                    DialogResult res = dlg.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        lstGames.RedrawItems(lstGames.SelectedIndices[0], lstGames.SelectedIndices[0], true);
                        AddStatusMsg(string.Format(GlobalStrings.DBEditDlg_EditedGame, game.Id));
                        UnsavedChanges = true;
                    }
                }
            }
        }

        /// <summary>
        ///     Fetches the app list provided by the steam public web API
        /// </summary>
        private void FetchList()
        {
            Cursor = Cursors.WaitCursor;

            FetchPrcDlg dlg = new FetchPrcDlg();
            DialogResult res = dlg.ShowDialog();

            if (dlg.Error != null)
            {
                MessageBox.Show(string.Format(GlobalStrings.DBEditDlg_ErrorWhileUpdatingGameList, dlg.Error.Message), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                AddStatusMsg(GlobalStrings.DBEditDlg_ErrorUpdatingGameList);
            }
            else
            {
                if (res == DialogResult.Cancel || res == DialogResult.Abort)
                {
                    AddStatusMsg(GlobalStrings.DBEditDlg_CanceledListUpdate);
                }
                else
                {
                    AddStatusMsg(string.Format(GlobalStrings.DBEditDlg_UpdatedGameList, dlg.Added));
                    UnsavedChanges = true;
                }
            }

            RebuildDisplayList();
            Cursor = Cursors.Default;
        }

        /// <summary>
        ///     Displays the status message and clears it.
        /// </summary>
        private void FlushStatusMsg()
        {
            statusMsg.Text = statusBuilder.ToString();
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

        /// <summary>
        ///     Loads a db from a user-specified file.
        /// </summary>
        private void LoadDatabase()
        {
            if (CheckForUnsaved())
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.DefaultExt = "json";
                dlg.AddExtension = true;
                dlg.CheckFileExists = true;
                dlg.Filter = GlobalStrings.DBEditDlg_DialogFilter;
                DialogResult res = dlg.ShowDialog();
                if (res == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    Database.Load(dlg.FileName);
                    RebuildDisplayList();
                    AddStatusMsg(GlobalStrings.DBEditDlg_FileLoaded);
                    UnsavedChanges = true;
                    Cursor = Cursors.Default;
                }
            }
        }

        private void lstGames_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (columnSortMap.ContainsKey(e.Column))
            {
                dbEntrySorter.SetSortMode(columnSortMap[e.Column]);
                lstGames.SetSortIcon(e.Column, dbEntrySorter.SortDirection > 0 ? SortOrder.Ascending : SortOrder.Descending);
                displayedGames.Sort(dbEntrySorter);
                InvalidateAllListViewItems();
            }
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
            e.Item = CreateListViewItem(displayedGames[e.ItemIndex]);
        }

        private void lstGames_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
        {
            for (int i = e.StartIndex; i < displayedGames.Count; i++)
            {
                if (displayedGames[i].Name.StartsWith(e.Text, StringComparison.CurrentCultureIgnoreCase))
                {
                    e.Index = i;
                    return;
                }
            }

            for (int i = 0; i < e.StartIndex; i++)
            {
                if (displayedGames[i].Name.StartsWith(e.Text, StringComparison.CurrentCultureIgnoreCase))
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
            dbEntrySorter.SetSortMode(columnSortMap[initialSortCol], 1);
            lstGames.SetSortIcon(initialSortCol, SortOrder.Ascending);

            if (ownedList == null)
            {
                chkOwned.Checked = false;
                chkOwned.Enabled = false;
            }

            numIdRangeMax.Maximum = numIdRangeMax.Value = ID_FILTER_MAX;
            numIdRangeMin.Maximum = ID_FILTER_MAX;

            RebuildDisplayList();
        }

        private void menu_File_Clear_Click(object sender, EventArgs e)
        {
            ClearStatusMsg();
            ClearDB();
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

            ;
        }

        private void RebuildDisplayList()
        {
            lstGames.SelectedIndices.Clear();
            displayedGames.Clear();
            foreach (DatabaseEntry g in Database.Games.Values)
            {
                if (ShouldDisplayGame(g))
                {
                    displayedGames.Add(g);
                }
            }

            displayedGames.Sort(dbEntrySorter);
            lstGames.VirtualListSize = displayedGames.Count;
            InvalidateAllListViewItems();
            UpdateStatusCount();
        }

        private void RefilterDisplayList()
        {
            lstGames.SelectedIndices.Clear();
            displayedGames.RemoveAll(ShouldHideGame);
            lstGames.VirtualListSize = displayedGames.Count;
            InvalidateAllListViewItems();
            UpdateStatusCount();
        }

        /// <summary>
        ///     Saves the DB to the given file.
        /// </summary>
        /// <param name="filename">Path to save to</param>
        /// <returns>True if successful</returns>
        private bool Save(string filename)
        {
            Cursor c = Cursor;
            Cursor = Cursors.WaitCursor;
            try
            {
                Database.Save(filename);
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(GlobalStrings.DBEditDlg_ErrorSavingFile, e.Message));
                Cursor = Cursors.Default;
                return false;
            }

            Cursor = c;
            return true;
        }

        /// <summary>
        ///     Saves the database to a user-specified file.
        /// </summary>
        private void SaveAs()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "json";
            dlg.AddExtension = true;
            dlg.CheckFileExists = false;
            dlg.Filter = GlobalStrings.DBEditDlg_DialogFilter;
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                AddStatusMsg(Save(dlg.FileName) ? GlobalStrings.DBEditDlg_FileSaved : GlobalStrings.DBEditDlg_SaveFailed);
            }
        }

        /// <summary>
        ///     Saves the database to the program DB file.
        /// </summary>
        /// <returns></returns>
        private bool SaveDatabase()
        {
            if (Save("database.json"))
            {
                AddStatusMsg(GlobalStrings.DBEditDlg_DatabaseSaved);
                UnsavedChanges = false;
                return true;
            }

            AddStatusMsg(GlobalStrings.DBEditDlg_DatabaseSaveFailed);
            return false;
        }

        /// <summary>
        ///     Performs a web scrape on the given games.
        /// </summary>
        /// <param name="gamesToScrape">Queue of games to scrape</param>
        private void ScrapeGames(Queue<int> gamesToScrape)
        {
            if (gamesToScrape.Count > 0)
            {
                DbScrapeDlg dlg = new DbScrapeDlg(gamesToScrape);
                DialogResult res = dlg.ShowDialog();

                if (dlg.Error != null)
                {
                    AddStatusMsg(GlobalStrings.DBEditDlg_ErrorUpdatingGames);
                    MessageBox.Show(string.Format(GlobalStrings.DBEditDlg_ErrorWhileUpdatingGames, dlg.Error.Message), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (res == DialogResult.Cancel)
                {
                    AddStatusMsg(GlobalStrings.DBEditDlg_UpdateCanceled);
                }
                else if (res == DialogResult.Abort)
                {
                    AddStatusMsg(string.Format(GlobalStrings.DBEditDlg_AbortedUpdate, dlg.JobsCompleted, dlg.JobsTotal));
                }
                else
                {
                    AddStatusMsg(string.Format(GlobalStrings.DBEditDlg_UpdatedEntries, dlg.JobsCompleted));
                }

                if (dlg.JobsCompleted > 0)
                {
                    UnsavedChanges = true;
                    RebuildDisplayList();
                }
            }
            else
            {
                AddStatusMsg(GlobalStrings.DBEditDlg_NoGamesToScrape);
            }
        }

        /// <summary>
        ///     Performs a web scrape on all games without a last scrape date set.
        /// </summary>
        private void ScrapeNew()
        {
            Cursor = Cursors.WaitCursor;

            Queue<int> gamesToScrape = new Queue<int>();

            foreach (DatabaseEntry g in Database.Games.Values)
                //Only scrape displayed games
            {
                if (g.LastStoreScrape == 0 && ShouldDisplayGame(g))
                {
                    gamesToScrape.Enqueue(g.Id);
                }
            }

            ScrapeGames(gamesToScrape);
            Cursor = Cursors.Default;
        }

        /// <summary>
        ///     Performs a web scrape on all games that are selected in the list.
        /// </summary>
        private void ScrapeSelected()
        {
            if (lstGames.SelectedIndices.Count > 0)
            {
                Cursor = Cursors.WaitCursor;

                Queue<int> gamesToScrape = new Queue<int>();

                foreach (int index in lstGames.SelectedIndices)
                {
                    gamesToScrape.Enqueue(displayedGames[index].Id);
                }

                ScrapeGames(gamesToScrape);
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        ///     Determine whether a particular entry should be displayed based on current filter selections
        /// </summary>
        /// <param name="g">entry to evaluate</param>
        /// <returns>True if the entry should be displayed</returns>
        private bool ShouldDisplayGame(DatabaseEntry g)
        {
            if (g == null)
            {
                return false;
            }

            if (!Database.Contains(g.Id))
            {
                return false;
            }

            if (chkIdRange.Checked && (g.Id < currentMinId || g.Id > currentMaxId))
            {
                return false;
            }

            if (ownedList != null && chkOwned.Checked && !ownedList.Games.ContainsKey(g.Id))
            {
                return false;
            }

            if (chkTypeAll.Checked == false)
            {
                switch (g.AppType)
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
                if (radWebNo.Checked && g.LastStoreScrape > 0)
                {
                    return false;
                }

                if (radWebYes.Checked && g.LastStoreScrape <= 0)
                {
                    return false;
                }

                if (radWebSince.Checked && g.LastStoreScrape > Utility.GetUTime(dateWeb.Value))
                {
                    return false;
                }
            }

            if (radAppAll.Checked == false)
            {
                if (radAppNo.Checked && g.LastAppInfoUpdate > 0)
                {
                    return false;
                }

                if (radAppYes.Checked && g.LastAppInfoUpdate <= 0)
                {
                    return false;
                }
            }

            if (currentFilter.Length > 0 && g.Name.IndexOf(currentFilter, StringComparison.CurrentCultureIgnoreCase) == -1)
            {
                return false;
            }

            return true;
        }

        private bool ShouldHideGame(DatabaseEntry g)
        {
            return !ShouldDisplayGame(g);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyTextFilterChange();
        }

        private void UpdateFromAppInfo()
        {
            try
            {
                string path = string.Format(Resources.AppInfoPath, Settings.Instance.SteamPath);
                int updated = Database.UpdateFromAppInfo(path);
                if (updated > 0)
                {
                    UnsavedChanges = true;
                }

                RebuildDisplayList();
                AddStatusMsg(string.Format(GlobalStrings.DBEditDlg_Status_UpdatedAppInfo, updated));
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(GlobalStrings.DBEditDlg_AppInfoUpdateFailed, e.Message), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.DBEditDlg_Log_ExceptionAppInfo, e.ToString());
            }
        }

        private void UpdateFromHltb()
        {
            Cursor = Cursors.WaitCursor;

            HltbPrcDlg dlg = new HltbPrcDlg();
            DialogResult res = dlg.ShowDialog();

            if (dlg.Error != null)
            {
                MessageBox.Show(string.Format(GlobalStrings.DBEditDlg_ErrorWhileUpdatingHltb, dlg.Error.Message), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.DBEditDlg_Log_ExceptionHltb, dlg.Error.Message);
                AddStatusMsg(GlobalStrings.DBEditDlg_ErrorUpdatingHltb);
            }
            else
            {
                if (res == DialogResult.Cancel || res == DialogResult.Abort)
                {
                    AddStatusMsg(GlobalStrings.DBEditDlg_CanceledHltbUpdate);
                }
                else
                {
                    AddStatusMsg(string.Format(GlobalStrings.DBEditDlg_Status_UpdatedHltb, dlg.Updated));
                    UnsavedChanges = true;
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
            statSelected.Text = string.Format(GlobalStrings.DBEditDlg_SelectedDisplayedTotal, lstGames.SelectedIndices.Count, lstGames.VirtualListSize, Database.Games.Count);
            cmdDeleteGame.Enabled = cmdEditGame.Enabled = cmdStore.Enabled = cmdUpdateSelected.Enabled = lstGames.SelectedIndices.Count >= 1;
        }

        #endregion
    }
}
