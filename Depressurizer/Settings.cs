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

namespace Depressurizer {

    enum StartupAction {
        None,
        Load,
        Create
    }

    enum GameListSource {
        XmlPreferred,
        XmlOnly,
        WebsiteOnly
    }

    enum UserLanguage
    {
        windows,
        en,
        es
    }

    class Settings : AppSettings {

        private static Settings instance;

        public static Settings Instance {
            get {
                if( instance == null ) {
                    instance = new Settings();
                }
                return instance;
            }
        }

        public int SettingsVersion {
            get {
                return 2;
            }
        }

        private string _steamPath = null;
        public string SteamPath {
            get {
                return _steamPath;
            }
            set {
                if( _steamPath != value ) {
                    _steamPath = value;
                    outOfDate = true;
                }
            }
        }

        private int _configBackupCount = 3;
        public int ConfigBackupCount {
            get {
                return _configBackupCount;
            }
            set {
                if( _configBackupCount != value ) {
                    _configBackupCount = value;
                    outOfDate = true;
                }
            }
        }

        private StartupAction _startupAction = StartupAction.Create;
        public StartupAction StartupAction {
            get {
                return _startupAction;
            }
            set {
                if( _startupAction != value ) {
                    _startupAction = value;
                    outOfDate = true;
                }
            }
        }

        private string _profileToLoad = null;
        public string ProfileToLoad {
            get {
                return _profileToLoad;
            }
            set {
                if( _profileToLoad != value ) {
                    _profileToLoad = value;
                    outOfDate = true;
                }
            }
        }

        private bool _updateAppInfoOnStart = true;
        public bool UpdateAppInfoOnStart {
            get {
                return _updateAppInfoOnStart;
            }
            set {
                if( _updateAppInfoOnStart != value ) {
                    _updateAppInfoOnStart = value;
                    outOfDate = true;
                }
            }
        }

        private bool _updateHltbOnStart = true;
        public bool UpdateHltbOnStart
        {
            get
            {
                return _updateHltbOnStart;
            }
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
            get
            {
                return _IncludeImputedTimes;
            }
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
        public bool AutosaveDB {
            get {
                return _autosaveDB;
            }
            set {
                if( _autosaveDB != value ) {
                    _autosaveDB = value;
                    outOfDate = true;
                }
            }
        }

        private bool _checkForDepressurizerUpdates = true;
        public bool CheckForDepressurizerUpdates
        {
            get
            {
                return _checkForDepressurizerUpdates;
            }
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
        public bool RemoveExtraEntries {
            get {
                return _removeExtraEntries;
            }
            set {
                if( _removeExtraEntries != value ) {
                    _removeExtraEntries = value;
                    outOfDate = true;
                }
            }
        }

        private GameListSource _listSource = GameListSource.XmlPreferred;
        public GameListSource ListSource {
            get {
                return _listSource;
            }
            set {
                if( _listSource != value ) {
                    _listSource = value;
                    outOfDate = true;
                }
            }
        }

        private LoggerLevel _logLevel = LoggerLevel.Info;
        public LoggerLevel LogLevel {
            get {
                return _logLevel;
            }
            set {
                Program.Logger.Level = value;
                if( _logLevel != value ) {
                    _logLevel = value;
                    outOfDate = true;
                }
            }
        }

        private int _logSize = 2000000;
        public int LogSize {
            get {
                return _logSize;
            }
            set {
                Program.Logger.MaxFileSize = value;
                if( _logSize != value ) {
                    _logSize = value;
                    outOfDate = true;
                }
            }
        }

        private int _logBackups = 1;
        public int LogBackups {
            get {
                return _logBackups;
            }
            set {
                Program.Logger.MaxBackup = value;
                if( _logBackups != value ) {
                    _logBackups = value;
                    outOfDate = true;
                }
            }
        }

        private UserLanguage _userLanguage = UserLanguage.windows;
        public UserLanguage UserLang
        {
            get
            {
                return _userLanguage;
            }
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

        private void changeLanguage(UserLanguage userLanguage)
        {
            CultureInfo newCulture;

            switch (userLanguage)
            {
                case UserLanguage.en:
                    newCulture = new CultureInfo("en");
                    break;
                case UserLanguage.es:
                    newCulture = new CultureInfo("es");
                    break;
                default:
                    newCulture = Thread.CurrentThread.CurrentCulture;
                    break;
            }

            Thread.CurrentThread.CurrentUICulture = newCulture;
        }

        private bool _singleCatMode = false;
        public bool SingleCatMode {
            get {
                return _singleCatMode;
            }
            set {
                if( _singleCatMode != value ) {
                    _singleCatMode = value;
                    outOfDate = true;
                }
            }
        }

        private string _lstGamesState = "";
        public string LstGamesState
        {
            get
            {
                return _lstGamesState;
            }
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
            : base() {
            FilePath = System.Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ) + @"\Depressurizer\Settings.xml";
        }

        public override void Load() {
            base.Load();
         //   Program.Logger.Level = LogLevel;
        }
    }
}
