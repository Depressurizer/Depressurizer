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

    class DepSettings : AppSettings {

        private static DepSettings instance;

        public static DepSettings Instance() {
            if( instance == null ) {
                instance = new DepSettings();
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
                lock( threadLock ) {
                    return _steamPath;
                }
            }
            set {
                lock( threadLock ) {
                    _steamPath = value;
                    outOfDate = true;
                }
            }
        }

        private StartupAction _startupAction = StartupAction.Create;
        public StartupAction StartupAction {
            get {
                lock( threadLock ) {
                    return _startupAction;
                }
            }
            set {
                lock( threadLock ) {
                    _startupAction = value;
                    outOfDate = true;
                }
            }
        }

        private string _profileToLoad = null;
        public string ProfileToLoad {
            get {
                lock( threadLock ) {
                    return _profileToLoad;
                }
            }
            set {
                lock( threadLock ) {
                    _profileToLoad = value;
                    outOfDate = true;
                }
            }
        }

        private bool _removeExtraEntries = true;
        public bool RemoveExtraEntries {
            get {
                lock( threadLock ) {
                    return _removeExtraEntries;
                }
            }
            set {
                lock( threadLock ) {
                    _removeExtraEntries = value;
                    outOfDate = true;
                }
            }
        }

        private bool _ignoreDlc = true;
        public bool IgnoreDlc {
            get {
                lock( threadLock ) {
                    return _ignoreDlc;
                }
            }
            set {
                lock( threadLock ) {
                    _ignoreDlc = value;
                    outOfDate = true;
                }
            }
        }

        private DepSettings()
            : base() {
            FilePath = System.Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ) + @"\Depressurizer\Settings.xml";
        }
    }
}
