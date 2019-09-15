namespace Depressurizer.Core.Models
{
    public class GetOwnedGamesObject
    {
        #region Public Properties

        public Response response { get; set; }

        #endregion

        public class Game
        {
            #region Public Properties

            public int appid { get; set; }

            public bool has_community_visible_stats { get; set; }

            public string img_icon_url { get; set; }

            public string img_logo_url { get; set; }

            public string name { get; set; }

            public int playtime_2weeks { get; set; }

            public int playtime_forever { get; set; }

            public int playtime_linux_forever { get; set; }

            public int playtime_mac_forever { get; set; }

            public int playtime_windows_forever { get; set; }

            #endregion
        }

        public class Response
        {
            #region Public Properties

            public int game_count { get; set; }

            public Game[] games { get; set; }

            #endregion
        }
    }
}
