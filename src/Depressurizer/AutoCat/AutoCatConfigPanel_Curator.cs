using System;
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class AutoCatConfigPanel_Curator : AutoCatConfigPanel
    {
        #region Constructors and Destructors

        public AutoCatConfigPanel_Curator()
        {
            InitializeComponent();

            // Set up help tooltips
            ttHelp.Ext_SetToolTip(helpCategoryName, GlobalStrings.AutoCatCurator_Help_CategoryName);
            ttHelp.Ext_SetToolTip(helpCuratorUrl, "e.g http://store.steampowered.com/curator/6090344-depressurizer/");

            FillRecommendationsList();
        }

        #endregion

        #region Public Methods and Operators

        public void FillRecommendationsList()
        {
            lstIncluded.Items.Clear();
            lstIncluded.Items.Add(Utility.GetEnumDescription(CuratorRecommendation.Recommended));
            lstIncluded.Items[0].Tag = Enum.GetName(typeof(CuratorRecommendation), CuratorRecommendation.Recommended);
            lstIncluded.Items.Add(Utility.GetEnumDescription(CuratorRecommendation.NotRecommended));
            lstIncluded.Items[1].Tag = Enum.GetName(typeof(CuratorRecommendation), CuratorRecommendation.NotRecommended);
            lstIncluded.Items.Add(Utility.GetEnumDescription(CuratorRecommendation.Informational));
            lstIncluded.Items[2].Tag = Enum.GetName(typeof(CuratorRecommendation), CuratorRecommendation.Informational);
        }

        public override void LoadFromAutoCat(AutoCat autoCat)
        {
            AutoCatCurator ac = autoCat as AutoCatCurator;
            if (ac == null)
            {
                return;
            }

            txtCategoryName.Text = ac.CategoryName;
            txtCuratorUrl.Text = ac.CuratorUrl;

            foreach (CuratorRecommendation rec in ac.IncludedRecommendations)
            {
                lstIncluded.Items[rec.GetHashCode() - 1].Checked = true;
            }
        }

        public override void SaveToAutoCat(AutoCat autoCat)
        {
            AutoCatCurator ac = autoCat as AutoCatCurator;
            if (ac == null)
            {
                return;
            }

            ac.CategoryName = txtCategoryName.Text;
            ac.CuratorUrl = txtCuratorUrl.Text;

            ac.IncludedRecommendations.Clear();
            foreach (ListViewItem i in lstIncluded.Items)
            {
                if (i.Checked)
                {
                    ac.IncludedRecommendations.Add((CuratorRecommendation) Enum.Parse(typeof(CuratorRecommendation), i.Tag.ToString()));
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
