#region LICENSE

//     This file (SentryLogger.cs) is part of DepressurizerCore.
//     Copyright (C) 2018  Martijn Vegter
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <https://www.gnu.org/licenses/>.

#endregion

using System;
using SharpRaven;
using SharpRaven.Data;

namespace DepressurizerCore.Helpers
{
    public static class SentryLogger
    {
        #region Static Fields

        private static readonly RavenClient RavenClient = new RavenClient("https://a9d2b7ef3ae04cb6bdcb47868d04941b:82545065dd864f878defde6bd2ae51d9@sentry.io/267726");

        #endregion

        #region Public Methods and Operators

        public static void LogException(Exception eException)
        {
            RavenClient.Capture(new SentryEvent(e.Exception));
        }

        #endregion
    }
}