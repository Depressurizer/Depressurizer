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
using System.Windows.Forms;

namespace Depressurizer {
    public partial class GetStringDlg : Form {

        public string Value {
            get {
                return txtValue.Text;
            }
            set {
                txtValue.Text = ( value == null ) ? string.Empty : value;
            }
        }

        public string LabelText {
            set {
                lblValue.Text = ( value == null ) ? string.Empty : value;
            }
        }

        public string AcceptButtonText {
            set {
                cmdOk.Text = ( value == null ) ? string.Empty : value;
            }
        }

        public GetStringDlg( string initialValue = "", string title = "Enter value", string label = "Enter value:", string accept = "OK" ) {
            InitializeComponent();
            Value = initialValue;
            Text = title;
            LabelText = label;
            AcceptButtonText = accept;
        }
    }
}
