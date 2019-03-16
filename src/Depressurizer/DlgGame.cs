using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Depressurizer.Core.Enums;
using Depressurizer.Properties;

namespace Depressurizer
{
    public partial class DlgGame : Form
    {
        #region Fields

        public GameInfo Game;

        private readonly GameList Data;

        private readonly bool editMode;

        #endregion

        #region Constructors and Destructors

        public DlgGame(GameList data, GameInfo game = null) : this()
        {
            Data = data;
            Game = game;
            editMode = Game != null;
        }

        private DlgGame()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(txtExecutable.Text);
                    dialog.InitialDirectory = fileInfo.DirectoryName;
                    dialog.FileName = fileInfo.Name;
                }
                catch (ArgumentException) { }

                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtExecutable.Text = dialog.FileName;
                }
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
                if (!int.TryParse(txtId.Text, out int id))
                {
                    MessageBox.Show(GlobalStrings.DlgGameDBEntry_IDMustBeInteger, Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Data.Games.ContainsKey(id))
                {
                    MessageBox.Show(GlobalStrings.DBEditDlg_GameIdAlreadyExists, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Game = new GameInfo(id, txtName.Text, Data, txtExecutable.Text);
                Game.ApplySource(GameListingSource.Manual);
                Data.Games.Add(id, Game);
            }

            Game.SetFavorite(chkFavorite.Checked);

            Game.IsHidden = chkHidden.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void GameDlg_Load(object sender, EventArgs e)
        {
            if (editMode)
            {
                Text = GlobalStrings.DlgGame_EditGame;
                txtId.Text = Game.Id.ToString(CultureInfo.CurrentCulture);
                txtName.Text = Game.Name;
                txtHoursPlayed.Text = Game.HoursPlayed.ToString(CultureInfo.CurrentCulture);
                txtCategory.Text = Game.GetCatString();
                txtExecutable.Text = Game.Executable;
                chkFavorite.Checked = Game.IsFavorite();
                chkHidden.Checked = Game.IsHidden;
                txtId.ReadOnly = true;
            }
            else
            {
                Text = GlobalStrings.DlgGame_CreateGame;
            }
        }

        #endregion
    }
}
