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
using Rallion;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;


namespace Depressurizer {

    public partial class AutomaticModeForm : Form {
        bool encounteredError = false;
        bool dbModified = false;
        AutomaticModeOptions options;

        public AutomaticModeForm( AutomaticModeOptions opts ) {
            this.options = opts;
            InitializeComponent();
        }

        private void AutomaticModeForm_Load( object sender, EventArgs e ) {
            cmdClose.Enabled = false;
        }

        private void AutomaticModeForm_Shown( object sender, EventArgs e ) {
            Run();

            if( options.AutoClose == AutoCloseType.Always || ( options.AutoClose == AutoCloseType.UnlessError && !encounteredError ) )
                this.Close();
            else
                cmdClose.Enabled = true;
        }

        private void Write( string text ) {
            if( txtOutput.Text.Length == 0 || txtOutput.Text.EndsWith( Environment.NewLine ) ) {
                txtOutput.AppendText( "> " );
            }
            txtOutput.AppendText( text );
            Program.Logger.Write( LoggerLevel.Info, "Automatic mode: " + text );
        }

        private void WriteLine( string text = "" ) {
            Write( text );
            txtOutput.AppendText( Environment.NewLine );
        }

        private void Run() {
            Program.Logger.Write( LoggerLevel.Info, "Starting automatic operation." );

            if( !LoadGameDB() ) {
                encounteredError = true;
                WriteLine( "Aborting." );
                return;
            }

            if( !CheckSteam( options.CheckSteam, options.CloseSteam ) ) {
                encounteredError = true;
                WriteLine( "Aborting." );
                return;
            }

            Profile profile = LoadProfile( options.CustomProfile );
            if( profile == null ) {
                encounteredError = true;
                WriteLine( "Aborting." );
                return;
            }

            if( !UpdateGameList( profile, options.UpdateGameList ) ) {
                encounteredError = true;
                WriteLine( "Aborting." );
                return;
            }

            if( !ImportSteamCategories( profile, options.ImportSteamCategories ) ) {
                encounteredError = true;
                if( !options.TolerateMinorErrors ) {
                    WriteLine( "Aborting." );
                    return;
                }
            }

            if( !UpdateDBWithAppInfo( options.UpdateAppInfo ) ) {
                encounteredError = true;
                if( !options.TolerateMinorErrors ) {
                    WriteLine( "Aborting." );
                    return;
                }
            }

            if (!UpdateDBWithHltb(options.UpdateHltb))
            {
                encounteredError = true;
                if (!options.TolerateMinorErrors)
                {
                    WriteLine("Aborting.");
                    return;
                }
            }

            if( !ScrapeUnscrapedGames( profile, options.ScrapeUnscrapedGames ) ) {
                encounteredError = true;
                if( !options.TolerateMinorErrors ) {
                    WriteLine( "Aborting." );
                    return;
                }
            }

            if( !SaveDB( options.SaveDBChanges ) ) {
                encounteredError = true;
                if( !options.TolerateMinorErrors ) {
                    WriteLine( "Aborting." );
                    return;
                }
            }

            if( !AutocatGames( profile, options.AutoCats, options.ApplyAllAutoCats ) ) {
                encounteredError = true;
                if( !options.TolerateMinorErrors ) {
                    WriteLine( "Aborting." );
                    return;
                }
            }

            if( !SaveProfile( profile, options.SaveProfile ) ) {
                encounteredError = true;
                if( !options.TolerateMinorErrors ) {
                    WriteLine( "Aborting." );
                    return;
                }
            }

            if( !ExportToSteam( profile, options.ExportToSteam ) ) {
                encounteredError = true;
                WriteLine( "Aborting." );
                return;
            }

            LaunchSteam( options.SteamLaunch );

            WriteLine( "Done." );
        }

        private bool LoadGameDB() {
            Write( "Loading database..." );
            bool success = false;
            try {
                Program.GameDB = new GameDB();
                if( File.Exists( "GameDB.xml.gz" ) ) {
                    Program.GameDB.Load( "GameDB.xml.gz" );
                    success = true;
                } else if( File.Exists( "GameDB.xml" ) ) {
                    Program.GameDB.Load( "GameDB.xml" );
                    success = true;
                } else {
                    WriteLine( "Database not found." );
                }
            } catch( Exception e ) {
                WriteLine( "Error loading database: " + e.Message );
                Program.Logger.WriteException( "Automatic mode: Error loading database.", e );
            }
            if( success ) WriteLine( "Database loaded." );
            return success;
        }

