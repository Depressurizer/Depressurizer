#region LICENSE

//     This file (AppInfo.cs) is part of Depressurizer.
//     Original Copyright (C) 2011  Steve Labbe
//     Modified Copyright (C) 2018  Martijn Vegter
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
using System.Collections.Generic;
using System.Data;
using System.IO;
using DepressurizerCore;
using DepressurizerCore.Helpers;
using DepressurizerCore.Models;
using ValueType = DepressurizerCore.ValueType;

namespace Depressurizer
{
    public sealed class AppInfo
    {
        #region Constructors and Destructors

        public AppInfo(int id, string name = null, AppType type = AppType.Unknown, AppPlatforms platforms = AppPlatforms.All)
        {
            Id = id;
            Name = name;
            AppType = type;

            Platforms = platforms;
        }

        #endregion

        #region Public Properties

        public AppType AppType { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Parent { get; set; } = 0; // 0 if none

        public AppPlatforms Platforms { get; set; }

        #endregion

        #region Public Methods and Operators

        public static AppInfo FromVDFNode(VDFNode commonNode)
        {
            if (commonNode == null || commonNode.NodeType != ValueType.Array)
            {
                return null;
            }

            VDFNode idNode = commonNode.GetNodeAt(new[]
            {
                "gameid"
            }, false);
            if (idNode == null)
            {
                return null;
            }

            int id = -1;
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

            if (id < 0)
            {
                return null;
            }

            // Get name
            string name = null;
            VDFNode nameNode = commonNode.GetNodeAt(new[]
            {
                "name"
            }, false);
            if (nameNode != null)
            {
                name = nameNode.NodeData.ToString();
            }

            // Get type
            AppType type = AppType.Unknown;
            VDFNode typeNode = commonNode.GetNodeAt(new[]
            {
                "type"
            }, false);

            if (typeNode != null)
            {
                string typeStr = typeNode.NodeData.ToString();

                if (!Enum.TryParse(typeStr, true, out type))
                {
                    SentryLogger.LogException(new DataException($"Unknown AppType '{typeStr}'"));
                    type = AppType.Unknown;
                }
            }

            // Get platforms
            AppPlatforms platforms = AppPlatforms.None;
            VDFNode oslistNode = commonNode.GetNodeAt(new[]
            {
                "oslist"
            }, false);

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

            AppInfo result = new AppInfo(id, name, type, platforms);

            // Get parent
            VDFNode parentNode = commonNode.GetNodeAt(new[]
            {
                "parent"
            }, false);
            if (parentNode != null)
            {
                result.Parent = parentNode.NodeInt;
            }

            return result;
        }

        public static Dictionary<int, AppInfo> LoadApps(string path)
        {
            Dictionary<int, AppInfo> result = new Dictionary<int, AppInfo>();

            try
            {
                using (BinaryReader binaryReader = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    long streamLength = binaryReader.BaseStream.Length;
                    // Go to the start of a new entry
                    byte[] start =
                    {
                        0x00, // 0x00
                        0x00, // 0x00
                        0x63, // c
                        0x6F, // o
                        0x6D, // m
                        0x6D, // m
                        0x6F, // o
                        0x6E, // n
                        0x00 // 0x00
                    };

                    VDFNode.ReadBin_SeekTo(binaryReader, start, streamLength);

                    VDFNode node = VDFNode.LoadFromBinary(binaryReader, streamLength);
                    while (node != null)
                    {
                        AppInfo appInfo = FromVDFNode(node);
                        if (appInfo != null)
                        {
                            if (appInfo.AppType == AppType.Game || appInfo.AppType == AppType.Application || appInfo.AppType == AppType.Unknown)
                            {
                                result.Add(appInfo.Id, appInfo);
                            }
                        }

                        VDFNode.ReadBin_SeekTo(binaryReader, start, streamLength);
                        node = VDFNode.LoadFromBinary(binaryReader, streamLength);
                    }
                }
            }
            catch (Exception e)
            {
                SentryLogger.LogException(e);
            }

            return result;
        }

        #endregion
    }
}