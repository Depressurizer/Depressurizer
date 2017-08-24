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
using System.Collections.Generic;
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class GameDBEntryDialog : Form
    {
        public GameDBEntry Game;

        private bool editMode;

        public GameDBEntryDialog()
            : this(null) { }

        public GameDBEntryDialog(GameDBEntry game)
        {
            InitializeComponent();
            Game = game;
            editMode = (game == null) ? false : true;
        }

        private void GameDBEntryForm_Load(object sender, EventArgs e)
        {
            foreach (object o in Enum.GetValues(typeof(AppTypes)))
            {
                int val = (int) o;
                if ((val & (val - 1)) == 0)
                {
                    cmbType.Items.Add(o);
                }
            }

            InitializeFields(Game);
        }

        private void InitializeFields(GameDBEntry entry = null)
        {
            if (entry == null)
            {
                cmdSave.Text = GlobalStrings.DlgGameDBEntry_Add;
                cmbType.SelectedIndex = 0;
            }
            else
            {
                txtId.Text = Game.Id.ToString();
                txtId.Enabled = false;

                txtParent.Text = (Game.ParentId < 0) ? "" : Game.ParentId.ToString();

                cmbType.SelectedItem = Game.AppType;

                txtName.Text = Game.Name;
                if (Game.Genres != null) txtGenres.Text = string.Join(",", Game.Genres);
                if (Game.Flags != null) txtFlags.Text = string.Join(",", Game.Flags);
                if (Game.Tags != null) txtTags.Text = string.Join(",", Game.Tags);
                if (Game.Developers != null) txtDev.Text = string.Join(",", Game.Developers);
                if (Game.Publishers != null) txtPub.Text = string.Join(",", Game.Publishers);
                if (Game.MetacriticUrl != null) txtMCName.Text = Game.MetacriticUrl;
                if (Game.SteamReleaseDate != null) txtRelease.Text = Game.SteamReleaseDate;
                numAchievements.Value = Utility.Clamp(Game.TotalAchievements, (int) numAchievements.Minimum,
                    (int) numAchievements.Maximum);
                numReviewScore.Value = Utility.Clamp(Game.ReviewPositivePercentage, (int) numReviewScore.Minimum,
                    (int) numReviewScore.Maximum);
                numReviewCount.Value = Utility.Clamp(Game.ReviewTotal, (int) numReviewCount.Minimum,
                    (int) numReviewCount.Maximum);
                numHltbMain.Value = Utility.Clamp(Game.HltbMain, (int) numHltbMain.Minimum, (int) numHltbMain.Maximum);
                numHltbExtras.Value = Utility.Clamp(Game.HltbExtras, (int) numHltbExtras.Minimum,
                    (int) numHltbExtras.Maximum);
                numHltbCompletionist.Value = Utility.Clamp(Game.HltbCompletionist, (int) numHltbCompletionist.Minimum,
                    (int) numHltbCompletionist.Maximum);
                chkPlatWin.Checked = Game.Platforms.HasFlag(AppPlatforms.Windows);
                chkPlatMac.Checked = Game.Platforms.HasFlag(AppPlatforms.Mac);
                chkPlatLinux.Checked = Game.Platforms.HasFlag(AppPlatforms.Linux);

                chkWebUpdate.Checked = Game.LastStoreScrape > 0;
                chkAppInfoUpdate.Checked = Game.LastAppInfoUpdate > 0;

                dateWeb.Value = Utility.GetDTFromUTime(Game.LastStoreScrape);
                dateAppInfo.Value = Utility.GetDTFromUTime(Game.LastAppInfoUpdate);
            }
        }

        private bool ValidateEntries(out int id, out int parent)
        {
            parent = -1;
            if (!int.TryParse(txtId.Text, out id) || id <= 0)
            {
                MessageBox.Show(GlobalStrings.DlgGameDBEntry_IDMustBeInteger);
                return false;
            }
            if (!string.IsNullOrEmpty(txtParent.Text) && !int.TryParse(txtParent.Text, out parent))
            {
                MessageBox.Show(GlobalStrings.DlgGameDBEntry_ParentMustBeInt);
            }
            return true;
        }

        private bool SaveToGame()
        {
            int id, parent;
            if (!ValidateEntries(out id, out parent))
            {
                return false;
            }

            if (Game == null)
            {
                Game = new GameDBEntry();
                Game.Id = id;
            }

            Game.ParentId = parent;

            Game.AppType = (AppTypes) cmbType.SelectedItem;
            Game.Name = txtName.Text;


            Game.Genres = SplitAndTrim(txtGenres.Text);
            Game.Flags = SplitAndTrim(txtFlags.Text);
            Game.Tags = SplitAndTrim(txtTags.Text);
            Game.Developers = SplitAndTrim(txtDev.Text);
            Game.Publishers = SplitAndTrim(txtPub.Text);

            Game.TotalAchievements = (int) numAchievements.Value;
            Game.ReviewPositivePercentage = (int) numReviewScore.Value;
            Game.ReviewTotal = (int) numReviewCount.Value;

            Game.HltbMain = (int) numHltbMain.Value;
            Game.HltbExtras = (int) numHltbExtras.Value;
            Game.HltbCompletionist = (int) numHltbCompletionist.Value;

            Game.MetacriticUrl = txtMCName.Text;
            Game.SteamReleaseDate = txtRelease.Text;

            Game.Platforms = AppPlatforms.None;
            if (chkPlatWin.Checked) Game.Platforms |= AppPlatforms.Windows;
            if (chkPlatMac.Checked) Game.Platforms |= AppPlatforms.Mac;
            if (chkPlatLinux.Checked) Game.Platforms |= AppPlatforms.Linux;

            Game.LastStoreScrape = chkWebUpdate.Checked ? Utility.GetUTime(dateWeb.Value) : 0;
            Game.LastAppInfoUpdate = chkAppInfoUpdate.Checked ? Utility.GetUTime(dateAppInfo.Value) : 0;

            return true;
        }

        private readonly char[] SPLIT_CHAR = {','};

        private List<string> SplitAndTrim(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;

            string[] split = s.Split(SPLIT_CHAR, StringSplitOptions.RemoveEmptyEntries);
            List<string> result = new List<string>();
            foreach (string sp in split)
            {
                if (!string.IsNullOrWhiteSpace(sp)) result.Add(sp.Trim());
            }
            if (result.Count > 0) return result;
            return null;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (SaveToGame())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}