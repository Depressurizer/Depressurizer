#region LICENSE

//     This file (Location.cs) is part of DepressurizerCore.
//     Copyright (C) 2018  Martijn Vegter
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
//     along with this program.  If not, see <https://www.gnu.org/licenses/>.

#endregion

using System;
using System.IO;

namespace DepressurizerCore.Helpers
{
    /// <summary>
    ///     Location Controller
    /// </summary>
    public static class Location
    {
        /// <summary>
        ///     File Controller
        /// </summary>
        public static class File
        {
            #region Public Properties

            /// <summary>
            ///     Active log file
            /// </summary>
            public static string Log => Path.Combine(Folder.Logs, $"Depressurizer-({DateTime.Now:dd-MM-yyyy}).log");

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