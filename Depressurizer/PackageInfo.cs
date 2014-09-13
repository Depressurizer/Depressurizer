using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO;

namespace Depressurizer {
    

    class PackageInfo {

        public List<int> appIds;
        public int packageId;
        public string name;

        public DateTime expiryTime;
        public bool expires = false;

        public bool IsExpired {
            get {
                return expires && ( expiryTime < DateTime.Now );
            }
        }

        public PackageInfo( int id = 0, string name = null) {
            appIds = new List<int>();
            this.packageId = id;
            this.name = name;
        }

        public static PackageInfo FromVdfNode( VdfFileNode node ) {
            VdfFileNode idNode = node.GetNodeAt( new string[] { "packageId" }, false );
            if( (idNode != null ) && idNode.NodeType == ValueType.Int ) {
                int id = idNode.NodeInt;

                string name = null;
                VdfFileNode nameNode =  node.GetNodeAt( new string[] {"name"}, false );
                if( nameNode != null && nameNode.NodeType == ValueType.String ) {
                    name = nameNode.NodeString;
                }

                PackageInfo package = new PackageInfo(id, name);

                VdfFileNode appsNode = node["appids"];
                if( appsNode.NodeType == ValueType.Array ) {
                    foreach( VdfFileNode aNode in appsNode.NodeArray.Values ) {
                        if( aNode.NodeType == ValueType.Int ) {
                            package.appIds.Add( aNode.NodeInt );
                        }
                    }
                }

                VdfFileNode expiryNode = node.GetNodeAt( new string[] {"extended", "ExpiryTime"}, false);
                if( expiryNode != null && expiryNode.NodeType == ValueType.Int ) {
                    package.expiryTime = GetLocalDateTime( expiryNode.NodeInt );
                    package.expires = true;
                } else {
                    package.expires = false;
                }

                return package;

            }
            return null;
        }

        public static DateTime GetLocalDateTime( int timeStamp ) {
            DateTime result = new DateTime( 1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc );
            return result.AddSeconds( timeStamp ).ToLocalTime();
        }

        public static Dictionary<int, PackageInfo> LoadPackages( string path ) {
            Dictionary<int, PackageInfo> result = new Dictionary<int, PackageInfo>();

            BinaryReader bReader = new BinaryReader( new FileStream( path, FileMode.Open  ), Encoding.ASCII );

            bReader.ReadBytes( 38 );

            VdfFileNode node = VdfFileNode.LoadFromBinary( bReader );

            while( node != null ) {
                PackageInfo p = FromVdfNode( node );
                if( p != null ) {
                    result.Add( p.packageId, p );
                }
                bReader.ReadBytes( 31 );
                node = node = VdfFileNode.LoadFromBinary( bReader );
            }

            bReader.Close();

            return result;
        }

    }

}
