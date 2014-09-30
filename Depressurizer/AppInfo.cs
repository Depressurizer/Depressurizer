using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Depressurizer {
    [Flags]
    public enum AppPlatforms {
        None = 0,
        Windows = 1,
        Mac = 1 << 1,
        Linux = 1 << 2,
        All = Windows | Mac | Linux
    }

    [Flags]
    public enum AppTypes {
        Application = 1,
        Demo = 1 << 1,
        DLC = 1 << 2,
        Game = 1 << 3,
        Media = 1 << 4,
        Tool = 1 << 5,
        Other = 1 << 6,
        Unknown = 1 << 7,
        InclusionNormal = Application | Game,
        InclusionUnknown = InclusionNormal | Unknown,
        InclusionAll = 1 << 8 - 1
    }

    class AppInfo {
        public int Id;
        public string Name;
        public AppTypes AppType;
        public AppPlatforms Platforms;
        public int Parent; // 0 if none

        public AppInfo( int id, string name = null, AppTypes type = AppTypes.Unknown, AppPlatforms platforms = AppPlatforms.All ) {
            this.Id = id;
            this.Name = name;
            this.AppType = type;

            this.Platforms = platforms;
        }

        public static AppInfo FromVdfNode( VdfFileNode commonNode ) {
            if( commonNode == null || commonNode.NodeType != ValueType.Array ) return null;

            AppInfo result = null;

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
                // Get name
                string name = null;
                VdfFileNode nameNode = commonNode.GetNodeAt( new string[] { "name" }, false );
                if( nameNode != null ) name = nameNode.NodeData.ToString();

                // Get type
                string typeStr = null;
                AppTypes type = AppTypes.Unknown;
                VdfFileNode typeNode = commonNode.GetNodeAt( new string[] { "type" }, false );
                if( typeNode != null ) typeStr = typeNode.NodeData.ToString();

                if( typeStr != null ) {
                    if( !Enum.TryParse<AppTypes>( typeStr, true, out type ) ) {
                        type = AppTypes.Other;
                    }
                }

                // Get platforms
                string oslist = null;
                AppPlatforms platforms = AppPlatforms.None;
                VdfFileNode oslistNode = commonNode.GetNodeAt( new string[] { "oslist" }, false );
                if( oslistNode != null ) {
                    oslist = oslistNode.NodeData.ToString();
                    if( oslist.IndexOf( "windows", StringComparison.OrdinalIgnoreCase ) != -1 ) {
                        platforms |= AppPlatforms.Windows;
                    }
                    if( oslist.IndexOf( "mac", StringComparison.OrdinalIgnoreCase ) != -1 ) {
                        platforms |= AppPlatforms.Mac;
                    }
                    if( oslist.IndexOf( "linux", StringComparison.OrdinalIgnoreCase ) != -1 ) {
                        platforms |= AppPlatforms.Linux;
                    }
                }

                result = new AppInfo( id, name, type, platforms );

                // Get parent
                VdfFileNode parentNode = commonNode.GetNodeAt( new string[] { "parent" }, false );
                if( parentNode != null ) {
                    result.Parent = parentNode.NodeInt;
                }

            }
            return result;
        }

        public static Dictionary<int, AppInfo> LoadApps( string path ) {
            Dictionary<int, AppInfo> result = new Dictionary<int, AppInfo>();
            BinaryReader bReader = new BinaryReader( new FileStream( path, FileMode.Open, FileAccess.Read ) );
            long fileLength = bReader.BaseStream.Length;

            // seek to common: start of a new entry
            byte[] start = new byte[] {0x02, 0x00, 0x63, 0x6F, 0x6D, 0x6D, 0x6F, 0x6E, 0x00}; // 0x02 0x00 c o m m o n 0x00

            VdfFileNode.ReadBin_SeekTo( bReader, start, fileLength );

            VdfFileNode node = VdfFileNode.LoadFromBinary( bReader, fileLength );
            while( node != null ) {
                AppInfo app = AppInfo.FromVdfNode( node );
                if( app != null ) {
                    result.Add( app.Id, app );
                }
                VdfFileNode.ReadBin_SeekTo( bReader, start, fileLength );    
                node = VdfFileNode.LoadFromBinary( bReader, fileLength );
            }
            bReader.Close();
            return result;
        }
    }
}
