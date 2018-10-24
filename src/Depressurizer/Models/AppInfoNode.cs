#region LICENSE

//     This file (AppInfoNode.cs) is part of Depressurizer.
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

using System.Collections.Generic;

namespace Depressurizer.Models
{
    /// <summary>
    ///     AppInfo Node
    /// </summary>
    public sealed class AppInfoNode
    {
        #region Constructors and Destructors

        public AppInfoNode() { }

        public AppInfoNode(string value)
        {
            Value = value;
        }

        #endregion

        #region Public Properties

        public Dictionary<string, AppInfoNode> Items { get; set; } = new Dictionary<string, AppInfoNode>();

        public string Value { get; set; }

        #endregion

        #region Public Indexers

        public AppInfoNode this[string index]
        {
            get => Items[index];
            set => Items[index] = value;
        }

        #endregion
    }
}
