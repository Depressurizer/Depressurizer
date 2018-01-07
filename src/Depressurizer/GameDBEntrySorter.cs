using System.Collections.Generic;
using DepressurizerCore.Models;

namespace Depressurizer
{
    public class GameDBEntrySorter : IComparer<DatabaseEntry>
    {
        public enum SortModes
        {
            Id,
            Name,
            Genre,
            Type,
            IsScraped,
            HasAppInfo,
            Parent
        }

        public SortModes SortMode = SortModes.Id;
        public int SortDirection = 1;

        public void SetSortMode(SortModes mode, int forceDir = 0)
        {
            if (mode == SortMode)
            {
                SortDirection = (forceDir == 0) ? (SortDirection *= -1) : forceDir;
            }
            else
            {
                SortMode = mode;
                SortDirection = (forceDir == 0) ? 1 : forceDir;
            }
        }

        public int Compare(DatabaseEntry a, DatabaseEntry b)
        {
            int res = 0;
            switch (SortMode)
            {
                case SortModes.Id:
                    res = a.Id - b.Id;
                    break;
                case SortModes.Name:
                    res = string.Compare(a.Name, b.Name);
                    break;
                case SortModes.Genre:
                    res = Utility.CompareLists(a.Genres, b.Genres);
                    break;
                case SortModes.Type:
                    res = a.AppTypes - b.AppTypes;
                    break;
                case SortModes.IsScraped:
                    res = ((a.LastStoreScrape > 0) ? 1 : 0) - ((b.LastStoreScrape > 0) ? 1 : 0);
                    break;
                case SortModes.HasAppInfo:
                    res = ((a.LastAppInfoUpdate > 0) ? 1 : 0) - ((b.LastAppInfoUpdate > 0) ? 1 : 0);
                    break;
                case SortModes.Parent:
                    res = a.ParentId - b.ParentId;
                    break;
            }
            return SortDirection * res;
        }
    }
}