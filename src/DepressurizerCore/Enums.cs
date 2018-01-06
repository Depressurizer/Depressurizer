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

    public enum StoreLanguage
    {
        windows,

        bg, // Bulgarian

        cs, // Czech

        da, // Danish

        nl, // Dutch

        en, // English

        fi, // Finnish

        fr, // French

        de, // German

        el, // Greek

        hu, // Hungarian

        it, // Italian

        ja, // Japanese

        ko, // Korean

        no, // Norwegian

        pl, // Polish

        pt, // Portuguese

        pt_BR, // Portuguese (Brasil)

        ro, // Romanian

        ru, // Russian

        zh_Hans, // Simplified Chinese

        es, // Spanish

        sv, // Swedish

        th, // Thai

        zh_Hant, // Traditional Chinese

        tr, // Turkish

        uk // Ukrainian
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
    ///     Depressurizer Interface Languages
    /// </summary>
    /// <remarks>
    ///     Format: https://msdn.microsoft.com/en-us/library/ee825488(v=cs.20).aspx
    /// </remarks>
    public enum InterfaceLanguage
    {
        Default,

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