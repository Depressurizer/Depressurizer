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
            StreamWriter writeAllPackageApps = new StreamWriter( "AllPackageApps.txt", false );

            Dictionary<int, PackageInfo> packages = PackageInfo.LoadPackages( packageInfoPath );

            VdfFileNode vdfFile = VdfFileNode.LoadFromText( new StreamReader( localConfigPath ) );

            VdfFileNode licensesNode = vdfFile.GetNodeAt( new string[] {"UserLocalConfigStore","Licenses"}, false );

            List<int> ownedPackageIds = new List<int>();

            foreach( string key in licensesNode.NodeArray.Keys ) {
                int id;
                if( int.TryParse( key, out id ) ) {
                    ownedPackageIds.Add( id );
                } else {
                    writeNonIntPackage.WriteLine( key );
                }
            }

            SortedSet<int> appIds = new SortedSet<int>();
            foreach( int packageId in ownedPackageIds ) {
                if( packageId == 0 ) continue;

                if( packages.ContainsKey( packageId ) ) {
                    PackageInfo package = packages[packageId];
                    if( package.IsExpired ) {
                        continue;
                    }
                    foreach( int appId in package.appIds ) {
                        if( Program.GameDB.Contains( (int)appId ) ) {
                            if( Program.GameDB.Games[(int)appId].Type == AppType.Game ) {
                                appIds.Add( appId );
                                writeAllPackageApps.WriteLine( "App {0} from Package {1}", appId, packageId );
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
            writeAllPackageApps.Close();

            StreamWriter writeInOldNotNew = new StreamWriter( "InOldNotNew.txt", false );
            StreamWriter writeInNewNotOld = new StreamWriter( "InNewNotOld.txt", false );
            StreamWriter writeInBoth = new StreamWriter( "InBoth.txt", false );

            if( list != null ) {
                foreach( GameInfo g in list.Games.Values ) {
                    if( !appIds.Contains( g.Id ) ) {
                        writeInOldNotNew.WriteLine( string.Format("{0}: {1}", g.Id, g.Name ) );
                    } else {
                        writeInBoth.WriteLine( string.Format( "{0}: {1}", g.Id, g.Name ) );
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
            writeInBoth.Close();
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
