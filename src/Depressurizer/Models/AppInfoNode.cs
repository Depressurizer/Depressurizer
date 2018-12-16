using System.Collections.Generic;

namespace Depressurizer.Models
{
    /// <summary>
    ///     AppInfo Node
    /// </summary>
    public sealed class AppInfoNode
    {
        #region Constructors and Destructors

        public AppInfoNode() { }

        public AppInfoNode(string value)
        {
            Value = value;
        }

        #endregion

        #region Public Properties

        public Dictionary<string, AppInfoNode> Items { get; set; } = new Dictionary<string, AppInfoNode>();

        public string Value { get; set; }

        #endregion

        #region Public Indexers

        public AppInfoNode this[string index]
        {
            get => Items[index];
            set => Items[index] = value;
        }

        #endregion
    }
}
