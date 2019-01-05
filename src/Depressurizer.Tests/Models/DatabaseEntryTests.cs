using System;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
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
            _entry1.ScrapeStore(Language.LanguageCode(StoreLanguage.English));
            _entry2.ScrapeStore(Language.LanguageCode(StoreLanguage.Dutch));
            _entry3.ScrapeStore(Language.LanguageCode(StoreLanguage.ChineseSimplified));
        }

        #endregion

        #region Public Methods and Operators

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

        #endregion
    }
}
