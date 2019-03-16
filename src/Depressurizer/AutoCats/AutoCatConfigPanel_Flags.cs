using System;
using System.Windows.Forms;

namespace Depressurizer.AutoCats
{
    public partial class AutoCatConfigPanel_Flags : AutoCatConfigPanel
    {
        #region Constructors and Destructors

        public AutoCatConfigPanel_Flags()
        {
            InitializeComponent();

            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);

            FillFlagsList();
        }

        #endregion

        #region Properties

        private static Database Database => Database.Instance;

        #endregion

        #region Public Methods and Operators

        public void FillFlagsList()
        {
            lstIncluded.Items.Clear();

            foreach (string flag in Database.AllFlags)
            {
                lstIncluded.Items.Add(flag);
            }
        }

        public override void LoadFromAutoCat(AutoCat autoCat)
        {
            AutoCatFlags ac = autoCat as AutoCatFlags;
            if (ac == null)
            {
                return;
            }

            txtPrefix.Text = ac.Prefix;

            foreach (ListViewItem item in lstIncluded.Items)
            {
                item.Checked = ac.IncludedFlags.Contains(item.Text);
            }
        }

        public override void SaveToAutoCat(AutoCat autoCat)
        {
            AutoCatFlags ac = autoCat as AutoCatFlags;
            if (ac == null)
            {
                return;
            }

            ac.Prefix = txtPrefix.Text;

            ac.IncludedFlags.Clear();
            foreach (ListViewItem i in lstIncluded.Items)
            {
                if (i.Checked)
                {
                    ac.IncludedFlags.Add(i.Text);
                }
            }
        }

        #endregion

        #region Methods

        private void cmdCheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstIncluded, true);
        }

        private void cmdUncheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstIncluded, false);
        }

        private void SetAllListCheckStates(ListView list, bool to)
        {
            foreach (ListViewItem item in list.Items)
            {
                item.Checked = to;
            }
        }

        #endregion
    }
}