        private bool CheckSteam( bool doCheck, bool tryClose ) {
            try {
                if( doCheck ) {
                    Write( "Checking for running Steam instance..." );
                    Process[] processes = Process.GetProcessesByName( "steam" );
                    if( processes.Count() == 0 ) {
                        WriteLine( "Not found. Continuing." );
                        return true;
                    } else {
                        WriteLine( "Found running Steam process." );
                        if( tryClose ) {
                            return TryCloseSteam( processes );
                        }
                        WriteLine( "Skipping trying to close Steam." );
                        return false;
                    }
                } else {
                    WriteLine( "Skipping running Steam check." );
                    return true;
                }
            } catch( Exception e ) {
                WriteLine( "Checking for running Steam process failed: " + e.Message );
                Program.Logger.WriteException( "Automatic mode error:", e );
                return false;
            }
        }

        private bool TryCloseSteam( Process[] steamProcs = null ) {
            try {
                if( steamProcs == null ) {
                    steamProcs = Process.GetProcessesByName( "steam" );
                }

                string steamDir = Settings.Instance.SteamPath;
                Write( "Trying to close Steam..." );
                Process closeProc = Process.Start( new ProcessStartInfo( steamDir + "\\steam.exe", "-shutdown" ) );
                bool closeProcSuccess = closeProc.WaitForExit( 5000 );

                if( !closeProcSuccess ) {
                    WriteLine( "Steam closing process did not terminate." );
                    return false;
                }

                foreach( Process sProc in steamProcs ) {
                    if( !sProc.WaitForExit( 5000 ) ) { 
                        WriteLine( "Steam process did not terminate as requested." );
                        return false;
                    }
                }

                WriteLine( "Steam terminated." );
                return true;
            } catch( Exception e ) {
                WriteLine( "Closing Steam failed: " + e.Message );
                Program.Logger.WriteException( "Automatic mode error:", e );
                return false;
            }
        }

        private Profile LoadProfile( string customProfile ) {
            // First, decide which profile to load
            string profileToLoad = null;
            Write( "Deciding what profile to load..." );
            if( string.IsNullOrWhiteSpace( customProfile ) ) {
                Write( "No custom profile specified. Checking settings..." );
                if( string.IsNullOrWhiteSpace( Settings.Instance.ProfileToLoad ) ) {
                    WriteLine( "No profile specified in settings." );
                    return null;
                } else {
                    WriteLine( "Default profile found: " + Settings.Instance.ProfileToLoad );
                    profileToLoad = Settings.Instance.ProfileToLoad;
                }
            } else {
                WriteLine( "Custom profile specified: " + customProfile );
                profileToLoad = customProfile;
            }

            // Then, actually load that profile
            Write( "Loading profile..." );
            Profile profile = null;
            try {
                profile = Profile.Load( profileToLoad );
                WriteLine( "Profile loaded." );
            } catch( Exception e ) {
                WriteLine( "Profile loading failed: " + e.Message );
                Program.Logger.WriteException( "Automatic mode: Error loading profile.", e );
            }
            return profile;
        }

        private bool UpdateGameList( Profile profile, bool doUpdate ) {
            if( !doUpdate ) {
                WriteLine( "Skipping updating game list." );
                return false;
            }
            Write( "Updating game list..." );
            bool success = false;
            if( profile.LocalUpdate ) {
                int newApps = 0;
                try {
                    Write( "Trying local update..." );
                    profile.GameData.UpdateGameListFromOwnedPackageInfo( profile.SteamID64, profile.IgnoreList, profile.IncludeUnknown ? AppTypes.InclusionUnknown : AppTypes.InclusionNormal, out newApps );
                    success = true;
                } catch( Exception e ) {
                    Write( "Local update failed. " );
                    Program.Logger.WriteException( "Automatic mode: Error on local profile update.", e );
                }
            }
            if( !success && profile.WebUpdate ) {
                Write( "Trying web update..." );
                switch( Settings.Instance.ListSource ) {
                    case GameListSource.XmlPreferred:
                        success = UpdateGameList_Web_Xml( profile );
                        if( !success ) {
                            success = UpdateGameList_Web_Html( profile );
                        }
                        break;
                    case GameListSource.XmlOnly:
                        success = UpdateGameList_Web_Xml( profile );
                        break;
                    case GameListSource.WebsiteOnly:
                        success = UpdateGameList_Web_Html( profile );
                        break;
                }
            }
            if( success ) {
                WriteLine( "Game list updated." );
            } else {
                WriteLine( "Update failed." );
            }
            return success;
        }

