using System;
using SharpRaven;
using SharpRaven.Data;

namespace Depressurizer.Helpers
{
    /// <summary>
    ///     Error tracking that helps monitor and fix crashes in real time
    /// </summary>
    public static class Sentry
    {
        #region Properties

        private static RavenClient RavenClient => new RavenClient("https://fbb6fca0ff1748d7a9160b6bc92bcb1d@sentry.io/267726");

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Sends Exception to Sentry
        /// </summary>
        public static void Log(Exception e)
        {
            RavenClient.Capture(new SentryEvent(e));
        }

        #endregion
    }
}
