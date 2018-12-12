#region LICENSE

//     This file (Steam.cs) is part of Depressurizer.
//     Copyright (C) 2018 Martijn Vegter
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
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sentry;

namespace Depressurizer.Helpers
{
    internal class Steam
    {
        #region Static Fields

        private static readonly List<int> BannerFailed = new List<int>();

        private static readonly List<int> IgnoreList = new List<int>();

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Grabs the banner from the Steam store
        /// </summary>
        /// <param name="appIds">AppId of the apps to fetch</param>
        public static async void GrabBanners(List<int> appIds)
        {
            appIds = appIds.Distinct().ToList();
            await Task.Run(() => { Parallel.ForEach(appIds, FetchBanner); });

#if DEBUG
            // Report failing banners using Sentry.IO
            if (BannerFailed.Count <= 0)
            {
                return;
            }

            IgnoreList.AddRange(BannerFailed);
            IgnoreList.Sort();

            InvalidDataException exception = new InvalidDataException("Found new failing banners!");
            exception.Data.Add("ID", JsonConvert.SerializeObject(IgnoreList.Distinct()));

            SentrySdk.CaptureException(exception);
#endif
        }

        #endregion

        #region Methods

        private static void FetchBanner(int appId)
        {
            if (appId <= 0 || IgnoreList.Contains(appId))
            {
                return;
            }

            if (File.Exists(Locations.File.Banner(appId)))
            {
                return;
            }

            string bannerLink = string.Format(CultureInfo.InvariantCulture, "https://steamcdn-a.akamaihd.net/steam/apps/{0}/capsule_sm_120.jpg", appId);
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Set("User-Agent", "Depressurizer");
                    webClient.DownloadFile(bannerLink, Locations.File.Banner(appId));
                }
            }
            catch (WebException we)
            {
                if (we.InnerException is IOException)
                {
                    FetchBanner(appId);

                    return;
                }

                if (we.Response is HttpWebResponse errorResponse && errorResponse.StatusCode != HttpStatusCode.NotFound)
                {
                    throw;
                }

                BannerFailed.Add(appId);
                Logger.Warn("Couldn't fetch banner for appId: {0}", appId);
            }
            catch
            {
                Logger.Warn("Couldn't fetch banner for appId: {0}", appId);
            }
        }

        #endregion
    }
}
