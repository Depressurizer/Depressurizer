#region LICENSE

//     This file (LanguageSupport.cs) is part of DepressurizerCore.
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

using System.Collections.Generic;

namespace DepressurizerCore.Models
{
    /// <summary>
    ///     Steam LanguageSupport Object
    /// </summary>
    public sealed class LanguageSupport
    {
        #region Fields

        private List<string> _fullAudio;

        private List<string> _interface;

        private List<string> _subtitles;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Supported audio langauges
        /// </summary>
        public List<string> FullAudio
        {
            get => _fullAudio ?? (_fullAudio = new List<string>());
            set => _fullAudio = value;
        }

        /// <summary>
        ///     Supported interface langauges
        /// </summary>
        public List<string> Interface
        {
            get => _interface ?? (_interface = new List<string>());
            set => _interface = value;
        }

        /// <summary>
        ///     Supported subtitles languages
        /// </summary>
        public List<string> Subtitles
        {
            get => _subtitles ?? (_subtitles = new List<string>());
            set => _subtitles = value;
        }

        #endregion
    }
}