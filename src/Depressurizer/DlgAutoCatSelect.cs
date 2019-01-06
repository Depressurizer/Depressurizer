using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Depressurizer.Core.Enums;

namespace Depressurizer
{
    public partial class DlgAutoCatSelect : Form
    {
        #region Fields

        public List<AutoCat> AutoCatList;

        public string originalGroup;

        #endregion

        #region Constructors and Destructors

        public DlgAutoCatSelect(List<AutoCat> autoCats, string name)
        {
            InitializeComponent();

            AutoCatList = new List<AutoCat>();
            originalGroup = name;

            foreach (AutoCat c in autoCats)
            {
                AutoCat clone = c.Clone();
                clone.Selected = false;
                AutoCatList.Add(clone);
            }
        }

        #endregion

        #region Public Methods and Operators

        // find and return AutoCat using the name
        public AutoCat GetAutoCat(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            foreach (AutoCat ac in AutoCatList)
            {
                if (string.Equals(ac.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return ac;
                }
            }

            return null;
        }

        #endregion

        #region Methods

        private void clbAutocats_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ((AutoCat) clbAutocats.Items[e.Index]).Selected = e.NewValue == CheckState.Checked;
        }

        private void DlgAutoCat_Load(object sender, EventArgs e)
        {
            FillAutocatList();
        }

        private void FillAutocatList()
        {
            clbAutocats.Items.Clear();
            foreach (AutoCat ac in AutoCatList)
            {
                if (ac.Name != originalGroup)
                {
                    bool addAC = true;
                    if (ac.AutoCatType == AutoCatType.Group)
                    {
                        addAC = SafeGroup(((AutoCatGroup) ac).Autocats, new List<string>(new[]
                        {
                            originalGroup
                        }));
                    }

                    if (addAC)
                    {
                        clbAutocats.Items.Add(ac);
                    }
                }
            }

            clbAutocats.DisplayMember = "DisplayName";
        }

        private bool IsGroup(string find)
        {
            AutoCat test = GetAutoCat(find);
            return test.AutoCatType == AutoCatType.Group;
        }

        private bool SafeGroup(IEnumerable<string> autocats, ICollection<string> groups)
        {
            foreach (string ac in autocats)
                // is AutoCat a group?
            {
                if (!IsGroup(ac))
                {
                    continue;
                }

                // if group list already contains the group then we are stuck in an infinite loop.  RETURN FALSE.
                if (groups.Contains(ac))
                {
                    return false;
                }

                // add new group to group list
                groups.Add(ac);
                // get AutoCat from group name
                AutoCatGroup group = GetAutoCat(ac) as AutoCatGroup;
                // send new group to SafeGroup to continue testing
                return SafeGroup(group.Autocats, groups);
            }

            // no duplicate group found.  All good! RETURN TRUE.
            return true;
        }

        #endregion
    }
}
