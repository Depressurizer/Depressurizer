#region LICENSE

//     This file (CDlgFetch.cs) is part of Depressurizer.
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

using System.Xml;
using Rallion;

namespace Depressurizer
{
    internal class FetchPrcDlg : CancelableDlg
    {
        #region Fields

        private XmlDocument doc;

        #endregion

        #region Constructors and Destructors

        public FetchPrcDlg() : base(GlobalStrings.CDlgFetch_UpdatingGameList, false)
        {
            SetText(GlobalStrings.CDlgFetch_DownloadingGameList);
            Added = 0;
        }

        #endregion

        #region Public Properties

        public int Added { get; private set; }

        #endregion

        #region Methods

        protected override void Finish()
        {
            if (!Canceled && doc != null && Error == null)
            {
                SetText(GlobalStrings.CDlgFetch_FinishingDownload);
                Added = Program.Database.IntegrateAppList(doc);
                OnJobCompletion();
            }
        }

        protected override void RunProcess()
        {
            Added = 0;
            doc = Database.FetchAppListFromWeb();
            OnThreadCompletion();
        }

        #endregion
    }
}
