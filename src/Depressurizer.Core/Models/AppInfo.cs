using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;

namespace Depressurizer.Core.Models
{
    /// <summary>
    ///     Class representing a single appinfo object.
    /// </summary>
    public sealed class AppInfo
    {
        #region Constants

        private const string NodeAppInfo = "appinfo";

        private const string NodeAppType = "type";

        private const string NodeCommon = "common";

        private const string NodeId = "gameid";

        private const string NodeName = "name";

        private const string NodeParentId = "parent";

        private const string NodePlatforms = "oslist";

        private const string NodePlatformsLinux = "linux";

        private const string NodePlatformsMac = "mac";

        private const string NodePlatformsWindows = "windows";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Creates a AppInfo object with the specified appId.
        /// </summary>
        /// <param name="appId">
        ///     Steam Application ID.
        /// </param>
        public AppInfo(int appId)
        {
            Id = appId;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Type of this application.
        /// </summary>
        public AppType AppType { get; set; } = AppType.Unknown;

        /// <summary>
        ///     Steam Application ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     The name of this application.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Steam Application ID of the parent application.
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        ///     Supported platforms.
        /// </summary>
        public AppPlatforms Platforms { get; set; } = AppPlatforms.None;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Parses the appinfo.vdf file and converts it into a dictionary where the key equals the AppId and value is the
        ///     AppInfo object.
        /// </summary>
        /// <param name="path">
        ///     Path to the appinfo.vdf file.
        /// </param>
        /// <returns>
        ///     Returns a dictionary where the key equals the AppId and value is the AppInfo object.
        /// </returns>
        public static Dictionary<int, AppInfo> LoadApps(string path)
        {
            Dictionary<int, AppInfo> appInfos = new Dictionary<int, AppInfo>();
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return appInfos;
            }

            Dictionary<uint, AppInfoNode> appInfoNodes = new AppInfoReader(path).Items;
            foreach (AppInfoNode appInfoNode in appInfoNodes.Values)
            {
                AppInfo appInfo = FromNode(appInfoNode);
                if (appInfo != null)
                {
                    appInfos.Add(appInfo.Id, appInfo);
                }
            }

            return appInfos;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Converts an AppInfo node to an AppInfo object.
        /// </summary>
        /// <param name="node">
        ///     AppInfo node to parse.
        /// </param>
        /// <returns>
        ///     Returns an AppInfo object if the node was successfully parsed, else it returns null.
        /// </returns>
        private static AppInfo FromNode(AppInfoNode node)
        {
            if (node?.Items == null)
            {
                return null;
            }

            if (!node.Items.ContainsKey(NodeAppInfo))
            {
                return null;
            }

            if (node[NodeAppInfo].Items == null || !node[NodeAppInfo].Items.ContainsKey(NodeCommon))
            {
                return null;
            }

            AppInfoNode dataNode = node[NodeAppInfo][NodeCommon];
            if (dataNode.Items == null || !dataNode.Items.ContainsKey(NodeId))
            {
                return null;
            }

            string gameIdNode = dataNode[NodeId].Value;
            if (!int.TryParse(gameIdNode, out int appId))
            {
                return null;
            }

            AppInfo appInfo = new AppInfo(appId);

            if (dataNode.Items.ContainsKey(NodeName))
            {
                appInfo.Name = dataNode[NodeName].Value;
            }

            if (dataNode.Items.ContainsKey(NodeAppType))
            {
                string typeData = dataNode[NodeAppType].Value;
                if (Enum.TryParse(typeData, true, out AppType type))
                {
                    appInfo.AppType = type;
                }
                else
                {
                    Debug.WriteLine(string.Format(CultureInfo.InvariantCulture, "AppInfo: New AppType '{0}'", typeData));
                }
            }

            if (dataNode.Items.ContainsKey(NodePlatforms))
            {
                string osList = dataNode[NodePlatforms].Value;
                if (osList.IndexOf(NodePlatformsWindows, StringComparison.OrdinalIgnoreCase) != -1)
                {
                    appInfo.Platforms |= AppPlatforms.Windows;
                }

                if (osList.IndexOf(NodePlatformsMac, StringComparison.OrdinalIgnoreCase) != -1)
                {
                    appInfo.Platforms |= AppPlatforms.Mac;
                }

                if (osList.IndexOf(NodePlatformsLinux, StringComparison.OrdinalIgnoreCase) != -1)
                {
                    appInfo.Platforms |= AppPlatforms.Linux;
                }
            }

            if (!dataNode.Items.ContainsKey(NodeParentId))
            {
                return appInfo;
            }

            string parentNode = dataNode[NodeParentId].Value;
            if (int.TryParse(parentNode, out int parentId))
            {
                appInfo.ParentId = parentId;
            }

            return appInfo;
        }

        #endregion
    }
}
