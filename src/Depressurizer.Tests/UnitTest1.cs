using Depressurizer.Core.Enums;
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
            Assert.AreEqual((int) StoreLanguage.English, 7);
        }

        #endregion
    }
}
