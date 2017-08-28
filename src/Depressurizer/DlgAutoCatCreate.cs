/*
This file is part of Depressurizer.
Copyright 2011, 2012, 2013 Steve Labbe.

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

using System;
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class DlgAutoCatCreate : Form
    {
        public string SelectedName { get; set; }
        public AutoCatType SelectedType { get; set; }

        private string[] typeNames;
        private AutoCatType[] types;

        public DlgAutoCatCreate(string name = null, AutoCatType type = AutoCatType.None)
        {
            InitializeComponent();

            // To add new types to this  dialog, add the name and type to these arrays. The indexes must line up.
            // This method allows adding new types in one place AND allows localization to work properly
            typeNames = new[]
            {
                GlobalStrings.AutoCat_Name_Genre,
                GlobalStrings.AutoCat_Name_Flags,
                GlobalStrings.AutoCat_Name_Tags,
                GlobalStrings.AutoCat_Name_Year,
                GlobalStrings.AutoCat_Name_UserScore,
                GlobalStrings.AutoCat_Name_Hltb,
                GlobalStrings.AutoCat_Name_Manual,
                GlobalStrings.AutoCat_Name_DevPub,
                GlobalStrings.AutoCat_Name_Group,
                GlobalStrings.AutoCat_Name_Name,
                GlobalStrings.AutoCat_Name_VrSupport,
                GlobalStrings.AutoCat_Name_Language,
                GlobalStrings.AutoCat_Name_Curator,
                GlobalStrings.AutoCat_Name_Platform
            };
            types = new[]
            {
                AutoCatType.Genre,
                AutoCatType.Flags,
                AutoCatType.Tags,
                AutoCatType.Year,
                AutoCatType.UserScore,
                AutoCatType.Hltb,
                AutoCatType.Manual,
                AutoCatType.DevPub,
                AutoCatType.Group,
                AutoCatType.Name,
                AutoCatType.VrSupport,
                AutoCatType.Language,
                AutoCatType.Curator,
                AutoCatType.Platform
            };

            SelectedName = name;
            SelectedType = type;
        }

        private string TypeToString(AutoCatType t)
        {
            if (t == AutoCatType.None) return null;
            int index = Array.IndexOf(types, t);
            if (index >= 0 && index < typeNames.Length) return typeNames[index];
            return null;
        }

        private AutoCatType StringToType(string s)
        {
            if (s == null) return AutoCatType.None;
            int index = Array.IndexOf(typeNames, s);
            if (index >= 0 && index < types.Length) return types[index];
            return AutoCatType.None;
        }

        private void SaveUIToFields()
        {
            SelectedName = txtName.Text;
            SelectedType = StringToType(cmbType.SelectedItem as string);
        }

        private void LoadUIFromFields()
        {
            if (SelectedName == null)
            {
                txtName.Clear();
            }
            else
            {
                txtName.Text = SelectedName;
            }

            string selString = TypeToString(SelectedType);
            if (selString == null)
            {
                cmbType.SelectedIndex = 0;
            }
            else
            {
                cmbType.SelectedItem = selString;
            }
        }

        private void DlgAutoCatCreate_Load(object sender, EventArgs e)
        {
            foreach (string s in typeNames)
            {
                cmbType.Items.Add(s);
            }
            LoadUIFromFields();
        }

        private void cmdCreate_Click(object sender, EventArgs e)
        {
            SaveUIToFields();
        }
    }
}