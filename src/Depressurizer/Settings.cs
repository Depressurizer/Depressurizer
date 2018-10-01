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
using System.Globalization;
using System.Threading;
using Rallion;

namespace Depressurizer
{
    internal enum StartupAction
    {
        None,
        Load,
        Create
    }

    internal enum GameListSource
    {
        XmlPreferred,
        XmlOnly,
        WebsiteOnly
    }

    internal enum UILanguage
    {
        windows,
        en, // English
        es, // Spanish
        ru, // Russian
        uk, // Ukranian
        nl // Dutch
    }

    public enum StoreLanguage
    {
        windows,
        bg, // Bulgarian
        cs, // Czech
        da, // Danish
        nl, // Dutch
        en, // English
        fi, // Finnish
        fr, // French
        de, // German
        el, // Greek
        hu, // Hungarian
        it, // Italian
        ja, // Japanese
        ko, // Korean
        no, // Norwegian
        pl, // Polish
        pt, // Portuguese
        pt_BR, // Portuguese (Brasil)
        ro, // Romanian
        ru, // Russian
        zh_Hans, // Simplified Chinese
        es, // Spanish
        sv, // Swedish
        th, // Thai
        zh_Hant, // Traditional Chinese
        tr, // Turkish
        uk // Ukrainian
    }

    internal class Settings : AppSettings
    {
        private static Settings instance;

        private string _autocats;

        private bool _autosaveDB = true;

        private string _category;

        private bool _checkForDepressurizerUpdates = true;

        private int _configBackupCount = 3;

        private string _filter;

        private int _height;

        private bool _IncludeImputedTimes = true;

        private GameListSource _listSource = GameListSource.XmlPreferred;

        private int _logBackups = 1;

        private LoggerLevel _logLevel = LoggerLevel.Info;

        private int _logSize = 2000000;

        private string _lstGamesState = "";

        private string _profileToLoad;

        private bool _removeExtraEntries = true;

        private int _scrapePromptDays = 30;

        private bool _singleCatMode;
        private int _splitBrowser;

        private int _splitContainer;
        private int _splitGame;

        private StartupAction _startupAction = StartupAction.Create;

        private string _steamPath;

        //Language of steam store. Used in browser and when scraping tags, genres, etc
        private StoreLanguage _storeLanguage = StoreLanguage.windows;

        private bool _updateAppInfoOnStart = true;

        private bool _updateHltbOnStart = true;

        //Depressurizer UI language
        private UILanguage _userLanguage = UILanguage.windows;

        private int _width;

        private int _x;

        private int _y;

        public int SplitBrowserContainerWidth = 722;

        public int SplitGameContainerHeight = 510;

        private Settings()
        {
            FilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                       @"\Depressurizer\Settings.xml";
        }

        public static Settings Instance => instance ?? (instance = new Settings());

        public int SettingsVersion => 3;

        public int X
        {
            get => _x;
            set
            {
                if (_x != value)
                {
                    _x = value;
                    outOfDate = true;
                }
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                if (_y != value)
                {
                    _y = value;
                    outOfDate = true;
                }
            }
        }

        public int Height
        {
            get
            {
                if (_height <= 350) Height = 600;
                return _height;
            }
            set
            {
                if (_height != value)
                {
                    _height = value;
                    outOfDate = true;
                }
            }
        }

        public int Width
        {
            get
            {
                if (_width <= 600) Width = 1000;
                return _width;
            }
            set
            {
                if (_width != value)
                {
                    _width = value;
                    outOfDate = true;
                }
            }
        }

        public int SplitContainer
        {
            get
            {
                if (_splitContainer <= 100) SplitContainer = 250;
                return _splitContainer;
            }
            set
            {
                if (_splitContainer != value)
                {
                    _splitContainer = value;
                    outOfDate = true;
                }
            }
        }

        public int SplitGame
        {
            get
            {
                if (_splitGame <= 100) SplitGame = SplitGameContainerHeight - 150;
                return _splitGame;
            }
            set
            {
                if (_splitGame != value)
                {
                    _splitGame = value;
                    outOfDate = true;
                }
            }
        }

        public int SplitBrowser
        {
            get
            {
                if (_splitBrowser <= 100) SplitBrowser = SplitBrowserContainerWidth - 300;
                return _splitBrowser;
            }
            set
            {
                if (_splitBrowser != value)
                {
                    _splitBrowser = value;
                    outOfDate = true;
                }
            }
        }

