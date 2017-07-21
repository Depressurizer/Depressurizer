/*
    This file is part of Depressurizer.
    Original work Copyright 2011, 2012, 2013 Steve Labbe.
    Modified work Copyright 2017 Martijn Vegter.

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

namespace Depressurizer.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class AppInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AppTypes AppType { get; set; }
        public AppPlatforms Platforms { get; set; }
        public int Parent { get; set; } // Is 0 if no parent

        public AppInfo(int id, string name = null, AppTypes type = AppTypes.Unknown, AppPlatforms platforms = AppPlatforms.All)
        {
            Id = id;
            Name = name;
            AppType = type;
            Platforms = platforms;
            Parent = 0;
        }

        // TODO Remove FromVdfNode in GameDB.cs
        public AppInfo(VdfFileNode commonNode)
        {
            throw new NotImplementedException();
        }
    }
}
