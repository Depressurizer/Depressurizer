#region LICENSE

//     This file (DlgGetString.cs) is part of Depressurizer.
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

using System.Windows.Forms;

namespace Rallion
{
    public partial class GetStringDlg : Form
    {
        #region Constructors and Destructors

        public GetStringDlg(string initialValue = "", string title = "Enter value", string label = "Enter value:", string accept = "OK")
        {
            InitializeComponent();
            Value = initialValue;
            Text = title;
            LabelText = label;
            AcceptButtonText = accept;
        }

        #endregion

        #region Public Properties

        public string AcceptButtonText
        {
            set => cmdOk.Text = value == null ? string.Empty : value;
        }

        public string LabelText
        {
            set => lblValue.Text = value == null ? string.Empty : value;
        }

        public string Value
        {
            get => txtValue.Text;
            set => txtValue.Text = value == null ? string.Empty : value;
        }

        #endregion
    }
}
