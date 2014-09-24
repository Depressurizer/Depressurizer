using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO;

namespace Depressurizer {
    enum PackageBillingType {
        NoCost = 0,
        Store = 1,
        CDKey = 3,
        HardwarePromo = 5,
        Gift = 6,
        AutoGrant = 7,
        StoreOrCDKey = 10,
        FreeOnDemand = 12
    }

    class PackageInfo {

        public List<int> AppIds;
        public int Id;
        public string Name;

        public PackageBillingType BillingType;

        public DateTime ExpiryTime;
        public bool Expires = false;

        public bool IsExpired {
            get {
                return Expires && ( ExpiryTime < DateTime.Now );
            }
        }

        public PackageInfo( int id = 0, string name = null) {
            AppIds = new List<int>();
            this.Id = id;
            this.Name = name;
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

                VdfFileNode billingtypeNode = node["billingtype"];
                if( billingtypeNode != null && billingtypeNode.NodeType == ValueType.String || billingtypeNode.NodeType == ValueType.Int ) {
                    int bType = billingtypeNode.NodeInt;
                    /*if( Enum.IsDefined( typeof(PackageBillingType), bType ) ) {

                    } else {

                    }*/
                    package.BillingType = (PackageBillingType)bType;
                }

                VdfFileNode appsNode = node["appids"];
                if( appsNode != null && appsNode.NodeType == ValueType.Array ) {
                    foreach( VdfFileNode aNode in appsNode.NodeArray.Values ) {
                        if( aNode.NodeType == ValueType.Int ) {
                            package.AppIds.Add( aNode.NodeInt );
                        }
                    }
                }

                VdfFileNode expiryNode = node.GetNodeAt( new string[] {"extended", "ExpiryTime"}, false);
                if( expiryNode != null && expiryNode.NodeType == ValueType.Int ) {
                    package.ExpiryTime = GetLocalDateTime( expiryNode.NodeInt );
                    package.Expires = true;
                } else {
                    package.Expires = false;
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
                    result.Add( p.Id, p );
                }
                bReader.ReadBytes( 31 );
                node = node = VdfFileNode.LoadFromBinary( bReader );
            }

            bReader.Close();

            return result;
        }

    }

}
