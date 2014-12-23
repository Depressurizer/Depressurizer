using System;
using System.Collections.Generic;
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
            Run();

            if( options.AutoClose && ( !encounteredError || options.AutoCloseWithErrors ) ) {
                this.Close();
            } else {
                cmdClose.Enabled = true;
            }
        }

        private void AddText( string text ) {
            txtOutput.AppendText( text );
        }

        private void Run() {
            if( !LoadGameDB() ) {
                encounteredError = true;
                AddText( "Aborting." );
                return;
            }

            if( !CheckSteam( options.CheckSteam ) ) return;

            Profile profile = LoadProfile( options.CustomProfile );
            if( profile == null ) {
                encounteredError = true;
                AddText( "Aborting." );
                return;
            }

            if( !UpdateGameList( profile, options.UpdateGameList ) ) {
                encounteredError = true;
                AddText( "Aborting." );
                return;
            }

            if( !ImportSteamCategories( profile, options.ImportSteamCategories ) ) {
                encounteredError = true;
                if( !options.TolerateMinorErrors ) {
                    AddText( "Aborting." );
                    return;
                }
            }

            if( !UpdateDBWithAppInfo( options.UpdateAppInfo ) ) {
                encounteredError = true;
                if( !options.TolerateMinorErrors ) {
                    AddText( "Aborting." );
                    return;
                }
            }

            if( !ScrapeUnscrapedGames( profile, options.ScrapeUnscrapedGames ) ) {
                encounteredError = true;
                if( !options.TolerateMinorErrors ) {
                    AddText( "Aborting." );
                    return;
                }
            }

            if( !SaveDB( options.SaveDBChanges ) ) {
                encounteredError = true;
                if( !options.TolerateMinorErrors ) {
                    AddText( "Aborting." );
                    return;
                }
            }

            if( !AutocatGames( profile, options.AutoCats, options.ApplyAllAutoCats ) ) {
                encounteredError = true;
                if( !options.TolerateMinorErrors ) {
                    AddText( "Aborting." );
                    return;
                }
            }

            if( !SaveProfile( profile, options.SaveProfile ) ) {
                encounteredError = true;
                if( !options.TolerateMinorErrors ) {
                    AddText( "Aborting." );
                    return;
                }
            }

            if( !ExportToSteam( profile, options.ExportToSteam ) ) {
                encounteredError = true;
                AddText( "Aborting." );
                return;
            }

            LaunchSteam( options.SteamLaunch );
        }

        private bool LoadGameDB() {
            AddText( "Loading database..." );
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
                    AddText( "Database not found." + Environment.NewLine );
                }
            } catch( Exception e ) {
                AddText( "Error loading database: " + e.Message + Environment.NewLine );
                Program.Logger.WriteException( "Automatic mode: Error loading database.", e );
            }
            return success;
        }

        private bool CheckSteam( bool doCheck ) {
            if( doCheck ) {
                AddText( "Checking for running Steam instance..." );
                bool steamIsRunning = false; // TODO: actually check if steam is running
                if( !steamIsRunning ) {
                    AddText( "Not found. Continuing." + Environment.NewLine );
                    return true;
                } else {
                    AddText( "Found. Aborting." + Environment.NewLine );
                    return false;
                }
            } else {
                AddText( "Skipping running Steam check." + Environment.NewLine );
                return true;
            }
        }

        private Profile LoadProfile( string customProfile ) {
            // First, decide which profile to load
            string profileToLoad = null;
            AddText( "Deciding what profile to load..." );
            if( string.IsNullOrWhiteSpace( customProfile ) ) {
                AddText( "No custom profile specified. Checking settings..." );
                if( string.IsNullOrWhiteSpace( Settings.Instance.ProfileToLoad ) ) {
                    AddText( "No profile specified in settings." + Environment.NewLine );
                    return null;
                } else {
                    AddText( "Default profile found: " + Settings.Instance.ProfileToLoad + Environment.NewLine );
                    profileToLoad = Settings.Instance.ProfileToLoad;
                }
            } else {
                AddText( "Custom profile specified: " + customProfile + Environment.NewLine );
                profileToLoad = customProfile;
            }

            // Then, actually load that profile
            AddText( "Loading profile..." );
            Profile profile = null;
            try {
                profile = Profile.Load( profileToLoad );
                AddText( "Profile loaded." + Environment.NewLine );
            } catch( Exception e ) {
                AddText( "Profile loading failed: " + e.Message + Environment.NewLine );
                Program.Logger.WriteException( "Automatic mode: Error loading profile.", e );
            }
            return profile;
        }

        private bool UpdateGameList( Profile profile, bool doUpdate ) {
            if( !doUpdate ) {
                AddText( "Skipping updating game list." + Environment.NewLine );
                return false;
            }
            AddText( "Updating game list..." );
            bool success = false;
            if( profile.LocalUpdate ) {
                int newApps = 0;
                try {
                    AddText( "Trying local update..." );
                    profile.GameData.UpdateGameListFromOwnedPackageInfo( profile.SteamID64, profile.IgnoreList, profile.IncludeUnknown ? AppTypes.InclusionUnknown : AppTypes.InclusionNormal, out newApps );
                    success = true;
                } catch( Exception e ) {
                    AddText( "Local update failed." );
                    Program.Logger.WriteException( "Automatic mode: Error on local profile update.", e );
                }
            }
            if( !success && profile.WebUpdate ) {
                AddText( "Trying web update..." );
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
                AddText( "Game list updated." + Environment.NewLine );
            } else {
                AddText( "Update failed." + Environment.NewLine );
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
                AddText( "Skipping Steam category import." + Environment.NewLine );
                return true;
            }
            AddText( "Importing Steam category data..." );
            bool success = false;
            try {
                p.ImportSteamData();
                success = true;
            } catch( Exception e ) {
                AddText( "Import failed: " + e.Message + Environment.NewLine );
                Program.Logger.WriteException( "Automatic mode: Error on steam import.", e );
            }
            if( success ) AddText( "Import complete." + Environment.NewLine );
            return success;
        }

        private bool UpdateDBWithAppInfo( bool doUpdate ) {
            if( !doUpdate ) {
                AddText( "Skipping AppInfo update." + Environment.NewLine );
                return true;
            }
            AddText( "Updating database from AppInfo..." );
            bool success = false;
            try {
                string path = string.Format( Properties.Resources.AppInfoPath, Settings.Instance.SteamPath );
                if( Program.GameDB.UpdateFromAppInfo( path ) > 0 ) dbModified = true;
                success = true;
            } catch( Exception e ) {
                AddText( "Error updating database from AppInfo: " + e.Message );
                Program.Logger.WriteException( "Automatic mode: Error updating from AppInfo.", e );
            }
            if( success ) AddText( "AppInfo update complete." );
            return success;
        }

        private bool ScrapeUnscrapedGames( Profile p, bool doScrape ) {
            if( !doScrape ) {
                AddText( "Skipping game scraping." + Environment.NewLine );
                return true;
            }
            bool success = false;
            AddText( "Scraping unscraped games..." );
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
                        AddText( "Scraping cancelled." );
                    } else {
                        AddText( "Scraping complete." );
                        if( scrapeDlg.JobsCompleted > 0 ) dbModified = true;
                    }
                } else {
                    AddText( "No unscraped games found." );
                }
                success = true;
            } catch( Exception e ) {
                AddText( "Error updating database from web: " + e.Message );
                Program.Logger.WriteException( "Automatic mode: Error updating db from web.", e );
            }
            return success;
        }

        private bool SaveDB( bool doSave ) {
            if( !doSave ) {
                AddText( "Skipping database saving." + Environment.NewLine );
                return true;
            }
            if( !dbModified ) {
                AddText( "No database changes to save." + Environment.NewLine );
                return true;
            }
            bool success = false;
            AddText( "Saving database..." );
            try {
                Program.GameDB.Save( "GameDB.xml.gz" );
                success = true;
            } catch( Exception e ) {
                AddText( "Error saving database: " + e.Message );
                Program.Logger.WriteException( "Automatic mode: Error saving db.", e );
            }

            if( success ) AddText( "Saved." + Environment.NewLine );
            return success;
        }

        private bool AutocatGames( Profile p, List<string> autocatStrings, bool doAll ) {
            AddText( "Starting autocategorization..." + Environment.NewLine );
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
                    AddText( "Running autocat '" + ac.Name + "'..." );
                    ac.PreProcess( p.GameData, Program.GameDB );


                    foreach( GameInfo g in p.GameData.Games.Values ) {
                        if( g.Id > 0 ) {
                            ac.CategorizeGame( g );
                        }
                    }
                    
                    ac.DeProcess();
                    AddText( "Complete." + Environment.NewLine );
                }
                //TODO: Implement
                success = true;
            } catch( Exception e ) {
                AddText( "Error autocategorizing games: " + e.Message + Environment.NewLine );
                Program.Logger.WriteException( "Automatic mode: Error autocategorizing games.", e );
            }
            if( success ) AddText( "Autocategorization complete." + Environment.NewLine );
            return success;
        }

        private bool SaveProfile( Profile p, bool doSave ) {
            if( !doSave ) {
                AddText( "Skipping profile save." + Environment.NewLine );
                return true;
            }
            AddText( "Saving profile..." );
            bool success = false;
            try {
                p.Save();
                success = true;
            } catch( Exception e ) {
                AddText( "Error saving profile: " + e.Message + Environment.NewLine );
                Program.Logger.WriteException( "Automatic mode: Error saving profile.", e );
            }
            if( success ) AddText( "Saved." + Environment.NewLine );
            return success;
        }

        private bool ExportToSteam( Profile p, bool doExport ) {
            if( !doExport ) {
                AddText( "Skipping Steam export." + Environment.NewLine );
                return true;
            }
            AddText( "Exporting to Steam..." );
            bool success = false;
            try {
                p.GameData.ExportSteamConfig( p.SteamID64, p.ExportDiscard, false );
                success = true;
            } catch( Exception e ) {
                AddText( "Error exporting Steam config: " + e.Message + Environment.NewLine );
                Program.Logger.WriteException( "Automatic mode: Error exporting config.", e );
            }
            if( success ) AddText( "Export complete." + Environment.NewLine );
            return success;
        }

        private void LaunchSteam( SteamLaunchType t ) {
            switch( t ) {
                case SteamLaunchType.None:
                    AddText( "Not launching Steam." + Environment.NewLine );
                    break;
                case SteamLaunchType.Normal:
                    AddText( "Launching Steam in normal mode." + Environment.NewLine );
                    System.Diagnostics.Process.Start( "steam://open" );
                    break;
                case SteamLaunchType.BigPicture:
                    AddText( "Launching Steam in big picture mode." + Environment.NewLine );
                    System.Diagnostics.Process.Start( "steam://open/bigpicture" );
                    break;
            }
        }

        private void cmdClose_Click( object sender, EventArgs e ) {
            this.Close();
        }
    }

    public enum SteamLaunchType { None, Normal, BigPicture }

    public class AutomaticModeOptions {
        public bool CheckSteam = true;
        public string CustomProfile = null;
        public List<string> AutoCats = new List<string>();
        public bool ApplyAllAutoCats = false;
        public bool UpdateGameList = true;
        public bool ImportSteamCategories = false;
        public bool UpdateAppInfo = true;
        public bool ScrapeUnscrapedGames = true;
        public bool SaveDBChanges = true;
        public bool SaveProfile = true;
        public bool ExportToSteam = true;
        public SteamLaunchType SteamLaunch = SteamLaunchType.None;
        public bool AutoClose = false;
        public bool TolerateMinorErrors = false;
        public bool AutoCloseWithErrors = false;
    }
}
