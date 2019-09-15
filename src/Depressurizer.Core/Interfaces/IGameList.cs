using Depressurizer.Core.Models;

namespace Depressurizer.Core.Interfaces
{
    public interface IGameList
    {
        #region Public Properties

        Category FavoriteCategory { get; }

        string FavoriteConfigValue { get; }

        string FavoriteNewConfigValue { get; }

        #endregion
    }
}
