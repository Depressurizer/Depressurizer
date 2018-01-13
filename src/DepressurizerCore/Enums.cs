#region LICENSE

//     This file (Enums.cs) is part of DepressurizerCore.
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

namespace DepressurizerCore
{
    public enum ValueType
    {
        Array,

        String,

        Int
    }

    public enum GameListSource
    {
        XmlPreferred,

        XmlOnly,

        WebsiteOnly
    }

    /// <summary>
    ///     Languages supported on Steam.
    /// </summary>
    /// <remarks>
    ///     https://partner.steamgames.com/doc/store/localization
    ///     https://msdn.microsoft.com/en-us/library/ee825488(v=cs.20).aspx
    /// </remarks>
    public enum StoreLanguage
    {
        /// <summary>
        ///     Equal to the Depressurizer Interface language or English
        /// </summary>
        Default,

        Arabic,

        /// <summary>
        ///     Bulgarian - Bulgaria
        /// </summary>
        Bulgarian,

        // TODO: Chinese (Simplified)

        // TODO: Chinese (Traditional)

        /// <summary>
        ///     Czech - Czech Republic
        /// </summary>
        Czech,

        /// <summary>
        ///     Danish - Denmark
        /// </summary>
        Danish,

        /// <summary>
        ///     Dutch - The Netherlands
        /// </summary>
        Dutch,

        /// <summary>
        ///     English - United States
        /// </summary>
        English,

        /// <summary>
        ///     Finnish - Finland
        /// </summary>
        Finnish,

        /// <summary>
        ///     French - France
        /// </summary>
        French,

        /// <summary>
        ///     German - Germany
        /// </summary>
        German,

        /// <summary>
        ///     Greek - Greece
        /// </summary>
        Greek,

        /// <summary>
        ///     Hungarian - Hungary
        /// </summary>
        Hungarian,

        /// <summary>
        ///     Italian - Italy
        /// </summary>
        Italian,

        /// <summary>
        ///     Japanese - Japan
        /// </summary>
        Japanese,

        /// <summary>
        ///     Korean - Korea
        /// </summary>
        Korean,

        Norwegian,

        /// <summary>
        ///     Polish - Poland
        /// </summary>
        Polish,

        /// <summary>
        ///     Portuguese - Portugal
        /// </summary>
        Portuguese,

        // TODO: Portuguese-Brazil

        /// <summary>
        ///     Romanian - Romania
        /// </summary>
        Romanian,

        /// <summary>
        ///     Russian - Russia
        /// </summary>
        Russian,

        /// <summary>
        ///     Spanish - Spain
        /// </summary>
        Spanish,

        /// <summary>
        ///     Swedish - Sweden
        /// </summary>
        Swedish,

        /// <summary>
        ///     Thai - Thailand
        /// </summary>
        Thai,

        /// <summary>
        ///     Turkish - Turkey
        /// </summary>
        Turkish,

        /// <summary>
        ///     Ukrainian - Ukraine
        /// </summary>
        Ukrainian
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
        Video
    }

    /// <summary>
    ///     Action on startup
    /// </summary>
    public enum StartupAction
    {
        /// <summary>
        ///     Do nothing
        /// </summary>
        None = 0,

        /// <summary>
        ///     Load a profile
        /// </summary>
        Load,

        /// <summary>
        ///     Create a profile
        /// </summary>
        Create
    }

    /// <summary>
    ///     Level to log at
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        ///     Verbose messages (enter/exit subroutine, buffer contents, etc.)
        /// </summary>
        Verbose = 0,

        /// <summary>
        ///     Debug messages, to help in diagnosing a problem
        /// </summary>
        Debug = 1,

        /// <summary>
        ///     Informational messages, showing completion, progress, etc.
        /// </summary>
        Info = 2,

        /// <summary>
        ///     Warning error messages which do not cause a functional failure
        /// </summary>
        Warn = 3,

        /// <summary>
        ///     Major error messages, some lost functionality
        /// </summary>
        Error = 4,

        /// <summary>
        ///     Critical error messages, aborts the subsystem
        /// </summary>
        Fatal = 5
    }

    /// <summary>
    ///     Depressurizer Interface Languages
    /// </summary>
    /// <remarks>
    ///     Format: https://msdn.microsoft.com/en-us/library/ee825488(v=cs.20).aspx
    /// </remarks>
    public enum InterfaceLanguage
    {
        /// <summary>
        ///     English - United States
        /// </summary>
        English,

        /// <summary>
        ///     Spanish - Spain
        /// </summary>
        Spanish,

        /// <summary>
        ///     Russian - Russia
        /// </summary>
        Russian,

        /// <summary>
        ///     Ukrainian - Ukraine
        /// </summary>
        Ukranian,

        /// <summary>
        ///     Dutch - The Netherlands
        /// </summary>
        Dutch
    }
}