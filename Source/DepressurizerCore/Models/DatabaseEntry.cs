#region LICENSE

//     This file (DatabaseEntry.cs) is part of Depressurizer.
//     Original Copyright (C) 2011  Steve Labbe
//     Modified Copyright (C) 2018  Martijn Vegter
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
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using DepressurizerCore.Helpers;
using Newtonsoft.Json;

namespace DepressurizerCore.Models
{
	public sealed class DatabaseEntry
	{
		#region Static Fields

		private static readonly Regex RegAchievements = new Regex(@"<div (?:id=""achievement_block"" ?|class=""block responsive_apppage_details_right"" ?){2}>\s*<div class=""block_title"">[^\d]*(\d+)[^\d<]*</div>\s*<div class=""communitylink_achievement_images"">", RegexOptions.Compiled);

		private static readonly Regex RegDevelopers = new Regex(@"(<a href=""https://store\.steampowered\.com/search/\?developer=[^""]*"">([^<]+)</a>,?\s*)+\s*<br>", RegexOptions.Compiled);

		private static readonly Regex RegDevelopersTSA = new Regex(@"<a href=""\/gameslist\.aspx\?developer=[^""""]*"" rel=""nofollow"">([^<]*)<\/a>", RegexOptions.Compiled);

		private static readonly Regex RegDlCcheck = new Regex(@"<img class=""category_icon"" src=""https://store\.akamai\.steamstatic\.com/public/images/v6/ico/ico_dlc\.png"">", RegexOptions.Compiled);

		private static readonly Regex RegFlags = new Regex(@"<a class=""name"" href=""https://store\.steampowered\.com/search/\?category2=.*?"">([^<]*)</a>", RegexOptions.Compiled);

		private static readonly Regex RegGamecheck = new Regex(@"<a href=""https://store\.steampowered\.com/search/\?term=&snr=", RegexOptions.Compiled);

		private static readonly Regex RegGenre = new Regex(@"<div class=""details_block"">\s*<b>[^:]*:</b>.*?<br>\s*<b>[^:]*:</b>\s*(<a href=""https://store\.steampowered\.com/genre/[^>]*>([^<]+)</a>,?\s*)+\s*<br>", RegexOptions.Compiled);

		private static readonly Regex RegGenreTSA = new Regex(@"<a href=""\/genre\/"">([^<]*)<\/a>", RegexOptions.Compiled);

		private static readonly Regex RegLanguageSupport = new Regex(@"<td style=""width: 94px; text-align: left"" class=""ellipsis"">\s*([^<]*)\s*<\/td>[\s\n\r]*<td class=""checkcol"">[\s\n\r]*(.*)[\s\n\r]*<\/td>[\s\n\r]*<td class=""checkcol"">[\s\n\r]*(.*)[\s\n\r]*<\/td>[\s\n\r]*<td class=""checkcol"">[\s\n\r]*(.*)[\s\n\r]*<\/td>", RegexOptions.Compiled);

		private static readonly Regex RegMetalink = new Regex(@"<div id=""game_area_metalink"">\s*<a href=""http://www\.metacritic\.com/game/pc/([^""]*)\?ftag=", RegexOptions.Compiled);

		private static readonly Regex RegPlatformLinux = new Regex(@"<span class=""platform_img linux""></span>", RegexOptions.Compiled);

		private static readonly Regex RegPlatformMac = new Regex(@"<span class=""platform_img mac""></span>", RegexOptions.Compiled);

		private static readonly Regex RegPlatformTSA = new Regex(@"<a href=""\/gameslist\.aspx\?platform=[^""""]*"" rel=""nofollow"">([^<]*)<\/a>", RegexOptions.Compiled);

		private static readonly Regex RegPlatformWindows = new Regex(@"<span class=""platform_img win""></span>", RegexOptions.Compiled);

		private static readonly Regex RegPublishers = new Regex(@"(<a href=""https://store\.steampowered\.com/search/\?publisher=[^""]*"">([^<]+)</a>,?\s*)+\s*<br>", RegexOptions.Compiled);

