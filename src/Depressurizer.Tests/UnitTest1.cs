using System;
using Depressurizer.Core.Enums;
using Xunit;

namespace Depressurizer.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(7, (int) StoreLanguage.English);
        }
    }
}
