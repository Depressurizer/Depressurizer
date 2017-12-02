﻿/*
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
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Rallion;

namespace Depressurizer
{
    enum StartupAction
    {
        None,
        Load,
        Create
    }

    enum GameListSource
    {
        XmlPreferred,
        XmlOnly,
        WebsiteOnly
    }

    enum UILanguage
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

    class Settings : AppSettings
    {
        private static Settings instance;

        public static Settings Instance
        {
            get { return instance ?? (instance = new Settings()); }
        }

        public int SettingsVersion
        {
            get { return 3; }
        }

        private int _x;

        public int X
        {
            get { return _x; }
            set
            {
                if (_x != value)
                {
                    _x = value;
                    outOfDate = true;
                }
            }
        }

        private int _y;

        public int Y
        {
            get { return _y; }
            set
            {
                if (_y != value)
                {
                    _y = value;
                    outOfDate = true;
                }
            }
        }

        private int _height;

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
            set
            {
                if (_height != value)
                {
                    _height = value;
                    outOfDate = true;
                }
            }
        }

        private int _width;

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
            set
            {
                if (_width != value)
                {
                    _width = value;
                    outOfDate = true;
                }
            }
        }

        private int _splitContainer;

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
            set
            {
                if (_splitContainer != value)
                {
                    _splitContainer = value;
                    outOfDate = true;
                }
            }
        }

        public int SplitGameContainerHeight = 510;
        private int _splitGame;

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
            set
            {
                if (_splitGame != value)
                {
                    _splitGame = value;
                    outOfDate = true;
                }
            }
        }

        public int SplitBrowserContainerWidth = 722;
        private int _splitBrowser;

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
            set
            {
                if (_splitBrowser != value)
                {
                    _splitBrowser = value;
                    outOfDate = true;
                }
            }
        }

        private string _filter;

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    outOfDate = true;
                }
            }
        }

        private string _category;

        public string Category
        {
            get { return _category; }
            set
            {
                if (_category != value)
                {
                    _category = value;
                    outOfDate = true;
                }
            }
        }

        private string _autocats;

        public string AutoCats
        {
            get { return _autocats; }
            set
            {
                if (_autocats != value)
                {
                    _autocats = value;
                    outOfDate = true;
                }
            }
        }

        private string _steamPath;

        public string SteamPath
        {
            get { return _steamPath; }
            set
            {
                if (_steamPath != value)
                {
                    _steamPath = value;
                    outOfDate = true;
                }
            }
        }

        private int _configBackupCount = 3;

        public int ConfigBackupCount
        {
            get { return _configBackupCount; }
            set
            {
                if (_configBackupCount != value)
                {
                    _configBackupCount = value;
                    outOfDate = true;
                }
            }
        }

        private StartupAction _startupAction = StartupAction.Create;

        public StartupAction StartupAction
        {
            get { return _startupAction; }
            set
            {
                if (_startupAction != value)
                {
                    _startupAction = value;
                    outOfDate = true;
                }
            }
        }

        private string _profileToLoad;

        public string ProfileToLoad
        {
            get { return _profileToLoad; }
            set
            {
                if (_profileToLoad != value)
                {
                    _profileToLoad = value;
                    outOfDate = true;
                }
            }
        }

        private bool _updateAppInfoOnStart = true;

        public bool UpdateAppInfoOnStart
        {
            get { return _updateAppInfoOnStart; }
            set
            {
                if (_updateAppInfoOnStart != value)
                {
                    _updateAppInfoOnStart = value;
                    outOfDate = true;
                }
            }
        }

        private bool _updateHltbOnStart = true;

        public bool UpdateHltbOnStart
        {
            get { return _updateHltbOnStart; }
            set
            {
                if (_updateHltbOnStart != value)
                {
                    _updateHltbOnStart = value;
                    outOfDate = true;
                }
            }
        }

        private bool _IncludeImputedTimes = true;

        public bool IncludeImputedTimes
        {
            get { return _IncludeImputedTimes; }
            set
            {
                if (_IncludeImputedTimes != value)
                {
                    _IncludeImputedTimes = value;
                    outOfDate = true;
                }
            }
        }

        private bool _autosaveDB = true;

        public bool AutosaveDB
        {
            get { return _autosaveDB; }
            set
            {
                if (_autosaveDB != value)
                {
                    _autosaveDB = value;
                    outOfDate = true;
                }
            }
        }

        private int _scrapePromptDays = 30;

        public int ScrapePromptDays
        {
            get { return _scrapePromptDays; }
            set
            {
                if (_scrapePromptDays != value)
                {
                    _scrapePromptDays = value;
                    outOfDate = true;
                }
            }
        }

        private bool _checkForDepressurizerUpdates = true;

        public bool CheckForDepressurizerUpdates
        {
            get { return _checkForDepressurizerUpdates; }
            set
            {
                if (_checkForDepressurizerUpdates != value)
                {
                    _checkForDepressurizerUpdates = value;
                    outOfDate = true;
                }
            }
        }

        private bool _removeExtraEntries = true;

        public bool RemoveExtraEntries
        {
            get { return _removeExtraEntries; }
            set
            {
                if (_removeExtraEntries != value)
                {
                    _removeExtraEntries = value;
                    outOfDate = true;
                }
            }
        }

        private GameListSource _listSource = GameListSource.XmlPreferred;

        public GameListSource ListSource
        {
            get { return _listSource; }
            set
            {
                if (_listSource != value)
                {
                    _listSource = value;
                    outOfDate = true;
                }
            }
        }

        private LoggerLevel _logLevel = LoggerLevel.Info;

        public LoggerLevel LogLevel
        {
            get { return _logLevel; }
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

        private int _logSize = 2000000;

        public int LogSize
        {
            get { return _logSize; }
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

        private int _logBackups = 1;

        public int LogBackups
        {
            get { return _logBackups; }
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

        //Language of steam store. Used in browser and when scraping tags, genres, etc
        private StoreLanguage _storeLanguage = StoreLanguage.windows;

        public StoreLanguage StoreLang
        {
            get { return _storeLanguage; }
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

        //Depressurizer UI language
        private UILanguage _userLanguage = UILanguage.windows;

        public UILanguage UserLang
        {
            get { return _userLanguage; }
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

        private bool _singleCatMode;

        public bool SingleCatMode
        {
            get { return _singleCatMode; }
            set
            {
                if (_singleCatMode != value)
                {
                    _singleCatMode = value;
                    outOfDate = true;
                }
            }
        }

        private string _lstGamesState = "";

        public string LstGamesState
        {
            get { return _lstGamesState; }
            set
            {
                if (_lstGamesState != value)
                {
                    _lstGamesState = value;
                    outOfDate = true;
                }
            }
        }

        private Settings()
        {
            FilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                       @"\Depressurizer\Settings.xml";
        }

        public override void Load()
        {
            base.Load();
            //   Program.Logger.Level = LogLevel;
        }
    }
}