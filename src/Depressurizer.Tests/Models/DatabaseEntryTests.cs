using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Models;
using FluentAssertions;
using Xunit;

namespace Depressurizer.Tests.Models
{
    public class DatabaseEntryTests
    {
        #region Public Methods and Operators

        [Fact]
        public void DifferentLanguagesShouldHaveSameReviewPositivePercentage()
        {
            DatabaseEntry entry1 = new DatabaseEntry(730); // CS:GO
            DatabaseEntry entry2 = new DatabaseEntry(730); // CS:GO
            DatabaseEntry entry3 = new DatabaseEntry(730); // CS:GO

            entry1.ScrapeStore(Language.LanguageCode(StoreLanguage.English));
            entry2.ScrapeStore(Language.LanguageCode(StoreLanguage.Dutch));
            entry3.ScrapeStore(Language.LanguageCode(StoreLanguage.ChineseSimplified));

            entry1.ReviewPositivePercentage.Should().Be(entry2.ReviewPositivePercentage);
            entry1.ReviewPositivePercentage.Should().Be(entry3.ReviewPositivePercentage);
        }

        [Fact]
        public void DifferentLanguagesShouldHaveSameReviewTotal()
        {
            DatabaseEntry entry1 = new DatabaseEntry(730); // CS:GO
            DatabaseEntry entry2 = new DatabaseEntry(730); // CS:GO
            DatabaseEntry entry3 = new DatabaseEntry(730); // CS:GO

            entry1.ScrapeStore(Language.LanguageCode(StoreLanguage.English));
            entry2.ScrapeStore(Language.LanguageCode(StoreLanguage.Dutch));
            entry3.ScrapeStore(Language.LanguageCode(StoreLanguage.ChineseSimplified));

            entry1.ReviewTotal.Should().Be(entry2.ReviewTotal);
            entry1.ReviewTotal.Should().Be(entry3.ReviewTotal);
        }

        #endregion
    }
}
