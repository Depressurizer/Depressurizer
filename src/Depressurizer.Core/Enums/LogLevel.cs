namespace Depressurizer.Core.Enums
{
    /// <summary>
    ///     Defines the set of levels recognized by the system.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        ///     Designates finer-grained informational events than the DEBUG.
        /// </summary>
        Verbose = 0,

        /// <summary>
        ///     Designates fine-grained informational events that are most useful to debug an application.
        /// </summary>
        Debug = 1,

        /// <summary>
        ///     Designates informational messages that highlight the progress of the application at coarse-grained level.
        /// </summary>
        Info = 2,

        /// <summary>
        ///     Designates potentially harmful situations.
        /// </summary>
        Warn = 3,

        /// <summary>
        ///     Designates error events that might still allow the application to continue running.
        /// </summary>
        Error = 4,

        /// <summary>
        ///     Designates very severe error events that will presumably lead the application to abort.
        /// </summary>
        Fatal = 5
    }
}
