#region License

//     This file (LogLevel.cs) is part of Depressurizer.
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

namespace Depressurizer.Core.Enums
{
	/// <summary>
	///     Defines the set of levels recognized by the system.
	/// </summary>
	public enum LogLevel
	{
		/// <summary>
		///     Designates finer-grained informational events than the DEBUG.
		/// </summary>
		Verbose,

		/// <summary>
		///     Designates fine-grained informational events that are most useful to debug an application.
		/// </summary>
		Debug,

		/// <summary>
		///     Designates informational messages that highlight the progress of the application at coarse-grained level.
		/// </summary>
		Info,

		/// <summary>
		///     Designates potentially harmful situations.
		/// </summary>
		Warn,

		/// <summary>
		///     Designates error events that might still allow the application to continue running.
		/// </summary>
		Error,

		/// <summary>
		///     Designates very severe error events that will presumably lead the application to abort.
		/// </summary>
		Fatal
	}
}
