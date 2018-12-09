using BrightIdeasSoftware;

namespace Depressurizer
{
    /// <summary>
    ///     Clustering strategy for columns with comma-separated strings. (Tags, Categories, Flags, Genres etc)
    /// </summary>
    /// <inheritdoc />
    public class CommaClusteringStrategy : ClusteringStrategy
    {
        #region Public Methods and Operators

        public override object GetClusterKey(object model)
        {
            return ((string) Column.GetValue(model)).Replace(", ", ",").Split(',');
        }

        #endregion
    }
}
