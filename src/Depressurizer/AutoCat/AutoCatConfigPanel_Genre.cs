using System;
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class AutoCatConfigPanel_Genre : AutoCatConfigPanel
    {
        #region Constructors and Destructors

        public AutoCatConfigPanel_Genre()
        {
            InitializeComponent();

            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);
            ttHelp.Ext_SetToolTip(helpRemoveExisting, GlobalStrings.DlgAutoCat_Help_Genre_RemoveExisting);
            ttHelp.Ext_SetToolTip(helpTagFallback, GlobalStrings.AutoCatGenrePanel_Help_TagFallback);

            FillGenreList();
        }

        #endregion

        #region Properties

        private static Database Database => Database.Instance;

        #endregion

        #region Public Methods and Operators

        public void FillGenreList()
        {
            lstIgnore.Items.Clear();

            foreach (string genre in Database.AllGenres)
            {
                ListViewItem listViewItem = new ListViewItem
                {
                    Text = genre,
                    Checked = true
                };

                lstIgnore.Items.Add(listViewItem);
            }
        }

        public override void LoadFromAutoCat(AutoCat autoCat)
        {
            AutoCatGenre ac = autoCat as AutoCatGenre;
            if (ac == null)
            {
                return;
            }

            chkRemoveExisting.Checked = ac.RemoveOtherGenres;
            chkTagFallback.Checked = ac.TagFallback;
            numMaxCats.Value = ac.MaxCategories;
            txtPrefix.Text = ac.Prefix;

            foreach (ListViewItem item in lstIgnore.Items)
            {
                item.Checked = !ac.IgnoredGenres.Contains(item.Text);
            }
        }

        public override void SaveToAutoCat(AutoCat autoCat)
        {
            AutoCatGenre ac = autoCat as AutoCatGenre;
            if (ac == null)
            {
                return;
            }

            ac.Prefix = txtPrefix.Text;
            ac.MaxCategories = (int) numMaxCats.Value;
            ac.RemoveOtherGenres = chkRemoveExisting.Checked;
            ac.TagFallback = chkTagFallback.Checked;

            ac.IgnoredGenres.Clear();
            foreach (ListViewItem i in lstIgnore.Items)
            {
                if (!i.Checked)
                {
                    ac.IgnoredGenres.Add(i.Text);
                }
            }
        }

        #endregion

        #region Methods

        private void cmdCheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstIgnore, true);
        }

        private void cmdUncheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstIgnore, false);
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
