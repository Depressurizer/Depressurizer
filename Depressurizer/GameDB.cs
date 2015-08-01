/*
This file is part of Depressurizer.
Copyright 2011, 2012, 2013 Steve Labbe.

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
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using Rallion;
using System.IO.Compression;
using System.Globalization;
using System.Linq;

namespace Depressurizer {

    public class GameDBEntry {

        #region Fields
        public int Id;
        public string Name;
        public AppTypes AppType = AppTypes.Unknown;
        public int ParentId = -1;
        public AppPlatforms Platforms = AppPlatforms.All;

        // Basics:
        public List<string> Genres = new List<string>();
        public List<string> Flags = new List<string>();
        public List<string> Tags = new List<string>();
        public List<string> Developers = null;
        public List<string> Publishers = null;
        public string SteamReleaseDate = null;

        public int ReviewTotal = 0;
        public int ReviewPositivePercentage = 0;

        // Metacritic:
        public string MC_Url = null;

        public int LastStoreScrape = 0;
        public int LastAppInfoUpdate = 0;
        #endregion

        #region Regex
        // If this regex maches a store page, the app is a game
        private static Regex regGamecheck = new Regex( "<a[^>]*>All Games</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled );

        private static Regex regGenre = new Regex( "<div class=\\\"details_block\\\">\\s*<b>Title:</b>[^<]*<br>\\s*<b>Genre:</b>\\s*(<a[^>]*>([^<]+)</a>,?\\s*)+\\s*<br>", RegexOptions.Compiled | RegexOptions.IgnoreCase );
        private static Regex regFlags = new Regex("<a class=\\\"name\\\" href=\\\"http://store.steampowered.com/search/\\?category2=.*?\">([^<]*)</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static Regex regTags = new Regex( "<a[^>]*class=\\\"app_tag\\\"[^>]*>([^<]*)</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled );

        private static Regex regDevelopers = new Regex( "<b>Developer:</b>\\s*(<a[^>]*>([^<]+)</a>,?\\s*)+\\s*<br>", RegexOptions.IgnoreCase | RegexOptions.Compiled );
        private static Regex regPublishers = new Regex( "<b>Publisher:</b>\\s*(<a[^>]*>([^<]+)</a>,?\\s*)+\\s*<br>", RegexOptions.IgnoreCase | RegexOptions.Compiled );

        private static Regex regRelDate = new Regex( "<b>Release Date:</b>\\s*([^<]*)<br>", RegexOptions.IgnoreCase | RegexOptions.Compiled );
        private static Regex regMetalink = new Regex( "<div id=\\\"game_area_metalink\\\">\\s*<a href=\\\"http://www.metacritic.com/game/pc/([^\\\"]*)", RegexOptions.IgnoreCase | RegexOptions.Compiled );

        private static Regex regReviews = new Regex( @"data-store-tooltip=""([\d]+)% of the ([\d,]+) user reviews for this game are positive.""", RegexOptions.IgnoreCase | RegexOptions.Compiled );
        #endregion

        #region Scraping
        /// <summary>
        /// Scrapes the store page with this game entry's ID and updates this entry with the information found.
        /// </summary>
        /// <returns>The type determined during the scrape</returns>
        public AppTypes ScrapeStore() {
            AppTypes result = ScrapeStoreHelper( this.Id );
            SetTypeFromStoreScrape( result );
            return result;
        }

        /// <summary>
        /// Private helper function to perform scraping work. Downloads the given store page and updates the entry with all information found.
        /// </summary>
        /// <param name="id">The id of the store page to scrape</param>
        /// <returns>The type determined during the scrape</returns>
        private AppTypes ScrapeStoreHelper( int id ) {
            Program.Logger.Write( LoggerLevel.Verbose, GlobalStrings.GameDB_InitiatingStoreScrapeForGame, id );

            string page = "";

            int redirectTarget = -1;

            int oldTime = LastStoreScrape;

            try {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create( string.Format( Properties.Resources.UrlSteamStore, id ) );

                // Cookie bypasses the age gate
                req.CookieContainer = new CookieContainer( 1 );
                req.CookieContainer.Add( new Cookie( "birthtime", "-2208959999", "/", "store.steampowered.com" ) );

                using( WebResponse resp = req.GetResponse() ) {
                    LastStoreScrape = Utility.GetCurrentUTime();
                    if( resp.ResponseUri.Segments.Length < 2 ) { // If we were redirected to the store front page
                        Program.Logger.Write( LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingRedirectedToMainStorePage, id );
                        SetTypeFromStoreScrape( AppTypes.Unknown );
                        return AppTypes.Unknown;

                    } else if( resp.ResponseUri.Segments[1] == "agecheck/" ) { // If we encountered an age gate (cookies should bypass this, but sometimes they don't seem to)
                        if( resp.ResponseUri.Segments.Length >= 4 && resp.ResponseUri.Segments[3].TrimEnd( '/' ) != id.ToString() ) { // Age check + redirect
                            Program.Logger.Write( LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingHitAgeCheck, id, resp.ResponseUri.Segments[3].TrimEnd( '/' ) );
                            if( int.TryParse( resp.ResponseUri.Segments[3].TrimEnd( '/' ), out redirectTarget ) ) {
                            } else { // If we got an age check without numeric id (shouldn't happen)
                                return AppTypes.Unknown;
                            }
                        } else { // If we got an age check with no redirect
                            Program.Logger.Write( LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingAgeCheckNoRedirect, id );
                            return AppTypes.Unknown;
                        }
                    } else if( resp.ResponseUri.Segments[1] != "app/" ) { // Redirected outside of the app path
                        Program.Logger.Write( LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingRedirectedToNonApp, id );
                        return AppTypes.Other;
                    } else if( resp.ResponseUri.Segments.Length < 3 ) { // The URI ends with "/app/" ?
                        Program.Logger.Write( LoggerLevel.Verbose, GlobalStrings.GameDB_Log_ScrapingNoAppId, id );
                        return AppTypes.Unknown;
                    } else if( resp.ResponseUri.Segments[2].TrimEnd( '/' ) != id.ToString() ) { // Redirected to a different app id
                        Program.Logger.Write( LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingRedirectedToOtherApp, id, resp.ResponseUri.Segments[2].TrimEnd( '/' ) );
                        if( !int.TryParse( resp.ResponseUri.Segments[2].TrimEnd( '/' ), out redirectTarget ) ) { // if new app id is an actual number
                            return AppTypes.Unknown;
                        }
                    }

                    StreamReader sr = new StreamReader( resp.GetResponseStream() );
                    page = sr.ReadToEnd();
                    Program.Logger.Write( LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingPageRead, id );
                }
            } catch( Exception e ) {
                // Something went wrong with the download.
                Program.Logger.Write( LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingPageReadFailed, id, e.Message );
                LastStoreScrape = oldTime;
                return AppTypes.Unknown;
            }

            AppTypes result = AppTypes.Unknown;

            if( page.Contains( "<title>Site Error</title>" ) ) {
                Program.Logger.Write( LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingReceivedSiteError, id );
                result = AppTypes.Unknown;
            } else if( regGamecheck.IsMatch( page ) ) { // Here we should have an app, but make sure.

                GetAllDataFromPage( page );

                // Check whether it's DLC and return appropriately
                if( Flags.Contains( "Downloadable Content" ) ) {
                    Program.Logger.Write( LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingParsedDLC, id, string.Join( ",", Genres ) );
                    result = AppTypes.DLC;
                } else {
                    Program.Logger.Write( LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingParsed, id, string.Join( ",", Genres ) );
                    result = AppTypes.Game;
                }
            } else { // The URI is right, but it didn't pass the regex check
                Program.Logger.Write( LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingCouldNotParse, id );
                result = AppTypes.Unknown;
            }

            if( redirectTarget != -1 ) {
                this.ParentId = redirectTarget;
                result = AppTypes.Unknown;
            }

            return result;
        }

        /// <summary>
        /// Updates the game's type with a type determined during a store scrape. Makes sure that better data (AppInfo type) isn't overwritten with worse data.
        /// </summary>
        /// <param name="typeFromStore">Type found from the store scrape</param>
        private void SetTypeFromStoreScrape( AppTypes typeFromStore ) {
            if( this.AppType == AppTypes.Unknown || ( typeFromStore != AppTypes.Unknown && LastAppInfoUpdate == 0 ) ) {
                this.AppType = typeFromStore;
            }
        }

        /// <summary>
        /// Applies all data from a steam store page to this entry
        /// </summary>
        /// <param name="page">The full result of the HTTP request.</param>
        private void GetAllDataFromPage( string page ) {
            // Genres
            Match m = regGenre.Match( page );
            if( m.Success ) {
                Genres = new List<string>();
                foreach( Capture cap in m.Groups[2].Captures ) {
                    Genres.Add( cap.Value );
                }
            }

            // Flags
            MatchCollection matches = regFlags.Matches( page );
            if( matches.Count > 0 ) {
                Flags = new List<string>();
                foreach( Match ma in matches ) {
                    string flag = ma.Groups[1].Value;
                    if( !string.IsNullOrWhiteSpace( flag ) ) this.Flags.Add( flag );
                }
            }

            matches = regTags.Matches( page );
            if( matches.Count > 0 ) {
                Tags = new List<string>();
                foreach( Match ma in matches ) {
                    string tag = WebUtility.HtmlDecode(ma.Groups[1].Value.Trim());
                    if( !string.IsNullOrWhiteSpace( tag ) ) this.Tags.Add( tag );
                }
            }

            // Get Developer
            m = regDevelopers.Match( page );
            if( m.Success ) {
                Developers = new List<string>();
                foreach( Capture cap in m.Groups[2].Captures ) {
                    Developers.Add( WebUtility.HtmlDecode(cap.Value) );
                }
            }

            // Get Publishers
            m = regPublishers.Match( page );
            if( m.Success ) {
                Publishers = new List<string>();
                foreach( Capture cap in m.Groups[2].Captures ) {
                    Publishers.Add(WebUtility.HtmlDecode(cap.Value));
                }
            }

            // Get release date
            m = regRelDate.Match( page );
            if( m.Success ) {
                this.SteamReleaseDate = m.Groups[1].Captures[0].Value;
            }

            // Get user review data
            m = regReviews.Match( page );
            if( m.Success ) {
                int num = 0;
                if( int.TryParse( m.Groups[1].Value, out num ) ) {
                    this.ReviewPositivePercentage = num;
                }
                if( int.TryParse( m.Groups[2].Value, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out num ) ) {
                    this.ReviewTotal = num;
                }
            }

            m = regMetalink.Match( page );
            if( m.Success ) {
                this.MC_Url = m.Groups[1].Captures[0].Value;
            }
        }
        #endregion

        /// <summary>
        /// Merges in data from another entry. Useful for merging scrape results, but could also merge data from a different database.
        /// Uses newer data when there is a conflict.
        /// Does NOT perform deep copies of list fields.
        /// </summary>
        /// <param name="other">GameDBEntry containing info to be merged into this entry.</param>
        public void MergeIn( GameDBEntry other ) {
            bool useAppInfoFields = other.LastAppInfoUpdate > this.LastAppInfoUpdate || ( this.LastAppInfoUpdate == 0 && other.LastStoreScrape >= this.LastStoreScrape );
            bool useScrapeOnlyFields = other.LastStoreScrape >= this.LastStoreScrape;

            if( other.AppType != AppTypes.Unknown && ( this.AppType == AppTypes.Unknown || useAppInfoFields ) ) {
                this.AppType = other.AppType;
            }

            if( other.LastAppInfoUpdate >= this.LastAppInfoUpdate ) {
                this.Platforms = other.Platforms;
            }

            if( useAppInfoFields ) {
                if( !string.IsNullOrEmpty( other.Name ) ) Name = other.Name;
                if( other.ParentId > 0 ) ParentId = other.ParentId;
            }

            if( useScrapeOnlyFields ) {
                if( other.Genres != null && other.Genres.Count > 0 ) Genres = other.Genres;
                if( other.Flags != null && other.Flags.Count > 0 ) Flags = other.Flags;
                if( other.Tags != null && other.Tags.Count > 0 ) Tags = other.Tags;
                if( other.Developers != null && other.Developers.Count > 0 ) Developers = other.Developers;
                if( other.Publishers != null && other.Publishers.Count > 0 ) Publishers = other.Publishers;
                if( !string.IsNullOrEmpty( other.SteamReleaseDate ) ) SteamReleaseDate = other.SteamReleaseDate;

                if( other.ReviewTotal != 0 ) {
                    this.ReviewTotal = other.ReviewTotal;
                    this.ReviewPositivePercentage = other.ReviewPositivePercentage;
                }

                if( !string.IsNullOrEmpty( other.MC_Url ) ) MC_Url = other.MC_Url;
            }

            if( other.LastStoreScrape > this.LastStoreScrape ) this.LastStoreScrape = other.LastStoreScrape;
            if( other.LastAppInfoUpdate > this.LastAppInfoUpdate ) this.LastAppInfoUpdate = other.LastAppInfoUpdate;
        }
    }

    public class GameDB {
        // Main Data
        public Dictionary<int, GameDBEntry> Games = new Dictionary<int, GameDBEntry>();
        // Extra data
        private SortedSet<string> allStoreGenres;
        private SortedSet<string> allStoreFlags;
        // Utility
        static char[] genreSep = new char[] { ',' };

        private const int VERSION = 1;
        private const string
            XmlName_Version = "version",
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
            XmlName_Game_Developer = "developer",
            XmlName_Game_Publisher = "publisher",
            XmlName_Game_Flag = "flag",
            XmlName_Game_ReviewTotal = "reviewTotal",
            XmlName_Game_ReviewPositivePercent = "reviewPositiveP",
            XmlName_Game_MCUrl = "mcUrl",
            XmlName_Game_Date = "steamDate";

        #region Accessors

        public bool Contains( int id ) {
            return Games.ContainsKey( id );
        }

        public bool IncludeItemInGameList( int id, AppTypes scheme ) {
            if( Games.ContainsKey( id ) ) {
                return scheme.HasFlag( Games[id].AppType );
            }
            return scheme.HasFlag( AppTypes.Unknown );
        }

        public string GetName( int id )
        {
            if( Games.ContainsKey( id ) ) {
                return Games[id].Name;
            }
            return null;
        }

        public List<string> GetGenreList( int gameId, int depth = 3, bool tagFallback = true )
        {
            if( Games.ContainsKey( gameId ) ) {
                List<string> res = Games[gameId].Genres;
                if( tagFallback && ( res == null || res.Count == 0 ) ) {
                    List<string> tags = GetTagList( gameId, 0 );
                    if( tags != null && tags.Count > 0 ) {
                        res = new List<string>( tags.Intersect( GetAllGenres() ) );
                    }
                }
                if( ( res == null || res.Count == 0 ) && depth > 0 && Games[gameId].ParentId > 0 ) {
                    res = GetGenreList( Games[gameId].ParentId, depth - 1, tagFallback );
                }
                return res;
            }
            return null;
        }

        public List<string> GetFlagList( int gameId, int depth = 3 )
        {
            if( Games.ContainsKey( gameId ) ) {
                List<string> res = Games[gameId].Flags;
                if( ( res == null || res.Count == 0 ) && depth > 0 && Games[gameId].ParentId > 0 ) {
                    res = GetFlagList( Games[gameId].ParentId, depth - 1 );
                }
                return res;
            }
            return null;
        }

        public List<string> GetTagList( int gameId, int depth = 3 ) {
            if( Games.ContainsKey( gameId ) ) {
                List<string> res = Games[gameId].Tags;
                if( ( res == null || res.Count == 0 ) && depth > 0 && Games[gameId].ParentId > 0 ) {
                    res = GetTagList( Games[gameId].ParentId, depth - 1 );
                }
                return res;
            } 
                return null;
        }

        public int GetReleaseYear( int gameId ) {
            if( Games.ContainsKey( gameId ) ) {
                GameDBEntry dbEntry = Games[gameId];
                DateTime releaseDate;
                if( DateTime.TryParse( dbEntry.SteamReleaseDate, out releaseDate ) ) {
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
        public SortedSet<string> GetAllGenres() {
            if( allStoreGenres == null ) {
                return CalculateAllGenres();
            } else {
                return allStoreGenres;
            }
        }

        /// <summary>
        /// Gets a list of all Steam store genres found in the entire database.
        /// Always recalculates.
        /// </summary>
        /// <returns>A set of genres, as strings</returns>
        public SortedSet<string> CalculateAllGenres() {
            if( allStoreGenres == null ) {
                allStoreGenres = new SortedSet<string>( StringComparer.OrdinalIgnoreCase );
            } else {
                allStoreGenres.Clear();
            }

            foreach( GameDBEntry entry in Games.Values ) {
                if( entry.Genres != null ) {
                    allStoreGenres.UnionWith( entry.Genres );
                }
            }

            return allStoreGenres;
        }

        /// <summary>
        /// Gets a list of all Steam store flags found in the entire database.
        /// Only recalculates if necessary.
        /// </summary>
        /// <returns>A set of genres, as strings</returns>
        public SortedSet<string> GetAllStoreFlags() {
            if( allStoreFlags == null ) {
                return CalculateAllStoreFlags();
            } else {
                return allStoreFlags;
            }
        }

        /// <summary>
        /// Gets a list of all Steam store flags found in the entire database.
        /// Always recalculates.
        /// </summary>
        /// <returns>A set of genres, as strings</returns>
        public SortedSet<string> CalculateAllStoreFlags() {
            if( allStoreFlags == null ) {
                allStoreFlags = new SortedSet<string>( StringComparer.OrdinalIgnoreCase );
            } else {
                allStoreFlags.Clear();
            }

            foreach( GameDBEntry entry in Games.Values ) {
                if( entry.Flags != null ) {
                    allStoreFlags.UnionWith( entry.Flags );
                }
            }
            return allStoreFlags;
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
        public IEnumerable<Tuple<string, float>> CalculateSortedTagList( GameList filter, float weightFactor, int minScore, int tagsPerGame, bool excludeGenres, bool scoreSort ) {
            SortedSet<string> genreNames = GetAllGenres();
            Dictionary<string, float> tagCounts = new Dictionary<string, float>();
            if( filter == null ) {
                foreach( GameDBEntry dbEntry in Games.Values ) {
                    CalculateSortedTagListHelper( tagCounts, dbEntry, weightFactor, tagsPerGame );
                }
            } else {
                foreach( int gameId in filter.Games.Keys ) {
                    if( Games.ContainsKey( gameId ) ) {
                        CalculateSortedTagListHelper( tagCounts, Games[gameId], weightFactor, tagsPerGame );
                    }
                }
            }

            if( excludeGenres ) {
                foreach( string genre in genreNames ) {
                    tagCounts.Remove( genre );
                }
            }

            var unsortedList = ( from entry in tagCounts where entry.Value >= minScore select new Tuple<string, float>( entry.Key, entry.Value ) );
            var sortedList = scoreSort ?
                from entry in unsortedList orderby entry.Item2 descending select entry :
                from entry in unsortedList orderby entry.Item1 select entry;
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
        private void CalculateSortedTagListHelper( Dictionary<string, float> counts, GameDBEntry dbEntry, float weightFactor, int tagsPerGame ) {
            if( dbEntry.Tags != null ) {
                int tagsToLoad = ( tagsPerGame == 0 ) ? dbEntry.Tags.Count : Math.Min( tagsPerGame, dbEntry.Tags.Count );
                for( int i = 0; i < tagsToLoad; i++ ) {
                    // Get the score based on the weighting factor
                    float score = 1;
                    if( weightFactor > 1 ) {
                        if( tagsToLoad <= 1 ) {
                            score = weightFactor;
                        } else {
                            float interp = (float)i / (float)( tagsToLoad - 1 );
                            score = ( 1 - interp ) * weightFactor + interp;
                        }
                    }

                    string tag = dbEntry.Tags[i];
                    if( counts.ContainsKey( tag ) ) {
                        counts[tag] += score;
                    } else {
                        counts[tag] = score;
                    }
                }
            }
        }

        private void ClearAggregates() {
            allStoreGenres = null;
            allStoreFlags = null;
        }


        #endregion

        #region Operations
        public void UpdateAppListFromWeb() {
            XmlDocument doc = FetchAppListFromWeb();
            IntegrateAppList( doc );
        }

        public static XmlDocument FetchAppListFromWeb() {
            XmlDocument doc = new XmlDocument();
            Program.Logger.Write( Rallion.LoggerLevel.Info, GlobalStrings.GameDB_DownloadingSteamAppList );
            WebRequest req = WebRequest.Create( @"http://api.steampowered.com/ISteamApps/GetAppList/v0002/?format=xml" );
            using( WebResponse resp = req.GetResponse() ) {
                doc.Load( resp.GetResponseStream() );
            }
            Program.Logger.Write( LoggerLevel.Info, GlobalStrings.GameDB_XMLAppListDownloaded );
            return doc;
        }

        public int IntegrateAppList( XmlDocument doc ) {
            int added = 0;
            foreach( XmlNode node in doc.SelectNodes( "/applist/apps/app" ) ) {
                int appId;
                if( XmlUtil.TryGetIntFromNode( node["appid"], out appId ) ) {
                    string gameName = XmlUtil.GetStringFromNode( node["name"], null );
                    if( Games.ContainsKey( appId ) ) {
                        GameDBEntry g = Games[appId];
                        if( string.IsNullOrEmpty( g.Name ) || g.Name != gameName ) {
                            g.Name = gameName;
                            g.AppType = AppTypes.Unknown;
                        }
                    } else {
                        GameDBEntry g = new GameDBEntry();
                        g.Id = appId;
                        g.Name = gameName;
                        Games.Add( appId, g );
                        added++;
                    }
                }
            }
            Program.Logger.Write( LoggerLevel.Info, GlobalStrings.GameDB_LoadedNewItemsFromAppList, added );
            return added;
        }

        /// <summary>
        /// Updated the database with information from the AppInfo cache file.
        /// </summary>
        /// <param name="path">Path to the cache file</param>
        /// <returns>The number of entries integrated into the database.</returns>
        public int UpdateFromAppInfo( string path ) {
            int updated = 0;

            Dictionary<int, AppInfo> appInfos = AppInfo.LoadApps( path );
            int timestamp = Utility.GetCurrentUTime();

            foreach( AppInfo aInf in appInfos.Values ) {
                GameDBEntry entry;
                if( !Games.ContainsKey( aInf.Id ) ) {
                    entry = new GameDBEntry();
                    entry.Id = aInf.Id;
                    Games.Add( entry.Id, entry );
                } else {
                    entry = Games[aInf.Id];
                }

                entry.LastAppInfoUpdate = timestamp;
                entry.AppType = aInf.AppType;
                entry.Name = aInf.Name;
                entry.Platforms = aInf.Platforms;
                entry.ParentId = aInf.Parent;
                updated++;
            }
            return updated;
        }

        #endregion

        #region Serialization

        public void Save( string path ) {
            Save( path, path.EndsWith( ".gz" ) );
        }

        public void Save( string path, bool compress ) {
            Program.Logger.Write( LoggerLevel.Info, GlobalStrings.GameDB_SavingGameDBTo, path );
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.CloseOutput = true;

            Stream stream = null;
            try {
                stream = new FileStream( path, FileMode.Create );

                if( compress ) {
                    stream = new GZipStream( stream, CompressionMode.Compress );
                }

                XmlWriter writer = XmlWriter.Create( stream, settings );
                writer.WriteStartDocument();
                writer.WriteStartElement( XmlName_GameList );

                writer.WriteElementString( XmlName_Version, VERSION.ToString() );

                foreach( GameDBEntry g in Games.Values ) {

                    writer.WriteStartElement( XmlName_Game );

                    writer.WriteElementString( XmlName_Game_Id, g.Id.ToString() );

                    if( !string.IsNullOrEmpty( g.Name ) ) {
                        writer.WriteElementString( XmlName_Game_Name, g.Name );
                    }

                    if( g.LastStoreScrape > 0 ) writer.WriteElementString( XmlName_Game_LastStoreUpdate, g.LastStoreScrape.ToString() );
                    if( g.LastAppInfoUpdate > 0 ) writer.WriteElementString( XmlName_Game_LastAppInfoUpdate, g.LastAppInfoUpdate.ToString() );

                    writer.WriteElementString( XmlName_Game_Type, g.AppType.ToString() );

                    writer.WriteElementString( XmlName_Game_Platforms, g.Platforms.ToString() );

                    if( g.ParentId >= 0 ) writer.WriteElementString( XmlName_Game_Parent, g.ParentId.ToString() );

                    if( g.Genres != null ) {
                        foreach( string str in g.Genres ) {
                            writer.WriteElementString( XmlName_Game_Genre, str );
                        }
                    }

                    if( g.Tags != null ) {
                        foreach( string str in g.Tags ) {
                            writer.WriteElementString( XmlName_Game_Tag, str );
                        }
                    }

                    if( g.Developers != null ) {
                        foreach( string str in g.Developers ) {
                            writer.WriteElementString( XmlName_Game_Developer, str );
                        }
                    }

                    if( g.Publishers != null ) {
                        foreach( string str in g.Publishers ) {
                            writer.WriteElementString( XmlName_Game_Publisher, str );
                        }
                    }

                    if( g.Flags != null ) {
                        foreach( string s in g.Flags ) {
                            writer.WriteElementString( XmlName_Game_Flag, s );
                        }
                    }

                    if( g.ReviewTotal > 0 ) {
                        writer.WriteElementString( XmlName_Game_ReviewTotal, g.ReviewTotal.ToString() );
                        writer.WriteElementString( XmlName_Game_ReviewPositivePercent, g.ReviewPositivePercentage.ToString() );
                    }

                    if( !string.IsNullOrEmpty( g.MC_Url ) ) {
                        writer.WriteElementString( XmlName_Game_MCUrl, g.MC_Url );
                    }

                    if( !string.IsNullOrEmpty( g.SteamReleaseDate ) ) {
                        writer.WriteElementString( XmlName_Game_Date, g.SteamReleaseDate );
                    }

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            } catch( Exception e ) {
                throw e;
            } finally {
                if( stream != null ) {
                    stream.Close();
                }
            }
            Program.Logger.Write( LoggerLevel.Info, GlobalStrings.GameDB_GameDBSaved );
        }

        public void Load( string path ) {
            Load( path, path.EndsWith( ".gz" ) );
        }

        public void Load( string path, bool compress ) {
            Program.Logger.Write( LoggerLevel.Info, GlobalStrings.GameDB_LoadingGameDBFrom, path );
            XmlDocument doc = new XmlDocument();

            Stream stream = null;
            try {
                stream = new FileStream( path, FileMode.Open );
                if( compress ) {
                    stream = new GZipStream( stream, CompressionMode.Decompress );
                }

                doc.Load( stream );

                Program.Logger.Write( LoggerLevel.Info, GlobalStrings.GameDB_GameDBXMLParsed );
                Games.Clear();
                ClearAggregates();

                XmlNode gameListNode = doc.SelectSingleNode( "/" + XmlName_GameList );

                int fileVersion = XmlUtil.GetIntFromNode( gameListNode[XmlName_Version], 0 );

                foreach( XmlNode gameNode in gameListNode.SelectNodes( XmlName_Game ) ) {
                    int id;
                    if( !XmlUtil.TryGetIntFromNode( gameNode[XmlName_Game_Id], out id ) || Games.ContainsKey( id ) ) {
                        continue;
                    }
                    GameDBEntry g = new GameDBEntry();
                    g.Id = id;

                    g.Name = XmlUtil.GetStringFromNode( gameNode[XmlName_Game_Name], null );

                    if( fileVersion < 1 ) {
                        g.AppType = AppTypes.Unknown;
                        string typeString;
                        if( XmlUtil.TryGetStringFromNode( gameNode[XmlName_Game_Type], out typeString ) ) {
                            if( typeString == "DLC" ) {
                                g.AppType = AppTypes.DLC;
                            } else if( typeString == "Game" ) {
                                g.AppType = AppTypes.Game;
                            } else if( typeString == "NonApp" ) {
                                g.AppType = AppTypes.Other;
                            }
                        }
                    } else {
                        g.AppType = XmlUtil.GetEnumFromNode<AppTypes>( gameNode[XmlName_Game_Type], AppTypes.Unknown );
                    }

                    g.Platforms = XmlUtil.GetEnumFromNode<AppPlatforms>( gameNode[XmlName_Game_Platforms], AppPlatforms.All );

                    g.ParentId = XmlUtil.GetIntFromNode( gameNode[XmlName_Game_Parent], -1 );

                    if( fileVersion < 1 ) {
                        List<string> genreList = new List<string>();
                        string genreString = XmlUtil.GetStringFromNode( gameNode["genre"], null );
                        if( genreString != null ) {
                            string[] genStrList = genreString.Split( ',' );
                            foreach( string s in genStrList ) {
                                genreList.Add( s.Trim() );
                            }
                        }
                        g.Genres = genreList;
                    } else {
                        g.Genres = XmlUtil.GetStringsFromNodeList( gameNode.SelectNodes( XmlName_Game_Genre ) );
                    }

                    g.Tags = XmlUtil.GetStringsFromNodeList( gameNode.SelectNodes( XmlName_Game_Tag ) );

                    g.Developers = XmlUtil.GetStringsFromNodeList( gameNode.SelectNodes( XmlName_Game_Developer ) );

                    if( fileVersion < 1 ) {
                        List<string> pubList = new List<string>();
                        string pubString = XmlUtil.GetStringFromNode( gameNode["publisher"], null );
                        if( pubString != null ) {
                            string[] pubStrList = pubString.Split( ',' );
                            foreach( string s in pubStrList ) {
                                pubList.Add( s.Trim() );
                            }
                        }
                        g.Publishers = pubList;
                    } else {
                        g.Publishers = XmlUtil.GetStringsFromNodeList( gameNode.SelectNodes( XmlName_Game_Publisher ) );
                    }

                    if( fileVersion < 1 ) {
                        int steamDate = XmlUtil.GetIntFromNode( gameNode["steamDate"], 0 );
                        if( steamDate > 0 ) {
                            g.SteamReleaseDate = DateTime.FromOADate( steamDate ).ToString( "MMM d, yyyy" );
                        } else {
                            g.SteamReleaseDate = null;
                        }
                    } else {
                        g.SteamReleaseDate = XmlUtil.GetStringFromNode( gameNode[XmlName_Game_Date], null );
                    }

                    g.Flags = XmlUtil.GetStringsFromNodeList( gameNode.SelectNodes( XmlName_Game_Flag ) );

                    g.ReviewTotal = XmlUtil.GetIntFromNode( gameNode[XmlName_Game_ReviewTotal], 0 );
                    g.ReviewPositivePercentage = XmlUtil.GetIntFromNode( gameNode[XmlName_Game_ReviewPositivePercent], 0 );

                    g.MC_Url = XmlUtil.GetStringFromNode( gameNode[XmlName_Game_MCUrl], null );

                    g.LastAppInfoUpdate = XmlUtil.GetIntFromNode( gameNode[XmlName_Game_LastAppInfoUpdate], 0 );
                    g.LastStoreScrape = XmlUtil.GetIntFromNode( gameNode[XmlName_Game_LastStoreUpdate], 0 );

                    Games.Add( id, g );
                }
                Program.Logger.Write( LoggerLevel.Info, GlobalStrings.GameDB_GameDBXMLProcessed );
            } catch( Exception e ) {
                throw e;
            } finally {
                if( stream != null ) {
                    stream.Close();
                }
            }
        }

        #endregion
    }

    public class GameDBEntrySorter : IComparer<GameDBEntry> {
        public enum SortModes {
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

        public void SetSortMode( SortModes mode, int forceDir = 0 ) {
            if( mode == SortMode ) {
                SortDirection = ( forceDir == 0 ) ? ( SortDirection *= -1 ) : forceDir;
            } else {
                SortMode = mode;
                SortDirection = ( forceDir == 0 ) ? 1 : forceDir;
            }
        }

        public int Compare( GameDBEntry a, GameDBEntry b ) {
            int res = 0;
            switch( SortMode ) {
                case SortModes.Id:
                    res = a.Id - b.Id;
                    break;
                case SortModes.Name:
                    res = string.Compare( a.Name, b.Name );
                    break;
                case SortModes.Genre:
                    res = Utility.CompareLists( a.Genres, b.Genres );
                    break;
                case SortModes.Type:
                    res = a.AppType - b.AppType;
                    break;
                case SortModes.IsScraped:
                    res = ( ( a.LastStoreScrape > 0 ) ? 1 : 0 ) - ( ( b.LastStoreScrape > 0 ) ? 1 : 0 );
                    break;
                case SortModes.HasAppInfo:
                    res = ( ( a.LastAppInfoUpdate > 0 ) ? 1 : 0 ) - ( ( b.LastAppInfoUpdate > 0 ) ? 1 : 0 );
                    break;
                case SortModes.Parent:
                    res = a.ParentId - b.ParentId;
                    break;
            }
            return SortDirection * res;
        }
    }
}
