#region LICENSE

//     This file (AppInfo.cs) is part of DepressurizerCore.
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
using System.Data;
using System.Globalization;
using System.IO;
using DepressurizerCore.Helpers;

namespace DepressurizerCore.Models
{
	/// <summary>
	///     Operating System(s)
	/// </summary>
	[Flags]
	public enum AppPlatforms
	{
		/// <summary>
		///     Default value
		/// </summary>
		None = 0,

		/// <summary>
		///     Microsoft Windows
		/// </summary>
		Windows = 1 << 0,

		/// <summary>
		///     macOS
		/// </summary>
		Mac = 1 << 1,

		/// <summary>
		///     Linux
		/// </summary>
		Linux = 1 << 2,

		/// <summary>
		///     Windows, Mac and Linux
		/// </summary>
		All = (1 << 3) - 1
	}

	/// <summary>
	///     Steam App Type
	/// </summary>
	public enum AppType
	{
		/// <summary>
		///     Unknown
		/// </summary>
		Unknown,

		/// <summary>
		///     Game
		/// </summary>
		Game,

		/// <summary>
		///     DLC
		/// </summary>
		DLC,

		/// <summary>
		///     Steam Demo
		/// </summary>
		Demo,

		/// <summary>
		///     Steam Software
		/// </summary>
		Application,

		/// <summary>
		///     SDK's, servers etc..
		/// </summary>
		Tool,

		/// <summary>
		///     Steam Media
		/// </summary>
		Media,

		/// <summary>
		///     Steam Config File
		/// </summary>
		Config,

		/// <summary>
		///     Steam Media Content
		/// </summary>
		Series,

		/// <summary>
		///     Steam Media Content
		/// </summary>
		Video
	}

	public sealed class AppInfo
	{
		#region Static Fields

		private static readonly byte[] StartBytes = {0x00, 0x00, 0x63, 0x6F, 0x6D, 0x6D, 0x6F, 0x6E, 0x00};

		#endregion

		#region Constructors and Destructors

		public AppInfo(int appId)
		{
			AppId = appId;
		}

		#endregion

		#region Public Properties

		/// <summary>
		///     App Id
		/// </summary>
		public int AppId { get; set; } = 0;

		/// <summary>
		///     App Type
		/// </summary>
		public AppType AppType { get; set; } = AppType.Unknown;

		/// <summary>
		///     App Name
		/// </summary>
		public string Name { get; set; } = null;

		/// <summary>
		///     App's Parent Id
		/// </summary>
		public int ParentId { get; set; } = 0;

		/// <summary>
		///     Supported Platforms
		/// </summary>
		public AppPlatforms Platforms { get; set; } = AppPlatforms.None;

		#endregion

		#region Public Methods and Operators

		public static AppInfo FromNode(VDFNode commonNode)
		{
			if ((commonNode == null) || (commonNode.NodeType != ValueType.Array))
			{
				return null;
			}

			VDFNode idNode = commonNode.GetNodeAt(new[] {"gameid"}, false);
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

			if (id < 0)
			{
				return null;
			}

			AppInfo result = new AppInfo(id);

			VDFNode nameNode = commonNode.GetNodeAt(new[] {"name"}, false);
			if (nameNode != null)
			{
				result.Name = nameNode.NodeData.ToString();
			}

			string typeStr = null;
			VDFNode typeNode = commonNode.GetNodeAt(new[] {"type"}, false);
			if (typeNode != null)
			{
				typeStr = typeNode.NodeData.ToString();
			}

			if (typeStr != null)
			{
				if (Enum.TryParse(typeStr, true, out AppType type))
				{
					result.AppType = type;
				}
				else
				{
					SentryLogger.Log(new DataException(string.Format(CultureInfo.InvariantCulture, "New AppType '{0}'", typeStr)));
				}
			}

			VDFNode oslistNode = commonNode.GetNodeAt(new[] {"oslist"}, false);
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

			// Get parent
			VDFNode parentNode = commonNode.GetNodeAt(new[] {"parent"}, false);
			if (parentNode != null)
			{
				result.ParentId = parentNode.NodeInt;
			}

			return result;
		}

		public static Dictionary<int, AppInfo> LoadApps(string path)
		{
			Dictionary<int, AppInfo> appInfos = new Dictionary<int, AppInfo>();

			using (BinaryReader binaryReader = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)))
			{
				long fileLength = binaryReader.BaseStream.Length;

				VDFNode.ReadBin_SeekTo(binaryReader, StartBytes, fileLength);

				VDFNode node = VDFNode.LoadFromBinary(binaryReader, fileLength);
				while (node != null)
				{
					AppInfo app = FromNode(node);
					if (app != null)
					{
						appInfos.Add(app.AppId, app);
					}

					VDFNode.ReadBin_SeekTo(binaryReader, StartBytes, fileLength);
					node = VDFNode.LoadFromBinary(binaryReader, fileLength);
				}
			}

			return appInfos;
		}

		#endregion
	}
}