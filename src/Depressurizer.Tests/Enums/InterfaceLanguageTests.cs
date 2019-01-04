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
            InterfaceLanguage.English.Should().Be(0);
            InterfaceLanguage.Spanish.Should().Be(1);
            InterfaceLanguage.Russian.Should().Be(2);
            InterfaceLanguage.Ukrainian.Should().Be(3);
            InterfaceLanguage.Dutch.Should().Be(4);
        }

        #endregion
    }
}
