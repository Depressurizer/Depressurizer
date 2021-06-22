using System;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Models;
using FluentAssertions;
using Xunit;

namespace Depressurizer.Tests.Models
{
    public class DatabaseEntryTests
    {
        #region Fields

        /// <summary>
        ///     CS:GO - English
        /// </summary>
        private readonly DatabaseEntry _entry1 = new DatabaseEntry(730);

        /// <summary>
        ///     CS:GO - Dutch
        /// </summary>
        private readonly DatabaseEntry _entry2 = new DatabaseEntry(730);

        /// <summary>
        ///     CS:GO - Chinese Simplified
        /// </summary>
        private readonly DatabaseEntry _entry3 = new DatabaseEntry(730);

        #endregion

        #region Constructors and Destructors

        public DatabaseEntryTests()
        {
            _entry1.ScrapeStore("", StoreLanguage.English);
            _entry2.ScrapeStore("", StoreLanguage.Dutch);
            _entry3.ScrapeStore("", StoreLanguage.ChineseSimplified);
        }

        #endregion

        #region Public Methods and Operators

        [Fact]
        public void DifferentLanguagesShouldHaveSameAppType()
        {
            _entry1.AppType.Should().Be(AppType.Game);
            _entry2.AppType.Should().Be(AppType.Game);
            _entry3.AppType.Should().Be(AppType.Game);
        }

        [Fact]
        public void DifferentLanguagesShouldHaveSameNumDevelopers()
        {
            _entry1.Developers.Count.Should().BeGreaterThan(0);
            _entry2.Developers.Count.Should().Be(_entry1.Developers.Count);
            _entry3.Developers.Count.Should().Be(_entry1.Developers.Count);
        }

        [Fact]
        public void DifferentLanguagesShouldHaveSameNumFlags()
        {
            _entry1.Flags.Count.Should().BeGreaterThan(0);
            _entry2.Flags.Count.Should().Be(_entry1.Flags.Count);
            _entry3.Flags.Count.Should().Be(_entry1.Flags.Count);
        }

        [Fact]
        public void DifferentLanguagesShouldHaveSameNumGenres()
        {
            _entry1.Genres.Count.Should().BeGreaterThan(0);
            _entry2.Genres.Count.Should().Be(_entry1.Genres.Count);
            _entry3.Genres.Count.Should().Be(_entry1.Genres.Count);
        }

        [Fact]
        public void DifferentLanguagesShouldHaveSameNumPublishers()
        {
            _entry1.Publishers.Count.Should().BeGreaterThan(0);
            _entry2.Publishers.Count.Should().Be(_entry1.Publishers.Count);
            _entry3.Publishers.Count.Should().Be(_entry1.Publishers.Count);
        }

        [Fact]
        public void DifferentLanguagesShouldHaveSameNumTags()
        {
            _entry1.Tags.Count.Should().BeGreaterThan(0);
            _entry2.Tags.Count.Should().Be(_entry1.Tags.Count);
            _entry3.Tags.Count.Should().Be(_entry1.Tags.Count);
        }

        [Fact]
        public void DifferentLanguagesShouldHaveSamePlatforms()
        {
            const AppPlatforms supportedPlatforms = AppPlatforms.All;

            _entry1.Platforms.Should().Be(supportedPlatforms);
            _entry2.Platforms.Should().Be(supportedPlatforms);
            _entry3.Platforms.Should().Be(supportedPlatforms);
        }

        [Fact]
        public void DifferentLanguagesShouldHaveSameReviewPositivePercentage()
        {
            _entry1.ReviewPositivePercentage.Should().BeGreaterThan(0);
            _entry1.ReviewPositivePercentage.Should().Be(_entry2.ReviewPositivePercentage);
            _entry1.ReviewPositivePercentage.Should().Be(_entry3.ReviewPositivePercentage);
        }

        [Fact]
        public void DifferentLanguagesShouldHaveSameReviewTotal()
        {
            _entry1.ReviewTotal.Should().BeGreaterThan(0);
            _entry2.ReviewTotal.Should().BeInRange((int) Math.Round(_entry1.ReviewTotal * 0.995), (int) Math.Round(_entry1.ReviewTotal * 1.005));
            _entry3.ReviewTotal.Should().BeInRange((int) Math.Round(_entry1.ReviewTotal * 0.995), (int) Math.Round(_entry1.ReviewTotal * 1.005));
        }

        [Fact]
        public void DifferentLanguagesShouldHaveSameTotalAchievements()
        {
            _entry1.TotalAchievements.Should().BeGreaterThan(0);
            _entry2.TotalAchievements.Should().Be(_entry1.TotalAchievements);
            _entry3.TotalAchievements.Should().Be(_entry1.TotalAchievements);
        }

        #endregion
    }
}
