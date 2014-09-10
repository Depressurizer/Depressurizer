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
        // Current Xml names
        private const string
            XmlName_Profile = "profile",
            XmlName_Version = "version",
            XmlName_SteamID = "steam_id_64",
            XmlName_AutoDownload = "auto_download",
            XmlName_AutoImport = "auto_import",
            XmlName_AutoExport = "auto_export",
            XmlName_ExportDiscard = "export_discard",
            XmlName_AutoIgnore = "auto_ignore",
            XmlName_OverwriteNames = "overwrite_names",
            XmlName_IgnoreDlc = "ignore_dlc",
            XmlName_IncludeShortcuts = "include_shortcuts",
            XmlName_ExclusionList = "exclusions",
            XmlName_Exclusion = "exclusion",
            XmlName_GameList = "games",
            XmlName_Game = "game",
            XmlName_AutoCatList = "autocats",
            XmlName_Game_Id = "id",
            XmlName_Game_Name = "name",
            XmlName_Game_Hidden = "hidden",
            XmlName_Game_CategoryList = "categories",
            XmlName_Game_Category = "category";

        // Old Xml names
        private const string XmlName_Old_SteamIDShort = "account_id",
            XmlName_Old_IgnoreExternal = "ignore_external",
            XmlName_Old_Game_Favorite = "favorite";

        public const int VERSION = 2;

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

        public bool IncludeShortcuts = true;

        public int ImportSteamData() {
            return GameData.ImportSteamConfig( SteamID64, IgnoreList, IgnoreDlc, IncludeShortcuts );
        }

        public void ExportSteamData() {
            GameData.ExportSteamConfig( SteamID64, ExportDiscard, IncludeShortcuts );
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

            XmlNode profileNode = doc.SelectSingleNode( "/" + XmlName_Profile );

            if( profileNode != null ) {
                // Get the profile version that we're loading
                XmlAttribute versionAttr = profileNode.Attributes[XmlName_Version];
                int profileVersion = 0;
                if( versionAttr != null ) {
                    if( !int.TryParse( versionAttr.Value, out profileVersion ) ) {
                        profileVersion = 0;
                    }
                }
                // Get the 64-bit Steam ID
                Int64 accId = XmlUtil.GetInt64FromNode( profileNode[XmlName_SteamID], 0 );
                if( accId == 0 ) {
                    string oldAcc = XmlUtil.GetStringFromNode( profileNode[XmlName_Old_SteamIDShort], null );
                    if( oldAcc != null ) {
                        accId = DirNametoID64( oldAcc );
                    }
                }
                profile.SteamID64 = accId;
                // Get other attributes
                profile.AutoDownload = XmlUtil.GetBoolFromNode( profileNode[XmlName_AutoDownload], profile.AutoDownload );
                profile.AutoImport = XmlUtil.GetBoolFromNode( profileNode[XmlName_AutoImport], profile.AutoImport );
                profile.AutoExport = XmlUtil.GetBoolFromNode( profileNode[XmlName_AutoExport], profile.AutoExport );
                profile.ExportDiscard = XmlUtil.GetBoolFromNode( profileNode[XmlName_ExportDiscard], profile.ExportDiscard );
                profile.AutoIgnore = XmlUtil.GetBoolFromNode( profileNode[XmlName_AutoIgnore], profile.AutoIgnore );
                profile.OverwriteOnDownload = XmlUtil.GetBoolFromNode( profileNode[XmlName_OverwriteNames], profile.OverwriteOnDownload );
                profile.IgnoreDlc = XmlUtil.GetBoolFromNode( profileNode[XmlName_IgnoreDlc], profile.IgnoreDlc );

                if( profileVersion < 2 ) {
                    bool ignoreShortcuts = false;
                    if( XmlUtil.TryGetBoolFromNode( profileNode[XmlName_Old_IgnoreExternal], out ignoreShortcuts ) ) {
                        profile.IncludeShortcuts = !ignoreShortcuts;
                    } 
                } else {
                    profile.IncludeShortcuts = XmlUtil.GetBoolFromNode( profileNode[XmlName_IncludeShortcuts], profile.IncludeShortcuts );
                }

                XmlNode exclusionListNode = profileNode.SelectSingleNode( XmlName_ExclusionList );
                if( exclusionListNode != null ) {
                    XmlNodeList exclusionNodes = exclusionListNode.SelectNodes( XmlName_Exclusion );
                    foreach( XmlNode node in exclusionNodes ) {
                        int id;
                        if( XmlUtil.TryGetIntFromNode( node, out id ) ) {
                            profile.IgnoreList.Add( id );
                        }
                    }
                }

                XmlNode gameListNode = profileNode.SelectSingleNode( XmlName_GameList );
                if( gameListNode != null ) {
                    XmlNodeList gameNodes = gameListNode.SelectNodes( XmlName_Game );
                    foreach( XmlNode node in gameNodes ) {
                        AddGameFromXmlNode( node, profile, profileVersion );
                    }
                }

                XmlNode autocatListNode = profileNode.SelectSingleNode( XmlName_AutoCatList );
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
            if( XmlUtil.TryGetIntFromNode( node[XmlName_Game_Id], out id ) ) {
                if( profile.IgnoreList.Contains( id ) || ( profile.IgnoreDlc && Program.GameDB.IsDlc( id ) ) ) {
                    return;
                }
                string name = XmlUtil.GetStringFromNode( node[XmlName_Game_Name], null );
                GameInfo game = new GameInfo( id, name );
                profile.GameData.Games.Add( id, game );

                game.Hidden = XmlUtil.GetBoolFromNode( node[XmlName_Game_Hidden], false );

                if( profileVersion < 1 ) {
                    string catName;
                    if( XmlUtil.TryGetStringFromNode( node[XmlName_Game_Category], out catName ) ) {
                        game.AddCategory( profile.GameData.GetCategory( catName ) );
                    }
                    if( ( node.SelectSingleNode( XmlName_Old_Game_Favorite ) != null ) ) {
                        game.AddCategory( profile.GameData.FavoriteCategory );
                    }
                } else {
                    XmlNode catListNode = node.SelectSingleNode( XmlName_Game_CategoryList );
                    if( catListNode != null ) {
                        XmlNodeList catNodes = catListNode.SelectNodes( XmlName_Game_Category );
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
            writer.WriteStartElement( XmlName_Profile );

            writer.WriteAttributeString( XmlName_Version, VERSION.ToString() );

            writer.WriteElementString( XmlName_SteamID, SteamID64.ToString() );

            writer.WriteElementString( XmlName_AutoDownload, AutoDownload.ToString() );
            writer.WriteElementString( XmlName_AutoImport, AutoImport.ToString() );
            writer.WriteElementString( XmlName_AutoExport, AutoExport.ToString() );
            writer.WriteElementString( XmlName_ExportDiscard, ExportDiscard.ToString() );
            writer.WriteElementString( XmlName_AutoIgnore, AutoIgnore.ToString() );
            writer.WriteElementString( XmlName_OverwriteNames, OverwriteOnDownload.ToString() );
            writer.WriteElementString( XmlName_IgnoreDlc, IgnoreDlc.ToString() );
            writer.WriteElementString( XmlName_IncludeShortcuts, IncludeShortcuts.ToString() );

            writer.WriteStartElement( XmlName_GameList );

            foreach( GameInfo g in GameData.Games.Values ) {
                writer.WriteStartElement( XmlName_Game );

                writer.WriteElementString( XmlName_Game_Id, g.Id.ToString() );

                if( g.Name != null ) {
                    writer.WriteElementString( XmlName_Game_Name, g.Name );
                }

                writer.WriteElementString( XmlName_Game_Hidden, g.Hidden.ToString() );

                writer.WriteStartElement( XmlName_Game_CategoryList );
                foreach( Category c in g.Categories ) {
                    writer.WriteElementString( XmlName_Game_Category, c.Name );
                }
                writer.WriteEndElement(); // categories

                writer.WriteEndElement(); // game
            }

            writer.WriteEndElement(); // games

            writer.WriteStartElement( XmlName_AutoCatList );

            foreach( AutoCat autocat in AutoCats ) {
                autocat.WriteToXml( writer );
            }

            writer.WriteEndElement(); //autocats

            writer.WriteStartElement( XmlName_ExclusionList );

            foreach( int i in IgnoreList ) {
                writer.WriteElementString( XmlName_Exclusion, i.ToString() );
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
