using System;

namespace Depressurizer
{
    class ProfileAccessException : ApplicationException
    {
        public ProfileAccessException(string m) : base(m) { }
    }
}