#region LICENSE

//     This file (Locations.cs) is part of Depressurizer.
//     Copyright (C) 2018 Martijn Vegter
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Globalization;
using System.IO;

namespace Depressurizer.Helpers
{
    internal class Locations
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

            /// <summary>
            ///     App-Specific Banner File
            /// </summary>
            public static string Banner(int appId)
            {
                return Path.Combine(Folder.Banners, string.Format(CultureInfo.InvariantCulture, "{0}.jpg", appId));
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