        public string Filter
        {
            get => _filter;
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    outOfDate = true;
                }
            }
        }

        public string Category
        {
            get => _category;
            set
            {
                if (_category != value)
                {
                    _category = value;
                    outOfDate = true;
                }
            }
        }

        public string AutoCats
        {
            get => _autocats;
            set
            {
                if (_autocats != value)
                {
                    _autocats = value;
                    outOfDate = true;
                }
            }
        }

        public string SteamPath
        {
            get => _steamPath;
            set
            {
                if (_steamPath != value)
                {
                    _steamPath = value;
                    outOfDate = true;
                }
            }
        }

        public int ConfigBackupCount
        {
            get => _configBackupCount;
            set
            {
                if (_configBackupCount != value)
                {
                    _configBackupCount = value;
                    outOfDate = true;
                }
            }
        }

        public StartupAction StartupAction
        {
            get => _startupAction;
            set
            {
                if (_startupAction != value)
                {
                    _startupAction = value;
                    outOfDate = true;
                }
            }
        }

        public string ProfileToLoad
        {
            get => _profileToLoad;
            set
            {
                if (_profileToLoad != value)
                {
                    _profileToLoad = value;
                    outOfDate = true;
                }
            }
        }

        public bool UpdateAppInfoOnStart
        {
            get => _updateAppInfoOnStart;
            set
            {
                if (_updateAppInfoOnStart != value)
                {
                    _updateAppInfoOnStart = value;
                    outOfDate = true;
                }
            }
        }

        public bool UpdateHltbOnStart
        {
            get => _updateHltbOnStart;
            set
            {
                if (_updateHltbOnStart != value)
                {
                    _updateHltbOnStart = value;
                    outOfDate = true;
                }
            }
        }

        public bool IncludeImputedTimes
        {
            get => _IncludeImputedTimes;
            set
            {
                if (_IncludeImputedTimes != value)
                {
                    _IncludeImputedTimes = value;
                    outOfDate = true;
                }
            }
        }

        public bool AutosaveDB
        {
            get => _autosaveDB;
            set
            {
                if (_autosaveDB != value)
                {
                    _autosaveDB = value;
                    outOfDate = true;
                }
            }
        }

        public int ScrapePromptDays
        {
            get => _scrapePromptDays;
            set
            {
                if (_scrapePromptDays != value)
                {
                    _scrapePromptDays = value;
                    outOfDate = true;
                }
            }
        }

        public bool CheckForDepressurizerUpdates
        {
            get => _checkForDepressurizerUpdates;
            set
            {
                if (_checkForDepressurizerUpdates != value)
                {
                    _checkForDepressurizerUpdates = value;
                    outOfDate = true;
                }
            }
        }

        public bool RemoveExtraEntries
        {
            get => _removeExtraEntries;
            set
            {
                if (_removeExtraEntries != value)
                {
                    _removeExtraEntries = value;
                    outOfDate = true;
                }
            }
        }

        public GameListSource ListSource
        {
            get => _listSource;
            set
            {
                if (_listSource != value)
                {
                    _listSource = value;
                    outOfDate = true;
                }
            }
        }

        public LoggerLevel LogLevel
        {
            get => _logLevel;
            set
            {
                Program.Logger.Level = value;
                if (_logLevel != value)
                {
                    _logLevel = value;
                    outOfDate = true;
                }
            }
        }

        public int LogSize
        {
            get => _logSize;
            set
            {
                Program.Logger.MaxFileSize = value;
                if (_logSize != value)
                {
                    _logSize = value;
                    outOfDate = true;
                }
            }
        }

        public int LogBackups
        {
            get => _logBackups;
            set
            {
                Program.Logger.MaxBackup = value;
                if (_logBackups != value)
                {
                    _logBackups = value;
                    outOfDate = true;
                }
            }
        }

        public StoreLanguage StoreLang
        {
            get => _storeLanguage;
            set
            {
                if (_storeLanguage != value)
                {
                    _storeLanguage = value;
                    outOfDate = true;
                    if (Program.GameDB != null) Program.GameDB.ChangeLanguage(_storeLanguage);
                }
            }
        }

        public UILanguage UserLang
        {
            get => _userLanguage;
            set
            {
                if (_userLanguage != value)
                {
                    _userLanguage = value;
                    outOfDate = true;
                    changeLanguage(_userLanguage);
                }
            }
        }

        public bool SingleCatMode
        {
            get => _singleCatMode;
            set
            {
                if (_singleCatMode != value)
                {
                    _singleCatMode = value;
                    outOfDate = true;
                }
            }
        }

        public string LstGamesState
        {
            get => _lstGamesState;
            set
            {
                if (_lstGamesState != value)
                {
                    _lstGamesState = value;
                    outOfDate = true;
                }
            }
        }

        private void changeLanguage(UILanguage userLanguage)
        {
            CultureInfo newCulture;

            switch (userLanguage)
            {
                case UILanguage.en:
                    newCulture = new CultureInfo("en");
                    break;
                case UILanguage.es:
                    newCulture = new CultureInfo("es");
                    break;
                case UILanguage.ru:
                    newCulture = new CultureInfo("ru");
                    break;
                case UILanguage.uk:
                    newCulture = new CultureInfo("uk");
                    break;
                case UILanguage.nl:
                    newCulture = new CultureInfo("nl");
                    break;
                default:
                    newCulture = Thread.CurrentThread.CurrentCulture;
                    break;
            }

            Thread.CurrentThread.CurrentUICulture = newCulture;
        }

        public override void Load()
        {
            base.Load();
            //   Program.Logger.Level = LogLevel;
        }
    }
}