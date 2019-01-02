using Depressurizer.Core.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Depressurizer.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            ((int)StoreLanguage.English).Should().Be(7);
        }
    }
}