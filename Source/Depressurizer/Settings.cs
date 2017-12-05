#region License

//     This file (Settings.cs) is part of Depressurizer.
//     Copyright (C) 2017  Martijn Vegter
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
//     along with this program.  If not, see <https://www.gnu.org/licenses/>.

#endregion

using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Depressurizer.Helpers;
using Newtonsoft.Json;

namespace Depressurizer
{
    /// <summary>
    ///     Depressurizer Settings
    /// </summary>
    public sealed class Settings
    {
        private static volatile Settings _instance;
        private static readonly object SyncRoot = new object();
        private string _autoCats;
        private bool _autosaveDb = true;
        private string _category;
        private bool _checkForUpdates = true;
        private int _configBackupCount = 3;
        private string _filter;
        private int _height;
        private bool _includeImputedTimes = true;
        private InterfaceLanguage _interfaceLanguage = InterfaceLanguage.English;
        private string _lastGamesState = "";
        private GameListSource _listSource = GameListSource.XmlPreferred;
        private string _profileToLoad;
        private bool _removeExtraEntries = true;
        private int _scrapePromptDays = 30;
        private bool _singleCatMode;
        private int _splitBrowser;
        private int _splitBrowserContainerWidth = 722;
        private int _splitContainer;
        private int _splitGame;
        private int _splitGameContainerHeight = 510;
        private StartupAction _startupAction = StartupAction.Create;
        private string _steamPath;
        private StoreLanguage _storeLanguage = StoreLanguage.windows;
        private bool _updateAppInfoOnStart = true;
        private bool _updateHowLongToBeatOnStart = true;
        private int _width;
        private int _x;
        private int _y;

        /// <summary>
        ///     Only instance of the Settings controller
        /// </summary>
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

        public string AutoCats
        {
            get
            {
                lock (SyncRoot)
                {
                    return _autoCats;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _autoCats = value;
                }
            }
        }

        /// <summary>
        ///     Automatically save database
        /// </summary>
        public bool AutoSaveDatabase
        {
            get
            {
                lock (SyncRoot)
                {
                    return _autosaveDb;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _autosaveDb = value;
                }
            }
        }

        /// <summary>
        ///     Active category
        /// </summary>
        public string Category
        {
            get
            {
                lock (SyncRoot)
                {
                    return _category;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _category = value;
                }
            }
        }

        /// <summary>
        ///     Check for updates
        /// </summary>
        public bool CheckForUpdates
        {
            get
            {
                lock (SyncRoot)
                {
                    return _checkForUpdates;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _checkForUpdates = value;
                }
            }
        }

        /// <summary>
        ///     Number of backup of the config
        /// </summary>
        public int ConfigBackupCount
        {
            get
            {
                lock (SyncRoot)
                {
                    return _configBackupCount;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _configBackupCount = value;
                }
            }
        }

        /// <summary>
        ///     Active filter
        /// </summary>
        public string Filter
        {
            get
            {
                lock (SyncRoot)
                {
                    return _filter;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _filter = value;
                }
            }
        }

        /// <summary>
        ///     Application height
        /// </summary>
        public int Height
        {
            get
            {
                lock (SyncRoot)
                {
                    if (_height <= 350)
                    {
                        Height = 600;
                    }
                    return _height;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _height = value;
                }
            }
        }

        public bool IncludeImputedTimes
        {
            get
            {
                lock (SyncRoot)
                {
                    return _includeImputedTimes;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _includeImputedTimes = value;
                }
            }
        }

        public InterfaceLanguage InterfaceLanguage
        {
            get
            {
                lock (SyncRoot)
                {
                    return _interfaceLanguage;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _interfaceLanguage = value;
                    ChangeLanguage(_interfaceLanguage);
                }
            }
        }

        public string LastGamesState
        {
            get
            {
                lock (SyncRoot)
                {
                    return _lastGamesState;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _lastGamesState = value;
                }
            }
        }

        /// <summary>
        ///     User's list source
        /// </summary>
        public GameListSource ListSource
        {
            get
            {
                lock (SyncRoot)
                {
                    return _listSource;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _listSource = value;
                }
            }
        }

        /// <summary>
        ///     User profile to load
        /// </summary>
        public string ProfileToLoad
        {
            get
            {
                lock (SyncRoot)
                {
                    return _profileToLoad;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _profileToLoad = value;
                }
            }
        }

        public bool RemoveExtraEntries
        {
            get
            {
                lock (SyncRoot)
                {
                    return _removeExtraEntries;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _removeExtraEntries = value;
                }
            }
        }

        public int ScrapePromptDays
        {
            get
            {
                lock (SyncRoot)
                {
                    return _scrapePromptDays;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _scrapePromptDays = value;
                }
            }
        }

        public bool SingleCatMode
        {
            get
            {
                lock (SyncRoot)
                {
                    return _singleCatMode;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _singleCatMode = value;
                }
            }
        }

