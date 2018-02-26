#region LICENSE

//     This file (ProfileAccessException.cs) is part of DepressurizerCore.
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
using System.Runtime.Serialization;

namespace DepressurizerCore.Models
{
	[Serializable]
	public class ProfileAccessException : Exception
	{
		#region Constructors and Destructors

		public ProfileAccessException()
		{
			// TODO: Add any type-specific logic, and supply the default message.
		}

		public ProfileAccessException(string message) : base(message)
		{
			// TODO: Add any type-specific logic.
		}

		public ProfileAccessException(string message, Exception innerException) : base(message, innerException)
		{
			// TODO: Add any type-specific logic for inner exceptions.
		}

		protected ProfileAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// TODO: Implement type-specific serialization constructor logic.
		}

		#endregion
	}
}