using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Depressurizer.Properties;
using Rallion;

namespace Depressurizer.Models
{
    [XmlRoot(ElementName = "game")]
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

        [DefaultValue(AppTypes.Unknown)] public AppTypes AppType = AppTypes.Unknown;

        [DefaultValue(null)] [XmlElement("Genre")]
        public List<string> Genres = new List<string>();

        [DefaultValue(null)] [XmlElement("Developer")]
        public List<string> Developers = new List<string>();

        [DefaultValue(null)] [XmlArrayItem("Flag")]
        public List<string> Flags = new List<string>();


        // Basics:


        [DefaultValue(0)] public int HltbCompletionist;

        [DefaultValue(0)] public int HltbExtras;

        //howlongtobeat.com times
        [DefaultValue(0)] public int HltbMain;

        public int Id;

        public LanguageSupport LanguageSupport; //TODO: Add field to DB edit dialog

        [DefaultValue(0)] public int LastAppInfoUpdate;

        [DefaultValue(0)] public int LastStoreScrape;

        // Metacritic:
        [DefaultValue(null)] public string MetacriticUrl;
        public string Name;

        [DefaultValue(-1)] public int ParentId = -1;

        public AppPlatforms Platforms = AppPlatforms.None;

        [DefaultValue(null)] [XmlElement("Publisher")]
        public List<string> Publishers = new List<string>();

        [DefaultValue(0)] public int ReviewPositivePercentage;

        [DefaultValue(0)] public int ReviewTotal;

        [DefaultValue(null)] public string SteamReleaseDate;

        [DefaultValue(null)] [XmlArrayItem("Tag")]
        public List<string> Tags = new List<string>();

        [DefaultValue(0)] public int TotalAchievements;

        public VrSupport VrSupport; //TODO: Add field to DB edit dialog

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

            if (other.AppType != AppTypes.Unknown && (AppType == AppTypes.Unknown || useAppInfoFields))
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
                if (other.VrSupport.Headsets != null && other.VrSupport.Headsets.Count > 0)
                {
                    VrSupport.Headsets = other.VrSupport.Headsets;
                }

                if (other.VrSupport.Input != null && other.VrSupport.Input.Count > 0)
                {
                    VrSupport.Input = other.VrSupport.Input;
                }

                if (other.VrSupport.PlayArea != null && other.VrSupport.PlayArea.Count > 0)
                {
                    VrSupport.PlayArea = other.VrSupport.PlayArea;
                }

                //Language Support
                if (other.LanguageSupport.FullAudio != null && other.LanguageSupport.FullAudio.Count > 0)
                {
                    LanguageSupport.FullAudio = other.LanguageSupport.FullAudio;
                }

                if (other.LanguageSupport.Interface != null && other.LanguageSupport.Interface.Count > 0)
                {
                    LanguageSupport.Interface = other.LanguageSupport.Interface;
                }

                if (other.LanguageSupport.Subtitles != null && other.LanguageSupport.Subtitles.Count > 0)
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
        public AppTypes ScrapeStore()
        {
            AppTypes result = ScrapeStoreHelper(Id);
            SetTypeFromStoreScrape(result);
            return result;
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

            // Tags
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

            // Get VR Support headsets
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

            // Get VR Support Input
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

            // Get VR Support Play Area
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

        /// <summary>
        ///     Private helper function to perform scraping work. Downloads the given store page and updates the entry with all
        ///     information found.
        /// </summary>
        /// <param name="id">The id of the store page to scrape</param>
        /// <returns>The type determined during the scrape</returns>
        private AppTypes ScrapeStoreHelper(int id)
        {
            Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_InitiatingStoreScrapeForGame, id);

            string page;

            int redirectTarget = -1;

            int oldTime = LastStoreScrape;

            LastStoreScrape = Utility.GetCurrentUTime();

            HttpWebResponse resp = null;
            Stream responseStream = null;

            try
            {
                string storeLanguage = "en";
                if (Program.Database != null)
                {
                    switch (Program.Database.dbLanguage)
                    {
                        case StoreLanguage.zh_Hans:
                            storeLanguage = "schinese";
                            break;
                        case StoreLanguage.zh_Hant:
                            storeLanguage = "tchinese";
                            break;
                        case StoreLanguage.pt_BR:
                            storeLanguage = "brazilian";
                            break;
                        default:
                            storeLanguage = CultureInfo.GetCultureInfo(Enum.GetName(typeof(StoreLanguage), Program.Database.dbLanguage)).EnglishName.ToLowerInvariant();
                            break;
                    }
                }

                HttpWebRequest req = GetSteamRequest(string.Format(Resources.UrlSteamStoreApp + "?l=" + storeLanguage, id));
                resp = (HttpWebResponse) req.GetResponse();

                int count = 0;
                while (resp.StatusCode == HttpStatusCode.Found && count < 5)
                {
                    resp.Close();
                    if (resp.Headers[HttpResponseHeader.Location] == Resources.UrlSteamStore)
                    {
                        // If we are redirected to the store front page
                        Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingRedirectedToMainStorePage, id);
                        SetTypeFromStoreScrape(AppTypes.Unknown);
                        return AppTypes.Unknown;
                    }

                    if (resp.ResponseUri.ToString() == resp.Headers[HttpResponseHeader.Location])
                    {
                        //If page redirects to itself
                        Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_RedirectsToItself, id);
                        return AppTypes.Unknown;
                    }

                    req = GetSteamRequest(resp.Headers[HttpResponseHeader.Location]);
                    resp = (HttpWebResponse) req.GetResponse();
                    count++;
                }

