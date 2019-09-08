using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml.XPath;
using Depressurizer.Core.Helpers;
using Depressurizer.Dialogs;
using Newtonsoft.Json;

namespace Depressurizer
{
    internal class CDlgUpdateProfile : CancelableDialog
    {
        #region Fields

        private readonly GameList data;

        private readonly SortedSet<int> ignore;

        private readonly bool overwrite;

        private readonly long SteamId;

        private IXPathNavigable doc;

        private GetOwnedGamesObject ownedGamesObject;

        #endregion

        #region Constructors and Destructors

        public CDlgUpdateProfile(GameList data, long accountId, bool overwrite, SortedSet<int> ignore) : base(GlobalStrings.CDlgUpdateProfile_UpdatingGameList, true)
        {
            SteamId = accountId;

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
            if (FormMain.ProfileLoaded && string.IsNullOrWhiteSpace(FormMain.CurrentProfile.SteamWebApiKey))
            {
                using (SteamKeyDialog dialog = new SteamKeyDialog())
                {
                    dialog.ShowDialog();
                }
            }

            if (FormMain.ProfileLoaded && !string.IsNullOrWhiteSpace(FormMain.CurrentProfile.SteamWebApiKey))
            {
                Logger.Info("Updating profile using Steam Web API!");

                string json;
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(string.Format(Constants.SteamWebApiOwnedGames, FormMain.CurrentProfile.SteamWebApiKey, SteamId));
                }

                ownedGamesObject = JsonConvert.DeserializeObject<GetOwnedGamesObject>(json);
            }
            else
            {
                doc = GameList.FetchGameList(SteamId);
            }
        }

        protected override void Finish()
        {
            if (Canceled || Error != null || doc == null && ownedGamesObject == null)
            {
                return;
            }

            SetText(GlobalStrings.CDlgFetch_FinishingDownload);

            int newItems;
            if (doc != null)
            {
                Fetched = data.IntegrateGameList(doc, overwrite, ignore, out newItems);
            }
            else
            {
                Fetched = data.IntegrateGameList(ownedGamesObject, overwrite, ignore, out newItems);
            }

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

    public class GetOwnedGamesObject
    {
        #region Public Properties

        public Response response { get; set; }

        #endregion

        public class Game
        {
            #region Public Properties

            public int appid { get; set; }

            public bool has_community_visible_stats { get; set; }

            public string img_icon_url { get; set; }

            public string img_logo_url { get; set; }

            public string name { get; set; }

            public int playtime_2weeks { get; set; }

            public int playtime_forever { get; set; }

            public int playtime_linux_forever { get; set; }

            public int playtime_mac_forever { get; set; }

            public int playtime_windows_forever { get; set; }

            #endregion
        }

        public class Response
        {
            #region Public Properties

            public int game_count { get; set; }

            public Game[] games { get; set; }

            #endregion
        }
    }
}
