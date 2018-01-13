#region LICENSE

//     This file (Program.cs) is part of Depressurizer.
//     Copyright (C) 2018  Martijn Vegter
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <https://www.gnu.org/licenses/>.

#endregion

using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Depressurizer.Forms;
using Depressurizer.Properties;
using DepressurizerCore;
using DepressurizerCore.Helpers;
using Newtonsoft.Json.Linq;

namespace Depressurizer
{
    internal static class Program
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Logger.Instance.Info("Depressurizer has started");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += OnApplicationExit;
            Application.ThreadException += OnThreadException;

            OnApplicationStart();

            Application.Run(new FormMain());
        }

        #endregion

        #region Methods

        private static void CheckForUpdates()
        {
            Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            Logger.Instance.Info("Current version is {0}.{1}.{2}", currentVersion.Major, currentVersion.Minor, currentVersion.Revision);

            try
            {
                Version githubVersion;
                string url;

                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Set("User-Agent", "Depressurizer");
                    string json = webClient.DownloadString(Constant.LatestReleaseURL);

                    JObject parsedJson = JObject.Parse(json);
                    githubVersion = new Version(((string) parsedJson.SelectToken("tag_name")).Replace("v", ""));
                    url = (string) parsedJson.SelectToken("html_url");
                }

                Logger.Instance.Info("Latest version is {0}", githubVersion.ToString());
                if (githubVersion <= currentVersion)
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
                SentryLogger.LogException(e);
                MessageBox.Show(string.Format(GlobalStrings.MainForm_Msg_ErrorDepressurizerUpdate, e.Message), GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            Settings.Instance.Save();
            Database.Instance.Save();
            Logger.Instance.Dispose();
        }

        private static void OnApplicationStart()
        {
            /* Settings */
            Settings.Instance.Load();

            /* Check for Depressurizer updates */
            if (Settings.Instance.CheckForUpdates)
            {
                CheckForUpdates();
            }

            /* Make sure we have a SteamPath */
            if (Settings.Instance.SteamPath == null)
            {
                Logger.Instance.Warn("Could not find SteamPath");

                using (SteamPathDialog dialog = new SteamPathDialog())
                {
                    dialog.ShowDialog();
                    Settings.Instance.SteamPath = dialog.Path;
                }
            }

            Settings.Instance.Save();

            /* Database */
            Database.Instance.Load();

            if (Settings.Instance.AutoSaveDatabase)
            {
                Database.Instance.Save();
            }
        }

        private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            SentryLogger.LogException(e.Exception);
        }

        #endregion
    }
}