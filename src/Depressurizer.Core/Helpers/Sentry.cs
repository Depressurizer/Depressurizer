#region License

//     This file (Sentry.cs) is part of Depressurizer.
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

namespace Depressurizer.Core.Helpers
{
	/// <summary>
	///     Eror tracking that helps monitor and fix crashes in real time
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
