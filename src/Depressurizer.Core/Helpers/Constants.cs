namespace Depressurizer.Core.Helpers
{
    /// <summary>
    ///     Static class containing all constants.
    /// </summary>
    public static class Constants
    {
        #region Public Properties

        /// <summary>
        ///     URL to the Depressurizer homepage.
        /// </summary>
        public static string DepressurizerHomepage => "https://github.com/Depressurizer/depressurizer";

        /// <summary>
        ///     URL to the API page for the latest Depressurizer release.
        /// </summary>
        public static string DepressurizerLatestRelease => "https://api.github.com/repos/Depressurizer/Depressurizer/releases/latest";

        /// <summary>
        ///     Full list of every publicly facing program in the store/library.
        /// </summary>
        public static string GetAppList => "https://api.steampowered.com/ISteamApps/GetAppList/v2";

        /// <summary>
        ///     URL to the API page of HowLongToBeat.com.
        /// </summary>
        public static string HowLongToBeat => "https://www.howlongtobeatsteam.com/api/games/library/cached/all";

        /// <summary>
        ///     Generic path to localconfig.vdf, must be formatted with the Steam installation path and the Steam ID.
        /// </summary>
        public static string LocalConfig => @"{0}\userdata\{1}\config\localconfig.vdf";

        /// <summary>
        ///     Generic path to sharedconfig.vdf, must be formatted with the Steam installation path and the Steam ID.
        /// </summary>
        public static string SharedConfig => @"{0}\userdata\{1}\7\remote\sharedconfig.vdf";

        public static string SteamCuratorRecommendations => "https://store.steampowered.com/curator/{0}/ajaxgetfilteredrecommendations/render/?query=&start={1}&count=50";

        /// <summary>
        ///     URL to the Steam Store homepage.
        /// </summary>
        public static string SteamStore => "https://store.steampowered.com/";

        /// <summary>
        ///     Generic URL for a single Steam Store app page, must be formatted with an appid.
        /// </summary>
        public static string SteamStoreApp => "https://store.steampowered.com/app/{0}/";

        /// <summary>
        ///     Generic URL for a single Steam Store banner, must be formatted with an appid.
        /// </summary>
        public static string StoreBanner => "https://steamcdn-a.akamaihd.net/steam/apps/{0}/capsule_sm_120.jpg";

        #endregion
    }
}
