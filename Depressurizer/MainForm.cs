/*
Copyright 2011, 2012, 2013 Steve Labbe.

This file is part of Depressurizer.

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
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Rallion;

namespace Depressurizer {
    public partial class FormMain : Form {
        #region Fields
        // Stores currently loaded profile
        Profile currentProfile;
        // Stores all actual game data
        GameData gameData;

        // Game list sorting state
        MultiColumnListViewComparer listSorter = new MultiColumnListViewComparer();

        // Stores last selected category to minimize game list refreshes
        object lastSelectedCat = null;

        bool unsavedChanges = false;

        Settings settings = Settings.Instance();

        StringBuilder statusBuilder = new StringBuilder();

        // Allow visual feedback when dragging over the cat list
        bool isDragging;
        int dragOldCat;

        // jpodadera. Used to reload resources of main form while switching language
        private int originalWidth, originalHeight;

        #endregion
        #region Properties
        /// <summary>
        /// Just checks to see if there is currently a profile loaded
        /// </summary>
        public bool ProfileLoaded {
            get {
                return currentProfile != null;
            }
        }
        #endregion
        public FormMain() {
            gameData = new GameData();
            InitializeComponent();
            // jpodadera. Changed combo to checkbox to set favorite value
            //combFavorite.SelectedIndex = 0;
            chkFavorite.Checked = false;

            listSorter.AddIntCol( 1 );
            lstGames.ListViewItemSorter = listSorter;

            FillCategoryList();
        }
        #region Manual Operations

        /// <summary>
        /// Loads a Steam configuration file and adds its data to the currently loaded game list. Asks the user to select a file, handles the load, and refreshes the UI.
        /// </summary>
        void ManualImport() {
            if( ProfileLoaded || gameData.Games.Count > 0 ) {
                if (MessageBox.Show(GlobalStrings.MainForm_AddContentsOfSteamConfigFile,
                    GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                 == DialogResult.No ) {
                    return;
                }
            }

            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult res = dlg.ShowDialog();
            if( res == DialogResult.OK ) {
                Cursor = Cursors.WaitCursor;
                try {
                    int loadedGames = gameData.ImportSteamFile( dlg.FileName, null, settings.IgnoreDlc );
                    if( loadedGames == 0 ) {
                        MessageBox.Show(GlobalStrings.MainForm_NoGameInfoFound, GlobalStrings.DBEditDlg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        AddStatus(GlobalStrings.MainForm_NoGamesFound);
                    } else {
                        MakeChange( true );
                        AddStatus(string.Format(GlobalStrings.MainForm_ImportedGames, loadedGames));
                        lastSelectedCat = null; // Make sure the game list refreshes
                        FillCategoryList();
                    }
                } catch( ApplicationException e ) {
                    MessageBox.Show(e.Message, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AddStatus(GlobalStrings.MainForm_ImportFailed);
                }
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Saves a Steam configuration file. Asks the user to select the file to save as.
        /// </summary>
        /// <returns>True if save was completed, false otherwise</returns>
        bool ManualExport() {
            SaveFileDialog dlg = new SaveFileDialog();
            DialogResult res = dlg.ShowDialog();
            if( res == DialogResult.OK ) {
                Cursor = Cursors.WaitCursor;
                try {
                    gameData.SaveSteamFile( dlg.FileName, settings.RemoveExtraEntries );
                    AddStatus(GlobalStrings.MainForm_DataExported);
                    return true;
                } catch( ApplicationException e ) {
                    MessageBox.Show(e.Message, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AddStatus(GlobalStrings.MainForm_ExportFailed);
                }
                Cursor = Cursors.Default;
            }
            return false;
        }

        /// <summary>
        /// Loads game list information from a Steam profile and adds to currently loaded game list. Asks user for Steam profile name.
        /// </summary>
        public void ManualDownload() {

            if( ProfileLoaded || gameData.Games.Count > 0 ) {
                if (MessageBox.Show(GlobalStrings.MainForm_AddContentsOfSteamCommunity,
                    GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                 == DialogResult.No ) {
                    return;
                }
            }
            
            DlgManualDownload dlg = new DlgManualDownload();
            if( dlg.ShowDialog() == DialogResult.OK ) {

                try {
                    if( dlg.Custom ) {
                        DownloadProfileData( dlg.UrlVal, true, null, settings.IgnoreDlc );
                    } else {
                        DownloadProfileData( dlg.IdVal, true, null, settings.IgnoreDlc );
                    }
                } catch( ApplicationException e ) {
                    MessageBox.Show(e.Message, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AddStatus(GlobalStrings.MainForm_ErrorDownloadingGames);
                }
            }
        }

        #endregion

        private void DownloadProfileData( Int64 steamId, bool overwrite, SortedSet<int> ignore, bool ignoreDlc ) {
            CDlgUpdateProfile updateDlg = new CDlgUpdateProfile( gameData, steamId, overwrite, ignore, ignoreDlc );
            DownloadProfileDataHelper( updateDlg );
        }

        private void DownloadProfileData( string customUrl, bool overwrite, SortedSet<int> ignore, bool ignoreDlc ) {
            CDlgUpdateProfile updateDlg = new CDlgUpdateProfile( gameData, customUrl, overwrite, ignore, ignoreDlc );
            DownloadProfileDataHelper( updateDlg );
        }

        private void DownloadProfileDataHelper( CDlgUpdateProfile updateDlg ) {
            DialogResult res = updateDlg.ShowDialog();

            if( updateDlg.Error != null ) {
                AddStatus(string.Format(GlobalStrings.MainForm_ErrorDownloadingProfileData, updateDlg.UseHtml ? "HTML" : "XML"));
                MessageBox.Show(string.Format(GlobalStrings.MainForm_ErrorDowloadingProfile, updateDlg.Error.Message), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                if( res == DialogResult.Abort || res == DialogResult.Cancel ) {
                    AddStatus(GlobalStrings.MainForm_DownloadAborted);
                } else {
                    if( updateDlg.Failover ) {
                        AddStatus(GlobalStrings.MainForm_XMLDownloadFailed);
                    }
                    if( updateDlg.Fetched == 0 ) {
                        MessageBox.Show(GlobalStrings.MainForm_NoGameDataFound, GlobalStrings.DBEditDlg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        AddStatus(GlobalStrings.MainForm_NoGamesInDownload);
                    } else {
                        MakeChange( true );
                        AddStatus(string.Format(GlobalStrings.MainForm_DownloadedGames, updateDlg.Fetched, updateDlg.Added, updateDlg.UseHtml ? "HTML" : "XML"));
                        FillCategoryList();
                        FillGameList();
                    }
                }
            }
        }

        private void LoadGameDB() {
            try {
                Program.GameDB = new GameDB();
                if( File.Exists( "GameDB.xml.gz" ) ) {
                    Program.GameDB.Load( "GameDB.xml.gz" );
                } else if( File.Exists( "GameDB.xml" ) ) {
                    Program.GameDB.Load( "GameDB.xml" );
                } else {
                    throw new ApplicationException(GlobalStrings.MainForm_GameDBFileNotExist);
                }
            } catch( Exception ex ) {
                MessageBox.Show(GlobalStrings.MainForm_ErrorLoadingGameDB + ex.Message);
                Program.GameDB = new GameDB();
            }
        }

        #region Profile Management

        /// <summary>
        /// Prompts user to create a new profile.
        /// </summary>
        void CreateProfile() {
            DlgProfile dlg = new DlgProfile();
            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                Cursor = Cursors.WaitCursor;
                currentProfile = dlg.Profile;
                gameData = currentProfile.GameData;
                AddStatus(GlobalStrings.MainForm_ProfileCreated);
                if( dlg.DownloadNow ) {
                    UpdateProfileDownload( false );
                }

                if( dlg.ImportNow ) {
                    UpdateProfileImport( false );
                }
                if( dlg.SetStartup ) {
                    settings.StartupAction = StartupAction.Load;
                    settings.ProfileToLoad = currentProfile.FilePath;
                    settings.Save();
                }

                FillCategoryList();
                FillGameList();
                Cursor = Cursors.Default;
            }
            UpdateEnableStatesForProfileChange();
        }

        /// <summary>
        /// Prompts the user to modify the currently loaded profile.
        /// </summary>
        void EditProfile() {
            if( ProfileLoaded ) {
                DlgProfile dlg = new DlgProfile( currentProfile );
                if( dlg.ShowDialog() == DialogResult.OK ) {
                    AddStatus(GlobalStrings.MainForm_ProfileEdited);
                    MakeChange( true );
                    Cursor = Cursors.WaitCursor;
                    bool refresh = false;
                    if( dlg.DownloadNow ) {
                        UpdateProfileDownload( false );
                        refresh = true;
                    }
                    if( dlg.ImportNow ) {
                        UpdateProfileImport( false );
                        refresh = true;
                    }
                    if( dlg.SetStartup ) {
                        settings.StartupAction = StartupAction.Load;
                        settings.ProfileToLoad = currentProfile.FilePath;
                        settings.Save();
                    }
                    Cursor = Cursors.Default;
                    if( refresh ) {
                        FillCategoryList();
                        FillGameList();
                    }
                }
            } else {
                if (MessageBox.Show(GlobalStrings.MainForm_NoProfileLoaded, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    CreateProfile();
                }
            }
        }

        /// <summary>
        /// Prompts user for a profile file to load, then loads it.
        /// </summary>
        void LoadProfile() {
            if( !CheckForUnsaved() ) return;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "profile";
            dlg.AddExtension = true;
            dlg.CheckFileExists = true;
            dlg.Filter = GlobalStrings.DlgProfile_Filter;
            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                LoadProfile( dlg.FileName, false );
            }
        }

        /// <summary>
        /// Loads the given profile file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="checkForChanges"></param>
        void LoadProfile( string path, bool checkForChanges = true ) {
            if( checkForChanges && !CheckForUnsaved() ) return;

            try {
                currentProfile = Profile.Load( path );
                AddStatus(GlobalStrings.MainForm_ProfileLoaded);
            } catch( ApplicationException e ) {
                MessageBox.Show(e.Message, GlobalStrings.MainForm_ErrorLoadingProfile, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AddStatus(GlobalStrings.MainForm_FailedLoadProfile);
                return;
            }

            gameData = currentProfile.GameData;

            if( currentProfile.AutoDownload ) {
                UpdateProfileDownload();
            }
            if( currentProfile.AutoImport ) {
                UpdateProfileImport();
            }

            FillCategoryList();
            FillGameList();
            UpdateEnableStatesForProfileChange();
        }

        /// <summary>
        /// Prompts user for a file location and saves profile
        /// </summary>
        void SaveProfileAs() {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "profile";
            dlg.AddExtension = true;
            dlg.CheckPathExists = true;
            dlg.Filter = GlobalStrings.DlgProfile_Filter;
            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                SaveProfile( dlg.FileName );
            }
        }

        /// <summary>
        /// Saves profile data to a file and performs any related tasks. This is the main saving function, all saves go through this function.
        /// </summary>
        /// <param name="path">Path to save to. If null, just saves profile to its current path.</param>
        /// <returns>True if successful, false if there is a failure</returns>
        bool SaveProfile( string path = null ) {
            if( currentProfile.AutoExport ) {
                ProfileExport();
            }
            try {
                if( path == null ) {
                    currentProfile.Save();
                } else {
                    currentProfile.Save( path );
                }
                AddStatus(GlobalStrings.MainForm_ProfileSaved);
                MakeChange( false );
                return true;
            } catch( ApplicationException e ) {
                MessageBox.Show(e.Message, GlobalStrings.MainForm_ErrorSavingProfile, MessageBoxButtons.OK, MessageBoxIcon.Error);
                AddStatus(GlobalStrings.MainForm_FailedSaveProfile);
                return false;
            }

        }

        /// <summary>
        /// Attempts to download game list for the loaded profile.
        /// </summary>
        /// <param name="updateUI">If true, will update the UI</param>
        void UpdateProfileDownload( bool updateUI = true ) {
            if( currentProfile != null ) {
                if( updateUI ) Cursor = Cursors.WaitCursor;
                try {
                    DownloadProfileData( currentProfile.SteamID64, currentProfile.OverwriteOnDownload, currentProfile.IgnoreList, currentProfile.IgnoreDlc );
                } catch( ApplicationException e ) {
                    if( updateUI ) Cursor = Cursors.Default;
                    MessageBox.Show(e.Message, GlobalStrings.MainForm_ErrorDownloadingGameList, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AddStatus(GlobalStrings.MainForm_DownloadFailed);
                }
                if( updateUI ) Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Attempts to import steam categories
        /// </summary>
        /// <param name="updateUI"></param>
        void UpdateProfileImport( bool updateUI = true ) {
            if( currentProfile != null ) {
                if( updateUI ) Cursor = Cursors.WaitCursor;
                try {
                    int count = currentProfile.ImportSteamData();
                    AddStatus(string.Format(GlobalStrings.MainForm_ImportedItems, count));
                    if( count > 0 ) {
                        MakeChange( true );
                        if( updateUI ) {
                            FillCategoryList();
                            FillGameList();
                        }
                    }
                    if( updateUI ) Cursor = Cursors.Default;
                } catch( ApplicationException e ) {
                    if( updateUI ) Cursor = Cursors.Default;
                    MessageBox.Show(e.Message, GlobalStrings.MainForm_ErrorImportingSteamDataList, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AddStatus(GlobalStrings.MainForm_ImportFailed);
                }
            }
        }

        /// <summary>
        /// Attempts to export steam categories
        /// </summary>
        void ProfileExport() {
            if( currentProfile != null ) {
                try {
                    currentProfile.ExportSteamData();
                    AddStatus(GlobalStrings.MainForm_ExportedCategories);
                } catch( ApplicationException e ) {
                    MessageBox.Show(e.Message, GlobalStrings.MainForm_ErrorExportingToSteam, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AddStatus(GlobalStrings.MainForm_ExportFailed);
                }
            }
        }

        #endregion
        #region Data modifiers

        /// <summary>
        /// Creates a new category, first prompting the user for the name to use. If the name is not valid or in use, displays a notification.
        /// Also updates the UI, and selects the new category in the category combobox.
        /// </summary>
        /// <returns>The category that was added, or null if the operation was canceled or failed.</returns>
        Category CreateCategory() {
            GetStringDlg dlg = new GetStringDlg(string.Empty, GlobalStrings.MainForm_CreateCategory, GlobalStrings.MainForm_EnterNewCategoryName, GlobalStrings.MainForm_Create);
            if( dlg.ShowDialog() == DialogResult.OK && CatUtil.ValidateCategoryName( dlg.Value ) ) {
                Category newCat = gameData.AddCategory( dlg.Value );
                if( newCat != null ) {
                    FillCategoryList();
                    combCategory.SelectedItem = newCat;
                    MakeChange( true );
                    AddStatus(string.Format(GlobalStrings.MainForm_CategoryAdded, newCat.Name));
                    return newCat;
                } else {
                    MessageBox.Show(String.Format(GlobalStrings.MainForm_CouldNotAddCategory, dlg.Value), GlobalStrings.DBEditDlg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            return null;
        }

        /// <summary>
        /// Deletes the given category and updates the UI. Prompts user for confirmation. Will completely rebuild the gamelist.
        /// </summary>
        /// <param name="c">Category to delete.</param>
        /// <returns>True if deletion occurred, false otherwise.</returns>
        bool DeleteCategory() {
            if( lstCategories.SelectedItems.Count > 0 ) {
                Category c = lstCategories.SelectedItem as Category;
                if( c != null ) {
                    DialogResult res = MessageBox.Show(string.Format(GlobalStrings.MainForm_DeleteCategory, c.Name), GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if( res == System.Windows.Forms.DialogResult.Yes ) {
                        if( gameData.RemoveCategory( c ) ) {
                            FillCategoryList();
                            FillGameList();
                            MakeChange( true );
                            AddStatus(string.Format(GlobalStrings.MainForm_CategoryDeleted, c.Name));
                            return true;
                        } else {
                            MessageBox.Show(string.Format(GlobalStrings.MainForm_CouldNotDeleteCategory, c.Name), GlobalStrings.DBEditDlg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Renames the given category. Prompts user for a new name. Updates UI. Will display an error if the rename fails.
        /// </summary>
        /// <param name="c">Category to rename</param>
        /// <returns>True if category was renamed, false otherwise.</returns>
        bool RenameCategory() {
            if( lstCategories.SelectedItems.Count > 0 ) {
                Category c = lstCategories.SelectedItem as Category;
                if( c != null ) {
                    GetStringDlg dlg = new GetStringDlg(c.Name, string.Format(GlobalStrings.MainForm_RenameCategory, c.Name), GlobalStrings.MainForm_EnterNewName, GlobalStrings.MainForm_Rename);
                    if( dlg.ShowDialog() == DialogResult.OK ) {
                        return RenameCategoryHelper( c, dlg.Value );
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Renames a category as specified, displaying an error if the operation fails.
        /// </summary>
        /// <param name="c">Category to rename</param>
        /// <param name="newName">Name to change to</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool RenameCategoryHelper( Category c, string newName ) {
            if( newName == c.Name ) return true;
            if( CatUtil.ValidateCategoryName( newName ) && gameData.RenameCategory( c, newName ) ) {
                FillCategoryList();
                UpdateGameList();
                MakeChange( true );
                AddStatus(string.Format(GlobalStrings.MainForm_CategoryRenamed, c.Name));
                return true;
            } else {
                MessageBox.Show(string.Format(GlobalStrings.MainForm_NameIsInUse, newName), GlobalStrings.DBEditDlg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        /// <summary>
        /// Adds a new game. Displays the game dialog to the user.
        /// </summary>
        void AddGame() {
            DlgGame dlg = new DlgGame( gameData, null );
            if( dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK ) {
                if( ProfileLoaded ) {
                    if( currentProfile.IgnoreList.Remove( dlg.Game.Id ) ) {
                        AddStatus(string.Format(GlobalStrings.MainForm_UnignoredGame, dlg.Game.Id));
                    }
                }
                FillCategoryList();
                FillGameList();
                MakeChange( true );
                AddStatus(GlobalStrings.MainForm_AddedGame);
            }
        }

        /// <summary>
        /// Edits the first selected game. Displays game dialog.
        /// </summary>
        void EditGame() {
            if( lstGames.SelectedIndices.Count > 0 ) {
                int index = lstGames.SelectedIndices[0];
                Game g = lstGames.Items[index].Tag as Game;
                DlgGame dlg = new DlgGame( gameData, g );
                if( dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK ) {
                    FillCategoryList();
                    UpdateGame( index );
                    MakeChange( true );
                    AddStatus(GlobalStrings.MainForm_EditedGame);
                }
            }
        }

        /// <summary>
        /// Removes all selected games. Prompts for confirmation.
        /// </summary>
        void RemoveGames() {
            int selectCount = lstGames.SelectedIndices.Count;
            if( selectCount > 0 ) {
                if (MessageBox.Show(string.Format(GlobalStrings.MainForm_RemoveGame, selectCount, (selectCount == 1) ? "" : "s"), GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes ) {
                    int ignored = 0;
                    int removed = 0;
                    foreach( ListViewItem item in lstGames.SelectedItems ) {
                        Game g = (Game)item.Tag;
                        if( gameData.Games.Remove( g.Id ) ) {
                            removed++;
                        }
                        if( ProfileLoaded && currentProfile.AutoIgnore ) {
                            if( currentProfile.IgnoreList.Add( g.Id ) ) {
                                ignored++;
                            }
                        }
                    }
                    if( removed > 0 ) {
                        AddStatus(string.Format(GlobalStrings.MainForm_RemovedGame, removed, (removed == 1) ? "" : "s"));
                        MakeChange( true );
                    }
                    if( ignored > 0 ) {
                        AddStatus(string.Format(GlobalStrings.MainForm_IgnoredGame, ignored, (ignored == 1) ? "" : "s"));
                        MakeChange( true );
                    }
                    UpdateGameListSelected();
                }
            }
        }

        /// <summary>
        /// Assigns the given category to all selected items in the game list.
        /// </summary>
        /// <param name="cat">Category to assign</param>
        void AssignCategoryToSelectedGames( Category cat ) {
            if( lstGames.SelectedItems.Count > 0 ) {
                foreach( ListViewItem item in lstGames.SelectedItems ) {
                    ( item.Tag as Game ).Category = cat;
                }
                UpdateGameListSelected();
                MakeChange( true );
            }
        }

        /// <summary>
        /// Assigns the given favorite state to all selected items in the game list.
        /// </summary>
        /// <param name="fav">True to turn fav on, false to turn it off.</param>
        void AssignFavoriteToSelectedGames( bool fav ) {
            if( lstGames.SelectedItems.Count > 0 ) {
                foreach( ListViewItem item in lstGames.SelectedItems ) {
                    ( item.Tag as Game ).Favorite = fav;
                }
                UpdateGameListSelected();
                MakeChange( true );
            }
        }

        /// <summary>
        /// Unloads the current profile or game list, making sure the user gets the option to save any changes.
        /// </summary>
        /// <param name="updateUI">If true, will redraw the UI. Otherwise, will leave it up to the caller to do so.</param>
        /// <returns>True if there is now no loaded profile, false otherwise.</returns>
        void Unload( bool updateUI = true ) {
            if( !CheckForUnsaved() ) {
                return;
            }

            AddStatus(GlobalStrings.MainForm_ClearedData);
            currentProfile = null;
            gameData = new GameData();
            MakeChange( false );
            UpdateEnableStatesForProfileChange();
            if( updateUI ) {
                FillCategoryList();
                FillGameList();
            }
        }

        private void Autocategorize( bool selectedOnly ) {
            // Check to see if there are any selected items with set categories
            bool overwrite = true;
            List<Game> gamesToUpdate = new List<Game>();

            if( selectedOnly ) {
                foreach( ListViewItem item in lstGames.SelectedItems ) {
                    Game g = item.Tag as Game;
                    if( g != null ) {
                        if( g.Category != null ) {
                            overwrite = false;
                        }
                        gamesToUpdate.Add( g );
                    }
                }
            } else {
                foreach( Game g in gameData.Games.Values ) {
                    if( g != null ) {
                        if( g.Category != null ) {
                            overwrite = false;
                        }
                        gamesToUpdate.Add( g );
                    }
                }
            }

            if( overwrite == false ) {
                DialogResult res = MessageBox.Show(GlobalStrings.MainForm_SomeSelectedGameHaveCategories, GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if( res == System.Windows.Forms.DialogResult.Yes ) overwrite = true;
            }

            int updated = 0;

            Queue<int> notFound = new Queue<int>();

            foreach( Game g in gamesToUpdate ) {
                if( g != null && ( overwrite || g.Category == null ) ) {
                    if( Program.GameDB.Contains( g.Id ) ) {
                        g.Category = gameData.GetCategory( Program.GameDB.GetGenre( g.Id, settings.FullAutocat ) );
                        updated++;
                    } else {
                        notFound.Enqueue( g.Id );
                    }
                }
            }

            AddStatus(string.Format(GlobalStrings.MainForm_UpdatedCategories, updated));
            if( updated > 0 ) MakeChange( true );

            if( notFound.Count > 0 ) {
                DialogResult res = MessageBox.Show(string.Format(GlobalStrings.MainForm_GamesNotFoundInGameDB, notFound.Count), GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if( res == System.Windows.Forms.DialogResult.Yes ) {
                    CDlgDataScrape scrapeDlg = new CDlgDataScrape( notFound, gameData, settings.FullAutocat );
                    DialogResult scrapeRes = scrapeDlg.ShowDialog();

                    if( scrapeRes == DialogResult.Cancel ) {
                        AddStatus(string.Format(GlobalStrings.MainForm_CanceledWebUpdate, scrapeDlg.JobsTotal));
                    } else {
                        if( scrapeRes == DialogResult.Abort ) {
                            AddStatus(string.Format(GlobalStrings.MainForm_UpdatedViaWebAndAborted, scrapeDlg.JobsCompleted, scrapeDlg.JobsTotal));
                        } else {
                            AddStatus(string.Format(GlobalStrings.MainForm_UpdatedViaWeb, scrapeDlg.JobsCompleted));
                        }
                        if( scrapeDlg.Failures > 0 ) {
                            MessageBox.Show(string.Format(GlobalStrings.MainForm_FailedToLoadStorePages, scrapeDlg.Failures), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            AddStatus(string.Format(GlobalStrings.MainForm_ErrorOcurredOnGames, scrapeDlg.Failures));
                        }
                        if( scrapeDlg.JobsCompleted > 0 ) MakeChange( true );
                    }
                }
            }

            FillCategoryList();
            FillGameList();
        }

        private void AutonameAll() {
            DialogResult res = MessageBox.Show(GlobalStrings.MainForm_OverwriteExistingNames, GlobalStrings.MainForm_Overwrite, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            bool overwrite = false;

            if( res == DialogResult.Cancel ) {
                AddStatus(GlobalStrings.MainForm_AutonameCanceled);
                return;
            } else if( res == DialogResult.Yes ) {
                overwrite = true;
            }

            int named = 0;
            foreach( Game g in gameData.Games.Values ) {
                if( overwrite || string.IsNullOrEmpty( g.Name ) ) {
                    g.Name = Program.GameDB.GetName( g.Id );
                    named++;
                }
            }
            AddStatus(string.Format(GlobalStrings.MainForm_AutonamedGames, named));
            if( named > 0 ) {
                MakeChange( true );
            }

            FillGameList();
        }

        void RemoveEmptyCats() {
            int count = gameData.RemoveEmptyCategories();
            AddStatus(string.Format(GlobalStrings.MainForm_RemovedEmptyCategories, count));
            FillCategoryList();
        }

        #endregion
        #region UI Updaters
        #region Status and text updaters

        /// <summary>
        /// Adds a string to the status builder
        /// </summary>
        /// <param name="s"></param>
        public void AddStatus( string s ) {
            statusBuilder.Append( s );
            statusBuilder.Append( ' ' );
        }

        /// <summary>
        /// Empties the status builder
        /// </summary>
        public void ClearStatus() {
            statusBuilder.Clear();
        }

        /// <summary>
        /// Sets the status text to the builder text, and clear the builder text.
        /// </summary>
        public void FlushStatus() {
            statusMsg.Text = statusBuilder.ToString();
            statusBuilder.Clear();
        }

        /// <summary>
        /// Updates the text displaying the number of items in the game list
        /// </summary>
        private void UpdateSelectedStatusText() {
            statusSelection.Text = string.Format(GlobalStrings.MainForm_SelectedDisplayed, lstGames.SelectedItems.Count, lstGames.Items.Count);
        }

        /// <summary>
        /// Updates the window title.
        /// </summary>
        void UpdateTitle() {
            StringBuilder sb = new StringBuilder( "Depressurizer" );
            if( ProfileLoaded ) {
                sb.Append( " - " );
                sb.Append( Path.GetFileName( currentProfile.FilePath ) );
            }
            if( unsavedChanges ) {
                sb.Append( " *" );
            }
            this.Text = sb.ToString();
        }

        #endregion
        #region List updaters

        /// <summary>
        /// Completely re-populates the game list based on the current category selection.
        /// </summary>
        private void FillGameList() {
            lstGames.BeginUpdate();
            lstGames.Items.Clear();
            if( lstCategories.SelectedItems.Count > 0 ) {
                object catObj = lstCategories.SelectedItem;
                bool showAll = false;
                bool showFav = false;
                if( catObj is string ) {
                    if( (string)catObj == CatUtil.CAT_ALL_NAME ) {
                        showAll = true;
                    } else if( (string)catObj == CatUtil.CAT_FAV_NAME ) {
                        showFav = true;
                    }
                }

                Category cat = lstCategories.SelectedItem as Category;

                foreach (Game g in gameData.Games.Values)
                {
                    if (showAll || (showFav && g.Favorite) || (!showFav && g.Category == cat))
                    {
                        AddGameToList(g);
                    }
                }

                lstGames.Sort();
            }
            lstGames.EndUpdate();
            UpdateSelectedStatusText();
        }

        /// <summary>
        /// Adds an entry to the game list representing the given game.
        /// </summary>
        /// <param name="g">The game the new entry should represent.</param>
        private void AddGameToList( Game g ) {
            string catName = ( g.Category == null ) ? CatUtil.CAT_UNC_NAME : g.Category.Name;

            // jpodadera. Change favorite column contents from Y-N to X or blank
            //ListViewItem item = new ListViewItem(new string[] { g.Name, g.Id.ToString(), catName, g.Favorite ? GlobalStrings.MainForm_FirstLetterYes : GlobalStrings.MainForm_FirstLetterNo });
            ListViewItem item = new ListViewItem(new string[] { g.Id.ToString(), g.Name, catName, g.Favorite ? "X" : String.Empty });
            item.Tag = g;
            lstGames.Items.Add(item);
        }

        /// <summary>
        /// Completely repopulates the category list and combobox. Maintains selection on both.
        /// </summary>
        private void FillCategoryList() {
            gameData.Categories.Sort();
            object[] catList = gameData.Categories.ToArray();

            lstCategories.BeginUpdate();
            object selected = lstCategories.SelectedItem;
            lstCategories.Items.Clear();
            lstCategories.Items.Add( CatUtil.CAT_ALL_NAME );
            lstCategories.Items.Add( CatUtil.CAT_FAV_NAME );
            lstCategories.Items.Add( CatUtil.CAT_UNC_NAME );
            lstCategories.Items.AddRange( catList );
            if( selected == null || !lstCategories.Items.Contains( selected ) ) {
                lstCategories.SelectedIndex = 0;
            } else {
                lstCategories.SelectedItem = selected;
            }
            lstCategories.EndUpdate();

            combCategory.BeginUpdate();
            selected = combCategory.SelectedItem;
            combCategory.Items.Clear();
            combCategory.Items.Add( CatUtil.CAT_UNC_NAME );
            combCategory.Items.AddRange( catList );
            combCategory.SelectedItem = selected;
            combCategory.EndUpdate();

            while( contextGame_SetCat.DropDownItems.Count > 3 ) {
                contextGame_SetCat.DropDownItems.RemoveAt( 3 );
            }
            foreach( Category c in gameData.Categories ) {
                ToolStripItem item = contextGame_SetCat.DropDownItems.Add( c.Name );
                item.Tag = c;
                item.Click += contextGameCat_Category_Click;
            }
        }

        /// <summary>
        /// Updates the entry for the game in the given position in the list.
        /// </summary>
        /// <param name="index">List index of the game to update</param>
        /// <returns>True if game should be in the list, false otherwise.</returns>
        bool UpdateGame( int index ) {
            ListViewItem item = lstGames.Items[index];
            Game g = (Game)item.Tag;
            if( ShouldDisplayGame( g ) ) {
                item.SubItems[0].Text = g.Name;
                item.SubItems[2].Text = g.Category == null ? CatUtil.CAT_UNC_NAME : g.Category.Name;
                // jpodadera. Change favorite column contents from Y-N to X or blank
                //item.SubItems[3].Text = g.Favorite ? GlobalStrings.MainForm_FirstLetterYes : GlobalStrings.MainForm_FirstLetterNo;
                item.SubItems[3].Text = g.Favorite ? "X" : String.Empty;
                return true;
            } else {
                lstGames.Items.RemoveAt( index );
                return false;
            }
        }

        /// <summary>
        /// Updates list item for every game on the list, removing games that no longer need to be there, but not adding new ones.
        /// </summary>
        void UpdateGameList() {
            int i = 0;
            lstGames.BeginUpdate();
            while( i < lstGames.Items.Count ) {
                if( UpdateGame( i ) ) i++;
            }
            lstGames.EndUpdate();
            UpdateSelectedStatusText();
        }

        /// <summary>
        /// Updates the list item for every selected item on the list.
        /// </summary>
        void UpdateGameListSelected() {
            int i = 0;
            lstGames.BeginUpdate();
            while( i < lstGames.SelectedIndices.Count ) {
                if( UpdateGame( lstGames.SelectedIndices[i] ) ) i++;
            }
            lstGames.EndUpdate();
            UpdateSelectedStatusText();
        }

        #endregion
        #region Enabled-state updaters

        /// <summary>
        /// Runs after the loaded profile might change and sets the enabled states of some interface elements
        /// </summary>
        void UpdateEnableStatesForProfileChange() {
            bool enable = ProfileLoaded;
            menu_File_SaveProfile.Enabled = enable;
            menu_File_SaveProfileAs.Enabled = enable;

            menu_Profile_Download.Enabled = enable;
            menu_Profile_Export.Enabled = enable;
            menu_Profile_Import.Enabled = enable;
            menu_Profile_Edit.Enabled = enable;

            UpdateTitle();
        }

        /// <summary>
        /// Updates enabled states for all game and category buttons
        /// </summary>
        void UpdateButtonEnabledStates() {
            bool gamesSelected = lstGames.SelectedIndices.Count > 0;
            cmdGameRemove.Enabled = gamesSelected;
            cmdGameEdit.Enabled = gamesSelected;
            cmdGameSetCategory.Enabled = gamesSelected;
            cmdGameSetFavorite.Enabled = gamesSelected;
            cmdGameLaunch.Enabled = lstGames.SelectedIndices.Count == 1;
            contextGame_LaunchGame.Enabled = cmdGameLaunch.Enabled;

            bool catSelected = lstCategories.SelectedIndices.Count > 0;
            cmdCatDelete.Enabled = catSelected;
            cmdCatRename.Enabled = catSelected;
        }

        #endregion
        #endregion
        #region UI Event Handlers
        #region General
        private void FormMain_Load( object sender, EventArgs e ) {
            UpdateButtonEnabledStates();
            LoadGameDB();
            // jpodadera. Save original width and height
            originalHeight = this.Height;
            originalWidth = this.Width;
        }

        private void FormMain_Shown( object sender, EventArgs e ) {
            ClearStatus();
            if( settings.SteamPath == null ) {
                DlgSteamPath dlg = new DlgSteamPath();
                dlg.ShowDialog();
                settings.SteamPath = dlg.Path;
                settings.Save();
            }
            switch( settings.StartupAction ) {
                case StartupAction.Load:
                    LoadProfile( settings.ProfileToLoad, false );
                    break;
                case StartupAction.Create:
                    CreateProfile();
                    break;
            }
            FlushStatus();

        }

        private void FormMain_FormClosing( object sender, FormClosingEventArgs e ) {
            if( e.CloseReason == CloseReason.UserClosing ) {
                e.Cancel = !CheckForUnsaved();
            }
        }
        #endregion
        #region Drag and drop

        private void lstCategories_DragEnter( object sender, DragEventArgs e ) {
            e.Effect = DragDropEffects.Move;
        }

        private void lstCategories_DragDrop( object sender, DragEventArgs e ) {
            if( e.Data.GetDataPresent( typeof( int[] ) ) ) {
                lstCategories.SelectedIndex = dragOldCat;
                isDragging = false;
                ClearStatus();
                Point clientPoint = lstCategories.PointToClient( new Point( e.X, e.Y ) );
                object dropItem = lstCategories.Items[lstCategories.IndexFromPoint( clientPoint )];
                if( dropItem is Category ) {
                    gameData.SetGameCategories( (int[])e.Data.GetData( typeof( int[] ) ), (Category)dropItem );
                    UpdateGameList();
                    MakeChange( true );
                } else if( dropItem is string ) {
                    if( (string)dropItem == CatUtil.CAT_FAV_NAME ) {
                        gameData.SetGameFavorites( (int[])e.Data.GetData( typeof( int[] ) ), true );
                        UpdateGameList();
                        MakeChange( true );
                    } else if( (string)dropItem == CatUtil.CAT_UNC_NAME ) {
                        gameData.SetGameCategories( (int[])e.Data.GetData( typeof( int[] ) ), null );
                        UpdateGameList();
                        MakeChange( true );
                    }
                }

                FlushStatus();
            }
        }

        private void lstGames_ItemDrag( object sender, ItemDragEventArgs e ) {
            int[] selectedGames = new int[lstGames.SelectedItems.Count];
            for( int i = 0; i < lstGames.SelectedItems.Count; i++ ) {
                selectedGames[i] = ( (Game)lstGames.SelectedItems[i].Tag ).Id;
            }
            isDragging = true;
            dragOldCat = lstCategories.SelectedIndex;
            lstGames.DoDragDrop( selectedGames, DragDropEffects.Move );

        }

        private void lstCategories_DragOver( object sender, DragEventArgs e ) {
            if( isDragging ) {
                lstCategories.SelectedIndex = lstCategories.IndexFromPoint( lstCategories.PointToClient( new Point( e.X, e.Y ) ) );
            }
        }

        #endregion
        #region Main menu

        private void menu_File_NewProfile_Click( object sender, EventArgs e ) {
            ClearStatus();
            CreateProfile();
            FlushStatus();
        }

        private void menu_File_LoadProfile_Click( object sender, EventArgs e ) {
            ClearStatus();
            LoadProfile();
            FlushStatus();
        }

        private void menu_File_SaveProfile_Click( object sender, EventArgs e ) {
            ClearStatus();
            SaveProfile();
            FlushStatus();
        }

        private void menu_File_SaveProfileAs_Click( object sender, EventArgs e ) {
            ClearStatus();
            SaveProfileAs();
            FlushStatus();
        }

        private void menu_File_Close_Click( object sender, EventArgs e ) {
            ClearStatus();
            Unload();
            FlushStatus();
        }

        private void menu_File_Manual_Import_Click( object sender, EventArgs e ) {
            ClearStatus();
            ManualImport();
            FlushStatus();
        }

        private void menu_File_Manual_Download_Click( object sender, EventArgs e ) {
            ClearStatus();
            ManualDownload();
            FlushStatus();
        }

        private void menu_File_Manual_Export_Click( object sender, EventArgs e ) {
            ClearStatus();
            ManualExport();
            FlushStatus();
        }

        private void menu_File_Exit_Click( object sender, EventArgs e ) {
            this.Close();
        }

        private void menu_Profile_Download_Click( object sender, EventArgs e ) {
            ClearStatus();
            UpdateProfileDownload();
            FlushStatus();
        }

        private void menu_Profile_Import_Click( object sender, EventArgs e ) {
            ClearStatus();
            UpdateProfileImport();
            FlushStatus();
        }

        private void menu_Profile_Export_Click( object sender, EventArgs e ) {
            ClearStatus();
            ProfileExport();
            FlushStatus();
        }

        private void menu_Profile_Edit_Click( object sender, EventArgs e ) {
            ClearStatus();
            EditProfile();
            FlushStatus();
        }

        private void menu_Tools_AutocatAll_Click( object sender, EventArgs e ) {
            ClearStatus();
            Autocategorize( false );
            FlushStatus();
        }

        private void menu_Tools_AutonameAll_Click( object sender, EventArgs e ) {
            ClearStatus();
            AutonameAll();
            FlushStatus();
        }

        private void menu_Tools_RemoveEmpty_Click( object sender, EventArgs e ) {
            ClearStatus();
            RemoveEmptyCats();
            FlushStatus();
        }

        private void menu_Tools_DBEdit_Click( object sender, EventArgs e ) {
            Depressurizer.DBEditDlg dlg = new Depressurizer.DBEditDlg();
            dlg.ShowDialog();
            LoadGameDB();
        }

        /// <summary>
        /// jpodadera. Recursive function to reload resources of new language for a menu item and its childs
        /// </summary>
        /// <param name="item"></param> Item menu to reload resources
        /// <param name="resources"></param> Resource manager
        /// <param name="newCulture"></param> Culture of language to load
        private void changeLanguageToolStripItems(ToolStripItem item, ComponentResourceManager resources, CultureInfo newCulture)
        {
            if (item != null)
            {
                if (item is ToolStripDropDownItem)
                {
                    foreach (ToolStripItem childItem in (item as ToolStripDropDownItem).DropDownItems)
                        changeLanguageToolStripItems(childItem, resources, newCulture);
                }
                resources.ApplyResources(item, item.Name, newCulture);
            }
        }
        
        /// <summary>
        /// jpodadera. Recursive function to reload resources of new language for a control and its childs 
        /// </summary>
        /// <param name="c"></param> Control to reload resources
        /// <param name="resources"></param> Resource manager
        /// <param name="newCulture"></param> Culture of language to load
        private void changeLanguageControls(Control c, ComponentResourceManager resources, CultureInfo newCulture)
        {
            if (c != null)
            {
                if (c.GetType() == typeof(MenuStrip))
                {
                    foreach (ToolStripDropDownItem mItem in (c as MenuStrip).Items)
                        changeLanguageToolStripItems(mItem, resources, newCulture);
                }
                else if (c.GetType() == typeof(ListView))
                {
                    // jpodadera. Because a framework bug, names of ColumnHeader objects are empty. 
                    // Resolved by saving names to Tag property.
                    foreach (ColumnHeader cHeader in (c as ListView).Columns)
                        resources.ApplyResources(cHeader, cHeader.Tag.ToString(), newCulture);
                }
                else
                {
                    foreach (Control childControl in c.Controls)
                        changeLanguageControls(childControl, resources, newCulture);
                }
                resources.ApplyResources(c, c.Name, newCulture);
            }
        }

        private void menu_Tools_Settings_Click( object sender, EventArgs e ) {
            ClearStatus();
            DlgOptions dlg = new DlgOptions();

            // jpodadera. Save culture of actual language
            CultureInfo actualCulture = Thread.CurrentThread.CurrentUICulture;

            dlg.ShowDialog();

            // jpodadera. If language has been changed, reload resources of main window
            if (actualCulture.Name != Thread.CurrentThread.CurrentUICulture.Name)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(FormMain));
                resources.ApplyResources(this, this.Name, Thread.CurrentThread.CurrentUICulture);

                // jpodadera. Save actual size and recover original size before reload resources of controls
                int actualWidth = this.Width;
                int actualHeight = this.Height;
                this.Width = this.originalWidth;
                this.Height = this.originalHeight;

                changeLanguageControls(this, resources, Thread.CurrentThread.CurrentUICulture);

                // jpodadera. Recover previous size
                this.Width = actualWidth;
                this.Height = actualHeight;
                
                // reload new strings for status bar
                UpdateSelectedStatusText();
            }

            FlushStatus();
        }

        #endregion
        #region Context menus

        private void contextCat_Opening( object sender, System.ComponentModel.CancelEventArgs e ) {
            bool selectedCat = lstCategories.SelectedItems.Count > 0 && lstCategories.SelectedItem as Category != null;
            contextCat_Delete.Enabled = contextCat_Rename.Enabled = selectedCat;
        }

        private void contectCat_RemoveEmpty_Click( object sender, EventArgs e ) {
            ClearStatus();
            RemoveEmptyCats();
            FlushStatus();
        }

        private void contextGame_Opening( object sender, System.ComponentModel.CancelEventArgs e ) {
            bool selectedGames = lstGames.SelectedItems.Count > 0;
            cntxtGame_Edit.Enabled = selectedGames;
            contextGame_Remove.Enabled = selectedGames;
            contextGame_SetCat.Enabled = selectedGames;
            contextGame_SetFav.Enabled = selectedGames;
            contextGame_VisitStore.Enabled = selectedGames;
        }

        private void contextGame_SetFav_Yes_Click( object sender, EventArgs e ) {
            ClearStatus();
            AssignFavoriteToSelectedGames( true );
            FlushStatus();
        }

        private void contextGame_SetFav_No_Click( object sender, EventArgs e ) {
            ClearStatus();
            AssignFavoriteToSelectedGames( false );
            FlushStatus();
        }

        private void contextGameCat_Create_Click( object sender, EventArgs e ) {
            Category c = CreateCategory();
            if( c != null ) {
                ClearStatus();
                AssignCategoryToSelectedGames( c );
                FlushStatus();
            }
        }

        private void contextGameCat_Category_Click( object sender, EventArgs e ) {
            ToolStripItem menuItem = sender as ToolStripItem;
            if( menuItem != null ) {
                ClearStatus();
                Category c = menuItem.Tag as Category;
                AssignCategoryToSelectedGames( c );
                FlushStatus();
            }
        }

        private void contextGame_VisitStore_Click( object sender, EventArgs e ) {
            VisitSelectedGameStorePage();
        }

        #endregion
        #region Buttons

        private void cmdCatAdd_Click( object sender, EventArgs e ) {
            ClearStatus();
            CreateCategory();
            FlushStatus();
        }

        private void cmdCatRename_Click( object sender, EventArgs e ) {
            ClearStatus();
            RenameCategory();
            FlushStatus();
        }

        private void cmdCatDelete_Click( object sender, EventArgs e ) {
            ClearStatus();
            DeleteCategory();
            FlushStatus();
        }

        private void cmdGameSetCategory_Click( object sender, EventArgs e ) {
            Category c;
            if( CatUtil.StringToCategory( combCategory.Text, gameData, out c ) ) {
                ClearStatus();
                FillCategoryList();
                AssignCategoryToSelectedGames( c );
                FlushStatus();
            }
        }

        private void cmdGameSetFavorite_Click( object sender, EventArgs e ) {
            ClearStatus();
            AssignFavoriteToSelectedGames( GetSelectedFavorite() );
            FlushStatus();
        }

        private void cmdAutoCat_Click( object sender, EventArgs e ) {
            ClearStatus();
            Autocategorize( true );
            FlushStatus();
        }

        private void cmdGameAdd_Click( object sender, EventArgs e ) {
            ClearStatus();
            AddGame();
            FlushStatus();
        }

        private void cmdGameEdit_Click( object sender, EventArgs e ) {
            ClearStatus();
            EditGame();
            FlushStatus();
        }

        private void cmdGameRemove_Click( object sender, EventArgs e ) {
            ClearStatus();
            RemoveGames();
            FlushStatus();
        }

        private void cmdGameLaunch_Click(object sender, EventArgs e)
        {
            ClearStatus();
            if (lstGames.SelectedItems.Count > 0)
            {
                Game g = lstGames.SelectedItems[0].Tag as Game;
                LaunchGame(g);
            }
            FlushStatus();
        }

        #endregion
        #region Assorted list events

        private void lstCategories_SelectedIndexChanged( object sender, EventArgs e ) {
            if( !isDragging ) {
                if( lstCategories.SelectedItem != lastSelectedCat ) {
                    FillGameList();
                    lastSelectedCat = lstCategories.SelectedItem;
                }
                UpdateButtonEnabledStates();
            }
        }

        private void lstCategories_KeyDown( object sender, KeyEventArgs e ) {
            switch( e.KeyCode ) {
                case Keys.Delete:
                    ClearStatus();
                    DeleteCategory();
                    FlushStatus();
                    break;
                case Keys.N:
                    ClearStatus();
                    if( e.Modifiers == Keys.Control ) CreateCategory();
                    FlushStatus();
                    break;
                case Keys.F2:
                    ClearStatus();
                    RenameCategory();
                    FlushStatus();
                    break;
            }
        }

        private void lstCategories_MouseDown( object sender, MouseEventArgs e ) {
            if( e.Button == System.Windows.Forms.MouseButtons.Right ) {
                int index = lstCategories.IndexFromPoint( new Point( e.X, e.Y ) );
                if( index >= 0 )
                    lstCategories.SelectedIndex = index;

            }
        }

        private void lstGames_ColumnClick( object sender, ColumnClickEventArgs e ) {
            listSorter.ColClick( e.Column );
            lstGames.Sort();
        }

        private void lstGames_SelectedIndexChanged( object sender, EventArgs e ) {
            UpdateSelectedStatusText();
            UpdateButtonEnabledStates();
        }

        private void lstGames_AfterLabelEdit( object sender, LabelEditEventArgs e ) {
            Game g = lstGames.Items[e.Item].Tag as Game;
            string oldName = g.Name;
            if( oldName != e.Label ) {
                g.Name = e.Label;
                MakeChange( true );
            }
        }

        private void lstGames_DoubleClick( object sender, EventArgs e ) {
            ClearStatus();
            EditGame();
            FlushStatus();
        }

        private void lstGames_KeyDown( object sender, KeyEventArgs e ) {
            ClearStatus();
            switch( e.KeyCode ) {
                case Keys.Delete:
                    RemoveGames();
                    break;
                case Keys.N:
                    if( e.Control ) AddGame();
                    break;
                case Keys.Enter:
                    EditGame();
                    break;
                case Keys.F2:
                    if( lstGames.SelectedItems.Count > 0 ) lstGames.SelectedItems[0].BeginEdit();
                    break;
                case Keys.A:
                    if( e.Control ) {
                        foreach( ListViewItem i in lstGames.Items ) {
                            i.Selected = true;
                        }
                    }
                    break;
            }
            FlushStatus();
        }

        #endregion
        #endregion
        #region Utility

        /// <summary>
        /// Sets the unsaved changes flag to the given value and takes the requisite UI updating action
        /// </summary>
        /// <param name="changes"></param>
        void MakeChange( bool changes ) {
            unsavedChanges = changes;
            UpdateTitle();
        }

        /// <summary>
        /// If there are any unsaved changes, asks the user if they want to save. Also gives the user the option to cancel the calling action.
        /// </summary>
        /// <returns>True if the action should proceed, false otherwise.</returns>
        bool CheckForUnsaved() {
            if( !unsavedChanges ) {
                return true;
            }

            DialogResult res = MessageBox.Show(GlobalStrings.MainForm_UnsavedChangesWillBeLost, GlobalStrings.MainForm_UnsavedChanges, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if( res == System.Windows.Forms.DialogResult.No ) {
                // Don't save, just continue
                return true;
            }
            if( res == System.Windows.Forms.DialogResult.Cancel ) {
                // Don't save, don't continue
                return false;
            }
            if( ProfileLoaded ) {
                try {
                    SaveProfile();
                    return true;
                } catch( ApplicationException e ) {
                    MessageBox.Show(GlobalStrings.MainForm_SavingProfileDataFailed + e.Message, GlobalStrings.MainForm_ErrorSavingFile, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

            } else {
                return ManualExport();
            }
        }

        /// <summary>
        /// Gets the selected option on the favorite combo box.
        /// </summary>
        /// <returns>True if set to Yes, false otherwise.</returns>
        bool GetSelectedFavorite() {
            // jpodadera. Changed combo to checkbox to set favorite value
            //return combFavorite.SelectedItem as string == GlobalStrings.MainForm_Yes;
            return chkFavorite.Checked;
        }

        /// <summary>
        /// Checks to see if a game should currently be displayed, based on the state of the category list.
        /// </summary>
        /// <param name="g">Game to check</param>
        /// <returns>True if it should be displayed, false otherwise</returns>
        bool ShouldDisplayGame( Game g ) {
            if( !gameData.Games.ContainsKey( g.Id ) ) {
                return false;
            }
            if( lstCategories.SelectedItem == null ) {
                return false;
            }
            if( lstCategories.SelectedItem is string ) {
                if( (string)lstCategories.SelectedItem == CatUtil.CAT_ALL_NAME ) {
                    return true;
                }
                if( (string)lstCategories.SelectedItem == CatUtil.CAT_FAV_NAME ) {
                    return g.Favorite;
                }
                if( (string)lstCategories.SelectedItem == CatUtil.CAT_UNC_NAME ) {
                    return g.Category == null;
                }
            } else if( lstCategories.SelectedItem is Category ) {
                return g.Category == (Category)lstCategories.SelectedItem;
            }
            return false;
        }

        /// <summary>
        /// Launchs selected game
        /// <param name="g">Game to launch</param>
        /// </summary>
        void LaunchGame( Game g )
        {
            if (g != null)
            {
                System.Diagnostics.Process.Start("steam://rungameid/" + g.Id.ToString());
            }
        }

        #endregion

        void VisitSelectedGameStorePage() {
            if( lstGames.SelectedIndices.Count > 0 ) {
                int index = lstGames.SelectedIndices[0];
                Game g = lstGames.Items[index].Tag as Game;

                if( g != null ) {
                    System.Diagnostics.Process.Start( string.Format( Properties.Resources.UrlSteamStore, g.Id ) );
                }
            }
        }

        private void lstGames_ItemSelectionChanged( object sender, ListViewItemSelectionChangedEventArgs e ) {
            if( e.IsSelected ) {
                Game g = e.Item.Tag as Game;
                if( g != null ) {
                    // jpodadera. Changed combo to checkbox to set favorite value
                    //combFavorite.SelectedIndex = g.Favorite ? 0 : 1;
                    chkFavorite.Checked = !g.Favorite;
                }
            }
        }

        private void launchGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

    }

    /// <summary>
    /// A few constants and quick functions for dealing with categories in the context of the UI
    /// </summary>
    static class CatUtil {
        // Special names shown in the category list
        public static string CAT_ALL_NAME = GlobalStrings.MainForm_All;
        public static string CAT_FAV_NAME = GlobalStrings.MainForm_Favorite;
        public static string CAT_UNC_NAME = GlobalStrings.MainForm_Uncategorized;

        /// <summary>
        /// Checks to see if a category name is valid. Does not make sure it isn't already in use. If the name is not valid, displays a warning.
        /// </summary>
        /// <param name="name">Name to check</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool ValidateCategoryName( string name ) {
            if( name == null || name == string.Empty ) {
                MessageBox.Show(GlobalStrings.MainForm_CategoryNamesNotEmpty, GlobalStrings.DBEditDlg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            } else if( name == CAT_ALL_NAME || name == CAT_FAV_NAME || name == CAT_UNC_NAME ) {
                MessageBox.Show(string.Format(GlobalStrings.MainForm_CategoryNameReserved, name), GlobalStrings.DBEditDlg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            } else {
                return true;
            }
        }

        /// <summary>
        /// Gets a category based on a name. Creates the category if necessary. Displays error message on error.
        /// </summary>
        /// <param name="name">Name of the category to get</param>
        /// <param name="data">Game data object we're referencing</param>
        /// <param name="cat">Resulting category</param>
        /// <returns>True if successful, false otherwise</returns>
        public static bool StringToCategory( string name, GameData data, out Category cat ) {
            cat = null;
            if( string.IsNullOrWhiteSpace( name ) ) {
                return true;
            }
            if( name == CAT_FAV_NAME || name == CAT_ALL_NAME ) {
                MessageBox.Show(string.Format(GlobalStrings.MainForm_CategoryNameReserved, name), GlobalStrings.DBEditDlg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if( name != CAT_UNC_NAME ) {
                cat = data.GetCategory( name );
            }
            return true;
        }
    }
}