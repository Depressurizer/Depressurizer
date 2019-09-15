using System;

namespace Depressurizer.Core.Models
{
    [Serializable]
    public class SteamProfileAccessException : ApplicationException
    {
        #region Constructors and Destructors

        public SteamProfileAccessException(string m) : base(m) { }

        #endregion
    }
}
