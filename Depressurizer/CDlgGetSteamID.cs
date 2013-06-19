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
using System.Net;
using System.Xml;
using Rallion;

namespace Depressurizer {
    class CDlgGetSteamID : CancelableDlg {

        public Int64 SteamID { get; private set; }
        private string customUrlName;
        public bool Success { get; private set; }

        public CDlgGetSteamID( string customUrl )
            : base( "Getting Steam ID...", false ) {
            SteamID = 0;
            Success = false;
            customUrlName = customUrl;

            SetText( "Getting Steam ID from Custom URL..." );
        }

        protected override void RunProcess() {
            XmlDocument doc = new XmlDocument();

            try {
                string url = string.Format( Properties.Resources.UrlCustomProfileXml, customUrlName );
                Program.Logger.Write( LoggerLevel.Info, "Attempting to download XML profile page for custom URL name {0}: {1}", customUrlName, url );
                WebRequest req = HttpWebRequest.Create( url );
                WebResponse response = req.GetResponse();
                doc.Load( response.GetResponseStream() );
                response.Close();
                Program.Logger.Write( LoggerLevel.Info, "Successfully downloaded XML profile." );
            } catch( Exception e ) {
                Program.Logger.Write( LoggerLevel.Error, "Exception when downloading XML profile:\n{0}", e.Message );
                throw new ApplicationException( "Failed to download profile data: " + e.Message, e );
            }

            XmlNode idNode = doc.SelectSingleNode( "/profile/steamID64" );
            if( idNode != null ) {
                Int64 tmp;
                Success = Int64.TryParse( idNode.InnerText, out tmp );
                if( Success ) {
                    SteamID = tmp;
                }
            }

            OnThreadCompletion();
        }

        protected override void Finish() {
            if( !Canceled ) {
                OnJobCompletion();
            }
        }

    }
}
