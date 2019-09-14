using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.XPath;
using Depressurizer.AutoCats;
using Depressurizer.Core;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;
using Depressurizer.Dialogs;

namespace Depressurizer
{
    public partial class AutomaticModeForm : Form
    {
        #region Fields

        private readonly AutomaticModeOptions options;

        private bool dbModified;

        private bool encounteredError;

        #endregion

        #region Constructors and Destructors

        public AutomaticModeForm(AutomaticModeOptions opts)
        {
            options = opts;
            InitializeComponent();
        }

        #endregion

        #region Properties

        private static Database Database => Database.Instance;

        private static Logger Logger => Logger.Instance;

        private static Settings Settings => Settings.Instance;

        #endregion

        #region Methods

        private static bool UpdateGameList_Web_Xml(Profile profile)
        {
            try
            {
                IXPathNavigable doc = GameList.FetchGameList(profile.SteamID64);
                profile.GameData.IntegrateGameList(doc, false, profile.IgnoreList, out int _);
                return true;
            }
            catch (Exception e)
            {
                Logger.Exception("Automatic mode: Error on XML web profile update.", e);
                return false;
            }
        }

        private bool AutoCatGames(Profile profile, IEnumerable<string> autoCatStrings, bool doAll)
        {
            WriteLine("Starting autocategorization...");

            if (profile == null)
            {
                WriteLine("Canceled autocategorization, profile was null...");
                return false;
            }

            if (autoCatStrings == null)
            {
                WriteLine("Canceled autocategorization, autoCatStrings was null...");
                return false;
            }

            bool success = false;
            try
            {
                List<AutoCat> autoCats = new List<AutoCat>();
                if (doAll)
                {
                    foreach (AutoCat autoCat in profile.AutoCats)
                    {
                        autoCats.Add(autoCat);
                    }
                }
                else
                {
                    foreach (string autoCatString in autoCatStrings)
                    foreach (AutoCat autoCat in profile.AutoCats)
                    {
                        if (autoCat.Name == autoCatString && !autoCats.Contains(autoCat))
                        {
                            autoCats.Add(autoCat);
                        }
                    }
                }

                RunAutoCats(profile, autoCats);

                success = true;
            }
            catch (Exception e)
            {
                WriteLine("Error autocategorizing games: " + e.Message);
                Logger.Exception("Automatic mode: Error autocategorizing games.", e);
            }

            if (success)
            {
                WriteLine("Autocategorization complete.");
            }

            return success;
        }

        private void AutomaticModeForm_Load(object sender, EventArgs e)
        {
            cmdClose.Enabled = false;
        }

        private void AutomaticModeForm_Shown(object sender, EventArgs e)
        {
            Run();

            if (options.AutoClose == AutoCloseType.Always || options.AutoClose == AutoCloseType.UnlessError && !encounteredError)
            {
                Close();
            }
            else
            {
                cmdClose.Enabled = true;
            }
        }

