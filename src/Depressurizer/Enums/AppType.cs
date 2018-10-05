#region LICENSE

//     This file (AppType.cs) is part of Depressurizer.
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

namespace Depressurizer.Enums
{
    /// <summary>
    ///     Steam App Type
    /// </summary>
    public enum AppType
    {
        /// <summary>
        ///     Unknown
        /// </summary>
        Unknown,

        /// <summary>
        ///     Game
        /// </summary>
        Game,

        /// <summary>
        ///     DLC
        /// </summary>
        DLC,

        /// <summary>
        ///     Steam Demo
        /// </summary>
        Demo,

        /// <summary>
        ///     Steam Software
        /// </summary>
        Application,

        /// <summary>
        ///     SDK's, servers etc..
        /// </summary>
        Tool,

        /// <summary>
        ///     Steam Media
        /// </summary>
        Media,

        /// <summary>
        ///     Steam Config File
        /// </summary>
        Config,

        /// <summary>
        ///     Steam Media Content
        /// </summary>
        Series,

        /// <summary>
        ///     Steam Media Content
        /// </summary>
        Video,

        /// <summary>
        ///     Steam Hardware
        /// </summary>
        Hardware,

        /// <summary>
        ///     Player-created manuals and references
        /// </summary>
        Guide,

        /// <summary>
        ///     Mod
        /// </summary>
        Mod
    }
}
