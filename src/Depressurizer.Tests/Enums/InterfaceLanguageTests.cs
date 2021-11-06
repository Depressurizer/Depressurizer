using Depressurizer.Core.Enums;
using FluentAssertions;
using Xunit;

namespace Depressurizer.Tests.Enums
{
    public class InterfaceLanguageTests
    {
        #region Public Methods and Operators

        [Fact]
        public void Values()
        {
            ((int)InterfaceLanguage.English).Should().Be(0);
            ((int)InterfaceLanguage.Spanish).Should().Be(1);
            ((int)InterfaceLanguage.Russian).Should().Be(2);
            ((int)InterfaceLanguage.Ukrainian).Should().Be(3);
            ((int)InterfaceLanguage.Dutch).Should().Be(4);
            ((int)InterfaceLanguage.SChinese).Should().Be(5);
        }

        #endregion
    }
}
