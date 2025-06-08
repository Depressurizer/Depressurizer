using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using Depressurizer.Core;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Properties;
using NDesk.Options;
using Newtonsoft.Json.Linq;
using Rallion;
using Constants = Depressurizer.Core.Helpers.Constants;

namespace Depressurizer
{
    internal static class Program
    {
        #region Properties

        private static Database Database => Database.Instance;

        private static Version DepressurizerVersion => Assembly.GetExecutingAssembly().GetName().Version;

        private static Logger Logger => Logger.Instance;

        private static Settings Settings => Settings.Instance;

        #endregion

        #region Methods

        private static void ApplicationExit(object sender, EventArgs e)
        {
            Settings.Save();
            Logger.Dispose();
        }

        private static void CheckForDepressurizerUpdates()
        {
            try
            {
                Version githubVersion;
                string url;

                using (WebClient wc = new WebClient())
                {
                    // Github wants TLS 1.2
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    wc.Headers.Set("User-Agent", "Depressurizer");
                    string json = wc.DownloadString(Constants.DepressurizerLatestRelease);

                    JObject parsedJson = JObject.Parse(json);
                    githubVersion = new Version(((string) parsedJson.SelectToken("tag_name")).Replace("v", ""));
                    url = (string) parsedJson.SelectToken("html_url");
                }

                if (githubVersion <= DepressurizerVersion || DepressurizerVersion == new Version("0.0.0.0"))
                {
                    return;
                }

                if (MessageBox.Show(GlobalStrings.MainForm_Msg_UpdateFound, GlobalStrings.MainForm_Msg_UpdateFoundTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Process.Start(url);
                }
            }
            catch (Exception e)
            {
                Logger.Exception("Exception while checking for new updates for Depressurizer.", e);
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.MainForm_Msg_ErrorDepressurizerUpdate, e.Message), Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += ApplicationExit;

            FatalError.InitializeHandler();

            Logger.Info("Running Depressurizer v{0}", DepressurizerVersion);

            SingletonKeeper.Database = Database;

            Database.Load();
            Settings.Load();

            if (Settings.CheckForDepressurizerUpdates)
            {
                CheckForDepressurizerUpdates();
            }

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

        private static AutomaticModeOptions ParseAutoOptions(IEnumerable<string> args)
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
