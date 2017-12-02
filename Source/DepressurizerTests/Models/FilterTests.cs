#region License

//     This file (FilterTests.cs) is part of DepressurizerTests.
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
    public class FilterTests
    {
        [TestMethod]
        public void DefaultValues() { }

        [TestMethod]
        public void CompareTo_AA0()
        {
            Filter testFilter = new Filter("a");

            Assert.AreEqual(0, testFilter.CompareTo(testFilter));
        }

        [TestMethod]
        public void CompareTo_AB0_BA0()
        {
            Filter testFilter1 = new Filter("a");
            Filter testFilter2 = new Filter("A");

            Assert.AreEqual(0, testFilter1.CompareTo(testFilter2));
            Assert.AreEqual(0, testFilter2.CompareTo(testFilter1));
        }

        [TestMethod]
        public void CompareTo_AB0_BC0_AC0()
        {
            Filter testFilter1 = new Filter("a");
            Filter testFilter2 = new Filter("A");
            Filter testFilter3 = new Filter("a");

            Assert.AreEqual(0, testFilter1.CompareTo(testFilter2));
            Assert.AreEqual(0, testFilter2.CompareTo(testFilter3));
            Assert.AreEqual(0, testFilter1.CompareTo(testFilter3));
        }

        [TestMethod]
        public void CompareTo_ABn1_BA1()
        {
            Filter testFilter1 = new Filter("a");
            Filter testFilter2 = new Filter("b");

            Assert.AreEqual(-1, testFilter1.CompareTo(testFilter2));
            Assert.AreEqual(1, testFilter2.CompareTo(testFilter1));
        }
    }
}