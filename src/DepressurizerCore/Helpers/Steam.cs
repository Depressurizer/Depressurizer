#region LICENSE

//     This file (Steam.cs) is part of DepressurizerCore.
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DepressurizerCore.Helpers
{
    /// <summary>
    ///     Steam Helper Class
    /// </summary>
    public static class Steam
    {
        #region Static Fields

        private static readonly List<int> BannerIgnoreList = new List<int>
        {
            212680,
            228300,
            239140,
            245620,
            252950,
            295690,
            297130,
            343180,
            344040,
            407750,
            476480,
            478010,
            524440,
            562020,
            700580
        };

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Grabs the banner from the Steam store
        /// </summary>
        /// <param name="apps">AppId of the apps to fetch</param>
        public static async void GrabBanners(List<int> apps)
        {
            apps = apps.Distinct().ToList();
            await Task.Run(() => { Parallel.ForEach(apps, FetchBanner); });
        }

        /// <summary>
        ///     Opens up the store for an app.
        /// </summary>
        public static void LaunchStorePage(int appId)
        {
            if (appId < 0)
            {
                return;
            }

            ExecuteBrowser("steam://store/{0}", appId);
        }

        #endregion

        #region Methods

        private static void ExecuteBrowser(string url, params object[] args)
        {
            ExecuteBrowser(string.Format(CultureInfo.InvariantCulture, url, args));
        }

        private static void ExecuteBrowser(string url)
        {
            Process.Start(url);
        }

        private static void FetchBanner(int appId)
        {
            if (appId <= 0 || File.Exists(Location.File.Banner(appId)) || BannerIgnoreList.Contains(appId))
            {
                return;
            }

            string bannerLink = string.Format(CultureInfo.InvariantCulture, "https://steamcdn-a.akamaihd.net/steam/apps/{0}/capsule_sm_120.jpg", appId);
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(bannerLink, Location.File.Banner(appId));
                }
            }
            catch (WebException we)
            {
                if (we.InnerException is IOException)
                {
                    Thread.Sleep(100);
                    FetchBanner(appId);
                }

                if (we.Response is HttpWebResponse errorResponse && errorResponse.StatusCode != HttpStatusCode.NotFound)
                {
                    throw;
                }

                Logger.Instance.Warn("Couldn't fetch banner for appId: {0}", appId);
            }
            catch (Exception e)
            {
                SentryLogger.LogException(e);
            }
        }

        #endregion
    }
}