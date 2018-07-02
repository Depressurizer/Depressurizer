#region License

//     This file (Steam.cs) is part of Depressurizer.
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

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Depressurizer.Core.Enums;

namespace Depressurizer.Core.Helpers
{
	/// <summary>
	///     Helper functions for Steam related actions
	/// </summary>
	public static class Steam
	{
		#region Static Fields

		private static readonly List<int> IgnoreList = new List<int>
		{
			480,
			52440,
			524440,
			562020,
			700580
		};

		#endregion

		#region Properties

		private static Logger Logger => Logger.Instance;

		#endregion

		#region Public Methods and Operators

		/// <summary>
		///     Converts a StoreLanguage to a Steam accepted API language code
		/// </summary>
		/// <param name="language">Language to convert</param>
		/// <returns>Steam Store API language code</returns>
		public static string GetStoreLanguage(StoreLanguage language)
		{
			string storeLanguage;

			switch (language)
			{
				case StoreLanguage.ChineseSimplified:
					storeLanguage = "schinese";

					break;

				case StoreLanguage.ChineseTraditional:
					storeLanguage = "tchinese";

					break;

				case StoreLanguage.Korean:
					storeLanguage = "koreana";

					break;

				case StoreLanguage.PortugueseBrasil:
					storeLanguage = "brazilian";

					break;

				default:
					storeLanguage = language.ToString();

					break;
			}

			return storeLanguage.ToLower();
		}

		/// <summary>
		///     Grabs the banner from the Steam store
		/// </summary>
		/// <param name="appIds">AppId of the apps to fetch</param>
		public static async void GrabBanners(List<int> appIds)
		{
			appIds = appIds.Distinct().ToList();
			await Task.Run(() =>
			{
				Parallel.ForEach(appIds, FetchBanner);
			});
		}

		#endregion

		#region Methods

		private static void FetchBanner(int appId)
		{
			if ((appId <= 0) || IgnoreList.Contains(appId))
			{
				return;
			}

			if (File.Exists(Location.File.Banner(appId)))
			{
				return;
			}

			string bannerLink = string.Format(CultureInfo.InvariantCulture, "https://steamcdn-a.akamaihd.net/steam/apps/{0}/capsule_sm_120.jpg", appId);
			try
			{
				using (WebClient webClient = new WebClient())
				{
					webClient.Headers.Set("User-Agent", "Depressurizer");
					webClient.DownloadFile(bannerLink, Location.File.Banner(appId));
				}
			}
			catch (WebException we)
			{
				if (we.InnerException is IOException)
				{
					FetchBanner(appId);
				}

				if (we.Response is HttpWebResponse errorResponse && (errorResponse.StatusCode != HttpStatusCode.NotFound))
				{
					throw;
				}
			}
			catch
			{
				Logger.Warn("Couldn't fetch banner for appId: {0}", appId);
			}
		}

		#endregion
	}
}
