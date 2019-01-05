using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;

namespace Depressurizer.Models
{
    public class DatabaseEntry
    {
        #region Static Fields

        private static readonly Regex RegexAchievements = new Regex(@"<div (?:id=""achievement_block"" ?|class=""block responsive_apppage_details_right"" ?){2}>\s*<div class=""block_title"">[^\d]*(\d+)[^\d<]*</div>\s*<div class=""communitylink_achievement_images"">", RegexOptions.Compiled);

        private static readonly Regex RegexDevelopers = new Regex(@"(<a href=""(https?:\/\/store\.steampowered\.com\/search\/\?developer=[^""]*|https?:\/\/store\.steampowered\.com\/developer\/[^""]*)"">([^<]+)<\/a>,?\s*)+", RegexOptions.Compiled);

        private static readonly Regex RegexFlags = new Regex(@"<a class=""name"" href=""https?://store\.steampowered\.com/search/\?category2=.*?"">([^<]*)</a>", RegexOptions.Compiled);

        private static readonly Regex RegexGenre = new Regex(@"<div class=""details_block"">\s*<b>[^:]*:</b>.*?<br>\s*<b>[^:]*:</b>\s*(<a href=""https?://store\.steampowered\.com/genre/[^>]*>([^<]+)</a>,?\s*)+\s*<br>", RegexOptions.Compiled);

        private static readonly Regex RegexIsDLC = new Regex(@"<img class=""category_icon"" src=""https?://store\.akamai\.steamstatic\.com/public/images/v6/ico/ico_dlc\.png"">", RegexOptions.Compiled);

        private static readonly Regex RegexIsGame = new Regex(@"<a href=""https?://store\.steampowered\.com/search/\?term=&snr=", RegexOptions.Compiled);

        private static readonly Regex RegexIsSoftware = new Regex(@"<a href=""https?://store\.steampowered\.com/search/\?category1=994&snr=", RegexOptions.Compiled);

        private static readonly Regex RegexLanguageSupport = new Regex(@"<td style=""width: 94px; text-align: left"" class=""ellipsis"">\s*([^<]*)\s*<\/td>[\s\n\r]*<td class=""checkcol"">[\s\n\r]*(.*)[\s\n\r]*<\/td>[\s\n\r]*<td class=""checkcol"">[\s\n\r]*(.*)[\s\n\r]*<\/td>[\s\n\r]*<td class=""checkcol"">[\s\n\r]*(.*)[\s\n\r]*<\/td>", RegexOptions.Compiled);

        private static readonly Regex RegexMetacriticLink = new Regex(@"<div id=""game_area_metalink"">\s*<a href=""https?://www\.metacritic\.com/game/pc/([^""]*)\?ftag=", RegexOptions.Compiled);

        private static readonly Regex RegexPlatformLinux = new Regex(@"<span class=""platform_img linux""></span>", RegexOptions.Compiled);

        private static readonly Regex RegexPlatformMac = new Regex(@"<span class=""platform_img mac""></span>", RegexOptions.Compiled);

        private static readonly Regex RegexPlatformWindows = new Regex(@"<span class=""platform_img win""></span>", RegexOptions.Compiled);

        private static readonly Regex RegexPublishers = new Regex(@"(<a href=""(https?:\/\/store\.steampowered\.com\/search\/\?publisher=[^""]*|https?:\/\/store\.steampowered\.com\/curator\/[^""]*|https?:\/\/store\.steampowered\.com\/publisher\/[^""]*)"">([^<]+)<\/a>,?\s*)+", RegexOptions.Compiled);

        private static readonly Regex RegexReleaseDate = new Regex(@"<div class=""release_date"">\s*<div[^>]*>[^<]*<\/div>\s*<div class=""date"">([^<]+)<\/div>", RegexOptions.Compiled);

        private static readonly Regex RegexReviews = new Regex(@"<span class=""(?:nonresponsive_hidden ?| responsive_reviewdesc ?){2}"">[^\d]*(\d+)%[^\d]*([\d.,]+)[^\d]*\s*</span>", RegexOptions.Compiled);

        private static readonly Regex RegexTags = new Regex(@"<a[^>]*class=""app_tag""[^>]*>([^<]*)</a>", RegexOptions.Compiled);

        private static readonly Regex RegexVrSupportFlagMatch = new Regex(@"<div class=""game_area_details_specs"">.*?<a class=""name"" href=""https?:\/\/store\.steampowered\.com\/search\/\?vrsupport=\d*"">([^<]*)<\/a><\/div>", RegexOptions.Compiled);

