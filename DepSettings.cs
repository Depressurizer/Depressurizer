using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Depressurizer {
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

        private bool _loadProfileOnStartup = false;
        public bool LoadProfileOnStartup {
            get {
                lock( threadLock ) {
                    return _loadProfileOnStartup;
                }
            }
            set {
                lock( threadLock ) {
                    _loadProfileOnStartup = value;
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

        private DepSettings() : base() {
            //FilePath = System.Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ) + @"\Depressurizer\Settings.xml";
        }
    }
}
