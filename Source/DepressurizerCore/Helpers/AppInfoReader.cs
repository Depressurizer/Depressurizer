#region LICENSE

//     This file (AppInfoReader.cs) is part of DepressurizerCore.
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DepressurizerCore.Models;

namespace DepressurizerCore.Helpers
{
	/// <summary>
	///     Steam AppInfo.vdf Reader
	/// </summary>
	public class AppInfoReader
	{
		#region Constructors and Destructors

		public AppInfoReader(string path)
		{
			using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
			{
				// Read some header fields
				reader.ReadByte();
				if ((reader.ReadByte() != 0x44) || (reader.ReadByte() != 0x56))
				{
					throw new Exception("Invalid vdf format");
				}

				// Skip more header fields
				reader.ReadBytes(5);

				while (true)
				{
					uint id = reader.ReadUInt32();
					if (id == 0)
					{
						break;
					}

					// Skip unused fields
					reader.ReadBytes(44);

					// Load details
					Items[id] = ReadEntries(reader);
				}
			}
		}

		#endregion

		#region Public Properties

		public Dictionary<uint, AppInfoNode> Items { get; set; } = new Dictionary<uint, AppInfoNode>();

		#endregion

		#region Methods

		private static AppInfoNode ReadEntries(BinaryReader reader)
		{
			AppInfoNode result = new AppInfoNode();

			while (true)
			{
				byte type = reader.ReadByte();
				if (type == 0x08)
				{
					break;
				}

				string key = ReadString(reader);

				switch (type)
				{
					case 0x00:
						result[key] = ReadEntries(reader);
						break;
					case 0x01:
						result[key] = new AppInfoNode(ReadString(reader));
						break;
					case 0x02:
						result[key] = new AppInfoNode(reader.ReadUInt32().ToString());
						break;
					default:
						throw new Exception("Uknown entry type " + type + ".");
				}
			}

			return result;
		}

		private static string ReadString(BinaryReader reader)
		{
			List<byte> bytes = new List<byte>();

			try
			{
				bool stringDone = false;
				do
				{
					byte b = reader.ReadByte();
					if (b == 0)
					{
						stringDone = true;
					}
					else
					{
						bytes.Add(b);
					}
				} while (!stringDone);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			return Encoding.UTF8.GetString(bytes.ToArray());
		}

		#endregion
	}
}