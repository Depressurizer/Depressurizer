using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Depressurizer
{
    class GameBanners
    {
        private volatile bool _shouldStop;
        private List<GameInfo> _games;

        public GameBanners(List<GameInfo> games)
        {
            _games = games;
        }

        public void Grab()
        {
            foreach (GameInfo g in _games)
            {
                if (_shouldStop)
                {
                    return;
                }

                if (g.Id < 0)
                {
                    continue; //external game
                }

                string bannerFile = string.Format(Properties.Resources.GameBannerPath,
                    Path.GetDirectoryName(Application.ExecutablePath), g.Id);
                if (!File.Exists(bannerFile))
                {
                    Utility.GrabBanner(g.Id);
                }
            }
        }

        public void Stop()
        {
            _shouldStop = true;
        }
    }
}