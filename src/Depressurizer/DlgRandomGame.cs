using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;

namespace Depressurizer
{
    public partial class DlgRandomGame : Form
    {
        private GameInfo game;
        List<GameInfo> gameInfos = new List<GameInfo>();
        private static Logger Logger => Logger.Instance;

        //Constructor
        public DlgRandomGame(GameInfo game)
        {
            this.game = game;
            this.gameInfos.Add(game);

            InitializeComponent();
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
                    Steam.GrabBanners(gameInfos.Select(game => game.Id));
                }

                try
                {
                    Locations.File.Banner(game.Id);
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
            if (game != null)
            {
                game.LastPlayed = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                System.Diagnostics.Process.Start(game.Executable);
            }
        }
    }
}
