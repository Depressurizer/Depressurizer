/*
This file is part of Depressurizer.
Copyright 2011, 2012, 2013 Steve Labbe.

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
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Depressurizer
{
    enum PackageBillingType
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

    class PackageInfo
    {
        public List<int> AppIds;
        public int Id;
        public string Name;

        public PackageBillingType BillingType;

        public PackageInfo(int id = 0, string name = null)
        {
            AppIds = new List<int>();
            Id = id;
            Name = name;
        }

        public static PackageInfo FromVdfNode(VdfFileNode node)
        {
            VdfFileNode idNode = node.GetNodeAt(new[] {"packageId"}, false);
            if ((idNode != null) && (idNode.NodeType == ValueType.Int))
            {
                int id = idNode.NodeInt;

                string name = null;
                VdfFileNode nameNode = node.GetNodeAt(new[] {"name"}, false);
                if ((nameNode != null) && (nameNode.NodeType == ValueType.String))
                {
                    name = nameNode.NodeString;
                }

                PackageInfo package = new PackageInfo(id, name);

                VdfFileNode billingtypeNode = node["billingtype"];
                if (((billingtypeNode != null) && (billingtypeNode.NodeType == ValueType.String)) ||
                    (billingtypeNode.NodeType == ValueType.Int))
                {
                    int bType = billingtypeNode.NodeInt;
                    /*if( Enum.IsDefined( typeof(PackageBillingType), bType ) ) {

                    } else {

                    }*/
                    package.BillingType = (PackageBillingType) bType;
                }

                VdfFileNode appsNode = node["appids"];
                if ((appsNode != null) && (appsNode.NodeType == ValueType.Array))
                {
                    foreach (VdfFileNode aNode in appsNode.NodeArray.Values)
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
        /// Loads Apps from packageinfo.vdf.
        /// </summary>
        /// <param name="path">Path of packageinfo.vdf</param>
        public static Dictionary<int, PackageInfo> LoadPackages(string path)
        {
            Dictionary<int, PackageInfo> result = new Dictionary<int, PackageInfo>();

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

            BinaryReader bReader = new BinaryReader(new FileStream(path, FileMode.Open), Encoding.ASCII);
            long fileLength = bReader.BaseStream.Length;

            // seek to packageid: start of a new entry
            byte[] packageidBytes =
            {
                0x00, 0x02, 0x70, 0x61, 0x63, 0x6B, 0x61, 0x67, 0x65, 0x69, 0x64, 0x00
            }; // 0x00 0x02 p a c k a g e i d 0x00
            byte[] billingtypeBytes =
            {
                0x02, 0x62, 0x69, 0x6C, 0x6C, 0x69, 0x6E, 0x67, 0x74, 0x79, 0x70, 0x65, 0x00
            }; // 0x02 b i l l i n g t y p e 0x00
            byte[] appidsBytes = {0x08, 0x00, 0x61, 0x70, 0x70, 0x69, 0x64, 0x73, 0x00}; // 0x08 0x00 appids 0x00

            VdfFileNode.ReadBin_SeekTo(bReader, packageidBytes, fileLength);
            while (bReader.BaseStream.Position < fileLength)
            {
                int id = bReader.ReadInt32();
                PackageInfo package = new PackageInfo(id);

                VdfFileNode.ReadBin_SeekTo(bReader, billingtypeBytes, fileLength);
                package.BillingType = (PackageBillingType) bReader.ReadInt32();

                VdfFileNode.ReadBin_SeekTo(bReader, appidsBytes, fileLength);
                while (bReader.ReadByte() == 0x02)
                {
                    while (bReader.ReadByte() != 0x00) { }
                    package.AppIds.Add(bReader.ReadInt32());
                }

                result.Add(package.Id, package);
                VdfFileNode.ReadBin_SeekTo(bReader, packageidBytes, fileLength);
            }

            return result;
        }
    }
}