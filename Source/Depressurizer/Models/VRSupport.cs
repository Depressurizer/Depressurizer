#region License

//     This file (VRSupport.cs) is part of Depressurizer.
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

using System.Collections.Generic;

namespace Depressurizer.Models
{
    /// <summary>
    ///     Steam VR Support
    /// </summary>
    public sealed class VRSupport
    {
        private List<string> _headsets;
        private List<string> _input;
        private List<string> _playArea;

        /// <summary>
        ///     Type of Headset
        /// </summary>
        public List<string> Headsets
        {
            get => _headsets ?? (_headsets = new List<string>());
            set => _headsets = value;
        }

        /// <summary>
        ///     Type of Input
        /// </summary>
        public List<string> Input
        {
            get => _input ?? (_input = new List<string>());
            set => _input = value;
        }

        /// <summary>
        ///     Type of PlayArea
        /// </summary>
        public List<string> PlayArea
        {
            get => _playArea ?? (_playArea = new List<string>());
            set => _playArea = value;
        }
    }
}