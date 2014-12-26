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
using System.Windows.Forms;
using Rallion;
using NDesk.Options;

namespace Depressurizer {
    static class Program {

        public static AppLogger Logger;
        public static GameDB GameDB;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main( string[] args ) {
            FatalError.InitializeHandler();

            Logger = new AppLogger();
            Logger.Level = LoggerLevel.None;
            Logger.DateFormat = "HH:mm:ss'.'ffffff";

            Logger.MaxFileSize = 2000000;
            Logger.MaxBackup = 1;
            Logger.FileNameTemplate = "Depressurizer.log";

            Settings.Instance.Load();

            Logger.Write( LoggerLevel.Info, GlobalStrings.Program_ProgramInitialized, Logger.Level );

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );

            AutomaticModeOptions autoOpts = ParseAutoOptions( args );

            if( autoOpts != null ) {

                Application.Run( new AutomaticModeForm( autoOpts ) );

            } else {

                Application.Run( new FormMain() );

            }
            Settings.Instance.Save();

            Logger.Write( LoggerLevel.Info, GlobalStrings.Program_ProgramClosing );
            Logger.EndSession();
        }

        static AutomaticModeOptions ParseAutoOptions( string[] args ) {
            AutomaticModeOptions config = new AutomaticModeOptions();
            bool auto = false;

            var opts = new OptionSet() {
                { "auto",       v => auto = true },
                { "p|profile=", v => config.CustomProfile = v },
                { "nocheck",    v => config.CheckSteam = false },
                { "noupdate",   v => config.UpdateGameList = false },
                { "import",     v => config.ImportSteamCategories = true },
                { "noappinfo",  v => config.UpdateAppInfo = false },
                { "scrapedb",   v => config.ScrapeUnscrapedGames = true },
                { "nodbsave",   v => config.SaveDBChanges = false },
                { "nosave",     v => config.SaveProfile = false },
                { "noexport",   v => config.ExportToSteam = false },
                { "launch",     v => config.SteamLaunch = SteamLaunchType.Normal },
                { "launchbp",   v => config.SteamLaunch = SteamLaunchType.BigPicture},
                { "tolerant",   v => config.TolerateMinorErrors = true },
                { "quiet",      v => config.AutoClose = AutoCloseType.UnlessError},
                { "silent",     v => config.AutoClose = AutoCloseType.Always },
                { "all",        v => config.ApplyAllAutoCats = true },
                { "<>",         v => config.AutoCats.Add( v ) }
            };

            opts.Parse( args );

            return auto ? config : null;
        }
    }


}
