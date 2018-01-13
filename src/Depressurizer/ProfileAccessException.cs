using System;

namespace Depressurizer
{
    internal class ProfileAccessException : ApplicationException
    {
        #region Constructors and Destructors

        public ProfileAccessException(string m) : base(m) { }

        #endregion
    }
}