using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;

namespace Depressurizer
{
    public partial class DlgRandomGame : Form
    {
        private GameInfo game;
        private static Logger Logger => Logger.Instance;

        //Constructor
        public DlgRandomGame(GameInfo game)
        {
            InitializeComponent();
            this.game = game;
        }

        private void DlgRandomGame_Load(object sender, EventArgs e)
        {
            DisplayGame();
        }

        private void DisplayGame()
        {
            //game id's less than zero denote a game external to steam.
            if (game.Id > 0)
            {
                //Check to see if the banner is already downloaded, if not, get it now
                String bannerFile = Locations.File.Banner(game.Id);
                if (!File.Exists(bannerFile))
                {
                    Steam.GrabBanner(game.Id);
                }

                try
                {
                    Image gameBannerImage = Image.FromFile(bannerFile);
                    gameBannerBox.Image = gameBannerImage;
                }
                catch (Exception e)
                {
                    //process the error
                    MessageBox.Show("Warning", GlobalStrings.RandomGame_Banner_Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Logger.Exception($"Failed to load game banner for random game selection: ", e);
                }

            }

            gameTextBox.Text = game.Name;
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            game.LastPlayed = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            Utils.RunProcess(game.Executable);
            Close();
        }

    }
}
