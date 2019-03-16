using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Depressurizer.AutoCats
{
    public partial class AutoCatConfigPanel_Group : AutoCatConfigPanel
    {
        #region Fields

        //private List<string> stringAutocats;
        private readonly List<AutoCat> Autocats;

        private AutoCat current;

        #endregion

        #region Constructors and Destructors

        public AutoCatConfigPanel_Group(List<AutoCat> autocats)
        {
            InitializeComponent();

            Autocats = autocats;
        }

        #endregion

        #region Public Properties

        public List<string> Groups
        {
            get
            {
                List<string> group = new List<string>();
                foreach (string name in lbAutocats.Items)
                {
                    group.Add(name);
                }

                return group;
            }
        }

        #endregion

        #region Public Methods and Operators

        public override void LoadFromAutoCat(AutoCat autoCat)
        {
            AutoCatGroup ac = autoCat as AutoCatGroup;
            current = autoCat;
            if (ac == null)
            {
                return;
            }

            FillAutocatList(ac.Autocats);
        }

        public override void SaveToAutoCat(AutoCat autoCat)
        {
            if (!(autoCat is AutoCatGroup ac))
            {
                return;
            }

            ac.Autocats = Groups;
        }

        #endregion

        #region Methods

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (DlgAutoCatSelect dialog = new DlgAutoCatSelect(Autocats, current.Name))
            {
                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK)
                {
                    return;
                }

                foreach (AutoCat autoCat in dialog.AutoCatList)
                {
                    if (autoCat.Selected && !InGroup(autoCat.Name))
                    {
                        lbAutocats.Items.Add(autoCat.Name);
                    }
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            Utility.MoveItem(lbAutocats, 1);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbAutocats.SelectedItems.Count > 1)
            {
                foreach (string s in lbAutocats.SelectedItems)
                {
                    lbAutocats.Items.Remove(s);
                }
            }
            else if (lbAutocats.SelectedItem != null)
            {
                lbAutocats.Items.Remove(lbAutocats.SelectedItem);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            Utility.MoveItem(lbAutocats, -1);
        }

        private void FillAutocatList(List<string> group)
        {
            if (group != null)
            {
                lbAutocats.Items.Clear();
            }

            {
                foreach (string name in group)
                {
                    lbAutocats.Items.Add(name);
                }
            }
        }

        private bool InGroup(string find)
        {
            foreach (string name in lbAutocats.Items)
            {
                if (name == find)
                {
                    return true;
                }
            }

            return false;
        }

        private void lbAutocats_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbAutocats.SelectedItems.Count > 1)
            {
                btnRemove.Enabled = true;
                btnUp.Enabled = false;
                btnDown.Enabled = false;
            }
            else if (lbAutocats.SelectedItem != null)
            {
                btnRemove.Enabled = true;
                btnUp.Enabled = lbAutocats.SelectedIndex != 0;
                btnDown.Enabled = lbAutocats.SelectedIndex != lbAutocats.Items.Count - 1;
            }
            else
            {
                btnRemove.Enabled = false;
                btnUp.Enabled = false;
                btnDown.Enabled = false;
            }
        }

        #endregion
    }
}
