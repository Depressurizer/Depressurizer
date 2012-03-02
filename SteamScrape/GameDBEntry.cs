/*
Copyright 2011, 2012 Steve Labbe.

This file is part of Depressurizer.

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
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace SteamScrape {

    public enum AppType {
        Unchecked,
        Error,
        IdRedirect,
        Game,
        DLC,
        NonApp,
        NotFound
    }

    public class GameDBEntry {
        public int Id;
        public string Name;
        public string Genre;
        public AppType Type;

        private static Regex regGenre = new Regex( "<div class=\\\"glance_details\\\">\\s*<div>\\s*Genre:\\s*(<a[^>]*>([^<]+)</a>,?\\s*)+\\s*<br>\\s*</div>", RegexOptions.Compiled | RegexOptions.IgnoreCase );
        private static Regex regDLC = new Regex( "<div class=\\\"name\\\">Downloadable Content</div>", RegexOptions.IgnoreCase | RegexOptions.Compiled );

        public void ScrapeStore() {
            Type = ScrapeStore( Id, out Genre );
        }

        public static AppType ScrapeStore( int id, out string genre ) {
            genre = null;
            try {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create( string.Format( @"http://store.steampowered.com/app/{0}/", id ) );
                // Cookie bypasses the age gate
                req.CookieContainer = new CookieContainer( 1 );
                req.CookieContainer.Add( new Cookie( "birthtime", "0", "/", "store.steampowered.com" ) );
                string page = "";

                using( WebResponse resp = req.GetResponse() ) {
                    if( resp.ResponseUri.Segments.Length <= 1 ) {
                        // Redirected to the store front page
                        return AppType.NotFound;
                    } else if( resp.ResponseUri.Segments.Length < 2 || resp.ResponseUri.Segments[1] != "app/" ) {
                        // Redirected outside of the app path
                        return AppType.NonApp;
                    } else if( resp.ResponseUri.Segments.Length < 3 || !resp.ResponseUri.Segments[2].StartsWith( id.ToString() ) ) {
                        // Redirected to a different app id
                        return AppType.IdRedirect;
                    }
                    StreamReader sr = new StreamReader( resp.GetResponseStream() );
                    page = sr.ReadToEnd();
                }

                string newCat;
                if( GetGenreFromPage( page, out newCat ) ) {
                    genre = newCat;
                    // We have a genre, but it could be DLC
                    if( GetDLCFromPage( page ) ) {
                        return AppType.DLC;
                    } else {
                        return AppType.Game;
                    }
                } else {
                    // The URL looks like an app, but we can't find an error
                    return AppType.Error;
                }
            } catch {
                // Something went wrong. Just return unknown.
                return AppType.Error;
            }
        }

        private static bool GetGenreFromPage( string page, out string cat ) {
            cat = null;
            Match m = regGenre.Match( page );
            if( m.Success ) {
                int genres = m.Groups[2].Captures.Count;
                string[] array = new string[genres];
                for( int i = 0; i < genres; i++ ) {
                    array[i] = m.Groups[2].Captures[i].Value;
                }
                cat = string.Join( ", ", array );
                return true;
            }
            return false;
        }

        private static bool GetDLCFromPage( string page ) {
            return regDLC.IsMatch( page );
        }
    }
}
