using System;

namespace Depressurizer.Core.Helpers
{
    /// <summary>
    ///     Static class containing all constants.
    /// </summary>
    public static class Constants
    {
        #region Public Properties

        /// <summary>
        ///     Generic path to appinfo.vdf, must be formatted with the Steam installation path.
        /// </summary>
        public static string AppInfo => @"{0}\appcache\appinfo.vdf";

        /// <summary>
        ///     URL to the Depressurizer homepage.
        /// </summary>
        public static string DepressurizerHomepage => "https://github.com/Depressurizer/depressurizer";

        /// <summary>
        ///     URL to the API page for the latest Depressurizer release.
        /// </summary>
        public static string DepressurizerLatestRelease => "https://api.github.com/repos/Depressurizer/Depressurizer/releases/latest";

        /// <summary>
        ///     Generic URL for the game list of a Steam profile, must be formatted with a SteamID64.
        /// </summary>
        public static string GameList => @"https://steamcommunity.com/profiles/{0}/games?tab=all&xml=1";

        /// <summary>
        ///     Full list of every publicly facing program in the store/library.
        /// </summary>
        public static string GetAppList => "https://api.steampowered.com/ISteamApps/GetAppList/v2";

        /// <summary>
        ///     URL to the home page of HowLongToBeat.com.
        /// </summary>
        public static Uri HowLongToBeat => new Uri("https://howlongtobeat.com");

        /// <summary>
        ///     Generic path to localconfig.vdf, must be formatted with the Steam installation path and the Steam ID.
        /// </summary>
        public static string LocalConfig => @"{0}\userdata\{1}\config\localconfig.vdf";

        /// <summary>
        ///     Generic path to packageinfo.vdf, must be formatted with the Steam installation path.
        /// </summary>
        public static string PackageInfo => @"{0}\appcache\packageinfo.vdf";

        /// <summary>
        ///     Generic path to screenshots.vdf, must be formatted with the Steam installation path and the Steam ID.
        /// </summary>
        public static string Screenshots => @"{0}\userdata\{1}\760\screenshots.vdf";

        /// <summary>
        ///     Sentry Data Source Name.
        /// </summary>
        public static string SentryDSN => "https://9771482bc19c450b86ea6bcead6f7b6f@sentry.io/267726";

        /// <summary>
        ///     Generic path to sharedconfig.vdf, must be formatted with the Steam installation path and the Steam ID.
        /// </summary>
        public static string SharedConfig => @"{0}\userdata\{1}\7\remote\sharedconfig.vdf";

        /// <summary>
        ///     Generic path to shortcuts.vdf, must be formatted with the Steam installation path and the Steam ID.
        /// </summary>
        public static string Shortcuts => @"{0}\userdata\{1}\config\shortcuts.vdf";

        /// <summary>
        ///     Generic URL for a single Steam Community page, must be formatted with an appid.
        /// </summary>
        public static string SteamCommunityApp => "https://steamcommunity.com/app/{0}/";

        /// <summary>
        ///     Generic URL for a curators Steam recommendations, must be formatted with the curator id and page index.
        /// </summary>
        public static string SteamCuratorRecommendations => "https://store.steampowered.com/curator/{0}/ajaxgetfilteredrecommendations/render/?query=&start={1}&count=50";

        /// <summary>
        ///     Generic URL for a Steam profile, must be formatted with a SteamID64.
        /// </summary>
        public static string SteamProfile => @"https://steamcommunity.com/profiles/{0}?xml=1";

        /// <summary>
        ///     Generic URL for a Steam profile, must be formatted with the custom id.
        /// </summary>
        public static string SteamProfileCustom => @"https://steamcommunity.com/id/{0}?xml=1";

        /// <summary>
        ///     URL to the Steam Store homepage.
        /// </summary>
        public static string SteamStore => "https://store.steampowered.com/";

        /// <summary>
        ///     Generic URL for a single Steam Store app page, must be formatted with an appid.
        /// </summary>
        public static string SteamStoreApp => "https://store.steampowered.com/app/{0}/";

        /// <summary>
        ///     URL to Steam Web API Key page.
        /// </summary>
        public static string SteamWebApiKey => "https://steamcommunity.com/dev/apikey";

        /// <summary>
        ///     Generic URL for the Steam Web API GetOwnedGames, must be formatted with an api key and id.
        /// </summary>
        public static string SteamWebApiOwnedGames => "https://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key={0}&steamid={1}&include_appinfo=1&include_played_free_games=1";

        /// <summary>
        ///     Generic URL for a single Steam Store banner, must be formatted with an appid.
        /// </summary>
        public static string StoreBanner => "https://steamcdn-a.akamaihd.net/steam/apps/{0}/capsule_sm_120.jpg";

        #endregion
    }
}
