using System;
using System.Globalization;
using System.IO;
using static System.IO.File;

namespace Depressurizer.Core.Helpers
{
    public class Locations
    {
        /// <summary>
        ///     File Controller
        /// </summary>
        public static class File
        {
            #region Public Properties

            /// <summary>
            ///     Default database file location.
            /// </summary>
            public static string Database => "database.json";

            /// <summary>
            ///     Default log file location.
            /// </summary>
            public static string Log => Path.Combine(Folder.Logs, string.Format(CultureInfo.InvariantCulture, "depressurizer-({0:dd-MM-yyyy}).log", DateTime.Now));

            /// <summary>
            ///     Default settings file location.
            /// </summary>
            public static string Settings => Path.Combine(Folder.Depressurizer, "settings.json");

            #endregion

            #region Public Methods and Operators

            public static void Backup(string filePath, int maxBackups)
            {
                if (maxBackups < 1)
                {
                    return;
                }

                string targetPath = BackupClearSlot(filePath, maxBackups, 1);
                Copy(filePath, targetPath);
            }

            /// <summary>
            ///     App-Specific Banner File
            /// </summary>
            public static string Banner(int appId)
            {
                return Path.Combine(Folder.Banners, string.Format(CultureInfo.InvariantCulture, "{0}.jpg", appId));
            }

            #endregion

            #region Methods

            private static string BackupClearSlot(string basePath, int maxBackups, int current)
            {
                string thisPath = BackupGetName(basePath, current);
                if (!Exists(thisPath))
                {
                    return thisPath;
                }

                if (current >= maxBackups)
                {
                    Delete(thisPath);
                    return thisPath;
                }

                string moveTarget = BackupClearSlot(basePath, maxBackups, current + 1);
                Move(thisPath, moveTarget);
                return thisPath;
            }

            private static string BackupGetName(string baseName, int slotNum)
            {
                if (slotNum == 0)
                {
                    return baseName;
                }

                return $"{baseName}.bak_{slotNum}";
            }

            #endregion
        }

        /// <summary>
        ///     Folder Controller
        /// </summary>
        public static class Folder
        {
            #region Public Properties

            /// <summary>
            ///     Common application-specific folder
            /// </summary>
            public static string AppData
            {
                get
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    return path;
                }
            }

            /// <summary>
            ///     Depressurizer/Banners Folder
            /// </summary>
            public static string Banners
            {
                get
                {
                    string path = Path.Combine(Depressurizer, "Banners");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    return path;
                }
            }

            /// <summary>
            ///     Depressurizer Folder
            /// </summary>
            public static string Depressurizer
            {
                get
                {
                    string path = Path.Combine(AppData, "Depressurizer");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    return path;
                }
            }

            /// <summary>
            ///     Depressurizer/Logs Folder
            /// </summary>
            public static string Logs
            {
                get
                {
                    string path = Path.Combine(Depressurizer, "Logs");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    return path;
                }
            }

            #endregion
        }
    }
}
