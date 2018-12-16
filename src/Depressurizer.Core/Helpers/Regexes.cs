using System.Text.RegularExpressions;

namespace Depressurizer.Core.Helpers
{
    /// <summary>
    ///     Static class containing regexes used by Depressurizer.
    /// </summary>
    public static class Regexes
    {
        #region Public Properties

        public static Regex IsSteamStore => new Regex(@"^http(s)?:\/\/store\.steampowered\.com(\/)?$", RegexOptions.Compiled);

        #endregion
    }
}
