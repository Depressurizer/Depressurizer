using System;
using System.Windows.Forms;
using Depressurizer.Helpers;
using NDesk.Options;
using Rallion;
using Sentry;

namespace Depressurizer
{
    internal static class Program
    {
        #region Properties

        private static Database Database => Database.Instance;

        private static Logger Logger => Logger.Instance;

        private static Settings Settings => Settings.Instance;

        #endregion

        #region Methods

        private static void ApplicationExit(object sender, EventArgs e)
        {
            Settings.Save();
            Logger.Dispose();
        }

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            using (SentrySdk.Init("https://fbb6fca0ff1748d7a9160b6bc92bcb1d@sentry.io/267726"))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.ApplicationExit += ApplicationExit;

                FatalError.InitializeHandler();

                Database.Load();
                Settings.Load();

                AutomaticModeOptions autoOpts = ParseAutoOptions(args);

                if (autoOpts != null)
                {
                    Logger.Info("Automatic mode set, loading automatic mode form.");
                    Logger.Verbose("Automatic Mode Options: {0}", autoOpts);
                    Application.Run(new AutomaticModeForm(autoOpts));
                }
                else
                {
                    Logger.Info("Automatic mode not set, loading main form.");
                    Application.Run(new FormMain());
                }
            }
        }

        private static AutomaticModeOptions ParseAutoOptions(string[] args)
        {
            AutomaticModeOptions config = new AutomaticModeOptions();
            bool auto = false;

            OptionSet opts = new OptionSet
            {
                {
                    "auto", v => auto = true
                },
                {
                    "p|profile=", v => config.CustomProfile = v
                },
                {
                    "checksteam", v => config.CheckSteam = v != null
                },
                {
                    "closesteam", v => config.CloseSteam = v != null
                },
                {
                    "updatelib", v => config.UpdateGameList = v != null
                },
                {
                    "import", v => config.ImportSteamCategories = v != null
                },
                {
                    "updatedblocal", v => config.UpdateAppInfo = v != null
                },
                {
                    "updatedbhltb", v => config.UpdateHltb = v != null
                },
                {
                    "updatedbweb", v => config.ScrapeUnscrapedGames = v != null
                },
                {
                    "savedb", v => config.SaveDBChanges = v != null
                },
                {
                    "saveprofile", v => config.SaveProfile = v != null
                },
                {
                    "export", v => config.ExportToSteam = v != null
                },
                {
                    "launch", v => config.SteamLaunch = SteamLaunchType.Normal
                },
                {
                    "launchbp", v => config.SteamLaunch = SteamLaunchType.BigPicture
                },
                {
                    "tolerant", v => config.TolerateMinorErrors = v != null
                },
                {
                    "quiet", v => config.AutoClose = AutoCloseType.UnlessError
                },
                {
                    "silent", v => config.AutoClose = AutoCloseType.Always
                },
                {
                    "all", v => config.ApplyAllAutoCats = v != null
                },
                {
                    "<>", v => config.AutoCats.Add(v)
                }
            };

            opts.Parse(args);

            return auto ? config : null;
        }

        #endregion
    }
}