        public int SplitBrowser
        {
            get
            {
                lock (SyncRoot)
                {
                    if (_splitBrowser <= 100)
                    {
                        SplitBrowser = SplitBrowserContainerWidth - 300;
                    }
                    return _splitBrowser;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _splitBrowser = value;
                }
            }
        }

        public int SplitBrowserContainerWidth
        {
            get
            {
                lock (SyncRoot)
                {
                    return _splitBrowserContainerWidth;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _splitBrowserContainerWidth = value;
                }
            }
        }

        public int SplitContainer
        {
            get
            {
                lock (SyncRoot)
                {
                    if (_splitContainer <= 100)
                    {
                        SplitContainer = 250;
                    }
                    return _splitContainer;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _splitContainer = value;
                }
            }
        }

        public int SplitGame
        {
            get
            {
                lock (SyncRoot)
                {
                    if (_splitGame <= 100)
                    {
                        SplitGame = SplitGameContainerHeight - 150;
                    }
                    return _splitGame;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _splitGame = value;
                }
            }
        }

        public int SplitGameContainerHeight
        {
            get
            {
                lock (SyncRoot)
                {
                    return _splitGameContainerHeight;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _splitGameContainerHeight = value;
                }
            }
        }


        /// <summary>
        ///     Depressurizer Startup action
        /// </summary>
        public StartupAction StartupAction
        {
            get
            {
                lock (SyncRoot)
                {
                    return _startupAction;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _startupAction = value;
                }
            }
        }

        /// <summary>
        ///     Steam installation folder
        /// </summary>
        public string SteamPath
        {
            get
            {
                lock (SyncRoot)
                {
                    return _steamPath;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _steamPath = value;
                }
            }
        }

        /// <summary>
        ///     Language for scraping
        /// </summary>
        public StoreLanguage StoreLang
        {
            get
            {
                lock (SyncRoot)
                {
                    return _storeLanguage;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _storeLanguage = value;
                    if (Program.GameDB != null)
                    {
                        Program.GameDB.ChangeLanguage(_storeLanguage);
                    }
                }
            }
        }

        /// <summary>
        ///     Update AppInfo on startup
        /// </summary>
        public bool UpdateAppInfoOnStart
        {
            get
            {
                lock (SyncRoot)
                {
                    return _updateAppInfoOnStart;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _updateAppInfoOnStart = value;
                }
            }
        }

        /// <summary>
        ///     Update HowLongToBeat on startup
        /// </summary>
        public bool UpdateHowLongToBeatOnStart
        {
            get
            {
                lock (SyncRoot)
                {
                    return _updateHowLongToBeatOnStart;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _updateHowLongToBeatOnStart = value;
                }
            }
        }

        /// <summary>
        ///     Application width
        /// </summary>
        public int Width
        {
            get
            {
                lock (SyncRoot)
                {
                    if (_width <= 600)
                    {
                        Width = 1000;
                    }
                    return _width;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _width = value;
                }
            }
        }

        /// <summary>
        ///     X-position
        /// </summary>
        public int X
        {
            get
            {
                lock (SyncRoot)
                {
                    return _x;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _x = value;
                }
            }
        }

        /// <summary>
        ///     Y-position
        /// </summary>
        public int Y
        {
            get
            {
                lock (SyncRoot)
                {
                    return _y;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _y = value;
                }
            }
        }

        private Settings() { }

        /// <summary>
        ///     Load settings
        /// </summary>
        public void Load()
        {
            lock (SyncRoot)
            {
                if (!File.Exists(Location.File.Settings))
                {
                    _instance = new Settings();
                    return;
                }

                string jsonSettings = File.ReadAllText(Location.File.Settings);
                _instance = JsonConvert.DeserializeObject<Settings>(jsonSettings);
            }
        }

        /// <summary>
        ///     Save settings
        /// </summary>
        public void Save()
        {
            lock (SyncRoot)
            {
                try
                {
                    string jsonSettings = JsonConvert.SerializeObject(_instance, Program.SerializerSettings);
                    File.WriteAllText(Location.File.Settings, jsonSettings);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        private static void ChangeLanguage(InterfaceLanguage language)
        {
            lock (SyncRoot)
            {
                CultureInfo newCulture;

                switch (language)
                {
                    case InterfaceLanguage.English:
                        newCulture = new CultureInfo("en");
                        break;
                    case InterfaceLanguage.Spanish:
                        newCulture = new CultureInfo("es");
                        break;
                    case InterfaceLanguage.Russian:
                        newCulture = new CultureInfo("ru");
                        break;
                    case InterfaceLanguage.Ukrainian:
                        newCulture = new CultureInfo("uk");
                        break;
                    case InterfaceLanguage.Dutch:
                        newCulture = new CultureInfo("nl");
                        break;
                    default:
                        newCulture = Thread.CurrentThread.CurrentCulture;
                        break;
                }

                Thread.CurrentThread.CurrentUICulture = newCulture;
            }
        }
    }
}