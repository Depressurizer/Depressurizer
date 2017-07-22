/*
    This file is part of Depressurizer.
    Original work Copyright 2011, 2012, 2013 Steve Labbe.
    Modified work Copyright 2017 Martijn Vegter.

    Depressurizer is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Depressurizer is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Depressurizer.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Depressurizer.Model;
using Depressurizer.Properties;
using Newtonsoft.Json.Linq;
using Rallion;

namespace Depressurizer
{
    public struct VrSupport
    {
        public List<string> Headsets;
        public List<string> Input;
        public List<string> PlayArea;
    }

    public struct LanguageSupport
    {
        public List<string> Interface;
        public List<string> FullAudio;
        public List<string> Subtitles;
    }

    public class GameDBEntry
    {
        #region Fields

        public int Id;
        public string Name;
        public AppTypes AppType = AppTypes.Unknown;
        public int ParentId = -1;
        public AppPlatforms Platforms = AppPlatforms.None;

        // Basics:
        public List<string> Genres = new List<string>();

        public List<string> Flags = new List<string>();
        public List<string> Tags = new List<string>();
        public List<string> Developers;
        public List<string> Publishers;
        public string SteamReleaseDate;
        public int Achievements;

        public VrSupport vrSupport;     //TODO: Add field to DB edit dialog

        public LanguageSupport languageSupport;     //TODO: Add field to DB edit dialog

        public string Banner = null;

        public int ReviewTotal;
        public int ReviewPositivePercentage;

        //howlongtobeat.com times
        public int HltbMain;

        public int HltbExtras = -0;
        public int HltbCompletionist;

        // Metacritic:
        public string MC_Url;

        public int LastStoreScrape;
        public int LastAppInfoUpdate;

        #endregion

        #region Regex

        // If these regexes maches a store page, the app is a game, software or dlc respectively
        private static Regex regGamecheck = new Regex(@"<a href=""http://store\.steampowered\.com/search/\?term=&snr=",
            RegexOptions.Compiled);

        private static Regex regSoftwarecheck =
            new Regex(@"<a href=""http://store\.steampowered\.com/search/\?category1=994&snr=", RegexOptions.Compiled);

        private static Regex regDLCcheck =
            new Regex(
                @"<img class=""category_icon"" src=""http://store\.akamai\.steamstatic\.com/public/images/v6/ico/ico_dlc\.png"">",
                RegexOptions.Compiled);

        private static Regex regGenre =
            new Regex(
                @"<div class=""details_block"">\s*<b>[^:]*:</b>[^<]*<br>\s*<b>[^:]*:</b>\s*(<a[^>]*>([^<]+)</a>,?\s*)+\s*<br>",
                RegexOptions.Compiled);

        private static Regex regFlags =
            new Regex(@"<a class=""name"" href=""http://store\.steampowered\.com/search/\?category2=.*?"">([^<]*)</a>",
                RegexOptions.Compiled);

        private static Regex regTags = new Regex(@"<a[^>]*class=""app_tag""[^>]*>([^<]*)</a>", RegexOptions.Compiled);

        private static Regex regDevelopers =
            new Regex(
                @"(<a href=""http://store\.steampowered\.com/search/\?developer=[^""]*"">([^<]+)</a>,?\s*)+\s*<br>",
                RegexOptions.Compiled);

        private static Regex regPublishers =
            new Regex(
                @"(<a href=""http://store\.steampowered\.com/search/\?publisher=[^""]*"">([^<]+)</a>,?\s*)+\s*<br>",
                RegexOptions.Compiled);

        private static Regex regRelDate =
            new Regex(regPublishers + @"\s*<b>[^:]*:</b>\s*(.*)\s*<br>", RegexOptions.Compiled);

        private static Regex regMetalink =
            new Regex(
                @"<div id=""game_area_metalink"">\s*<a href=""http://www\.metacritic\.com/game/pc/([^""]*)\?ftag=",
                RegexOptions.Compiled);

        private static Regex regReviews =
            new Regex(
                @"<span class=""(?:nonresponsive_hidden ?| responsive_reviewdesc ?){2}"">[^\d]*(\d+)%[^\d]*([\d.,]+)[^\d]*\s*</span>",
                RegexOptions.Compiled);

        private static Regex regAchievements =
            new Regex(
                @"<div (?:id=""achievement_block"" ?|class=""block responsive_apppage_details_right"" ?){2}>\s*<div class=""block_title"">[^\d]*(\d+)[^\d<]*</div>\s*<div class=""communitylink_achievement_images"">",
                RegexOptions.Compiled);

        //VR Support
        //regVrSupportHeadsetsSection, regVrSupportInputSection and regVrSupportPlayAreaSection match the whole Headsets, Input and Play Area sections respectively
        //regVrSupportFlagMatch matches the flags inside those sections
        private static Regex regVrSupportHeadsetsSection =
            new Regex(
                @"<div class=""details_block vrsupport"">(.*)<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">",
                RegexOptions.Compiled);

        private static Regex regVrSupportInputSection =
            new Regex(
                @"<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">(.*)<div class=""details_block vrsupport"">",
                RegexOptions.Compiled);

        private static Regex regVrSupportPlayAreaSection =
            new Regex(
                @"<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">.*<div class=""details_block vrsupport"">(.*)",
                RegexOptions.Compiled);

        private static Regex regVrSupportFlagMatch =
            new Regex(
                @"<div class=""game_area_details_specs"">.*?<a class=""name"" href=""http:\/\/store\.steampowered\.com\/search\/\?vrsupport=\d*"">([^<]*)<\/a><\/div>",
                RegexOptions.Compiled);

        //Language Support

        private static Regex regLanguageSupport = new Regex(@"<td style=""width: 94px; text-align: left"" class=""ellipsis"">\s*([^<]*)\s*<\/td>[\s\n\r]*<td class=""checkcol"">[\s\n\r]*(.*)[\s\n\r]*<\/td>[\s\n\r]*<td class=""checkcol"">[\s\n\r]*(.*)[\s\n\r]*<\/td>[\s\n\r]*<td class=""checkcol"">[\s\n\r]*(.*)[\s\n\r]*<\/td>", RegexOptions.Compiled);

        //Platform Support

        private static Regex regPlatformWindows =
            new Regex(@"<span class=""platform_img win""></span>", RegexOptions.Compiled);

        private static Regex regPlatformMac =
            new Regex(@"<span class=""platform_img mac""></span>", RegexOptions.Compiled);

        private static Regex regPlatformLinux =
            new Regex(@"<span class=""platform_img linux""></span>", RegexOptions.Compiled);

        #endregion

        #region Scraping

        /// <summary>
        /// Scrapes the store page with this game entry's ID and updates this entry with the information found.
        /// </summary>
        /// <returns>The type determined during the scrape</returns>
        public AppTypes ScrapeStore()
        {
            AppTypes result = ScrapeStoreHelper(Id);
            SetTypeFromStoreScrape(result);
            return result;
        }

        /// <summary>
        /// Private helper function to perform scraping work. Downloads the given store page and updates the entry with all information found.
        /// </summary>
        /// <param name="id">The id of the store page to scrape</param>
        /// <returns>The type determined during the scrape</returns>
        private AppTypes ScrapeStoreHelper(int id)
        {
            Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_InitiatingStoreScrapeForGame, id);

            string page = "";

            int redirectTarget = -1;

            int oldTime = LastStoreScrape;

            LastStoreScrape = Utility.GetCurrentUTime();

            HttpWebResponse resp = null;
            try
            {
                string storeLanguage = "en";
                if (Program.GameDB != null)
                {
                    if (Program.GameDB.dbLanguage == StoreLanguage.zh_Hans)
                    {
                        storeLanguage = "schinese";
                    }
                    else if (Program.GameDB.dbLanguage == StoreLanguage.zh_Hant)
                    {
                        storeLanguage = "tchinese";
                    }
                    else if (Program.GameDB.dbLanguage == StoreLanguage.pt_BR)
                    {
                        storeLanguage = "brazilian";
                    }
                    else
                    {
                        storeLanguage = CultureInfo
                            .GetCultureInfo(Enum.GetName(typeof(StoreLanguage), Program.GameDB.dbLanguage)).EnglishName
                            .ToLowerInvariant();
                    }
                }
                HttpWebRequest req =
                    GetSteamRequest(string.Format(Resources.UrlSteamStoreApp + "?l=" + storeLanguage, id));
                resp = (HttpWebResponse) req.GetResponse();

                int count = 0;
                while ((resp.StatusCode == HttpStatusCode.Found) && (count < 5))
                {
                    resp.Close();
                    if (resp.Headers[HttpResponseHeader.Location] == Resources.UrlSteamStore)
                    {
                        // If we are redirected to the store front page
                        Program.Logger.Write(LoggerLevel.Verbose,
                            GlobalStrings.GameDB_ScrapingRedirectedToMainStorePage, id);
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

                if ((count == 5) && (resp.StatusCode == HttpStatusCode.Found))
                {
                    //If we got too many redirects
                    Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_TooManyRedirects, id);
                    return AppTypes.Unknown;
                }
                else if (resp.ResponseUri.Segments.Length < 2)
                {
                    // If we were redirected to the store front page
                    Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingRedirectedToMainStorePage,
                        id);
                    SetTypeFromStoreScrape(AppTypes.Unknown);
                    return AppTypes.Unknown;
                }
                else if (resp.ResponseUri.Segments[1] == "agecheck/")
                {
                    // If we encountered an age gate (cookies should bypass this, but sometimes they don't seem to)
                    if ((resp.ResponseUri.Segments.Length >= 4) &&
                        (resp.ResponseUri.Segments[3].TrimEnd('/') != id.ToString()))
                    {
                        // Age check + redirect
                        Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingHitAgeCheck, id,
                            resp.ResponseUri.Segments[3].TrimEnd('/'));
                        if (int.TryParse(resp.ResponseUri.Segments[3].TrimEnd('/'), out redirectTarget)) { }
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
                    Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingRedirectedToOtherApp, id,
                        resp.ResponseUri.Segments[2].TrimEnd('/'));
                    if (!int.TryParse(resp.ResponseUri.Segments[2].TrimEnd('/'), out redirectTarget))
                    {
                        // if new app id is an actual number
                        return AppTypes.Unknown;
                    }
                }

                StreamReader sr = new StreamReader(resp.GetResponseStream());
                page = sr.ReadToEnd();
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
                if (resp != null)
                {
                    resp.Close();
                }
            }

            AppTypes result = AppTypes.Unknown;

            if (page.Contains("<title>Site Error</title>"))
            {
                Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingReceivedSiteError, id);
                result = AppTypes.Unknown;
            }
            else if (regGamecheck.IsMatch(page) || regSoftwarecheck.IsMatch(page))
            {
                // Here we should have an app, but make sure.

                GetAllDataFromPage(page);

                // Check whether it's DLC and return appropriately
                if (regDLCcheck.IsMatch(page))
                {
                    Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingParsedDLC, id,
                        string.Join(",", Genres));
                    result = AppTypes.DLC;
                }
                else
                {
                    Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingParsed, id,
                        string.Join(",", Genres));
                    result = regSoftwarecheck.IsMatch(page) ? AppTypes.Application : AppTypes.Game;
                }
            }
            else
            {
                // The URI is right, but it didn't pass the regex check
                Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingCouldNotParse, id);
                result = AppTypes.Unknown;
            }

            if (redirectTarget != -1)
            {
                ParentId = redirectTarget;
                result = AppTypes.Unknown;
            }

            return result;
        }

        private HttpWebRequest GetSteamRequest(string url)
        {
            HttpWebRequest req = (HttpWebRequest) HttpWebRequest.Create(url);
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
        /// Updates the game's type with a type determined during a store scrape. Makes sure that better data (AppInfo type) isn't overwritten with worse data.
        /// </summary>
        /// <param name="typeFromStore">Type found from the store scrape</param>
        private void SetTypeFromStoreScrape(AppTypes typeFromStore)
        {
            if ((AppType == AppTypes.Unknown) || ((typeFromStore != AppTypes.Unknown) && (LastAppInfoUpdate == 0)))
            {
                AppType = typeFromStore;
            }
        }

        /// <summary>
        /// Applies all data from a steam store page to this entry
        /// </summary>
        /// <param name="page">The full result of the HTTP request.</param>
        private void GetAllDataFromPage(string page)
        {
            // Genres
            Match m = regGenre.Match(page);
            if (m.Success)
            {
                Genres = new List<string>();
                foreach (Capture cap in m.Groups[2].Captures)
                {
                    Genres.Add(cap.Value);
                }
            }

            // Flags
            MatchCollection matches = regFlags.Matches(page);
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
            matches = regTags.Matches(page);
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
            m = regVrSupportHeadsetsSection.Match(page);
            if (m.Success)
            {
                matches = regVrSupportFlagMatch.Matches(m.Groups[1].Value.Trim());
                vrSupport.Headsets = new List<string>();
                foreach (Match ma in matches)
                {
                    string headset = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
                    if (!string.IsNullOrWhiteSpace(headset))
                    {
                        vrSupport.Headsets.Add(headset);
                    }
                }
            }

            //Get VR Support Input
            m = regVrSupportInputSection.Match(page);
            if (m.Success)
            {
                matches = regVrSupportFlagMatch.Matches(m.Groups[1].Value.Trim());
                vrSupport.Input = new List<string>();
                foreach (Match ma in matches)
                {
                    string input = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        vrSupport.Input.Add(input);
                    }
                }
            }

            //Get VR Support Play Area
            m = regVrSupportPlayAreaSection.Match(page);
            if (m.Success)
            {
                matches = regVrSupportFlagMatch.Matches(m.Groups[1].Value.Trim());
                vrSupport.PlayArea = new List<string>();
                foreach (Match ma in matches)
                {
                    string playArea = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
                    if (!string.IsNullOrWhiteSpace(playArea))
                    {
                        vrSupport.PlayArea.Add(playArea);
                    }
                }
            }

            //Get Language Support
            matches = regLanguageSupport.Matches(page);
            if (matches.Count > 0)
            {
                languageSupport = new LanguageSupport();
                languageSupport.Interface = new List<string>();
                languageSupport.FullAudio = new List<string>();
                languageSupport.Subtitles = new List<string>();

                foreach (Match ma in matches)
                {
                    string language = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
                    if (language.StartsWith("#lang") || language.StartsWith("("))
                    {
                        continue; //Some store pages on steam are bugged.
                    }

                    if (WebUtility.HtmlDecode(ma.Groups[2].Value.Trim()) != "") //Interface
                    {
                        languageSupport.Interface.Add(language);
                    }
                    if (WebUtility.HtmlDecode(ma.Groups[3].Value.Trim()) != "") //Full Audio
                    {
                        languageSupport.FullAudio.Add(language);
                    }
                    if (WebUtility.HtmlDecode(ma.Groups[4].Value.Trim()) != "") //Subtitles
                    {
                        languageSupport.Subtitles.Add(language);
                    }
                }
            }

            //Get Achievement number
            m = regAchievements.Match(page);
            if (m.Success)
            {
                //sometimes games have achievements but don't have the "Steam Achievements" flag in the store
                if (!Flags.Contains("Steam Achievements"))
                {
                    Flags.Add("Steam Achievements");
                }
                int num = 0;
                if (int.TryParse(m.Groups[1].Value, out num))
                {
                    Achievements = num;
                }
            }

            // Get Developer
            m = regDevelopers.Match(page);
            if (m.Success)
            {
                Developers = new List<string>();
                foreach (Capture cap in m.Groups[2].Captures)
                {
                    Developers.Add(WebUtility.HtmlDecode(cap.Value));
                }
            }

            // Get Publishers
            m = regPublishers.Match(page);
            if (m.Success)
            {
                Publishers = new List<string>();
                foreach (Capture cap in m.Groups[2].Captures)
                {
                    Publishers.Add(WebUtility.HtmlDecode(cap.Value));
                }
            }

            // Get release date
            m = regRelDate.Match(page);
            if (m.Success)
            {
                SteamReleaseDate = m.Groups[3].Captures[0].Value;
            }

            // Get user review data
            m = regReviews.Match(page);
            if (m.Success)
            {
                int num = 0;
                if (int.TryParse(m.Groups[1].Value, out num))
                {
                    ReviewPositivePercentage = num;
                }
                if (int.TryParse(m.Groups[2].Value, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out num))
                {
                    ReviewTotal = num;
                }
            }

            // Get metacritic url
            m = regMetalink.Match(page);
            if (m.Success)
            {
                MC_Url = m.Groups[1].Captures[0].Value;
            }

            // Get Platforms
            m = regPlatformWindows.Match(page);
            if (m.Success)
            {
                Platforms |= AppPlatforms.Windows;
            }
            m = regPlatformMac.Match(page);
            if (m.Success)
            {
                Platforms |= AppPlatforms.Mac;
            }
            m = regPlatformLinux.Match(page);
            if (m.Success)
            {
                Platforms |= AppPlatforms.Linux;
            }
        }

        #endregion

        /// <summary>
        /// Merges in data from another entry. Useful for merging scrape results, but could also merge data from a different database.
        /// Uses newer data when there is a conflict.
        /// Does NOT perform deep copies of list fields.
        /// </summary>
        /// <param name="other">GameDBEntry containing info to be merged into this entry.</param>
        public void MergeIn(GameDBEntry other)
        {
            bool useAppInfoFields = (other.LastAppInfoUpdate > LastAppInfoUpdate) ||
                                    ((LastAppInfoUpdate == 0) && (other.LastStoreScrape >= LastStoreScrape));
            bool useScrapeOnlyFields = other.LastStoreScrape >= LastStoreScrape;

            if ((other.AppType != AppTypes.Unknown) && ((AppType == AppTypes.Unknown) || useAppInfoFields))
            {
                AppType = other.AppType;
            }

            if ((other.LastStoreScrape >= LastStoreScrape) ||
                ((LastStoreScrape == 0) && (other.LastAppInfoUpdate > LastAppInfoUpdate)) || (Platforms == AppPlatforms.None))
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
                if (other.Achievements != 0)
                {
                    Achievements = other.Achievements;
                }
                //VR Support
                if ((other.vrSupport.Headsets != null) && (other.vrSupport.Headsets.Count > 0))
                {
                    vrSupport.Headsets = other.vrSupport.Headsets;
                }
                if ((other.vrSupport.Input != null) && (other.vrSupport.Input.Count > 0))
                {
                    vrSupport.Input = other.vrSupport.Input;
                }
                if ((other.vrSupport.PlayArea != null) && (other.vrSupport.PlayArea.Count > 0))
                {
                    vrSupport.PlayArea = other.vrSupport.PlayArea;
                }

                //Language Support
                if ((other.languageSupport.FullAudio != null) && (other.languageSupport.FullAudio.Count > 0))
                {
                    languageSupport.FullAudio = other.languageSupport.FullAudio;
                }
                if ((other.languageSupport.Interface != null) && (other.languageSupport.Interface.Count > 0))
                {
                    languageSupport.Interface = other.languageSupport.Interface;
                }
                if ((other.languageSupport.Subtitles != null) && (other.languageSupport.Subtitles.Count > 0))
                {
                    languageSupport.Subtitles = other.languageSupport.Subtitles;
                }

                if (other.ReviewTotal != 0)
                {
                    ReviewTotal = other.ReviewTotal;
                    ReviewPositivePercentage = other.ReviewPositivePercentage;
                }

                if (!string.IsNullOrEmpty(other.MC_Url))
                {
                    MC_Url = other.MC_Url;
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
    }

    public class GameDB
    {
        // Main Data
        public Dictionary<int, GameDBEntry> Games = new Dictionary<int, GameDBEntry>();

        // Extra data
        private SortedSet<string> allStoreGenres;

        private SortedSet<string> allStoreFlags;
        private SortedSet<string> allStoreDevelopers;
        private SortedSet<string> allStorePublishers;
        private VrSupport allVrSupportFlags;
        private LanguageSupport allLanguages;
        public int LastHltbUpdate;

        public StoreLanguage dbLanguage = StoreLanguage.en;

        // Utility
        static char[] genreSep = {','};

        private const int VERSION = 1;

        private const string
            XmlName_Version = "version",
            XmlName_LastHltbUpdate = "lastHltbUpdate",
            XmlName_dbLanguage = "dbLanguage",
            XmlName_GameList = "gamelist",
            XmlName_Game = "game",
            XmlName_Game_Id = "id",
            XmlName_Game_Name = "name",
            XmlName_Game_LastStoreUpdate = "lastStoreUpdate",
            XmlName_Game_LastAppInfoUpdate = "lastAppInfoUpdate",
            XmlName_Game_Type = "type",
            XmlName_Game_Platforms = "platforms",
            XmlName_Game_Parent = "parent",
            XmlName_Game_Genre = "genre",
            XmlName_Game_Tag = "tag",
            XmlName_Game_Achievements = "achievements",
            XmlName_Game_Developer = "developer",
            XmlName_Game_Publisher = "publisher",
            XmlName_Game_Flag = "flag",
            XmlName_Game_ReviewTotal = "reviewTotal",
            XmlName_Game_ReviewPositivePercent = "reviewPositiveP",
            XmlName_Game_MCUrl = "mcUrl",
            XmlName_Game_Date = "steamDate",
            XmlName_Game_HltbMain = "hltbMain",
            XmlName_Game_HltbExtras = "hltbExtras",
            XmlName_Game_HltbCompletionist = "hltbCompletionist",
            XmlName_Game_vrSupport = "vrSupport",
            XmlName_Game_vrSupport_Headsets = "Headset",
            XmlName_Game_vrSupport_Input = "Input",
            XmlName_Game_vrSupport_PlayArea = "PlayArea",
            XmlName_Game_languageSupport = "languageSupport",
            XmlName_Game_languageSupport_Interface = "Headset",
            XmlName_Game_languageSupport_FullAudio = "Input",
            XmlName_Game_languageSupport_Subtitles = "PlayArea";
            

        #region Accessors

        public bool Contains(int id)
        {
            return Games.ContainsKey(id);
        }

        public bool IncludeItemInGameList(int id, AppTypes scheme)
        {
            if (Games.ContainsKey(id))
            {
                return scheme.HasFlag(Games[id].AppType);
            }
            return scheme.HasFlag(AppTypes.Unknown);
        }

        public string GetName(int id)
        {
            if (Games.ContainsKey(id))
            {
                return Games[id].Name;
            }
            return null;
        }

        public List<string> GetGenreList(int gameId, int depth = 3, bool tagFallback = true)
        {
            if (Games.ContainsKey(gameId))
            {
                List<string> res = Games[gameId].Genres;
                if (tagFallback && ((res == null) || (res.Count == 0)))
                {
                    List<string> tags = GetTagList(gameId, 0);
                    if ((tags != null) && (tags.Count > 0))
                    {
                        res = new List<string>(tags.Intersect(GetAllGenres()));
                    }
                }
                if (((res == null) || (res.Count == 0)) && (depth > 0) && (Games[gameId].ParentId > 0))
                {
                    res = GetGenreList(Games[gameId].ParentId, depth - 1, tagFallback);
                }
                return res;
            }
            return null;
        }

        public List<string> GetFlagList(int gameId, int depth = 3)
        {
            if (Games.ContainsKey(gameId))
            {
                List<string> res = Games[gameId].Flags;
                if (((res == null) || (res.Count == 0)) && (depth > 0) && (Games[gameId].ParentId > 0))
                {
                    res = GetFlagList(Games[gameId].ParentId, depth - 1);
                }
                return res;
            }
            return null;
        }

        public List<string> GetTagList(int gameId, int depth = 3)
        {
            if (Games.ContainsKey(gameId))
            {
                List<string> res = Games[gameId].Tags;
                if (((res == null) || (res.Count == 0)) && (depth > 0) && (Games[gameId].ParentId > 0))
                {
                    res = GetTagList(Games[gameId].ParentId, depth - 1);
                }
                return res;
            }
            return null;
        }

        /// <summary>
        /// Returns whether the game supports VR
        /// </summary>
        public bool SupportsVr(int gameId, int depth = 3)
        {
            if (Games.ContainsKey(gameId))
            {
                VrSupport res = Games[gameId].vrSupport;
                if (((res.Headsets != null) && (res.Headsets.Count > 0)) || ((res.Input != null) && (res.Input.Count > 0)) ||
                    (((res.PlayArea != null) && (res.PlayArea.Count > 0)) && (depth > 0) && (Games[gameId].ParentId > 0)))
                {
                    return true;
                }
                if ((depth > 0) && (Games[gameId].ParentId > 0))
                {
                    return SupportsVr(Games[gameId].ParentId, depth - 1);
                }
            }
            return false;
        }

        public VrSupport GetVrSupport(int gameId, int depth = 3)
        {
            if (Games.ContainsKey(gameId))
            {
                VrSupport res = Games[gameId].vrSupport;
                if (((res.Headsets == null) || (res.Headsets.Count == 0)) && ((res.Input == null) || (res.Input.Count == 0)) &&
                    ((res.PlayArea == null) || (res.PlayArea.Count == 0)) && (depth > 0) && (Games[gameId].ParentId > 0))
                {
                    res = GetVrSupport(Games[gameId].ParentId, depth - 1);
                }
                return res;
            }
            return new VrSupport();
        }


        public List<string> GetDevelopers(int gameId, int depth = 3)
        {
            if (Games.ContainsKey(gameId))
            {
                List<string> res = Games[gameId].Developers;
                if (((res == null) || (res.Count == 0)) && (depth > 0) && (Games[gameId].ParentId > 0))
                {
                    res = GetDevelopers(Games[gameId].ParentId, depth - 1);
                }
                return res;
            }
            return null;
        }

        public List<string> GetPublishers(int gameId, int depth = 3)
        {
            if (Games.ContainsKey(gameId))
            {
                List<string> res = Games[gameId].Publishers;
                if (((res == null) || (res.Count == 0)) && (depth > 0) && (Games[gameId].ParentId > 0))
                {
                    res = GetPublishers(Games[gameId].ParentId, depth - 1);
                }
                return res;
            }
            return null;
        }

        public int GetReleaseYear(int gameId)
        {
            if (Games.ContainsKey(gameId))
            {
                GameDBEntry dbEntry = Games[gameId];
                DateTime releaseDate;
                if (DateTime.TryParse(dbEntry.SteamReleaseDate, out releaseDate))
                {
                    return releaseDate.Year;
                }
            }
            return 0;
        }

        #endregion

        #region Aggregate Accessors

        /// <summary>
        /// Gets a list of all Steam store genres found in the entire database.
        /// Only recalculates if necessary.
        /// </summary>
        /// <returns>A set of genres, as strings</returns>
        public SortedSet<string> GetAllGenres()
        {
            if (allStoreGenres == null)
            {
                return CalculateAllGenres();
            }
            return allStoreGenres;
        }

        /// <summary>
        /// Gets a list of all Steam store genres found in the entire database.
        /// Always recalculates.
        /// </summary>
        /// <returns>A set of genres, as strings</returns>
        public SortedSet<string> CalculateAllGenres()
        {
            if (allStoreGenres == null)
            {
                allStoreGenres = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                allStoreGenres.Clear();
            }

            foreach (GameDBEntry entry in Games.Values)
            {
                if (entry.Genres != null)
                {
                    allStoreGenres.UnionWith(entry.Genres);
                }
            }

            return allStoreGenres;
        }

        /// <summary>
        /// Gets a list of all Steam store developers found in the entire database.
        /// Only recalculates if necessary.
        /// </summary>
        /// <returns>A set of developers, as strings</returns>
        public SortedSet<string> GetAllDevelopers()
        {
            if (allStoreDevelopers == null)
            {
                return CalculateAllDevelopers();
            }
            return allStoreDevelopers;
        }

        /// <summary>
        /// Gets a list of all Steam store developers found in the entire database.
        /// Always recalculates.
        /// </summary>
        /// <returns>A set of developers, as strings</returns>
        public SortedSet<string> CalculateAllDevelopers()
        {
            if (allStoreDevelopers == null)
            {
                allStoreDevelopers = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                allStoreDevelopers.Clear();
            }

            foreach (GameDBEntry entry in Games.Values)
            {
                if (entry.Developers != null)
                {
                    allStoreDevelopers.UnionWith(entry.Developers);
                }
            }

            return allStoreDevelopers;
        }

        /// <summary>
        /// Gets a list of all Steam store publishers found in the entire database.
        /// Only recalculates if necessary.
        /// </summary>
        /// <returns>A set of publishers, as strings</returns>
        public SortedSet<string> GetAllPublishers()
        {
            if (allStorePublishers == null)
            {
                return CalculateAllPublishers();
            }
            return allStorePublishers;
        }

        /// <summary>
        /// Gets a list of all Steam store publishers found in the entire database.
        /// Always recalculates.
        /// </summary>
        /// <returns>A set of publishers, as strings</returns>
        public SortedSet<string> CalculateAllPublishers()
        {
            if (allStorePublishers == null)
            {
                allStorePublishers = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                allStorePublishers.Clear();
            }

            foreach (GameDBEntry entry in Games.Values)
            {
                if (entry.Publishers != null)
                {
                    allStorePublishers.UnionWith(entry.Publishers);
                }
            }

            return allStorePublishers;
        }

        /// <summary>
        /// Gets a list of all Steam store flags found in the entire database.
        /// Only recalculates if necessary.
        /// </summary>
        /// <returns>A set of genres, as strings</returns>
        public SortedSet<string> GetAllStoreFlags()
        {
            if (allStoreFlags == null)
            {
                return CalculateAllStoreFlags();
            }
            return allStoreFlags;
        }

        /// <summary>
        /// Gets a list of all Steam store flags found in the entire database.
        /// Always recalculates.
        /// </summary>
        /// <returns>A set of genres, as strings</returns>
        public SortedSet<string> CalculateAllStoreFlags()
        {
            if (allStoreFlags == null)
            {
                allStoreFlags = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                allStoreFlags.Clear();
            }

            foreach (GameDBEntry entry in Games.Values)
            {
                if (entry.Flags != null)
                {
                    allStoreFlags.UnionWith(entry.Flags);
                }
            }
            return allStoreFlags;
        }

        /// <summary>
        /// Gets a list of all Steam store VR Support flags found in the entire database.
        /// Only recalculates if necessary.
        /// </summary>
        /// <returns>A VrSupport struct containing the flags</returns>
        public VrSupport GetAllVrSupportFlags()
        {
            if ((allVrSupportFlags.Headsets == null) || (allVrSupportFlags.Input == null) ||
                (allVrSupportFlags.PlayArea == null))
            {
                return CalculateAllVrSupportFlags();
            }
            return allVrSupportFlags;
        }

        /// <summary>
        /// Gets a list of all Steam store VR Support flags found in the entire database.
        /// Always recalculates.
        /// </summary>
        /// <returns>A VrSupport struct containing the flags</returns>
        public VrSupport CalculateAllVrSupportFlags()
        {
            SortedSet<string> headsets = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> input = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> playArea = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (GameDBEntry entry in Games.Values)
            {
                if (entry.vrSupport.Headsets != null)
                {
                    headsets.UnionWith(entry.vrSupport.Headsets);
                }
                if (entry.vrSupport.Input != null)
                {
                    input.UnionWith(entry.vrSupport.Input);
                }
                if (entry.vrSupport.PlayArea != null)
                {
                    playArea.UnionWith(entry.vrSupport.PlayArea);
                }
            }
            allVrSupportFlags.Headsets = headsets.ToList();
            allVrSupportFlags.Input = input.ToList();
            allVrSupportFlags.PlayArea = playArea.ToList();
            return allVrSupportFlags;
        }

        /// <summary>
        /// Gets a list of all Game Languages found in the entire database.
        /// Only recalculates if necessary.
        /// </summary>
        /// <returns>A LanguageSupport struct containing the languages</returns>
        public LanguageSupport GetAllLanguages()
        {
            if ((allLanguages.FullAudio == null) || (allLanguages.Interface == null) ||
                (allLanguages.Subtitles == null))
            {
                return CalculateAllLanguages();
            }
            return allLanguages;
        }

        /// <summary>
        /// Gets a list of all Game Languages found in the entire database.
        /// Always recalculates.
        /// </summary>
        /// <returns>A LanguageSupport struct containing the languages</returns>
        public LanguageSupport CalculateAllLanguages()
        {
            SortedSet<string> Interface = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> Subtitles = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            SortedSet<string> FullAudio = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (GameDBEntry entry in Games.Values)
            {
                if (entry.languageSupport.Interface != null)
                {
                    Interface.UnionWith(entry.languageSupport.Interface);
                }
                if (entry.languageSupport.Subtitles != null)
                {
                    Subtitles.UnionWith(entry.languageSupport.Subtitles);
                }
                if (entry.languageSupport.FullAudio != null)
                {
                    FullAudio.UnionWith(entry.languageSupport.FullAudio);
                }
            }
            allLanguages.Interface = Interface.ToList();
            allLanguages.Subtitles = Subtitles.ToList();
            allLanguages.FullAudio = FullAudio.ToList();
            return allLanguages;
        }

        /// <summary>
        /// Gets a list of developers found on games with their game count.
        /// </summary>
        /// <param name="filter">GameList including games to include in the search. If null, finds developers for all games in the database.</param>
        /// <param name="minScore">Minimum count of developers games to include in the result list. Developers with lower game counts will be discarded.</param>
        /// <returns>List of developers, as strings with game counts</returns>
        public IEnumerable<Tuple<string, int>> CalculateSortedDevList(GameList filter, int minCount)
        {
            SortedSet<string> developers = GetAllDevelopers();
            Dictionary<string, int> devCounts = new Dictionary<string, int>();
            if (filter == null)
            {
                foreach (GameDBEntry dbEntry in Games.Values)
                {
                    CalculateSortedDevListHelper(devCounts, dbEntry);
                }
            }
            else
            {
                foreach (int gameId in filter.Games.Keys)
                {
                    if (Games.ContainsKey(gameId) && !filter.Games[gameId].Hidden)
                    {
                        CalculateSortedDevListHelper(devCounts, Games[gameId]);
                    }
                }
            }

            var unsortedList = (from entry in devCounts
                where entry.Value >= minCount
                select new Tuple<string, int>(entry.Key, entry.Value));
            return unsortedList.ToList();
        }

        /// <summary>
        /// Counts games for each developer.
        /// </summary>
        /// <param name="counts">Existing dictionary of developers and game count. Key is the developer as a string, value is the count</param>
        /// <param name="dbEntry">Entry to add developers from</param>
        private void CalculateSortedDevListHelper(Dictionary<string, int> counts, GameDBEntry dbEntry)
        {
            if (dbEntry.Developers != null)
            {
                for (int i = 0; i < dbEntry.Developers.Count; i++)
                {
                    string dev = dbEntry.Developers[i];
                    if (counts.ContainsKey(dev))
                    {
                        counts[dev] += 1;
                    }
                    else
                    {
                        counts[dev] = 1;
                    }
                }
            }
        }

        /// <summary>
        /// Gets a list of publishers found on games with their game count.
        /// </summary>
        /// <param name="filter">GameList including games to include in the search. If null, finds publishers for all games in the database.</param>
        /// <param name="minScore">Minimum count of publishers games to include in the result list. publishers with lower game counts will be discarded.</param>
        /// <returns>List of publishers, as strings with game counts</returns>
        public IEnumerable<Tuple<string, int>> CalculateSortedPubList(GameList filter, int minCount)
        {
            SortedSet<string> publishers = GetAllPublishers();
            Dictionary<string, int> PubCounts = new Dictionary<string, int>();
            if (filter == null)
            {
                foreach (GameDBEntry dbEntry in Games.Values)
                {
                    CalculateSortedPubListHelper(PubCounts, dbEntry);
                }
            }
            else
            {
                foreach (int gameId in filter.Games.Keys)
                {
                    if (Games.ContainsKey(gameId) && !filter.Games[gameId].Hidden)
                    {
                        CalculateSortedPubListHelper(PubCounts, Games[gameId]);
                    }
                }
            }

            var unsortedList = (from entry in PubCounts
                where entry.Value >= minCount
                select new Tuple<string, int>(entry.Key, entry.Value));
            return unsortedList.ToList();
        }

        /// <summary>
        /// Counts games for each publisher.
        /// </summary>
        /// <param name="counts">Existing dictionary of publishers and game count. Key is the publisher as a string, value is the count</param>
        /// <param name="dbEntry">Entry to add publishers from</param>
        private void CalculateSortedPubListHelper(Dictionary<string, int> counts, GameDBEntry dbEntry)
        {
            if (dbEntry.Publishers != null)
            {
                for (int i = 0; i < dbEntry.Publishers.Count; i++)
                {
                    string Pub = dbEntry.Publishers[i];
                    if (counts.ContainsKey(Pub))
                    {
                        counts[Pub] += 1;
                    }
                    else
                    {
                        counts[Pub] = 1;
                    }
                }
            }
        }

        /// <summary>
        /// Gets a list of tags found on games, sorted by a popularity score.
        /// </summary>
        /// <param name="filter">GameList including games to include in the search. If null, finds tags for all games in the database.</param>
        /// <param name="weightFactor">Value of the popularity score contributed by the first processed tag for each game. Each subsequent tag contributes less to its own score.
        /// The last tag always contributes 1. Value less than or equal to 1 indicates no weighting.</param>
        /// <param name="minScore">Minimum score of tags to include in the result list. Tags with lower scores will be discarded.</param>
        /// <param name="tagsPerGame">Maximum tags to find per game. If a game has more tags than this, they will be discarded. 0 indicates no limit.</param>
        /// <returns>List of tags, as strings</returns>
        public IEnumerable<Tuple<string, float>> CalculateSortedTagList(GameList filter, float weightFactor,
            int minScore, int tagsPerGame, bool excludeGenres, bool scoreSort)
        {
            SortedSet<string> genreNames = GetAllGenres();
            Dictionary<string, float> tagCounts = new Dictionary<string, float>();
            if (filter == null)
            {
                foreach (GameDBEntry dbEntry in Games.Values)
                {
                    CalculateSortedTagListHelper(tagCounts, dbEntry, weightFactor, tagsPerGame);
                }
            }
            else
            {
                foreach (int gameId in filter.Games.Keys)
                {
                    if (Games.ContainsKey(gameId) && !filter.Games[gameId].Hidden)
                    {
                        CalculateSortedTagListHelper(tagCounts, Games[gameId], weightFactor, tagsPerGame);
                    }
                }
            }

            if (excludeGenres)
            {
                foreach (string genre in genreNames)
                {
                    tagCounts.Remove(genre);
                }
            }

            var unsortedList = (from entry in tagCounts
                where entry.Value >= minScore
                select new Tuple<string, float>(entry.Key, entry.Value));
            var sortedList = scoreSort
                ? from entry in unsortedList orderby entry.Item2 descending select entry
                : from entry in unsortedList orderby entry.Item1 select entry;
            return sortedList.ToList();
        }

        /// <summary>
        /// Adds tags from the given DBEntry to the dictionary. Adds new elements if necessary, and increases values on existing elements.
        /// </summary>
        /// <param name="counts">Existing dictionary of tags and scores. Key is the tag as a string, value is the score</param>
        /// <param name="dbEntry">Entry to add tags from</param>
        /// <param name="weightFactor">The score value of the first tag in the list.
        /// The first tag on the game will have this score, and the last tag processed will always have score 1.
        /// The tags between will have linearly interpolated values between them.</param>
        /// <param name="tagsPerGame"></param>
        private void CalculateSortedTagListHelper(Dictionary<string, float> counts, GameDBEntry dbEntry,
            float weightFactor, int tagsPerGame)
        {
            if (dbEntry.Tags != null)
            {
                int tagsToLoad = (tagsPerGame == 0) ? dbEntry.Tags.Count : Math.Min(tagsPerGame, dbEntry.Tags.Count);
                for (int i = 0; i < tagsToLoad; i++)
                {
                    // Get the score based on the weighting factor
                    float score = 1;
                    if (weightFactor > 1)
                    {
                        if (tagsToLoad <= 1)
                        {
                            score = weightFactor;
                        }
                        else
                        {
                            float interp = i / (float) (tagsToLoad - 1);
                            score = ((1 - interp) * weightFactor) + interp;
                        }
                    }

                    string tag = dbEntry.Tags[i];
                    if (counts.ContainsKey(tag))
                    {
                        counts[tag] += score;
                    }
                    else
                    {
                        counts[tag] = score;
                    }
                }
            }
        }

        private void ClearAggregates()
        {
            allStoreGenres = null;
            allStoreFlags = null;
            allStoreDevelopers = null;
            allStorePublishers = null;
        }

        #endregion

        #region Operations

        public void UpdateAppListFromWeb()
        {
            XmlDocument doc = FetchAppListFromWeb();
            IntegrateAppList(doc);
        }

        public static XmlDocument FetchAppListFromWeb()
        {
            XmlDocument doc = new XmlDocument();
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_DownloadingSteamAppList);
            WebRequest req = WebRequest.Create(@"http://api.steampowered.com/ISteamApps/GetAppList/v0002/?format=xml");
            using (WebResponse resp = req.GetResponse())
            {
                doc.Load(resp.GetResponseStream());
            }
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_XMLAppListDownloaded);
            return doc;
        }

        public int IntegrateAppList(XmlDocument doc)
        {
            int added = 0;
            foreach (XmlNode node in doc.SelectNodes("/applist/apps/app"))
            {
                int appId;
                if (XmlUtil.TryGetIntFromNode(node["appid"], out appId))
                {
                    string gameName = XmlUtil.GetStringFromNode(node["name"], null);
                    if (Games.ContainsKey(appId))
                    {
                        GameDBEntry g = Games[appId];
                        if (string.IsNullOrEmpty(g.Name) || (g.Name != gameName))
                        {
                            g.Name = gameName;
                            g.AppType = AppTypes.Unknown;
                        }
                    }
                    else
                    {
                        GameDBEntry g = new GameDBEntry();
                        g.Id = appId;
                        g.Name = gameName;
                        Games.Add(appId, g);
                        added++;
                    }
                }
            }
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_LoadedNewItemsFromAppList, added);
            return added;
        }

        /// <summary>
        /// Updated the database with information from the AppInfo cache file.
        /// </summary>
        /// <param name="path">Path to the cache file</param>
        /// <returns>The number of entries integrated into the database.</returns>
        public int UpdateFromAppInfo(string path)
        {
            int updated = 0;

            Dictionary<int, AppInfo> appInfos = LoadApps(path);
            int timestamp = Utility.GetCurrentUTime();

            foreach (AppInfo aInf in appInfos.Values)
            {
                GameDBEntry entry;
                if (!Games.ContainsKey(aInf.Id))
                {
                    entry = new GameDBEntry();
                    entry.Id = aInf.Id;
                    Games.Add(entry.Id, entry);
                }
                else
                {
                    entry = Games[aInf.Id];
                }

                entry.LastAppInfoUpdate = timestamp;
                if (aInf.AppType != AppTypes.Unknown)
                {
                    entry.AppType = aInf.AppType;
                }
                if (!string.IsNullOrEmpty(aInf.Name))
                {
                    entry.Name = aInf.Name;
                }
                if ((entry.Platforms == AppPlatforms.None) ||
                    ((entry.LastStoreScrape == 0) && (aInf.Platforms > AppPlatforms.None)))
                {
                    entry.Platforms = aInf.Platforms;
                }
                if (aInf.Parent > 0)
                {
                    entry.ParentId = aInf.Parent;
                }
                updated++;
            }
            return updated;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Dictionary<int, AppInfo> LoadApps(string path)
        {
            Dictionary<int, AppInfo> result = new Dictionary<int, AppInfo>();
            BinaryReader bReader = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read));
            long fileLength = bReader.BaseStream.Length;

            // seek to common: start of a new entry
            byte[] start = { 0x00, 0x00, 0x63, 0x6F, 0x6D, 0x6D, 0x6F, 0x6E, 0x00 }; // 0x00 0x00 c o m m o n 0x00

            VdfFileNode.ReadBin_SeekTo(bReader, start, fileLength);

            VdfFileNode node = VdfFileNode.LoadFromBinary(bReader, fileLength);
            while (node != null)
            {
                AppInfo app = AppInfo.Create(node);
                if (app != null)
                {
                    result.Add(app.Id, app);
                }
                VdfFileNode.ReadBin_SeekTo(bReader, start, fileLength);
                node = VdfFileNode.LoadFromBinary(bReader, fileLength);
            }
            bReader.Close();
            return result;
        }

        /// <summary>
        /// Update the database with information from howlongtobeatsteam.com.
        /// </summary>
        /// <param name="includeImputedTimes">Whether to include imputed hltb times</param>
        /// <returns>The number of entries integrated into the database.</returns>
        public int UpdateFromHltb(bool includeImputedTimes)
        {
            int updated = 0;

            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                string json = wc.DownloadString(Resources.UrlHLTBAll);
                JObject parsedJson = JObject.Parse(json);
                dynamic games = parsedJson.SelectToken("Games");
                foreach (dynamic g in games)
                {
                    dynamic steamAppData = g.SteamAppData;
                    int id = steamAppData.SteamAppId;
                    if (Games.ContainsKey(id))
                    {
                        dynamic htlbInfo = steamAppData.HltbInfo;

                        if (!includeImputedTimes && (htlbInfo.MainTtbImputed == "True"))
                        {
                            Games[id].HltbMain = 0;
                        }
                        else
                        {
                            Games[id].HltbMain = htlbInfo.MainTtb;
                        }

                        if (!includeImputedTimes && (htlbInfo.ExtrasTtbImputed == "True"))
                        {
                            Games[id].HltbExtras = 0;
                        }
                        else
                        {
                            Games[id].HltbExtras = htlbInfo.ExtrasTtb;
                        }

                        if (!includeImputedTimes && (htlbInfo.CompletionistTtbImputed == "True"))
                        {
                            Games[id].HltbCompletionist = 0;
                        }
                        else
                        {
                            Games[id].HltbCompletionist = htlbInfo.CompletionistTtb;
                        }

                        updated++;
                    }
                }
            }
            LastHltbUpdate = Utility.GetCurrentUTime();
            return updated;
        }

        #endregion

        #region Serialization

        public void Save(string path)
        {
            Save(path, path.EndsWith(".gz"));
        }

        public void Save(string path, bool compress)
        {
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_SavingGameDBTo, path);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.CloseOutput = true;

            Stream stream = null;
            try
            {
                stream = new FileStream(path, FileMode.Create);

                if (compress)
                {
                    stream = new GZipStream(stream, CompressionMode.Compress);
                }

                XmlWriter writer = XmlWriter.Create(stream, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement(XmlName_GameList);

                writer.WriteElementString(XmlName_Version, VERSION.ToString());

                writer.WriteElementString(XmlName_LastHltbUpdate, LastHltbUpdate.ToString());

                writer.WriteElementString(XmlName_dbLanguage, Enum.GetName(typeof(StoreLanguage), dbLanguage));

                foreach (GameDBEntry g in Games.Values)
                {
                    writer.WriteStartElement(XmlName_Game);

                    writer.WriteElementString(XmlName_Game_Id, g.Id.ToString());

                    if (!string.IsNullOrEmpty(g.Name))
                    {
                        writer.WriteElementString(XmlName_Game_Name, g.Name);
                    }

                    if (g.LastStoreScrape > 0)
                    {
                        writer.WriteElementString(XmlName_Game_LastStoreUpdate, g.LastStoreScrape.ToString());
                    }
                    if (g.LastAppInfoUpdate > 0)
                    {
                        writer.WriteElementString(XmlName_Game_LastAppInfoUpdate, g.LastAppInfoUpdate.ToString());
                    }

                    writer.WriteElementString(XmlName_Game_Type, g.AppType.ToString());

                    writer.WriteElementString(XmlName_Game_Platforms, g.Platforms.ToString());

                    if (g.ParentId >= 0)
                    {
                        writer.WriteElementString(XmlName_Game_Parent, g.ParentId.ToString());
                    }

                    if (g.Genres != null)
                    {
                        foreach (string str in g.Genres)
                        {
                            writer.WriteElementString(XmlName_Game_Genre, str);
                        }
                    }

                    if (g.Tags != null)
                    {
                        foreach (string str in g.Tags)
                        {
                            writer.WriteElementString(XmlName_Game_Tag, str);
                        }
                    }

                    if (g.Developers != null)
                    {
                        foreach (string str in g.Developers)
                        {
                            writer.WriteElementString(XmlName_Game_Developer, str);
                        }
                    }

                    if (g.Publishers != null)
                    {
                        foreach (string str in g.Publishers)
                        {
                            writer.WriteElementString(XmlName_Game_Publisher, str);
                        }
                    }

                    if (g.Flags != null)
                    {
                        foreach (string s in g.Flags)
                        {
                            writer.WriteElementString(XmlName_Game_Flag, s);
                        }
                    }

                    //vr support
                    writer.WriteStartElement(XmlName_Game_vrSupport);
                    if (g.vrSupport.Headsets != null)
                    {
                        foreach (string str in g.vrSupport.Headsets)
                        {
                            writer.WriteElementString(XmlName_Game_vrSupport_Headsets, str);
                        }
                    }

                    if (g.vrSupport.Input != null)
                    {
                        foreach (string str in g.vrSupport.Input)
                        {
                            writer.WriteElementString(XmlName_Game_vrSupport_Input, str);
                        }
                    }

                    if (g.vrSupport.PlayArea != null)
                    {
                        foreach (string str in g.vrSupport.PlayArea)
                        {
                            writer.WriteElementString(XmlName_Game_vrSupport_PlayArea, str);
                        }
                    }

                    writer.WriteEndElement();

                    //language support
                    writer.WriteStartElement(XmlName_Game_languageSupport);
                    if (g.languageSupport.Interface != null)
                    {
                        foreach (string str in g.languageSupport.Interface)
                        {
                            writer.WriteElementString(XmlName_Game_languageSupport_Interface, str);
                        }
                    }

                    if (g.languageSupport.FullAudio != null)
                    {
                        foreach (string str in g.languageSupport.FullAudio)
                        {
                            writer.WriteElementString(XmlName_Game_languageSupport_FullAudio, str);
                        }
                    }

                    if (g.languageSupport.Subtitles != null)
                    {
                        foreach (string str in g.languageSupport.Subtitles)
                        {
                            writer.WriteElementString(XmlName_Game_languageSupport_Subtitles, str);
                        }
                    }

                    writer.WriteEndElement();

                    if (g.Achievements > 0)
                    {
                        writer.WriteElementString(XmlName_Game_Achievements, g.Achievements.ToString());
                    }

                    if (g.ReviewTotal > 0)
                    {
                        writer.WriteElementString(XmlName_Game_ReviewTotal, g.ReviewTotal.ToString());
                        writer.WriteElementString(XmlName_Game_ReviewPositivePercent,
                            g.ReviewPositivePercentage.ToString());
                    }

                    if (!string.IsNullOrEmpty(g.MC_Url))
                    {
                        writer.WriteElementString(XmlName_Game_MCUrl, g.MC_Url);
                    }

                    if (!string.IsNullOrEmpty(g.SteamReleaseDate))
                    {
                        writer.WriteElementString(XmlName_Game_Date, g.SteamReleaseDate);
                    }

                    if (g.HltbMain > 0)
                    {
                        writer.WriteElementString(XmlName_Game_HltbMain, g.HltbMain.ToString());
                    }

                    if (g.HltbExtras > 0)
                    {
                        writer.WriteElementString(XmlName_Game_HltbExtras, g.HltbExtras.ToString());
                    }

                    if (g.HltbCompletionist > 0)
                    {
                        writer.WriteElementString(XmlName_Game_HltbCompletionist, g.HltbCompletionist.ToString());
                    }

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_GameDBSaved);
        }

        public void Load(string path)
        {
            Load(path, path.EndsWith(".gz"));
        }

        public void Load(string path, bool compress)
        {
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_LoadingGameDBFrom, path);
            XmlDocument doc = new XmlDocument();

            Stream stream = null;
            try
            {
                stream = new FileStream(path, FileMode.Open);
                if (compress)
                {
                    stream = new GZipStream(stream, CompressionMode.Decompress);
                }

                doc.Load(stream);

                Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_GameDBXMLParsed);
                Games.Clear();
                ClearAggregates();

                XmlNode gameListNode = doc.SelectSingleNode("/" + XmlName_GameList);

                int fileVersion = XmlUtil.GetIntFromNode(gameListNode[XmlName_Version], 0);

                LastHltbUpdate = XmlUtil.GetIntFromNode(gameListNode[XmlName_LastHltbUpdate], 0);

                dbLanguage = (StoreLanguage) Enum.Parse(typeof(StoreLanguage),
                    XmlUtil.GetStringFromNode(gameListNode[XmlName_dbLanguage], "en"), true);

                foreach (XmlNode gameNode in gameListNode.SelectNodes(XmlName_Game))
                {
                    int id;
                    if (!XmlUtil.TryGetIntFromNode(gameNode[XmlName_Game_Id], out id) || Games.ContainsKey(id))
                    {
                        continue;
                    }
                    GameDBEntry g = new GameDBEntry();
                    g.Id = id;

                    g.Name = XmlUtil.GetStringFromNode(gameNode[XmlName_Game_Name], null);

                    if (fileVersion < 1)
                    {
                        g.AppType = AppTypes.Unknown;
                        string typeString;
                        if (XmlUtil.TryGetStringFromNode(gameNode[XmlName_Game_Type], out typeString))
                        {
                            if (typeString == "DLC")
                            {
                                g.AppType = AppTypes.DLC;
                            }
                            else if (typeString == "Game")
                            {
                                g.AppType = AppTypes.Game;
                            }
                            else if (typeString == "NonApp")
                            {
                                g.AppType = AppTypes.Other;
                            }
                        }
                    }
                    else
                    {
                        g.AppType = XmlUtil.GetEnumFromNode(gameNode[XmlName_Game_Type], AppTypes.Unknown);
                    }

                    g.Platforms = XmlUtil.GetEnumFromNode(gameNode[XmlName_Game_Platforms], AppPlatforms.All);

                    g.ParentId = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_Parent], -1);

                    if (fileVersion < 1)
                    {
                        List<string> genreList = new List<string>();
                        string genreString = XmlUtil.GetStringFromNode(gameNode["genre"], null);
                        if (genreString != null)
                        {
                            string[] genStrList = genreString.Split(',');
                            foreach (string s in genStrList)
                            {
                                genreList.Add(s.Trim());
                            }
                        }
                        g.Genres = genreList;
                    }
                    else
                    {
                        g.Genres = XmlUtil.GetStringsFromNodeList(gameNode.SelectNodes(XmlName_Game_Genre));
                    }

                    g.Tags = XmlUtil.GetStringsFromNodeList(gameNode.SelectNodes(XmlName_Game_Tag));

                    foreach (XmlNode vrNode in gameNode.SelectNodes(XmlName_Game_vrSupport))
                    {
                        g.vrSupport.Headsets =
                            XmlUtil.GetStringsFromNodeList(vrNode.SelectNodes(XmlName_Game_vrSupport_Headsets));
                        g.vrSupport.Input =
                            XmlUtil.GetStringsFromNodeList(vrNode.SelectNodes(XmlName_Game_vrSupport_Input));
                        g.vrSupport.PlayArea =
                            XmlUtil.GetStringsFromNodeList(vrNode.SelectNodes(XmlName_Game_vrSupport_PlayArea));
                    }

                    foreach (XmlNode langNode in gameNode.SelectNodes(XmlName_Game_languageSupport))
                    {
                        g.languageSupport.Interface =
                            XmlUtil.GetStringsFromNodeList(langNode.SelectNodes(XmlName_Game_languageSupport_Interface));
                        g.languageSupport.FullAudio =
                            XmlUtil.GetStringsFromNodeList(langNode.SelectNodes(XmlName_Game_languageSupport_FullAudio));
                        g.languageSupport.Subtitles =
                            XmlUtil.GetStringsFromNodeList(langNode.SelectNodes(XmlName_Game_languageSupport_Subtitles));
                    }

                    g.Developers = XmlUtil.GetStringsFromNodeList(gameNode.SelectNodes(XmlName_Game_Developer));

                    if (fileVersion < 1)
                    {
                        List<string> pubList = new List<string>();
                        string pubString = XmlUtil.GetStringFromNode(gameNode["publisher"], null);
                        if (pubString != null)
                        {
                            string[] pubStrList = pubString.Split(',');
                            foreach (string s in pubStrList)
                            {
                                pubList.Add(s.Trim());
                            }
                        }
                        g.Publishers = pubList;
                    }
                    else
                    {
                        g.Publishers = XmlUtil.GetStringsFromNodeList(gameNode.SelectNodes(XmlName_Game_Publisher));
                    }

                    if (fileVersion < 1)
                    {
                        int steamDate = XmlUtil.GetIntFromNode(gameNode["steamDate"], 0);
                        g.SteamReleaseDate = steamDate > 0 ? DateTime.FromOADate(steamDate).ToString("MMM d, yyyy") : null;
                    }
                    else
                    {
                        g.SteamReleaseDate = XmlUtil.GetStringFromNode(gameNode[XmlName_Game_Date], null);
                    }

                    g.Flags = XmlUtil.GetStringsFromNodeList(gameNode.SelectNodes(XmlName_Game_Flag));

                    g.Achievements = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_Achievements], 0);

                    g.ReviewTotal = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_ReviewTotal], 0);
                    g.ReviewPositivePercentage =
                        XmlUtil.GetIntFromNode(gameNode[XmlName_Game_ReviewPositivePercent], 0);

                    g.MC_Url = XmlUtil.GetStringFromNode(gameNode[XmlName_Game_MCUrl], null);

                    g.LastAppInfoUpdate = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_LastAppInfoUpdate], 0);
                    g.LastStoreScrape = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_LastStoreUpdate], 0);

                    g.HltbMain = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_HltbMain], 0);
                    g.HltbExtras = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_HltbExtras], 0);
                    g.HltbCompletionist = XmlUtil.GetIntFromNode(gameNode[XmlName_Game_HltbCompletionist], 0);

                    Games.Add(id, g);
                }
                Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_GameDBXMLProcessed);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        #endregion
    }

    public class GameDBEntrySorter : IComparer<GameDBEntry>
    {
        public enum SortModes
        {
            Id,
            Name,
            Genre,
            Type,
            IsScraped,
            HasAppInfo,
            Parent
        }

        public SortModes SortMode = SortModes.Id;
        public int SortDirection = 1;

        public void SetSortMode(SortModes mode, int forceDir = 0)
        {
            if (mode == SortMode)
            {
                SortDirection = (forceDir == 0) ? (SortDirection *= -1) : forceDir;
            }
            else
            {
                SortMode = mode;
                SortDirection = (forceDir == 0) ? 1 : forceDir;
            }
        }

        public int Compare(GameDBEntry a, GameDBEntry b)
        {
            int res = 0;
            switch (SortMode)
            {
                case SortModes.Id:
                    res = a.Id - b.Id;
                    break;
                case SortModes.Name:
                    res = string.Compare(a.Name, b.Name);
                    break;
                case SortModes.Genre:
                    res = Utility.CompareLists(a.Genres, b.Genres);
                    break;
                case SortModes.Type:
                    res = a.AppType - b.AppType;
                    break;
                case SortModes.IsScraped:
                    res = ((a.LastStoreScrape > 0) ? 1 : 0) - ((b.LastStoreScrape > 0) ? 1 : 0);
                    break;
                case SortModes.HasAppInfo:
                    res = ((a.LastAppInfoUpdate > 0) ? 1 : 0) - ((b.LastAppInfoUpdate > 0) ? 1 : 0);
                    break;
                case SortModes.Parent:
                    res = a.ParentId - b.ParentId;
                    break;
            }
            return SortDirection * res;
        }
    }
}