        private static readonly Regex RegexVrSupportHeadsetsSection = new Regex(@"<div class=""details_block vrsupport"">(.*)<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">", RegexOptions.Compiled);

        private static readonly Regex RegexVrSupportInputSection = new Regex(@"<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">(.*)<div class=""details_block vrsupport"">", RegexOptions.Compiled);

        private static readonly Regex RegexVrSupportPlayAreaSection = new Regex(@"<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">(.*)", RegexOptions.Compiled);

        #endregion

        #region Fields

        private Collection<string> _developers;

        private Collection<string> _flags;

        private Collection<string> _genres;

        private LanguageSupport _languageSupport;

        private Collection<string> _publishers;

        private Collection<string> _tags;

        private VRSupport _vrSupport;

        #endregion

        #region Constructors and Destructors

        public DatabaseEntry(int appId)
        {
            Id = appId;
        }

        #endregion

        #region Public Properties

        public AppType AppType { get; set; } = AppType.Unknown;

        public Collection<string> Developers
        {
            get => _developers ?? (_developers = new Collection<string>());
            set => _developers = value;
        }

        public Collection<string> Flags
        {
            get => _flags ?? (_flags = new Collection<string>());
            set => _flags = value;
        }

        public Collection<string> Genres
        {
            get => _genres ?? (_genres = new Collection<string>());
            set => _genres = value;
        }

        public int HltbCompletionist { get; set; }

        public int HltbExtras { get; set; }

        public int HltbMain { get; set; }

        public int Id { get; set; }

        /// <remarks>
        ///     TODO: Add field to DB edit dialog
        /// </remarks>
        public LanguageSupport LanguageSupport
        {
            get => _languageSupport ?? (_languageSupport = new LanguageSupport());
            set => _languageSupport = value;
        }

        public long LastAppInfoUpdate { get; set; }

        public long LastStoreScrape { get; set; }

        public string MetacriticUrl { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; } = -1;

        public AppPlatforms Platforms { get; set; } = AppPlatforms.None;

        public Collection<string> Publishers
        {
            get => _publishers ?? (_publishers = new Collection<string>());
            set => _publishers = value;
        }

        public int ReviewPositivePercentage { get; set; }

        public int ReviewTotal { get; set; }

        public string SteamReleaseDate { get; set; }

        public Collection<string> Tags
        {
            get => _tags ?? (_tags = new Collection<string>());
            set => _tags = value;
        }

        public int TotalAchievements { get; set; }

        /// <remarks>
        ///     TODO: Add field to DB edit dialog
        /// </remarks>
        public VRSupport VRSupport
        {
            get => _vrSupport ?? (_vrSupport = new VRSupport());
            set => _vrSupport = value;
        }

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Merges in data from another entry. Useful for merging scrape results, but could also merge data from a different
        ///     database.
        ///     Uses newer data when there is a conflict.
        ///     Does NOT perform deep copies of list fields.
        /// </summary>
        /// <param name="other">DatabaseEntry containing info to be merged into this entry.</param>
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
                if (other.Genres.Count > 0)
                {
                    Genres = other.Genres;
                }

                if (other.Flags.Count > 0)
                {
                    Flags = other.Flags;
                }

                if (other.Tags.Count > 0)
                {
                    Tags = other.Tags;
                }

                if (other.Developers.Count > 0)
                {
                    Developers = other.Developers;
                }

                if (other.Publishers.Count > 0)
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

                // VR Support
                if (other.VRSupport.Headsets.Count > 0)
                {
                    VRSupport.Headsets.Clear();
                    VRSupport.Headsets.AddRange(other.VRSupport.Headsets);
                }

                if (other.VRSupport.Input.Count > 0)
                {
                    VRSupport.Input.Clear();
                    VRSupport.Input.AddRange(other.VRSupport.Input);
                }

                if (other.VRSupport.PlayArea.Count > 0)
                {
                    VRSupport.PlayArea.Clear();
                    VRSupport.PlayArea.AddRange(other.VRSupport.PlayArea);
                }

                // Language Support
                if (other.LanguageSupport.FullAudio.Count > 0)
                {
                    LanguageSupport.FullAudio.Clear();
                    LanguageSupport.FullAudio.AddRange(other.LanguageSupport.FullAudio);
                }

                if (other.LanguageSupport.Interface.Count > 0)
                {
                    LanguageSupport.Interface.Clear();
                    LanguageSupport.Interface.AddRange(other.LanguageSupport.Interface);
                }

                if (other.LanguageSupport.Subtitles.Count > 0)
                {
                    LanguageSupport.Subtitles.Clear();
                    LanguageSupport.Subtitles.AddRange(other.LanguageSupport.Subtitles);
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

        public void ScrapeStore(string languageCode)
        {
            AppType result = ScrapeStoreHelper(languageCode);
            SetTypeFromStoreScrape(result);
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
            // Genres
            Match m = RegexGenre.Match(page);
            if (m.Success)
            {
                Genres.Clear();
                foreach (Capture cap in m.Groups[2].Captures)
                {
                    Genres.Add(cap.Value);
                }
            }

            // Flags
            MatchCollection matches = RegexFlags.Matches(page);
            if (matches.Count > 0)
            {
                Flags.Clear();
                foreach (Match ma in matches)
                {
                    string flag = ma.Groups[1].Value;
                    if (!string.IsNullOrWhiteSpace(flag))
                    {
                        Flags.Add(flag);
                    }
                }
            }

            // Tags
            matches = RegexTags.Matches(page);
            if (matches.Count > 0)
            {
                Tags.Clear();
                foreach (Match ma in matches)
                {
                    string tag = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
                    if (!string.IsNullOrWhiteSpace(tag))
                    {
                        Tags.Add(tag);
                    }
                }
            }

            // Get VR Support headsets
            m = RegexVrSupportHeadsetsSection.Match(page);
            if (m.Success)
            {
                matches = RegexVrSupportFlagMatch.Matches(m.Groups[1].Value.Trim());
                VRSupport.Headsets.Clear();
                foreach (Match ma in matches)
                {
                    string headset = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
                    if (!string.IsNullOrWhiteSpace(headset))
                    {
                        VRSupport.Headsets.Add(headset);
                    }
                }
            }

            // Get VR Support Input
            m = RegexVrSupportInputSection.Match(page);
            if (m.Success)
            {
                matches = RegexVrSupportFlagMatch.Matches(m.Groups[1].Value.Trim());
                VRSupport.Input.Clear();
                foreach (Match ma in matches)
                {
                    string input = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        VRSupport.Input.Add(input);
                    }
                }
            }

            // Get VR Support Play Area
            m = RegexVrSupportPlayAreaSection.Match(page);
            if (m.Success)
            {
                matches = RegexVrSupportFlagMatch.Matches(m.Groups[1].Value.Trim());
                VRSupport.PlayArea.Clear();
                foreach (Match ma in matches)
                {
                    string playArea = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
                    if (!string.IsNullOrWhiteSpace(playArea))
                    {
                        VRSupport.PlayArea.Add(playArea);
                    }
                }
            }

            // Get Language Support
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

            // Get Achievement number
            m = RegexAchievements.Match(page);
            if (m.Success)
            {
                // Sometimes games have achievements but don't have the "Steam Achievements" flag in the store
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
                Developers.Clear();
                foreach (Capture cap in m.Groups[3].Captures)
                {
                    Developers.Add(WebUtility.HtmlDecode(cap.Value));
                }
            }

            // Get Publishers
            m = RegexPublishers.Match(page);
            if (m.Success)
            {
                Publishers.Clear();
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

        private AppType ScrapeStoreHelper(string languageCode)
        {
            // Logger.Verbose(GlobalStrings.GameDB_InitiatingStoreScrapeForGame, Id);

            int redirectTarget = -1;

            HttpWebResponse resp;

            try
            {
                HttpWebRequest req = GetSteamRequest(string.Format(CultureInfo.InvariantCulture, Constants.SteamStoreApp + "?l=" + languageCode, Id));
                resp = (HttpWebResponse) req.GetResponse();

                int count = 0;
                while (resp.StatusCode == HttpStatusCode.Found && count < 5)
                {
                    resp.Close();
                    if (Regexes.IsSteamStore.IsMatch(resp.Headers[HttpResponseHeader.Location]))
                    {
                        Logger.Warn("Scraping {0}: Location header points to the Steam Store homepage, aborting scraping.", Id);
                        return AppType.Unknown;
                    }

                    if (resp.ResponseUri.ToString() == resp.Headers[HttpResponseHeader.Location])
                    {
                        //If page redirects to itself
                        // Logger.Verbose(GlobalStrings.GameDB_RedirectsToItself, Id);
                        return AppType.Unknown;
                    }

                    req = GetSteamRequest(resp.Headers[HttpResponseHeader.Location]);
                    resp = (HttpWebResponse) req.GetResponse();
                    count++;
                }

                if (count == 5 && resp.StatusCode == HttpStatusCode.Found)
                {
                    Logger.Warn("Scraping {0}: Received too many redirects, aborting scraping.", Id);
                    return AppType.Unknown;
                }

                if (resp.ResponseUri.Segments.Length < 2)
                {
                    Logger.Warn("Scraping {0}: Redirected to the Steam Store homepage, aborting scraping.", Id);
                    return AppType.Unknown;
                }

                if (resp.ResponseUri.Segments[1] == "agecheck/")
                {
                    // If we encountered an age gate (cookies should bypass this, but sometimes they don't seem to)
                    if (resp.ResponseUri.Segments.Length >= 4 && resp.ResponseUri.Segments[3].TrimEnd('/') != Id.ToString())
                    {
                        // Age check + redirect
                        // Logger.Verbose(GlobalStrings.GameDB_ScrapingHitAgeCheck, Id, resp.ResponseUri.Segments[3].TrimEnd('/'));
                        if (!int.TryParse(resp.ResponseUri.Segments[3].TrimEnd('/'), out redirectTarget))
                        {
                            // If we got an age check without numeric id (shouldn't happen)
                            return AppType.Unknown;
                        }
                    }
                    else
                    {
                        // If we got an age check with no redirect
                        // Logger.Verbose(GlobalStrings.GameDB_ScrapingAgeCheckNoRedirect, Id);
                        return AppType.Unknown;
                    }
                }
                else if (resp.ResponseUri.Segments[1] != "app/")
                {
                    Logger.Warn("Scraping {0}: Redirected to a non-app URL, aborting scraping.", Id);
                    return AppType.Unknown;
                }
                else if (resp.ResponseUri.Segments.Length < 3)
                {
                    // The URI ends with "/app/" ?
                    // Logger.Verbose(GlobalStrings.GameDB_Log_ScrapingNoAppId, Id);
                    return AppType.Unknown;
                }
                else if (resp.ResponseUri.Segments[2].TrimEnd('/') != Id.ToString())
                {
                    // Redirected to a different app id
                    // Logger.Verbose(GlobalStrings.GameDB_ScrapingRedirectedToOtherApp, Id, resp.ResponseUri.Segments[2].TrimEnd('/'));
                    if (!int.TryParse(resp.ResponseUri.Segments[2].TrimEnd('/'), out redirectTarget))
                    {
                        return AppType.Unknown;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Warn("Scraping {0}: Exception thrown while reading page; {1}.", Id, e);
                return AppType.Unknown;
            }

            Stream responseStream = null;
            string page;
            try
            {
                responseStream = resp.GetResponseStream();
                if (responseStream == null)
                {
                    Logger.Warn("Scraping {0}: The response stream was null, aborting scraping.", Id);
                    return AppType.Unknown;
                }

                using (StreamReader streamReader = new StreamReader(responseStream))
                {
                    page = streamReader.ReadToEnd();
                }

                Logger.Verbose("Scraping {0}: Successfully read page.", Id);
            }
            catch (Exception e)
            {
                Logger.Warn("Scraping {0}: Exception thrown while reading page; {1}.", Id, e);
                return AppType.Unknown;
            }
            finally
            {
                resp?.Dispose();
                responseStream?.Dispose();
            }

            LastStoreScrape = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            AppType result;
            if (page.Contains("<title>Site Error</title>"))
            {
                // Logger.Verbose(GlobalStrings.GameDB_ScrapingReceivedSiteError, Id);
                result = AppType.Unknown;
            }
            else if (RegexIsGame.IsMatch(page) || RegexIsSoftware.IsMatch(page))
            {
                // Here we should have an app, but make sure.

                GetAllDataFromPage(page);

                // Check whether it's DLC and return appropriately
                if (RegexIsDLC.IsMatch(page))
                {
                    // Logger.Verbose(GlobalStrings.GameDB_ScrapingParsedDLC, Id, string.Join(",", Genres));
                    result = AppType.DLC;
                }
                else
                {
                    // Logger.Verbose(GlobalStrings.GameDB_ScrapingParsed, Id, string.Join(",", Genres));
                    result = RegexIsSoftware.IsMatch(page) ? AppType.Application : AppType.Game;
                }
            }
            else
            {
                // The URI is right, but it didn't pass the regex check
                // Logger.Verbose(GlobalStrings.GameDB_ScrapingCouldNotParse, Id);
                result = AppType.Unknown;
            }

            if (redirectTarget == -1)
            {
                return result;
            }

            ParentId = redirectTarget;
            return AppType.Unknown;
        }

        /// <summary>
        ///     Updates the game's type with a type determined during a store scrape. Makes sure that better data (AppInfo type)
        ///     isn't overwritten with worse data.
        /// </summary>
        /// <param name="typeFromStore">Type found from the store scrape</param>
        private void SetTypeFromStoreScrape(AppType typeFromStore)
        {
            if (AppType == AppType.Unknown || typeFromStore != AppType.Unknown && LastAppInfoUpdate == 0)
            {
                AppType = typeFromStore;
            }
        }

        #endregion
    }
}
