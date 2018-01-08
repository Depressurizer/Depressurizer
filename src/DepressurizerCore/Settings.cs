#region LICENSE

//     This file (Settings.cs) is part of DepressurizerCore.
//     Copyright (C) 2018  Martijn Vegter
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
using DepressurizerCore.Helpers;
using Newtonsoft.Json;

namespace DepressurizerCore
{
    /// <summary>
    ///     Depressurizer Settings Controller
    /// </summary>
    public sealed class Settings
    {
        #region Static Fields

        private static readonly object SyncRoot = new object();

        private static volatile Settings _instance;

        #endregion

        #region Fields

        private int _height;

        private int _splitBrowser;

        private int _splitContainer;

        private int _splitGame;

        private InterfaceLanguage _userLanguage = InterfaceLanguage.English;

        private int _width;

        #endregion

        #region Constructors and Destructors

        private Settings()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Depressurizer Settings Instance
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

        public string AutoCats { get; set; }

        /// <summary>
        ///     Auto Save Database
        /// </summary>
        public bool AutoSaveDatabase { get; set; } = true;

        /// <summary>
        ///     Selected category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        ///     Check for Depressurizer updates
        /// </summary>
        public bool CheckForUpdates { get; set; } = true;

        /// <summary>
        ///     Number of backups of config files
        /// </summary>
        public int ConfigBackupCount { get; set; } = 3;

        /// <summary>
        ///     Active filter
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        ///     Height of MainForm
        /// </summary>
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
        ///     Depressurizer Interface Language
        /// </summary>
        public InterfaceLanguage InterfaceLanguage
        {
            get => _userLanguage;
            set
            {
                _userLanguage = value;
                ChangeLanguage(_userLanguage);
            }
        }

        /// <summary>
        ///     ListGames State
        /// </summary>
        public string ListGamesState { get; set; } = "";

        public GameListSource ListSource { get; set; } = GameListSource.XmlPreferred;

        /// <summary>
        ///     Profile (file) to load
        /// </summary>
        public string ProfileToLoad { get; set; }

        public bool RemoveExtraEntries { get; set; } = true;

        public int ScrapePromptDays { get; set; } = 30;

        /// <summary>
        ///     Single Category Mode
        /// </summary>
        public bool SingleCatMode { get; set; }

        public int SplitBrowser
        {
            get => _splitBrowser;
            set
            {
                if (value <= 100)
                {
                    value = SplitBrowserContainerWidth - 300;
                }

                _splitBrowser = value;
            }
        }

        public int SplitBrowserContainerWidth { get; set; } = 722;

        public int SplitContainer
        {
            get => _splitContainer;
            set
            {
                if (value <= 250)
                {
                    value = 250;
                }

                _splitContainer = value;
            }
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

        /// <summary>
        ///     Action on Startup
        /// </summary>
        public StartupAction StartupAction { get; set; } = StartupAction.Create;

        /// <summary>
        ///     Steam Installation Folder
        /// </summary>
        public string SteamPath { get; set; }

        /// <summary>
        ///     Prefered Steam Store Language, used for scraping
        /// </summary>
        public StoreLanguage StoreLanguage { get; set; } = StoreLanguage.Default;

        /// <summary>
        ///     Update from AppInfo on start
        /// </summary>
        public bool UpdateAppInfoOnStart { get; set; } = true;

        /// <summary>
        ///     Update from HowLongToBeat.com on start
        /// </summary>
        public bool UpdateHLTBOnStart { get; set; } = true;

        /// <summary>
        ///     Width of MainForm
        /// </summary>
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

        /// <summary>
        ///     X-Position of MainForm
        /// </summary>
        public int X { get; set; }

        /// <summary>
        ///     Y-Position of MainForm
        /// </summary>
        public int Y { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Save current settings to a file
        /// </summary>
        public void Load()
        {
            lock (SyncRoot)
            {
                if (!File.Exists(Location.File.Settings))
                {
                    return;
                }

                try
                {
                    string jsonSettings = File.ReadAllText(Location.File.Settings);
                    _instance = JsonConvert.DeserializeObject<Settings>(jsonSettings, new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                    throw;
                }
            }
        }

        /// <summary>
        ///     Load settings from a file
        /// </summary>
        public void Save()
        {
            lock (SyncRoot)
            {
                try
                {
                    string jsonSettings = JsonConvert.SerializeObject(_instance, new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        TypeNameHandling = TypeNameHandling.Auto
                    });

                    File.WriteAllText(Location.File.Settings, jsonSettings);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                    throw;
                }
            }
        }

        #endregion

        #region Methods

        private static void ChangeLanguage(InterfaceLanguage interfaceLanguage)
        {
            CultureInfo newCulture;

            switch (interfaceLanguage)
            {
                case InterfaceLanguage.English:
                    newCulture = new CultureInfo("en-US");

                    break;
                case InterfaceLanguage.Spanish:
                    newCulture = new CultureInfo("es-ES");

                    break;
                case InterfaceLanguage.Russian:
                    newCulture = new CultureInfo("ru-RU");

                    break;
                case InterfaceLanguage.Ukranian:
                    newCulture = new CultureInfo("uk-UA");

                    break;
                case InterfaceLanguage.Dutch:
                    newCulture = new CultureInfo("nl-NL");

                    break;
                default:

                    throw new ArgumentOutOfRangeException(nameof(interfaceLanguage), interfaceLanguage, null);
            }

            Thread.CurrentThread.CurrentUICulture = newCulture;
        }

        #endregion
    }
}