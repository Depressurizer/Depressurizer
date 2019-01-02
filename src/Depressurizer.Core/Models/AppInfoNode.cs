using System.Collections.Generic;

namespace Depressurizer.Core.Models
{
    /// <summary>
    ///     AppInfo Node
    /// </summary>
    public sealed class AppInfoNode
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Creates an empty AppInfo node.
        /// </summary>
        public AppInfoNode() { }

        /// <summary>
        ///     Creates an AppInfo node with the specified (node) value.
        /// </summary>
        /// <param name="value">
        ///     Data of the AppInfo node.
        /// </param>
        public AppInfoNode(string value)
        {
            Value = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Dictionary containing the sub-nodes.
        /// </summary>
        public Dictionary<string, AppInfoNode> Items { get; set; } = new Dictionary<string, AppInfoNode>();

        /// <summary>
        ///     Data of the AppInfo node.
        /// </summary>
        public string Value { get; set; }

        #endregion

        #region Public Indexers

        /// <summary>
        ///     Gets or sets the sub-node with the given key.
        /// </summary>
        /// <param name="key">
        ///     Key of the sub-node to get or set.
        /// </param>
        /// <returns>
        ///     Returns the sub-node at the specified key.
        /// </returns>
        public AppInfoNode this[string key]
        {
            get => Items[key];
            set => Items[key] = value;
        }

        #endregion
    }
}
