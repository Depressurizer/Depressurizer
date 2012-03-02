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
using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using DPLib;

namespace SteamScrape {
    public class GameDB {
        public Dictionary<int, GameDBEntry> Games = new Dictionary<int, GameDBEntry>();

        public void FetchAppList() {
            XmlDocument doc = new XmlDocument();
            WebRequest req = WebRequest.Create( @"http://api.steampowered.com/ISteamApps/GetAppList/v0002/?format=xml" );
            using( WebResponse resp = req.GetResponse() ) {

                doc.Load( resp.GetResponseStream() );
            }
            foreach( XmlNode node in doc.SelectNodes( "/applist/apps/app" ) ) {
                int appId;
                if( !XmlUtil.TryGetIntFromNode( node["appid"], out appId ) ) {
                    continue;
                }
                string name;
                XmlUtil.TryGetStringFromNode( node["name"], out name );

                GameDBEntry g = new GameDBEntry();
                g.Id = appId;
                g.Name = name;

                if( !Games.ContainsKey( appId ) ) {
                    Games.Add( appId, g );
                }
            }
        }

        public void SaveToXml( string path, bool saveAll = false ) {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.CloseOutput = true;
            XmlWriter writer = XmlWriter.Create( path, settings );
            writer.WriteStartDocument();
            writer.WriteStartElement( "gamelist" );
            foreach( GameDBEntry g in Games.Values ) {
                if( saveAll || g.Type == AppType.Game || g.Type == AppType.DLC ) {
                    writer.WriteStartElement( "game" );

                    writer.WriteElementString( "id", g.Id.ToString() );
                    if( !string.IsNullOrEmpty( g.Name ) ) {
                        writer.WriteElementString( "name", g.Name );
                    }
                    writer.WriteElementString( "type", g.Type.ToString() );
                    if( !string.IsNullOrEmpty( g.Genre ) ) {
                        writer.WriteElementString( "genre", g.Genre );
                    }
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        public void LoadFromXml( string path ) {
            XmlDocument doc = new XmlDocument();
            doc.Load( path );

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
                    g.Type = AppType.Unchecked;
                }

                g.Genre = XmlUtil.GetStringFromNode( gameNode["genre"], null );

                Games.Add( id, g );
            }
        }
    }
}
