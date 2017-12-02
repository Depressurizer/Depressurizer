#region License

//     This file (AppInfoTests.cs) is part of DepressurizerTests.
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

using Depressurizer;
using Depressurizer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DepressurizerTests.Models
{
    [TestClass]
    public class AppInfoTests
    {
        [TestMethod]
        public void DefaultValues()
        {
            AppInfo testAppInfo = new AppInfo();
            Assert.AreEqual(0, testAppInfo.Id);
            Assert.AreEqual(null, testAppInfo.Name);
            Assert.AreEqual(AppTypes.Unknown, testAppInfo.AppType);
            Assert.AreEqual(AppPlatforms.None, testAppInfo.Platforms);
            Assert.AreEqual(0, testAppInfo.Parent);
        }
    }
}