using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Rallion;


namespace Depressurizer {
    class TESTER {
        public static void GetLists( GameList list ) {

            string packageInfoPath = @"D:\Steam\appcache\packageinfo.vdf";
            string localConfigPath = @"D:\Steam\userdata\29476466\config\localconfig.vdf";

            StreamWriter writeNonIntPackage = new StreamWriter( "NonIntPackage.txt", false );
            StreamWriter writeInDbNotGame = new StreamWriter( "InDB_NotGame.txt", false );
            StreamWriter writeNotInDB = new StreamWriter( "NotInDB.txt", false );
            StreamWriter writeNoPackageForLicense = new StreamWriter( "NoPackageFound.txt", false );

            PackageInfoCollection pic = new PackageInfoCollection();
            pic.LoadFromFile( packageInfoPath );

            VdfFileNode vdfFile = VdfFileNode.LoadFromText( new StreamReader( localConfigPath ) );

            VdfFileNode licensesNode = vdfFile.GetNodeAt( new string[] {"UserLocalConfigStore","Licenses"}, false );

            List<uint> packageIds = new List<uint>();

            foreach( string key in licensesNode.NodeArray.Keys ) {
                uint id;
                if( uint.TryParse( key, out id ) ) {
                    packageIds.Add( id );
                } else {
                    writeNonIntPackage.WriteLine( key );
                }
            }

            SortedSet<uint> appIds = new SortedSet<uint>();
            foreach( uint packageId in packageIds ) {
                if( pic.packages.ContainsKey( packageId ) ) {
                    PackageInfo package = pic.packages[packageId];
                    foreach( uint appId in package.appIds.Values ) {
                        if( Program.GameDB.Contains( (int)appId ) ) {
                            if( Program.GameDB.Games[(int)appId].Type == AppType.Game ) {
                                appIds.Add( appId );
                            } else {
                                writeInDbNotGame.WriteLine( GetGameString( (int)appId, Program.GameDB ) ); // Game is in database, but isn't a game
                            }
                        } else {
                            writeNotInDB.WriteLine( appId );
                        }
                    }
                } else {
                    writeNoPackageForLicense.WriteLine( string.Format( "Package: {0}", packageId ) );
                }
                
            }

            writeInDbNotGame.Close();
            writeNonIntPackage.Close();
            writeNoPackageForLicense.Close();
            writeNotInDB.Close();

            StreamWriter writeInOldNotNew = new StreamWriter( "InOldNotNew.txt", false );
            StreamWriter writeInNewNotOld = new StreamWriter( "InNewNotOld.txt", false );

            if( list != null ) {
                foreach( GameInfo g in list.Games.Values ) {
                    if( !appIds.Contains( (uint)g.Id ) ) {
                        writeInOldNotNew.WriteLine( string.Format("{0}: {1}", g.Id, g.Name ) );
                    }
                }
                foreach( uint aId in appIds ) {
                    if( !list.Games.ContainsKey((int)aId)) {
                        writeInNewNotOld.WriteLine( GetGameString( (int)aId, Program.GameDB ) );
                    }
                }
            }

            writeInOldNotNew.Close();
            writeInNewNotOld.Close();
        }

        public static void TestNewReader() {
            BinaryReader bReader = new BinaryReader( new FileStream( @"D:\Steam\appcache\packageinfo.vdf", FileMode.Open ) );

            bReader.ReadBytes(38);

            List<VdfFileNode> nodes = new List<VdfFileNode>();
            VdfFileNode node;

            while( (node = VdfFileNode.LoadFromBinary( bReader )) != null ) {
                nodes.Add( node );
                bReader.ReadBytes( 31 );
            }
        }

        public static string GetGameString( int id, GameDB db ) {
            string s = string.Format( "ID: {0} ", id );
            if( db.Contains( id ) ) {
                s += string.Format( " {0} ({1})", db.Games[id].Name, db.Games[id].Type.ToString() );
            } else {
                s += "Not in Database";
            }
            return s;
        }
    }
}