		private static readonly Regex RegPublisherTSA = new Regex(@"<a href=""\/gameslist\.aspx\?publisher=[^""""]*"" rel=""nofollow"">([^<]*)<\/a>", RegexOptions.Compiled);

		private static readonly Regex RegRelDate = new Regex(@"<div class=""release_date"">\s*<div[^>]*>[^<]*<\/div>\s*<div class=""date"">([^<]+)<\/div>", RegexOptions.Compiled);

		private static readonly Regex RegReviews = new Regex(@"<span class=""(?:nonresponsive_hidden ?| responsive_reviewdesc ?){2}"">[^\d]*(\d+)%[^\d]*([\d.,]+)[^\d]*\s*</span>", RegexOptions.Compiled);

		private static readonly Regex RegSoftwarecheck = new Regex(@"<a href=""https://store\.steampowered\.com/search/\?category1=994&snr=", RegexOptions.Compiled);

		private static readonly Regex RegTags = new Regex(@"<a[^>]*class=""app_tag""[^>]*>([^<]*)</a>", RegexOptions.Compiled);

		private static readonly Regex RegVrSupportFlagMatch = new Regex(@"<div class=""game_area_details_specs"">.*?<a class=""name"" href=""http:\/\/store\.steampowered\.com\/search\/\?vrsupport=\d*"">([^<]*)<\/a><\/div>", RegexOptions.Compiled);

		private static readonly Regex RegVrSupportHeadsetsSection = new Regex(@"<div class=""details_block vrsupport"">(.*)<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">", RegexOptions.Compiled);

		private static readonly Regex RegVrSupportInputSection = new Regex(@"<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">(.*)<div class=""details_block vrsupport"">", RegexOptions.Compiled);

		private static readonly Regex RegVrSupportPlayAreaSection = new Regex(@"<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">(.*)", RegexOptions.Compiled);

		#endregion

		#region Constructors and Destructors

		[JsonConstructor]
		public DatabaseEntry() { }

		public DatabaseEntry(int appId)
		{
			Id = appId;
		}

		public DatabaseEntry(int appId, string appName)
		{
			Id = appId;
			Name = appName;
		}

		#endregion

		#region Public Properties

		/// <summary>
		///     App Type
		/// </summary>
		public AppType AppType { get; set; } = AppType.Unknown;

		public List<string> Developers { get; set; } = new List<string>();

		public List<string> Flags { get; set; } = new List<string>();

		public List<string> Genres { get; set; } = new List<string>();

		public int HltbCompletionist { get; set; } = 0;

		public int HltbExtras { get; set; } = 0;

		public int HltbMain { get; set; } = 0;

		/// <summary>
		///     App Id
		/// </summary>
		public int Id { get; set; } = 0;

		public LanguageSupport LanguageSupport { get; set; } = new LanguageSupport(); //TODO: Add field to DB edit dialog

		/// <summary>
		///     Last appinfo update
		/// </summary>
		public long LastAppInfoUpdate { get; set; } = 0;

		/// <summary>
		///     Last store scrape
		/// </summary>
		public long LastStoreScrape { get; set; } = 0;

		/// <summary>
		///     MetaCritic URL
		/// </summary>
		public string MetacriticUrl { get; set; } = null;

		/// <summary>
		///     App Name
		/// </summary>
		public string Name { get; set; } = null;

		public int ParentId { get; set; } = -1;

		public AppPlatforms Platforms { get; set; } = AppPlatforms.None;

		public List<string> Publishers { get; set; } = new List<string>();

		/// <summary>
		///     Positive Steam review percentage
		/// </summary>
		public int ReviewPositivePercentage { get; set; } = 0;

		/// <summary>
		///     Total Steam reviews
		/// </summary>
		public int ReviewTotal { get; set; } = 0;

		/// <summary>
		///     Steam releasedate
		/// </summary>
		public string SteamReleaseDate { get; set; } = null;

		public List<string> Tags { get; set; } = new List<string>();

		/// <summary>
		///     Total Steam achievements
		/// </summary>
		public int TotalAchievements { get; set; } = 0;

		public VRSupport VRSupport { get; set; } = new VRSupport(); //TODO: Add field to DB edit dialog

		#endregion

		#region Public Methods and Operators

		public DatabaseEntry MergeIn(DatabaseEntry otherEntry)
		{
			if (otherEntry == null)
			{
				return this;
			}

			bool useAppInfoFields = (otherEntry.LastAppInfoUpdate > LastAppInfoUpdate) || ((LastAppInfoUpdate == 0) && (otherEntry.LastStoreScrape >= LastStoreScrape));
			bool useScrapeOnlyFields = otherEntry.LastStoreScrape >= LastStoreScrape;

			if ((otherEntry.AppType != AppType.Unknown) && ((AppType == AppType.Unknown) || useAppInfoFields))
			{
				AppType = otherEntry.AppType;
			}

			if ((otherEntry.LastStoreScrape >= LastStoreScrape) || ((LastStoreScrape == 0) && (otherEntry.LastAppInfoUpdate > LastAppInfoUpdate)) || (Platforms == AppPlatforms.None))
			{
				Platforms = otherEntry.Platforms;
			}

			if (useAppInfoFields)
			{
				if (!string.IsNullOrEmpty(otherEntry.Name))
				{
					Name = otherEntry.Name;
				}

				if (otherEntry.ParentId > 0)
				{
					ParentId = otherEntry.ParentId;
				}
			}

			if (useScrapeOnlyFields)
			{
				if ((otherEntry.Genres != null) && (otherEntry.Genres.Count > 0))
				{
					Genres = otherEntry.Genres;
				}

				if ((otherEntry.Flags != null) && (otherEntry.Flags.Count > 0))
				{
					Flags = otherEntry.Flags;
				}

				if ((otherEntry.Tags != null) && (otherEntry.Tags.Count > 0))
				{
					Tags = otherEntry.Tags;
				}

				if ((otherEntry.Developers != null) && (otherEntry.Developers.Count > 0))
				{
					Developers = otherEntry.Developers;
				}

				if ((otherEntry.Publishers != null) && (otherEntry.Publishers.Count > 0))
				{
					Publishers = otherEntry.Publishers;
				}

				if (!string.IsNullOrEmpty(otherEntry.SteamReleaseDate))
				{
					SteamReleaseDate = otherEntry.SteamReleaseDate;
				}

				if (otherEntry.TotalAchievements != 0)
				{
					TotalAchievements = otherEntry.TotalAchievements;
				}

				//VR Support
				if ((otherEntry.VRSupport.Headsets != null) && (otherEntry.VRSupport.Headsets.Count > 0))
				{
					VRSupport.Headsets = otherEntry.VRSupport.Headsets;
				}

				if ((otherEntry.VRSupport.Input != null) && (otherEntry.VRSupport.Input.Count > 0))
				{
					VRSupport.Input = otherEntry.VRSupport.Input;
				}

				if ((otherEntry.VRSupport.PlayArea != null) && (otherEntry.VRSupport.PlayArea.Count > 0))
				{
					VRSupport.PlayArea = otherEntry.VRSupport.PlayArea;
				}

				//Language Support
				if ((otherEntry.LanguageSupport.FullAudio != null) && (otherEntry.LanguageSupport.FullAudio.Count > 0))
				{
					LanguageSupport.FullAudio = otherEntry.LanguageSupport.FullAudio;
				}

				if ((otherEntry.LanguageSupport.Interface != null) && (otherEntry.LanguageSupport.Interface.Count > 0))
				{
					LanguageSupport.Interface = otherEntry.LanguageSupport.Interface;
				}

				if ((otherEntry.LanguageSupport.Subtitles != null) && (otherEntry.LanguageSupport.Subtitles.Count > 0))
				{
					LanguageSupport.Subtitles = otherEntry.LanguageSupport.Subtitles;
				}

				if (otherEntry.ReviewTotal != 0)
				{
					ReviewTotal = otherEntry.ReviewTotal;
					ReviewPositivePercentage = otherEntry.ReviewPositivePercentage;
				}

				if (!string.IsNullOrEmpty(otherEntry.MetacriticUrl))
				{
					MetacriticUrl = otherEntry.MetacriticUrl;
				}
			}

			if (otherEntry.LastStoreScrape > LastStoreScrape)
			{
				LastStoreScrape = otherEntry.LastStoreScrape;
			}

			if (otherEntry.LastAppInfoUpdate > LastAppInfoUpdate)
			{
				LastAppInfoUpdate = otherEntry.LastAppInfoUpdate;
			}

			return this;
		}

		public void ScrapeStore()
		{
			Logger.Instance.Verbose("Scraping {0}: Initializing store scraping for Id: {0}", Id);

			string page;
			int redirectTarget = -1;

			HttpWebResponse resp = null;
			Stream responseStream = null;

			try
			{
				string storePage = string.Format(CultureInfo.InvariantCulture, "https://store.steampowered.com/app/{0}/?l={1}", Id, Settings.Instance.StoreLanguage).ToLowerInvariant();

				HttpWebRequest req = GetSteamRequest(storePage);
				resp = (HttpWebResponse) req.GetResponse();

				int count = 0;
				while ((resp.StatusCode == HttpStatusCode.Found) && (count < 5))
				{
					resp.Close();

					// Check if we were redirected to the Steam Store front page
					if (resp.Headers[HttpResponseHeader.Location] == @"https://store.steampowered.com/")
					{
						Logger.Instance.Warn("Scraping {0}: Redirected to main store page, aborting scraping", Id);
						return;
					}

					// Check if we were redirected to the same page
					if (resp.ResponseUri.ToString() == resp.Headers[HttpResponseHeader.Location])
					{
						Logger.Instance.Warn("Scraping {0}: Store page redirected to itself, aborting scraping", Id);
						return;
					}

					req = GetSteamRequest(resp.Headers[HttpResponseHeader.Location]);
					resp = (HttpWebResponse) req.GetResponse();
					count++;
				}

				// Check if we were redirected too many times
				if ((count == 5) && (resp.StatusCode == HttpStatusCode.Found))
				{
					Logger.Instance.Warn("Scraping {0}: Too many redirects, aborting scraping", Id);
					return;
				}

				// Check if we were redirected to the Steam Store front page
				if (resp.ResponseUri.Segments.Length < 2)
				{
					Logger.Instance.Warn("Scraping {0}: Redirected to main store page, aborting scraping", Id);
					return;
				}

				// Check if we were redirected outside of the app route
				if (resp.ResponseUri.Segments[1] != "app/")
				{
					Logger.Instance.Warn("Scraping {0}: Redirected outside the app (app/) route, aborting scraping", Id);
					return;
				}

				// The URI ends with "/app/" ?
				if (resp.ResponseUri.Segments.Length < 3)
				{
					Logger.Instance.Warn("Scraping {0}: Response URI ends with 'app' thus missing ID found, aborting scraping", Id);
					return;
				}

				// Check if we encountered an age gate, cookies should bypass this, but sometimes they don't seem to
				if (resp.ResponseUri.Segments[1] == "agecheck/")
				{
					// Encountered an age check with no redirect
					if ((resp.ResponseUri.Segments.Length < 4) || (resp.ResponseUri.Segments[3].TrimEnd('/') == Id.ToString(CultureInfo.InvariantCulture)))
					{
						Logger.Instance.Warn("Scraping {0}: Encounterd an age check without redirect, aborting scraping", Id);
						return;
					}

					// Age check + redirect
					Logger.Instance.Verbose("Scraping {0}: Hit age check for Id: {1}", Id, resp.ResponseUri.Segments[3].TrimEnd('/'));

					// Check if we encountered an age gate without a numeric id
					if (!int.TryParse(resp.ResponseUri.Segments[3].TrimEnd('/'), out redirectTarget))
					{
						return;
					}
				}

				// Check if we were redirected to a different Id
				else if (resp.ResponseUri.Segments[2].TrimEnd('/') != Id.ToString(CultureInfo.InvariantCulture))
				{
					// if new app id is an actual number
					if (!int.TryParse(resp.ResponseUri.Segments[2].TrimEnd('/'), out redirectTarget))
					{
						Logger.Instance.Warn("Scraping {0}: Redirected to an unknown Id ({1}), aborting scraping", Id, resp.ResponseUri.Segments[2].TrimEnd('/'));
						return;
					}

					Logger.Instance.Verbose("Scraping {0}: Redirected to another app Id ({1})", Id, resp.ResponseUri.Segments[2].TrimEnd('/'));
				}

				responseStream = resp.GetResponseStream();
				if (responseStream == null)
				{
					Logger.Instance.Warn("Scraping {0}: The response stream was null, aborting scraping", Id);
					return;
				}

				using (StreamReader streamReader = new StreamReader(responseStream))
				{
					page = streamReader.ReadToEnd();
					Logger.Instance.Verbose("Scraping {0}: Page read", Id);
				}
			}
			catch (WebException e)
			{
				if (e.Status == WebExceptionStatus.Timeout)
				{
					return;
				}

				throw;
			}
			catch (Exception e)
			{
				SentryLogger.Log(e);
				throw;
			}
			finally
			{
				if (resp != null)
				{
					resp.Dispose();
				}

				if (responseStream != null)
				{
					responseStream.Dispose();
				}
			}

			// Check for server-sided errors
			if (page.Contains("<title>Site Error</title>"))
			{
				Logger.Instance.Warn("Scraping {0}: Received Site Error, aborting scraping", Id);
				return;
			}

			// Double checking if this is an app (Game or Application)
			if (!RegGamecheck.IsMatch(page) && !RegSoftwarecheck.IsMatch(page))
			{
				Logger.Instance.Warn("Scraping {0}: Could not parse info from page, aborting scraping", Id);
				return;
			}

			LastStoreScrape = Utility.CurrentUnixTime();
			GetAllDataFromPage(page);

			// Set or Update ParentId if we got a redirect target
			if (redirectTarget != -1)
			{
				ParentId = redirectTarget;
			}

			// Check whether it's DLC and return appropriately
			if (RegDlCcheck.IsMatch(page))
			{
				Logger.Instance.Verbose("Scraping {0}: Parsed. DLC. Genre: {1}", Id, string.Join(",", Genres));
				AppType = AppType.DLC;
				return;
			}

			Logger.Instance.Verbose("Scraping {0}: Parsed. Genre: {1}", Id, string.Join(",", Genres));

			if (RegSoftwarecheck.IsMatch(page))
			{
				AppType = AppType.Application;
			}

			if (RegGamecheck.IsMatch(page))
			{
				AppType = AppType.Game;
			}
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

			MatchCollection matches = RegPlatformTSA.Matches(page);
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

			Match match = RegDevelopersTSA.Match(page);
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

			match = RegPublisherTSA.Match(page);
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

			matches = RegGenreTSA.Matches(page);
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

			LastStoreScrape = Utility.CurrentUnixTime();
		}

		#endregion

		#region Methods

		private static HttpWebRequest GetSteamRequest(string url)
		{
			HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(url);

			// Cookies to bypasses the age gate
			webRequest.CookieContainer = new CookieContainer(3);
			webRequest.CookieContainer.Add(new Cookie("birthtime", "-473392799", "/", "store.steampowered.com"));
			webRequest.CookieContainer.Add(new Cookie("mature_content", "1", "/", "store.steampowered.com"));
			webRequest.CookieContainer.Add(new Cookie("lastagecheckage", "1-January-1955", "/", "store.steampowered.com"));

			// Cookies get discarded on automatic redirects so we have to follow them manually
			webRequest.AllowAutoRedirect = false;

			return webRequest;
		}

		private void GetAllDataFromPage(string page)
		{
			if (string.IsNullOrWhiteSpace(page))
			{
				return;
			}

			// Genres
			Match m = RegGenre.Match(page);
			if (m.Success)
			{
				Genres = new List<string>();
				foreach (Capture cap in m.Groups[2].Captures)
				{
					Genres.Add(cap.Value);
				}
			}

			// Flags
			MatchCollection matches = RegFlags.Matches(page);
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
			matches = RegTags.Matches(page);
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
			m = RegVrSupportHeadsetsSection.Match(page);
			if (m.Success)
			{
				matches = RegVrSupportFlagMatch.Matches(m.Groups[1].Value.Trim());
				VRSupport.Headsets = new List<string>();
				foreach (Match ma in matches)
				{
					string headset = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
					if (!string.IsNullOrWhiteSpace(headset))
					{
						VRSupport.Headsets.Add(headset);
					}
				}
			}

			//Get VR Support Input
			m = RegVrSupportInputSection.Match(page);
			if (m.Success)
			{
				matches = RegVrSupportFlagMatch.Matches(m.Groups[1].Value.Trim());
				VRSupport.Input = new List<string>();
				foreach (Match ma in matches)
				{
					string input = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
					if (!string.IsNullOrWhiteSpace(input))
					{
						VRSupport.Input.Add(input);
					}
				}
			}

			//Get VR Support Play Area
			m = RegVrSupportPlayAreaSection.Match(page);
			if (m.Success)
			{
				matches = RegVrSupportFlagMatch.Matches(m.Groups[1].Value.Trim());
				VRSupport.PlayArea = new List<string>();
				foreach (Match ma in matches)
				{
					string playArea = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
					if (!string.IsNullOrWhiteSpace(playArea))
					{
						VRSupport.PlayArea.Add(playArea);
					}
				}
			}

			//Get Language Support
			matches = RegLanguageSupport.Matches(page);
			if (matches.Count > 0)
			{
				LanguageSupport = new LanguageSupport();

				foreach (Match ma in matches)
				{
					string language = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
					if (language.StartsWith("#lang", true, CultureInfo.InvariantCulture) || language.StartsWith("(", true, CultureInfo.InvariantCulture))
					{
						continue; //Some store pages on steam are bugged.
					}

					// Interface
					if (!string.IsNullOrWhiteSpace(WebUtility.HtmlDecode(ma.Groups[2].Value.Trim())))
					{
						LanguageSupport.Interface.Add(language);
					}

					// Full Audio
					if (!string.IsNullOrWhiteSpace(WebUtility.HtmlDecode(ma.Groups[3].Value.Trim())))
					{
						LanguageSupport.FullAudio.Add(language);
					}

					// Subtitles
					if (!string.IsNullOrWhiteSpace(WebUtility.HtmlDecode(ma.Groups[4].Value.Trim())))
					{
						LanguageSupport.Subtitles.Add(language);
					}
				}
			}

			//Get Achievement number
			m = RegAchievements.Match(page);
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
			m = RegDevelopers.Match(page);
			if (m.Success)
			{
				Developers = new List<string>();
				foreach (Capture cap in m.Groups[2].Captures)
				{
					Developers.Add(WebUtility.HtmlDecode(cap.Value));
				}
			}

			// Get Publishers
			m = RegPublishers.Match(page);
			if (m.Success)
			{
				Publishers = new List<string>();
				foreach (Capture cap in m.Groups[2].Captures)
				{
					Publishers.Add(WebUtility.HtmlDecode(cap.Value));
				}
			}

			// Get release date
			m = RegRelDate.Match(page);
			if (m.Success)
			{
				SteamReleaseDate = m.Groups[1].Captures[0].Value;
			}

			// Get user review data
			m = RegReviews.Match(page);
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
			m = RegMetalink.Match(page);
			if (m.Success)
			{
				MetacriticUrl = m.Groups[1].Captures[0].Value;
			}

			// Get Platforms
			m = RegPlatformWindows.Match(page);
			if (m.Success)
			{
				Platforms |= AppPlatforms.Windows;
			}

			m = RegPlatformMac.Match(page);
			if (m.Success)
			{
				Platforms |= AppPlatforms.Mac;
			}

			m = RegPlatformLinux.Match(page);
			if (m.Success)
			{
				Platforms |= AppPlatforms.Linux;
			}
		}

		#endregion
	}
}
