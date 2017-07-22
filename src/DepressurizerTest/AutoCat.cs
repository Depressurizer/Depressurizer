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

using Depressurizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DepressurizerTest
{
    [TestClass]
    public class AutoCat
    {
        [TestMethod]
        public void DisplayName()
        {
            AutoCatGenre x = new AutoCatGenre("Action");

            string expected = "Action";
            Assert.AreEqual(expected, x.Name);              // Action - Action
            Assert.AreEqual(expected, x.DisplayName);       // Action - Action

            x.Filter = "All";
            Assert.AreEqual(expected, x.Name);              // Action - Action
            Assert.AreNotEqual(expected, x.DisplayName);    // Action - Action*

            expected = "Action*";
            Assert.AreNotEqual(expected, x.Name);           // Action* - Action
            Assert.AreEqual(expected, x.DisplayName);       // Action* - Action*

            x.Filter = null;
            expected = "Action";
            Assert.AreEqual(expected, x.Name);              // Action - Action
            Assert.AreEqual(expected, x.DisplayName);       // Action - Action
        }
    }
}
