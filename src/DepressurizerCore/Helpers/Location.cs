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
    internal static class Location
    {
        internal static class File
        {
            #region Public Properties

            public static string Settings => Path.Combine(Folder.Depressurizer, "Settings.json");

            #endregion
        }

        internal static class Folder
        {
            #region Public Properties

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

            #endregion
        }
    }
}