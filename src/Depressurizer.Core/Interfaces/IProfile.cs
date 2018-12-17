namespace Depressurizer.Core.Interfaces
{
    public interface IProfile
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Save profile to the default location.
        /// </summary>
        void Save();

        /// <summary>
        ///     Save profile to the specified location.
        /// </summary>
        void Save(string path);

        #endregion
    }
}
