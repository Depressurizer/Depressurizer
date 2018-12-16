using System;

namespace Depressurizer.Core.Enums
{
    /// <summary>
    ///     Operating System(s)
    /// </summary>
    [Flags]
    public enum AppPlatforms
    {
        /// <summary>
        ///     Default value
        /// </summary>
        None = 0,

        /// <summary>
        ///     Microsoft Windows
        /// </summary>
        Windows = 1 << 0,

        /// <summary>
        ///     macOS
        /// </summary>
        Mac = 1 << 1,

        /// <summary>
        ///     Linux
        /// </summary>
        Linux = 1 << 2,

        /// <summary>
        ///     Windows, Mac and Linux
        /// </summary>
        All = (1 << 3) - 1
    }
}
