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
using System;
using System.Collections.Generic;
using System.Xml;
using Rallion;

namespace Depressurizer {

    class UpdateProfileDlg : CancelableDlg {
        public int Added { get; private set; }

        private string profileName;
        private GameData data;

        XmlDocument doc;

        private bool overwrite;
        private SortedSet<int> ignore;
        private bool ignoreDlc;

        public UpdateProfileDlg( GameData data, string profileName, bool overwrite, SortedSet<int> ignore, bool ignoreDlc )
            : base( "Updating game list...", true ) {
            Added = 0;
            this.data = data;
            this.profileName = profileName;
            this.overwrite = overwrite;
            this.ignore = ignore;
            this.ignoreDlc = ignoreDlc;

            SetText( "Downloading game list..." );
        }

        protected override void RunProcess() {
            Added = 0;
            doc = GameData.FetchGameList( profileName );
            OnThreadCompletion();
        }

        protected override void Finish() {
            if( !Canceled && Error == null && doc != null ) {
                SetText( "Finishing download..." );
                Added = data.IntegrateXmlGameList( doc, overwrite, ignore, ignoreDlc );
                OnJobCompletion();
            }
        }

    }
}
