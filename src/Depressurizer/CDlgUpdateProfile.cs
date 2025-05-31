using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml.XPath;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Interfaces;
using Depressurizer.Core.Models;
using Depressurizer.Dialogs;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Depressurizer
{
    internal class CDlgUpdateProfile : CancelableDialog
    {
        #region Fields

        private readonly IGameList data;

        private readonly SortedSet<int> ignore;

        private readonly bool overwrite;

        private readonly long SteamId;

        private IXPathNavigable doc;

        private GetOwnedGamesObject ownedGamesObject;

        #endregion

        #region Constructors and Destructors

        public CDlgUpdateProfile(IGameList data, long accountId, bool overwrite, SortedSet<int> ignore) : base(GlobalStrings.CDlgUpdateProfile_UpdatingGameList, true)
        {
            SteamId = accountId;

            this.data = data;

            this.overwrite = overwrite;
            this.ignore = ignore;

            SetText(GlobalStrings.CDlgFetch_DownloadingGameList);
        }

        #endregion

        #region Public Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Added { get; private set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:139.0) Gecko/20100101 Firefox/139.0");
                using (Stream s = client.GetStreamAsync(string.Format(Constants.SteamWebApiOwnedGames, FormMain.CurrentProfile.SteamWebApiKey, SteamId)).Result)
                using (StreamReader sr = new StreamReader(s))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    ownedGamesObject = serializer.Deserialize<GetOwnedGamesObject>(reader);
                }
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
}
