using System;
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

        Dictionary<int, GameInfo> Games { get; set; }

        #endregion

        #region Public Methods and Operators

        Category AddCategory(string name, bool forRename = false);

        Filter AddFilter(string name);

        void AddGameCategory(int appId, Category category);

        void AddGameCategory(int[] appIds, Category category);

        bool CategoryExists(string name, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase);

        void ClearGameCategories(int appId, bool preserveFavorite);

        void ClearGameCategories(int[] appIds, bool preserveFavorite);

        void ExportSteamConfig(long steamId, bool discardMissing, bool includeShortcuts);

        void ExportSteamConfigFile(string filePath, bool discardMissing);

        bool FilterExists(string dialogValue);

        Category GetCategory(string name);

        Filter GetFilter(string acgFilter);

        void HideGames(int appId, bool isHidden);

        void HideGames(int[] appIds, bool isHidden);

        int ImportSteamConfig(long steamId, SortedSet<int> ignore, bool includeShortcuts);

        int IntegrateGameList(GetOwnedGamesObject doc, bool overwrite, SortedSet<int> ignore, out int newItems);

        int IntegrateGameList(IXPathNavigable doc, bool overwrite, SortedSet<int> ignore, out int newItems);

        bool RemoveCategory(Category category);

        int RemoveEmptyCategories();

        void RemoveGameCategory(int appId, Category category);

        void RemoveGameCategory(int[] appIds, Category category);

        Category RenameCategory(Category category, string newName);

        void SetGameCategories(int[] appIds, Category cat, bool preserveFavorites);

        void SetGameCategories(int appId, ICollection<Category> catSet, bool preserveFavorites);

        void SetGameCategories(int[] appIds, ICollection<Category> catSet, bool preserveFavorites);

        int UpdateGameListFromOwnedPackageInfo(long accountId, SortedSet<int> ignored, out int newApps);

        #endregion
    }
}
