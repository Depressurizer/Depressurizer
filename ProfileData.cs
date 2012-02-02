using System.Collections.Generic;
using System.IO;
using System.Xml;
using System;

namespace Depressurizer {
    public class ProfileData {

        public string FilePath = null;

        public GameData GameData = new GameData();

        public SortedSet<int> ExclusionList = new SortedSet<int>();

        public string AccountID = null;

        public string CommunityName = null;

        public bool AutoDownload = true;

        public bool AutoImport = false;

        public bool AutoExport = true;

        public bool ExportDiscard = true;

        public int ImportSteamData() {
            string filePath = string.Format( @"{0}\userdata\{1}\7\remote\sharedconfig.vdf", DepSettings.Instance().SteamPath, AccountID );
            return GameData.ImportSteamFile( filePath );
        }

        public int DownloadGameList() {
            return GameData.LoadGameList( CommunityName );
        }

        public void ExportSteamData() {
            string filePath = string.Format( @"{0}\userdata\{1}\7\remote\sharedconfig.vdf", DepSettings.Instance().SteamPath, AccountID );
            GameData.SaveSteamFile( filePath, ExportDiscard );
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
                profile.CommunityName = XmlHelper.GetStringFromXmlElement( profileNode, "community_name", null );
                profile.AccountID = XmlHelper.GetStringFromXmlElement( profileNode, "account_id", null );

                profile.AutoDownload = XmlHelper.GetBooleanFromXmlElement( profileNode, "auto_download", profile.AutoDownload );
                profile.AutoImport = XmlHelper.GetBooleanFromXmlElement( profileNode, "auto_import", profile.AutoImport );
                profile.AutoExport = XmlHelper.GetBooleanFromXmlElement( profileNode, "auto_export", profile.AutoExport );
                profile.ExportDiscard = XmlHelper.GetBooleanFromXmlElement( profileNode, "export_discard", profile.ExportDiscard );

                XmlNode gameListNode = profileNode.SelectSingleNode( "games" );
                if( gameListNode != null ) {
                    XmlNodeList gameNodes = gameListNode.SelectNodes( "game" );
                    foreach( XmlNode node in gameNodes ) {
                        AddGameFromXmlNode( node, profile.GameData );
                    }
                }

                XmlNode exclusionListNode = profileNode.SelectSingleNode( "exclusions" );
                if( exclusionListNode != null ) {
                    XmlNodeList exclusionNodes = exclusionListNode.SelectNodes( "exclusion" );
                    foreach( XmlNode node in exclusionNodes ) {
                        int id;
                        if( XmlHelper.GetIntFromXmlElement( node, ".", out id ) ) {
                            profile.ExclusionList.Add( id );
                        }
                    }
                }

            }

            return profile;
        }

        private static void AddGameFromXmlNode( XmlNode node, GameData data ) {
            int id;
            if( XmlHelper.GetIntFromXmlElement( node, "id", out id ) ) {
                string name = XmlHelper.GetStringFromXmlElement( node, "name", null );
                Game game = new Game( id, name );
                data.Games.Add( id, game );

                string catName;
                if( XmlHelper.GetStringFromXmlElement( node, "category", out catName ) ) {
                    game.Category = data.GetCategory( catName );
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
            } catch (Exception e) {
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

            foreach( int i in ExclusionList ) {
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

    public static class XmlHelper {

        public static bool GetStringFromXmlElement( XmlNode refNode, string path, out string val ) {
            if( refNode != null ) {
                XmlNode node = refNode.SelectSingleNode( path + "/text()" );
                if( node != null && !string.IsNullOrEmpty( node.InnerText ) ) {
                    val = node.InnerText;
                    return true;
                }
            }
            val = null;
            return false;
        }

        public static string GetStringFromXmlElement( XmlNode refNode, string path, string defVal ) {
            string val;
            if( GetStringFromXmlElement( refNode, path, out val ) ) {
                return val;
            }
            return defVal;
        }

        public static bool GetIntFromXmlElement( XmlNode refNode, string path, out int val ) {
            string strVal;
            if( GetStringFromXmlElement( refNode, path, out strVal ) ) {
                if( int.TryParse( strVal, out val ) ) {
                    return true;
                }
            }
            val = 0;
            return false;
        }

        public static int GetIntFromXmlElement( XmlNode refNode, string path, int defVal ) {
            int val;
            if( GetIntFromXmlElement( refNode, path, out val ) ) {
                return val;
            }
            return defVal;
        }

        public static bool GetBooleanFromXmlElement( XmlNode refNode, string path, out bool val ) {
            string strVal;
            if( GetStringFromXmlElement( refNode, path, out strVal ) ) {
                if( bool.TryParse( strVal, out val ) ) {
                    return true;
                }
            }
            val = false;
            return false;
        }

        public static bool GetBooleanFromXmlElement( XmlNode refNode, string path, bool defVal ) {
            bool val;
            if( GetBooleanFromXmlElement( refNode, path, out val ) ) {
                return val;
            }
            return defVal;
        }
    }
}
