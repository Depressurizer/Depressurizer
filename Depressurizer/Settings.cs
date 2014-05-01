/*
Copyright 2011, 2012 Steve Labbe.

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

    class Settings : AppSettings {

        private static Settings instance;

        public static Settings Instance() {
            if( instance == null ) {
                instance = new Settings();
            }
            return instance;
        }

        public string SettingsVersion {
            get {
                return "0.3";
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

        private bool _ignoreDlc = true;
        public bool IgnoreDlc {
            get {
                return _ignoreDlc;
            }
            set {
                if( _ignoreDlc != value ) {
                    _ignoreDlc = value;
                    outOfDate = true;
                }
            }
        }

        private bool _fullAutocat = false;
        public bool FullAutocat {
            get {
                return _fullAutocat;
            }
            set {
                if( _fullAutocat != value ) {
                    _fullAutocat = value;
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

        private Settings()
            : base() {
            FilePath = System.Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ) + @"\Depressurizer\Settings.xml";
        }
    }
}
