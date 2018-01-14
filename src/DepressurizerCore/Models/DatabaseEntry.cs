#region LICENSE

//     This file (DatabaseEntry.cs) is part of DepressurizerCore.
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
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace DepressurizerCore.Models
{
    /// <summary>
    ///     Depressurizer Database Entry
    /// </summary>
    public sealed class DatabaseEntry
    {
        #region Static Fields

        private static readonly Regex RegAchievements = new Regex(@"<div (?:id=""achievement_block"" ?|class=""block responsive_apppage_details_right"" ?){2}>\s*<div class=""block_title"">[^\d]*(\d+)[^\d<]*</div>\s*<div class=""communitylink_achievement_images"">", RegexOptions.Compiled);

        private static readonly Regex RegDevelopers = new Regex(@"(<a href=""http://store\.steampowered\.com/search/\?developer=[^""]*"">([^<]+)</a>,?\s*)+\s*<br>", RegexOptions.Compiled);

        private static readonly Regex RegDlCcheck = new Regex(@"<img class=""category_icon"" src=""http://store\.akamai\.steamstatic\.com/public/images/v6/ico/ico_dlc\.png"">", RegexOptions.Compiled);

        private static readonly Regex RegFlags = new Regex(@"<a class=""name"" href=""http://store\.steampowered\.com/search/\?category2=.*?"">([^<]*)</a>", RegexOptions.Compiled);

        // If these regexes maches a store page, the app is a game, software or dlc respectively
        private static readonly Regex RegGamecheck = new Regex(@"<a href=""http://store\.steampowered\.com/search/\?term=&snr=", RegexOptions.Compiled);

        private static readonly Regex RegGenre = new Regex(@"<div class=""details_block"">\s*<b>[^:]*:</b>.*?<br>\s*<b>[^:]*:</b>\s*(<a href=""http://store\.steampowered\.com/genre/[^>]*>([^<]+)</a>,?\s*)+\s*<br>", RegexOptions.Compiled);

        //Language Support

        private static readonly Regex RegLanguageSupport = new Regex(@"<td style=""width: 94px; text-align: left"" class=""ellipsis"">\s*([^<]*)\s*<\/td>[\s\n\r]*<td class=""checkcol"">[\s\n\r]*(.*)[\s\n\r]*<\/td>[\s\n\r]*<td class=""checkcol"">[\s\n\r]*(.*)[\s\n\r]*<\/td>[\s\n\r]*<td class=""checkcol"">[\s\n\r]*(.*)[\s\n\r]*<\/td>", RegexOptions.Compiled);

        private static readonly Regex RegMetalink = new Regex(@"<div id=""game_area_metalink"">\s*<a href=""http://www\.metacritic\.com/game/pc/([^""]*)\?ftag=", RegexOptions.Compiled);

        private static readonly Regex RegPlatformLinux = new Regex(@"<span class=""platform_img linux""></span>", RegexOptions.Compiled);

        private static readonly Regex RegPlatformMac = new Regex(@"<span class=""platform_img mac""></span>", RegexOptions.Compiled);

        //Platform Support

        private static readonly Regex RegPlatformWindows = new Regex(@"<span class=""platform_img win""></span>", RegexOptions.Compiled);

        private static readonly Regex RegPublishers = new Regex(@"(<a href=""http://store\.steampowered\.com/search/\?publisher=[^""]*"">([^<]+)</a>,?\s*)+\s*<br>", RegexOptions.Compiled);

        private static readonly Regex RegRelDate = new Regex(@"<div class=""release_date"">\s*<div[^>]*>[^<]*<\/div>\s*<div class=""date"">([^<]+)<\/div>", RegexOptions.Compiled);

        private static readonly Regex RegReviews = new Regex(@"<span class=""(?:nonresponsive_hidden ?| responsive_reviewdesc ?){2}"">[^\d]*(\d+)%[^\d]*([\d.,]+)[^\d]*\s*</span>", RegexOptions.Compiled);

        private static readonly Regex RegSoftwarecheck = new Regex(@"<a href=""http://store\.steampowered\.com/search/\?category1=994&snr=", RegexOptions.Compiled);

        private static readonly Regex RegTags = new Regex(@"<a[^>]*class=""app_tag""[^>]*>([^<]*)</a>", RegexOptions.Compiled);

        private static readonly Regex RegVrSupportFlagMatch = new Regex(@"<div class=""game_area_details_specs"">.*?<a class=""name"" href=""http:\/\/store\.steampowered\.com\/search\/\?vrsupport=\d*"">([^<]*)<\/a><\/div>", RegexOptions.Compiled);

        //VR Support
        //regVrSupportHeadsetsSection, regVrSupportInputSection and regVrSupportPlayAreaSection match the whole Headsets, Input and Play Area sections respectively
        //regVrSupportFlagMatch matches the flags inside those sections
        private static readonly Regex RegVrSupportHeadsetsSection = new Regex(@"<div class=""details_block vrsupport"">(.*)<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">", RegexOptions.Compiled);

        private static readonly Regex RegVrSupportInputSection = new Regex(@"<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">(.*)<div class=""details_block vrsupport"">", RegexOptions.Compiled);

        private static readonly Regex RegVrSupportPlayAreaSection = new Regex(@"<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">(.*)", RegexOptions.Compiled);

        #endregion

        #region Fields

        private List<string> _developers;

        private List<string> _flags;

        private List<string> _genres;

        private LanguageSupport _languageSupport;

        private List<string> _publishers;

        private List<string> _tags;

        private VRSupport _vrSupport;

        #endregion

        #region Constructors and Destructors

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
        ///     App Type(s)
        /// </summary>
        public AppType AppType { get; set; } = AppType.Unknown;

        /// <summary>
        ///     Steam Store Banner
        /// </summary>
        public string Banner { get; set; }

        /// <summary>
        ///     Steam Store Developers
        /// </summary>
        public List<string> Developers
        {
            get => _developers ?? (_developers = new List<string>());
            set => _developers = value;
        }

        /// <summary>
        ///     Steam Store Flags
        /// </summary>
        public List<string> Flags
        {
            get => _flags ?? (_flags = new List<string>());
            set => _flags = value;
        }

        /// <summary>
        ///     Steam Store Genres
        /// </summary>
        public List<string> Genres
        {
            get => _genres ?? (_genres = new List<string>());
            set => _genres = value;
        }

        public int HltbCompletionist { get; set; } = 0;

        public int HltbExtras { get; set; } = 0;

        public int HltbMain { get; set; } = 0;

        /// <summary>
        ///     Steam AppId
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Steam Store Language Support
        /// </summary>
        /// TODO: Add field to DB edit dialog
        public LanguageSupport LanguageSupport
        {
            get => _languageSupport ?? (_languageSupport = new LanguageSupport());
            set => _languageSupport = value;
        }

        public long LastAppInfoUpdate { get; set; } = 0;

        public long LastStoreScrape { get; set; } = 0;

        /// <summary>
        ///     Metacritic URL
        /// </summary>
        public string MetacriticUrl { get; set; }

        /// <summary>
        ///     App Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     App ParentId
        /// </summary>
        public int ParentId { get; set; } = -1;

        /// <summary>
        ///     App Platform(s)
        /// </summary>
        public AppPlatforms Platforms { get; set; } = AppPlatforms.None;

        /// <summary>
        ///     Steam Store Publishers
        /// </summary>
        public List<string> Publishers
        {
            get => _publishers ?? (_publishers = new List<string>());
            set => _publishers = value;
        }

        public int ReviewPositivePercentage { get; set; }

        /// <summary>
        ///     Total Steam Store Reviews
        /// </summary>
        public int ReviewTotal { get; set; }

        /// <summary>
        ///     Steam Store Release Date
        /// </summary>
        public string SteamReleaseDate { get; set; }

        /// <summary>
        ///     Steam Store Tags
        /// </summary>
        public List<string> Tags
        {
            get => _tags ?? (_tags = new List<string>());
            set => _tags = value;
        }

        /// <summary>
        ///     Total Achievements
        /// </summary>
        public int TotalAchievements { get; set; } = 0;

        /// <summary>
        ///     Steam Store VR Support
        /// </summary>
        /// TODO: Add field to DB edit dialog
        public VRSupport VRSupport
        {
            get => _vrSupport ?? (_vrSupport = new VRSupport());
            set => _vrSupport = value;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Merges in data from another entry. Useful for merging scrape results, but could also merge data from a different
        ///     database.
        ///     Uses newer data when there is a conflict.
        ///     Does NOT perform deep copies of list fields.
        /// </summary>
        /// <param name="other">GameDBEntry containing info to be merged into this entry.</param>
        public void MergeIn(DatabaseEntry other)
        {
            bool useAppInfoFields = other.LastAppInfoUpdate > LastAppInfoUpdate || LastAppInfoUpdate == 0 && other.LastStoreScrape >= LastStoreScrape;
            bool useScrapeOnlyFields = other.LastStoreScrape >= LastStoreScrape;

            if (other.AppType != AppType.Unknown && (AppType == AppType.Unknown || useAppInfoFields))
            {
                AppType = other.AppType;
            }

            if (other.LastStoreScrape >= LastStoreScrape || LastStoreScrape == 0 && other.LastAppInfoUpdate > LastAppInfoUpdate || Platforms == AppPlatforms.None)
            {
                Platforms = other.Platforms;
            }

            if (string.IsNullOrEmpty(Name))
            {
                if (!string.IsNullOrEmpty(other.Name))
                {
                    Name = other.Name;
                }
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
                if (other.Genres != null && other.Genres.Count > 0)
                {
                    Genres = other.Genres;
                }

                if (other.Flags != null && other.Flags.Count > 0)
                {
                    Flags = other.Flags;
                }

                if (other.Tags != null && other.Tags.Count > 0)
                {
                    Tags = other.Tags;
                }

                if (other.Developers != null && other.Developers.Count > 0)
                {
                    Developers = other.Developers;
                }

                if (other.Publishers != null && other.Publishers.Count > 0)
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
                if (other.VRSupport.Headsets != null && other.VRSupport.Headsets.Count > 0)
                {
                    VRSupport.Headsets = other.VRSupport.Headsets;
                }

                if (other.VRSupport.Input != null && other.VRSupport.Input.Count > 0)
                {
                    VRSupport.Input = other.VRSupport.Input;
                }

                if (other.VRSupport.PlayArea != null && other.VRSupport.PlayArea.Count > 0)
                {
                    VRSupport.PlayArea = other.VRSupport.PlayArea;
                }

                // Language Support
                if (other.LanguageSupport.FullAudio.Count > 0)
                {
                    LanguageSupport.FullAudio = other.LanguageSupport.FullAudio;
                }

                if (other.LanguageSupport.Interface.Count > 0)
                {
                    LanguageSupport.Interface = other.LanguageSupport.Interface;
                }

                if (other.LanguageSupport.Subtitles.Count > 0)
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

        /// <summary>
        ///     Scrapes the store page with this game entry's ID and updates this entry with the information found.
        /// </summary>
        /// <returns>The type determined during the scrape</returns>
        public AppType ScrapeStore()
        {
            return AppType = ScrapeStoreHelper(Id);
        }

        #endregion

        #region Methods

        private static HttpWebRequest GetSteamRequest(string url)
        {
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(url);
            // Cookie bypasses the age gate
            req.CookieContainer = new CookieContainer(3);
            req.CookieContainer.Add(new Cookie("birthtime", "-473392799", "/", "store.steampowered.com"));
            req.CookieContainer.Add(new Cookie("mature_content", "1", "/", "store.steampowered.com"));
            req.CookieContainer.Add(new Cookie("lastagecheckage", "1-January-1955", "/", "store.steampowered.com"));
            // Cookies get discarded on automatic redirects so we have to follow them manually
            req.AllowAutoRedirect = false;

            return req;
        }

        /// <summary>
        ///     Applies all data from a steam store page to this entry
        /// </summary>
        /// <param name="page">The full result of the HTTP request.</param>
        private void GetAllDataFromPage(string page)
        {
            Regex regName = new Regex(@"<span itemprop=""name"">([^<]+)<\/span>", RegexOptions.Compiled);

            Match m = regName.Match(page);
            if (m.Success)
            {
                Name = m.Groups[1].Captures[0].Value;
            }

            // Genres
            m = RegGenre.Match(page);
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
                LanguageSupport.Interface = new List<string>();
                LanguageSupport.FullAudio = new List<string>();
                LanguageSupport.Subtitles = new List<string>();

                foreach (Match ma in matches)
                {
                    string language = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
                    if (language.StartsWith("#lang") || language.StartsWith("("))
                    {
                        continue; //Some store pages on steam are bugged.
                    }

                    // Interface
                    if (!string.IsNullOrEmpty(WebUtility.HtmlDecode(ma.Groups[2].Value.Trim())))
                    {
                        LanguageSupport.Interface.Add(language);
                    }

                    // Full Audio
                    if (!string.IsNullOrEmpty(WebUtility.HtmlDecode(ma.Groups[3].Value.Trim())))
                    {
                        LanguageSupport.FullAudio.Add(language);
                    }

                    // Subtitles
                    if (!string.IsNullOrEmpty(WebUtility.HtmlDecode(ma.Groups[4].Value.Trim())))
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

        private AppType ScrapeStoreHelper(int id)
        {
            string page;

            int redirectTarget = -1;

            HttpWebResponse resp = null;
            try
            {
                string storeLanguage = Settings.Instance.StoreLanguage.ToString().ToLower();
                HttpWebRequest req = GetSteamRequest(string.Format(CultureInfo.InvariantCulture, "http://store.steampowered.com/app/{0}/?l={1}", id, storeLanguage));
                resp = (HttpWebResponse) req.GetResponse();

                int count = 0;
                while (resp.StatusCode == HttpStatusCode.Found && count < 5)
                {
                    resp.Close();
                    if (resp.Headers[HttpResponseHeader.Location] == "http://store.steampowered.com/")
                    {
                        // If we are redirected to the store front page
                        return AppType.Unknown;
                    }

                    if (resp.ResponseUri.ToString() == resp.Headers[HttpResponseHeader.Location])
                    {
                        //If page redirects to itself

                        return AppType.Unknown;
                    }

                    req = GetSteamRequest(resp.Headers[HttpResponseHeader.Location]);
                    resp = (HttpWebResponse) req.GetResponse();
                    count++;
                }

                if (count == 5 && resp.StatusCode == HttpStatusCode.Found)
                {
                    //If we got too many redirects

                    return AppType.Unknown;
                }
                else if (resp.ResponseUri.Segments.Length < 2)
                {
                    return AppType.Unknown;
                }
                else if (resp.ResponseUri.Segments[1] == "agecheck/")
                {
                    // If we encountered an age gate (cookies should bypass this, but sometimes they don't seem to)
                    if (resp.ResponseUri.Segments.Length >= 4 && resp.ResponseUri.Segments[3].TrimEnd('/') != id.ToString(CultureInfo.InvariantCulture))
                    {
                        // Age check + redirect
                        if (int.TryParse(resp.ResponseUri.Segments[3].TrimEnd('/'), out redirectTarget)) { }
                        else
                        {
                            // If we got an age check without numeric id (shouldn't happen)
                            return AppType.Unknown;
                        }
                    }
                    else
                    {
                        // If we got an age check with no redirect

                        return AppType.Unknown;
                    }
                }
                else if (resp.ResponseUri.Segments[1] != "app/")
                {
                    // Redirected outside of the app path

                    return AppType.Unknown;
                }
                else if (resp.ResponseUri.Segments.Length < 3)
                {
                    // The URI ends with "/app/" ?

                    return AppType.Unknown;
                }
                else if (resp.ResponseUri.Segments[2].TrimEnd('/') != id.ToString(CultureInfo.InvariantCulture))
                {
                    // Redirected to a different app id

                    if (!int.TryParse(resp.ResponseUri.Segments[2].TrimEnd('/'), out redirectTarget))
                    {
                        // if new app id is an actual number
                        return AppType.Unknown;
                    }
                }

                StreamReader sr = new StreamReader(resp.GetResponseStream());
                page = sr.ReadToEnd();
            }
            catch (Exception e)
            {
                // Something went wrong with the download.
                return AppType.Unknown;
            }
            finally
            {
                if (resp != null)
                {
                    resp.Close();
                }
            }

            AppType result;

            if (page.Contains("<title>Site Error</title>"))
            {
                result = AppType.Unknown;
            }
            else if (RegGamecheck.IsMatch(page) || RegSoftwarecheck.IsMatch(page))
            {
                // Here we should have an app, but make sure.

                GetAllDataFromPage(page);

                // Check whether it's DLC and return appropriately
                if (RegDlCcheck.IsMatch(page))
                {
                    result = AppType.DLC;
                }
                else
                {
                    result = RegSoftwarecheck.IsMatch(page) ? AppType.Application : AppType.Game;
                }
            }
            else
            {
                // The URI is right, but it didn't pass the regex check

                result = AppType.Unknown;
            }

            if (redirectTarget != -1)
            {
                ParentId = redirectTarget;
                result = AppType.Unknown;
            }

            LastStoreScrape = DateTimeOffset.Now.ToUnixTimeSeconds();

            return result;
        }

        #endregion
    }
}