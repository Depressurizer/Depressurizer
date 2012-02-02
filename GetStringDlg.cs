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
