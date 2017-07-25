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
using System.ComponentModel;
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
using Depressurizer.Helpers;
using MaterialSkin;
using MaterialSkin.Controls;
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
        #region Fields

        #region Constants

        const int MAX_FILTER_STATE = 2;

        Color highlightCellColor = Color.FromArgb(255, 25, 55, 84);
        Color primaryCellColor = Color.FromArgb(255, 29, 29, 29);
        Color headerCellColor = Color.FromArgb(255, 58, 58, 58);
        Color headerFontColor = Color.FromArgb(255, 169, 167, 167);
        Color textColor = Color.FromArgb(255, 255, 255, 255);
        Color borderColor = Color.FromArgb(255, 25, 28, 38);
        Color formColor = Color.FromArgb(255, 42, 42, 44);
        Color menuColorDark = Color.FromArgb(255, 38, 50, 56);
        Color menuColorLight = Color.FromArgb(255, 55, 71, 79);
        Color menuPrimaryText = Color.FromArgb(255, 168, 173, 175);
        Color menuHighlightText = Color.FromArgb(255, 255, 255, 234);
        Color primary = Color.FromArgb(255, 55, 71, 79);
        Color primaryDark = Color.FromArgb(255, 38, 50, 56);
        Color primaryLight = Color.FromArgb(255, 96, 125, 139);
        Color accent = Color.FromArgb(255, 0, 145, 234);
        Color listBackground = Color.FromArgb(255, 22, 22, 22);

        const string BIG_UP = "{UP},{UP},{UP},{UP},{UP},{UP},{UP},{UP},{UP},{UP}";
        const string BIG_DOWN = "{DOWN},{DOWN},{DOWN},{DOWN},{DOWN},{DOWN},{DOWN},{DOWN},{DOWN},{DOWN}";
        const string ADVANCED_FILTER = "ADVANCED_FILTER";
        const string EARLY_ACCESS = "Early Access";

        #endregion

        public static Profile CurrentProfile;

        bool unsavedChanges;

        StringBuilder statusBuilder = new StringBuilder();

        TypedObjectListView<GameInfo> tlstGames;

        // Allow visual feedback when dragging over the cat list
        bool isDragging;

        int dragOldCat;

        // Used to reload resources of main form while switching language
        private int originalWidth,
            originalHeight,
            originalSplitDistanceMain,
            originalSplitDistanceSecondary,
            originalSplitDistanceBrowser;

        private readonly MaterialSkinManager materialSkinManager;

        // For getting game banners
        GameBanners bannerGrabber;

        Thread bannerThread;

        // used to prevent moving the filler column in the game list
        Thread columnReorderThread;

        // Used to prevent double clicking in Autocat listview from changing checkstate
        bool doubleClick;

        #region Filter caching fields

        object lastSelectedCat; // Stores last selected category to minimize game list refreshes
        Filter advFilter = new Filter(ADVANCED_FILTER);

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Just checks to see if there is currently a profile loaded
        /// </summary>
        public bool ProfileLoaded
        {
            get { return CurrentProfile != null; }
        }

        private bool AdvancedCategoryFilter
        {
            get { return mchkAdvancedCategories.Checked; }
        }

        #endregion

        #region Init

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
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900,
                Primary.BlueGrey500, Accent.LightBlue700, TextShade.WHITE);

            lstCategories.BackColor = formColor;
            lstCategories.ForeColor = textColor;

            InitializeLstGames();
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
        /// Initializes the lstGames Control.
        /// </summary>
        private void InitializeLstGames()
        {
            tlstGames = new TypedObjectListView<GameInfo>(lstGames);
            //Aspect Getters
            tlstGames.GenerateAspectGetters();
            colGameID.AspectToStringConverter = delegate { return String.Empty; };
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

                return ((GameInfo) g).GetCatString(GlobalStrings.MainForm_Uncategorized);
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
                if (Program.GameDB.Games.ContainsKey(id) && (Program.GameDB.Games[id].Genres != null))
                {
                    return string.Join(", ", Program.GameDB.Games[id].Genres);
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
                if (Program.GameDB.Games.ContainsKey(id) && (Program.GameDB.Games[id].Flags != null))
                {
                    return string.Join(", ", Program.GameDB.Games[id].Flags);
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
                if (Program.GameDB.Games.ContainsKey(id) && (Program.GameDB.Games[id].Tags != null))
                {
                    return string.Join(", ", Program.GameDB.Games[id].Tags);
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
                if (Program.GameDB.Games.ContainsKey(id) && (Program.GameDB.Games[id].vrSupport.Headsets != null))
                {
                    return string.Join(", ", Program.GameDB.Games[id].vrSupport.Headsets);
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
                if (Program.GameDB.Games.ContainsKey(id) && (Program.GameDB.Games[id].vrSupport.Input != null))
                {
                    return string.Join(", ", Program.GameDB.Games[id].vrSupport.Input);
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
                if (Program.GameDB.Games.ContainsKey(id) && (Program.GameDB.Games[id].vrSupport.PlayArea != null))
                {
                    return string.Join(", ", Program.GameDB.Games[id].vrSupport.PlayArea);
                }

                return string.Empty;
            };
            colLanguageInterface.AspectGetter = delegate (object g)
            {
                if (g == null)
                {
                    return string.Empty;
                }

                int id = ((GameInfo)g).Id;
                if (Program.GameDB.Games.ContainsKey(id) && (Program.GameDB.Games[id].languageSupport.Interface != null))
                {
                    return string.Join(", ", Program.GameDB.Games[id].languageSupport.Interface);
                }

                return string.Empty;
            };
            colLanguageSubtitles.AspectGetter = delegate (object g)
            {
                if (g == null)
                {
                    return string.Empty;
                }

                int id = ((GameInfo)g).Id;
                if (Program.GameDB.Games.ContainsKey(id) && (Program.GameDB.Games[id].languageSupport.Subtitles != null))
                {
                    return string.Join(", ", Program.GameDB.Games[id].languageSupport.Subtitles);
                }

                return string.Empty;
            };
            colLanguageFullAudio.AspectGetter = delegate (object g)
            {
                if (g == null)
                {
                    return string.Empty;
                }

                int id = ((GameInfo)g).Id;
                if (Program.GameDB.Games.ContainsKey(id) && (Program.GameDB.Games[id].languageSupport.FullAudio != null))
                {
                    return string.Join(", ", Program.GameDB.Games[id].languageSupport.FullAudio);
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
                DateTime releaseDate;
                CultureInfo culture = Utility.GetCultureInfoFromStoreLanguage(Program.GameDB.dbLanguage);
                if (Program.GameDB.Games.ContainsKey(id) && DateTime.TryParse(Program.GameDB.Games[id].SteamReleaseDate,
                        culture, DateTimeStyles.None, out releaseDate))
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
                if (((GameInfo) g).LastPlayed <= 0)
                {
                    return DateTime.MinValue;
                }

                return Utility.GetDTFromUTime(((GameInfo) g).LastPlayed).Date;
            };
            colAchievements.AspectGetter = delegate(object g)
            {
                if (g == null)
                {
                    return 0;
                }

                int id = ((GameInfo) g).Id;
                return Program.GameDB.Games.ContainsKey(id) ? Program.GameDB.Games[id].Achievements : 0;
            };
            colPlatforms.AspectGetter = delegate(object g)
            {
                if (g == null)
                {
                    return "";
                }

                AppPlatforms platforms = Program.GameDB.Games[((GameInfo)g).Id].Platforms;
                return ((platforms & AppPlatforms.Linux) != 0) && (platforms != AppPlatforms.All) ? platforms + ", SteamOS" : platforms.ToString() ;
            };
            colDevelopers.AspectGetter = delegate(object g)
            {
                if (g == null)
                {
                    return GlobalStrings.MainForm_Unknown;
                }

                int id = ((GameInfo) g).Id;
                if (Program.GameDB.Games.ContainsKey(id) && (Program.GameDB.Games[id].Developers != null))
                {
                    return string.Join(", ", Program.GameDB.Games[id].Developers);
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
                if (Program.GameDB.Games.ContainsKey(id) && (Program.GameDB.Games[id].Publishers != null))
                {
                    return string.Join(", ", Program.GameDB.Games[id].Publishers);
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
                return Program.GameDB.Games.ContainsKey(id) ? Program.GameDB.Games[id].ReviewTotal : 0;
            };
            colReviewScore.AspectGetter = delegate(object g)
            {
                if (g == null)
                {
                    return 0;
                }

                int id = ((GameInfo) g).Id;
                return Program.GameDB.Games.ContainsKey(id) ? Program.GameDB.Games[id].ReviewPositivePercentage : 0;
            };
            colReviewLabel.AspectGetter = delegate(object g)
            {
                if (g == null)
                {
                    return 0;
                }

                int id = ((GameInfo) g).Id;
                if (Program.GameDB.Games.ContainsKey(id))
                {
                    int reviewTotal = Program.GameDB.Games[id].ReviewTotal;
                    int reviewPositivePercentage = Program.GameDB.Games[id].ReviewPositivePercentage;
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
                return Program.GameDB.Games.ContainsKey(id) ? Program.GameDB.Games[id].HltbMain : 0;
            };
            colHltbExtras.AspectGetter = delegate(object g)
            {
                if (g == null)
                {
                    return 0;
                }

                int id = ((GameInfo) g).Id;
                return Program.GameDB.Games.ContainsKey(id) ? Program.GameDB.Games[id].HltbExtras : 0;
            };
            colHltbCompletionist.AspectGetter = delegate(object g)
            {
                if (g == null)
                {
                    return 0;
                }

                int id = ((GameInfo) g).Id;
                return Program.GameDB.Games.ContainsKey(id) ? Program.GameDB.Games[id].HltbCompletionist : 0;
            };

            //Aspect to String Converters
            colNumberOfReviews.AspectToStringConverter = delegate(object obj)
            {
                int reviewTotal = (int) obj;
                return (reviewTotal <= 0) ? "0" : reviewTotal.ToString();
            };
            colReviewScore.AspectToStringConverter = delegate(object obj)
            {
                int reviewScore = (int) obj;
                return (reviewScore <= 0) ? GlobalStrings.MainForm_Unknown : reviewScore.ToString() + '%';
            };
            colReviewLabel.AspectToStringConverter = delegate(object obj)
            {
                int index = (int) obj;
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
                    {1, "Overwhelmingly Negative"}
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
                DateTime LastPlayed = (DateTime) obj;
                Thread threadForCulture = new Thread(delegate() { });
                string format = threadForCulture.CurrentCulture.DateTimeFormat.ShortDatePattern;
                return (LastPlayed == DateTime.MinValue) ? null : LastPlayed.ToString(format);
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
            lstGames.RestoreState(Convert.FromBase64String(Settings.Instance.LstGamesState));
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // allow mousewheel scrolling for Add Category submenu.  Send 10 UP/DOWN per wheel click.
            contextGame.MouseWheel += HandleMouseWheel;

            // Load saved forms settings
            Settings settings = Settings.Instance;
            Location = new Point(settings.X, settings.Y);
            if (!Utility.IsOnScreen(this))
            {
                //TopLeft corner is off screen, so reset location
                Location = new Point(0, 0);
            }
            Size = new Size(settings.Width, settings.Height);
            splitContainer.SplitterDistance = settings.SplitContainer;
            settings.SplitGameContainerHeight = splitGame.Height;
            splitGame.SplitterDistance = settings.SplitGame;
            settings.SplitBrowserContainerWidth = splitBrowser.Width;
            splitBrowser.SplitterDistance = settings.SplitBrowser;

            ttHelp.Ext_SetToolTip(mchkAdvancedCategories, GlobalStrings.MainForm_Help_AdvancedCategories);

            InitializeObjectListView();

            LoadGameDB();

            // Save original width and height
            originalHeight = Height;
            originalWidth = Width;
            originalSplitDistanceMain = splitContainer.SplitterDistance;
            originalSplitDistanceSecondary = splitGame.SplitterDistance;
            originalSplitDistanceBrowser = splitBrowser.SplitterDistance;

            ClearStatus();
            if (Settings.Instance.SteamPath == null)
            {
                DlgSteamPath dlg = new DlgSteamPath();
                dlg.ShowDialog();
                Settings.Instance.SteamPath = dlg.Path;
                Settings.Instance.Save();
            }
            if (Settings.Instance.UpdateAppInfoOnStart)
            {
                UpdateGameDBFromAppInfo();
            }
            int threePointFiveDaysInSecs = 84 * 60 * 60;
            if (Settings.Instance.UpdateHltbOnStart && (Utility.GetCurrentUTime() >
                                                        (Program.GameDB.LastHltbUpdate + threePointFiveDaysInSecs)))
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

            if (CurrentProfile != null)
            {
                // restore previous session
                SelectCategory(settings);
                SelectFilter(settings);
                SelectAutoCats(settings);
            }
        }

        #endregion

        #region DB Operations

        /// <summary>
        /// Loads the database from disk. If the load fails, displays a message box and creates an empty DB.
        /// </summary>
        private void LoadGameDB()
        {
            try
            {
                Program.GameDB = new GameDB();
                if (File.Exists("GameDB.xml.gz"))
                {
                    Program.GameDB.Load("GameDB.xml.gz");
                }
                else if (File.Exists("GameDB.xml"))
                {
                    Program.GameDB.Load("GameDB.xml");
                }
                else
                {
                    MessageBox.Show(GlobalStrings.MainForm_ErrorLoadingGameDB +
                                    GlobalStrings.MainForm_GameDBFileNotExist);
                    Program.Logger.Write(LoggerLevel.Warning, GlobalStrings.MainForm_GameDBFileNotExist);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(GlobalStrings.MainForm_ErrorLoadingGameDB + ex.Message);
                Program.Logger.WriteException(GlobalStrings.MainForm_Log_ExceptionOnDBLoad, ex);
                Program.GameDB = new GameDB();
            }
        }

        /// <summary>
        /// Saves the current database to disk. Displays a message box on failure.
        /// </summary>
        private void SaveGameDB()
        {
            try
            {
                Program.GameDB.Save("GameDB.xml.gz");
                AddStatus(GlobalStrings.MainForm_Status_SavedDB);
            }
            catch (Exception e)
            {
                Program.Logger.WriteException(GlobalStrings.MainForm_Log_ExceptionAutosavingDB, e);
                MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_ErrorAutosavingDB, e.Message),
                    GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Updates the database using AppInfo cache. Displays an error message on failure. Saves the DB afterwards if AutosaveDB is set.
        /// </summary>
        private void UpdateGameDBFromAppInfo()
        {
            try
            {
                int num = Program.GameDB.UpdateFromAppInfo(
                    string.Format(Properties.Resources.AppInfoPath, Settings.Instance.SteamPath));
                AddStatus(string.Format(GlobalStrings.MainForm_Status_AppInfoAutoupdate, num));
                if ((num > 0) && Settings.Instance.AutosaveDB)
                {
                    SaveGameDB();
                }
            }
            catch (Exception e)
            {
                Program.Logger.WriteException(GlobalStrings.MainForm_Log_ExceptionAppInfo, e);
                MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_ErrorAppInfo, e.Message),
                    GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Updates the database using data from howlongtobeatsteam.com. Displays an error message on failure. Saves the DB afterwards if AutosaveDB is set.
        /// </summary>
        private void UpdateGameDBFromHltb()
        {
            Cursor = Cursors.WaitCursor;

            HltbPrcDlg dlg = new HltbPrcDlg();
            DialogResult res = dlg.ShowDialog();

            if (dlg.Error != null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.DBEditDlg_Log_ExceptionHltb, dlg.Error.Message);
                MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_ErrorHltb, dlg.Error.Message),
                    GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    if ((dlg.Updated > 0) && Settings.Instance.AutosaveDB)
                    {
                        SaveGameDB();
                    }
                }
            }

            FullListRefresh();
            Cursor = Cursors.Default;
        }

        #endregion

        #region Profile Operations

        /// <summary>
        /// Prompts user to create a new profile.
        /// </summary>
        void CreateProfile()
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
        /// Prompts the user to modify the currently loaded profile. If there isn't one, asks if the user would like to create one.
        /// </summary>
        void EditProfile()
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
                if (MessageBox.Show(GlobalStrings.MainForm_NoProfileLoaded, GlobalStrings.DBEditDlg_Error,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    CreateProfile();
                }
            }
        }

        /// <summary>
        /// Prompts user for a profile file to load, then loads it.
        /// </summary>
        void LoadProfile()
        {
            if (!CheckForUnsaved())
            {
                return;
            }

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "profile";
            dlg.AddExtension = true;
            dlg.CheckFileExists = true;
            dlg.Filter = GlobalStrings.DlgProfile_Filter;
            dlg.InitialDirectory = Path.GetDirectoryName(CurrentProfile == null
                ? Assembly.GetExecutingAssembly().CodeBase
                : CurrentProfile.FilePath);
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                LoadProfile(dlg.FileName, false);
            }
        }

        /// <summary>
        /// Loads the given profile file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="checkForChanges"></param>
        void LoadProfile(string path, bool checkForChanges = true)
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
                MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_ErrorLoadingProfile, e.Message),
                    GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Program.Logger.WriteException(GlobalStrings.MainForm_Log_ExceptionLoadingProfile, e);
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

        /// <summary>
        /// Prompts user for a file location and saves profile
        /// </summary>
        void SaveProfileAs()
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

        /// <summary>
        /// Saves profile data to a file and performs any related tasks. This is the main saving function, all saves go through this function.
        /// </summary>
        /// <param name="path">Path to save to. If null, just saves profile to its current path.</param>
        /// <returns>True if successful, false if there is a failure</returns>
        bool SaveProfile(string path = null)
        {
            if (!ProfileLoaded)
            {
                return false;
            }

            if (CurrentProfile.AutoExport)
            {
                ExportConfig();
            }
            Settings.Instance.LstGamesState = Convert.ToBase64String(lstGames.SaveState());

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
                MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_ErrorSavingProfile, e.Message),
                    GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.Logger.WriteException(GlobalStrings.MainForm_Log_ExceptionSavingProfile, e);
                AddStatus(GlobalStrings.MainForm_FailedSaveProfile);
                return false;
            }
        }

        /// <summary>
        /// Updates the game list for the loaded profile.
        /// </summary>
        void UpdateLibrary()
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
                    int newApps = 0;
                    AppTypes appFilter = CurrentProfile.IncludeUnknown
                        ? AppTypes.InclusionUnknown
                        : AppTypes.InclusionNormal;
                    int totalApps = CurrentProfile.GameData.UpdateGameListFromOwnedPackageInfo(CurrentProfile.SteamID64,
                        CurrentProfile.IgnoreList, appFilter, out newApps);
                    AddStatus(string.Format(GlobalStrings.MainForm_Status_LocalUpdate, totalApps, newApps));
                    success = true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_LocalUpdateError, e.Message),
                        GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Program.Logger.WriteException(GlobalStrings.MainForm_Log_ExceptionLocalUpdate, e);
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
                    CDlgUpdateProfile updateDlg = new CDlgUpdateProfile(CurrentProfile.GameData,
                        CurrentProfile.SteamID64, CurrentProfile.OverwriteOnDownload, CurrentProfile.IgnoreList,
                        CurrentProfile.IncludeUnknown);
                    DialogResult res = updateDlg.ShowDialog();

                    if (updateDlg.Error != null)
                    {
                        Program.Logger.WriteException(GlobalStrings.MainForm_Log_ExceptionWebUpdateDialog,
                            updateDlg.Error);
                        AddStatus(string.Format(GlobalStrings.MainForm_ErrorDownloadingProfileData,
                            updateDlg.UseHtml ? "HTML" : "XML"));
                        MessageBox.Show(
                            string.Format(GlobalStrings.MainForm_ErrorDowloadingProfile, updateDlg.Error.Message),
                            GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                MessageBox.Show(GlobalStrings.MainForm_NoGameDataFound, GlobalStrings.Gen_Warning,
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                AddStatus(GlobalStrings.MainForm_NoGamesInDownload);
                            }
                            else
                            {
                                MakeChange(true);
                                AddStatus(string.Format(GlobalStrings.MainForm_DownloadedGames, updateDlg.Fetched,
                                    updateDlg.Added, updateDlg.UseHtml ? "HTML" : "XML"));
                                FullListRefresh();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Program.Logger.WriteException(GlobalStrings.MainForm_Log_ExceptionWebUpdate, e);
                    MessageBox.Show(string.Format(GlobalStrings.MainForm_ErrorDowloadingProfile, e.Message),
                        GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AddStatus(GlobalStrings.MainForm_DownloadFailed);
                }
            }

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Attempts to import steam categories
        /// </summary>
        void ImportConfig()
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
                MessageBox.Show(string.Format(GlobalStrings.MainForm_ErrorImportingSteamDataList, e.Message),
                    GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Program.Logger.WriteException("Exception encountered while importing the remoteconfig file.", e);
                AddStatus(GlobalStrings.MainForm_ImportFailed);
            }
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Attempts to export steam categories
        /// </summary>
        void ExportConfig()
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
                    MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_ErrorExportingToSteam, e.Message),
                        GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.Logger.WriteException(GlobalStrings.MainForm_Log_ExceptionExport, e);
                    AddStatus(GlobalStrings.MainForm_ExportFailed);
                }
            }
        }

        /// <summary>
        /// Saves a Steam configuration file. Asks the user to select the file to save as.
        /// </summary>
        /// <returns>True if save was completed, false otherwise</returns>
        void ManualExportConfig()
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
                    MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_ErrorManualExport, e.Message),
                        GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.Logger.WriteException(GlobalStrings.MainForm_Log_ExceptionExport, e);
                    AddStatus(GlobalStrings.MainForm_ExportFailed);
                }
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Creates an Edit AutoCats dialog for the user
        /// </summary>
        private void EditAutoCats(AutoCat selected)
        {
            if (!ProfileLoaded)
            {
                return;
            }

            DlgAutoCat dlg = new DlgAutoCat(CurrentProfile.AutoCats, CurrentProfile.GameData, selected,
                CurrentProfile.FilePath);

            DialogResult res = dlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                CurrentProfile.AutoCats = dlg.AutoCatList;
                MakeChange(true);
                FillAutoCatLists();
            }
        }

        #endregion

        #region Data modifiers

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

        private void SaveFilter()
        {
            if ((!ProfileLoaded) || (!AdvancedCategoryFilter))
            {
                return;
            }

            GetStringDlg dlg = new GetStringDlg(cboFilter.Text, GlobalStrings.MainForm_SaveFilter,
                GlobalStrings.MainForm_EnterNewFilterName, GlobalStrings.MainForm_Save);
            if ((dlg.ShowDialog() == DialogResult.OK) && ValidateFilterName(dlg.Value))
            {
                Filter f;
                bool refresh = true;
                if (CurrentProfile.GameData.FilterExists(dlg.Value))
                {
                    DialogResult res =
                        MessageBox.Show(String.Format(GlobalStrings.MainForm_OverwriteFilterName, dlg.Value),
                            GlobalStrings.MainForm_Overwrite, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2);

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
                    f.Uncategorized = advFilter.Uncategorized;
                    f.Hidden = advFilter.Hidden;
                    f.VR = advFilter.VR;
                    f.Allow = advFilter.Allow;
                    f.Require = advFilter.Require;
                    f.Exclude = advFilter.Exclude;
                    if (refresh)
                    {
                        AddStatus(string.Format(GlobalStrings.MainForm_FilterAdded, f.Name));
                        RefreshFilters();
                        cboFilter.SelectedItem = f;
                    }
                }
                else
                {
                    MessageBox.Show(String.Format(GlobalStrings.MainForm_CouldNotAddFilter, dlg.Value),
                        GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        void DeleteFilter(Filter f)
        {
            if ((!ProfileLoaded) || (!AdvancedCategoryFilter))
            {
                return;
            }

            DialogResult res;
            res = MessageBox.Show(string.Format(GlobalStrings.MainForm_DeleteFilter, f.Name),
                GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                    MessageBox.Show(string.Format(GlobalStrings.MainForm_CouldNotDeleteFilter),
                        GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// Creates a new category, first prompting the user for the name to use. If the name is not valid or in use, displays a notification.
        /// </summary>
        /// <returns>The category that was added, or null if the operation was canceled or failed.</returns>
        Category CreateCategory()
        {
            if (!ProfileLoaded)
            {
                return null;
            }

            GetStringDlg dlg = new GetStringDlg(string.Empty, GlobalStrings.MainForm_CreateCategory,
                GlobalStrings.MainForm_EnterNewCategoryName, GlobalStrings.MainForm_Create);
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
                MessageBox.Show(String.Format(GlobalStrings.MainForm_CouldNotAddCategory, dlg.Value),
                    GlobalStrings.Gen_Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return null;
        }

        /// <summary>
        /// Deletes the selected categories and updates the UI. Prompts user for confirmation.
        /// </summary>
        void DeleteCategory()
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
                        MessageBox.Show(string.Format(GlobalStrings.MainForm_CouldNotDeleteCategory),
                            GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        /// <summary>
        /// Renames the given category. Prompts user for a new name. Updates UI. Will display an error if the rename fails.
        /// </summary>
        /// <param name="c">Category to rename</param>
        /// <returns>True if category was renamed, false otherwise.</returns>
        bool RenameCategory()
        {
            if (lstCategories.SelectedItems.Count > 0)
            {
                Category c = lstCategories.SelectedItems[0].Tag as Category;
                if ((c != null) && (c != CurrentProfile.GameData.FavoriteCategory))
                {
                    GetStringDlg dlg = new GetStringDlg(c.Name,
                        string.Format(GlobalStrings.MainForm_RenameCategory, c.Name),
                        GlobalStrings.MainForm_EnterNewName, GlobalStrings.MainForm_Rename);
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        string newName = dlg.Value;
                        if (newName == c.Name)
                        {
                            return true;
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
                                    if (lstCategories.Items[index].Tag == newCat)
                                    {
                                        lstCategories.SelectedIndices.Add(index);
                                        break;
                                    }
                                }
                                AddStatus(string.Format(GlobalStrings.MainForm_CategoryRenamed, c.Name));
                                return true;
                            }
                        }
                        MessageBox.Show(string.Format(GlobalStrings.MainForm_NameIsInUse, newName),
                            GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Adds a new game. Displays the game dialog to the user.
        /// </summary>
        void AddGame()
        {
            DlgGame dlg = new DlgGame(CurrentProfile.GameData, null);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                if (ProfileLoaded)
                {
                    if (CurrentProfile.IgnoreList.Remove(dlg.Game.Id))
                    {
                        AddStatus(string.Format(GlobalStrings.MainForm_UnignoredGame, dlg.Game.Id));
                    }
                }
                FullListRefresh();
                MakeChange(true);
                AddStatus(GlobalStrings.MainForm_AddedGame);
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Edits the first selected game. Displays game dialog.
        /// </summary>
        void EditGame()
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
        /// Removes all selected games. Prompts for confirmation.
        /// </summary>
        void RemoveGames()
        {
            int selectCount = lstGames.SelectedObjects.Count;
            if (selectCount > 0)
            {
                if (MessageBox.Show(
                        string.Format(GlobalStrings.MainForm_RemoveGame, selectCount, (selectCount == 1) ? "" : "s"),
                        GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
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
                        AddStatus(string.Format(GlobalStrings.MainForm_RemovedGame, removed,
                            (removed == 1) ? "" : "s"));
                        MakeChange(true);
                    }
                    if (ignored > 0)
                    {
                        AddStatus(string.Format(GlobalStrings.MainForm_IgnoredGame, ignored,
                            (ignored == 1) ? "" : "s"));
                        MakeChange(true);
                    }
                    FillGameList();
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Adds the given category to all selected games.
        /// </summary>
        /// <param name="cat">Category to add</param>
        /// <param name="refreshCatList">If true, refresh category views afterwards</param>
        /// <param name="forceClearOthers">If true, remove other categories from the affected games.</param>
        void AddCategoryToSelectedGames(Category cat, bool forceClearOthers)
        {
            if (lstGames.SelectedObjects.Count > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                foreach (GameInfo g in tlstGames.SelectedObjects)
                {
                    if (g != null)
                    {
                        if (forceClearOthers || Settings.Instance.SingleCatMode)
                        {
                            g.ClearCategories(alsoClearFavorite: false);
                            if (cat != null)
                            {
                                g.AddCategory(cat);
                            }
                        }
                        else
                        {
                            g.AddCategory(cat);
                        }
                    }
                }
                FillAllCategoryLists();
                if (forceClearOthers)
                {
                    FilterGamelist(false);
                }
                if (lstCategories.SelectedItems[0].Tag.ToString() == GlobalStrings.MainForm_Uncategorized)
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
        }

        /// <summary>
        /// Removes the given category from all selected games.
        /// </summary>
        /// <param name="cat">Category to remove.</param>
        void RemoveCategoryFromSelectedGames(Category cat)
        {
            if (lstGames.SelectedObjects.Count > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                foreach (GameInfo g in tlstGames.SelectedObjects)
                {
                    g.RemoveCategory(cat);
                }
                FillAllCategoryLists();
                if (lstCategories.SelectedItems[0].Tag is Category &&
                    ((Category) lstCategories.SelectedItems[0].Tag == cat))
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
        /// Assigns the given favorite state to all selected items in the game list.
        /// </summary>
        /// <param name="fav">True to turn fav on, false to turn it off.</param>
        void AssignFavoriteToSelectedGames(bool fav)
        {
            if (lstGames.SelectedObjects.Count > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                foreach (GameInfo g in tlstGames.SelectedObjects)
                {
                    g.SetFavorite(fav);
                }
                FillCategoryList(false);
                RebuildGamelist();
                MakeChange(true);
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Add or remove the hidden attribute to the selected games
        /// </summary>
        /// <param name="hidden">Whether the games should be hidden</param>
        void AssignHiddenToSelectedGames(bool hidden)
        {
            if (lstGames.SelectedObjects.Count > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                foreach (GameInfo g in tlstGames.SelectedObjects)
                {
                    g.SetHidden(hidden);
                }
                FillCategoryList(false);
                FilterGamelist(false);
                MakeChange(true);
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Unloads the current profile or game list, making sure the user gets the option to save any changes.
        /// </summary>
        /// <returns>True if there is now no loaded profile, false otherwise.</returns>
        void Unload()
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
        /// Autocategorizes a set of games.
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
            Queue<int> notInDbOrOldData = new Queue<int>();
            int oldDbDataCount = 0;
            int notInDbCount = 0;
            foreach (GameInfo game in gamesToUpdate)
            {
                if ((game.Id > 0) && (!Program.GameDB.Contains(game.Id) ||
                                    (Program.GameDB.Games[game.Id].LastStoreScrape == 0)))
                {
                    notInDbOrOldData.Enqueue(game.Id);
                    notInDbCount++;
                }
                else if ((game.Id > 0) && (Utility.GetCurrentUTime() >
                                           (Program.GameDB.Games[game.Id].LastStoreScrape +
                                            (Settings.Instance.ScrapePromptDays * 86400)))) //86400 seconds in a day
                {
                    notInDbOrOldData.Enqueue(game.Id);
                    oldDbDataCount++;
                }
            }

            if (((notInDbCount > 0) || (oldDbDataCount > 0)) && scrape)
            {
                Cursor.Current = Cursors.Default;
                string message = "";
                message += notInDbCount > 0
                    ? string.Format(GlobalStrings.MainForm_GamesNotFoundInGameDB, notInDbCount)
                    : "";
                if ((notInDbCount > 0) && (oldDbDataCount > 0))
                {
                    message += " " + GlobalStrings.Text_And + " ";
                }
                message += oldDbDataCount > 0
                    ? string.Format(GlobalStrings.MainForm_GamesHaveOldDataInGameDB, oldDbDataCount,
                        Settings.Instance.ScrapePromptDays)
                    : "";
                message += ". " + GlobalStrings.MainForm_ScrapeNow;
                if (MessageBox.Show(message, GlobalStrings.DBEditDlg_Confirm, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    == DialogResult.Yes)
                {
                    DbScrapeDlg scrapeDlg = new DbScrapeDlg(notInDbOrOldData);
                    DialogResult scrapeRes = scrapeDlg.ShowDialog();

                    if (scrapeRes == DialogResult.Cancel)
                    {
                        AddStatus(string.Format(GlobalStrings.MainForm_CanceledDatabaseUpdate));
                    }
                    else
                    {
                        AddStatus(string.Format(GlobalStrings.MainForm_UpdatedDatabaseEntries,
                            scrapeDlg.JobsCompleted));
                        if ((scrapeDlg.JobsCompleted > 0) && Settings.Instance.AutosaveDB)
                        {
                            SaveGameDB();
                        }
                    }
                }
                Cursor.Current = Cursors.WaitCursor;
            }

            autoCat.PreProcess(CurrentProfile.GameData, Program.GameDB);

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

        /// <summary>
        /// Renames all games with names from the database.
        /// </summary>
        private void AutonameAll()
        {
            DialogResult res = MessageBox.Show(GlobalStrings.MainForm_OverwriteExistingNames,
                GlobalStrings.MainForm_Overwrite, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
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
                    g.Name = Program.GameDB.GetName(g.Id);
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

        /// <summary>
        /// Removes any categories with no games assigned.
        /// </summary>
        void RemoveEmptyCats()
        {
            int count = CurrentProfile.GameData.RemoveEmptyCategories();
            AddStatus(string.Format(GlobalStrings.MainForm_RemovedEmptyCategories, count));
            FillAllCategoryLists();
        }

        #endregion

        #region UI Updaters

        #region Status and text updaters

        /// <summary>
        /// Adds a string to the status builder
        /// </summary>
        /// <param name="s"></param>
        public void AddStatus(string s)
        {
            statusBuilder.Append(s);
            statusBuilder.Append(' ');
        }

        /// <summary>
        /// Empties the status builder
        /// </summary>
        public void ClearStatus()
        {
            statusBuilder.Clear();
        }

        /// <summary>
        /// Sets the status text to the builder text, and clear the builder text.
        /// </summary>
        public void FlushStatus()
        {
            mlblStatusMsg.Font = new Font("Arial", 9);
            mlblStatusMsg.Text = statusBuilder.ToString();
            statusBuilder.Clear();
        }

        /// <summary>
        /// Updates the text displaying the number of items in the game list
        /// </summary>
        private void UpdateSelectedStatusText()
        {
            mlblStatusSelection.Font = new Font("Arial", 9);
            mlblStatusSelection.Text = string.Format(GlobalStrings.MainForm_SelectedDisplayed,
                lstGames.SelectedObjects.Count, lstGames.GetItemCount());
        }

        /// <summary>
        /// Updates the window title.
        /// </summary>
        void UpdateTitle()
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

        #endregion

        #region List updaters

        private void SelectCategory(Settings settings)
        {
            if (settings.Category != string.Empty)
            {
                lstCategories.SelectedIndices.Clear();
                for (int i = 0; i < lstCategories.Items.Count; i++)
                {
                    if (lstCategories.Items[i].Name == settings.Category)
                    {
                        lstCategories.SelectedIndices.Add(i);
                        break;
                    }
                }
            }
        }

        private void SelectFilter(Settings settings)
        {
            if (settings.Filter != string.Empty)
            {
                for (int i = 0; i < cboFilter.Items.Count; i++)
                {
                    string name = cboFilter.GetItemText(cboFilter.Items[i]);
                    if (name == settings.Filter)
                    {
                        mchkAdvancedCategories.Checked = true;
                        cboFilter.SelectedIndex = i;
                        cboFilter.Text = name;
                        ApplyFilter((Filter) cboFilter.SelectedItem);
                    }
                }
            }
        }

        private void SelectAutoCats(Settings settings)
        {
            if (settings.AutoCats != null)
            {
                List<string> autocats = settings.AutoCats.Split(',').ToList();
                foreach (string ac in autocats)
                {
                    for (int i = 0; i < lvAutoCatType.Items.Count; i++)
                    {
                        if (lvAutoCatType.Items[i].Name == ac)
                        {
                            lvAutoCatType.Items[i].Checked = true;
                        }
                    }
                }
            }
        }

        private void SaveSelectedAutoCats()
        {
            Settings settings = Settings.Instance;
            string autocats = string.Empty;
            for (int i = 0; i < lvAutoCatType.CheckedItems.Count; i++)
            {
                if (autocats == string.Empty)
                {
                    autocats += lvAutoCatType.CheckedItems[i].Name;
                }
                else
                {
                    autocats += "," + lvAutoCatType.CheckedItems[i].Name;
                }
            }
            settings.AutoCats = autocats;
        }

        /// <summary>
        /// Does all list updating that's required if the filter changes (category selection changes).
        /// </summary>
        private void OnViewChange()
        {
            FilterGamelist(false);
        }

        /// <summary>
        /// Completely regenerates both the category and game lists
        /// </summary>
        private void FullListRefresh()
        {
            FillAllCategoryLists();
            FillGameList();
        }

        /// <summary>
        /// Filters game list based on based on the current category selection and advanced filters
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

        /// <summary>
        /// Rebuild all the list view items in the gamelist, preserving as much state as is possible
        /// </summary>
        private void RebuildGamelist()
        {
            lstGames.BuildList();
        }

        /// <summary>
        /// Updates list item for every game on the list, removing games that no longer need to be there, but not adding new ones.
        /// </summary>
        private void UpdateGameList()
        {
            List<GameInfo> gamelist = lstGames.Objects.Cast<GameInfo>().ToList();
            foreach (GameInfo g in gamelist)
            {
                if ((CurrentProfile != null) && (!CurrentProfile.GameData.Games.ContainsKey(g.Id) ||
                                               ((g.Id < 0) && !CurrentProfile.IncludeShortcuts)))
                {
                    gamelist.Remove(g);
                }
            }
        }

        /// <summary>
        /// Completely re-populates the game list.
        /// </summary>
        private void FillGameList()
        {
            List<GameInfo> gamelist = new List<GameInfo>();
            Cursor = Cursors.WaitCursor;
            if (CurrentProfile != null)
            {
                foreach (GameInfo g in CurrentProfile.GameData.Games.Values)
                {
                    if ((g.Id < 0) && !CurrentProfile.IncludeShortcuts)
                    {
                        continue;
                    }

                    gamelist.Add(g);
                    if (g.Name == null)
                    {
                        g.Name = string.Empty;
                        gamelist.Add(g);
                    }
                }
            }

            if (gamelist.Count > 0)
            {
                StartBannerThread(new List<GameInfo>(gamelist));
            }

            lstGames.SetObjects(gamelist);

            lstGames.BuildList();

            mbtnAutoCategorize.Text = string.Format(Properties.Resources.AutoCat_ButtonLabel, AutoCatGameCount());

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Completely repopulates the category list and combobox. Maintains selection on both.
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

            FillCategoryList(true);

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

        /// <summary>
        /// Completely repopulates the category list. Maintains selection.
        /// </summary>
        private void FillCategoryList(bool sort)
        {
            object selected = (lstCategories.SelectedItems.Count > 0) ? lstCategories.SelectedItems[0].Tag : null;
            int selectedIndex = (lstCategories.SelectedItems.Count > 0) ? lstCategories.SelectedIndices[0] : -1;

            lstCategories.Items.Clear();

            if (!ProfileLoaded)
            {
                return;
            }

            CurrentProfile.GameData.Categories.Sort();

            lstCategories.BeginUpdate();
            lstCategories.Items.Clear();

            //calculate number of hidden, VR and uncategorized games
            int hidden = 0, uncategorized = 0, VR = 0;
            foreach (GameInfo g in CurrentProfile.GameData.Games.Values)
            {
                if (g.Hidden)
                {
                    hidden++;
                }
                else if (!g.HasCategories())
                {
                    uncategorized++;
                }
                if (Program.GameDB.SupportsVr(g.Id) && !g.Hidden)
                {
                    VR++;
                }
            }
            if (!AdvancedCategoryFilter)
            {
                ListViewItem i = new ListViewItem(GlobalStrings.MainForm_All + " (" +
                                                  (CurrentProfile.GameData.Games.Count - hidden) + ")")
                {
                    Tag = GlobalStrings.MainForm_All,
                    Name = GlobalStrings.MainForm_All
                };
                lstCategories.Items.Add(i);
            }

            ListViewItem lvi =
                new ListViewItem(GlobalStrings.MainForm_Uncategorized + " (" + uncategorized + ")")
                {
                    Tag = GlobalStrings.MainForm_Uncategorized,
                    Name = GlobalStrings.MainForm_Uncategorized
                };
            lstCategories.Items.Add(lvi);

            lvi = new ListViewItem(GlobalStrings.MainForm_Hidden + " (" + hidden + ")")
            {
                Tag = GlobalStrings.MainForm_Hidden,
                Name = GlobalStrings.MainForm_Hidden
            };
            lstCategories.Items.Add(lvi);

            lvi = new ListViewItem(GlobalStrings.MainForm_VR + " (" + VR + ")")
            {
                Tag = GlobalStrings.MainForm_VR,
                Name = GlobalStrings.MainForm_VR
            };
            lstCategories.Items.Add(lvi);

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
                lstCategories.ListViewItemSorter = new lstCategoriesComparer(lstCategoriesComparer.categorySortMode.Name, SortOrder.Ascending);
            }
            lstCategories.Sort();
            lstCategories.EndUpdate();
        }

        private void StartBannerThread(List<GameInfo> games)
        {
            if ((bannerThread != null) && (bannerThread.IsAlive))
            {
                bannerGrabber.Stop();
                Thread.Sleep(100);
            }
            bannerGrabber = new GameBanners(games);
            bannerThread = new Thread(bannerGrabber.Grab);
            bannerThread.Start();
        }

        private ListViewItem CreateCategoryListViewItem(Category c)
        {
            ListViewItem i = new ListViewItem(c.Name + " (" + c.Count + ")");
            i.Tag = c;
            i.Name = c.Name;
            return i;
        }

        void UpdateGameCheckStates()
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

        void AddRemoveCategoryContextMenu(GameInfo game)
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
                if (!found)
                {
                    ToolStripItem item = contextGameRemCat.Items.Add(c.Name);
                    item.Tag = c;
                    item.Click += contextGameRemCat_Category_Click;
                }
            }
        }

        void AddGameToMultiCatCheckStates(GameInfo game, bool first)
        {
            foreach (ListViewItem catItem in lstMultiCat.Items)
            {
                if (catItem.StateImageIndex != 2)
                {
                    Category cat = catItem.Tag as Category;
                    if (cat != null)
                    {
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
            }
        }

        //void AddGameToCheckboxStates( GameInfo game, bool first ) {
        //    ignoreCheckChanges = true;
        //    if( first ) {
        //        chkFavorite.CheckState = game.IsFavorite() ? CheckState.Checked : CheckState.Unchecked;
        //        chkHidden.CheckState = game.Hidden ? CheckState.Checked : CheckState.Unchecked;
        //    } else {
        //        if( chkFavorite.CheckState != CheckState.Indeterminate ) {
        //            if( game.IsFavorite() ) {
        //                if( chkFavorite.CheckState == CheckState.Unchecked ) chkFavorite.CheckState = CheckState.Indeterminate;
        //            } else {
        //                if( chkFavorite.CheckState == CheckState.Checked ) chkFavorite.CheckState = CheckState.Indeterminate;
        //            }
        //        }
        //        if( game.Hidden ) {
        //            if( chkHidden.CheckState == CheckState.Unchecked ) chkHidden.CheckState = CheckState.Indeterminate;
        //        } else {
        //            if( chkHidden.CheckState == CheckState.Checked ) chkHidden.CheckState = CheckState.Indeterminate;
        //        }
        //    }
        //    ignoreCheckChanges = false;
        //}

        private bool ShouldHideGame(GameInfo g)
        {
            return !ShouldDisplayGame(g);
        }

        void FillAutoCatLists()
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
                        ListViewItem listItem = new ListViewItem(ac.DisplayName);
                        listItem.Tag = ac;
                        listItem.Name = ac.Name;
                        listItem.Checked = ac.Selected;
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

        #endregion

        #region UI State updaters

        /// <summary>
        /// Updates UI after a profile is created, loaded, modified or closed.
        /// </summary>
        void OnProfileChange()
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

            Settings.Instance.ChangeStoreLanguage(Settings.Instance.StoreLang);

            UpdateTitle();
        }

        /// <summary>
        /// Updates enabled states for all game and category buttons
        /// </summary>
        void UpdateEnabledStatesForGames()
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

        void UpdateEnabledStatesForCategories()
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
            c = (lstCategories.SelectedItems.Count > 0) ? lstCategories.SelectedItems[0].Tag as Category : null;
            mbtnCatRename.Enabled = (c != null) &&
                                    !((CurrentProfile != null) && (c == CurrentProfile.GameData.FavoriteCategory));
        }

        /// <summary>
        /// Update UI to match current state of the SingleCatMode setting
        /// </summary>
        private void UpdateUIForSingleCat()
        {
            bool sCat = Settings.Instance.SingleCatMode;
            menu_Tools_SingleCat.Checked = sCat;
            UpdateTitle();
        }

        #endregion

        private void SetAdvancedMode(bool enabled)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (enabled)
            {
                splitCategories.Panel1Collapsed = false;
                lstCategories.StateImageList = imglistFilter;
                advFilter = new Filter(ADVANCED_FILTER);
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
            FillCategoryList(false);
            OnViewChange();
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region UI Event Handlers

        void HandleMouseWheel(object sender, MouseEventArgs e)
        {
            if (contextGame.IsDropDown)
            {
                SendKeys.SendWait(e.Delta > 0 ? BIG_UP : BIG_DOWN);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((bannerThread != null) && (bannerThread.IsAlive))
            {
                bannerGrabber.Stop();
                Thread.Sleep(100);
            }

            Settings settings = Settings.Instance;
            settings.X = Left;
            settings.Y = Top;
            settings.Height = Height;
            settings.Width = Width;
            settings.SplitContainer = splitContainer.SplitterDistance;
            settings.SplitGame = splitGame.SplitterDistance;
            settings.SplitBrowser = splitBrowser.SplitterDistance;

            settings.Filter = AdvancedCategoryFilter ? cboFilter.Text : string.Empty;

            if (lstCategories.SelectedItems.Count > 0)
            {
                settings.Category = lstCategories.SelectedItems[0].Name;
            }

            SaveSelectedAutoCats();

            //try
            //{
            //    settings.Save(true);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(GlobalStrings.DlgOptions_ErrorSavingSettingsFile + ex.Message, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = !CheckForUnsaved();
            }
        }

        #region Drag and drop

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

        private ListViewItem GetCategoryItemAtPoint(int x, int y)
        {
            Point clientPoint = lstCategories.PointToClient(new Point(x, y));
            return lstCategories.GetItemAt(clientPoint.X, clientPoint.Y);
        }

        private void lstCategories_DragEnter(object sender, DragEventArgs e)
        {
            isDragging = true;
            dragOldCat = lstCategories.SelectedIndices.Count > 0 ? lstCategories.SelectedIndices[0] : -1;

            SetDragDropEffect(e);
        }

        private void lstCategories_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(int[])))
            {
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
                            CurrentProfile.GameData.SetGameCategories((int[]) e.Data.GetData(typeof(int[])), dropCat,
                                true);
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
                else if ((string) dropItem.Tag == GlobalStrings.MainForm_Uncategorized)
                {
                    CurrentProfile.GameData.ClearGameCategories((int[]) e.Data.GetData(typeof(int[])), true);
                    FillCategoryList(false);
                    FilterGamelist(false);
                    MakeChange(true);
                }
                else if ((string) dropItem.Tag == GlobalStrings.MainForm_Hidden)
                {
                    CurrentProfile.GameData.HideGames((int[]) e.Data.GetData(typeof(int[])), true);
                    FillCategoryList(false);
                    FilterGamelist(false);
                    MakeChange(true);
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

        private void lstCategories_DragLeave(object sender, EventArgs e)
        {
            isDragging = false;
            lstCategories.SelectedIndices.Clear();
            if (dragOldCat >= 0)
            {
                lstCategories.SelectedIndices.Add(dragOldCat);
            }
        }

        #endregion

        #region Main menu

        private void menu_File_NewProfile_Click(object sender, EventArgs e)
        {
            ClearStatus();
            CreateProfile();
            FlushStatus();
        }

        private void menu_File_LoadProfile_Click(object sender, EventArgs e)
        {
            ClearStatus();
            LoadProfile();
            FlushStatus();
        }

        private void menu_File_SaveProfile_Click(object sender, EventArgs e)
        {
            ClearStatus();
            DlgClose close = new DlgClose(GlobalStrings.MainForm_SaveProfileConfirm, GlobalStrings.MainForm_SaveProfile,
                SystemIcons.Question.ToBitmap(), false, CurrentProfile.AutoExport);
            DialogResult res = close.ShowDialog();
            if (res == DialogResult.Yes)
            {
                CurrentProfile.AutoExport = close.Export;
                SaveProfile(null);
            }
            FlushStatus();
        }

        private void menu_File_SaveProfileAs_Click(object sender, EventArgs e)
        {
            ClearStatus();
            SaveProfileAs();
            FlushStatus();
        }

        private void menu_File_Close_Click(object sender, EventArgs e)
        {
            ClearStatus();
            Unload();
            FlushStatus();
        }

        private void menu_File_Manual_Export_Click(object sender, EventArgs e)
        {
            ClearStatus();
            ManualExportConfig();
            FlushStatus();
        }

        private void menu_File_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menu_Profile_Update_Click(object sender, EventArgs e)
        {
            ClearStatus();
            UpdateLibrary();
            FlushStatus();
        }

        private void menu_Profile_Import_Click(object sender, EventArgs e)
        {
            ClearStatus();
            ImportConfig();
            FlushStatus();
        }

        private void menu_Profile_Export_Click(object sender, EventArgs e)
        {
            ClearStatus();
            ExportConfig();
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
                selected = ((AutoCat) lvAutoCatType.Items[0].Tag);
            }
            EditAutoCats(selected);
            FlushStatus();
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

        private void menu_Tools_AutonameAll_Click(object sender, EventArgs e)
        {
            ClearStatus();
            AutonameAll();
            FlushStatus();
        }

        private void menu_Tools_RemoveEmpty_Click(object sender, EventArgs e)
        {
            ClearStatus();
            RemoveEmptyCats();
            FlushStatus();
        }

        private void menu_Tools_DBEdit_Click(object sender, EventArgs e)
        {
            DBEditDlg dlg = new DBEditDlg((CurrentProfile != null) ? CurrentProfile.GameData : null);
            dlg.ShowDialog();
            LoadGameDB();
        }

        private void menu_Tools_SingleCat_Click(object sender, EventArgs e)
        {
            Settings.Instance.SingleCatMode = !Settings.Instance.SingleCatMode;
            UpdateUIForSingleCat();
        }

        private void menu_About_Click(object sender, EventArgs e)
        {
            (new DlgAbout()).ShowDialog();
        }

        /// <summary>
        /// jpodadera. Recursive function to reload resources of new language for a menu item and its childs
        /// </summary>
        /// <param name="item"></param> Item menu to reload resources
        /// <param name="resources"></param> Resource manager
        /// <param name="newCulture"></param> Culture of language to load
        private void changeLanguageToolStripItems(ToolStripItem item, ComponentResourceManager resources,
            CultureInfo newCulture)
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
        /// jpodadera. Recursive function to reload resources of new language for a control and its childs 
        /// </summary>
        /// <param name="c"></param> Control to reload resources
        /// <param name="resources"></param> Resource manager
        /// <param name="newCulture"></param> Culture of language to load
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

        #endregion

        #region Context menus

        private void contextCat_Opening(object sender, CancelEventArgs e)
        {
            bool selectedCat = (lstCategories.SelectedItems.Count > 0) && (lstCategories.SelectedItems[0].Tag != null);
            contextCat_Delete.Enabled = contextCat_Rename.Enabled = selectedCat;
        }

        private void contectCat_RemoveEmpty_Click(object sender, EventArgs e)
        {
            ClearStatus();
            RemoveEmptyCats();
            FlushStatus();
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

        private void contextGame_SetFav_Yes_Click(object sender, EventArgs e)
        {
            ClearStatus();
            AssignFavoriteToSelectedGames(true);
            FlushStatus();
        }

        private void contextGame_SetFav_No_Click(object sender, EventArgs e)
        {
            ClearStatus();
            AssignFavoriteToSelectedGames(false);
            FlushStatus();
        }

        private void contextGameHidden_Yes_Click(object sender, EventArgs e)
        {
            ClearStatus();
            AssignHiddenToSelectedGames(true);
            FlushStatus();
        }

        private void contextGameHidden_No_Click(object sender, EventArgs e)
        {
            ClearStatus();
            AssignHiddenToSelectedGames(false);
            FlushStatus();
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

        private void contextGame_VisitStore_Click(object sender, EventArgs e)
        {
            if (lstGames.SelectedObjects.Count > 0)
            {
                Steam.LaunchStorePage(tlstGames.SelectedObjects[0].Id);
            }
        }

        private void contextAutoCat_Edit_Click(object sender, EventArgs e)
        {
            ClearStatus();
            AutoCat selected = null;
            if (lvAutoCatType.SelectedItems.Count > 0)
            {
                selected = ((AutoCat) lvAutoCatType.SelectedItems[0].Tag);
            }
            else if (lvAutoCatType.CheckedItems.Count > 0)
            {
                selected = ((AutoCat) lvAutoCatType.CheckedItems[0].Tag);
            }
            else
            {
                if (lvAutoCatType.Items.Count > 0)
                {
                    selected = ((AutoCat) lvAutoCatType.Items[0].Tag);
                }
            }
            EditAutoCats(selected);
            FlushStatus();
        }

        private void nameascendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstCategories.ListViewItemSorter =
                new lstCategoriesComparer(lstCategoriesComparer.categorySortMode.Name, SortOrder.Ascending);

            lstCategories.Sort();
        }

        private void namedescendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstCategories.ListViewItemSorter =
                new lstCategoriesComparer(lstCategoriesComparer.categorySortMode.Name, SortOrder.Descending);
            lstCategories.Sort();
        }

        private void countascendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstCategories.ListViewItemSorter =
                new lstCategoriesComparer(lstCategoriesComparer.categorySortMode.Count, SortOrder.Ascending);
            lstCategories.Sort();
        }

        private void countdescendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstCategories.ListViewItemSorter =
                new lstCategoriesComparer(lstCategoriesComparer.categorySortMode.Count, SortOrder.Descending);
            lstCategories.Sort();
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

        private void SetItemState(ListViewItem i, int state)
        {
            i.StateImageIndex = state;

            Category c = i.Tag as Category;

            if (i.Tag.ToString() == GlobalStrings.MainForm_Uncategorized)
            {
                advFilter.Uncategorized = state;
            }
            else if (i.Tag.ToString() == GlobalStrings.MainForm_Hidden)
            {
                advFilter.Hidden = state;
            }
            else if (i.Tag.ToString() == GlobalStrings.MainForm_VR)
            {
                advFilter.VR = state;
            }
            else
            {
                switch ((AdvancedFilterState) state)
                {
                    case AdvancedFilterState.Allow:
                        advFilter.Allow.Add(c);
                        advFilter.Require.Remove(c);
                        advFilter.Exclude.Remove(c);
                        break;
                    case AdvancedFilterState.Require:
                        advFilter.Allow.Remove(c);
                        advFilter.Require.Add(c);
                        advFilter.Exclude.Remove(c);
                        break;
                    case AdvancedFilterState.Exclude:
                        advFilter.Allow.Remove(c);
                        advFilter.Require.Remove(c);
                        advFilter.Exclude.Add(c);
                        break;
                    case AdvancedFilterState.None:
                        advFilter.Allow.Remove(c);
                        advFilter.Require.Remove(c);
                        advFilter.Exclude.Remove(c);
                        break;
                }
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

        private void menu_Profile_Restore_Config_Click(object sender, EventArgs e)
        {
            string sharedconfigPath = Path.GetDirectoryName(string.Format(Properties.Resources.ConfigFilePath,
                Settings.Instance.SteamPath, Profile.ID64toDirName(CurrentProfile.SteamID64)));
            DlgRestore restore = new DlgRestore(sharedconfigPath);

            DialogResult res = restore.ShowDialog();

            if (restore.Restored)
            {
                ClearStatus();
                ImportConfig();
                FlushStatus();
            }
        }

        #endregion

        #region Controls (Buttons, Textboxes and Checkboxes)

        private void mbtnSaveFilter_Click(object sender, EventArgs e)
        {
            if (AdvancedCategoryFilter)
            {
                SaveFilter();
            }
        }

        private void mbtnFilterDelete_Click(object sender, EventArgs e)
        {
            if (AdvancedCategoryFilter)
            {
                DeleteFilter((Filter) cboFilter.SelectedItem);
            }
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cboFilter.SelectedItem != null) && (AdvancedCategoryFilter))
            {
                ApplyFilter((Filter) cboFilter.SelectedItem);
            }
        }

        private void mbtnFilterRename_Click(object sender, EventArgs e)
        {
            if ((cboFilter.SelectedItem != null) && (AdvancedCategoryFilter))
            {
                RenameFilter((Filter) cboFilter.SelectedItem);
            }
        }

        private void mbtnClearFilters_Click(object sender, EventArgs e)
        {
            ApplyFilter(new Filter(String.Empty));
            cboFilter.Text = string.Empty;
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

        private void lvAutoCatType_DoubleClick(object sender, EventArgs e)
        {
            ClearStatus();
            AutoCat selected = null;
            if (lvAutoCatType.SelectedItems.Count > 0)
            {
                selected = ((AutoCat) lvAutoCatType.SelectedItems[0].Tag);
            }
            else if (lvAutoCatType.CheckedItems.Count > 0)
            {
                selected = ((AutoCat) lvAutoCatType.CheckedItems[0].Tag);
            }
            else
            {
                if (lvAutoCatType.Items.Count > 0)
                {
                    selected = ((AutoCat) lvAutoCatType.Items[0].Tag);
                }
            }
            EditAutoCats(selected);
            FlushStatus();
        }

        private void lvAutoCatType_Layout(object sender, LayoutEventArgs e)
        {
            lvAutoCatType.Columns[0].Width = lvAutoCatType.DisplayRectangle.Width;
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

        private void mtxtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterGamelist(false);
        }

        private void mbtnCatAdd_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ClearStatus();
            CreateCategory();
            FlushStatus();
            Cursor.Current = Cursors.Default;
        }

        private void mbtnCatRename_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ClearStatus();
            RenameCategory();
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
                        AutoCat ac = ((AutoCat) item.Tag);
                        autocats.Add(ac);
                    }
                    //RunAutoCats(currentProfile.AutoCats);  WILL THIS WORK?  ARE AUTOCATS SELECTED VALUES SET CORRECTLY
                    RunAutoCats(autocats, true);
                    RemoveEmptyCats();
                    FilterGamelist(true);
                }
            }
        }

        private void RunAutoCats(List<AutoCat> autocats, bool first, bool group = false)
        {
            foreach (AutoCat ac in autocats)
            {
                if (ac != null)
                {
                    if (ac.AutoCatType == AutoCatType.Group)
                    {
                        AutoCatGroup acg = (AutoCatGroup) ac;
                        RunAutoCats(
                            CurrentProfile.CloneAutoCatList(acg.Autocats,
                                CurrentProfile.GameData.GetFilter(acg.Filter)), first, true);
                    }
                    else
                    {
                        if (ac.Selected || group)
                        {
                            ClearStatus();
                            Autocategorize(mchkAutoCatSelected.Checked, ac, first, false);
                            first = false;
                            FlushStatus();
                        }
                    }
                }
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

        private void cmdGameRemove_Click(object sender, EventArgs e)
        {
            ClearStatus();
            RemoveGames();
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

        private void cmdAddCatAndAssign_Click(object sender, EventArgs e)
        {
            if (ValidateCategoryName(txtAddCatAndAssign.Text))
            {
                Category cat = CurrentProfile.GameData.GetCategory(txtAddCatAndAssign.Text);
                AddCategoryToSelectedGames(cat, false);
                txtAddCatAndAssign.Clear();
            }
        }

        private void mbtnSearchClear_Click(object sender, EventArgs e)
        {
            mtxtSearch.Clear();
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

        private void mchkAdvancedCategories_CheckedChanged(object sender, EventArgs e)
        {
            SetAdvancedMode(mchkAdvancedCategories.Checked);
        }

        private void txtAddCatAndAssign_TextChanged(object sender, EventArgs e) { }

        private void mchkAutoCatSelected_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAutoCatSelected_StatusMessage();
            mbtnAutoCategorize.Text = string.Format(Properties.Resources.AutoCat_ButtonLabel, AutoCatGameCount());
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

        #endregion

        #region List events

        private void lstCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isDragging)
            {
                object nowSelected = null;
                if (lstCategories.SelectedItems.Count > 0)
                {
                    ListViewItem selItem = lstCategories.SelectedItems[0];
                    nowSelected = (selItem.Tag == null) ? selItem.Text : selItem.Tag;
                }

                if (nowSelected != lastSelectedCat)
                {
                    OnViewChange();
                    lastSelectedCat = nowSelected;
                }
                UpdateEnabledStatesForCategories();
            }
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
                    if ((lstCategories.SelectedItems.Contains(i)) && !(ModifierKeys == Keys.Control))
                    {
                        HandleAdvancedCategoryItemActivation(i, ModifierKeys == Keys.Shift);
                    }
                }
            }
        }

        private void lstCategories_Layout(object sender, LayoutEventArgs e)
        {
            lstCategories.Columns[0].Width = lstCategories.DisplayRectangle.Width;
        }

        private void RenameFilter(Filter f)
        {
            if (AdvancedCategoryFilter)
            {
                GetStringDlg dlg = new GetStringDlg(f.Name, string.Format(GlobalStrings.MainForm_RenameFilter, f.Name),
                    GlobalStrings.MainForm_EnterNewName, GlobalStrings.MainForm_Rename);
                if ((dlg.ShowDialog() == DialogResult.OK) && (f.Name != dlg.Value))
                {
                    if (CurrentProfile.GameData.FilterExists(dlg.Value))
                    {
                        MessageBox.Show(GlobalStrings.MainForm_FilterExists, GlobalStrings.Gen_Warning,
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    f.Name = dlg.Value;
                    RefreshFilters();
                    cboFilter.SelectedItem = f;
                    cboFilter.Text = f.Name;
                }
            }
        }

        private void ApplyFilter(Filter f)
        {
            if (AdvancedCategoryFilter)
            {
                // reset Advanced settings
                advFilter = new Filter(ADVANCED_FILTER);

                // load new Advanced settings
                foreach (ListViewItem i in lstCategories.Items)
                {
                    if (i.Tag.ToString() == GlobalStrings.MainForm_Uncategorized)
                    {
                        i.StateImageIndex = f.Uncategorized;
                        advFilter.Uncategorized = f.Uncategorized;
                    }
                    else if (i.Tag.ToString() == GlobalStrings.MainForm_Hidden)
                    {
                        i.StateImageIndex = f.Hidden;
                        advFilter.Hidden = f.Hidden;
                    }
                    else if (i.Tag.ToString() == GlobalStrings.MainForm_VR)
                    {
                        i.StateImageIndex = f.VR;
                        advFilter.VR = f.VR;
                    }
                    else
                    {
                        if (f.Allow.Contains(((Category) i.Tag)))
                        {
                            i.StateImageIndex = (int) AdvancedFilterState.Allow;
                            advFilter.Allow.Add((Category) i.Tag);
                        }
                        else if (f.Require.Contains(((Category) i.Tag)))
                        {
                            i.StateImageIndex = (int) AdvancedFilterState.Require;
                            advFilter.Require.Add((Category) i.Tag);
                        }
                        else if (f.Exclude.Contains(((Category) i.Tag)))
                        {
                            i.StateImageIndex = (int) AdvancedFilterState.Exclude;
                            advFilter.Exclude.Add((Category) i.Tag);
                        }
                        else
                        {
                            i.StateImageIndex = (int) AdvancedFilterState.None;
                        }
                    }
                }
                OnViewChange();
            }
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

            Category c = i.Tag as Category;

            if (i.Tag.ToString() == GlobalStrings.MainForm_Uncategorized)
            {
                advFilter.Uncategorized = i.StateImageIndex;
            }
            else if (i.Tag.ToString() == GlobalStrings.MainForm_Hidden)
            {
                advFilter.Hidden = i.StateImageIndex;
            }
            else if (i.Tag.ToString() == GlobalStrings.MainForm_VR)
            {
                advFilter.VR = i.StateImageIndex;
            }
            else
            {
                switch (oldState)
                {
                    case (int) AdvancedFilterState.Allow:
                        advFilter.Allow.Remove(c);
                        break;
                    case (int) AdvancedFilterState.Require:
                        advFilter.Require.Remove(c);
                        break;
                    case (int) AdvancedFilterState.Exclude:
                        advFilter.Exclude.Remove(c);
                        break;
                }

                switch (i.StateImageIndex)
                {
                    case (int) AdvancedFilterState.Allow:
                        advFilter.Allow.Add(c);
                        break;
                    case (int) AdvancedFilterState.Require:
                        advFilter.Require.Add(c);
                        break;
                    case (int) AdvancedFilterState.Exclude:
                        advFilter.Exclude.Add(c);
                        break;
                }
            }

            if (updateView)
            {
                OnViewChange();
            }
        }

        private void lstGames_SelectionChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            UpdateSelectedStatusText();
            UpdateEnabledStatesForGames();
            UpdateGameCheckStates();
            UpdateAutoCatSelected_StatusMessage();
            mbtnAutoCategorize.Text = string.Format(Properties.Resources.AutoCat_ButtonLabel, AutoCatGameCount());
            Cursor.Current = Cursors.Default;
        }

        private void lstGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string storeLanguage = "en";
            contextGameFav_Yes.Checked = false;
            contextGameFav_No.Checked = false;
            contextGameHidden_Yes.Checked = false;
            contextGameHidden_No.Checked = false;
            if (Program.GameDB != null)
            {
                if (Program.GameDB.dbLanguage == StoreLanguage.zh_Hans)
                {
                    storeLanguage = "schinese";
                }
                else if (Program.GameDB.dbLanguage == StoreLanguage.zh_Hant)
                {
                    storeLanguage = "tchinese";
                }
                else if (Program.GameDB.dbLanguage == StoreLanguage.pt_BR)
                {
                    storeLanguage = "brazilian";
                }
                else
                {
                    storeLanguage = CultureInfo
                        .GetCultureInfo(Enum.GetName(typeof(StoreLanguage), Program.GameDB.dbLanguage)).EnglishName
                        .ToLowerInvariant();
                }
            }

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
                    webBrowser1.Navigate(string.Format(Properties.Resources.UrlSteamStoreApp + "?l=" + storeLanguage,
                        g.Id));
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
                        webBrowser1.Navigate(string.Format(
                            Properties.Resources.UrlSteamStoreApp + "?l=" + storeLanguage, g.Id));
                    }
                    else
                    {
                        webBrowser1.ScriptErrorsSuppressed = true;
                        webBrowser1.Navigate(Properties.Resources.UrlSteamStore + "?l=" + storeLanguage);
                    }
                }
                catch { }
            }
        }

        private void lstGames_ItemsChanged(object sender, ItemsChangedEventArgs e)
        {
            UpdateSelectedStatusText();
            UpdateEnabledStatesForGames();
            UpdateGameCheckStates();
        }

        private void lstGames_DoubleClick(object sender, EventArgs e)
        {
            ClearStatus();
            EditGame();
            FlushStatus();
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
            string bannerFile = string.Format(Properties.Resources.GameBannerPath,
                Path.GetDirectoryName(Application.ExecutablePath), g.Id);
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
            if (Program.GameDB.Games.ContainsKey(g.Id) && (Program.GameDB.Games[g.Id].Tags != null))
            {
                if (Program.GameDB.Games[g.Id].Tags.Contains(EARLY_ACCESS))
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

        private void lstMultiCat_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewItem i = lstMultiCat.GetItemAt(e.X, e.Y);
            HandleMultiCatItemActivation(i, ModifierKeys == Keys.Shift);
        }

        private void lstMultiCat_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool modKey = ModifierKeys == Keys.Shift;
            if ((e.KeyChar == (char) Keys.Return) || (e.KeyChar == (char) Keys.Space))
            {
                if (lstMultiCat.SelectedItems.Count == 0)
                {
                    return;
                }

                ListViewItem item = lstMultiCat.SelectedItems[0];
                HandleMultiCatItemActivation(item, ModifierKeys == Keys.Shift);
            }
        }

        void HandleMultiCatItemActivation(ListViewItem item, bool modKey)
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

        private void lstGames_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            columnReorderThread = new Thread(ColumnReorderWorker);
            columnReorderThread.Start();
        }

        #region Filler Column Reorder Prevent Thread 

        delegate void RemoveItemCallback();

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
                colFiller.DisplayIndex = (lstGames.ColumnsInDisplayOrder.Count - 1);
            }
        }

        private void ColumnReorderWorker()
        {
            reorderFillerColumn();
        }

        #endregion

        #endregion

        #endregion

        #region Utility

        private void ResortToolStripItemCollection(ToolStripItemCollection coll)
        {
            System.Collections.ArrayList oAList = new System.Collections.ArrayList(coll);
            oAList.Sort(new ToolStripItemComparer());
            coll.Clear();

            foreach (ToolStripItem oItem in oAList)
            {
                coll.Add(oItem);
            }
        }

        public class ToolStripItemComparer : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                ToolStripItem oItem1 = (ToolStripItem) x;
                ToolStripItem oItem2 = (ToolStripItem) y;
                return string.Compare(oItem1.Text, oItem2.Text, true);
            }
        }

        /// <summary>
        /// Sets the unsaved changes flag to the given value and takes the requisite UI updating action
        /// </summary>
        /// <param name="changes"></param>
        void MakeChange(bool changes)
        {
            unsavedChanges = changes;
            UpdateTitle();
        }

        /// <summary>
        /// If there are any unsaved changes, asks the user if they want to save. Also gives the user the option to cancel the calling action.
        /// </summary>
        /// <returns>True if the action should proceed, false otherwise.</returns>
        bool CheckForUnsaved()
        {
            if (!ProfileLoaded || !unsavedChanges)
            {
                return true;
            }

            DlgClose close = new DlgClose(GlobalStrings.MainForm_UnsavedChangesWillBeLost,
                GlobalStrings.MainForm_UnsavedChanges, SystemIcons.Warning.ToBitmap(), true, CurrentProfile.AutoExport);

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
            return SaveProfile(null);
        }

        /// <summary>
        /// Checks to see if a game should currently be displayed, based on the state of the category list.
        /// </summary>
        /// <param name="g">Game to check</param>
        /// <returns>True if it should be displayed, false otherwise</returns>
        bool ShouldDisplayGame(GameInfo g)
        {
            if (CurrentProfile == null)
            {
                return false;
            }
            if ((mtxtSearch.Text != string.Empty) &&
                (g.Name.IndexOf(mtxtSearch.Text, StringComparison.CurrentCultureIgnoreCase) == -1))
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
                return g.IncludeGame(advFilter);
            }

            if (lstCategories.SelectedItems[0].Tag.ToString() == GlobalStrings.MainForm_All)
            {
                return true;
            }

            if (lstCategories.SelectedItems[0].Tag.ToString() == GlobalStrings.MainForm_Uncategorized)
            {
                return !g.HasCategories();
            }

            if (g.Hidden)
            {
                return lstCategories.SelectedItems[0].Tag.ToString() == GlobalStrings.MainForm_Hidden;
            }

            if (lstCategories.SelectedItems[0].Tag.ToString() == GlobalStrings.MainForm_VR)
            {
                return Program.GameDB.SupportsVr(g.Id);
            }

            if (lstCategories.SelectedItems[0].Tag is Category)
            {
                if (((Category) lstCategories.SelectedItems[0].Tag).Name == GlobalStrings.MainForm_Favorite)
                {
                    return g.IsFavorite();
                }

                return g.ContainsCategory(lstCategories.SelectedItems[0].Tag as Category);
            }

            return false;
        }

        void FixWebBrowserRegistry()
        {
            string installkey = @"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
            string entryLabel = GetType().Assembly.GetName().Name + ".exe";

            int value = 0;
            int version = (new WebBrowser()).Version.Major;

            if ((version >= 8) && (version <= 11))
            {
                value = version * 1000;
            }
            else
            {
                return;
            }

            Microsoft.Win32.RegistryKey existingSubKey =
                Microsoft.Win32.Registry.LocalMachine.OpenSubKey(installkey, false); // readonly key

            if ((existingSubKey.GetValue(entryLabel) == null) ||
                (Convert.ToInt32(existingSubKey.GetValue(entryLabel)) != value))
            {
                new System.Security.Permissions.RegistryPermission(System.Security.Permissions.PermissionState
                    .Unrestricted).Assert();
                try
                {
                    existingSubKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(installkey,
                        Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree); // writable key
                    existingSubKey.SetValue(entryLabel, value, Microsoft.Win32.RegistryValueKind.DWord);
                }
                catch
                {
                    MessageBox.Show(GlobalStrings.MainForm_AdminRights, GlobalStrings.Gen_Warning, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                finally
                {
                    System.Security.Permissions.RegistryPermission.RevertAssert();
                }
            }
        }

        /// <summary>
        /// Launchs selected game
        /// <param name="g">Game to launch</param>
        /// </summary>
        void LaunchGame(GameInfo g)
        {
            if (g != null)
            {
                //string gameIdentifier;
                //if( g.Id < 0 ) {   // External game
                //    if( g.LaunchString == null ) {
                //        MessageBox.Show( GlobalStrings.MainForm_LaunchFailed );
                //        return;
                //    }
                //    gameIdentifier = g.LaunchString;
                //} else {
                //    // Steam game
                //    gameIdentifier = g.Id.ToString();
                //}
                g.LastPlayed = Utility.GetCurrentUTime();
                System.Diagnostics.Process.Start(g.Executable);
            }
        }

        /// <summary>
        /// Checks to see if a category name is valid. Does not make sure it isn't already in use. If the name is not valid, displays a warning.
        /// </summary>
        /// <param name="name">Name to check</param>
        /// <returns>True if valid, false otherwise</returns>
        private bool ValidateCategoryName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(GlobalStrings.MainForm_CategoryNamesNotEmpty, GlobalStrings.Gen_Warning,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private bool ValidateFilterName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(GlobalStrings.MainForm_FilterNamesNotEmpty, GlobalStrings.Gen_Warning,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private void mbtnEditAutocats_Click(object sender, EventArgs e)
        {
            ClearStatus();
            AutoCat selected = null;
            if (lvAutoCatType.SelectedItems.Count > 0)
            {
                selected = (AutoCat)lvAutoCatType.SelectedItems[0].Tag;
            }
            else if (lvAutoCatType.CheckedItems.Count > 0)
            {
                selected = (AutoCat)lvAutoCatType.CheckedItems[0].Tag;
            }
            else if (lvAutoCatType.Items.Count > 0)
            {
                selected = (AutoCat)lvAutoCatType.Items[0].Tag;
            }
            EditAutoCats(selected);
            FlushStatus();
        }

        /// <summary>
        /// Clustering strategy for columns with comma-seperated strings. (Tags, Categories, Flags, Genres etc)
        /// </summary>
        public class CommaClusteringStrategy : ClusteringStrategy
        {
            public override object GetClusterKey(object model)
            {
                return ((string) Column.GetValue(model)).Replace(", ", ",").Split(',');
            }
        }

        /// <summary>
        /// Checks github for newer versions of depressurizer.
        /// </summary>
        /// <returns>True if there is a newer release, false otherwise</returns>
        private void CheckForDepressurizerUpdates()
        {
            Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            try
            {
                Version githubVersion;
                string url;
                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Set("User-Agent", "Depressurizer");
                    string json = wc.DownloadString(Properties.Resources.UrlLatestRelease);
                    JObject parsedJson = JObject.Parse(json);
                    githubVersion = new Version(((string) parsedJson.SelectToken("tag_name")).Replace("v", ""));
                    url = (string) parsedJson.SelectToken("html_url");
                }
                if (githubVersion > currentVersion)
                {
                    if (
                        MessageBox.Show(
                            GlobalStrings.MainForm_Msg_UpdateFound, GlobalStrings.MainForm_Msg_UpdateFoundTitle,
                            MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(url);
                    }
                }
            }
            catch (Exception e)
            {
                Program.Logger.WriteException(GlobalStrings.MainForm_Log_ExceptionDepressurizerUpdate, e);
                MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_ErrorDepressurizerUpdate, e.Message),
                    GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Skinning

        public class MyRenderer : ToolStripRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                Color colorText = e.Item.Selected ? Color.FromArgb(255, 255, 255) : Color.FromArgb(169, 167, 167);
                if (e.ToolStrip is ToolStripDropDown)
                {
                    Color colorItem = Color.FromArgb(55, 71, 79);
                    using (SolidBrush brush = new SolidBrush(colorItem))
                    {
                        e.Graphics.FillRectangle(brush, rc);
                    }
                }
                else
                {
                    Color colorItem = Color.FromArgb(38, 50, 56);
                    using (SolidBrush brush = new SolidBrush(colorItem))
                    {
                        e.Graphics.FillRectangle(brush, rc);
                    }
                }

                e.Item.ForeColor = colorText;

                base.OnRenderMenuItemBackground(e);
            }

            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                Brush bLight = new SolidBrush(Color.FromArgb(157, 168, 157));

                if (!e.Vertical)
                {
                    Rectangle r3;
                    if (e.Item.IsOnDropDown)
                    {
                        r3 = new Rectangle(0, 3, e.Item.Width, 1);
                        e.Graphics.FillRectangle(bLight, r3);
                    }
                }

                base.OnRenderSeparator(e);
            }

            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                // Don't clear and fill the background if we already painted an image there
                //if (e.ToolStrip.BackgroundImage != null)
                //{
                //    if (e.ToolStrip is StatusStrip)
                //        e.Graphics.DrawLine(Pens.White, e.AffectedBounds.Left, e.AffectedBounds.Top, e.AffectedBounds.Right, e.AffectedBounds.Top);

                //    return;
                //}

                if (e.ToolStrip is ToolStripDropDown)
                {
                    e.Graphics.Clear(Color.FromArgb(55, 71, 79));
                    return;
                }

                base.OnRenderToolStripBackground(e);
            }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                if (e.ToolStrip is ToolStripDropDown)
                {
                    Pen p = new Pen(Color.FromArgb(41, 42, 46));
                    if (e.ToolStrip is ToolStripOverflow)
                    {
                        e.Graphics.DrawLines(p,
                            new[]
                            {
                                e.AffectedBounds.Location,
                                new Point(e.AffectedBounds.Left, e.AffectedBounds.Bottom - 1),
                                new Point(e.AffectedBounds.Right - 1, e.AffectedBounds.Bottom - 1),
                                new Point(e.AffectedBounds.Right - 1, e.AffectedBounds.Top),
                                new Point(e.AffectedBounds.Left, e.AffectedBounds.Top)
                            });
                    }
                    else
                    {
                        e.Graphics.DrawLines(p,
                            new[]
                            {
                                new Point(e.AffectedBounds.Left + e.ConnectedArea.Left, e.AffectedBounds.Top),
                                e.AffectedBounds.Location,
                                new Point(e.AffectedBounds.Left, e.AffectedBounds.Bottom - 1),
                                new Point(e.AffectedBounds.Right - 1, e.AffectedBounds.Bottom - 1),
                                new Point(e.AffectedBounds.Right - 1, e.AffectedBounds.Top),
                                new Point(e.AffectedBounds.Left + e.ConnectedArea.Right, e.AffectedBounds.Top)
                            });
                    }
                    return;
                }

                if (e.ToolStrip is MenuStrip || e.ToolStrip is StatusStrip)
                {
                    return;
                }

                using (Pen p = new Pen(Color.FromArgb(41, 42, 46)))
                {
                    e.Graphics.DrawLine(p, new Point(e.ToolStrip.Left, e.ToolStrip.Bottom - 1),
                        new Point(e.ToolStrip.Width, e.ToolStrip.Bottom - 1));
                }

                base.OnRenderToolStripBorder(e);
            }
        }

        #endregion Skinning

        private void autoModeHelperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new DlgAutomaticModeHelper(CurrentProfile)).ShowDialog();
        }
    }

    public class CategorySort
    {
        public int Column;
        public SortOrder Order;
    }
}