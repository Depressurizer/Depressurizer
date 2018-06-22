using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Depressurizer.Properties;
using Rallion;

namespace Depressurizer.Helpers
{
	public static class Steam
	{

		private static readonly List<int> IgnoreList = new List<int>
		{

		};

		/// <summary>
		///     Grabs the banner from the Steam store
		/// </summary>
		/// <param name="apps">AppId of the apps to fetch</param>
		public static async void GrabBanners(List<int> apps)
		{
			apps = apps.Distinct().ToList();
			await Task.Run(() =>
			{
				Parallel.ForEach(apps, FetchBanner);
			});
		}

		private static void FetchBanner(int appId)
		{
			if ((appId <= 0) || File.Exists(Location.File.Banner(appId)) || IgnoreList.Contains(appId))
			{
				return;
			}

			string bannerLink = string.Format(CultureInfo.InvariantCulture, Constants.SteamStoreAppBanner, appId);
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

				if (we.Response is HttpWebResponse errorResponse && (errorResponse.StatusCode != HttpStatusCode.NotFound))
				{
					throw;
				}
			}
			catch
			{
				Program.Logger.Write(LoggerLevel.Warning, "Couldn't fetch banner for appId: {0}", appId);
				Debug.WriteLine("Couldn't fetch banner for appId: {0}", appId);
			}
		}
	}
}
