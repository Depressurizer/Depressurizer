using System;
using Depressurizer.Core.Models;
using FluentAssertions;
using Xunit;

namespace Depressurizer.Tests.Models
{
    public class CategoryTests
    {
        #region Public Methods and Operators

        [Fact]
        public void SteamOrder()
        {
            Category dollar = new Category("$");
            Category dash = new Category("/");
            Category star = new Category("*");
            Category minus = new Category("-");
            Category plus = new Category("+");
            Category num1 = new Category("1");
            Category num2 = new Category("2");
            Category a = new Category("a");
            Category b = new Category("b");
            Category lowDash = new Category("_");

            Category[] categories =
            {
                lowDash,
                dash,
                b,
                num1,
                star,
                minus,
                dollar,
                plus,
                num2,
                a
            };

            Array.Sort(categories);

            categories[0].Name.Should().Be("$");
            categories[1].Name.Should().Be("*");
            categories[2].Name.Should().Be("+");
            categories[3].Name.Should().Be("-");
            categories[4].Name.Should().Be("/");
            categories[5].Name.Should().Be("1");
            categories[6].Name.Should().Be("2");
            categories[7].Name.Should().Be("a");
            categories[8].Name.Should().Be("b");
            categories[9].Name.Should().Be("_");
        }

        #endregion
    }
}
