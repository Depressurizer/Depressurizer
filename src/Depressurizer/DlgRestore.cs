#region LICENSE

//     This file (DlgRestore.cs) is part of Depressurizer.
//     Copyright (C) 2011 Steve Labbe
//     Copyright (C) 2017 Theodoros Dimos
//     Copyright (C) 2017 Martijn Vegter
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class DlgRestore : Form
    {
        #region Fields

        public bool Restored;

        #endregion

        #region Constructors and Destructors

        public DlgRestore(string path)
        {
            InitializeComponent();

            IEnumerable<string> files = Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".bak_1") || s.EndsWith(".bak_2") || s.EndsWith(".bak_3"));

            foreach (string f in files)
            {
                cboRestore.Items.Add(new ComboItem(Path.GetFileName(f), f));
            }
        }

        #endregion

        #region Methods

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            string name = ((ComboItem) cboRestore.SelectedItem).Name;
            string message = name.Contains("vdf") ? string.Format(GlobalStrings.DlgRestore_ConfigConfirm, name) : string.Format(GlobalStrings.DlgRestore_ProfileConfirm, name);
            DialogResult result = MessageBox.Show(message, GlobalStrings.MainForm_Overwrite, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (((ComboItem) cboRestore.SelectedItem).Restore())
                {
                    Restored = true;
                    Close();
                }
            }
        }

        private void cboRestore_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (File.Exists(((ComboItem) cboRestore.SelectedItem).Path))
            {
                rtbRestore.Text = "";
                string path = ((ComboItem) cboRestore.SelectedItem).Path;
                rtbRestore.Text = File.ReadAllText(path);
                DateTime dt = File.GetLastWriteTime(path);
                long length = new FileInfo(path).Length;
                length = length / 1024;
                lblDateStamp.Text = dt.ToString();
                lblSize.Text = length + " KB";
                btnRestore.Enabled = true;
            }
            else
            {
                btnRestore.Enabled = false;
            }
        }

        #endregion
    }

    internal class ComboItem
    {
        #region Constructors and Destructors

        public ComboItem(string name, string path)
        {
            Name = name;
            Path = path;
        }

        #endregion

        #region Public Properties

        public string Name { get; set; }

        public string Path { get; set; }

        #endregion

        #region Public Methods and Operators

        public bool Restore()
        {
            string file = System.IO.Path.GetFileNameWithoutExtension(Path);
            string path = System.IO.Path.GetDirectoryName(Path);
            file = System.IO.Path.Combine(path, file);
            try
            {
                File.Copy(Path, file, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
