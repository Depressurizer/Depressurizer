using System;

namespace Depressurizer
{
    public class ParseException : ApplicationException
    {
        #region Constructors and Destructors

        public ParseException()
        {
        }

        public ParseException(string message) : base(message)
        {
        }

        #endregion
    }
}
