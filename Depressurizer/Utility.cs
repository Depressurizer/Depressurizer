using System.IO;
using System.Collections.Generic;
using System;

namespace Depressurizer {
    public static class Utility {
        public static void BackupFile( string filePath, int maxBackups ) {
            if( maxBackups < 1 ) return;
            string targetPath = BackupFile_ClearSlot( filePath, maxBackups, 1 );
            File.Copy( filePath, targetPath );
        }

        private static string BackupFile_ClearSlot( string basePath, int maxBackups, int current ) {
            string thisPath = BackupFile_GetName( basePath, current );
            if( !File.Exists( thisPath ) ) {
                return thisPath;
            }
            if( current >= maxBackups ) {
                File.Delete( thisPath );
                return thisPath;
            }
            string moveTarget = BackupFile_ClearSlot( basePath, maxBackups, current + 1 );
            File.Move( thisPath, moveTarget );
            return thisPath;
        }

        private static string BackupFile_GetName( string baseName, int slotNum ) {
            if( slotNum == 0 ) return baseName;
            return string.Format( "{0}.bak_{1}", baseName, slotNum );
        }

        static DateTime epoch = new DateTime( 1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc );

        public static int GetCurrentUTime() {
            return GetUTime( DateTime.UtcNow );
        }

        public static int GetUTime( DateTime dt ) {
            double tSecs = ( dt - epoch ).TotalSeconds;
            if( tSecs > int.MaxValue ) return int.MaxValue;
            if( tSecs < 0 ) return 0;
            return (int)tSecs;
        }

        public static DateTime GetDTFromUTime( int uTime ) {
            return epoch.AddSeconds( uTime );
        }

        public static int CompareLists( List<string> a, List<string> b ) {
            if( a == null ) {
                return ( b == null ) ? 0 : 1;
            } else if( b == null ) {
                return -1;
            }
            for( int i = 0; i < a.Count && i < b.Count; i++ ) {
                int res = string.Compare( a[i], b[i] );
                if( res != 0 ) return res;
            }
            return b.Count - a.Count;
        }

        public static void LaunchStorePage( int appId ) {
            System.Diagnostics.Process.Start( string.Format( Properties.Resources.UrlSteamStore, appId ) );
        }
    }
}
