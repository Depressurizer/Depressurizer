namespace Depressurizer.Core.Helpers
{
    public static class Constants
    {
        #region Public Properties

        public static string DepressurizerHomepage => "https://github.com/Depressurizer/depressurizer";

        public static string DepressurizerLatestRelease => "https://api.github.com/repos/Depressurizer/Depressurizer/releases/latest";

        /// <summary>
        ///     Full list of every publicly facing program in the store/library.
        /// </summary>
        public static string GetAppList => "https://api.steampowered.com/ISteamApps/GetAppList/v2";

        public static string HowLongToBeat => "https://www.howlongtobeatsteam.com/api/games/library/cached/all";

        public static string StoreBanner => "https://steamcdn-a.akamaihd.net/steam/apps/{0}/capsule_sm_120.jpg";

        #endregion
    }
}
