namespace Depressurizer.Core.Models
{
    public class ScrapeJob
    {
        #region Constructors and Destructors

        public ScrapeJob(int id, int scrapeId)
        {
            Id = id;
            ScrapeId = scrapeId;
        }

        #endregion

        #region Public Properties

        public int Id { get; }

        public int ScrapeId { get; }

        #endregion
    }
}
