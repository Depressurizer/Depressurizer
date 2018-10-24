#region LICENSE

//     This file (CDlgGetSteamID.cs) is part of Depressurizer.
//     Copyright (C) 2011 Steve Labbe
//     Copyright (C) 2017 Theodoros Dimos
//     Copyright (C) 2017 Martijn Vegter
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Net;
using System.Xml;
using Depressurizer.Properties;
using Rallion;

namespace Depressurizer
{
    internal class CDlgGetSteamID : CancelableDlg
    {
        #region Fields

        private readonly string customUrlName;

        #endregion

        #region Constructors and Destructors

        public CDlgGetSteamID(string customUrl) : base(GlobalStrings.CDlgGetSteamID_GettingSteamID, false)
        {
            SteamID = 0;
            Success = false;
            customUrlName = customUrl;

            SetText(GlobalStrings.CDlgGetSteamID_GettingIDFromURL);
        }

        #endregion

        #region Public Properties

        public long SteamID { get; private set; }

        public bool Success { get; private set; }

        #endregion

        #region Methods

        protected override void Finish()
        {
            if (!Canceled)
            {
                OnJobCompletion();
            }
        }

        protected override void RunProcess()
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                string url = string.Format(Resources.UrlCustomProfileXml, customUrlName);
                Program.Logger.Write(LoggerLevel.Info, GlobalStrings.CDlgGetSteamID_AttemptingDownloadXMLProfile, customUrlName, url);
                WebRequest req = WebRequest.Create(url);
                WebResponse response = req.GetResponse();
                doc.Load(response.GetResponseStream());
                response.Close();
                Program.Logger.Write(LoggerLevel.Info, GlobalStrings.CDlgGetSteamID_XMLProfileDownloaded);
            }
            catch (Exception e)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.CDlgGetSteamID_ExceptionDownloadingXMLProfile, e.Message);
                throw new ApplicationException(GlobalStrings.CDlgGetSteamID_FailedToDownloadProfile + e.Message, e);
            }

            XmlNode idNode = doc.SelectSingleNode("/profile/steamID64");
            if (idNode != null)
            {
                long tmp;
                Success = long.TryParse(idNode.InnerText, out tmp);
                if (Success)
                {
                    SteamID = tmp;
                }
            }

            OnThreadCompletion();
        }

        #endregion
    }
}
