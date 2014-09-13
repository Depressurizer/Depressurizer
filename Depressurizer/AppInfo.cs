using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Depressurizer {
    class AppInfo {
        public int appId;
        public string name;
        public string type;

        public AppInfo( int id, string name = null, string type = null ) {
            this.appId = id;
            this.name = name;
            this.type = type;
        }

        public static AppInfo FromVdfNode( VdfFileNode commonNode ) {
            if( commonNode == null || commonNode.NodeType != ValueType.Array ) return null;

            VdfFileNode idNode = commonNode.GetNodeAt( new string[] { "gameid" }, false );
            int id = -1;
            if( idNode != null ) {
                if( idNode.NodeType == ValueType.Int ) {
                    id = idNode.NodeInt;
                } else if( idNode.NodeType == ValueType.String ) {
                    if( !int.TryParse( idNode.NodeString, out id ) ) {
                        id = -1;
                    }
                }
            }
            if( id >= 0 ) {
                string name = null;
                VdfFileNode nameNode = commonNode.GetNodeAt( new string[] { "name" }, false );
                if( nameNode != null ) name = nameNode.NodeData.ToString();

                string type = null;
                VdfFileNode typeNode = commonNode.GetNodeAt( new string[] { "type" }, false );
                if( typeNode != null ) type = typeNode.NodeData.ToString();

                return new AppInfo( id, name, type );
            }
            return null;
        }

        public static Dictionary<int, AppInfo> LoadApps( string path ) {
            Dictionary<int, AppInfo> result = new Dictionary<int, AppInfo>();
            BinaryReader bReader = new BinaryReader( new FileStream( path, FileMode.Open, FileAccess.Read ) );
            long fileLength = bReader.BaseStream.Length;

            byte[] start = new byte[] {0x02, 0x00, 0x63, 0x6F, 0x6D, 0x6D, 0x6F, 0x6E, 0x00};

            VdfFileNode.ReadBin_SeekTo( bReader, start, fileLength );

            VdfFileNode node = VdfFileNode.LoadFromBinary( bReader );
            while( node != null ) {
                AppInfo app = AppInfo.FromVdfNode( node );
                if( app != null ) {
                    result.Add( app.appId, app );
                }
                VdfFileNode.ReadBin_SeekTo( bReader, start, fileLength );    
                node = VdfFileNode.LoadFromBinary( bReader );
            }
            bReader.Close();
            return result;
        }
    }
}
