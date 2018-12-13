namespace Depressurizer.Core.Enums
{
    /// <summary>
    ///     Depressurizer startup actions.
    /// </summary>
    public enum StartupAction
    {
        /// <summary>
        ///     Execute the default sequence.
        /// </summary>
        None,

        /// <summary>
        ///     Load the profile specified in the settings file.
        /// </summary>
        Load,

        /// <summary>
        ///     Load the "Create a profile" dialog.
        /// </summary>
        Create
    }
}
