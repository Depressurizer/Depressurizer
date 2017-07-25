/*
    This file is part of Depressurizer.
    Original work Copyright 2017 Martijn Vegter.

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

using System.Drawing;
using Depressurizer.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DepressurizerTest.Helpers
{
    [TestClass]
    public class SteamTest
    {
        [TestMethod]
        public void GetAvatarInvalidSteamId()
        {
            Image steamAvatar = Steam.GetAvatar(long.MaxValue);
            Assert.IsNull(steamAvatar);
        }

        [TestMethod]
        public void GetAvatarValidSteamId()
        {
            Image steamAvatar = Steam.GetAvatar(76561198347847228);
            Assert.IsNotNull(steamAvatar);
        }
    }
}