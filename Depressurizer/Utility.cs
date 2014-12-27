/*
This file is part of Depressurizer.
Copyright 2011, 2012, 2013 Steve Labbe.

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
using System.IO;
using System.Collections.Generic;
using System;

namespace Depressurizer {
    public static class Utility {

        #region File Backups
        /// <summary>
        /// Moves a file to back it up in anticipation of a save. Maintains a certain number of old versions of the file.
        /// </summary>
        /// <param name="filePath">File to move</param>
        /// <param name="maxBackups">The number of old versions to maintain</param>
        public static void BackupFile( string filePath, int maxBackups ) {
            if( maxBackups < 1 ) return;
            string targetPath = BackupFile_ClearSlot( filePath, maxBackups, 1 );
            File.Copy( filePath, targetPath );
        }

        /// <summary>
        /// Clears a slot for a file to be moved into as part of a backup.
        /// </summary>
        /// <param name="basePath">Path of the main file that's being backed up.</param>
        /// <param name="maxBackups">The number of backups that we're looking to keep</param>
        /// <param name="current">The number of the backup file to process. For example, if 1, this is clearing a spot for the most recent backup.</param>
        /// <returns>The path of the cleared slot</returns>
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

        /// <summary>
        /// Gets the name for a certain backup slot.
        /// </summary>
        /// <param name="baseName">Name of the current version of the file</param>
        /// <param name="slotNum">Slot number to get the name for</param>
        /// <returns>The name</returns>
        private static string BackupFile_GetName( string baseName, int slotNum ) {
            if( slotNum == 0 ) return baseName;
            return string.Format( "{0}.bak_{1}", baseName, slotNum );
        }
        #endregion

        #region Date and time
        /// <summary>
        /// Unix epoch
        /// </summary>
        static DateTime epoch = new DateTime( 1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc );

        /// <summary>
        /// Gets the current time as Unix timestamp
        /// </summary>
        /// <returns>Int containing Unix time</returns>
        public static int GetCurrentUTime() {
            return GetUTime( DateTime.UtcNow );
        }

        /// <summary>
        /// Converts a given DateTime to unix time
        /// </summary>
        /// <param name="dt">DateTime to convert</param>
        /// <returns>int containing unix time</returns>
        public static int GetUTime( DateTime dt ) {
            double tSecs = ( dt - epoch ).TotalSeconds;
            if( tSecs > int.MaxValue ) return int.MaxValue;
            if( tSecs < 0 ) return 0;
            return (int)tSecs;
        }

        /// <summary>
        /// Converts unix time to a DateTime object
        /// </summary>
        /// <param name="uTime">Unix time to convert</param>
        /// <returns>DateTime representation</returns>
        public static DateTime GetDTFromUTime( int uTime ) {
            return epoch.AddSeconds( uTime );
        }
        #endregion

        #region General
        /// <summary>
        /// Compares two lists of strings for equality / sorting purposes.
        /// </summary>
        /// <param name="a">First list</param>
        /// <param name="b">Second list</param>
        /// <returns>0 if equal, negative if a is greater, positive if b is greater</returns>
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

        /// <summary>
        /// Clamp the value of an item to be between two values.
        /// </summary>
        /// <typeparam name="T">Type of the clamped object</typeparam>
        /// <param name="val">Value to clamp</param>
        /// <param name="min">Minimum return value</param>
        /// <param name="max">Maximum return value</param>
        /// <returns>If val is between min and max, return val. If greater than max, return max. If less than min, return min.</returns>
        public static T Clamp<T>( T val, T min, T max ) where T : IComparable<T> {
            if( val.CompareTo( min ) < 0 ) return min;
            if( val.CompareTo( max ) > 0 ) return max;
            return val;
        }
        #endregion

        #region Steam-specific
        /// <summary>
        /// Opens the store page for the specified app in the default browser.
        /// </summary>
        /// <param name="appId"></param>
        public static void LaunchStorePage( int appId ) {
            System.Diagnostics.Process.Start( string.Format( Properties.Resources.UrlSteamStore, appId ) );
        }
        #endregion
    }
}
