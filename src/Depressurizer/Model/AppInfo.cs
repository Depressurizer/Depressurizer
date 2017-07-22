/*
    This file is part of Depressurizer.
    Original work Copyright 2011, 2012, 2013 Steve Labbe.
    Modified work Copyright 2017 Martijn Vegter.

    Depressurizer is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Depressurizer is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Depressurizer.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;

namespace Depressurizer.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class AppInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AppTypes AppType { get; set; }
        public AppPlatforms Platforms { get; set; }
        public int Parent { get; set; } // Is 0 if no parent

        public AppInfo(int id, string name = null, AppTypes type = AppTypes.Unknown, AppPlatforms platforms = AppPlatforms.All)
        {
            Id = id;
            Name = name;
            AppType = type;
            Platforms = platforms;
            Parent = 0;
        }

        public static AppInfo Create(VdfFileNode commonNode)
        {
            if ((commonNode == null) || (commonNode.NodeType != ValueType.Array))
            {
                return null;
            }

            AppInfo appInfo = null;

            VdfFileNode idNode = commonNode.GetNodeAt(new[] {"gameid"}, false);
            int id = -1;
            if (idNode != null)
            {
                if (idNode.NodeType == ValueType.Int)
                {
                    id = idNode.NodeInt;
                }
                else if (idNode.NodeType == ValueType.String)
                {
                    if (!int.TryParse(idNode.NodeString, out id))
                    {
                        id = -1;
                    }
                }
            }

            if (id >= 0)
            {
                // Get name
                string name = null;
                VdfFileNode nameNode = commonNode.GetNodeAt(new[] {"name"}, false);
                if (nameNode != null)
                {
                    name = nameNode.NodeData.ToString();
                }

                // Get type
                string typeStr = null;
                AppTypes type = AppTypes.Unknown;
                VdfFileNode typeNode = commonNode.GetNodeAt(new[] {"type"}, false);
                if (typeNode != null)
                {
                    typeStr = typeNode.NodeData.ToString();
                }

                if (typeStr != null)
                {
                    if (!Enum.TryParse(typeStr, true, out type))
                    {
                        type = AppTypes.Other;
                    }
                }

                // Get platforms
                AppPlatforms platforms = AppPlatforms.None;
                VdfFileNode oslistNode = commonNode.GetNodeAt(new[] {"oslist"}, false);
                if (oslistNode != null)
                {
                    string oslist = oslistNode.NodeData.ToString();
                    if (oslist.IndexOf("windows", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        platforms |= AppPlatforms.Windows;
                    }
                    if (oslist.IndexOf("mac", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        platforms |= AppPlatforms.Mac;
                    }
                    if (oslist.IndexOf("linux", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        platforms |= AppPlatforms.Linux;
                    }
                }

                appInfo = new AppInfo(id, name, type, platforms);

                // Get parent
                VdfFileNode parentNode = commonNode.GetNodeAt(new[] {"parent"}, false);
                if (parentNode != null)
                {
                    appInfo.Parent = parentNode.NodeInt;
                }
            }
            return appInfo;
        }
    }
}
