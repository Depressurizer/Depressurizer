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
using Newtonsoft.Json;

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
		Video,

		/// <summary>
		///     Steam Hardware
		/// </summary>
		Hardware,

		/// <summary>
		///     Player-created manuals & references
		/// </summary>
		Guide
	}

	/// <summary>
	///     Steam AppInfo object
	/// </summary>
	public sealed class AppInfo
	{
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

		public static AppInfo FromNode(AppInfoNode node)
		{
			if (node == null)
			{
				return null;
			}

			if (!node.Items.ContainsKey("appinfo") || !node["appinfo"].Items.ContainsKey("common") || !node["appinfo"]["common"].Items.ContainsKey("gameid"))
			{
				return null;
			}

			AppInfoNode dataNode = node["appinfo"]["common"];

			string gameIdNode = dataNode["gameid"].Value;
			if (!int.TryParse(gameIdNode, out int appId))
			{
				return null;
			}

			AppInfo appInfo = new AppInfo(appId);

			if (dataNode.Items.ContainsKey("name"))
			{
				appInfo.Name = dataNode["name"].Value;
			}

			if (dataNode.Items.ContainsKey("type"))
			{
				string typeData = dataNode["type"].Value;
				if (Enum.TryParse(typeData, true, out AppType type))
				{
					appInfo.AppType = type;
				}
				else
				{
					SentryLogger.Log(new DataException(string.Format(CultureInfo.InvariantCulture, "New AppType '{0}'", typeData)));
				}
			}

			if (dataNode.Items.ContainsKey("oslist"))
			{
				string osList = dataNode["oslist"].Value;
				if (osList.IndexOf("windows", StringComparison.OrdinalIgnoreCase) != -1)
				{
					appInfo.Platforms |= AppPlatforms.Windows;
				}

				if (osList.IndexOf("mac", StringComparison.OrdinalIgnoreCase) != -1)
				{
					appInfo.Platforms |= AppPlatforms.Mac;
				}

				if (osList.IndexOf("linux", StringComparison.OrdinalIgnoreCase) != -1)
				{
					appInfo.Platforms |= AppPlatforms.Linux;
				}
			}

			if (!dataNode.Items.ContainsKey("parent"))
			{
				return appInfo;
			}

			string parentNode = dataNode["parent"].Value;
			if (int.TryParse(parentNode, out int parentId))
			{
				appInfo.ParentId = parentId;
			}

			return appInfo;
		}

		public static Dictionary<int, AppInfo> LoadApps(string path)
		{
			Dictionary<int, AppInfo> appInfos = new Dictionary<int, AppInfo>();
			Dictionary<uint, AppInfoNode> appInfoNodes;

			try
			{
				string currentHash = Utility.CalculateMD5(path);

				if (!File.Exists(Location.File.AppInfoCache) || !File.Exists(Location.File.AppInfoHash) || (File.ReadAllText(Location.File.AppInfoHash) != currentHash))
				{
					appInfoNodes = new AppInfoReader(path).Items;
					File.WriteAllText(Location.File.AppInfoCache, JsonConvert.SerializeObject(appInfoNodes));
					File.WriteAllText(Location.File.AppInfoHash, currentHash);
				}
				else
				{
					appInfoNodes = JsonConvert.DeserializeObject<Dictionary<uint, AppInfoNode>>(File.ReadAllText(Location.File.AppInfoCache));
				}
			}
			catch (Exception e)
			{
				SentryLogger.Log(e);
				throw;
			}

			try
			{
				foreach (AppInfoNode appInfoNode in appInfoNodes.Values)
				{
					AppInfo appInfo = FromNode(appInfoNode);
					if (appInfo != null)
					{
						appInfos.Add(appInfo.AppId, appInfo);
					}
				}
			}
			catch (Exception e)
			{
				SentryLogger.Log(e);
				throw;
			}

			return appInfos;
		}

		#endregion
	}
}