                if (count == 5 && resp.StatusCode == HttpStatusCode.Found)
                {
                    //If we got too many redirects
                    Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_TooManyRedirects, id);
                    return AppTypes.Unknown;
                }
                else if (resp.ResponseUri.Segments.Length < 2)
                {
                    // If we were redirected to the store front page
                    Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingRedirectedToMainStorePage, id);
                    SetTypeFromStoreScrape(AppTypes.Unknown);
                    return AppTypes.Unknown;
                }
                else if (resp.ResponseUri.Segments[1] == "agecheck/")
                {
                    // If we encountered an age gate (cookies should bypass this, but sometimes they don't seem to)
                    if (resp.ResponseUri.Segments.Length >= 4 && resp.ResponseUri.Segments[3].TrimEnd('/') != id.ToString())
                    {
                        // Age check + redirect
                        Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingHitAgeCheck, id, resp.ResponseUri.Segments[3].TrimEnd('/'));
                        if (int.TryParse(resp.ResponseUri.Segments[3].TrimEnd('/'), out redirectTarget))
                        {
                        }
                        else
                        {
                            // If we got an age check without numeric id (shouldn't happen)
                            return AppTypes.Unknown;
                        }
                    }
                    else
                    {
                        // If we got an age check with no redirect
                        Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingAgeCheckNoRedirect, id);
                        return AppTypes.Unknown;
                    }
                }
                else if (resp.ResponseUri.Segments[1] != "app/")
                {
                    // Redirected outside of the app path
                    Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingRedirectedToNonApp, id);
                    return AppTypes.Other;
                }
                else if (resp.ResponseUri.Segments.Length < 3)
                {
                    // The URI ends with "/app/" ?
                    Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_Log_ScrapingNoAppId, id);
                    return AppTypes.Unknown;
                }
                else if (resp.ResponseUri.Segments[2].TrimEnd('/') != id.ToString())
                {
                    // Redirected to a different app id
                    Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingRedirectedToOtherApp, id, resp.ResponseUri.Segments[2].TrimEnd('/'));
                    if (!int.TryParse(resp.ResponseUri.Segments[2].TrimEnd('/'), out redirectTarget))
                    {
                        return AppTypes.Unknown;
                    }
                }

                responseStream = resp.GetResponseStream();
                if (responseStream == null)
                {
                    Program.Logger.Write(LoggerLevel.Warning, "Scraping {0}: The response stream was null, aborting scraping.", Id);
                    return AppTypes.Unknown;
                }

                using (StreamReader streamReader = new StreamReader(responseStream))
                {
                    page = streamReader.ReadToEnd();
                }

                Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingPageRead, id);
            }
            catch (Exception e)
            {
                // Something went wrong with the download.
                Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingPageReadFailed, id, e.Message);
                LastStoreScrape = oldTime;
                return AppTypes.Unknown;
            }
            finally
            {
                resp?.Dispose();
                responseStream?.Dispose();
            }

            AppTypes result;

            if (page.Contains("<title>Site Error</title>"))
            {
                Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingReceivedSiteError, id);
                result = AppTypes.Unknown;
            }
            else if (RegexIsGame.IsMatch(page) || RegexIsSoftware.IsMatch(page))
            {
                // Here we should have an app, but make sure.

                GetAllDataFromPage(page);

                // Check whether it's DLC and return appropriately
                if (RegexIsDLC.IsMatch(page))
                {
                    Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingParsedDLC, id, string.Join(",", Genres));
                    result = AppTypes.DLC;
                }
                else
                {
                    Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingParsed, id, string.Join(",", Genres));
                    result = RegexIsSoftware.IsMatch(page) ? AppTypes.Application : AppTypes.Game;
                }
            }
            else
            {
                // The URI is right, but it didn't pass the regex check
                Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingCouldNotParse, id);
                result = AppTypes.Unknown;
            }

            if (redirectTarget == -1)
            {
                return result;
            }

            ParentId = redirectTarget;
            result = AppTypes.Unknown;

            return result;
        }

        /// <summary>
        ///     Updates the game's type with a type determined during a store scrape. Makes sure that better data (AppInfo type)
        ///     isn't overwritten with worse data.
        /// </summary>
        /// <param name="typeFromStore">Type found from the store scrape</param>
        private void SetTypeFromStoreScrape(AppTypes typeFromStore)
        {
            if (AppType == AppTypes.Unknown || typeFromStore != AppTypes.Unknown && LastAppInfoUpdate == 0)
            {
                AppType = typeFromStore;
            }
        }

        #endregion
    }
}
