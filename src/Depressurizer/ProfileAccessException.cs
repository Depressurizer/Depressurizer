using System;

namespace Depressurizer
{
    [Serializable]
    internal class ProfileAccessException : ApplicationException
    {
        #region Constructors and Destructors

        public ProfileAccessException(string m) : base(m) { }

        #endregion
    }
}
