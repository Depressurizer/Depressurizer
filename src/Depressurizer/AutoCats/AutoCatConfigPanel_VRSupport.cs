using System;
using System.Windows.Forms;
using Depressurizer.Core.Models;

namespace Depressurizer.AutoCats
{
    public partial class AutoCatConfigPanel_VrSupport : AutoCatConfigPanel
    {
        #region Constructors and Destructors

        public AutoCatConfigPanel_VrSupport()
        {
            InitializeComponent();

            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);

            FillVrSupportLists();
        }

        #endregion

        #region Properties

        private static Database Database => Database.Instance;

        #endregion

        #region Public Methods and Operators

        public void FillVrSupportLists()
        {
            lstVrHeadsets.Items.Clear();
            lstVrInput.Items.Clear();
            lstVrPlayArea.Items.Clear();

            VRSupport vrSupport = Database.AllVRSupport;

            foreach (string s in vrSupport.Headsets)
            {
                lstVrHeadsets.Items.Add(s);
            }

            foreach (string s in vrSupport.Input)
            {
                lstVrInput.Items.Add(s);
            }

            foreach (string s in vrSupport.PlayArea)
            {
                lstVrPlayArea.Items.Add(s);
            }
        }

        public override void LoadFromAutoCat(AutoCat autoCat)
        {
            AutoCatVrSupport ac = autoCat as AutoCatVrSupport;
            if (ac == null)
            {
                return;
            }

            txtPrefix.Text = ac.Prefix;

            foreach (ListViewItem item in lstVrHeadsets.Items)
            {
                item.Checked = ac.IncludedVRSupportFlags.Headsets.Contains(item.Text);
            }

            foreach (ListViewItem item in lstVrInput.Items)
            {
                item.Checked = ac.IncludedVRSupportFlags.Input.Contains(item.Text);
            }

            foreach (ListViewItem item in lstVrPlayArea.Items)
            {
                item.Checked = ac.IncludedVRSupportFlags.PlayArea.Contains(item.Text);
            }
        }

        public override void SaveToAutoCat(AutoCat autoCat)
        {
            AutoCatVrSupport ac = autoCat as AutoCatVrSupport;
            if (ac == null)
            {
                return;
            }

            ac.Prefix = txtPrefix.Text;

            ac.IncludedVRSupportFlags.Headsets.Clear();
            ac.IncludedVRSupportFlags.Input.Clear();
            ac.IncludedVRSupportFlags.PlayArea.Clear();

            foreach (ListViewItem i in lstVrHeadsets.Items)
            {
                if (i.Checked)
                {
                    ac.IncludedVRSupportFlags.Headsets.Add(i.Text);
                }
            }

            foreach (ListViewItem i in lstVrInput.Items)
            {
                if (i.Checked)
                {
                    ac.IncludedVRSupportFlags.Input.Add(i.Text);
                }
            }

            foreach (ListViewItem i in lstVrPlayArea.Items)
            {
                if (i.Checked)
                {
                    ac.IncludedVRSupportFlags.PlayArea.Add(i.Text);
                }
            }
        }

        #endregion

        #region Methods

        private void cmdCheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstVrHeadsets, true);
            SetAllListCheckStates(lstVrInput, true);
            SetAllListCheckStates(lstVrPlayArea, true);
        }

        private void cmdUncheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstVrHeadsets, false);
            SetAllListCheckStates(lstVrInput, false);
            SetAllListCheckStates(lstVrPlayArea, false);
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
