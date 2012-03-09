using System.Xml;
using DPLib;

namespace SteamScrape {
    class FetchPrcDlg : CancelableDlg {
        GameDB games;

        public FetchPrcDlg( GameDB games )
            : base( "Updating Game List" ) {
            this.games = games;
            SetText( "Downloading game list..." );
        }

        protected override void RunProcess() {
            XmlDocument d = GameDB.FetchAppList();
            bool completed = false;
            lock( abortLock ) {
                if( !Aborted ) {
                    DisableAbort();
                    games.IntegrateAppList( d );
                    CompleteJob();
                    completed = true;
                }
            }
            if( completed ) {
                EndThread();
            }
        }
    }
}