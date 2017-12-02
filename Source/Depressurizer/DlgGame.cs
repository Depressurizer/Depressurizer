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
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class DlgGame : Form
    {
        GameList Data;
        public GameInfo Game;

        bool editMode;

        private DlgGame()
        {
            InitializeComponent();
        }

        public DlgGame(GameList data, GameInfo game = null)
            : this()
        {
            Data = data;
            Game = game;
            editMode = Game != null;
        }

        private void GameDlg_Load(object sender, EventArgs e)
        {
            if (editMode)
            {
                Text = GlobalStrings.DlgGame_EditGame;
                txtId.Text = Game.Id.ToString();
                txtName.Text = Game.Name;
                txtCategory.Text = Game.GetCatString();
                txtExecutable.Text = Game.Executable;
                chkFavorite.Checked = Game.IsFavorite();
                chkHidden.Checked = Game.Hidden;
                txtId.ReadOnly = true;
            }
            else
            {
                Text = GlobalStrings.DlgGame_CreateGame;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (editMode)
            {
                Game.Name = txtName.Text;
                Game.Executable = txtExecutable.Text;
            }
            else
            {
                int id;
                if (!int.TryParse(txtId.Text, out id))
                {
                    MessageBox.Show(GlobalStrings.DlgGameDBEntry_IDMustBeInteger, GlobalStrings.Gen_Warning,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Data.Games.ContainsKey(id))
                {
                    MessageBox.Show(GlobalStrings.DBEditDlg_GameIdAlreadyExists, GlobalStrings.DBEditDlg_Error,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Game = new GameInfo(id, txtName.Text, Data, txtExecutable.Text);
                Game.ApplySource(GameListingSource.Manual);
                Data.Games.Add(id, Game);
            }

            Game.SetFavorite(chkFavorite.Checked);

            Game.Hidden = chkHidden.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            try
            {
                FileInfo f = new FileInfo(txtExecutable.Text);
                dlg.InitialDirectory = f.DirectoryName;
                dlg.FileName = f.Name;
            }
            catch (ArgumentException) { }

            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                txtExecutable.Text = dlg.FileName;
            }
        }
    }
}