        private bool CheckSteam(bool doCheck, bool tryClose)
        {
            try
            {
                if (doCheck)
                {
                    Write("Checking for running Steam instance...");
                    Process[] processes = Process.GetProcessesByName("steam");
                    if (!processes.Any())
                    {
                        WriteLine("Not found. Continuing.");
                        return true;
                    }

                    WriteLine("Found running Steam process.");
                    if (tryClose)
                    {
                        return TryCloseSteam(processes);
                    }

                    WriteLine("Skipping trying to close Steam.");
                    return false;
                }

                WriteLine("Skipping running Steam check.");
                return true;
            }
            catch (Exception e)
            {
                WriteLine("Checking for running Steam process failed: " + e.Message);
                Logger.Exception("Automatic mode error:", e);
                return false;
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool ExportToSteam(Profile p, bool doExport)
        {
            if (!doExport)
            {
                WriteLine("Skipping Steam export.");
                return true;
            }

            Write("Exporting to Steam...");
            bool success = false;
            try
            {
                p.GameData.ExportSteamConfig(p.SteamID64, p.ExportDiscard, false);
                success = true;
            }
            catch (Exception e)
            {
                WriteLine("Error exporting Steam config: " + e.Message);
                Logger.Exception("Automatic mode: Error exporting config.", e);
            }

            if (success)
            {
                WriteLine("Export complete.");
            }

            return success;
        }

        private bool ImportSteamCategories(Profile p, bool doImport)
        {
            if (!doImport)
            {
                WriteLine("Skipping Steam category import.");
                return true;
            }

            Write("Importing Steam category data...");
            bool success = false;
            try
            {
                p.ImportSteamData();
                success = true;
            }
            catch (Exception e)
            {
                WriteLine("Import failed: " + e.Message);
                Logger.Exception("Automatic mode: Error on steam import.", e);
            }

            if (success)
            {
                WriteLine("Import complete.");
            }

            return success;
        }

        private void LaunchSteam(SteamLaunchType t)
        {
            switch (t)
            {
                case SteamLaunchType.None:
                    WriteLine("Not launching Steam.");
                    break;
                case SteamLaunchType.Normal:
                    WriteLine("Launching Steam in normal mode.");
                    Process.Start("steam://open/main");
                    break;
                case SteamLaunchType.BigPicture:
                    WriteLine("Launching Steam in big picture mode.");
                    Process.Start("steam://open/bigpicture");
                    break;
            }
        }

        private bool LoadDatabase()
        {
            Write("Loading database...");
            bool success = false;
            try
            {
                Database.Reset();
                if (Database.getExistsDatabasePath() != "")
                {
                    Database.Load();
                    success = true;
                }
                else
                {
                    WriteLine("Database not found.");
                }
            }
            catch (Exception e)
            {
                WriteLine("Error loading database: " + e.Message);
                Logger.Exception("Automatic mode: Error loading database.", e);
            }

            if (success)
            {
                WriteLine("Database loaded.");
            }

            return success;
        }

        private Profile LoadProfile(string customProfile)
        {
            // First, decide which profile to load
            string profileToLoad;
            Write("Deciding what profile to load...");
            if (string.IsNullOrWhiteSpace(customProfile))
            {
                Write("No custom profile specified. Checking settings...");
                if (string.IsNullOrWhiteSpace(Settings.Instance.ProfileToLoad))
                {
                    WriteLine("No profile specified in settings.");
                    return null;
                }

                WriteLine("Default profile found: " + Settings.Instance.ProfileToLoad);
                profileToLoad = Settings.Instance.ProfileToLoad;
            }
            else
            {
                WriteLine("Custom profile specified: " + customProfile);
                profileToLoad = customProfile;
            }

            // Then, actually load that profile
            Write("Loading profile...");
            Profile profile = null;
            try
            {
                profile = Profile.Load(profileToLoad);
                WriteLine("Profile loaded.");
            }
            catch (Exception e)
            {
                WriteLine("Profile loading failed: " + e.Message);
                Logger.Exception("Automatic mode: Error loading profile.", e);
            }

            return profile;
        }

        private void Run()
        {
            Logger.Info("Starting automatic operation.");

            if (!LoadDatabase())
            {
                encounteredError = true;
                WriteLine("Aborting.");
                return;
            }

            if (!CheckSteam(options.CheckSteam, options.CloseSteam))
            {
                encounteredError = true;
                WriteLine("Aborting.");
                return;
            }

            Profile profile = LoadProfile(options.CustomProfile);
            if (profile == null)
            {
                encounteredError = true;
                WriteLine("Aborting.");
                return;
            }

            if (!UpdateGameList(profile, options.UpdateGameList))
            {
                encounteredError = true;
                WriteLine("Aborting.");
                return;
            }

            if (!ImportSteamCategories(profile, options.ImportSteamCategories))
            {
                encounteredError = true;
                if (!options.TolerateMinorErrors)
                {
                    WriteLine("Aborting.");
                    return;
                }
            }

            if (!UpdateDatabaseFromAppInfo(options.UpdateAppInfo))
            {
                encounteredError = true;
                if (!options.TolerateMinorErrors)
                {
                    WriteLine("Aborting.");
                    return;
                }
            }

            if (!UpdateDatabaseFromHLTB(options.UpdateHltb))
            {
                encounteredError = true;
                if (!options.TolerateMinorErrors)
                {
                    WriteLine("Aborting.");
                    return;
                }
            }

            if (!ScrapeUnscrapedGames(profile, options.ScrapeUnscrapedGames))
            {
                encounteredError = true;
                if (!options.TolerateMinorErrors)
                {
                    WriteLine("Aborting.");
                    return;
                }
            }

            if (!SaveDatabase(options.SaveDBChanges))
            {
                encounteredError = true;
                if (!options.TolerateMinorErrors)
                {
                    WriteLine("Aborting.");
                    return;
                }
            }

            if (!AutoCatGames(profile, options.AutoCats, options.ApplyAllAutoCats))
            {
                encounteredError = true;
                if (!options.TolerateMinorErrors)
                {
                    WriteLine("Aborting.");
                    return;
                }
            }

            if (!SaveProfile(profile, options.SaveProfile))
            {
                encounteredError = true;
                if (!options.TolerateMinorErrors)
                {
                    WriteLine("Aborting.");
                    return;
                }
            }

            if (!ExportToSteam(profile, options.ExportToSteam))
            {
                encounteredError = true;
                WriteLine("Aborting.");
                return;
            }

            LaunchSteam(options.SteamLaunch);

            WriteLine("Done.");
        }

        private void RunAutoCats(Profile profile, IEnumerable<AutoCat> autoCats)
        {
            if (profile == null || autoCats == null)
            {
                return;
            }

            foreach (AutoCat autoCat in autoCats)
            {
                Write("Running AutoCat '" + autoCat.Name + "'...");
                autoCat.PreProcess(profile.GameData);

                if (autoCat.AutoCatType == AutoCatType.Group)
                {
                    AutoCatGroup autoCatGroup = (AutoCatGroup) autoCat;
                    RunAutoCats(profile, profile.CloneAutoCatList(autoCatGroup.Autocats, profile.GameData.GetFilter(autoCatGroup.Filter)));
                }
                else
                {
                    foreach (GameInfo g in profile.GameData.Games.Values)
                    {
                        if (g.Id > 0)
                        {
                            autoCat.CategorizeGame(g, profile.GameData.GetFilter(autoCat.Filter));
                        }
                    }
                }

                autoCat.DeProcess();
                WriteLine(autoCat.Name + " complete.");
            }
        }

        private bool SaveDatabase(bool doSave)
        {
            if (!doSave)
            {
                WriteLine("Skipping database saving.");
                return true;
            }

            if (!dbModified)
            {
                WriteLine("No database changes to save.");
                return true;
            }

            bool success = false;
            Write("Saving database...");
            try
            {
                Database.Save(Locations.File.DatabaseGzip);
                success = true;
            }
            catch (Exception e)
            {
                WriteLine("Error saving database: " + e.Message);
                Logger.Exception("Automatic mode: Error saving db.", e);
            }

            if (success)
            {
                WriteLine("Saved.");
            }

            return success;
        }

        private bool SaveProfile(Profile p, bool doSave)
        {
            if (!doSave)
            {
                WriteLine("Skipping profile save.");
                return true;
            }

            Write("Saving profile...");
            bool success = false;
            try
            {
                p.Save();
                success = true;
            }
            catch (Exception e)
            {
                WriteLine("Error saving profile: " + e.Message);
                Logger.Exception("Automatic mode: Error saving profile.", e);
            }

            if (success)
            {
                WriteLine("Saved.");
            }

            return success;
        }

        private bool ScrapeUnscrapedGames(Profile p, bool doScrape)
        {
            if (!doScrape)
            {
                WriteLine("Skipping game scraping.");
                return true;
            }

            bool success = false;
            Write("Scraping unscraped games...");
            try
            {
                List<ScrapeJob> scrapeJobs = new List<ScrapeJob>();
                foreach (int appId in p.GameData.Games.Keys.Where(g => !Settings.IgnoreList.Contains(g)))
                {
                    DatabaseEntry entry = null;
                    if (appId > 0 && !Database.Contains(appId, out entry))
                    {
                        scrapeJobs.Add(new ScrapeJob(appId, appId));
                    }
                    else if (entry != null && entry.LastStoreScrape == 0)
                    {
                        scrapeJobs.Add(new ScrapeJob(entry.Id, appId));
                    }
                }

                if (scrapeJobs.Count > 0)
                {
                    using (ScrapeDialog dialog = new ScrapeDialog(scrapeJobs))
                    {
                        DialogResult result = dialog.ShowDialog();

                        if (result == DialogResult.Cancel)
                        {
                            WriteLine("Scraping cancelled.");
                        }
                        else
                        {
                            WriteLine("Scraping complete.");
                            if (dialog.JobsCompleted > 0)
                            {
                                dbModified = true;
                            }
                        }
                    }
                }
                else
                {
                    WriteLine("No unscraped games found.");
                }

                success = true;
            }
            catch (Exception e)
            {
                WriteLine("Error updating database from web: " + e.Message);
                Logger.Exception("Automatic mode: Error updating db from web.", e);
            }

            return success;
        }

        private bool TryCloseSteam(Process[] steamProcesses = null)
        {
            try
            {
                if (steamProcesses == null)
                {
                    steamProcesses = Process.GetProcessesByName("steam");
                }

                string steamDir = Settings.Instance.SteamPath;
                Write("Trying to close Steam...");
                Process closeProc = Process.Start(new ProcessStartInfo(steamDir + "\\steam.exe", "-shutdown"));
                bool closeProcSuccess = closeProc != null && closeProc.WaitForExit(5000);

                if (!closeProcSuccess)
                {
                    WriteLine("Steam closing process did not terminate.");
                    return false;
                }

                if (steamProcesses.Any(sProc => !sProc.WaitForExit(5000)))
                {
                    WriteLine("Steam process did not terminate as requested.");
                    return false;
                }

                WriteLine("Steam terminated.");
                return true;
            }
            catch (Exception e)
            {
                WriteLine("Closing Steam failed: " + e.Message);
                Logger.Exception("Automatic mode error:", e);
                return false;
            }
        }

        private bool UpdateDatabaseFromAppInfo(bool doUpdate)
        {
            if (!doUpdate)
            {
                WriteLine("Skipping AppInfo update.");
                return true;
            }

            Write("Updating database from AppInfo...");
            bool success = false;
            try
            {
                string path = string.Format(CultureInfo.InvariantCulture, Constants.AppInfo, Settings.Instance.SteamPath);
                if (Database.UpdateFromAppInfo(path) > 0)
                {
                    dbModified = true;
                }

                success = true;
            }
            catch (Exception e)
            {
                WriteLine("Error updating database from AppInfo: " + e.Message);
                Logger.Exception("Automatic mode: Error updating from AppInfo.", e);
            }

            if (success)
            {
                WriteLine("AppInfo update complete.");
            }

            return success;
        }

        private bool UpdateDatabaseFromHLTB(bool doUpdate)
        {
            if (!doUpdate)
            {
                WriteLine("Skipping HLTB update.");
                return true;
            }

            int HalfAWeekInSecs = 84 * 24 * 60 * 60;
            if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > Database.LastHLTBUpdate + HalfAWeekInSecs)
            {
                WriteLine("Skipping HLTB update.");
                return true;
            }

            Write("Updating database from HLTB...");
            bool success = false;
            try
            {
                if (Database.UpdateFromHLTB(Settings.Instance.IncludeImputedTimes) > 0)
                {
                    dbModified = true;
                }

                success = true;
            }
            catch (Exception e)
            {
                WriteLine("Error updating database from HLTB: " + e.Message);
                Logger.Exception("Automatic mode: Error updating from HLTB.", e);
            }

            if (success)
            {
                WriteLine("HLTB update complete.");
            }

            return success;
        }

        private bool UpdateGameList(Profile profile, bool doUpdate)
        {
            if (!doUpdate)
            {
                WriteLine("Skipping updating game list.");
                return false;
            }

            Write("Updating game list...");
            bool success = false;
            if (profile.LocalUpdate)
            {
                try
                {
                    Write("Trying local update...");
                    profile.GameData.UpdateGameListFromOwnedPackageInfo(profile.SteamID64, profile.IgnoreList, out int _);
                    success = true;
                }
                catch (Exception e)
                {
                    Write("Local update failed. ");
                    Logger.Exception("Automatic mode: Error on local profile update.", e);
                }
            }

            if (!success && profile.WebUpdate)
            {
                Write("Trying web update...");

                success = UpdateGameList_Web_Xml(profile);
            }

            WriteLine(success ? "Game list updated." : "Update failed.");

            return success;
        }

        private void Write(string text)
        {
            if (txtOutput.Text.Length == 0 || txtOutput.Text.EndsWith(Environment.NewLine))
            {
                txtOutput.AppendText("> ");
            }

            txtOutput.AppendText(text);
            Logger.Info("Automatic mode: " + text);
        }

        private void WriteLine(string text = "")
        {
            Write(text);
            txtOutput.AppendText(Environment.NewLine);
        }

        #endregion
    }

    public class AutomaticModeOptions
    {
        #region Fields

        public bool ApplyAllAutoCats = false;

        public List<string> AutoCats = new List<string>();

        public AutoCloseType AutoClose = AutoCloseType.None;

        public bool CheckSteam = true;

        public bool CloseSteam = true;

        public string CustomProfile = null;

        public bool ExportToSteam = true;

        public bool ImportSteamCategories = false;

        public bool SaveDBChanges = true;

        public bool SaveProfile = true;

        public bool ScrapeUnscrapedGames = true;

        public SteamLaunchType SteamLaunch = SteamLaunchType.None;

        public bool TolerateMinorErrors = false;

        public bool UpdateAppInfo = true;

        public bool UpdateGameList = true;

        public bool UpdateHltb = true;

        #endregion
    }
}
