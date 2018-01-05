#region LICENSE

//     This file (Steam.cs) is part of DepressurizerCore.
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

using System.Diagnostics;
using System.Globalization;

namespace DepressurizerCore.Helpers
{
    /// <summary>
    ///     Steam Helper Class
    /// </summary>
    public static class Steam
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Opens up the store for an app.
        /// </summary>
        public static void LaunchStorePage(int appId)
        {
            if (appId < 0)
            {
                return;
            }

            ExecuteBrowser("steam://store/{0}", appId);
        }

        #endregion

        #region Methods

        private static void ExecuteBrowser(string url, params object[] args)
        {
            ExecuteBrowser(string.Format(CultureInfo.InvariantCulture, url, args));
        }

        private static void ExecuteBrowser(string url)
        {
            Process.Start(url);
        }

        #endregion
    }
}