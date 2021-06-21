using System;
using Depressurizer.Core.Interfaces;

namespace Depressurizer.Core.Helpers
{
    public static class SingletonKeeper
    {
        #region Static Fields

        private static IDatabase _database;

        #endregion

        #region Public Properties

        public static IDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    throw new InvalidOperationException();
                }

                return _database;
            }
            set
            {
                if (_database != null)
                {
                    throw new InvalidOperationException();
                }

                _database = value;
            }
        }

        public static string SteamWebApiKey { get; set; }

        #endregion
    }
}
