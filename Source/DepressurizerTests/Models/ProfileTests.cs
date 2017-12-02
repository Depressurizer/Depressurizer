#region License

//     This file (ProfileTests.cs) is part of DepressurizerTests.
//     Copyright (C) 2017  Martijn Vegter
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

using Depressurizer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DepressurizerTests.Models
{
    [TestClass]
    public class ProfileTests
    {
        [TestMethod]
        public void DefaultValues()
        {
            Profile testProfile = new Profile();
            Assert.AreEqual(0, testProfile.SteamID64);
        }

        [TestMethod]
        public void ToSteamID64()
        {
            Assert.AreEqual(76561198347847228, Profile.ToSteamID64(387581500));
            Assert.AreEqual(76561198347847228, Profile.ToSteamID64("387581500"));
        }


        [TestMethod]
        public void ToSteam3ID()
        {
            Assert.AreEqual(387581500, Profile.ToSteam3ID(76561198347847228));
        }
    }
}