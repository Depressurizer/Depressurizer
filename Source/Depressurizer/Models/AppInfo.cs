#region License

//     This file (AppInfo.cs) is part of Depressurizer.
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

namespace Depressurizer.Models
{
    /// <summary>
    ///     Steam AppInfo
    /// </summary>
    public sealed class AppInfo
    {
        /// <summary>
        ///     Steam AppId
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        ///     Steam App Name
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        ///     Steam App Type
        /// </summary>
        public AppTypes AppType { get; set; } = AppTypes.Unknown;

        /// <summary>
        ///     Supported Operating Systems
        /// </summary>
        public AppPlatforms Platforms { get; set; } = AppPlatforms.None;

        /// <summary>
        ///     Equal to Parent Id or 0
        /// </summary>
        public int Parent { get; set; } = 0;

        /// <summary>
        ///     Create an AppInfo object with the default values
        /// </summary>
        public AppInfo() { }

        /// <summary>
        ///     Create an AppInfo object
        /// </summary>
        /// <param name="id">Steam AppId</param>
        /// <param name="name">Steam App Name</param>
        /// <param name="type">Steam App Type</param>
        /// <param name="platforms">Supported Operating Systems</param>
        /// <param name="parent">Parent Id</param>
        public AppInfo(int id, string name = null, AppTypes type = AppTypes.Unknown, AppPlatforms platforms = AppPlatforms.None, int parent = 0)
        {
            Id = id;
            Name = name;
            AppType = type;
            Platforms = platforms;
            Parent = parent;
        }

        /// <summary>
        ///     Create an AppInfo object from VDFNode
        /// </summary>
        /// <param name="node">Node to convert</param>
        /// <returns>AppInfo object or null</returns>
        public static AppInfo FromNode(VdfFileNode node)
        {
            if (node == null || node.NodeType != ValueType.Array)
            {
                return null;
            }

            VdfFileNode idNode = node.GetNodeAt(new[] {"gameid"}, false);
            if (idNode == null)
            {
                return null;
            }

            AppInfo result = new AppInfo();

            switch (idNode.NodeType)
            {
                case ValueType.Int:
                    result.Id = idNode.NodeInt;
                    break;
                case ValueType.String:
                    if (int.TryParse(idNode.NodeString, out int id))
                    {
                        result.Id = id;
                    }
                    break;
            }

            if (result.Id <= 0)
            {
                return null;
            }

            VdfFileNode nameNode = node.GetNodeAt(new[] {"name"}, false);
            if (nameNode != null)
            {
                result.Name = nameNode.NodeData.ToString();
            }

            string typeStr = null;
            VdfFileNode typeNode = node.GetNodeAt(new[] {"type"}, false);
            if (typeNode != null)
            {
                typeStr = typeNode.NodeData.ToString();
            }

            if (typeStr != null)
            {
                if (Enum.TryParse(typeStr, true, out AppTypes type))
                {
                    result.AppType = type;
                }
            }

            VdfFileNode oslistNode = node.GetNodeAt(new[] {"oslist"}, false);
            if (oslistNode != null)
            {
                string oslist = oslistNode.NodeData.ToString();
                if (oslist.IndexOf("windows", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    result.Platforms |= AppPlatforms.Windows;
                }
                if (oslist.IndexOf("mac", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    result.Platforms |= AppPlatforms.Mac;
                }
                if (oslist.IndexOf("linux", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    result.Platforms |= AppPlatforms.Linux;
                }
            }

            VdfFileNode parentNode = node.GetNodeAt(new[] {"parent"}, false);
            if (parentNode != null)
            {
                result.Parent = parentNode.NodeInt;
            }
            return result;
        }
    }
}