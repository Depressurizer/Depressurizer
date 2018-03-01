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
using System.Linq;
using System.Net;
using System.Security.Cryptography;

namespace DepressurizerCore.Helpers
{
	public static class Utility
	{
		#region Public Methods and Operators

		public static string CalculateMD5(string filePath)
		{
			if (!File.Exists(filePath))
			{
				return null;
			}

			using (MD5 md5 = MD5.Create())
			{
				using (FileStream stream = File.OpenRead(filePath))
				{
					byte[] hash = md5.ComputeHash(stream);
					return BitConverter.ToString(hash).Replace("-", "").ToUpperInvariant();
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

		public static DateTime DateTimeFromUnix(long unixTimestamp)
		{
			return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).DateTime;
		}

		public static CultureInfo GetCultureInfoFromStoreLanguage(StoreLanguage storeLanguage)
		{
			CultureInfo cultureInfo;

			switch (storeLanguage)
			{
				case StoreLanguage.Default:
					cultureInfo = CultureInfo.GetCultures(CultureTypes.AllCultures).FirstOrDefault(c => c.EnglishName == Settings.Instance.InterfaceLanguage.ToString());
					break;
				case StoreLanguage.Arabic:
					cultureInfo = CultureInfo.GetCultureInfo("ar");
					break;
				case StoreLanguage.Bulgarian:
					cultureInfo = CultureInfo.GetCultureInfo("bg");
					break;
				case StoreLanguage.Schinese:
					cultureInfo = CultureInfo.GetCultureInfo("zh-CN");
					break;
				case StoreLanguage.Tchinese:
					cultureInfo = CultureInfo.GetCultureInfo("zh-TW");
					break;
				case StoreLanguage.Czech:
					cultureInfo = CultureInfo.GetCultureInfo("cs");
					break;
				case StoreLanguage.Danish:
					cultureInfo = CultureInfo.GetCultureInfo("da");
					break;
				case StoreLanguage.Dutch:
					cultureInfo = CultureInfo.GetCultureInfo("nl");
					break;
				case StoreLanguage.English:
					cultureInfo = CultureInfo.GetCultureInfo("en");
					break;
				case StoreLanguage.Finnish:
					cultureInfo = CultureInfo.GetCultureInfo("fi");
					break;
				case StoreLanguage.French:
					cultureInfo = CultureInfo.GetCultureInfo("fr");
					break;
				case StoreLanguage.German:
					cultureInfo = CultureInfo.GetCultureInfo("de");
					break;
				case StoreLanguage.Greek:
					cultureInfo = CultureInfo.GetCultureInfo("el");
					break;
				case StoreLanguage.Hungarian:
					cultureInfo = CultureInfo.GetCultureInfo("hu");
					break;
				case StoreLanguage.Italian:
					cultureInfo = CultureInfo.GetCultureInfo("it");
					break;
				case StoreLanguage.Japanese:
					cultureInfo = CultureInfo.GetCultureInfo("ja");
					break;
				case StoreLanguage.Koreana:
					cultureInfo = CultureInfo.GetCultureInfo("ko");
					break;
				case StoreLanguage.Norwegian:
					cultureInfo = CultureInfo.GetCultureInfo("no");
					break;
				case StoreLanguage.Polish:
					cultureInfo = CultureInfo.GetCultureInfo("pl");
					break;
				case StoreLanguage.Portuguese:
					cultureInfo = CultureInfo.GetCultureInfo("pt");
					break;
				case StoreLanguage.Brazilian:
					cultureInfo = CultureInfo.GetCultureInfo("pt-BR");
					break;
				case StoreLanguage.Romanian:
					cultureInfo = CultureInfo.GetCultureInfo("ro");
					break;
				case StoreLanguage.Russian:
					cultureInfo = CultureInfo.GetCultureInfo("ru");
					break;
				case StoreLanguage.Spanish:
					cultureInfo = CultureInfo.GetCultureInfo("es");
					break;
				case StoreLanguage.Swedish:
					cultureInfo = CultureInfo.GetCultureInfo("sv");
					break;
				case StoreLanguage.Thai:
					cultureInfo = CultureInfo.GetCultureInfo("th");
					break;
				case StoreLanguage.Turkish:
					cultureInfo = CultureInfo.GetCultureInfo("tr");
					break;
				case StoreLanguage.Ukrainian:
					cultureInfo = CultureInfo.GetCultureInfo("uk");
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(storeLanguage), storeLanguage, null);
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
					webClient.Headers.Set("User-Agent", "Depressurizer");

					byte[] imageBytes = webClient.DownloadData(url);
					using (MemoryStream memoryStream = new MemoryStream(imageBytes))
					{
						image = Image.FromStream(memoryStream);
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
