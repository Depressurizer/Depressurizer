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

namespace Depressurizer {
    class FetchPrcDlg : CancelableDlg {
        public int Added { get; private set; }

        public FetchPrcDlg()
            : base( "Updating Game List" ) {
            SetText( "Downloading game list..." );
            Added = 0;
        }

        protected override void RunProcess() {
            try {
                Added = 0;
                XmlDocument d = GameDB.FetchAppList();
                lock( abortLock ) {
                    if( !Aborted ) {
                        DisableAbort();
                        Added = Program.GameDB.IntegrateAppList( d );
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