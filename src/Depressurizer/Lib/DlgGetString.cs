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
