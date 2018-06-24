#region License

//     This file (DatabaseEntry.cs) is part of Depressurizer.
//     Copyright (C) 2011  Steve Labbe
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
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Depressurizer.Core.Enums;
using Depressurizer.Enums;
using Depressurizer.Helpers;
using Depressurizer.Properties;
using Steam = Depressurizer.Core.Helpers.Steam;

namespace Depressurizer.Models
{
	[XmlRoot(ElementName = "game")]
	public sealed class DatabaseEntry
	{
		#region Static Fields

		private static readonly Regex RegexAchievements = new Regex(@"<div (?:id=""achievement_block"" ?|class=""block responsive_apppage_details_right"" ?){2}>\s*<div class=""block_title"">[^\d]*(\d+)[^\d<]*</div>\s*<div class=""communitylink_achievement_images"">", RegexOptions.Compiled);

		private static readonly Regex RegexDevelopers = new Regex(@"(<a href=""(https?:\/\/store\.steampowered\.com\/search\/\?developer=[^""]*|https?:\/\/store\.steampowered\.com\/developer\/[^""]*)"">([^<]+)<\/a>,?\s*)+", RegexOptions.Compiled);

		private static readonly Regex RegexDevelopersTSA = new Regex(@"<a href=""\/gameslist\.aspx\?developer=[^""""]*"" rel=""nofollow"">([^<]*)<\/a>", RegexOptions.Compiled);

		private static readonly Regex RegexFlags = new Regex(@"<a class=""name"" href=""https?://store\.steampowered\.com/search/\?category2=.*?"">([^<]*)</a>", RegexOptions.Compiled);

		private static readonly Regex RegexGenre = new Regex(@"<div class=""details_block"">\s*<b>[^:]*:</b>.*?<br>\s*<b>[^:]*:</b>\s*(<a href=""https?://store\.steampowered\.com/genre/[^>]*>([^<]+)</a>,?\s*)+\s*<br>", RegexOptions.Compiled);

		private static readonly Regex RegexGenreTSA = new Regex(@"<a href=""\/genre\/"">([^<]*)<\/a>", RegexOptions.Compiled);

		private static readonly Regex RegexIsDLC = new Regex(@"<img class=""category_icon"" src=""https?://store\.akamai\.steamstatic\.com/public/images/v6/ico/ico_dlc\.png"">", RegexOptions.Compiled);

		private static readonly Regex RegexIsGame = new Regex(@"<a href=""https?://store\.steampowered\.com/search/\?term=&snr=", RegexOptions.Compiled);

		private static readonly Regex RegexIsSoftware = new Regex(@"<a href=""https?://store\.steampowered\.com/search/\?category1=994&snr=", RegexOptions.Compiled);

		private static readonly Regex RegexLanguageSupport = new Regex(@"<td style=""width: 94px; text-align: left"" class=""ellipsis"">\s*([^<]*)\s*<\/td>[\s\n\r]*<td class=""checkcol"">[\s\n\r]*(.*)[\s\n\r]*<\/td>[\s\n\r]*<td class=""checkcol"">[\s\n\r]*(.*)[\s\n\r]*<\/td>[\s\n\r]*<td class=""checkcol"">[\s\n\r]*(.*)[\s\n\r]*<\/td>", RegexOptions.Compiled);

		private static readonly Regex RegexMetacriticLink = new Regex(@"<div id=""game_area_metalink"">\s*<a href=""https?://www\.metacritic\.com/game/pc/([^""]*)\?ftag=", RegexOptions.Compiled);

		private static readonly Regex RegexPlatformLinux = new Regex(@"<span class=""platform_img linux""></span>", RegexOptions.Compiled);

		private static readonly Regex RegexPlatformMac = new Regex(@"<span class=""platform_img mac""></span>", RegexOptions.Compiled);

		private static readonly Regex RegexPlatformTSA = new Regex(@"<a href=""\/gameslist\.aspx\?platform=[^""""]*"" rel=""nofollow"">([^<]*)<\/a>", RegexOptions.Compiled);

