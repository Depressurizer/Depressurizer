using System.Globalization;
using System.IO;
using System.Threading;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Interfaces;
using Depressurizer.Helpers;
using Newtonsoft.Json;

namespace Depressurizer
{
    internal sealed class Settings : ISettings
    {
        #region Static Fields

        private static readonly object SyncRoot = new object();

        private static volatile Settings _instance;

        #endregion

        #region Fields

        public int SplitBrowserContainerWidth = 722;

        public int SplitGameContainerHeight = 510;

        private int _height;

        private int _splitBrowser;

        private int _splitContainer;

        private int _splitGame;

        private StoreLanguage _storeLanguage = StoreLanguage.English;

        private InterfaceLanguage _userLanguage = InterfaceLanguage.English;

        private int _width;

        #endregion

        #region Constructors and Destructors

        private Settings() { }

        #endregion

        #region Public Properties

        public static Settings Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                lock (SyncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new Settings();
                    }
                }

                return _instance;
            }
        }

        public string AutoCats { get; set; }

        public bool AutoSaveDatabase { get; set; } = true;

        public string Category { get; set; }

        public bool CheckForDepressurizerUpdates { get; set; } = true;

        public int ConfigBackupCount { get; set; } = 3;

        public string Filter { get; set; }

        /// <inheritdoc />
        public int Height
        {
            get
            {
                if (_height <= 350)
                {
                    Height = 600;
                }

                return _height;
            }
            set => _height = value;
        }

        public bool IncludeImputedTimes { get; set; } = true;

        /// <summary>
        ///     Depressurizer interface language.
        /// </summary>
        public InterfaceLanguage InterfaceLanguage
        {
            get => _userLanguage;
            set
            {
                if (_userLanguage == value)
                {
                    return;
                }

                _userLanguage = value;
                ChangeLanguage(_userLanguage);
            }
        }

        public GameListSource ListSource { get; set; } = GameListSource.XmlPreferred;

        public string LstGamesState { get; set; } = "";

        public string ProfileToLoad { get; set; }

        public bool RemoveExtraEntries { get; set; } = true;

        public int ScrapePromptDays { get; set; } = 30;

        public bool SingleCatMode { get; set; }

        public int SplitBrowser
        {
            get
            {
                if (_splitBrowser <= 100)
                {
                    SplitBrowser = SplitBrowserContainerWidth - 300;
                }

                return _splitBrowser;
            }
            set => _splitBrowser = value;
        }

        public int SplitContainer
        {
            get
            {
                if (_splitContainer <= 100)
                {
                    SplitContainer = 250;
                }

                return _splitContainer;
            }
            set => _splitContainer = value;
        }

        public int SplitGame
        {
            get
            {
                if (_splitGame <= 100)
                {
                    SplitGame = SplitGameContainerHeight - 150;
                }

                return _splitGame;
            }
            set => _splitGame = value;
        }

        public StartupAction StartupAction { get; set; } = StartupAction.Create;

        public string SteamPath { get; set; }

        /// <summary>
        ///     Language of the Steam Store. Used for the in-app browser and for scraping the Steam Store pages.
        /// </summary>
        public StoreLanguage StoreLanguage
        {
            get => _storeLanguage;
            set
            {
                if (_storeLanguage == value)
                {
                    return;
                }

                _storeLanguage = value;
                Database.ChangeLanguage(_storeLanguage);
            }
        }

        public bool UpdateAppInfoOnStart { get; set; } = true;

        public bool UpdateHltbOnStart { get; set; } = true;

        /// <inheritdoc />
        public int Width
        {
            get
            {
                if (_width <= 600)
                {
                    Width = 1000;
                }

                return _width;
            }
            set => _width = value;
        }

        /// <inheritdoc />
        public int X { get; set; }

        /// <inheritdoc />
        public int Y { get; set; }

        #endregion

        #region Properties

        private static Database Database => Database.Instance;

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
        public void Load()
        {
            Load(Locations.File.Settings);
        }

        /// <inheritdoc />
        public void Load(string path)
        {
            lock (SyncRoot)
            {
                Logger.Info("Settings: Loading a settings instance from '{0}'.", path);
                if (!File.Exists(path))
                {
                    Logger.Warn("Settings: Couldn't find a settings file at '{0}'.", path);

                    return;
                }

                using (StreamReader reader = File.OpenText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    _instance = (Settings) serializer.Deserialize(reader, typeof(Settings));
                }
            }
        }

        /// <inheritdoc />
        public void Save()
        {
            Save(Locations.File.Settings);
        }

        /// <inheritdoc />
        public void Save(string path)
        {
            lock (SyncRoot)
            {
                Logger.Info("Settings: Saving current settings instance to '{0}'.", path);

                using (StreamWriter writer = File.CreateText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, _instance);
                }
            }
        }

        #endregion

        #region Methods

        private static void ChangeLanguage(InterfaceLanguage language)
        {
            CultureInfo newCulture = Language.GetCultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = newCulture;
        }

        #endregion
    }
}
