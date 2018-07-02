#region License

//     This file (AppInfo.cs) is part of Depressurizer.
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
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Helpers;

namespace Depressurizer.Models
{
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
					Sentry.Log(new DataException(string.Format(CultureInfo.InvariantCulture, "AppInfo: New AppType '{0}'", typeData)));
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
			if (string.IsNullOrWhiteSpace(path))
			{
				return appInfos;
			}

			Dictionary<uint, AppInfoNode> appInfoNodes = new AppInfoReader(path).Items;
			foreach (AppInfoNode appInfoNode in appInfoNodes.Values)
			{
				AppInfo appInfo = FromNode(appInfoNode);
				if (appInfo != null)
				{
					appInfos.Add(appInfo.AppId, appInfo);
				}
			}

			return appInfos;
		}

		#endregion
	}
}
