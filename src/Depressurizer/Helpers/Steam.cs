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
using Rallion;

namespace Depressurizer.Helpers
{
    internal class Steam
    {
        #region Static Fields

        private static readonly List<int> BannerFailed = new List<int>();

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
            1001,
            1007,
            1210,
            1213,
            1220,
            1240,
            1255,
            1259,
            1260,
            1270,
            1273,
            1290,
            1306,
            1317,
            1320,
            1504,
            1507,
            1523,
            1525,
            1528,
            1532,
            1535,
            1635,
            1645,
            2110,
            2114,
            2115,
            2145,
            2150,
            2403,
            2413,
            2430,
            2460,
            2505,
            2625,
            2635,
            2645,
            2724,
            2725,
            2726,
            2740,
            2767,
            2768,
            2826,
            2827,
            2860,
            3030,
            3205,
            3273,
            3542,
            3599,
            3629,
            3640,
            3642,
            3750,
            4010,
            4020,
            4200,
            4206,
            4207,
            4210,
            4270,
            4280,
            4329,
            4450,
            4480,
            4808,
            4930,
            4931,
            4940,
            5004,
            5005,
            5006,
            5007,
            5018,
            5151,
            5221,
            5252,
            5284,
            5345,
            5433,
            5480,
            5499,
            5501,
            5512,
            5516,
            5586,
            5587,
            5590,
            5591,
            5592,
            5599,
            5614,
            5618,
            5633,
            5657,
            5662,
            5669,
            5678,
            5679,
            5680,
            5692,
            5702,
            5708,
            5709,
            5710,
            5711,
            5784,
            5821,
            5832,
            5834,
            5835,
            5836,
            5843,
            5845,
            5846,
            5847,
            5848,
            5849,
            5850,
            5851,
            5853,
            5876,
            5886,
            5894,
            5898,
            5921,
            5935,
            5936,
            5937,
            5938,
            5943,
            5944,
            5955,
            5957,
            5961,
            5962,
            5965,
            5967,
            5970,
            5971,
            5972,
            5973,
            5974,
            5975,
            6122,
            6129,
            6330,
            6350,
            6360,
            6520,
            6530,
            6540,
            6700,
            7203,
            7270,
            7700,
            7800,
            7950,
            8009,
            8070,
            8110,
            8142,
            8169,
            8179,
            8199,
            8610,
            8620,
            8662,
            8680,
            8710,
            8730,
            8770,
            8804,
            8950,
            8955,
            9240,
            9241,
            9360,
            9370,
            9890,
            9905,
            9906,
            9907,
            9910,
            9932,
            9933,
            9942,
            9949,
            10000
        };

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
                Program.Logger.Write(LoggerLevel.Warning, "Couldn't fetch banner for appId: {0}", appId);
            }
            catch
            {
                Program.Logger.Write(LoggerLevel.Warning, "Couldn't fetch banner for appId: {0}", appId);
            }
        }

        #endregion
    }
}
