#region LICENSE

//     This file (PackageInfo.cs) is part of DepressurizerCore.
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
using System.IO;
using System.Text;

namespace DepressurizerCore.Models
{
	public enum PackageBillingType
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

	public class PackageInfo
	{
		#region Static Fields

		/// <summary>
		///     0x08 0x00 a p p i d s 0x00
		/// </summary>
		private static readonly byte[] AppidsBytes = {0x08, 0x00, 0x61, 0x70, 0x70, 0x69, 0x64, 0x73, 0x00};

		/// <summary>
		///     0x02 b i l l i n g t y p e 0x00
		/// </summary>
		private static readonly byte[] BillingtypeBytes = {0x02, 0x62, 0x69, 0x6C, 0x6C, 0x69, 0x6E, 0x67, 0x74, 0x79, 0x70, 0x65, 0x00};

		/// <summary>
		///     0x00 0x02 p a c k a g e i d 0x00
		/// </summary>
		private static readonly byte[] PackageidBytes = {0x00, 0x02, 0x70, 0x61, 0x63, 0x6B, 0x61, 0x67, 0x65, 0x69, 0x64, 0x00};

		#endregion

		#region Constructors and Destructors

		public PackageInfo(int id, string name = null)
		{
			Id = id;
			Name = name;
		}

		#endregion

		#region Public Properties

		public List<int> AppIds { get; set; } = new List<int>();

		public PackageBillingType BillingType { get; set; }

		public int Id { get; set; }

		public string Name { get; set; }

		#endregion

		#region Public Methods and Operators

		public static PackageInfo FromNode(VDFNode node)
		{
			VDFNode idNode = node.GetNodeAt(new[] {"packageId"}, false);
			if ((idNode != null) && (idNode.NodeType == ValueType.Int))
			{
				int id = idNode.NodeInt;

				string name = null;
				VDFNode nameNode = node.GetNodeAt(new[] {"name"}, false);
				if ((nameNode != null) && (nameNode.NodeType == ValueType.String))
				{
					name = nameNode.NodeString;
				}

				PackageInfo package = new PackageInfo(id, name);

				VDFNode billingtypeNode = node["billingtype"];
				if (((billingtypeNode != null) && (billingtypeNode.NodeType == ValueType.String)) || (billingtypeNode.NodeType == ValueType.Int))
				{
					int bType = billingtypeNode.NodeInt;
					package.BillingType = (PackageBillingType) bType;
				}

				VDFNode appsNode = node["appids"];
				if ((appsNode != null) && (appsNode.NodeType == ValueType.Array))
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

		public static Dictionary<int, PackageInfo> LoadPackages(string path)
		{
			/*
			 * packageinfo.vdf entry example format, sometimes has extra values. Line breaks are only for readability and not part of format.
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

			using (BinaryReader binaryReader = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.ASCII))
			{
				long fileLength = binaryReader.BaseStream.Length;

				VDFNode.ReadBin_SeekTo(binaryReader, PackageidBytes, fileLength);
				while (binaryReader.BaseStream.Position < fileLength)
				{
					int id = binaryReader.ReadInt32();
					PackageInfo package = new PackageInfo(id);

					VDFNode.ReadBin_SeekTo(binaryReader, BillingtypeBytes, fileLength);
					package.BillingType = (PackageBillingType) binaryReader.ReadInt32();

					VDFNode.ReadBin_SeekTo(binaryReader, AppidsBytes, fileLength);
					while (binaryReader.ReadByte() == 0x02)
					{
						while (binaryReader.ReadByte() != 0x00)
						{
						}

						package.AppIds.Add(binaryReader.ReadInt32());
					}

					packageInfos.Add(package.Id, package);
					VDFNode.ReadBin_SeekTo(binaryReader, PackageidBytes, fileLength);
				}
			}

			return packageInfos;
		}

		#endregion
	}
}