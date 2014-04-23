/*
Copyright 2011, 2012, 2013 Steve Labbe.

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
using System.IO;
using Rallion;

namespace Depressurizer {
    public class Profile {

        public string FilePath = null;

        public GameData GameData = new GameData();

        public SortedSet<int> IgnoreList = new SortedSet<int>();

        public Int64 SteamID64 = 0;

        public bool AutoDownload = true;

        public bool AutoImport = false;

        public bool AutoExport = true;

        public bool ExportDiscard = true;

        public bool OverwriteOnDownload = false;

        public bool AutoIgnore = true;

        public bool IgnoreDlc = true;

        // jpodadera. Ignored non-Steam games
        public bool IgnoreExternal = true;

        public int ImportSteamData() {
            //string filePath = string.Format( Properties.Resources.ConfigFilePath, Settings.Instance().SteamPath, ID64toDirName( SteamID64 ) );
            return GameData.ImportSteamFile(SteamID64, IgnoreList, IgnoreDlc, IgnoreExternal);
        }

        public void ExportSteamData() {
            GameData.SaveSteamFile(SteamID64, ExportDiscard);
            //string filePath = string.Format( Properties.Resources.ConfigFilePath, Settings.Instance().SteamPath, ID64toDirName( SteamID64 ) );
            //GameData.SaveSteamFile( filePath, ExportDiscard );
        }

        public bool IgnoreGame( int gameId ) {
            return IgnoreList.Add( gameId );
        }

        #region Saving and Loading

        public static Profile Load( string path ) {
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.Profile_LoadingProfile, path);
            Profile profile = new Profile();

            profile.FilePath = path;

            XmlDocument doc = new XmlDocument();

            try {
                doc.Load( path );
            } catch( Exception e ) {
                Program.Logger.Write(LoggerLevel.Warning, GlobalStrings.Profile_FailedToLoadProfile, e.Message);
                throw new ApplicationException(GlobalStrings.Profile_ErrorLoadingProfile + e.Message, e);
            }

            XmlNode profileNode = doc.SelectSingleNode( "/profile" );

            if( profileNode != null ) {

                Int64 accId = XmlUtil.GetInt64FromNode( profileNode["steam_id_64"], 0 );
                if( accId == 0 ) {
                    string oldAcc = XmlUtil.GetStringFromNode( profileNode["account_id"], null );
                    if( oldAcc != null ) {
                        accId = DirNametoID64( oldAcc );
                    }
                }

                profile.SteamID64 = accId;

                profile.AutoDownload = XmlUtil.GetBoolFromNode( profileNode["auto_download"], profile.AutoDownload );
                profile.AutoImport = XmlUtil.GetBoolFromNode( profileNode["auto_import"], profile.AutoImport );
                profile.AutoExport = XmlUtil.GetBoolFromNode( profileNode["auto_export"], profile.AutoExport );
                profile.ExportDiscard = XmlUtil.GetBoolFromNode( profileNode["export_discard"], profile.ExportDiscard );
                profile.AutoIgnore = XmlUtil.GetBoolFromNode( profileNode["auto_ignore"], profile.AutoIgnore );
                profile.OverwriteOnDownload = XmlUtil.GetBoolFromNode( profileNode["overwrite_names"], profile.OverwriteOnDownload );
                profile.IgnoreDlc = XmlUtil.GetBoolFromNode( profileNode["ignore_dlc"], profile.IgnoreDlc );

                // jpodadera. Ignored non-Steam games
                profile.IgnoreExternal = XmlUtil.GetBoolFromNode(profileNode["ignore_external"], profile.IgnoreExternal);

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
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.MainForm_ProfileLoaded);
            return profile;
        }

        private static void AddGameFromXmlNode( XmlNode node, Profile profile ) {
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
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.Profile_SavingProfile, path);
            XmlWriterSettings writeSettings = new XmlWriterSettings();
            writeSettings.CloseOutput = true;
            writeSettings.Indent = true;
            
            XmlWriter writer;
            try {
                writer = XmlWriter.Create( path, writeSettings );
            } catch( Exception e ) {
                Program.Logger.Write(LoggerLevel.Warning, GlobalStrings.Profile_FailedToOpenProfileFile, e.Message);
                throw new ApplicationException(GlobalStrings.Profile_ErrorSavingProfileFile + e.Message, e);
            }
            writer.WriteStartElement( "profile" );

            writer.WriteElementString( "steam_id_64", SteamID64.ToString() );

            writer.WriteElementString( "auto_download", AutoDownload.ToString() );
            writer.WriteElementString( "auto_import", AutoImport.ToString() );
            writer.WriteElementString( "auto_export", AutoExport.ToString() );
            writer.WriteElementString( "export_discard", ExportDiscard.ToString() );
            writer.WriteElementString( "auto_ignore", AutoIgnore.ToString() );
            writer.WriteElementString( "overwrite_names", OverwriteOnDownload.ToString() );
            writer.WriteElementString( "ignore_dlc", IgnoreDlc.ToString() );

            // jpodadera. Ignored non-Steam games
            writer.WriteElementString("ignore_external", IgnoreExternal.ToString() );

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
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.Profile_ProfileSaveComplete);
            return true;
        }

        #endregion

        public static Int64 DirNametoID64( string cId ) {
            Int64 res;
            if( Int64.TryParse( cId, out res ) ) {
                return ( res + 0x0110000100000000 );
            }
            return 0;
        }

        public static string ID64toDirName( Int64 id ) {
            return ( id - 0x0110000100000000 ).ToString();
        }

    }
}
