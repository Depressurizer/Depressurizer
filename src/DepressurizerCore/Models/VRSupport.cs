#region LICENSE

//     This file (VRSupport.cs) is part of DepressurizerCore.
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
    ///     Steam VRSupport Object
    /// </summary>
    public sealed class VRSupport
    {
        #region Fields

        private List<string> _headsets;

        private List<string> _input;

        private List<string> _playArea;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Supported VR Headset Name
        /// </summary>
        public List<string> Headsets
        {
            get => _headsets ?? (_headsets = new List<string>());
            set => _headsets = value;
        }

        /// <summary>
        ///     Supported VR Input Type
        /// </summary>
        public List<string> Input
        {
            get => _input ?? (_input = new List<string>());
            set => _input = value;
        }

        /// <summary>
        ///     Type of Play Area
        /// </summary>
        public List<string> PlayArea
        {
            get => _playArea ?? (_playArea = new List<string>());
            set => _playArea = value;
        }

        #endregion
    }
}