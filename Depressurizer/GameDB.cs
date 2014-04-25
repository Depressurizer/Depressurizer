
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using Rallion;
using System.IO.Compression;
using System.Globalization;

namespace Depressurizer {

    public enum AppType {
        New,
        Game,
        DLC,
        IdRedirect,
        NonApp,
        NotFound,
        AgeGated,
        SiteError,
        WebError,
        Unknown
    }

    public class GameDBEntry {

        public int Id;
        public string Name;
        public string Genre;

        // New stuff:
        // Basics:
        public string Developer = null;
        public string Publisher = null;
        public DateTime SteamRelease = DateTime.MinValue;
        // Metacritic:
        public string MC_Url = null;
        public int MC_Score = -1;
        public string MC_Genre = null;
        public int MC_Year = -1;

        public List<string> Flags = new List<string>();

        public AppType Type;


        private static Regex regGamecheck = new Regex( "<a[^>]*>All Games</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled );

        private static Regex regGenre = new Regex( "<div class=\\\"details_block\\\">\\s*<b>Title:</b>[^<]*<br>\\s*<b>Genre:</b>\\s*(<a[^>]*>([^<]+)</a>,?\\s*)+\\s*<br>", RegexOptions.Compiled | RegexOptions.IgnoreCase );

        //private static Regex regDLC = new Regex("<div class=\\\"name\\\"><a href=[^>]*>Downloadable Content</a></div>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static Regex regFlags = new Regex("<div class=\\\"game_area_details_specs\\\">\\s*<div class=\\\"icon\\\"><a href=[^>]*><img[^>]*></a></div>\\s*<div class=\\\"name\\\"><a href=[^>]*>([^<]*)</a></div>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static Regex regDeveloper = new Regex("<b>Developer:</b>\\s*<a[^>]*>([^<]*)</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static Regex regPublishers = new Regex("<b>Publisher:</b>\\s*(<a[^>]*>([^<]+)</a>,?\\s*)+\\s*<br>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static Regex regRelDate = new Regex( "<b>Release Date:</b>\\s*([^<]*)<br>", RegexOptions.IgnoreCase | RegexOptions.Compiled );
        private static Regex regMetalink = new Regex( "<div id=\\\"game_area_metalink\\\">\\s*<a href=\\\"http://www.metacritic.com/game/pc/([^\\\"]*)", RegexOptions.IgnoreCase | RegexOptions.Compiled );

        public AppType ScrapeStore() {
            return ScrapeStore( this.Id );
        }

        public AppType ScrapeStore( int id, int redirectCount = 0 ) {
            Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_InitiatingStoreScrapeForGame, id);
            // jpodadera
            // In case of analizing redirections, this.Id should be kept
            //Id = id;

            bool redirect = ( redirectCount > 0 );

            // jpodadera
            // In case of no analizing redirections, this.Id is set
            if (!redirect)
                this.Id = id;

            string page = "";

            int redirectTarget = 0;
            bool needsRedirect = false;

            try {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create( string.Format( Properties.Resources.UrlSteamStore, id ) );
                // Cookie bypasses the age gate
                req.CookieContainer = new CookieContainer( 1 );
                req.CookieContainer.Add( new Cookie( "birthtime", "-2208959999", "/", "store.steampowered.com" ) );

                using( WebResponse resp = req.GetResponse() ) {
                    if( resp.ResponseUri.Segments.Length <= 1 ) {
                        // Redirected to the store front page
                        Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingRedirectedToMainStorePage, id);
                        Type = AppType.NotFound;
                        return Type;
                    } else if( resp.ResponseUri.Segments.Length >= 2 && resp.ResponseUri.Segments[1] == "agecheck/" ) {
                        if( redirectCount <= 3 && resp.ResponseUri.Segments.Length >= 4 && !resp.ResponseUri.Segments[3].StartsWith( id.ToString() ) ) {
                            // We got an age check for a different ID than we requested
                            if( int.TryParse( resp.ResponseUri.Segments[3].TrimEnd( '/' ), out redirectTarget ) ) {
                                Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingHitAgeCheck, id, redirectTarget);
                                needsRedirect = true;
                            } else {
                                // Age check without numeric id
                                Program.Logger.Write(LoggerLevel.Warning, GlobalStrings.GameDB_ScrapingStuckAtAgeCheck, id, resp.ResponseUri);
                                Type = AppType.AgeGated;
                                return Type;
                            }
                        } else {
                            // Age check with no redirect
                            Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingAgeCheckNoRedirect, id, redirectCount);
                            Type = AppType.AgeGated;
                            return Type;
                        }
                    } else if( resp.ResponseUri.Segments.Length < 2 || resp.ResponseUri.Segments[1] != "app/" ) {
                        // Redirected outside of the app path
                        Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingRedirectedToNonApp, id);
                        Type = AppType.NonApp;
                        return Type;
                    } else if( resp.ResponseUri.Segments.Length < 3 || !resp.ResponseUri.Segments[2].StartsWith( id.ToString() ) ) {
                        // Redirected to a different app id, but we still want to check the genre
                        Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingRedirectedToOtherApp, id, resp.ResponseUri.Segments.Length >= 3 ? resp.ResponseUri.Segments[2] : "unknown");
                        redirect = true;
                    }

                    if( !needsRedirect ) {
                        StreamReader sr = new StreamReader( resp.GetResponseStream() );
                        page = sr.ReadToEnd();
                        Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingPageRead, id);
                    }
                }
            } catch( Exception e ) {
                // Something went wrong with the download.
                Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingPageReadFailed, id, e.Message);
                Type = AppType.WebError;
                return Type;
            }

            if( needsRedirect ) {
                Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingRedirectedTo, id, redirectTarget);
                // jpodadera
                // Return AppType should be assigned to self object
                //return ScrapeStore( redirectTarget, redirectCount + 1 );
                this.Type = ScrapeStore(redirectTarget, redirectCount + 1);
                return this.Type;
            }

            if( page.Contains( "<title>Site Error</title>" ) ) {
                Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingReceivedSiteError, id);
                Type = AppType.SiteError;
                return Type;
            }

            // Here we should have an app, but we want to make sure.
            if( regGamecheck.IsMatch( page ) ) {

                GetGenreFromPage( page );
                GetOtherFromPage( page );
                GetMetalinkFromSteam( page );
                //TODO: add metacritic scrape

                // Check whethe it's DLC and return appropriately
                if( GetDLCFromPage( page ) ) {
                    Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingParsedDLC, id, Genre);
                    Type = AppType.DLC;
                    return Type;
                } else {
                    Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingParsed, id, Genre, redirect);
                    Type = redirect ? AppType.IdRedirect : AppType.Game;
                    return Type;
                }
            } else {
                // we don't know what it is.
                Program.Logger.Write(LoggerLevel.Verbose, GlobalStrings.GameDB_ScrapingCouldNotParse, id);
                Type = AppType.Unknown;
                return Type;
            }
        }

        private bool GetGenreFromPage( string page ) {
            Match m = regGenre.Match( page );
            if( m.Success ) {
                int genres = m.Groups[2].Captures.Count;
                string[] array = new string[genres];
                for( int i = 0; i < genres; i++ ) {
                    array[i] = m.Groups[2].Captures[i].Value;
                }
                this.Genre = string.Join( ", ", array );
                return true;
            }
            return false;
        }

        private void GetOtherFromPage( string page ) {
            foreach( Match ma in regFlags.Matches( page ) ) {
                string flag = ma.Groups[1].Captures[0].Value;
                if( !string.IsNullOrWhiteSpace( flag ) ) this.Flags.Add( flag );
            }

            Match m = regDeveloper.Match( page );
            if( m.Success ) {
                this.Developer = m.Groups[1].Captures[0].Value;
            }

            // jpodadera. Some games has more than one publisher. Check http://store.steampowered.com/app/96300
            m = regPublishers.Match(page);
            if (m.Success)
            {
                List<string> publishers = new List<string>();
                foreach (Capture cap in m.Groups[2].Captures)
                {
                    publishers.Add(cap.Value);
                }
                this.Publisher = string.Join(", ", publishers);
            }


            m = regRelDate.Match( page );
            if( m.Success ) {
                // Fail with other cultures (like spanish and italian) because release date is scraped in english. Check: http://store.steampowered.com/app/72904/
                // Day of realease may be missing: http://store.steampowered.com/app/61510/
                // Or Steam may place what they want... http://store.steampowered.com/app/264040/
                DateTime releaseDate;
                if (DateTime.TryParse(m.Groups[1].Captures[0].Value, new CultureInfo("en"), DateTimeStyles.None, out releaseDate))
                {
                    this.SteamRelease = releaseDate;
                }
            }
        }

        private void GetMetalinkFromSteam( string page ) {
            Match m = regMetalink.Match( page );
            if( m.Success ) {
                this.MC_Url = m.Groups[1].Captures[0].Value;
            }
        }

        private bool GetDLCFromPage( string page ) {
            return this.Flags.Contains("Downloadable Content");
        }

    }

    public class GameDB {
        public Dictionary<int, GameDBEntry> Games = new Dictionary<int, GameDBEntry>();
        static char[] genreSep = new char[] { ',' };

        #region Accessors
        public bool Contains(int id)
        {
            return Games.ContainsKey( id );
        }

        public bool IsDlc(int id)
        {
            return Games.ContainsKey( id ) && Games[id].Type == AppType.DLC;
        }

        public string GetName(int id)
        {
            if( Games.ContainsKey( id ) ) {
                return Games[id].Name;
            } else {
                return null;
            }
        }

        public string GetGenre(int id, bool full)
        {
            if( Games.ContainsKey( id ) ) {
                string fullString = Games[id].Genre;
                if( string.IsNullOrEmpty( fullString ) ) {
                    return null;
                } else if( full ) {
                    return fullString;
                } else {
                    return TruncateGenre( fullString );
                }
            } else {
                return null;
            }
        }

        #endregion

        #region Operations
        public void UpdateAppList() {
            XmlDocument doc = FetchAppList();
            IntegrateAppList( doc );
        }

        public static XmlDocument FetchAppList() {
            XmlDocument doc = new XmlDocument();
            Program.Logger.Write(Rallion.LoggerLevel.Info, GlobalStrings.GameDB_DownloadingSteamAppList);
            WebRequest req = WebRequest.Create( @"http://api.steampowered.com/ISteamApps/GetAppList/v0002/?format=xml" );
            using( WebResponse resp = req.GetResponse() ) {
                doc.Load( resp.GetResponseStream() );
            }
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_XMLAppListDownloaded);
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
                            g.Type = AppType.New;
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
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_LoadedNewItemsFromAppList, added);
            return added;
        }

        public int MergeGameDB( GameDB merge, bool overwriteGenres, out int genresUpdated ) {
            genresUpdated = 0;
            int added = 0;
            foreach( GameDBEntry mEntry in merge.Games.Values ) {
                if( this.Games.ContainsKey( mEntry.Id ) ) {
                    if( overwriteGenres || string.IsNullOrEmpty(this.Games[mEntry.Id].Genre) ) {
                        this.Games[mEntry.Id].Genre = mEntry.Genre;
                        genresUpdated++;
                    }
                } else {
                    this.Games.Add( mEntry.Id, mEntry );
                    added++;
                }
            }
            return added;
        }
        #endregion

        #region Serialization

        public void Save( string path ) {
            Save( path, path.EndsWith( ".gz" ) );
        }

        public void Save( string path, bool compress ) {
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_SavingGameDBTo, path);
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
                writer.WriteStartElement( "gamelist" );
                foreach( GameDBEntry g in Games.Values ) {

                    writer.WriteStartElement( "game" );

                    writer.WriteElementString( "id", g.Id.ToString() );
                    if( !string.IsNullOrEmpty( g.Name ) ) {
                        writer.WriteElementString( "name", g.Name );
                    }
                    writer.WriteElementString( "type", g.Type.ToString() );
                    if( !string.IsNullOrEmpty( g.Genre ) ) {
                        writer.WriteElementString( "genre", g.Genre );
                    }
                    if( !string.IsNullOrEmpty( g.Developer ) ) {
                        writer.WriteElementString( "developer", g.Developer );
                    }
                    if( !string.IsNullOrEmpty( g.Publisher ) ) {
                        writer.WriteElementString( "publisher", g.Publisher );
                    }
                    foreach( string s in g.Flags ) {
                        writer.WriteElementString( "flag", s );
                    }
                    if( !string.IsNullOrEmpty( g.MC_Url ) ) {
                        writer.WriteElementString( "mcUrl", g.MC_Url );
                    }
                    if( g.SteamRelease != DateTime.MinValue ) {
                        writer.WriteElementString( "steamDate", ( (int)( g.SteamRelease.ToOADate() ) ).ToString() );
                    }

                    // TODO: Save MC extras
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
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_GameDBSaved);
        }

        public void Load( string path ) {
            Load( path, path.EndsWith( ".gz" ) );
        }


        public void Load( string path, bool compress ) {
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_LoadingGameDBFrom, path);
            XmlDocument doc = new XmlDocument();

            Stream stream = null;
            try {
                stream = new FileStream( path, FileMode.Open );
                if( compress ) {
                    stream = new GZipStream( stream, CompressionMode.Decompress );
                }

                doc.Load( stream );

                Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_GameDBXMLParsed);
                Games.Clear();

                foreach( XmlNode gameNode in doc.SelectNodes( "/gamelist/game" ) ) {
                    int id;
                    if( !XmlUtil.TryGetIntFromNode( gameNode["id"], out id ) || Games.ContainsKey( id ) ) {
                        continue;
                    }
                    GameDBEntry g = new GameDBEntry();
                    g.Id = id;
                    XmlUtil.TryGetStringFromNode( gameNode["name"], out g.Name );
                    string typeString;
                    if( !XmlUtil.TryGetStringFromNode( gameNode["type"], out typeString ) || !Enum.TryParse<AppType>( typeString, out g.Type ) ) {
                        g.Type = AppType.New;
                    }

                    g.Genre = XmlUtil.GetStringFromNode( gameNode["genre"], null );

                    g.Developer = XmlUtil.GetStringFromNode( gameNode["developer"], null );
                    g.Publisher = XmlUtil.GetStringFromNode( gameNode["publisher"], null );

                    int steamDate = XmlUtil.GetIntFromNode( gameNode["steamDate"], 0 );
                    if( steamDate > 0 ) {
                        g.SteamRelease = DateTime.FromOADate( steamDate );
                    }

                    foreach( XmlNode n in gameNode.SelectNodes( "flag" ) ) {
                        string fName = XmlUtil.GetStringFromNode( n, null );
                        if( !string.IsNullOrEmpty( fName ) ) g.Flags.Add( fName );
                    }

                    g.MC_Url = XmlUtil.GetStringFromNode( gameNode["mcUrl"], null );


                    // TODO: Load MC extras

                    Games.Add( id, g );
                }
                Program.Logger.Write(LoggerLevel.Info, GlobalStrings.GameDB_GameDBXMLProcessed);
            } catch( Exception e ) {
                throw e;
            } finally {
                if( stream != null ) {
                    stream.Close();
                }
            }
        }
        #endregion

        #region Statics




        public static string TruncateGenre( string fullString ) {
            if( fullString == null ) return null;
            int index = fullString.IndexOf( ',' );
            if( index < 0 ) {
                return fullString;
            } else {
                return fullString.Substring( 0, index );
            }
        }

        #endregion
    }
}
