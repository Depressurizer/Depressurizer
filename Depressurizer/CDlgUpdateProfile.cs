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
using System.Collections.Generic;
using System.Xml;
using Rallion;

namespace Depressurizer {

    class CDlgUpdateProfile : CancelableDlg {
        public int Fetched { get; private set; }
        public int Added { get; private set; }

        public bool UseHtml { get; private set; }
        public bool Failover { get; private set; }

        private Int64 SteamId;
        private string customUrl;
        private bool custom;
        private GameData data;

        XmlDocument doc;
        string htmlDoc;

        private bool overwrite;
        private SortedSet<int> ignore;
        private bool ignoreDlc;

        public CDlgUpdateProfile( GameData data, Int64 accountId, bool overwrite, SortedSet<int> ignore, bool ignoreDlc )
            : base( "Updating game list...", true ) {
            custom = false;
            this.SteamId = accountId;
            
            Added = 0;
            Fetched = 0;
            UseHtml = false;
            Failover = false;

            this.data = data;
            
            this.overwrite = overwrite;
            this.ignore = ignore;
            this.ignoreDlc = ignoreDlc;

            SetText( "Downloading game list..." );
        }

        public CDlgUpdateProfile( GameData data, string customUrl, bool overwrite, SortedSet<int> ignore, bool ignoreDlc )
            : base( "Updating game list...", true ) {
            custom = true;
            this.customUrl = customUrl;

            Added = 0;
            Fetched = 0;
            UseHtml = false;
            Failover = false;

            this.data = data;

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
            if( custom ) {
                doc = GameData.FetchXmlGameList( customUrl );
            } else {
                doc = GameData.FetchXmlGameList( SteamId );
            }
        }

        protected void FetchHtml() {
            UseHtml = true;
            if( custom ) {
                htmlDoc = GameData.FetchHtmlGameList( customUrl );
            } else {
                htmlDoc = GameData.FetchHtmlGameList( SteamId );
            }
        }

        protected void FetchXmlPref() {
            try {
                FetchXml();
                return;
            } catch( ProfileAccessException e ) {
                throw e;
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
