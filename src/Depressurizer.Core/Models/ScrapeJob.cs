namespace Depressurizer.Core.Models
{
    public class ScrapeJob
    {
        #region Constructors and Destructors

        public ScrapeJob(long id, long scrapeId)
        {
            Id = id;
            ScrapeId = scrapeId;
        }

        #endregion

        #region Public Properties

        public long Id { get; }

        public long ScrapeId { get; }

        #endregion
    }
}
