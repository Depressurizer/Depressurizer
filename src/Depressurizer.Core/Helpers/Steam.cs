using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Depressurizer.Core.Helpers
{
    /// <summary>
    ///     Static class containing helper functions related to Steam.
    /// </summary>
    public static class Steam
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
            5,
            7,
            8,
            90,
            205,
            210,
            211,
            215,
            218,
            304,
            310,
            364,
            456,
            457,
            458,
            459,
            480,
            510,
            513,
            520,
            540,
            560,
            563,
            564,
            565,
            575,
            576,
            581,
            582,
            583,
            584,
            585,
            586,
            629,
            635,
            640,
            644,
            650,
            651,
            652,
            653,
            654,
            669,
            740,
            753,
            754,
            755,
            756,
            760,
            761,
            764,
            765,
            766,
            767,
            852,
            854,
            1007,
            1213,
            1255,
            1260,
            1273,
            2145,
            2403,
            4270,
            4940,
            8680,
            8710,
            8730,
            8770,
            12250,
            13180,
            13260,
            16830,
            16865,
            16871,
            17505,
            17515,
            17525,
            17535,
            17555,
            17575,
            17585,
            17705,
            20930,
            34120,
            34490,
            34491,
            34492,
            34493,
            34494,
            34495,
            34496,
            34497,
            34498,
            35428,
            40810,
            41005,
            41015,
            41040,
            41080,
            42300,
            42320,
            42750,
            43210,
            49536,
            49537,
            49538,
            55280,
            61800,
            61810,
            61820,
            61830,
            63220,
            70010,
            72310,
            72780,
            81099,
            81101,
            81103,
            81105,
            81106,
            81111,
            81112,
            81113,
            81148,
            81150,
            81152,
            81156,
            81162,
            91720,
            96810,
            99613,
            102000,
            102001,
            102002,
            102003,
            104310,
            111710,
            200172,
            200173,
            200174,
            200175,
            200176,
            200177,
            200178,
            200179,
            200180,
            200186,
            200269,
            202480,
            203180,
            203300,
            203600,
            205790,
            208050,
            208157,
            208691,
            210450,
            210890,
            210942,
            212542,
            213781,
            215350,
            215360,
            216280,
            216840,
            218800,
            220070,
            221002,
            221410,
            222840,
            222860,
            222890,
            223160,
            223240,
            223250,
            223540,
            223910,
            224620,
            226451,
            226454,
            226455,
            226456,
            226457,
            226470,
            227040,
            227050,
            228221,
            228980,
            230030,
            232644,
            232646,
            234430,
            236600,
            236650,
            237410,
            238670,
            238690,
            241100,
            243730,
            243750,
            244310,
            245850,
            254430,
            254671,
            254672,
            254673,
            254674,
            255470,
            255580,
            258680,
            261020,
            261140,
            261310,
            265360,
            266720,
            266910,
            267230,
            277734,
            277779,
            295185,
            301070,
            302530,
            302550,
            307781,
            312070,
            312800,
            312860,
            313250,
            315420,
            316000,
            319070,
            320420,
            321770,
            322050,
            323010,
            325957,
            325975,
            325982,
            325996,
            332670,
            332850,
            343050,
            348880,
            360230,
            366490,
            373300,
            376939,
            381690,
            388478,
            394620,
            394621,
            394622,
            396050,
            397080,
            399030,
            399080,
            399150,
            399160,
            399170,
            399190,
            399200,
            399210,
            399220,
            399290,
            399300,
            399310,
            399320,
            399330,
            399340,
            399350,
            399360,
            399370,
            399380,
            399390,
            399400,
            399440,
            399450,
            399470,
            399480,
            399490,
            399500,
            399510,
            399740,
            400690,
            401530,
            405270,
            405630,
            406790,
            407350,
            413080,
            413090,
            413100,
            421070,
            421080,
            421081,
            421090,
            421800,
            422310,
            424690,
            443030,
            443510,
            451534,
            476580,
            494560,
            503590,
            505620,
            517830,
            524440,
            532810,
            541260,
            551410,
            562020,
            563930,
            568880,
            571030,
            588460,
            594593,
            654310,
            700580,
            743320
        };

        #endregion

        #region Properties

        /// <summary>
        ///     Reference to the Logger instance.
        /// </summary>
        private static Logger Logger => Logger.Instance;

        private static long ProfileConstant => 0x0110000100000000;

        #endregion

        #region Public Methods and Operators

        public static async void GrabBanner(int appId)
        {
            await Task.Run(() => GrabBanners(new int[] { appId }));
        }

        /// <summary>
        ///     Grabs the banner from the Steam store.
        /// </summary>
        /// <param name="appIds">
        ///     IEnumerable containing the id's of the apps to download the banner for.
        /// </param>
        public static async void GrabBanners(IEnumerable<int> appIds)
        {
            appIds = appIds.Distinct().Except(IgnoreList);
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
#endif
        }

        /// <summary>
        ///     Opens the Steam Community page for the specified app in the default browser.
        /// </summary>
        /// <param name="appId"></param>
        public static void LaunchSteamCommunityPage(int appId)
        {
            Process.Start(string.Format(CultureInfo.InvariantCulture, Constants.SteamCommunityApp, appId));
        }

        /// <summary>
        ///     Opens the store page for the specified app in the default browser.
        /// </summary>
        /// <param name="appId"></param>
        public static void LaunchStorePage(int appId)
        {
            Process.Start(string.Format(CultureInfo.InvariantCulture, Constants.SteamStoreApp, appId));
        }

        public static string ToSteam3Id(long id)
        {
            return (id - ProfileConstant).ToString(CultureInfo.InvariantCulture);
        }

        public static int ToSteamId32(long id)
        {
            return (int)(id - ProfileConstant);
        }

        public static long ToSteamId64(string id)
        {
            if (long.TryParse(id, out long res))
            {
                return res + ProfileConstant;
            }

            return 0;
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