        private bool UpdateGameList_Web_Xml( Profile profile ) {
            try {
                XmlDocument doc = GameList.FetchXmlGameList( profile.SteamID64 );
                int newApps;
                profile.GameData.IntegrateXmlGameList( doc, false, profile.IgnoreList, profile.IncludeUnknown ? AppTypes.InclusionUnknown : AppTypes.InclusionNormal, out newApps );
                return true;
            } catch( Exception e ) {
                Program.Logger.WriteException( "Automatic mode: Error on XML web profile update.", e );
                return false;
            }
        }

        private bool UpdateGameList_Web_Html( Profile profile ) {
            try {
                string doc = GameList.FetchHtmlGameList( profile.SteamID64 );
                int newApps;
                profile.GameData.IntegrateHtmlGameList( doc, false, profile.IgnoreList, profile.IncludeUnknown ? AppTypes.InclusionUnknown : AppTypes.InclusionNormal, out newApps );
                return true;
            } catch( Exception e ) {
                Program.Logger.WriteException( "Automatic mode: Error on HTML web profile update.", e );
                return false;
            }
        }

        private bool ImportSteamCategories( Profile p, bool doImport ) {
            if( !doImport ) {
                WriteLine( "Skipping Steam category import." );
                return true;
            }
            Write( "Importing Steam category data..." );
            bool success = false;
            try {
                p.ImportSteamData();
                success = true;
            } catch( Exception e ) {
                WriteLine( "Import failed: " + e.Message );
                Program.Logger.WriteException( "Automatic mode: Error on steam import.", e );
            }
            if( success ) WriteLine( "Import complete." );
            return success;
        }

        private bool UpdateDBWithAppInfo( bool doUpdate ) {
            if( !doUpdate ) {
                WriteLine( "Skipping AppInfo update." );
                return true;
            }
            Write( "Updating database from AppInfo..." );
            bool success = false;
            try {
                string path = string.Format( Properties.Resources.AppInfoPath, Settings.Instance.SteamPath );
                if( Program.GameDB.UpdateFromAppInfo( path ) > 0 ) dbModified = true;
                success = true;
            } catch( Exception e ) {
                WriteLine( "Error updating database from AppInfo: " + e.Message );
                Program.Logger.WriteException( "Automatic mode: Error updating from AppInfo.", e );
            }
            if( success ) WriteLine( "AppInfo update complete." );
            return success;
        }

        private bool UpdateDBWithHltb(bool doUpdate)
        {
            if (!doUpdate)
            {
                WriteLine("Skipping HLTB update.");
                return true;
            }
            int HalfAWeekInSecs = 84*24*60*60;
            if (Utility.GetCurrentUTime() > (Program.GameDB.LastHltbUpdate + HalfAWeekInSecs))
            {
                WriteLine("Skipping HLTB update.");
                return true;
            }
            Write("Updating database from HLTB...");
            bool success = false;
            try
            {
                if (Program.GameDB.UpdateFromHltb(Settings.Instance.IncludeImputedTimes) > 0) dbModified = true;
                success = true;
            }
            catch (Exception e)
            {
                WriteLine("Error updating database from HLTB: " + e.Message);
                Program.Logger.WriteException("Automatic mode: Error updating from HLTB.", e);
            }
            if (success) WriteLine("HLTB update complete.");
            return success;
        }

        private bool ScrapeUnscrapedGames( Profile p, bool doScrape ) {
            if( !doScrape ) {
                WriteLine( "Skipping game scraping." );
                return true;
            }
            bool success = false;
            Write( "Scraping unscraped games..." );
            try {
                Queue<int> jobs = new Queue<int>();
                foreach( int id in p.GameData.Games.Keys ) {
                    if( id > 0 && !Program.GameDB.Contains( id ) || Program.GameDB.Games[id].LastStoreScrape == 0 ) {
                        jobs.Enqueue( id );
                    }
                }

                if( jobs.Count > 0 ) {

                    DbScrapeDlg scrapeDlg = new DbScrapeDlg( jobs );
                    DialogResult scrapeRes = scrapeDlg.ShowDialog();

                    if( scrapeRes == System.Windows.Forms.DialogResult.Cancel ) {
                        WriteLine( "Scraping cancelled." );
                    } else {
                        WriteLine( "Scraping complete." );
                        if( scrapeDlg.JobsCompleted > 0 ) dbModified = true;
                    }
                } else {
                    WriteLine( "No unscraped games found." );
                }
                success = true;
            } catch( Exception e ) {
                WriteLine( "Error updating database from web: " + e.Message );
                Program.Logger.WriteException( "Automatic mode: Error updating db from web.", e );
            }
            return success;
        }

