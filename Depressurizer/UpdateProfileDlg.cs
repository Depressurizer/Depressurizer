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
        public int Fetched { get; private set; }
        public int Added { get; private set; }

        public bool UseHtml { get; private set; }
        public bool Failover { get; private set; }

        private string profileName;
        private GameData data;

        XmlDocument doc;
        string htmlDoc;

        private bool overwrite;
        private SortedSet<int> ignore;
        private bool ignoreDlc;

        public UpdateProfileDlg( GameData data, string profileName, bool overwrite, SortedSet<int> ignore, bool ignoreDlc )
            : base( "Updating game list...", true ) {
            Added = 0;
            Fetched = 0;
            UseHtml = false;
            Failover = false;

            this.data = data;
            this.profileName = profileName;
            this.overwrite = overwrite;
            this.ignore = ignore;
            this.ignoreDlc = ignoreDlc;

            SetText( "Downloading game list..." );
        }

        protected override void RunProcess() {
            Added = 0;
            Fetched = 0;
            switch( Settings.Instance().ListSource ) {
                case GameListSource.XmlPreferred:
                    FetchXmlPref();
                    break;
                case GameListSource.XmlOnly:
                    FetchXml();
                    break;
                case GameListSource.WebsiteOnly:
                    FetchHtml();
                    break;
            }
            OnThreadCompletion();
        }

        protected void FetchXml() {
            UseHtml = false;
            doc = GameData.FetchXmlGameList( profileName );
        }

        protected void FetchHtml() {
            UseHtml = true;
            htmlDoc = GameData.FetchHtmlGameList( profileName );
        }

        protected void FetchXmlPref() {
            try {
                FetchXml();
                return;
            } catch( Exception ) { }
            Failover = true;
            FetchHtml();
        }

        protected override void Finish() {
            if( !Canceled && Error == null && ( UseHtml ? ( htmlDoc != null ) : ( doc != null ) ) ) {
                SetText( "Finishing download..." );
                if( UseHtml ) {
                    int newItems;
                    Fetched = data.IntegrateHtmlGameList( htmlDoc, overwrite, ignore, ignoreDlc, out newItems );
                    Added = newItems;
                } else {
                    int newItems;
                    Fetched = data.IntegrateXmlGameList( doc, overwrite, ignore, ignoreDlc, out newItems );
                    Added = newItems;
                }
                OnJobCompletion();
            }
        }
    }
}
