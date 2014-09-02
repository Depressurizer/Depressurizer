using System.IO;

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
    }
}
