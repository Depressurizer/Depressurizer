#region LICENSE

//     This file (GameBanners.cs) is part of Depressurizer.
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
using System.IO;
using System.Windows.Forms;
using Depressurizer.Properties;

namespace Depressurizer
{
    internal class GameBanners
    {
        #region Fields

        private readonly List<GameInfo> _games;
        private volatile bool _shouldStop;

        #endregion

        #region Constructors and Destructors

        public GameBanners(List<GameInfo> games)
        {
            _games = games;
        }

        #endregion

        #region Public Methods and Operators

        public void Grab()
        {
            foreach (GameInfo g in _games)
            {
                if (_shouldStop)
                {
                    return;
                }

                if (g.Id < 0)
                {
                    continue; //external game
                }

                string bannerFile = string.Format(Resources.GameBannerPath, Path.GetDirectoryName(Application.ExecutablePath), g.Id);
                if (!File.Exists(bannerFile))
                {
                    Utility.GrabBanner(g.Id);
                }
            }
        }

        public void Stop()
        {
            _shouldStop = true;
        }

        #endregion
    }
}