        private bool SaveDB( bool doSave ) {
            if( !doSave ) {
                WriteLine( "Skipping database saving." );
                return true;
            }
            if( !dbModified ) {
                WriteLine( "No database changes to save." );
                return true;
            }
            bool success = false;
            Write( "Saving database..." );
            try {
                Program.GameDB.Save( "GameDB.xml.gz" );
                success = true;
            } catch( Exception e ) {
                WriteLine( "Error saving database: " + e.Message );
                Program.Logger.WriteException( "Automatic mode: Error saving db.", e );
            }

            if( success ) WriteLine( "Saved." );
            return success;
        }

        private bool AutocatGames( Profile p, List<string> autocatStrings, bool doAll ) {
            WriteLine( "Starting autocategorization..." );
            bool success = false;
            try {
                List<AutoCat> acList = new List<AutoCat>();
                if( doAll ) {
                    foreach( AutoCat a in p.AutoCats ) {
                        acList.Add( a );
                    }
                } else {
                    foreach( string s in autocatStrings ) {
                        foreach( AutoCat a in p.AutoCats ) {
                            if( a.Name == s && !acList.Contains( a ) ) {
                                acList.Add( a );
                            }
                        }
                    }
                }

                foreach( AutoCat ac in acList ) {
                    Write( "Running autocat '" + ac.Name + "'..." );
                    ac.PreProcess( p.GameData, Program.GameDB );


                    foreach( GameInfo g in p.GameData.Games.Values ) {
                        if( g.Id > 0 ) {
                            ac.CategorizeGame( g );
                        }
                    }

                    ac.DeProcess();
                    WriteLine( "Complete." );
                }
                success = true;
            } catch( Exception e ) {
                WriteLine( "Error autocategorizing games: " + e.Message );
                Program.Logger.WriteException( "Automatic mode: Error autocategorizing games.", e );
            }
            if( success ) WriteLine( "Autocategorization complete." );
            return success;
        }

        private bool SaveProfile( Profile p, bool doSave ) {
            if( !doSave ) {
                WriteLine( "Skipping profile save." );
                return true;
            }
            Write( "Saving profile..." );
            bool success = false;
            try {
                p.Save();
                success = true;
            } catch( Exception e ) {
                WriteLine( "Error saving profile: " + e.Message );
                Program.Logger.WriteException( "Automatic mode: Error saving profile.", e );
            }
            if( success ) WriteLine( "Saved." );
            return success;
        }

        private bool ExportToSteam( Profile p, bool doExport ) {
            if( !doExport ) {
                WriteLine( "Skipping Steam export." );
                return true;
            }
            Write( "Exporting to Steam..." );
            bool success = false;
            try {
                p.GameData.ExportSteamConfig( p.SteamID64, p.ExportDiscard, false );
                success = true;
            } catch( Exception e ) {
                WriteLine( "Error exporting Steam config: " + e.Message );
                Program.Logger.WriteException( "Automatic mode: Error exporting config.", e );
            }
            if( success ) WriteLine( "Export complete." );
            return success;
        }

        private void LaunchSteam( SteamLaunchType t ) {
            switch( t ) {
                case SteamLaunchType.None:
                    WriteLine( "Not launching Steam." );
                    break;
                case SteamLaunchType.Normal:
                    WriteLine( "Launching Steam in normal mode." );
                    System.Diagnostics.Process.Start( "steam://open/main" );
                    break;
                case SteamLaunchType.BigPicture:
                    WriteLine( "Launching Steam in big picture mode." );
                    System.Diagnostics.Process.Start( "steam://open/bigpicture" );
                    break;
            }
        }

        private void cmdClose_Click( object sender, EventArgs e ) {
            this.Close();
        }
    }

    public enum SteamLaunchType { None, Normal, BigPicture }
    public enum AutoCloseType { None, UnlessError, Always }

    public class AutomaticModeOptions {
        public bool CheckSteam = true;
        public bool CloseSteam = true;
        public string CustomProfile = null;
        public List<string> AutoCats = new List<string>();
        public bool ApplyAllAutoCats = false;
        public bool UpdateGameList = true;
        public bool ImportSteamCategories = false;
        public bool UpdateAppInfo = true;
        public bool UpdateHltb = true;
        public bool ScrapeUnscrapedGames = true;
        public bool SaveDBChanges = true;
        public bool SaveProfile = true;
        public bool ExportToSteam = true;
        public SteamLaunchType SteamLaunch = SteamLaunchType.None;
        public AutoCloseType AutoClose = AutoCloseType.None;
        public bool TolerateMinorErrors = false;
    }
}
