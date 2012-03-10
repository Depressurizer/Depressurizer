using System;
/*
Copyright 2011, 2012 Steve Labbe.

This file is part of Depressurizer.

Depressurizer is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Depressurizer is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Depressurizer.  If not, see <http://www.gnu.org/licenses/>.
*/
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
            try {
                XmlDocument d = GameDB.FetchAppList();
                lock( abortLock ) {
                    if( !Aborted ) {
                        DisableAbort();
                        games.IntegrateAppList( d );
                        CompleteJob();
                    }
                }
            } catch( Exception e ) {
                this.Error = e;
            }
            EndThread();
        }
    }
}