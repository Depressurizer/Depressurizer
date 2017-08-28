/*
This file is part of Depressurizer.
Original Work Copyright 2011, 2012, 2013 Steve Labbe.
Modified Work Copyright 2017 Theodoros Dimos.

Depressurizer is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Depressurizer is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Depressurizer.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;

namespace Depressurizer
{
    public class AutoCatPlatform : AutoCat
    {
        public override AutoCatType AutoCatType
        {
            get { return AutoCatType.Platform; }
        }

        // AutoCat configuration
        public string Prefix { get; set; }

        public bool Windows { get; set; }
        public bool Mac { get; set; }
        public bool Linux { get; set; }
        public bool SteamOS { get; set; }

        // Serialization constants
        public const string TypeIdString = "AutoCatPlatform";

        public AutoCatPlatform(string name, string filter = null, string prefix = null, bool windows = false, bool mac = false, bool linux = false, bool steamOS = false,
            bool selected = false)
            : base(name)
        {
            Filter = filter;
            Prefix = prefix;
            Windows = windows;
            Mac = mac;
            Linux = linux;
            SteamOS = steamOS;
            Selected = selected;
        }

        //XmlSerializer requires a parameterless constructor
        private AutoCatPlatform() { }

        protected AutoCatPlatform(AutoCatPlatform other)
            : base(other)
        {
            Filter = other.Filter;
            Prefix = other.Prefix;
            Windows = other.Windows;
            Mac = other.Mac;
            Linux = other.Linux;
            SteamOS = other.SteamOS;
            Selected = other.Selected;
        }

        public override AutoCat Clone()
        {
            return new AutoCatPlatform(this);
        }

        public override AutoCatResult CategorizeGame(GameInfo game, Filter filter)
        {
            if (games == null)
            {
                Program.Logger.WriteError(GlobalStrings.Log_AutoCat_GamelistNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameList);
            }
            if (game == null)
            {
                Program.Logger.WriteError(GlobalStrings.Log_AutoCat_GameNull);
                return AutoCatResult.Failure;
            }

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            if (!db.Contains(game.Id) || db.Games[game.Id].LastStoreScrape == 0) return AutoCatResult.NotInDatabase;

            AppPlatforms platforms = db.Games[game.Id].Platforms;
            if (Windows && (platforms & AppPlatforms.Windows) != 0)
            {
                game.AddCategory(games.GetCategory(GetProcessedString("Windows")));
            }
            if (Windows && (platforms & AppPlatforms.Mac) != 0)
            {
                game.AddCategory(games.GetCategory(GetProcessedString("Mac")));
            }
            if (Windows && (platforms & AppPlatforms.Linux) != 0)
            {
                game.AddCategory(games.GetCategory(GetProcessedString("Linux")));
            }
            if (Windows && (platforms & AppPlatforms.Linux) != 0)
            {
                game.AddCategory(games.GetCategory(GetProcessedString("SteamOS")));
            }
            return AutoCatResult.Success;
        }

        public string GetProcessedString(string s)
        {
            if (string.IsNullOrEmpty(Prefix))
            {
                return s;
            }
            return Prefix + s;
        }
    }
}