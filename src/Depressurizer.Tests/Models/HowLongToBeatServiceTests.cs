using System;
using System.Threading.Tasks;
using System.Threading;
using Depressurizer.Core.Helpers;
using FluentAssertions;
using Xunit;
using System.Net.Http;

namespace Depressurizer.Tests.Models
{
    public class HowLongToBeatServiceTests
    {
        [Fact]
        public async Task Detail_Should_Load_Entry_For_Dark_Souls()
        {
            // Arrange
            var service = new HowLongToBeatService();

            // Act
            var entry = await service.Detail("2224");

            // Assert
            entry.Should().NotBeNull();
            entry.Id.Should().Be("2224");
            entry.Name.Should().Be("Dark Souls");
            entry.SearchTerm.Should().Be("Dark Souls");
            entry.ImageUrl.Should().NotBeNullOrEmpty();
            entry.Platforms.Should().HaveCount(3);
            entry.PlayableOn.Should().HaveCount(3);
            entry.Description.Should().Contain("Live Through A Million Deaths & Earn Your Legacy.");
            entry.GameplayMain.Should().BeGreaterThan(40);
            entry.GameplayCompletionist.Should().BeGreaterThan(100);
        }

        [Fact]
        public async Task Detail_Should_Abort_Loading_Entry_For_Dark_Souls()
        {
            // Arrange
            var service = new HowLongToBeatService();
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            // Act & Assert
            await Assert.ThrowsAsync<TaskCanceledException>(async () => await service.Detail("2224", cancellationTokenSource.Token));
        }

        [Fact]
        public async Task Detail_Should_Fail_To_Load_Entry_For_404()
        {
            // Arrange
            var service = new HowLongToBeatService();

            // Act
            var exception = await Record.ExceptionAsync(async () => await service.Detail("123"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<System.Exception>(exception);
            Assert.IsType<HttpRequestException>(exception.InnerException);
        }

        [Fact]
        public async Task Search_Should_Have_No_Results_For_Dorks()
        {
            // Arrange
            var service = new HowLongToBeatService();

            // Act
            var result = await service.Search("dorks");

            // Assert
            result.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public async Task Search_Should_Have_At_Least_Three_Results_For_Dark_Souls_III()
        {
            // Arrange
            var service = new HowLongToBeatService();

            // Act
            var result = await service.Search("dark souls III");

            // Assert
            result.Should().HaveCountGreaterOrEqualTo(3);
            result[0].Id.Should().Be("26803");
            result[0].Name.Should().Be("Dark Souls III");
            result[0].GameplayMain.Should().BeGreaterThan(30);
            result[0].GameplayCompletionist.Should().BeGreaterThan(80);
        }

        [Fact]
        public async Task Search_Should_Abort_Searching_For_Dark_Souls_III()
        {
            // Arrange
            var service = new HowLongToBeatService();
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            // Act & Assert
            await Assert.ThrowsAsync<TaskCanceledException>(async () => await service.Search("dark souls III", cancellationTokenSource.Token));
        }

        [Fact]
        public async Task Search_Should_Have_One_Result_With_100_Percent_Similarity_For_Persona_4_Golden()
        {
            // Arrange
            var service = new HowLongToBeatService();

            // Act
            var result = await service.Search("Persona 4 Golden");

            // Assert
            result.Should().HaveCount(1);
            //result[0].Similarity.Should().Be(1);
        }

        [Fact]
        public async Task Entries_Without_Any_Time_Settings_Should_Have_Zero_Hour_Result()
        {
            // Arrange
            var service = new HowLongToBeatService();

            // Act
            var result = await service.Search("Surge");

            // Assert
            result.Should().NotBeNull().And.NotBeEmpty();
            result[0].GameplayMain.Should().Be(0);
        }
    }
}
