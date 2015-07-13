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
using NDesk.Options;
using Rallion;
using System;
using System.Windows.Forms;

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
                Program.Logger.Write( LoggerLevel.Info, "Automatic mode set, loading automatic mode form." );
                Program.Logger.WriteObject( LoggerLevel.Verbose, autoOpts, "Automatic Mode Options:" );
                Application.Run( new AutomaticModeForm( autoOpts ) );

            } else {
                Program.Logger.Write( LoggerLevel.Info, "Automatic mode not set, loading main form." );
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
                { "auto",           v => auto = true },
                { "p|profile=",     v => config.CustomProfile = v },
                { "checksteam",     v => config.CheckSteam = ( v != null ) },
                { "closesteam",     v => config.CloseSteam = ( v != null ) },
                { "updatelib",      v => config.UpdateGameList = ( v != null ) },
                { "import",         v => config.ImportSteamCategories = ( v != null ) },
                { "updatedblocal",  v => config.UpdateAppInfo = ( v != null ) },
                { "updatedbweb",    v => config.ScrapeUnscrapedGames = ( v != null ) },
                { "savedb",         v => config.SaveDBChanges = ( v != null ) },
                { "saveprofile",    v => config.SaveProfile = ( v != null ) },
                { "export",         v => config.ExportToSteam = ( v != null ) },
                { "launch",         v => config.SteamLaunch = SteamLaunchType.Normal },
                { "launchbp",       v => config.SteamLaunch = SteamLaunchType.BigPicture},
                { "tolerant",       v => config.TolerateMinorErrors = ( v != null ) },
                { "quiet",          v => config.AutoClose = AutoCloseType.UnlessError},
                { "silent",         v => config.AutoClose = AutoCloseType.Always },
                { "all",            v => config.ApplyAllAutoCats = ( v != null ) },
                { "<>",             v => config.AutoCats.Add( v ) }
            };

            opts.Parse( args );

            return auto ? config : null;
        }
    }
}
