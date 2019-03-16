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
            get => cmdOk.Text;
            set => cmdOk.Text = value ?? string.Empty;
        }

        public string LabelText
        {
            get => lblValue.Text;
            set => lblValue.Text = value ?? string.Empty;
        }

        public string Value
        {
            get => txtValue.Text;
            set => txtValue.Text = value ?? string.Empty;
        }

        #endregion
    }
}
