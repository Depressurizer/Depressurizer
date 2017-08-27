#region GNU GENERAL PUBLIC LICENSE

// 
// This file is part of Depressurizer.
// Copyright (C) 2017 Martijn Vegter
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.
// 

#endregion

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Depressurizer.Properties;

namespace Depressurizer.Helpers
{
    public static class GameBanners
    {
        public static async void Grab(List<GameInfo> games)
        {
            await Task.Run(() =>
            {
                Parallel.ForEach(games, game =>
                {
                    if (game.Id < 0)
                    {
                        return;
                    }

                    string bannerFile = string.Format(Resources.GameBannerPath, Path.GetDirectoryName(Application.ExecutablePath), game.Id);
                    if (!File.Exists(bannerFile))
                    {
                        Utility.GrabBanner(game.Id);
                    }
                });
            });
        }
    }
}