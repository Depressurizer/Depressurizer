using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Depressurizer.Properties;

namespace Depressurizer
{
    internal class GameBanners
    {
        private readonly List<GameInfo> _games;
        private volatile bool _shouldStop;

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

                string bannerFile = string.Format(Resources.GameBannerPath, Path.GetDirectoryName(Application.ExecutablePath), g.Id);
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
