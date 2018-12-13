using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Depressurizer.Core.Helpers;
#if DEBUG
using Newtonsoft.Json;
using Sentry;

#endif

namespace Depressurizer.Helpers
{
    /// <summary>
    ///     Static class containing helper functions related to Steam.
    /// </summary>
    internal static class Steam
    {
        #region Static Fields

#if DEBUG
        /// <summary>
        ///     List containing the id's of the apps who's banner failed to download.
        /// </summary>
        private static readonly List<int> BannerFailed = new List<int>();
#endif
        /// <summary>
        ///     List of known id's that don't have a banner available.
        /// </summary>
        private static readonly List<int> IgnoreList = new List<int>
        {
            480,
            12250,
            524440,
            562020,
            654310,
            700580
        };

        #endregion

        #region Properties

        /// <summary>
        ///     Reference to the Logger instance.
        /// </summary>
        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Grabs the banner from the Steam store.
        /// </summary>
        /// <param name="appIds">
        ///     IEnumerable containing the id's of the apps to download the banner for.
        /// </param>
        public static async void GrabBanners(IEnumerable<int> appIds)
        {
            appIds = appIds.Distinct();
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
            exception.Data.Add("IgnoreList", JsonConvert.SerializeObject(IgnoreList.Distinct()));

            SentrySdk.CaptureException(exception);
#endif
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Downloads the banner of the specified appid.
        /// </summary>
        /// <param name="appId">
        ///     Id of the target app, must be greater than zero.
        /// </param>
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

            string bannerLink = string.Format(CultureInfo.InvariantCulture, Constants.StoreBanner, appId);
            try
            {
                using (WebClient webClient = new WebClient())
                {
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
#if DEBUG
                BannerFailed.Add(appId);
#endif
                Logger.Warn("Steam: Couldn't fetch banner for appId: {0}, error code 404.", appId);
            }
            catch (Exception e)
            {
                Logger.Warn("Steam: Couldn't fetch banner for appId: {0}, exception: {1}.", appId, e);
            }
        }

        #endregion
    }
}
