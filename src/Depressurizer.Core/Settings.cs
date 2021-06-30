using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Interfaces;
using Newtonsoft.Json;

namespace Depressurizer.Core
{
    public sealed class Settings : ISettings
    {
        #region Static Fields

        public static readonly IList<int> DefaultIgnoreList = new ReadOnlyCollection<int>(new List<int>
        {
            7,
            8,
            90,
            92,
            205,
            211,
            215,
            218,
            310,
            364,
            459,
            480,
            513,
            563,
            564,
            575,
            629,
            635,
            640,
            644,
            740,
            745,
            753,
            760,
            761,
            764,
            765,
            766,
            767,
            1007,
            1213,
            1255,
            1260,
            1273,
            2145,
            2403,
            4270,
            4940,
            8680,
            8710,
            8730,
            8770,
            12250,
            12750,
            13180,
            13260,
            16830,
            16864,
            16865,
            16871,
            16872,
            16879,
            17505,
            17515,
            17525,
            17535,
            17555,
            17575,
            17585,
            17705,
            43110,
            104700,
            201700,
            224220,
            245550,
            254000,
            254020,
            254040,
            259280,
            285050,
            312710,
            314700,
            321040,
            329950,
            331710,
            344750,
            354850,
            368900,
            373110,
            383030,
            385200,
            388870,
            392050,
            392870,
            395980,
            397620,
            399580,
            407730,
            407740,
            407750,
            410590,
            410700,
            416270,
            428430,
            431270,
            432150,
            447880,
            449630,
            457930,
            457940,
            460660,
            461570,
            462370,
            463620,
            473580,
            473620,
            473630,
            473640,
            473650,
            486340,
            488200,
            491250,
            492240,
            496600,
            501690,
            505440,
            509840,
            516230,
            516700,
            524440,
            526790,
            528210,
            533710,
            545950,
            546450,
            559500,
            559940,
            562020,
            576080,
            576440,
            581620,
            592490,
            603770,
            603780,
            605470,
            654310,
            700580
        });

        private static readonly object SyncRoot = new object();

        private static volatile Settings _instance;

        #endregion

        #region Fields

        private int _height;

        private List<int> _ignoreList;

        private int _splitBrowser;

        private int _splitContainer;

        private int _splitGame;

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

        public bool ReadFromLevelDB { get; set; } = false;

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

        public List<int> IgnoreList
        {
            get => _ignoreList ?? (_ignoreList = new List<int>());
            set => _ignoreList = value;
        }

        public bool IncludeImputedTimes { get; set; } = true;

        /// <summary>
        ///     Depressurizer interface language.
        /// </summary>
        public InterfaceLanguage InterfaceLanguage { get; set; } = InterfaceLanguage.English;

        public string LstGamesState { get; set; } = "";

        public string ProfileToLoad { get; set; }

        public string PremiumServer { get; set; }

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

        public int SplitBrowserContainerWidth { get; set; } = 722;

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

        public int SplitGameContainerHeight { get; set; } = 510;

        public StartupAction StartupAction { get; set; } = StartupAction.Create;

        public string SteamPath { get; set; }

        /// <summary>
        ///     Language of the Steam Store. Used for the in-app browser and for scraping the Steam Store pages.
        /// </summary>
        public StoreLanguage StoreLanguage { get; set; } = StoreLanguage.English;

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
    }
}
