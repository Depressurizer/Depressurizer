/*
Copyright 2011, 2012, 2013 Steve Labbe.

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
using System.Xml;
using Rallion;

namespace Depressurizer {
    class FetchPrcDlg : CancelableDlg {
        public int Added { get; private set; }
        XmlDocument doc;

        public FetchPrcDlg()
            : base( "Updating Game List", false ) {
            SetText( "Downloading game list..." );
            Added = 0;
        }

        protected override void RunProcess() {
            Added = 0;
            doc = GameDB.FetchAppList();
            OnThreadCompletion();
        }

        protected override void Finish() {
            if( !this.Canceled && doc != null && Error == null ) {
                SetText( "Finishing download..." );
                Added = Program.GameDB.IntegrateAppList( doc );
                OnJobCompletion();
            }
        }
    }
}