		private static readonly Regex RegexPlatformWindows = new Regex(@"<span class=""platform_img win""></span>", RegexOptions.Compiled);

		private static readonly Regex RegexPublishers = new Regex(@"(<a href=""(https?:\/\/store\.steampowered\.com\/search\/\?publisher=[^""]*|https?:\/\/store\.steampowered\.com\/curator\/[^""]*|https?:\/\/store\.steampowered\.com\/publisher\/[^""]*)"">([^<]+)<\/a>,?\s*)+", RegexOptions.Compiled);

		private static readonly Regex RegexPublisherTSA = new Regex(@"<a href=""\/gameslist\.aspx\?publisher=[^""""]*"" rel=""nofollow"">([^<]*)<\/a>", RegexOptions.Compiled);

		private static readonly Regex RegexReleaseDate = new Regex(@"<div class=""release_date"">\s*<div[^>]*>[^<]*<\/div>\s*<div class=""date"">([^<]+)<\/div>", RegexOptions.Compiled);

		private static readonly Regex RegexReviews = new Regex(@"<span class=""(?:nonresponsive_hidden ?| responsive_reviewdesc ?){2}"">[^\d]*(\d+)%[^\d]*([\d.,]+)[^\d]*\s*</span>", RegexOptions.Compiled);

		private static readonly Regex RegexTags = new Regex(@"<a[^>]*class=""app_tag""[^>]*>([^<]*)</a>", RegexOptions.Compiled);

		private static readonly Regex RegexVrSupportFlagMatch = new Regex(@"<div class=""game_area_details_specs"">.*?<a class=""name"" href=""https?:\/\/store\.steampowered\.com\/search\/\?vrsupport=\d*"">([^<]*)<\/a><\/div>", RegexOptions.Compiled);

		private static readonly Regex RegexVrSupportHeadsetsSection = new Regex(@"<div class=""details_block vrsupport"">(.*)<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">", RegexOptions.Compiled);

		private static readonly Regex RegexVrSupportInputSection = new Regex(@"<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">(.*)<div class=""details_block vrsupport"">", RegexOptions.Compiled);

		private static readonly Regex RegexVrSupportPlayAreaSection = new Regex(@"<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">(.*)", RegexOptions.Compiled);

		#endregion

		#region Fields

		[DefaultValue(AppType.Unknown)]
		public AppType AppType = AppType.Unknown;

		[XmlIgnore]
		public string Banner = null;

		[DefaultValue(null)]
		[XmlElement("Genre")]
		public List<string> Genres = new List<string>();

		[DefaultValue(null)]
		[XmlElement("Developer")]
		public List<string> Developers = new List<string>();

		[DefaultValue(null)]
		[XmlArrayItem("Flag")]
		public List<string> Flags = new List<string>();

		// Basics:

		[DefaultValue(0)]
		public int HltbCompletionist;

		[DefaultValue(0)]
		public int HltbExtras;

		//howlongtobeat.com times
		[DefaultValue(0)]
		public int HltbMain;

		public int Id;

		public LanguageSupport LanguageSupport; //TODO: Add field to DB edit dialog

		[DefaultValue(0)]
		public int LastAppInfoUpdate;

		[DefaultValue(0)]
		public int LastStoreScrape;

		// Metacritic:
		[DefaultValue(null)]
		public string MetacriticUrl;

		public string Name;

		[DefaultValue(-1)]
		public int ParentId = -1;

		public AppPlatforms Platforms = AppPlatforms.None;

		[DefaultValue(null)]
		[XmlElement("Publisher")]
		public List<string> Publishers = new List<string>();

		[DefaultValue(0)]
		public int ReviewPositivePercentage;

		[DefaultValue(0)]
		public int ReviewTotal;

		[DefaultValue(null)]
		public string SteamReleaseDate;

		[DefaultValue(null)]
		[XmlArrayItem("Tag")]
		public List<string> Tags = new List<string>();

		[DefaultValue(0)]
		public int TotalAchievements;

		public VrSupport VrSupport; //TODO: Add field to DB edit dialog

		#endregion

		#region Constructors and Destructors

		public DatabaseEntry()
		{
		}

		public DatabaseEntry(int appId)
		{
			Id = appId;
		}

		#endregion

		#region Properties

		private static Logger Logger => Logger.Instance;

		private static Settings Settings => Settings.Instance;

		#endregion

		#region Public Methods and Operators

		public void Clear()
		{
			Genres = null;
			Flags = null;
			Tags = null;

			Developers = null;
			Publishers = null;

			VrSupport = new VrSupport();
			LanguageSupport = new LanguageSupport();

			SteamReleaseDate = null;

			LastStoreScrape = 1; //pretend it is really old data
		}

		/// <summary>
		///     Merges in data from another entry. Useful for merging scrape results, but could also merge data from a different
		///     database.
		///     Uses newer data when there is a conflict.
		///     Does NOT perform deep copies of list fields.
		/// </summary>
		/// <param name="other">GameDBEntry containing info to be merged into this entry.</param>
		public void MergeIn(DatabaseEntry other)
		{
			bool useAppInfoFields = (other.LastAppInfoUpdate > LastAppInfoUpdate) || ((LastAppInfoUpdate == 0) && (other.LastStoreScrape >= LastStoreScrape));
			bool useScrapeOnlyFields = other.LastStoreScrape >= LastStoreScrape;

			if ((other.AppType != AppType.Unknown) && ((AppType == AppType.Unknown) || useAppInfoFields))
			{
				AppType = other.AppType;
			}

			if ((other.LastStoreScrape >= LastStoreScrape) || ((LastStoreScrape == 0) && (other.LastAppInfoUpdate > LastAppInfoUpdate)) || (Platforms == AppPlatforms.None))
			{
				Platforms = other.Platforms;
			}

			if (useAppInfoFields)
			{
				if (!string.IsNullOrEmpty(other.Name))
				{
					Name = other.Name;
				}

				if (other.ParentId > 0)
				{
					ParentId = other.ParentId;
				}
			}

			if (useScrapeOnlyFields)
			{
				if ((other.Genres != null) && (other.Genres.Count > 0))
				{
					Genres = other.Genres;
				}

				if ((other.Flags != null) && (other.Flags.Count > 0))
				{
					Flags = other.Flags;
				}

				if ((other.Tags != null) && (other.Tags.Count > 0))
				{
					Tags = other.Tags;
				}

				if ((other.Developers != null) && (other.Developers.Count > 0))
				{
					Developers = other.Developers;
				}

				if ((other.Publishers != null) && (other.Publishers.Count > 0))
				{
					Publishers = other.Publishers;
				}

				if (!string.IsNullOrEmpty(other.SteamReleaseDate))
				{
					SteamReleaseDate = other.SteamReleaseDate;
				}

				if (other.TotalAchievements != 0)
				{
					TotalAchievements = other.TotalAchievements;
				}

				//VR Support
				if ((other.VrSupport.Headsets != null) && (other.VrSupport.Headsets.Count > 0))
				{
					VrSupport.Headsets = other.VrSupport.Headsets;
				}

				if ((other.VrSupport.Input != null) && (other.VrSupport.Input.Count > 0))
				{
					VrSupport.Input = other.VrSupport.Input;
				}

				if ((other.VrSupport.PlayArea != null) && (other.VrSupport.PlayArea.Count > 0))
				{
					VrSupport.PlayArea = other.VrSupport.PlayArea;
				}

				//Language Support
				if ((other.LanguageSupport.FullAudio != null) && (other.LanguageSupport.FullAudio.Count > 0))
				{
					LanguageSupport.FullAudio = other.LanguageSupport.FullAudio;
				}

				if ((other.LanguageSupport.Interface != null) && (other.LanguageSupport.Interface.Count > 0))
				{
					LanguageSupport.Interface = other.LanguageSupport.Interface;
				}

				if ((other.LanguageSupport.Subtitles != null) && (other.LanguageSupport.Subtitles.Count > 0))
				{
					LanguageSupport.Subtitles = other.LanguageSupport.Subtitles;
				}

				if (other.ReviewTotal != 0)
				{
					ReviewTotal = other.ReviewTotal;
					ReviewPositivePercentage = other.ReviewPositivePercentage;
				}

				if (!string.IsNullOrEmpty(other.MetacriticUrl))
				{
					MetacriticUrl = other.MetacriticUrl;
				}
			}

			if (other.LastStoreScrape > LastStoreScrape)
			{
				LastStoreScrape = other.LastStoreScrape;
			}

			if (other.LastAppInfoUpdate > LastAppInfoUpdate)
			{
				LastAppInfoUpdate = other.LastAppInfoUpdate;
			}
		}

		public void ScrapeStore()
		{
			Logger.Verbose("Scraping {0}: Initializing store scraping for Id: {0}", Id);

			string page;
			int redirectTarget = -1;

			HttpWebResponse resp = null;
			Stream responseStream = null;

			try
			{
				string storeLanguage = Steam.GetStoreLanguage(Program.Database != null ? Program.Database.dbLanguage : Settings.StoreLanguage);

				HttpWebRequest req = GetSteamRequest(string.Format(Constants.SteamStoreApp + "?l=" + storeLanguage, Id));
				resp = (HttpWebResponse) req.GetResponse();

				int count = 0;
				while ((resp.StatusCode == HttpStatusCode.Found) && (count < 5))
				{
					resp.Close();

					// Check if we were redirected to the Steam Store front page
					if ((resp.Headers[HttpResponseHeader.Location] == @"https://store.steampowered.com/") || (resp.Headers[HttpResponseHeader.Location] == @"http://store.steampowered.com/"))
					{
						Logger.Warn("Scraping {0}: Redirected to main store page, aborting scraping", Id);

						return;
					}

					// Check if we were redirected to the same page
					if (resp.ResponseUri.ToString() == resp.Headers[HttpResponseHeader.Location])
					{
						Logger.Warn("Scraping {0}: Store page redirected to itself, aborting scraping", Id);

						return;
					}

					req = GetSteamRequest(resp.Headers[HttpResponseHeader.Location]);
					resp = (HttpWebResponse) req.GetResponse();
					count++;
				}

				// Check if we were redirected too many times
				if ((count == 5) && (resp.StatusCode == HttpStatusCode.Found))
				{
					Logger.Warn("Scraping {0}: Too many redirects, aborting scraping", Id);

					return;
				}

				// Check if we were redirected to the Steam Store front page
				if (resp.ResponseUri.Segments.Length < 2)
				{
					Logger.Warn("Scraping {0}: Redirected to main store page, aborting scraping", Id);

					return;
				}

				// Check if we encountered an age gate, cookies should bypass this, but sometimes they don't seem to
				if (resp.ResponseUri.Segments[1] == "agecheck/")
				{
					// Encountered an age check with no redirect
					if ((resp.ResponseUri.Segments.Length < 4) || (resp.ResponseUri.Segments[3].TrimEnd('/') == Id.ToString(CultureInfo.InvariantCulture)))
					{
						Logger.Warn("Scraping {0}: Encounterd an age check without redirect, aborting scraping", Id);

						return;
					}

					// Age check + redirect
					Logger.Warn("Scraping {0}: Hit age check for Id: {1}", Id, resp.ResponseUri.Segments[3].TrimEnd('/'));

					// Check if we encountered an age gate without a numeric id
					if (!int.TryParse(resp.ResponseUri.Segments[3].TrimEnd('/'), out redirectTarget))
					{
						return;
					}
				}

				// Check if we were redirected outside of the app route
				if (resp.ResponseUri.Segments[1] != "app/")
				{
					Logger.Warn("Scraping {0}: Redirected outside the app (app/) route, aborting scraping", Id);

					return;
				}

				// The URI ends with "/app/" ?
				if (resp.ResponseUri.Segments.Length < 3)
				{
					Logger.Warn("Scraping {0}: Response URI ends with 'app' thus missing ID found, aborting scraping", Id);

					return;
				}

				// Check if we were redirected to a different Id
				if (resp.ResponseUri.Segments[2].TrimEnd('/') != Id.ToString())
				{
					if (!int.TryParse(resp.ResponseUri.Segments[2].TrimEnd('/'), out redirectTarget))
					{
						Logger.Warn("Scraping {0}: Redirected to an unknown Id \"{1}\", aborting scraping", Id, resp.ResponseUri.Segments[2].TrimEnd('/'));

						return;
					}

					Logger.Warn("Scraping {0}: Redirected to another app Id \"{1}\"", Id, resp.ResponseUri.Segments[2].TrimEnd('/'));
				}

				responseStream = resp.GetResponseStream();
				if (responseStream == null)
				{
					Logger.Warn("Scraping {0}: The response stream was null, aborting scraping", Id);

					return;
				}

				using (StreamReader streamReader = new StreamReader(responseStream))
				{
					page = streamReader.ReadToEnd();
					Logger.Verbose("Scraping {0}: Page read", Id);
				}
			}
			catch (WebException e)
			{
				if (e.Status == WebExceptionStatus.Timeout)
				{
					LastStoreScrape = 1;

					return;
				}

				HttpStatusCode response = ((HttpWebResponse) e.Response).StatusCode;
				if (response != HttpStatusCode.InternalServerError)
				{
					throw;
				}

				LastStoreScrape = 1;

				return;
			}
			catch (Exception e)
			{
				Logger.Warn("Scraping {0}: Page read failed. {1}", Id, e.Message);

				return;
			}
			finally
			{
				resp?.Dispose();
				responseStream?.Dispose();
			}

			if (page.Contains("<title>Site Error</title>"))
			{
				Logger.Warn("Scraping {0}: Received Site Error, aborting scraping", Id);
				LastStoreScrape = 1;

				return;
			}

			if (!RegexIsGame.IsMatch(page) && !RegexIsSoftware.IsMatch(page))
			{
				Logger.Warn("Scraping {0}: Could not parse info from page, aborting scraping", Id);

				return;
			}

			LastStoreScrape = Utility.GetCurrentUTime();
			GetAllDataFromPage(page);

			if (RegexIsDLC.IsMatch(page))
			{
				AppType = AppType.DLC;
			}

			if (RegexIsGame.IsMatch(page))
			{
				AppType = AppType.Game;
			}

			if (RegexIsSoftware.IsMatch(page))
			{
				AppType = AppType.Application;
			}

			if (redirectTarget != -1)
			{
				ParentId = redirectTarget;
			}

			Logger.Info("Scraping {0}: Parsed. Genre: {1}", Id, string.Join(",", Genres));
		}

		public void ScrapeTrueSteamAchievements()
		{
			// We can only scrape TrueSteamAchievements in English
			if (Settings.Instance.StoreLanguage != StoreLanguage.English)
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(Name))
			{
				return;
			}

			string page;
			string name = Name.Replace(" ", "-").Replace(":", "").Replace("'", "");
			string hubPage = string.Format(CultureInfo.InvariantCulture, "https://truesteamachievements.com/game/{0}", name);

			using (WebClient webClient = new WebClient())
			{
				page = webClient.DownloadString(hubPage);
			}

			MatchCollection matches = RegexPlatformTSA.Matches(page);
			if (matches.Count > 0)
			{
				Platforms = AppPlatforms.None;

				foreach (Match m in matches)
				{
					string platform = m.Groups[1].Value;

					if (platform.Contains("Windows"))
					{
						Platforms |= AppPlatforms.Windows;
					}

					if (platform.Contains("Mac"))
					{
						Platforms |= AppPlatforms.Mac;
					}

					if (platform.Contains("Linux"))
					{
						Platforms |= AppPlatforms.Linux;
					}
				}
			}

			Match match = RegexDevelopersTSA.Match(page);
			if (match.Success)
			{
				string developer = match.Groups[1].Value;
				if (!string.IsNullOrWhiteSpace(developer))
				{
					Developers = new List<string>();

					string[] developers = developer.Split(',');
					foreach (string dev in developers)
					{
						if (!string.IsNullOrWhiteSpace(dev))
						{
							Developers.Add(dev);
						}
					}
				}
			}

			match = RegexPublisherTSA.Match(page);
			if (match.Success)
			{
				string publisher = match.Groups[1].Value;
				if (!string.IsNullOrWhiteSpace(publisher))
				{
					Publishers = new List<string>();

					string[] publishers = publisher.Split(',');
					foreach (string pub in publishers)
					{
						if (!string.IsNullOrWhiteSpace(pub))
						{
							Publishers.Add(pub);
						}
					}
				}
			}

			matches = RegexGenreTSA.Matches(page);
			if (matches.Count > 0)
			{
				Genres = new List<string>();
				foreach (Match m in matches)
				{
					string genre = WebUtility.HtmlDecode(m.Groups[1].Value.Trim());
					if (!string.IsNullOrWhiteSpace(genre))
					{
						Genres.Add(genre);
					}
				}
			}

			LastStoreScrape = Utility.GetCurrentUTime();
		}

		#endregion

		#region Methods

		private static HttpWebRequest GetSteamRequest(string url)
		{
			HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);

			request.CookieContainer = new CookieContainer(3);
			request.CookieContainer.Add(new Cookie("birthtime", "-473392799", "/", "store.steampowered.com"));
			request.CookieContainer.Add(new Cookie("mature_content", "1", "/", "store.steampowered.com"));
			request.CookieContainer.Add(new Cookie("lastagecheckage", "1-January-1955", "/", "store.steampowered.com"));

			request.AllowAutoRedirect = false;

			return request;
		}

		private void GetAllDataFromPage(string page)
		{
			// Genres
			Match m = RegexGenre.Match(page);
			if (m.Success)
			{
				Genres = new List<string>();
				foreach (Capture cap in m.Groups[2].Captures)
				{
					Genres.Add(cap.Value);
				}
			}

			// Flags
			MatchCollection matches = RegexFlags.Matches(page);
			if (matches.Count > 0)
			{
				Flags = new List<string>();
				foreach (Match ma in matches)
				{
					string flag = ma.Groups[1].Value;
					if (!string.IsNullOrWhiteSpace(flag))
					{
						Flags.Add(flag);
					}
				}
			}

			//Tags
			matches = RegexTags.Matches(page);
			if (matches.Count > 0)
			{
				Tags = new List<string>();
				foreach (Match ma in matches)
				{
					string tag = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
					if (!string.IsNullOrWhiteSpace(tag))
					{
						Tags.Add(tag);
					}
				}
			}

			//Get VR Support headsets
			m = RegexVrSupportHeadsetsSection.Match(page);
			if (m.Success)
			{
				matches = RegexVrSupportFlagMatch.Matches(m.Groups[1].Value.Trim());
				VrSupport.Headsets = new List<string>();
				foreach (Match ma in matches)
				{
					string headset = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
					if (!string.IsNullOrWhiteSpace(headset))
					{
						VrSupport.Headsets.Add(headset);
					}
				}
			}

			//Get VR Support Input
			m = RegexVrSupportInputSection.Match(page);
			if (m.Success)
			{
				matches = RegexVrSupportFlagMatch.Matches(m.Groups[1].Value.Trim());
				VrSupport.Input = new List<string>();
				foreach (Match ma in matches)
				{
					string input = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
					if (!string.IsNullOrWhiteSpace(input))
					{
						VrSupport.Input.Add(input);
					}
				}
			}

			//Get VR Support Play Area
			m = RegexVrSupportPlayAreaSection.Match(page);
			if (m.Success)
			{
				matches = RegexVrSupportFlagMatch.Matches(m.Groups[1].Value.Trim());
				VrSupport.PlayArea = new List<string>();
				foreach (Match ma in matches)
				{
					string playArea = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
					if (!string.IsNullOrWhiteSpace(playArea))
					{
						VrSupport.PlayArea.Add(playArea);
					}
				}
			}

			//Get Language Support
			matches = RegexLanguageSupport.Matches(page);
			if (matches.Count > 0)
			{
				LanguageSupport = new LanguageSupport
				{
					Interface = new List<string>(),
					FullAudio = new List<string>(),
					Subtitles = new List<string>()
				};

				foreach (Match ma in matches)
				{
					string language = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
					if (language.StartsWith("#lang") || language.StartsWith("("))
					{
						continue; //Some store pages on steam are bugged.
					}

					if (WebUtility.HtmlDecode(ma.Groups[2].Value.Trim()) != "") //Interface
					{
						LanguageSupport.Interface.Add(language);
					}

					if (WebUtility.HtmlDecode(ma.Groups[3].Value.Trim()) != "") //Full Audio
					{
						LanguageSupport.FullAudio.Add(language);
					}

					if (WebUtility.HtmlDecode(ma.Groups[4].Value.Trim()) != "") //Subtitles
					{
						LanguageSupport.Subtitles.Add(language);
					}
				}
			}

			//Get Achievement number
			m = RegexAchievements.Match(page);
			if (m.Success)
			{
				//sometimes games have achievements but don't have the "Steam Achievements" flag in the store
				if (!Flags.Contains("Steam Achievements"))
				{
					Flags.Add("Steam Achievements");
				}

				if (int.TryParse(m.Groups[1].Value, out int num))
				{
					TotalAchievements = num;
				}
			}

			// Get Developer
			m = RegexDevelopers.Match(page);
			if (m.Success)
			{
				Developers = new List<string>();
				foreach (Capture cap in m.Groups[3].Captures)
				{
					Developers.Add(WebUtility.HtmlDecode(cap.Value));
				}
			}

			// Get Publishers
			m = RegexPublishers.Match(page);
			if (m.Success)
			{
				Publishers = new List<string>();
				foreach (Capture cap in m.Groups[3].Captures)
				{
					Publishers.Add(WebUtility.HtmlDecode(cap.Value));
				}
			}

			// Get release date
			m = RegexReleaseDate.Match(page);
			if (m.Success)
			{
				SteamReleaseDate = m.Groups[1].Captures[0].Value;
			}

			// Get user review data
			m = RegexReviews.Match(page);
			if (m.Success)
			{
				if (int.TryParse(m.Groups[1].Value, out int num))
				{
					ReviewPositivePercentage = num;
				}

				if (int.TryParse(m.Groups[2].Value, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out num))
				{
					ReviewTotal = num;
				}
			}

			// Get metacritic url
			m = RegexMetacriticLink.Match(page);
			if (m.Success)
			{
				MetacriticUrl = m.Groups[1].Captures[0].Value;
			}

			// Get Platforms
			m = RegexPlatformWindows.Match(page);
			if (m.Success)
			{
				Platforms |= AppPlatforms.Windows;
			}

			m = RegexPlatformMac.Match(page);
			if (m.Success)
			{
				Platforms |= AppPlatforms.Mac;
			}

			m = RegexPlatformLinux.Match(page);
			if (m.Success)
			{
				Platforms |= AppPlatforms.Linux;
			}
		}

		#endregion
	}
}