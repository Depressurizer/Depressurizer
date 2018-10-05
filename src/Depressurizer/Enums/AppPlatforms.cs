#region LICENSE

//     This file (AppPlatforms.cs) is part of Depressurizer.
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

namespace Depressurizer.Enums
{
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
        Windows = 1 << 0,

        /// <summary>
        ///     macOS
        /// </summary>
        Mac = 1 << 1,

        /// <summary>
        ///     Linux
        /// </summary>
        Linux = 1 << 2,

        /// <summary>
        ///     Windows, Mac and Linux
        /// </summary>
        All = (1 << 3) - 1
    }
}
