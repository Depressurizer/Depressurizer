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
using Rallion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace Depressurizer {

    public enum AdvancedFilterState {
        None = -1,
        Allow = 0,
        Require = 1,
        Exclude = 2
    }

    public partial class FormMain : Form {
        #region Fields

        #region Constants
        const int MAX_FILTER_STATE = 2;
        #endregion

        Profile currentProfile;

        bool unsavedChanges = false;

        StringBuilder statusBuilder = new StringBuilder();

        TypedObjectListView<GameInfo> tlstGames;

        // Allow visual feedback when dragging over the cat list
        bool isDragging;
        int dragOldCat;

        bool ignoreCheckChanges = false;

        // Used to reload resources of main form while switching language
        private int originalWidth, originalHeight, originalSplitDistanceMain, originalSplitDistanceSecondary;

        #region Filter caching fields
        object lastSelectedCat = null;      // Stores last selected category to minimize game list refreshes
        string lastFilterString = "";
        SortedSet<Category> advFilterAllow = new SortedSet<Category>(),
            advFilterRequire = new SortedSet<Category>(),
            advFilterExclude = new SortedSet<Category>();
        AdvancedFilterState advFilterUncatState = AdvancedFilterState.None;
        #endregion

        #region List Backing Field
        private List<GameInfo> displayedGames = new List<GameInfo>();
        #endregion

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

        private bool AdvancedCategoryFilter {
            get { return radCatAdvanced.Checked; }
        }

        #endregion

        #region Init

        public FormMain() {
            InitializeComponent();

            InitializeLstGames();
        }

        /// <summary>
        /// Initializes the lstGames Control.
        /// </summary>
        private void InitializeLstGames()
        {
            tlstGames = new TypedObjectListView<GameInfo>(this.lstGames);
            //Aspect Getters
            tlstGames.GenerateAspectGetters();
            colGameID.AspectToStringConverter = delegate(object obj)
            {
                int id = (int)obj;
                return (id < 0) ? GlobalStrings.MainForm_External : id.ToString();
            };
            colCategories.AspectGetter = delegate(Object g) { return ((GameInfo)g).GetCatString(GlobalStrings.MainForm_Uncategorized); };
            colFavorite.AspectGetter = delegate(Object g) { return ((GameInfo)g).IsFavorite() ? "X" : String.Empty; };
            colHidden.AspectGetter = delegate(Object g) { return ((GameInfo)g).Hidden ? "X" : String.Empty; };
            colGenres.AspectGetter = delegate(Object g)
            {
                int id = ((GameInfo)g).Id;
                if (Program.GameDB.Games.ContainsKey(id) && Program.GameDB.Games[id].Genres != null)
                    return string.Join(", ", Program.GameDB.Games[id].Genres);
                return GlobalStrings.MainForm_NoGenres;
            };
            colFlags.AspectGetter = delegate(Object g)
            {
                int id = ((GameInfo)g).Id;
                if (Program.GameDB.Games.ContainsKey(id) && Program.GameDB.Games[id].Flags != null)
                    return string.Join(", ", Program.GameDB.Games[id].Flags);
                return GlobalStrings.MainForm_NoFlags;
            };
            colTags.AspectGetter = delegate(Object g)
            {
                int id = ((GameInfo)g).Id;
                if (Program.GameDB.Games.ContainsKey(id) && Program.GameDB.Games[id].Tags != null)
                    return string.Join(", ", Program.GameDB.Games[id].Tags);
                return GlobalStrings.MainForm_NoTags;
            };
            colYear.AspectGetter = delegate(object g)
            {
                int id = ((GameInfo)g).Id;
                DateTime releaseDate;
                if (Program.GameDB.Games.ContainsKey(id) && DateTime.TryParse(Program.GameDB.Games[id].SteamReleaseDate, out releaseDate))
                        return releaseDate.Year.ToString();
                return GlobalStrings.MainForm_Unknown;
            };
            colPlatforms.AspectGetter = delegate(Object g) { return Program.GameDB.Games[((GameInfo)g).Id].Platforms.ToString(); };
            colDevelopers.AspectGetter = delegate(Object g)
            {
                int id = ((GameInfo)g).Id;
                if (Program.GameDB.Games.ContainsKey(id) && Program.GameDB.Games[id].Developers != null)
                    return string.Join(", ", Program.GameDB.Games[id].Developers);
                return GlobalStrings.MainForm_Unknown;
            };
            colPublishers.AspectGetter = delegate(Object g)
            {
                int id = ((GameInfo)g).Id;
                if (Program.GameDB.Games.ContainsKey(id) && Program.GameDB.Games[id].Publishers != null)
                    return string.Join(", ", Program.GameDB.Games[id].Publishers);
                return GlobalStrings.MainForm_Unknown;
            };
            colNumberOfReviews.AspectGetter = delegate(object g)
            {
                int id = ((GameInfo)g).Id;
                return Program.GameDB.Games.ContainsKey(id) ? Program.GameDB.Games[id].ReviewTotal : 0;
            };
            colReviewScore.AspectGetter = delegate(object g)
            {
                int id = ((GameInfo)g).Id;
                return Program.GameDB.Games.ContainsKey(id) ? Program.GameDB.Games[id].ReviewPositivePercentage : 0;
            };
            colReviewLabel.AspectGetter = delegate(object g)
            {
                int id = ((GameInfo)g).Id;
                if (Program.GameDB.Games.ContainsKey(id))
                {
                    int reviewTotal = Program.GameDB.Games[id].ReviewTotal;
                    int reviewPositivePercentage = Program.GameDB.Games[id].ReviewPositivePercentage;
                    if (reviewTotal <= 0) return -1;
                    if (reviewPositivePercentage >= 95 && reviewTotal >= 500)
                        return 9;
                    else if (reviewPositivePercentage >= 85 && reviewTotal >= 50)
                        return 8;
                    else if (reviewPositivePercentage >= 80)
                        return 7;
                    else if (reviewPositivePercentage >= 70)
                        return 6;
                    else if (reviewPositivePercentage >= 40)
                        return 5;
                    else if (reviewPositivePercentage >= 20)
                        return 4;
                    else if (reviewTotal >= 500)
                        return 3;
                    else if (reviewTotal >= 50)
                        return 2;
                    else return 1;
                }
                return 0;
            };


            //Aspect to String Converters
            colNumberOfReviews.AspectToStringConverter = delegate(object obj)
            {
                int reviewTotal = (int)obj;
                return (reviewTotal <= 0) ? "0" : reviewTotal.ToString();
            };
            colReviewScore.AspectToStringConverter = delegate(object obj)
            {
                int reviewScore = (int)obj;
                return (reviewScore <= 0) ? GlobalStrings.MainForm_Unknown : reviewScore.ToString() + '%';
            };
            colReviewLabel.AspectToStringConverter = delegate(object obj)
            {
                int index = (int)obj;
                Dictionary<int, String> reviewLabels = new Dictionary<int, String>
           {
                {9, "Overwhelmingly Positive"},
                {8, "Very Positive"},
                {7, "Positive"},
                {6, "Mostly Positive"},
                {5, "Mixed"},
                {4, "Mostly Negative"},
                {3, "Negative"},
                {2, "Very Negative"},
                {1, "Overwhelmingly Negative"},
            };
                return reviewLabels.ContainsKey(index) ? reviewLabels[index] : GlobalStrings.MainForm_Unknown;
            };

            //Filtering
            colCategories.ClusteringStrategy = new CommaClusteringStrategy();
            colGenres.ClusteringStrategy = new CommaClusteringStrategy();
            colFlags.ClusteringStrategy = new CommaClusteringStrategy();
            colTags.ClusteringStrategy = new CommaClusteringStrategy();
            colPlatforms.ClusteringStrategy = new CommaClusteringStrategy();

            //Formating
            lstGames.RowFormatter = delegate(OLVListItem lvi)
            {
                if (((GameInfo)lvi.RowObject).Id < 0)
                    lvi.Font = new Font(lvi.Font, lvi.Font.Style | FontStyle.Italic);
            };

            lstGames.PrimarySortColumn = colTitle;
            lstGames.RestoreState(Convert.FromBase64String(Settings.Instance.LstGamesState));
        }

        private void FormMain_Load( object sender, EventArgs e ) {
            ttHelp.Ext_SetToolTip( helpAdvancedCategories, GlobalStrings.MainForm_Help_AdvancedCategories );

            LoadGameDB();

            // Save original width and height
            originalHeight = this.Height;
            originalWidth = this.Width;
            originalSplitDistanceMain = this.splitContainer.SplitterDistance;
            originalSplitDistanceSecondary = this.splitGame.SplitterDistance;

            ClearStatus();
            if( Settings.Instance.SteamPath == null ) {
                DlgSteamPath dlg = new DlgSteamPath();
                dlg.ShowDialog();
                Settings.Instance.SteamPath = dlg.Path;
                Settings.Instance.Save();
            }
            if( Settings.Instance.UpdateAppInfoOnStart ) {
                UpdateGameDBFromAppInfo();
            }

            switch( Settings.Instance.StartupAction ) {
                case StartupAction.Load:
                    LoadProfile( Settings.Instance.ProfileToLoad, false );
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
        }

        #endregion

        #region DB Operations

        /// <summary>
        /// Loads the database from disk. If the load fails, displays a message box and creates an empty DB.
        /// </summary>
        private void LoadGameDB() {
            try {
                Program.GameDB = new GameDB();
                if( File.Exists( "GameDB.xml.gz" ) ) {
                    Program.GameDB.Load( "GameDB.xml.gz" );
                } else if( File.Exists( "GameDB.xml" ) ) {
                    Program.GameDB.Load( "GameDB.xml" );
                } else {
                    MessageBox.Show( GlobalStrings.MainForm_ErrorLoadingGameDB + GlobalStrings.MainForm_GameDBFileNotExist );
                    Program.Logger.Write( LoggerLevel.Warning, GlobalStrings.MainForm_GameDBFileNotExist );
                }
            } catch( Exception ex ) {
                MessageBox.Show( GlobalStrings.MainForm_ErrorLoadingGameDB + ex.Message );
                Program.Logger.WriteException( GlobalStrings.MainForm_Log_ExceptionOnDBLoad, ex );
                Program.GameDB = new GameDB();
            }
        }

        /// <summary>
        /// Saves the current database to disk. Displays a message box on failure.
        /// </summary>
        private void SaveGameDB() {
            try {
                Program.GameDB.Save( "GameDB.xml.gz" );
                AddStatus( GlobalStrings.MainForm_Status_SavedDB );
            } catch( Exception e ) {
                Program.Logger.WriteException( GlobalStrings.MainForm_Log_ExceptionAutosavingDB, e );
                MessageBox.Show( string.Format( GlobalStrings.MainForm_Msg_ErrorAutosavingDB, e.Message ), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        /// <summary>
        /// Updates the database using AppInfo cache. Displays an error message on failure. Saves the DB afterwards if AutosaveDB is set.
        /// </summary>
        private void UpdateGameDBFromAppInfo() {
            try {
                int num = Program.GameDB.UpdateFromAppInfo( string.Format( Properties.Resources.AppInfoPath, Settings.Instance.SteamPath ) );
                AddStatus( string.Format( GlobalStrings.MainForm_Status_AppInfoAutoupdate, num ) );
                if( num > 0 && Settings.Instance.AutosaveDB ) {
                    SaveGameDB();
                }
            } catch( Exception e ) {
                Program.Logger.WriteException( GlobalStrings.MainForm_Log_ExceptionAppInfo, e );
                MessageBox.Show( GlobalStrings.MainForm_Msg_ErrorAppInfo, e.Message );
            }
        }

        #endregion

        #region Profile Operations

        /// <summary>
        /// Prompts user to create a new profile.
        /// </summary>
        void CreateProfile() {
            DlgProfile dlg = new DlgProfile();
            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                Cursor = Cursors.WaitCursor;
                currentProfile = dlg.Profile;
                AddStatus( GlobalStrings.MainForm_ProfileCreated );
                if( dlg.DownloadNow ) {
                    UpdateLibrary();
                }

                if( dlg.ImportNow ) {
                    ImportConfig();
                }
                if( dlg.SetStartup ) {
                    Settings.Instance.StartupAction = StartupAction.Load;
                    Settings.Instance.ProfileToLoad = currentProfile.FilePath;
                    Settings.Instance.Save();
                }

                FullListRefresh();

                Cursor = Cursors.Default;
            }
            OnProfileChange();
        }

        /// <summary>
        /// Prompts the user to modify the currently loaded profile. If there isn't one, asks if the user would like to create one.
        /// </summary>
        void EditProfile() {
            if( ProfileLoaded ) {
                DlgProfile dlg = new DlgProfile( currentProfile );
                if( dlg.ShowDialog() == DialogResult.OK ) {
                    AddStatus( GlobalStrings.MainForm_ProfileEdited );
                    MakeChange( true );
                    Cursor = Cursors.WaitCursor;
                    bool refresh = false;
                    if( dlg.DownloadNow ) {
                        UpdateLibrary();
                        refresh = true;
                    }
                    if( dlg.ImportNow ) {
                        ImportConfig();
                        refresh = true;
                    }
                    if( dlg.SetStartup ) {
                        Settings.Instance.StartupAction = StartupAction.Load;
                        Settings.Instance.ProfileToLoad = currentProfile.FilePath;
                        Settings.Instance.Save();
                    }
                    Cursor = Cursors.Default;
                    if( refresh ) {
                        FullListRefresh();
                    }
                }
            } else {
                if( MessageBox.Show( GlobalStrings.MainForm_NoProfileLoaded, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.YesNo, MessageBoxIcon.Warning ) == DialogResult.Yes ) {
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
                AddStatus( GlobalStrings.MainForm_ProfileLoaded );
            } catch( ApplicationException e ) {
                MessageBox.Show( string.Format( GlobalStrings.MainForm_Msg_ErrorLoadingProfile, e.Message ), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Warning );
                Program.Logger.WriteException( GlobalStrings.MainForm_Log_ExceptionLoadingProfile, e );
                OnProfileChange();
                AddStatus( GlobalStrings.MainForm_FailedLoadProfile );
                return;
            }

            if( currentProfile.AutoUpdate ) {
                UpdateLibrary();
            }
            if( currentProfile.AutoImport ) {
                ImportConfig();
            }

            FullListRefresh();

            OnProfileChange();
        }

        /// <summary>
        /// Prompts user for a file location and saves profile
        /// </summary>
        void SaveProfileAs() {
            if( !ProfileLoaded ) return;
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
            if( !ProfileLoaded ) return false;
            if( currentProfile.AutoExport ) {
                ExportConfig();
            }
            Settings.Instance.LstGamesState = Convert.ToBase64String(lstGames.SaveState());
            try {
                if( path == null ) {
                    currentProfile.Save();
                } else {
                    currentProfile.Save( path );
                }
                AddStatus( GlobalStrings.MainForm_ProfileSaved );
                MakeChange( false );
                return true;
            } catch( ApplicationException e ) {
                MessageBox.Show( string.Format( GlobalStrings.MainForm_Msg_ErrorSavingProfile, e.Message ), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Error );
                Program.Logger.WriteException( GlobalStrings.MainForm_Log_ExceptionSavingProfile, e );
                AddStatus( GlobalStrings.MainForm_FailedSaveProfile );
                return false;
            }

        }

        /// <summary>
        /// Updates the game list for the loaded profile.
        /// </summary>
        void UpdateLibrary() {
            if( currentProfile == null ) return;

            Cursor = Cursors.WaitCursor;

            bool success = false;

            // First, try to update via local config files, if they're enabled
            if( currentProfile.LocalUpdate ) {
                try {
                    int newApps = 0;
                    AppTypes appFilter = currentProfile.IncludeUnknown ? AppTypes.InclusionUnknown : AppTypes.InclusionNormal;
                    int totalApps = currentProfile.GameData.UpdateGameListFromOwnedPackageInfo( currentProfile.SteamID64, currentProfile.IgnoreList, appFilter, out newApps );
                    AddStatus( string.Format( GlobalStrings.MainForm_Status_LocalUpdate, totalApps, newApps ) );
                    success = true;
                } catch( Exception e ) {
                    MessageBox.Show( string.Format( GlobalStrings.MainForm_Msg_LocalUpdateError, e.Message ), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Warning );
                    Program.Logger.WriteException( GlobalStrings.MainForm_Log_ExceptionLocalUpdate, e );
                    AddStatus( GlobalStrings.MainForm_Status_LocalUpdateFailed );
                    success = false;
                }
            }
            if( success ) {
                MakeChange( true );
                FullListRefresh();
            } else if( currentProfile.WebUpdate ) {
                try {
                    CDlgUpdateProfile updateDlg = new CDlgUpdateProfile( currentProfile.GameData, currentProfile.SteamID64, currentProfile.OverwriteOnDownload, currentProfile.IgnoreList, currentProfile.IncludeUnknown );
                    DialogResult res = updateDlg.ShowDialog();

                    if( updateDlg.Error != null ) {
                        Program.Logger.WriteException( GlobalStrings.MainForm_Log_ExceptionWebUpdateDialog, updateDlg.Error );
                        AddStatus( string.Format( GlobalStrings.MainForm_ErrorDownloadingProfileData, updateDlg.UseHtml ? "HTML" : "XML" ) );
                        MessageBox.Show( string.Format( GlobalStrings.MainForm_ErrorDowloadingProfile, updateDlg.Error.Message ), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error );
                    } else {
                        if( res == DialogResult.Abort || res == DialogResult.Cancel ) {
                            AddStatus( GlobalStrings.MainForm_DownloadAborted );
                        } else {
                            if( updateDlg.Failover ) {
                                AddStatus( GlobalStrings.MainForm_XMLDownloadFailed );
                            }
                            if( updateDlg.Fetched == 0 ) {
                                MessageBox.Show( GlobalStrings.MainForm_NoGameDataFound, GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning );
                                AddStatus( GlobalStrings.MainForm_NoGamesInDownload );
                            } else {
                                MakeChange( true );
                                AddStatus( string.Format( GlobalStrings.MainForm_DownloadedGames, updateDlg.Fetched, updateDlg.Added, updateDlg.UseHtml ? "HTML" : "XML" ) );
                                FullListRefresh();
                            }
                        }
                    }
                } catch( Exception e ) {
                    Program.Logger.WriteException( GlobalStrings.MainForm_Log_ExceptionWebUpdate, e );
                    MessageBox.Show( string.Format( GlobalStrings.MainForm_ErrorDowloadingProfile, e.Message ), GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error );
                    AddStatus( GlobalStrings.MainForm_DownloadFailed );
                }
            }

            Cursor = Cursors.Default;

        }

        /// <summary>
        /// Attempts to import steam categories
        /// </summary>
        void ImportConfig() {
            if( !ProfileLoaded ) return;
            Cursor = Cursors.WaitCursor;
            try {
                int count = currentProfile.ImportSteamData();
                AddStatus( string.Format( GlobalStrings.MainForm_ImportedItems, count ) );
                if( count > 0 ) {
                    MakeChange( true );
                    FullListRefresh();
                }
            } catch( Exception e ) {
                MessageBox.Show( string.Format( GlobalStrings.MainForm_ErrorImportingSteamDataList, e.Message ), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Warning );
                Program.Logger.WriteException( "Exception encountered while importing the remoteconfig file.", e );
                AddStatus( GlobalStrings.MainForm_ImportFailed );

            }
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Attempts to export steam categories
        /// </summary>
        void ExportConfig() {
            if( currentProfile != null ) {
                try {
                    currentProfile.ExportSteamData();
                    AddStatus( GlobalStrings.MainForm_ExportedCategories );
                } catch( Exception e ) {
                    MessageBox.Show( string.Format( GlobalStrings.MainForm_Msg_ErrorExportingToSteam, e.Message ), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Error );
                    Program.Logger.WriteException( GlobalStrings.MainForm_Log_ExceptionExport, e );
                    AddStatus( GlobalStrings.MainForm_ExportFailed );
                }
            }
        }

        /// <summary>
        /// Saves a Steam configuration file. Asks the user to select the file to save as.
        /// </summary>
        /// <returns>True if save was completed, false otherwise</returns>
        void ManualExportConfig() {
            if( currentProfile == null ) return;

            SaveFileDialog dlg = new SaveFileDialog();
            DialogResult res = dlg.ShowDialog();
            if( res == DialogResult.OK ) {
                Cursor = Cursors.WaitCursor;
                try {
                    currentProfile.GameData.ExportSteamConfigFile( dlg.FileName, Settings.Instance.RemoveExtraEntries );
                    AddStatus( GlobalStrings.MainForm_DataExported );
                } catch( Exception e ) {
                    MessageBox.Show( string.Format( GlobalStrings.MainForm_Msg_ErrorManualExport, e.Message ), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Error );
                    Program.Logger.WriteException( GlobalStrings.MainForm_Log_ExceptionExport, e );
                    AddStatus( GlobalStrings.MainForm_ExportFailed );
                }
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Creates an Edit AutoCats dialog for the user
        /// </summary>
        private void EditAutoCats() {
            if( !ProfileLoaded ) return;
            DlgAutoCat dlg = new DlgAutoCat( currentProfile.AutoCats, currentProfile.GameData );

            DialogResult res = dlg.ShowDialog();

            if( res == DialogResult.OK ) {
                currentProfile.AutoCats = dlg.AutoCatList;
                MakeChange( true );
                FillAutoCatLists();
            }
        }

        #endregion

        #region Data modifiers

        /// <summary>
        /// Creates a new category, first prompting the user for the name to use. If the name is not valid or in use, displays a notification.
        /// </summary>
        /// <returns>The category that was added, or null if the operation was canceled or failed.</returns>
        Category CreateCategory() {
            if( !ProfileLoaded ) return null;

            GetStringDlg dlg = new GetStringDlg( string.Empty, GlobalStrings.MainForm_CreateCategory, GlobalStrings.MainForm_EnterNewCategoryName, GlobalStrings.MainForm_Create );
            if( dlg.ShowDialog() == DialogResult.OK && ValidateCategoryName( dlg.Value ) ) {
                Category newCat = currentProfile.GameData.AddCategory( dlg.Value );
                if( newCat != null ) {
                    OnCategoryChange();
                    MakeChange( true );
                    AddStatus( string.Format( GlobalStrings.MainForm_CategoryAdded, newCat.Name ) );
                    return newCat;
                } else {
                    MessageBox.Show( String.Format( GlobalStrings.MainForm_CouldNotAddCategory, dlg.Value ), GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                }
            }
            return null;
        }

        /// <summary>
        /// Deletes the selected categories and updates the UI. Prompts user for confirmation. Will completely rebuild the gamelist.
        /// </summary>
        void DeleteCategory() {
            List<Category> toDelete = new List<Category>();
            foreach( ListViewItem item in lstCategories.SelectedItems ) {
                Category c = item.Tag as Category;
                if( c != null && c != currentProfile.GameData.FavoriteCategory ) {
                    toDelete.Add( c );
                }
            }
            if( toDelete.Count > 0 ) {
                DialogResult res;
                if( toDelete.Count == 1 ) {
                    res = MessageBox.Show( string.Format( GlobalStrings.MainForm_DeleteCategory, toDelete[0].Name ), GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning );
                } else {
                    res = MessageBox.Show( string.Format( GlobalStrings.MainForm_DeleteCategoryMulti, toDelete.Count ), GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning );
                }
                if( res == System.Windows.Forms.DialogResult.Yes ) {
                    int deleted = 0;
                    foreach( Category c in toDelete ) {
                        if( currentProfile.GameData.RemoveCategory( c ) ) {
                            deleted++;
                        }
                    }
                    if( deleted > 0 ) {
                        FullListRefresh();
                        MakeChange( true );
                        AddStatus( string.Format( GlobalStrings.MainForm_CategoryDeleted, deleted ) );
                    } else {
                        MessageBox.Show( string.Format( GlobalStrings.MainForm_CouldNotDeleteCategory ), GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                    }
                }
            }
        }

        /// <summary>
        /// Renames the given category. Prompts user for a new name. Updates UI. Will display an error if the rename fails.
        /// </summary>
        /// <param name="c">Category to rename</param>
        /// <returns>True if category was renamed, false otherwise.</returns>
        bool RenameCategory() {
            if( lstCategories.SelectedItems.Count > 0 ) {
                Category c = lstCategories.SelectedItems[0].Tag as Category;
                if( c != null && c != currentProfile.GameData.FavoriteCategory ) {
                    GetStringDlg dlg = new GetStringDlg( c.Name, string.Format( GlobalStrings.MainForm_RenameCategory, c.Name ), GlobalStrings.MainForm_EnterNewName, GlobalStrings.MainForm_Rename );
                    if( dlg.ShowDialog() == DialogResult.OK ) {
                        string newName = dlg.Value;
                        if( newName == c.Name ) return true;
                        if( ValidateCategoryName( newName ) ) {
                            Category newCat = currentProfile.GameData.RenameCategory( c, newName );
                            if( newCat != null ) {
                                OnCategoryChange();
                                MakeChange( true );
                                for( int index = 2; index < lstCategories.Items.Count; index++ ) {
                                    if( lstCategories.Items[index].Tag == newCat ) {
                                        lstCategories.SelectedIndices.Add( index );
                                        break;
                                    }
                                }
                                AddStatus( string.Format( GlobalStrings.MainForm_CategoryRenamed, c.Name ) );
                                return true;
                            }
                        }
                        MessageBox.Show( string.Format( GlobalStrings.MainForm_NameIsInUse, newName ), GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning );
                        return false;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Adds a new game. Displays the game dialog to the user.
        /// </summary>
        void AddGame() {
            DlgGame dlg = new DlgGame( currentProfile.GameData, null );
            if( dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK ) {
                if( ProfileLoaded ) {
                    if( currentProfile.IgnoreList.Remove( dlg.Game.Id ) ) {
                        AddStatus( string.Format( GlobalStrings.MainForm_UnignoredGame, dlg.Game.Id ) );
                    }
                }
                FullListRefresh();
                MakeChange( true );
                AddStatus( GlobalStrings.MainForm_AddedGame );
            }
        }

        /// <summary>
        /// Edits the first selected game. Displays game dialog.
        /// </summary>
        void EditGame() {
            if (lstGames.SelectedObjects.Count > 0)
            {
                GameInfo g = tlstGames.SelectedObjects[0];
                DlgGame dlg = new DlgGame( currentProfile.GameData, g );
                if( dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK ) {
                    OnCategoryChange();
                    MakeChange( true );
                    AddStatus( GlobalStrings.MainForm_EditedGame );
                }
            }
        }

        /// <summary>
        /// Removes all selected games. Prompts for confirmation.
        /// </summary>
        void RemoveGames() {
            int selectCount = lstGames.SelectedObjects.Count;
            if( selectCount > 0 ) {
                if( MessageBox.Show( string.Format( GlobalStrings.MainForm_RemoveGame, selectCount, ( selectCount == 1 ) ? "" : "s" ), GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question )
                    == DialogResult.Yes ) {
                    int ignored = 0;
                    int removed = 0;
                    foreach (GameInfo g in tlstGames.SelectedObjects)
                    {
                        if( currentProfile.GameData.Games.Remove( g.Id ) ) {
                            removed++;
                        }
                        if( ProfileLoaded && currentProfile.AutoIgnore ) {
                            if( currentProfile.IgnoreList.Add( g.Id ) ) {
                                ignored++;
                            }
                        }
                    }
                    if( removed > 0 ) {
                        AddStatus( string.Format( GlobalStrings.MainForm_RemovedGame, removed, ( removed == 1 ) ? "" : "s" ) );
                        MakeChange( true );
                    }
                    if( ignored > 0 ) {
                        AddStatus( string.Format( GlobalStrings.MainForm_IgnoredGame, ignored, ( ignored == 1 ) ? "" : "s" ) );
                        MakeChange( true );
                    }
                    OnCategoryChange();
                }
            }
        }

        /// <summary>
        /// Adds the given category to all selected games.
        /// </summary>
        /// <param name="cat">Category to add</param>
        /// <param name="forceClearOthers">If true, remove other categories from the affected games.</param>
        void AddCategoryToSelectedGames( Category cat, bool forceClearOthers ) {
            if (lstGames.SelectedObjects.Count > 0)
            {
                foreach( GameInfo g in tlstGames.SelectedObjects ) {
                    if( g != null ) {
                        if( forceClearOthers || Settings.Instance.SingleCatMode ) {
                            g.ClearCategories( alsoClearFavorite: false );
                            if( cat != null ) {
                                g.AddCategory( cat );
                            }
                        } else {
                            g.AddCategory( cat );
                        }
                    }
                }
                OnCategoryChange();
                MakeChange( true );
            }
        }

        /// <summary>
        /// Removes the given category from all selected games.
        /// </summary>
        /// <param name="cat">Category to remove.</param>
        void RemoveCategoryFromSelectedGames( Category cat ) {
            if (lstGames.SelectedObjects.Count > 0)
            {
                foreach (GameInfo g in tlstGames.SelectedObjects)
                {
                    g.RemoveCategory( cat );
                }
                OnCategoryChange();
                MakeChange( true );
            }
        }

        /// <summary>
        /// Assigns the given favorite state to all selected items in the game list.
        /// </summary>
        /// <param name="fav">True to turn fav on, false to turn it off.</param>
        void AssignFavoriteToSelectedGames( bool fav ) {
            if (lstGames.SelectedObjects.Count > 0)
            {
                foreach (GameInfo g in tlstGames.SelectedObjects)
                {
                    g.SetFavorite( fav );
                }
                OnCategoryChange();
                MakeChange( true );
            }
        }

        /// <summary>
        /// Add or remove the hidden attribute to the selected games
        /// </summary>
        /// <param name="hidden">Whether the games should be hidden</param>
        void AssignHiddenToSelectedGames( bool hidden ) {
            if (lstGames.SelectedObjects.Count > 0)
            {
                foreach (GameInfo g in tlstGames.SelectedObjects)
                {
                    g.Hidden = hidden;
                }
                OnCategoryChange();
                MakeChange( true );
            }
        }

        /// <summary>
        /// Unloads the current profile or game list, making sure the user gets the option to save any changes.
        /// </summary>
        /// <returns>True if there is now no loaded profile, false otherwise.</returns>
        void Unload() {
            if( !CheckForUnsaved() ) return;
            AddStatus( GlobalStrings.MainForm_ClearedData );
            currentProfile = null;
            MakeChange( false );
            OnProfileChange();
            FullListRefresh();
        }

        /// <summary>
        /// Autocategorizes a set of games.
        /// </summary>
        /// <param name="selectedOnly">If true, runs on the selected games, otherwise, runs on all games.</param>
        /// <param name="autoCat">The autocat object to use.</param>
        private void Autocategorize( bool selectedOnly, AutoCat autoCat ) {
            if( autoCat == null ) return;

            // Get a list of games to update
            List<GameInfo> gamesToUpdate = new List<GameInfo>();

            if( selectedOnly ) {
                foreach (GameInfo g in tlstGames.SelectedObjects)
                {
                    if( g.Id > 0 ) {
                        gamesToUpdate.Add( g );
                    }
                }
            } else {
                foreach( GameInfo g in currentProfile.GameData.Games.Values ) {
                    if( ( g != null ) && ( g.Id > 0 ) ) {
                        gamesToUpdate.Add( g );
                    }
                }
            }

            int updated = 0;

            // List of games not found in database, so we can try to scrape data for them
            Queue<int> notInDb = new Queue<int>();
            foreach( GameInfo game in gamesToUpdate ) {
                if( game.Id > 0 && ( !Program.GameDB.Contains( game.Id ) || Program.GameDB.Games[game.Id].LastStoreScrape == 0 ) ) {
                    notInDb.Enqueue( game.Id );
                }
            }

            if( notInDb.Count > 0 ) {
                if( MessageBox.Show( string.Format( GlobalStrings.MainForm_GamesNotFoundInGameDB, notInDb.Count ), GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1 )
                        == System.Windows.Forms.DialogResult.Yes ) {

                    DbScrapeDlg scrapeDlg = new DbScrapeDlg( notInDb );
                    DialogResult scrapeRes = scrapeDlg.ShowDialog();

                    if( scrapeRes == System.Windows.Forms.DialogResult.Cancel ) {
                        AddStatus( string.Format( GlobalStrings.MainForm_CanceledDatabaseUpdate ) );
                    } else {
                        AddStatus( string.Format( GlobalStrings.MainForm_UpdatedDatabaseEntries, scrapeDlg.JobsCompleted ) );
                        if( scrapeDlg.JobsCompleted > 0 && Settings.Instance.AutosaveDB ) {
                            SaveGameDB();
                        }
                    }
                }
            }

            autoCat.PreProcess( currentProfile.GameData, Program.GameDB );

            foreach( GameInfo g in gamesToUpdate ) {
                AutoCatResult res = autoCat.CategorizeGame( g );
                if( res == AutoCatResult.Success ) {
                    updated++;
                }
            }

            autoCat.DeProcess();
            AddStatus( string.Format( GlobalStrings.MainForm_UpdatedCategories, updated ) );
            if( gamesToUpdate.Count > updated ) AddStatus( string.Format( GlobalStrings.MainForm_FailedToUpdate, gamesToUpdate.Count - updated ) );
            if( updated > 0 ) MakeChange( true );
            FullListRefresh();
        }

        /// <summary>
        /// Renames all games with names from the database.
        /// </summary>
        private void AutonameAll() {
            DialogResult res = MessageBox.Show( GlobalStrings.MainForm_OverwriteExistingNames, GlobalStrings.MainForm_Overwrite, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2 );
            bool overwrite = false;

            if( res == DialogResult.Cancel ) {
                AddStatus( GlobalStrings.MainForm_AutonameCanceled );
                return;
            } else if( res == DialogResult.Yes ) {
                overwrite = true;
            }

            int named = 0;
            foreach( GameInfo g in currentProfile.GameData.Games.Values ) {
                if( overwrite || string.IsNullOrEmpty( g.Name ) ) {
                    g.Name = Program.GameDB.GetName( g.Id );
                    named++;
                }
            }
            AddStatus( string.Format( GlobalStrings.MainForm_AutonamedGames, named ) );
            if( named > 0 ) {
                MakeChange( true );
            }

            UpdateGameList();
        }

        /// <summary>
        /// Removes any categories with no games assigned.
        /// </summary>
        void RemoveEmptyCats() {
            int count = currentProfile.GameData.RemoveEmptyCategories();
            AddStatus( string.Format( GlobalStrings.MainForm_RemovedEmptyCategories, count ) );
            OnCategoryChange();
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
            statusSelection.Text = string.Format(GlobalStrings.MainForm_SelectedDisplayed, lstGames.SelectedObjects.Count, lstGames.GetItemCount());
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
            if( Settings.Instance.SingleCatMode ) {
                sb.Append( " [" );
                sb.Append( GlobalStrings.MainForm_SingleCategoryMode );
                sb.Append( "]" );
            }
            if( unsavedChanges ) {
                sb.Append( " *" );
            }
            this.Text = sb.ToString();
        }

        #endregion
        #region List updaters

        /// <summary>
        /// Does all list-updating that should be done when adding, removing, or renaming a category.
        /// Also does all list-updating needed when a game is added or removed from a category.
        /// </summary>
        private void OnCategoryChange() {
            FillAllCategoryLists();

            UpdateGameList();
        }

        /// <summary>
        /// Does all list-updating that should be done when modifying one or more existing game entry.
        /// </summary>
        /// <param name="catCreationPossible">True if it's possible that a new category was added for the game.</param>
        /// <param name="limitToSelection">If true, only update entries for selected games instead of all of them</param>
        private void OnGameChange( bool catCreationPossible ) {
            if( catCreationPossible ) {
                OnCategoryChange();
            } else {
                UpdateGameList();
            }
        }

        /// <summary>
        /// Does all list updating that's required if the filter changes (category selection changes).
        /// </summary>
        private void OnViewChange() {
            FillGameList();
        }

        /// <summary>
        /// Completely regenerates both the category and game lists
        /// </summary>
        private void FullListRefresh() {
            FillAllCategoryLists();
            FillGameList();
        }

        /// <summary>
        /// Completely re-populates the game list based on the current category selection.
        /// Try to avoid calling this directly. Look at OnCategoryChange, OnGameChange, OnViewChange, and FullListRefresh.
        /// </summary>
        private void FillGameList() {
            lstGames.BeginUpdate();
            SortedSet<int> selectedIds = GetSelectedGameIds();

            displayedGames.Clear();
            if( currentProfile != null ) {
                foreach( GameInfo g in currentProfile.GameData.Games.Values ) {
                    if( ShouldDisplayGame( g ) ) {
                        displayedGames.Add(g);
                    }
					if ( g.Name == null ) {
						g.Name = string.Empty;
                        displayedGames.Add(g);
					}
                }
            }

            this.lstGames.SetObjects(displayedGames);

           lstGames.BuildList();

           SelectGameSet(selectedIds);

           lstGames.EndUpdate();
        }

        /// <summary>
        /// Completely repopulates the category list and combobox. Maintains selection on both.
        /// Try to avoid calling this directly. Look at OnCategoryChange, OnGameChange, OnViewChange, and FullListRefresh.
        /// </summary>
        private void FillAllCategoryLists() {
            object selected = ( lstCategories.SelectedItems.Count > 0 ) ? lstCategories.SelectedItems[0].Tag : null;
            int selectedIndex = ( lstCategories.SelectedItems.Count > 0 ) ? lstCategories.SelectedIndices[0] : -1;

            lstCategories.Items.Clear();
            contextGameAddCat.Items.Clear();
            contextGameAddCat.Items.Add( contextGameAddCat_Create );
            contextGameRemCat.Items.Clear();
            lstMultiCat.Items.Clear();

            if( !ProfileLoaded ) return;

            currentProfile.GameData.Categories.Sort();

            lstCategories.BeginUpdate();
            lstCategories.Items.Clear();
            if( !AdvancedCategoryFilter ) {
                ListViewItem i = new ListViewItem(GlobalStrings.MainForm_All + " (" + currentProfile.GameData.Games.Count + ")");
                i.Tag = GlobalStrings.MainForm_All;
                lstCategories.Items.Add(i);
            }
            
            int count = 0;
            foreach (GameInfo g in currentProfile.GameData.Games.Values)
            {
                if (!g.HasCategories())
                    count++;
            }
            ListViewItem lvi = new ListViewItem(GlobalStrings.MainForm_Uncategorized + " (" + count + ")");
            lvi.Tag = GlobalStrings.MainForm_Uncategorized;
            lstCategories.Items.Add( lvi );

            foreach( Category c in currentProfile.GameData.Categories ) {
                lstCategories.Items.Add( CreateCategoryListViewItem( c ) );
            }

            if( selected == null ) {
                    lstCategories.SelectedIndices.Add( 0 );
            } else {
                for( int i = 0; i < lstCategories.Items.Count; i++ ) {
                    if( lstCategories.Items[i].Tag == selected ) {
                        lstCategories.SelectedIndices.Add( i );
                        break;
                    }
                }
            }
            lstCategories.EndUpdate();

            lstMultiCat.BeginUpdate();
            foreach( Category c in currentProfile.GameData.Categories ) {
                if( c != currentProfile.GameData.FavoriteCategory ) {
                    ToolStripItem item = contextGame_AddCat.DropDownItems.Add( c.Name );
                    item.Tag = c;
                    item.Click += contextGameAddCat_Category_Click;

                    item = contextGameRemCat.Items.Add( c.Name );
                    item.Tag = c;
                    item.Click += contextGameRemCat_Category_Click;

                    ListViewItem listItem = new ListViewItem( c.Name );
                    listItem.Tag = c;
                    listItem.StateImageIndex = 0;
                    lstMultiCat.Items.Add( listItem );
                }
            }

            UpdateGameCheckStates();
            lstMultiCat.EndUpdate();
        }

        private ListViewItem CreateCategoryListViewItem( Category c )
        {
            int count=0;
            foreach (GameInfo g in currentProfile.GameData.Games.Values)
            {
                if (g.ContainsCategory(c))
                    count++;
            }
            ListViewItem i = new ListViewItem(c.Name + " (" + count + ")");
            i.Tag = c;
            return i;
        }

        void UpdateGameCheckStates() {
            lstMultiCat.BeginUpdate();
            bool first = true;
            foreach( ListViewItem item in lstMultiCat.Items ) {
                item.StateImageIndex = 0;
            }

            if (lstGames.SelectedObjects.Count == 0)
            {
                lstMultiCat.Enabled = false;
            } else {
                lstMultiCat.Enabled = true;
                foreach (GameInfo game in tlstGames.SelectedObjects)
                {
                    if( game != null ) {
                        AddGameToMultiCatCheckStates( game, first );
                        AddGameToCheckboxStates( game, first );
                        first = false;
                    }
                }
            }
            lstMultiCat.EndUpdate();
        }

        void AddGameToMultiCatCheckStates( GameInfo game, bool first ) {
            foreach( ListViewItem catItem in lstMultiCat.Items ) {
                if( catItem.StateImageIndex != 2 ) {
                    Category cat = catItem.Tag as Category;
                    if( cat != null ) {
                        if( first ) {
                            catItem.StateImageIndex = game.ContainsCategory( cat ) ? 1 : 0;
                        } else {
                            if( game.ContainsCategory( cat ) ) {
                                if( catItem.StateImageIndex == 0 ) catItem.StateImageIndex = 2;
                            } else {
                                if( catItem.StateImageIndex == 1 ) catItem.StateImageIndex = 2;
                            }
                        }
                    }
                }
            }
        }

        void AddGameToCheckboxStates( GameInfo game, bool first ) {
            ignoreCheckChanges = true;
            if( first ) {
                chkFavorite.CheckState = game.IsFavorite() ? CheckState.Checked : CheckState.Unchecked;
                chkHidden.CheckState = game.Hidden ? CheckState.Checked : CheckState.Unchecked;
            } else {
                if( chkFavorite.CheckState != CheckState.Indeterminate ) {
                    if( game.IsFavorite() ) {
                        if( chkFavorite.CheckState == CheckState.Unchecked ) chkFavorite.CheckState = CheckState.Indeterminate;
                    } else {
                        if( chkFavorite.CheckState == CheckState.Checked ) chkFavorite.CheckState = CheckState.Indeterminate;
                    }
                }
                if( game.Hidden ) {
                    if( chkHidden.CheckState == CheckState.Unchecked ) chkHidden.CheckState = CheckState.Indeterminate;
                } else {
                    if( chkHidden.CheckState == CheckState.Checked ) chkHidden.CheckState = CheckState.Indeterminate;
                }
            }
            ignoreCheckChanges = false;
        }

        /// <summary>
        /// Updates list item for every game on the list, removing games that no longer need to be there, but not adding new ones.
        /// Try to avoid calling this directly. Look at OnCategoryChange, OnGameChange, OnViewChange, and FullListRefresh.
        /// </summary>
        void UpdateGameList() {
            SortedSet<int> selectedIds = GetSelectedGameIds();

            displayedGames.RemoveAll( ShouldHideGame );
            lstGames.SetObjects(displayedGames);
            lstGames.BuildList();

            SelectGameSet(selectedIds);
        }

        private SortedSet<int> GetSelectedGameIds()
        {
            SortedSet<int> selectedGameIds = new SortedSet<int>();
            foreach (GameInfo g in tlstGames.SelectedObjects)
            {
                selectedGameIds.Add(g.Id);
            }
            return selectedGameIds;
        }

        private void SelectGameSet(SortedSet<int> selectedGameIds)
        {
            lstGames.DeselectAll();
            List<GameInfo> stillSelected = new List<GameInfo>();
            foreach (GameInfo g in tlstGames.Objects)
            {
                if (selectedGameIds.Contains(g.Id)) 
                    stillSelected.Add(g);
            }
            lstGames.SelectedObjects = stillSelected;
        }

        private bool ShouldHideGame( GameInfo g ) {
            return !ShouldDisplayGame( g );
        }

        void FillAutoCatLists() {
            // Prepare main screen AutoCat dropdown
            object selected = cmbAutoCatType.SelectedItem;
            cmbAutoCatType.Items.Clear();

            // Prepare main menu list
            menu_Tools_Autocat_List.Items.Clear();

            if( currentProfile != null ) {
                foreach( AutoCat ac in currentProfile.AutoCats ) {
                    if( ac != null ) {
                        // Fill main screen dropdown
                        cmbAutoCatType.Items.Add( ac );

                        // Fill main menu list
                        ToolStripItem item = menu_Tools_Autocat_List.Items.Add( ac.Name );
                        item.Tag = ac;
                        item.Click += menuToolsAutocat_Item_Click;
                    }
                }
            }

            // Finish main screen dropdown
            if( selected != null && cmbAutoCatType.Items.Contains( selected ) ) {
                cmbAutoCatType.SelectedItem = selected;
            } else if( cmbAutoCatType.Items.Count > 0 ) {
                cmbAutoCatType.SelectedIndex = 0;
            }

            // Finish main menu list
            menu_Tools_AutocatAll.Enabled = menu_Tools_Autocat_List.Items.Count > 0;
        }

        #endregion
        #region UI State updaters

        /// <summary>
        /// Updates UI after a profile is created, loaded, modified or closed.
        /// </summary>
        void OnProfileChange() {
            bool enable = ProfileLoaded;
            menu_File_SaveProfile.Enabled = enable;
            menu_File_SaveProfileAs.Enabled = enable;
            menu_File_Close.Enabled = enable;

            menu_Profile_Update.Enabled = enable;
            menu_Profile_Export.Enabled = enable;
            menu_Profile_Import.Enabled = enable;
            menu_Profile_Edit.Enabled = enable;
            menu_Profile_AutoCats.Enabled = enable;

            cmdCatAdd.Enabled = enable;
            cmdCatDelete.Enabled = enable;
            cmdCatRename.Enabled = enable;

            cmdGameAdd.Enabled = enable;
            contextGame_Add.Enabled = enable;


            UpdateEnabledStatesForGames();
            FillAutoCatLists();

            UpdateTitle();
        }

        /// <summary>
        /// Updates enabled states for all game and category buttons
        /// </summary>
        void UpdateEnabledStatesForGames() {
            bool gamesSelected = lstGames.SelectedObjects.Count > 0;

            foreach( Control c in splitGame.Panel2.Controls ) {
                if( !( c == cmdGameAdd || c == cmbAutoCatType ) ) {
                    c.Enabled = gamesSelected;
                }
            }
        }

        void UpdateEnabledStatesForCategories() {
            Category c = null;
            foreach( ListViewItem item in lstCategories.SelectedItems ) {
                c = item.Tag as Category;
                if( c != null && !( currentProfile != null && c == currentProfile.GameData.FavoriteCategory ) ) {
                    break;
                } else {
                    c = null;
                }
            }
            cmdCatDelete.Enabled = c != null;
            c = ( lstCategories.SelectedItems.Count > 0 ) ? lstCategories.SelectedItems[0].Tag as Category : null;
            cmdCatRename.Enabled = c != null && !( currentProfile != null && c == currentProfile.GameData.FavoriteCategory );
        }

        /// <summary>
        /// Update UI to match current state of the SingleCatMode setting
        /// </summary>
        private void UpdateUIForSingleCat() {
            bool sCat = Settings.Instance.SingleCatMode;
            menu_Tools_SingleCat.Checked = sCat;
            UpdateTitle();
        }

        #endregion

        private void SetAdvancedMode( bool enabled ) {
            if( enabled ) {
                lstCategories.StateImageList = imglistFilter;
                advFilterAllow.Clear();
                advFilterExclude.Clear();
                advFilterRequire.Clear();
                advFilterUncatState = AdvancedFilterState.None;
            } else {
                lstCategories.StateImageList = null;
            }
            FillAllCategoryLists();
            OnViewChange();
        }
        #endregion

        #region UI Event Handlers

        private void FormMain_FormClosing( object sender, FormClosingEventArgs e ) {
            if( e.CloseReason == CloseReason.UserClosing ) {
                e.Cancel = !CheckForUnsaved();
            }
        }

        #region Drag and drop

        private void SetDragDropEffect( DragEventArgs e ) {
            if( Settings.Instance.SingleCatMode /*|| (e.KeyState & 4) == 4*/ ) { // Commented segment: SHIFT
                e.Effect = DragDropEffects.Move;
            } else if( ( e.KeyState & 8 ) == 8 ) { // CTRL
                e.Effect = DragDropEffects.Link;
            } else {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private ListViewItem GetCategoryItemAtPoint( int x, int y ) {
            Point clientPoint = lstCategories.PointToClient( new Point( x, y ) );
            return lstCategories.GetItemAt( clientPoint.X, clientPoint.Y );
        }

        private void lstCategories_DragEnter( object sender, DragEventArgs e ) {
            isDragging = true;
            dragOldCat = lstCategories.SelectedIndices.Count > 0 ? lstCategories.SelectedIndices[0] : -1;

            SetDragDropEffect( e );
        }

        private void lstCategories_DragDrop( object sender, DragEventArgs e ) {
            if( e.Data.GetDataPresent( typeof( int[] ) ) ) {
                lstCategories.SelectedIndices.Clear();
                if( dragOldCat >= 0 ) {
                    lstCategories.SelectedIndices.Add( dragOldCat );
                }
                isDragging = false;
                ClearStatus();
                ListViewItem dropItem = GetCategoryItemAtPoint( e.X, e.Y );

                SetDragDropEffect( e );

                if( dropItem.Tag != null && dropItem.Tag is Category ) {
                    Category dropCat = (Category)dropItem.Tag;
                    if( e.Effect == DragDropEffects.Move ) {
                        if( dropCat == currentProfile.GameData.FavoriteCategory ) {
                            currentProfile.GameData.AddGameCategory( (int[])e.Data.GetData( typeof( int[] ) ), dropCat );
                        } else {
                            currentProfile.GameData.SetGameCategories( (int[])e.Data.GetData( typeof( int[] ) ), dropCat, true );
                        }
                    } else if( e.Effect == DragDropEffects.Link ) {
                        currentProfile.GameData.RemoveGameCategory( (int[])e.Data.GetData( typeof( int[] ) ), dropCat );
                    } else if( e.Effect == DragDropEffects.Copy ) {
                        currentProfile.GameData.AddGameCategory( (int[])e.Data.GetData( typeof( int[] ) ), dropCat );
                    }
                    OnCategoryChange();
                    MakeChange( true );
                } else {
                    if( dropItem.Tag as string == GlobalStrings.MainForm_Uncategorized ) {
                        currentProfile.GameData.ClearGameCategories( (int[])e.Data.GetData( typeof( int[] ) ), true );
                        OnCategoryChange();
                        MakeChange( true );
                    }
                }

                FlushStatus();
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

        private void lstCategories_DragOver( object sender, DragEventArgs e ) {
            if( isDragging ) { // This shouldn't get called if this is false, but the OnSelectChange method is tied to this variable so do the check
                lstCategories.SelectedIndices.Clear();
                ListViewItem overItem = GetCategoryItemAtPoint( e.X, e.Y );
                if( overItem != null ) overItem.Selected = true;
            }

            SetDragDropEffect( e );
        }

        private void lstCategories_DragLeave( object sender, EventArgs e ) {
            isDragging = false;
            lstCategories.SelectedIndices.Clear();
            if( dragOldCat >= 0 ) {
                lstCategories.SelectedIndices.Add( dragOldCat );
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

        private void menu_File_Manual_Export_Click( object sender, EventArgs e ) {
            ClearStatus();
            ManualExportConfig();
            FlushStatus();
        }

        private void menu_File_Exit_Click( object sender, EventArgs e ) {
            this.Close();
        }

        private void menu_Profile_Update_Click( object sender, EventArgs e ) {
            ClearStatus();
            UpdateLibrary();
            FlushStatus();
        }

        private void menu_Profile_Import_Click( object sender, EventArgs e ) {
            ClearStatus();
            ImportConfig();
            FlushStatus();
        }

        private void menu_Profile_Export_Click( object sender, EventArgs e ) {
            ClearStatus();
            ExportConfig();
            FlushStatus();
        }

        private void menu_Profile_Edit_Click( object sender, EventArgs e ) {
            ClearStatus();
            EditProfile();
            FlushStatus();
        }

        private void menu_Profile_EditAutoCats_Click( object sender, EventArgs e ) {
            ClearStatus();
            EditAutoCats();
            FlushStatus();
        }

        private void menuToolsAutocat_Item_Click( object sender, EventArgs e ) {
            ToolStripItem item = sender as ToolStripItem;
            if( item != null ) {
                AutoCat autoCat = item.Tag as AutoCat;
                if( autoCat != null ) {
                    ClearStatus();
                    Autocategorize( false, autoCat );
                    FlushStatus();
                }
            }
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
            Depressurizer.DBEditDlg dlg = new Depressurizer.DBEditDlg( ( currentProfile != null ) ? currentProfile.GameData : null );
            dlg.ShowDialog();
            LoadGameDB();
        }

        private void menu_Tools_SingleCat_Click( object sender, EventArgs e ) {
            Settings.Instance.SingleCatMode = !Settings.Instance.SingleCatMode;
            UpdateUIForSingleCat();
        }

        private void menu_About_Click( object sender, EventArgs e ) {
            ( new DlgAbout() ).ShowDialog();
        }

        /// <summary>
        /// jpodadera. Recursive function to reload resources of new language for a menu item and its childs
        /// </summary>
        /// <param name="item"></param> Item menu to reload resources
        /// <param name="resources"></param> Resource manager
        /// <param name="newCulture"></param> Culture of language to load
        private void changeLanguageToolStripItems( ToolStripItem item, ComponentResourceManager resources, CultureInfo newCulture ) {
            if( item != null ) {
                if( item is ToolStripDropDownItem ) {
                    foreach( ToolStripItem childItem in ( item as ToolStripDropDownItem ).DropDownItems )
                        changeLanguageToolStripItems( childItem, resources, newCulture );
                }
                resources.ApplyResources( item, item.Name, newCulture );
            }
        }

        /// <summary>
        /// jpodadera. Recursive function to reload resources of new language for a control and its childs 
        /// </summary>
        /// <param name="c"></param> Control to reload resources
        /// <param name="resources"></param> Resource manager
        /// <param name="newCulture"></param> Culture of language to load
        private void changeLanguageControls( Control c, ComponentResourceManager resources, CultureInfo newCulture ) {
            if( c != null ) {
                if( c.GetType() == typeof( MenuStrip ) ) {
                    foreach( ToolStripDropDownItem mItem in ( c as MenuStrip ).Items )
                        changeLanguageToolStripItems( mItem, resources, newCulture );
                } else if( c is ListView ) {
                    // jpodadera. Because a framework bug, names of ColumnHeader objects are empty. 
                    // Resolved by saving names to Tag property.
                    foreach( ColumnHeader cHeader in ( c as ListView ).Columns )
                        if ( cHeader.Tag !=null)
                        resources.ApplyResources( cHeader, cHeader.Tag.ToString(), newCulture );
                }
                else
                {
                    foreach (Control childControl in c.Controls)
                        changeLanguageControls(childControl, resources, newCulture);
                }
                resources.ApplyResources( c, c.Name, newCulture );
            }
        }

        private void menu_Tools_Settings_Click( object sender, EventArgs e ) {
            ClearStatus();
            DlgOptions dlg = new DlgOptions();

            // jpodadera. Save culture of actual language
            CultureInfo actualCulture = Thread.CurrentThread.CurrentUICulture;

            dlg.ShowDialog();

            // jpodadera. If language has been changed, reload resources of main window
            if( actualCulture.Name != Thread.CurrentThread.CurrentUICulture.Name ) {
                ComponentResourceManager resources = new ComponentResourceManager( typeof( FormMain ) );
                resources.ApplyResources( this, this.Name, Thread.CurrentThread.CurrentUICulture );

                // If the window is maximized, un-maximize it
                bool maximized = false;
                if( this.WindowState == FormWindowState.Maximized ) {
                    maximized = true;
                    this.WindowState = FormWindowState.Normal;
                }

                // jpodadera. Save actual size and recover original size before reload resources of controls
                int actualWidth = this.Width;
                int actualHeight = this.Height;
                int actualSplitDistanceMain = this.splitContainer.SplitterDistance;
                int actualSplitDistanceSecondary = this.splitGame.SplitterDistance;

                this.Width = this.originalWidth;
                this.Height = this.originalHeight;
                this.splitContainer.SplitterDistance = this.originalSplitDistanceMain;
                this.splitGame.SplitterDistance = this.originalSplitDistanceSecondary;

                changeLanguageControls( this, resources, Thread.CurrentThread.CurrentUICulture );

                // jpodadera. Recover previous size
                this.Width = actualWidth;
                this.Height = actualHeight;
                splitContainer.SplitterDistance = actualSplitDistanceMain;
                splitGame.SplitterDistance = actualSplitDistanceSecondary;

                // Re-maximize if it was maximized before
                if( maximized ) {
                    this.WindowState = FormWindowState.Maximized;
                }

                FullListRefresh();
            }

            FlushStatus();
        }

        #endregion

        #region Context menus

        private void contextCat_Opening( object sender, System.ComponentModel.CancelEventArgs e ) {
            bool selectedCat = lstCategories.SelectedItems.Count > 0 && lstCategories.SelectedItems[0].Tag != null;
            contextCat_Delete.Enabled = contextCat_Rename.Enabled = selectedCat;
        }

        private void contectCat_RemoveEmpty_Click( object sender, EventArgs e ) {
            ClearStatus();
            RemoveEmptyCats();
            FlushStatus();
        }

        private void contextGame_Opening( object sender, System.ComponentModel.CancelEventArgs e ) {
            bool selectedGames = lstGames.SelectedObjects.Count > 0;
            contextGame_Edit.Enabled = selectedGames;
            contextGame_Remove.Enabled = selectedGames;
            contextGame_AddCat.Enabled = selectedGames;
            contextGame_RemCat.Enabled = selectedGames && contextGameRemCat.Items.Count > 0;
            contextGame_SetFav.Enabled = selectedGames;
            contextGame_VisitStore.Enabled = selectedGames;
            contextGame_LaunchGame.Enabled = selectedGames;
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

        private void contextGameAddCat_Create_Click( object sender, EventArgs e ) {
            Category c = CreateCategory();
            if( c != null ) {
                ClearStatus();
                AddCategoryToSelectedGames( c, false );
                FlushStatus();
            }
        }

        private void contextGameAddCat_Category_Click( object sender, EventArgs e ) {
            ToolStripItem menuItem = sender as ToolStripItem;
            if( menuItem != null ) {
                ClearStatus();
                Category c = menuItem.Tag as Category;
                AddCategoryToSelectedGames( c, false );
                FlushStatus();
            }
        }

        private void contextGameRemCat_Category_Click( object sender, EventArgs e ) {
            ToolStripItem menuItem = sender as ToolStripItem;
            if( menuItem != null ) {
                ClearStatus();
                Category c = menuItem.Tag as Category;
                RemoveCategoryFromSelectedGames( c );
                FlushStatus();
            }
        }

        private void contextGame_VisitStore_Click( object sender, EventArgs e ) {
            if (lstGames.SelectedObjects.Count > 0)
            {
                Utility.LaunchStorePage(tlstGames.SelectedObjects[0].Id);
            }
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

        private void cmdAutoCat_Click( object sender, EventArgs e ) {
            AutoCat ac = cmbAutoCatType.SelectedItem as AutoCat;
            if( ac != null ) {
                ClearStatus();
                Autocategorize( true, ac );
                FlushStatus();
            }
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

        private void cmdGameLaunch_Click( object sender, EventArgs e ) {
            ClearStatus();
            if (lstGames.SelectedObjects.Count > 0)
            {
                LaunchGame(tlstGames.SelectedObjects[0]);
            }
            FlushStatus();
        }
        
        private void cmdAddCatAndAssign_Click( object sender, EventArgs e ) {
            if( ValidateCategoryName( txtAddCatAndAssign.Text ) ) {
                Category cat = currentProfile.GameData.GetCategory( txtAddCatAndAssign.Text );
                AddCategoryToSelectedGames( cat, false );
                txtAddCatAndAssign.Clear();
            }
        }

        #endregion

        #region List events

        private void lstCategories_SelectedIndexChanged( object sender, EventArgs e ) {
            if( !isDragging ) {

                object nowSelected = null;
                if( lstCategories.SelectedItems.Count > 0 ) {
                    ListViewItem selItem = lstCategories.SelectedItems[0];
                    nowSelected = ( selItem.Tag == null ) ? selItem.Text : selItem.Tag;
                }

                if( nowSelected != lastSelectedCat ) {
                    OnViewChange();
                    lastSelectedCat = nowSelected;
                }
                UpdateEnabledStatesForCategories();
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
                case Keys.Return:
                case Keys.Space:
                    if( AdvancedCategoryFilter ) {
                        bool reverse = Control.ModifierKeys == Keys.Shift;
                        foreach( ListViewItem i in lstCategories.SelectedItems ) {
                            HandleAdvancedCategoryItemActivation( i, reverse, false );
                        }
                        OnViewChange();
                    }
                    break;
            }
        }

        private void lstCategories_MouseDown( object sender, MouseEventArgs e ) {
            if( e.Button == System.Windows.Forms.MouseButtons.Right ) {
                ListViewItem overItem = lstCategories.GetItemAt( e.X, e.Y );
                if( overItem != null )
                    overItem.Selected = true;

            } else if( e.Button == System.Windows.Forms.MouseButtons.Left ) {
                if( AdvancedCategoryFilter ) {
                    ListViewItem i = lstCategories.GetItemAt( e.X, e.Y );
                    HandleAdvancedCategoryItemActivation( i, Control.ModifierKeys == Keys.Shift );
                }
            }
        }

        private void lstCategories_Layout( object sender, LayoutEventArgs e ) {
            lstCategories.Columns[0].Width = lstCategories.DisplayRectangle.Width;
        }

        private void HandleAdvancedCategoryItemActivation( ListViewItem i, bool reverse, bool updateView = true ) {
            int oldState = i.StateImageIndex;

            if( i.StateImageIndex == -1 && reverse ) {
                i.StateImageIndex = MAX_FILTER_STATE;
            } else if( i.StateImageIndex == MAX_FILTER_STATE && !reverse ) {
                i.StateImageIndex = -1;
            } else {
                i.StateImageIndex += reverse ? -1 : 1;
            }

            Category c = i.Tag as Category;

            if( c == null ) {
                advFilterUncatState = (AdvancedFilterState)i.StateImageIndex;
            } else {
                switch( oldState ) {
                    case (int)AdvancedFilterState.Allow:
                        advFilterAllow.Remove( c );
                        break;
                    case (int)AdvancedFilterState.Require:
                        advFilterRequire.Remove( c );
                        break;
                    case (int)AdvancedFilterState.Exclude:
                        advFilterExclude.Remove( c );
                        break;
                }

                switch( i.StateImageIndex ) {
                    case (int)AdvancedFilterState.Allow:
                        advFilterAllow.Add( c );
                        break;
                    case (int)AdvancedFilterState.Require:
                        advFilterRequire.Add( c );
                        break;
                    case (int)AdvancedFilterState.Exclude:
                        advFilterExclude.Add( c );
                        break;
                }
            }

            if( updateView ) OnViewChange();
        }

        private void lstGames_SelectionChanged( object sender, EventArgs e ) {
            UpdateSelectedStatusText();
            UpdateEnabledStatesForGames();
            UpdateGameCheckStates();
        }

        private void lstGames_ItemsChanged(object sender, ItemsChangedEventArgs e)
        {
            UpdateSelectedStatusText();
            UpdateEnabledStatesForGames();
            UpdateGameCheckStates();
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
            }
            FlushStatus();
        }

        private void lstMultiCat_MouseDown( object sender, MouseEventArgs e ) {
            ListViewItem i = lstMultiCat.GetItemAt( e.X, e.Y );
            HandleMultiCatItemActivation( i, Control.ModifierKeys == Keys.Shift );
        }

        private void lstMultiCat_KeyPress( object sender, KeyPressEventArgs e ) {
            bool modKey = Control.ModifierKeys == Keys.Shift;
            if( e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Space ) {
                if( lstMultiCat.SelectedItems.Count == 0 ) return;
                ListViewItem item = lstMultiCat.SelectedItems[0];
                HandleMultiCatItemActivation( item, Control.ModifierKeys == Keys.Shift );
            }
        }

        void HandleMultiCatItemActivation( ListViewItem item, bool modKey ) {
            if( item != null ) {
                if( item.StateImageIndex == 0 || ( item.StateImageIndex == 2 && modKey ) ) {
                    item.StateImageIndex = 1;
                    Category cat = item.Tag as Category;
                    if( cat != null ) {
                        AddCategoryToSelectedGames( cat, false );
                    }
                } else if( item.StateImageIndex == 1 || ( item.StateImageIndex == 2 && !modKey ) ) {
                    item.StateImageIndex = 0;
                    Category cat = item.Tag as Category;
                    if( cat != null ) {
                        RemoveCategoryFromSelectedGames( cat );
                    }
                }
            }
        }

        #endregion

        private void radCatMode_CheckedChanged( object sender, EventArgs e ) {
            RadioButton snd = sender as RadioButton;
            if( snd != null && snd.Checked ) {
                SetAdvancedMode( snd == radCatAdvanced );
            }
        }

        private void chkFavorite_CheckedChanged( object sender, EventArgs e ) {
            if( !ignoreCheckChanges ) {
                if( chkFavorite.CheckState == CheckState.Checked ) {
                    AddCategoryToSelectedGames( currentProfile.GameData.FavoriteCategory, false );
                } else if( chkFavorite.CheckState == CheckState.Unchecked ) {
                    RemoveCategoryFromSelectedGames( currentProfile.GameData.FavoriteCategory );
                }
            }
        }

        private void chkHidden_CheckedChanged( object sender, EventArgs e ) {
            if( !ignoreCheckChanges ) {
                if( chkHidden.CheckState == CheckState.Checked ) {
                    AssignHiddenToSelectedGames( true );
                } else if( chkHidden.CheckState == CheckState.Unchecked ) {
                    AssignHiddenToSelectedGames( false );
                }
            }
        }

        private void txtSearch_TextChanged( object sender, EventArgs e ) {
            if( txtSearch.Text.IndexOf( lastFilterString, StringComparison.CurrentCultureIgnoreCase ) == -1 ) {
                FillGameList();
            } else {
                UpdateGameList();
            }
            lastFilterString = txtSearch.Text;
        }

        private void cmdSearchClear_Click( object sender, EventArgs e ) {
            txtSearch.Clear();
        }

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
            if( !ProfileLoaded || !unsavedChanges ) return true;

            DialogResult res = MessageBox.Show( GlobalStrings.MainForm_UnsavedChangesWillBeLost, GlobalStrings.MainForm_UnsavedChanges, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning );
            if( res == System.Windows.Forms.DialogResult.No ) {
                return true;
            }
            if( res == System.Windows.Forms.DialogResult.Cancel ) {
                return false;
            }
            return SaveProfile();
        }

        /// <summary>
        /// Checks to see if a game should currently be displayed, based on the state of the category list.
        /// </summary>
        /// <param name="g">Game to check</param>
        /// <returns>True if it should be displayed, false otherwise</returns>
        bool ShouldDisplayGame( GameInfo g ) {
            if( currentProfile == null ) return false;
            if( txtSearch.Text != string.Empty && g.Name.IndexOf( txtSearch.Text, StringComparison.CurrentCultureIgnoreCase ) == -1 ) return false;
            if( !currentProfile.GameData.Games.ContainsKey( g.Id ) ) return false;
            if( g.Id < 0 && !currentProfile.IncludeShortcuts ) return false;

            if( lstCategories.SelectedItems.Count == 0 ) return false;

            if( AdvancedCategoryFilter ) {
                return ShouldDisplayGameAdvanced( g );
            } else {
                if( lstCategories.SelectedItems[0].Tag is Category ) {
                    return g.ContainsCategory( lstCategories.SelectedItems[0].Tag as Category );
                } else {
                    if( lstCategories.SelectedItems[0].Tag as string == GlobalStrings.MainForm_All ) {
                        return true;
                    }
                    if( lstCategories.SelectedItems[0].Tag as string == GlobalStrings.MainForm_Uncategorized ) {
                        return !g.HasCategories();
                    }
                }

                return false;
            }
        }

        bool ShouldDisplayGameAdvanced( GameInfo g ) {
            bool isCategorized = false;
            if( advFilterUncatState != AdvancedFilterState.None ) isCategorized = g.HasCategories();

            if( advFilterUncatState == AdvancedFilterState.Allow || advFilterAllow.Count > 0 ) {
                if( !( advFilterUncatState == AdvancedFilterState.Allow && !isCategorized ) ) {
                    if( !g.Categories.Overlaps( advFilterAllow ) ) return false;
                }
            }

            if( advFilterUncatState == AdvancedFilterState.Require && isCategorized ) return false;
            if( !g.Categories.IsSupersetOf( advFilterRequire ) ) return false;

            if( advFilterUncatState == AdvancedFilterState.Exclude && !isCategorized ) return false;
            if( g.Categories.Overlaps( advFilterExclude ) ) return false;

            return true;
        }

        /// <summary>
        /// Launchs selected game
        /// <param name="g">Game to launch</param>
        /// </summary>
        void LaunchGame( GameInfo g ) {
            if( g != null ) {
                string gameIdentifier;
                if( g.Id < 0 ) {   // External game
                    if( g.LaunchString == null ) {
                        MessageBox.Show( GlobalStrings.MainForm_LaunchFailed );
                        return;
                    }
                    gameIdentifier = g.LaunchString;
                } else {
                    // Steam game
                    gameIdentifier = g.Id.ToString();
                }
                System.Diagnostics.Process.Start( "steam://rungameid/" + gameIdentifier );
            }
        }

        /// <summary>
        /// Checks to see if a category name is valid. Does not make sure it isn't already in use. If the name is not valid, displays a warning.
        /// </summary>
        /// <param name="name">Name to check</param>
        /// <returns>True if valid, false otherwise</returns>
        private bool ValidateCategoryName( string name ) {
            if( string.IsNullOrEmpty(name) ) {
                MessageBox.Show( GlobalStrings.MainForm_CategoryNamesNotEmpty, GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                return false;
            } else {
                return true;
            }
        }

        /// <summary>
        /// Clustering strategy for columns with comma-seperated strings. (Tags, Categories, Flags, Genres etc)
        /// </summary>
        public class CommaClusteringStrategy : ClusteringStrategy
        {
            public override object GetClusterKey(object model)
            {
                return ((string)this.Column.GetValue(model)).Replace(", ", ",").Split(',');
            }
        }

        #endregion

        private void autoModeHelperToolStripMenuItem_Click( object sender, EventArgs e ) {
            (new DlgAutomaticModeHelper(currentProfile)).ShowDialog();
        }
    }
}