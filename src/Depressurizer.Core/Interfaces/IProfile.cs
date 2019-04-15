using Depressurizer.Core.Models;

namespace Depressurizer.Core.Interfaces
{
    public interface IProfile
    {
        #region Public Methods and Operators

        Filter AddFilter(string name);

        Category GetCategory(string name);

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
