using System.Collections.Generic;
using System.Xml.XPath;
using Rallion;

namespace Depressurizer
{
    internal class CDlgUpdateProfile : CancelableDlg
    {
        #region Fields

        private readonly bool custom;

        private readonly string customUrl;

        private readonly GameList data;

        private readonly SortedSet<int> ignore;

        private readonly bool overwrite;

        private readonly long SteamId;

        private IXPathNavigable doc;

        #endregion

        #region Constructors and Destructors

        public CDlgUpdateProfile(GameList data, long accountId, bool overwrite, SortedSet<int> ignore) : base(GlobalStrings.CDlgUpdateProfile_UpdatingGameList, true)
        {
            custom = false;
            SteamId = accountId;

            this.data = data;

            this.overwrite = overwrite;
            this.ignore = ignore;

            SetText(GlobalStrings.CDlgFetch_DownloadingGameList);
        }

        public CDlgUpdateProfile(GameList data, string customUrl, bool overwrite, SortedSet<int> ignore) : base(GlobalStrings.CDlgUpdateProfile_UpdatingGameList, true)
        {
            custom = true;
            this.customUrl = customUrl;

            this.data = data;

            this.overwrite = overwrite;
            this.ignore = ignore;

            SetText(GlobalStrings.CDlgFetch_DownloadingGameList);
        }

        #endregion

        #region Public Properties

        public int Added { get; private set; }

        public int Fetched { get; private set; }

        #endregion

        #region Methods

        protected void Fetch()
        {
            doc = custom ? GameList.FetchGameList(customUrl) : GameList.FetchGameList(SteamId);
        }

        protected override void Finish()
        {
            if (Canceled || Error != null || doc == null)
            {
                return;
            }

            SetText(GlobalStrings.CDlgFetch_FinishingDownload);

            Fetched = data.IntegrateGameList(doc, overwrite, ignore, out int newItems);
            Added = newItems;

            OnJobCompletion();
        }

        protected override void RunProcess()
        {
            Added = 0;
            Fetched = 0;

            Fetch();

            OnThreadCompletion();
        }

        #endregion
    }
}
