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

        public const int VERSION = 1;

        public string FilePath = null;

        public GameList GameData = new GameList();

        public SortedSet<int> IgnoreList = new SortedSet<int>();

        public List<AutoCat> AutoCats = new List<AutoCat>();

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
            return GameData.ImportSteamConfigFile( SteamID64, IgnoreList, IgnoreDlc, IgnoreExternal );
        }

        public void ExportSteamData() {
            GameData.ExportSteamConfigFile( SteamID64, ExportDiscard );
            if( !this.IgnoreExternal ) {
                GameData.ExportSteamShortcuts( this.SteamID64 );
            }
        }

        public bool IgnoreGame( int gameId ) {
            return IgnoreList.Add( gameId );
        }

        #region Saving and Loading

        public static Profile Load( string path ) {
            Program.Logger.Write( LoggerLevel.Info, GlobalStrings.Profile_LoadingProfile, path );
            Profile profile = new Profile();

            profile.FilePath = path;

            XmlDocument doc = new XmlDocument();

            try {
                doc.Load( path );
            } catch( Exception e ) {
                Program.Logger.Write( LoggerLevel.Warning, GlobalStrings.Profile_FailedToLoadProfile, e.Message );
                throw new ApplicationException( GlobalStrings.Profile_ErrorLoadingProfile + e.Message, e );
            }

            XmlNode profileNode = doc.SelectSingleNode( "/profile" );

            if( profileNode != null ) {

                XmlAttribute versionAttr = profileNode.Attributes["version"];
                int profileVersion = 0;
                if( versionAttr != null ) {
                    if( !int.TryParse( versionAttr.Value, out profileVersion ) ) {
                        profileVersion = 0;
                    }
                }

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
                profile.IgnoreExternal = XmlUtil.GetBoolFromNode( profileNode["ignore_external"], profile.IgnoreExternal );

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
                        AddGameFromXmlNode( node, profile, profileVersion );
                    }
                }

                XmlNode autocatListNode = profileNode.SelectSingleNode( "autocats" );
                if( autocatListNode != null ) {
                    XmlNodeList autoCatNodes = autocatListNode.ChildNodes;
                    foreach( XmlNode node in autoCatNodes ) {
                        XmlElement autocatElement = node as XmlElement;
                        if( node != null ) {
                            AutoCat autocat = AutoCat.LoadACFromXmlElement( autocatElement );
                            if( autocat != null ) {
                                profile.AutoCats.Add( autocat );
                            }
                        }
                    }
                } else {
                    GenerateDefaultAutoCatSet( profile.AutoCats );
                }
                profile.AutoCats.Sort();
            }
            Program.Logger.Write( LoggerLevel.Info, GlobalStrings.MainForm_ProfileLoaded );
            return profile;
        }

        private static void AddGameFromXmlNode( XmlNode node, Profile profile, int profileVersion ) {
            int id;
            if( XmlUtil.TryGetIntFromNode( node["id"], out id ) ) {
                if( profile.IgnoreList.Contains( id ) || ( profile.IgnoreDlc && Program.GameDB.IsDlc( id ) ) ) {
                    return;
                }
                string name = XmlUtil.GetStringFromNode( node["name"], null );
                GameInfo game = new GameInfo( id, name );
                profile.GameData.Games.Add( id, game );

                game.Hidden = XmlUtil.GetBoolFromNode( node["hidden"], false );

                if( profileVersion < 1 ) {
                    string catName;
                    if( XmlUtil.TryGetStringFromNode( node["category"], out catName ) ) {
                        game.AddCategory( profile.GameData.GetCategory( catName ) );
                    }
                    if( ( node.SelectSingleNode( "favorite" ) != null ) ) {
                        game.AddCategory( profile.GameData.FavoriteCategory );
                    }
                } else {
                    XmlNode catListNode = node.SelectSingleNode( "categories" );
                    if( catListNode != null ) {
                        XmlNodeList catNodes = catListNode.SelectNodes( "category" );
                        foreach( XmlNode cNode in catNodes ) {
                            string cat;
                            if( XmlUtil.TryGetStringFromNode( cNode, out cat ) ) {
                                game.AddCategory( profile.GameData.GetCategory( cat ) );
                            }
                        }
                    }
                }
            }
        }

        public void Save() {
            Save( FilePath );
        }

        public bool Save( string path ) {
            Program.Logger.Write( LoggerLevel.Info, GlobalStrings.Profile_SavingProfile, path );
            XmlWriterSettings writeSettings = new XmlWriterSettings();
            writeSettings.CloseOutput = true;
            writeSettings.Indent = true;

            XmlWriter writer;
            try {
                writer = XmlWriter.Create( path, writeSettings );
            } catch( Exception e ) {
                Program.Logger.Write( LoggerLevel.Warning, GlobalStrings.Profile_FailedToOpenProfileFile, e.Message );
                throw new ApplicationException( GlobalStrings.Profile_ErrorSavingProfileFile + e.Message, e );
            }
            writer.WriteStartElement( "profile" );

            writer.WriteAttributeString( "version", VERSION.ToString() );

            writer.WriteElementString( "steam_id_64", SteamID64.ToString() );

            writer.WriteElementString( "auto_download", AutoDownload.ToString() );
            writer.WriteElementString( "auto_import", AutoImport.ToString() );
            writer.WriteElementString( "auto_export", AutoExport.ToString() );
            writer.WriteElementString( "export_discard", ExportDiscard.ToString() );
            writer.WriteElementString( "auto_ignore", AutoIgnore.ToString() );
            writer.WriteElementString( "overwrite_names", OverwriteOnDownload.ToString() );
            writer.WriteElementString( "ignore_dlc", IgnoreDlc.ToString() );
            writer.WriteElementString( "ignore_external", IgnoreExternal.ToString() );

            writer.WriteStartElement( "games" );

            foreach( GameInfo g in GameData.Games.Values ) {
                writer.WriteStartElement( "game" );

                writer.WriteElementString( "id", g.Id.ToString() );

                if( g.Name != null ) {
                    writer.WriteElementString( "name", g.Name );
                }

                writer.WriteElementString( "hidden", g.Hidden.ToString() );

                writer.WriteStartElement( "categories" );
                foreach( Category c in g.Categories ) {
                    writer.WriteElementString( "category", c.Name );
                }
                writer.WriteEndElement(); // categories

                writer.WriteEndElement(); // game
            }

            writer.WriteEndElement(); // games

            writer.WriteStartElement( "autocats" );

            foreach( AutoCat autocat in AutoCats ) {
                autocat.WriteToXml( writer );
            }

            writer.WriteEndElement(); //autocats

            writer.WriteStartElement( "exclusions" );

            foreach( int i in IgnoreList ) {
                writer.WriteElementString( "exclusion", i.ToString() );
            }

            writer.WriteEndElement(); // exclusions

            writer.WriteEndElement(); // profile

            writer.Close();
            FilePath = path;
            Program.Logger.Write( LoggerLevel.Info, GlobalStrings.Profile_ProfileSaveComplete );
            return true;
        }

        public static void GenerateDefaultAutoCatSet( List<AutoCat> list ) {
            list.Add( new AutoCatGenre( GlobalStrings.Profile_DefaultAutoCatName_Genre, null, 0, false, null ) );
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
