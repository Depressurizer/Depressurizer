using System.Collections.Generic;
using System.Xml.XPath;
using Depressurizer.Core.Models;

namespace Depressurizer.Core.Interfaces
{
    public interface IGameList
    {
        #region Public Properties

        List<Category> Categories { get; set; }

        Category FavoriteCategory { get; }

        string FavoriteConfigValue { get; }

        string FavoriteNewConfigValue { get; }

        List<Filter> Filters { get; set; }

        Dictionary<long, GameInfo> Games { get; set; }

        #endregion

        #region Public Methods and Operators

        Category AddCategory(string dialogValue);

        Filter AddFilter(string name);

        void AddGameCategory(long appId, Category category);

        void AddGameCategory(long[] appIds, Category category);

        bool CategoryExists(string prefix);

        void ClearGameCategories(long appId, bool preserveFavorite);

        void ClearGameCategories(long[] appIds, bool preserveFavorite);

        void ExportSteamConfig(long steamId, bool discardMissing, bool includeShortcuts);

        void ExportSteamConfigFile(string filePath, bool discardMissing);

        bool FilterExists(string dialogValue);

        Category GetCategory(string name);

        Filter GetFilter(string acgFilter);

        void HideGames(long appId, bool isHidden);

        void HideGames(long[] appIds, bool isHidden);

        int ImportSteamConfig(long steamId, SortedSet<long> ignore, bool includeShortcuts);

        int IntegrateGameList(GetOwnedGamesObject doc, bool overwrite, SortedSet<long> ignore, out int newItems);

        int IntegrateGameList(IXPathNavigable doc, bool overwrite, SortedSet<long> ignore, out int newItems);

        bool RemoveCategory(Category category);

        int RemoveEmptyCategories();

        void RemoveGameCategory(long appId, Category category);

        void RemoveGameCategory(long[] appIds, Category category);

        Category RenameCategory(Category category, string newName);

        void SetGameCategories(long[] appIds, Category cat, bool preserveFavorites);

        void SetGameCategories(long appId, ICollection<Category> catSet, bool preserveFavorites);

        void SetGameCategories(long[] appIds, ICollection<Category> catSet, bool preserveFavorites);

        int UpdateGameListFromOwnedPackageInfo(long accountId, SortedSet<long> ignored, out int newApps);

        #endregion
    }
}
