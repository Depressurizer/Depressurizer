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
using System.Xml;

namespace Depressurizer {
    public class ProfileData {

        public string FilePath = null;

        public GameData GameData = new GameData();

        public SortedSet<int> IgnoreList = new SortedSet<int>();

        public string AccountID = null;

        public string CommunityName = null;

        public bool AutoDownload = true;

        public bool AutoImport = false;

        public bool AutoExport = true;

        public bool ExportDiscard = true;

        public bool OverwriteOnDownload = false;

        public bool AutoIgnore = true;

        public bool IgnoreDlc = true;

        public int ImportSteamData() {
            string filePath = string.Format( Properties.Resources.ConfigFilePath, DepSettings.Instance().SteamPath, AccountID );
            return GameData.ImportSteamFile( filePath, IgnoreList, IgnoreDlc );
        }

        public int DownloadGameList() {
            return GameData.LoadGameList( CommunityName, OverwriteOnDownload, IgnoreList, IgnoreDlc );
        }

        public void ExportSteamData() {
            string filePath = string.Format( Properties.Resources.ConfigFilePath, DepSettings.Instance().SteamPath, AccountID );
            GameData.SaveSteamFile( filePath, ExportDiscard );
        }

        public bool IgnoreGame( int gameId ) {
            return IgnoreList.Add( gameId );
        }

        #region Saving and Loading

        public static ProfileData Load( string path ) {
            ProfileData profile = new ProfileData();

            profile.FilePath = path;

            XmlDocument doc = new XmlDocument();

            try {
                doc.Load( path );
            } catch( Exception e ) {
                throw new ApplicationException( "Error loading profile: " + e.Message, e );
            }

            XmlNode profileNode = doc.SelectSingleNode( "/profile" );

            if( profileNode != null ) {
                profile.CommunityName = XmlUtil.GetStringFromNode( profileNode["community_name"], null );
                profile.AccountID = XmlUtil.GetStringFromNode( profileNode["account_id"], null );

                profile.AutoDownload = XmlUtil.GetBoolFromNode( profileNode["auto_download"], profile.AutoDownload );
                profile.AutoImport = XmlUtil.GetBoolFromNode( profileNode["auto_import"], profile.AutoImport );
                profile.AutoExport = XmlUtil.GetBoolFromNode( profileNode["auto_export"], profile.AutoExport );
                profile.ExportDiscard = XmlUtil.GetBoolFromNode( profileNode["export_discard"], profile.ExportDiscard );
                profile.AutoIgnore = XmlUtil.GetBoolFromNode( profileNode["auto_ignore"], profile.AutoIgnore );
                profile.OverwriteOnDownload = XmlUtil.GetBoolFromNode( profileNode["overwrite_names"], profile.OverwriteOnDownload );
                profile.IgnoreDlc = XmlUtil.GetBoolFromNode( profileNode["ignore_dlc"], profile.IgnoreDlc );

                XmlNode exclusionListNode = profileNode.SelectSingleNode( "exclusions" );
                if( exclusionListNode != null ) {
                    XmlNodeList exclusionNodes = exclusionListNode.SelectNodes( "exclusion" );
                    foreach( XmlNode node in exclusionNodes ) {
                        int id;
                        if( XmlUtil.TryGetIntFromNode( node, out id ) ) {
                            profile.IgnoreList.Add( id );
                        }
                    }
                }

                XmlNode gameListNode = profileNode.SelectSingleNode( "games" );
                if( gameListNode != null ) {
                    XmlNodeList gameNodes = gameListNode.SelectNodes( "game" );
                    foreach( XmlNode node in gameNodes ) {
                        AddGameFromXmlNode( node, profile );
                    }
                }
            }

            return profile;
        }

        private static void AddGameFromXmlNode( XmlNode node, ProfileData profile ) {
            int id;
            if( XmlUtil.TryGetIntFromNode( node["id"], out id ) ) {
                if( profile.IgnoreList.Contains( id ) || ( profile.IgnoreDlc && Program.GameDB.IsDlc( id ) ) ) {
                    return;
                }
                string name = XmlUtil.GetStringFromNode( node["name"], null );
                Game game = new Game( id, name );
                profile.GameData.Games.Add( id, game );

                string catName;
                if( XmlUtil.TryGetStringFromNode( node["category"], out catName ) ) {
                    game.Category = profile.GameData.GetCategory( catName );
                }

                game.Favorite = ( node.SelectSingleNode( "favorite" ) != null );
            }
        }

        public void Save() {
            Save( FilePath );
        }

        public bool Save( string path ) {
            XmlWriterSettings writeSettings = new XmlWriterSettings();
            writeSettings.CloseOutput = true;
            writeSettings.Indent = true;

            XmlWriter writer;
            try {
                writer = XmlWriter.Create( path, writeSettings );
            } catch( Exception e ) {
                throw new ApplicationException( "Error saving profile file: " + e.Message, e );
            }
            writer.WriteStartElement( "profile" );

            if( AccountID != null ) {
                writer.WriteElementString( "account_id", AccountID );
            }

            if( CommunityName != null ) {
                writer.WriteElementString( "community_name", CommunityName );
            }

            writer.WriteElementString( "auto_download", AutoDownload.ToString() );
            writer.WriteElementString( "auto_import", AutoImport.ToString() );
            writer.WriteElementString( "auto_export", AutoExport.ToString() );
            writer.WriteElementString( "export_discard", ExportDiscard.ToString() );
            writer.WriteElementString( "auto_ignore", AutoIgnore.ToString() );
            writer.WriteElementString( "overwrite_names", OverwriteOnDownload.ToString() );
            writer.WriteElementString( "ignore_dlc", IgnoreDlc.ToString() );

            writer.WriteStartElement( "games" );

            foreach( Game g in GameData.Games.Values ) {
                writer.WriteStartElement( "game" );

                writer.WriteElementString( "id", g.Id.ToString() );

                if( g.Name != null ) {
                    writer.WriteElementString( "name", g.Name );
                }

                if( g.Category != null ) {
                    writer.WriteElementString( "category", g.Category.Name );
                }

                if( g.Favorite ) {
                    writer.WriteElementString( "favorite", true.ToString() );
                }

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.WriteStartElement( "exclusions" );

            foreach( int i in IgnoreList ) {
                writer.WriteElementString( "exclusion", i.ToString() );
            }

            writer.WriteEndElement();

            writer.WriteEndElement();

            writer.Close();
            FilePath = path;
            return true;
        }

        #endregion
    }
}
