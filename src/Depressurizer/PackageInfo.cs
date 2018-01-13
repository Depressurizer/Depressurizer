#region LICENSE

//     This file (PackageInfo.cs) is part of Depressurizer.
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
using System.IO;
using System.Text;
using DepressurizerCore.Helpers;
using DepressurizerCore.Models;
using ValueType = DepressurizerCore.ValueType;

namespace Depressurizer
{
    internal enum PackageBillingType
    {
        NoCost = 0,

        Store = 1,

        CDKey = 3,

        HardwarePromo = 5,

        Gift = 6,

        AutoGrant = 7,

        StoreOrCDKey = 10,

        FreeOnDemand = 12
    }

    internal class PackageInfo
    {
        #region Constructors and Destructors

        public PackageInfo(int id = 0, string name = null)
        {
            AppIds = new List<int>();
            Id = id;
            Name = name;
        }

        #endregion

        #region Public Properties

        public List<int> AppIds { get; set; }

        public PackageBillingType BillingType { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        #endregion

        #region Public Methods and Operators

        public static PackageInfo FromVdfNode(VDFNode node)
        {
            VDFNode idNode = node.GetNodeAt(new[]
            {
                "packageId"
            }, false);
            if (idNode != null && idNode.NodeType == ValueType.Int)
            {
                int id = idNode.NodeInt;

                string name = null;
                VDFNode nameNode = node.GetNodeAt(new[]
                {
                    "name"
                }, false);
                if (nameNode != null && nameNode.NodeType == ValueType.String)
                {
                    name = nameNode.NodeString;
                }

                PackageInfo package = new PackageInfo(id, name);

                VDFNode billingtypeNode = node["billingtype"];
                if (billingtypeNode != null && billingtypeNode.NodeType == ValueType.String || billingtypeNode.NodeType == ValueType.Int)
                {
                    int bType = billingtypeNode.NodeInt;
                    /*if( Enum.IsDefined( typeof(PackageBillingType), bType ) ) {

                    } else {

                    }*/
                    package.BillingType = (PackageBillingType) bType;
                }

                VDFNode appsNode = node["appids"];
                if (appsNode != null && appsNode.NodeType == ValueType.Array)
                {
                    foreach (VDFNode aNode in appsNode.NodeArray.Values)
                    {
                        if (aNode.NodeType == ValueType.Int)
                        {
                            package.AppIds.Add(aNode.NodeInt);
                        }
                    }
                }

                return package;
            }

            return null;
        }

        public static DateTime GetLocalDateTime(int timeStamp)
        {
            DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            return result.AddSeconds(timeStamp).ToLocalTime();
        }

        /// <summary>
        ///     Loads Apps from packageinfo.vdf.
        /// </summary>
        /// <param name="path">Path of packageinfo.vdf</param>
        public static Dictionary<int, PackageInfo> LoadPackages(string path)
        {
            /* packageinfo.vdf entry example format, sometimes has extra values. Line breaks are only for readability and not part of format.
            * we only care about *packageid*, *billingtype*, *appids*
            * *undeciphered*(24 bytes i haven't deciphered) *changenumber*(4 bytes, little endian) 
            * 00 *packageid*(variable size, big endian, ascii) 00
            * 02 packageid 00 *packageid*(4 bytes, little endian) 
            * 02 billingtype 00 *billingtype*(4 bytes, little endian) 
            * 02 licensetype 00 *licensetype*(4 bytes, little endian) 
            * 02 status 00 00 00 00 00 00 extended 00 
            * 08 00 appids 00 02 *entrynumber*(variable size, number stored as string(ascii), starts at 0, random example: 31 38 39=189) 00 *appid*(4 bytes, little endian) 
            * 08 00 depotids 00 02 *entrynumber* 00 *depotid*(4 bytes, little endian) 02 *entrynumber* 00 *depotid* 02 *entrynumber* 00 *depotid* 
            * 08 00 appitems 00 08 08 08 
            */

            Dictionary<int, PackageInfo> packageInfos = new Dictionary<int, PackageInfo>();

            try
            {
                using (BinaryReader binaryReader = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.ASCII))
                {
                    // seek to packageid: start of a new entry
                    byte[] packageidBytes =
                    {
                        0x00, // 0x00
                        0x02, // 0x02
                        0x70, // p
                        0x61, // a
                        0x63, // c
                        0x6B, // k
                        0x61, // a
                        0x67, // g
                        0x65, // e
                        0x69, // i
                        0x64, // d
                        0x00 // 0x00
                    };

                    byte[] billingtypeBytes =
                    {
                        0x02, // 0x02
                        0x62, // b
                        0x69, // i
                        0x6C, // l
                        0x6C, // l
                        0x69, // i
                        0x6E, // n
                        0x67, // g
                        0x74, // t
                        0x79, // y
                        0x70, // p
                        0x65, // e
                        0x00 // 0x00
                    };

                    byte[] appidsBytes =
                    {
                        0x08, // 0x08
                        0x00, // 0x00
                        0x61, // a
                        0x70, // p
                        0x70, // p
                        0x69, // i
                        0x64, // d
                        0x73, // s
                        0x00 // 0x00
                    };

                    VDFNode.ReadBin_SeekTo(binaryReader, packageidBytes);
                    while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                    {
                        int id = binaryReader.ReadInt32();
                        PackageInfo package = new PackageInfo(id);

                        VDFNode.ReadBin_SeekTo(binaryReader, billingtypeBytes);
                        package.BillingType = (PackageBillingType) binaryReader.ReadInt32();

                        VDFNode.ReadBin_SeekTo(binaryReader, appidsBytes);
                        while (binaryReader.ReadByte() == 0x02)
                        {
                            while (binaryReader.ReadByte() != 0x00) { }

                            package.AppIds.Add(binaryReader.ReadInt32());
                        }

                        packageInfos.Add(package.Id, package);
                        VDFNode.ReadBin_SeekTo(binaryReader, packageidBytes);
                    }
                }
            }
            catch (Exception e)
            {
                SentryLogger.LogException(e);
            }

            return packageInfos;
        }

        #endregion
    }
}