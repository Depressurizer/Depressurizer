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
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class DlgRestore : Form
    {
        public bool Restored;

        public DlgRestore(string path)
        {
            InitializeComponent();

            var files = Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => s.EndsWith(".bak_1") || s.EndsWith(".bak_2") || s.EndsWith(".bak_3"));

            foreach (string f in files)
            {
                cboRestore.Items.Add(new ComboItem(Path.GetFileName(f), f));
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

        private void btnRestore_Click(object sender, EventArgs e)
        {
            string name = ((ComboItem) cboRestore.SelectedItem).Name;
            string message = name.Contains("vdf")
                ? String.Format(GlobalStrings.DlgRestore_ConfigConfirm, name)
                : String.Format(GlobalStrings.DlgRestore_ProfileConfirm, name);
            DialogResult result = MessageBox.Show(message, GlobalStrings.MainForm_Overwrite, MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (((ComboItem) cboRestore.SelectedItem).Restore())
                {
                    Restored = true;
                    Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    class ComboItem
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public ComboItem(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public override string ToString()
        {
            return Name;
        }

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
    }
}