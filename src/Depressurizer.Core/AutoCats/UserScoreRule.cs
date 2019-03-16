namespace Depressurizer.Core.AutoCats
{
    public class UserScoreRule : AutoCatRule
    {
        #region Constructors and Destructors

        public UserScoreRule(string name, int minScore, int maxScore, int minReviews, int maxReviews)
        {
            Name = name;
            MinScore = minScore;
            MaxScore = maxScore;
            MinReviews = minReviews;
            MaxReviews = maxReviews;
        }

        public UserScoreRule(UserScoreRule other)
        {
            Name = other.Name;
            MinScore = other.MinScore;
            MaxScore = other.MaxScore;
            MinReviews = other.MinReviews;
            MaxReviews = other.MaxReviews;
        }

        /// <summary>
        ///     Parameter-less constructor for XmlSerializer.
        /// </summary>
        private UserScoreRule() { }

        #endregion

        #region Public Properties

        public int MaxReviews { get; set; }

        public int MaxScore { get; set; }

        public int MinReviews { get; set; }

        public int MinScore { get; set; }

        #endregion
    }
}
