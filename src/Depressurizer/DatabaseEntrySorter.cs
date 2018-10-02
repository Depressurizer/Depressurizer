#region LICENSE

//     This file (DatabaseEntrySorter.cs) is part of Depressurizer.
//     Copyright (C) 2011 Steve Labbe
//     Copyright (C) 2017 Theodoros Dimos
//     Copyright (C) 2017 Martijn Vegter
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
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System.Collections.Generic;
using Depressurizer.Models;

namespace Depressurizer
{
    public class DatabaseEntrySorter : IComparer<DatabaseEntry>
    {
        #region Fields

        public int SortDirection = 1;

        public SortModes SortMode = SortModes.Id;

        #endregion

        #region Enums

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

        #endregion

        #region Public Methods and Operators

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
                    res = a.AppType - b.AppType;
                    break;
                case SortModes.IsScraped:
                    res = (a.LastStoreScrape > 0 ? 1 : 0) - (b.LastStoreScrape > 0 ? 1 : 0);
                    break;
                case SortModes.HasAppInfo:
                    res = (a.LastAppInfoUpdate > 0 ? 1 : 0) - (b.LastAppInfoUpdate > 0 ? 1 : 0);
                    break;
                case SortModes.Parent:
                    res = a.ParentId - b.ParentId;
                    break;
            }

            return SortDirection * res;
        }

        public void SetSortMode(SortModes mode, int forceDir = 0)
        {
            if (mode == SortMode)
            {
                SortDirection = forceDir == 0 ? (SortDirection *= -1) : forceDir;
            }
            else
            {
                SortMode = mode;
                SortDirection = forceDir == 0 ? 1 : forceDir;
            }
        }

        #endregion
    }
}
