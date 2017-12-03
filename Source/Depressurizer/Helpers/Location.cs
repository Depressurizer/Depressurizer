#region License

//     This file (Location.cs) is part of Depressurizer.
//     Copyright (C) 2017  Martijn Vegter
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
using System.Globalization;
using System.IO;

namespace Depressurizer.Helpers
{
    /// <summary>
    ///     Depressurizer File/Folder Location
    /// </summary>
    public static class Location
    {
        internal static class File
        {
            public static string ActiveLogFile
            {
                get
                {
                    string date = DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                    return string.Format(CultureInfo.InvariantCulture, "Depressurizer-({0}).log", date);
                }
            }
        }

        internal static class Folder
        {
            public static string AppData => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

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
        }
    }
}