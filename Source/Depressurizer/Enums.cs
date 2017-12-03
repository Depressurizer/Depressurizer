#region License

//     This file (Enums.cs) is part of Depressurizer.
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

namespace Depressurizer
{
    /// <summary>
    ///     Log Level
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        ///     Default Value
        /// </summary>
        Invalid = 0,

        /// <summary>
        ///     Verbose messages (enter/exit subroutine, buffer contents, etc.)
        /// </summary>
        Verbose = 1,

        /// <summary>
        ///     Debug messages, to help in diagnosing a problem
        /// </summary>
        Debug = 2,

        /// <summary>
        ///     Informational messages, showing completion, progress, etc.
        /// </summary>
        Info = 3,

        /// <summary>
        ///     Warning error messages which do not cause a functional failure
        /// </summary>
        Warn = 4,

        /// <summary>
        ///     Major error messages, some lost functionality
        /// </summary>
        Error = 5,

        /// <summary>
        ///     Critical error messages, aborts the subsystem
        /// </summary>
        Fatal = 6
    }

    /// <summary>
    ///     Operating System(s)
    /// </summary>
    [Flags]
    public enum AppPlatforms
    {
        /// <summary>
        ///     Default value
        /// </summary>
        None = 0,

        /// <summary>
        ///     Microsoft Windows
        /// </summary>
        Windows = 1,

        /// <summary>
        ///     macOS
        /// </summary>
        Mac = 2,

        /// <summary>
        ///     Linux
        /// </summary>
        Linux = 4,

        /// <summary>
        ///     Windows, Mac and Linux
        /// </summary>
        All = Windows | Mac | Linux
    }

    /// <summary>
    ///     Steam App Type
    /// </summary>
    [Flags]
    public enum AppTypes
    {
        /// <summary>
        ///     Default value
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     Steam Game
        /// </summary>
        Game = 1 << 0,

        /// <summary>
        ///     Steam Software
        /// </summary>
        Application = 1 << 1,

        /// <summary>
        ///     Steam Demo
        /// </summary>
        Demo = 1 << 2,

        /// <summary>
        ///     Downloadable Content
        /// </summary>
        DLC = 1 << 3,

        /// <summary>
        ///     SDK's, servers etc..
        /// </summary>
        Tool = 1 << 4,

        /// <summary>
        ///     Steam Config Files
        /// </summary>
        Config = 1 << 5,

        /// <summary>
        ///     Steam Streaming Videos
        /// </summary>
        Video = 1 << 6,

        /// <summary>
        ///     Steam Series
        /// </summary>
        Series = 1 << 7,

        /// <summary>
        ///     All Steam Games and Software
        /// </summary>
        IncludeNormal = Application | Game,

        /// <summary>
        ///     All Steam Games, Software and Unknown
        /// </summary>
        IncludeUnknown = IncludeNormal | Unknown,

        /// <summary>
        ///     All Steam Apps
        /// </summary>
        IncludeAll = (1 << 8) - 1
    }
}