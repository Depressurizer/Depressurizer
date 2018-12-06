namespace Depressurizer.Core.Interfaces
{
    /// <summary>
    ///     Interface for the Depressurizer settings controller.
    /// </summary>
    public interface ISettings
    {
        #region Public Properties

        /// <summary>
        ///     Height of the main window.
        /// </summary>
        int Height { get; set; }

        /// <summary>
        ///     Width of the main window.
        /// </summary>
        int Width { get; set; }

        /// <summary>
        ///     X-position of the main window.
        /// </summary>
        int X { get; set; }

        /// <summary>
        ///     Y-position of the main window.
        /// </summary>
        int Y { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Load settings from the default location.
        /// </summary>
        void Load();

        /// <summary>
        ///     Load settings from the specified location.
        /// </summary>
        void Load(string path);

        /// <summary>
        ///     Save settings to the default location.
        /// </summary>
        void Save();

        /// <summary>
        ///     Save settings to the specified location.
        /// </summary>
        void Save(string path);

        #endregion
    }
}
