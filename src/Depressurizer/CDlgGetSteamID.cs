using System;
using System.Globalization;
using System.Net;
using System.Xml;
using Depressurizer.Core.Helpers;
using Depressurizer.Dialogs;
using System.ComponentModel;

namespace Depressurizer
{
    internal class CDlgGetSteamID : CancelableDialog
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public long SteamID { get; private set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
                string url = string.Format(CultureInfo.InvariantCulture, Constants.SteamProfileCustom, customUrlName);
                Logger.Info(GlobalStrings.CDlgGetSteamID_AttemptingDownloadXMLProfile, customUrlName, url);
                WebRequest req = WebRequest.Create(url);
                WebResponse response = req.GetResponse();
                doc.Load(response.GetResponseStream());
                response.Close();
                Logger.Info(GlobalStrings.CDlgGetSteamID_XMLProfileDownloaded);
            }
            catch (Exception e)
            {
                Logger.Error(GlobalStrings.CDlgGetSteamID_ExceptionDownloadingXMLProfile, e.Message);
                throw new ApplicationException(GlobalStrings.CDlgGetSteamID_FailedToDownloadProfile + e.Message, e);
            }

            XmlNode idNode = doc.SelectSingleNode("/profile/steamID64");
            if (idNode != null)
            {
                Success = long.TryParse(idNode.InnerText, out long tmp);
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
