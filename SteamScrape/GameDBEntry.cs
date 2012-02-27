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
        Unknown,
        Game,
        DLC,
        Other,
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
            try {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create( string.Format( @"http://store.steampowered.com/app/{0}/", Id ) );
                req.CookieContainer = new CookieContainer( 1 );
                req.CookieContainer.Add( new Cookie( "birthtime", "0", "/", "store.steampowered.com" ) );
                string page = "";

                using( WebResponse resp = req.GetResponse() ) {
                    if( resp.ResponseUri.AbsolutePath == @"http://store.steampowered.com" ) {
                        Type = AppType.NotFound;
                        return;
                    } else if( !resp.ResponseUri.AbsolutePath.Contains( "/app/" ) ) {
                        Type = AppType.Other;
                        return;
                    }
                    StreamReader sr = new StreamReader( resp.GetResponseStream() );
                    page = sr.ReadToEnd();
                }

                string newCat;
                if( GetGenreFromPage( page, out newCat ) ) {
                    Genre = newCat;
                    if( GetDLCFromPage( page ) ) {
                        Type = AppType.DLC;
                    } else {
                        Type = AppType.Game;
                    }
                } else {
                    Type = AppType.Other;
                }
            } catch {
                Type = AppType.Unknown;
            }

        }

        private bool GetGenreFromPage( string page, out string cat ) {
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

        private bool GetDLCFromPage( string page ) {
            return regDLC.IsMatch( page );
        }
    }
}
