#region LICENSE

//     This file (Utility.cs) is part of DepressurizerCore.
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
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace DepressurizerCore.Helpers
{
	public static class Utility
	{
		#region Public Methods and Operators

		public static string CalculateMD5(string filename)
		{
			if (!File.Exists(filename))
			{
				return null;
			}

			using (MD5 md5 = MD5.Create())
			{
				using (FileStream stream = File.OpenRead(filename))
				{
					byte[] hash = md5.ComputeHash(stream);
					return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
				}
			}
		}

		public static int CompareLists(List<string> a, List<string> b)
		{
			if ((a == null) && (b == null))
			{
				return 0;
			}

			if (a == null)
			{
				return 1;
			}

			if (b == null)
			{
				return -1;
			}

			for (int i = 0; (i < a.Count) && (i < b.Count); i++)
			{
				int res = string.CompareOrdinal(a[i], b[i]);
				if (res != 0)
				{
					return res;
				}
			}

			return a.Count.CompareTo(b.Count);
		}

		public static long CurrentUnixTime()
		{
			return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		}

		public static DateTime DateTimeFromUnix(long unixTimeStamp)
		{
			return DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).DateTime;
		}

		public static CultureInfo GetCultureInfoFromStoreLanguage(StoreLanguage storeLanguage)
		{
			CultureInfo cultureInfo = CultureInfo.CurrentCulture;

			try
			{
				string language = storeLanguage == StoreLanguage.Default ? Settings.Instance.InterfaceLanguage.ToString() : storeLanguage.ToString();

				foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
				{
					if (culture.EnglishName == language)
					{
						cultureInfo = culture;
					}
				}
			}
			catch (Exception e)
			{
				SentryLogger.Log(e);
			}

			return cultureInfo;
		}

		public static Image GetImage(string url)
		{
			if (string.IsNullOrWhiteSpace(url))
			{
				return null;
			}

			Image image = null;

			try
			{
				using (WebClient webClient = new WebClient())
				{
					byte[] imageBytes = webClient.DownloadData(url);

					using (MemoryStream ms = new MemoryStream(imageBytes))
					{
						image = Image.FromStream(ms);
					}
				}
			}
			catch (Exception e)
			{
				SentryLogger.Log(e);
			}

			return image;
		}

		public static long UnixFromDateTime(DateTime dateTime)
		{
			return new DateTimeOffset(dateTime, TimeSpan.Zero).ToUnixTimeSeconds();
		}

		#endregion
	}
}