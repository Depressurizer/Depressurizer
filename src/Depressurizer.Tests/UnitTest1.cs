using Depressurizer.Core.Enums;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Depressurizer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        #region Public Methods and Operators

        [TestMethod]
        public void TestMethod1()
        {
            ((int)StoreLanguage.English).Should().Be(7);
        }

        #endregion
    }
}
