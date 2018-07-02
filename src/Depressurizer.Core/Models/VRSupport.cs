#region License

//     This file (VRSupport.cs) is part of Depressurizer.
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

using System.Collections.Generic;

namespace Depressurizer.Core.Models
{
	/// <summary>
	///     Steam VR Support
	/// </summary>
	public sealed class VRSupport
	{
		#region Public Properties

		/// <summary>
		///     Supported VR headsets
		/// </summary>
		public List<string> Headsets { get; set; } = new List<string>();

		/// <summary>
		///     Supported input / controllers
		/// </summary>
		public List<string> Input { get; set; } = new List<string>();

		/// <summary>
		///     Supported play areas
		/// </summary>
		public List<string> PlayArea { get; set; } = new List<string>();

		#endregion
	}
}
