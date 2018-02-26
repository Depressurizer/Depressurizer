#region LICENSE

//     This file (Category.cs) is part of DepressurizerCore.
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

namespace DepressurizerCore.Models
{
	/// <summary>
	///     Single App Category
	/// </summary>
	public sealed class Category : IComparable
	{
		#region Constructors and Destructors

		/// <summary>
		///     Create a new Category
		/// </summary>
		public Category(string name)
		{
			Name = name;
		}

		#endregion

		#region Public Properties

		/// <summary>
		///     Number of Apps in this Category
		/// </summary>
		public int Count { get; set; } = 0;

		/// <summary>
		///     Name of the Category
		/// </summary>
		public string Name { get; set; }

		#endregion

		#region Public Methods and Operators

		/// <inheritdoc />
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}

			if (!(obj is Category otherCategory))
			{
				throw new ArgumentException("Object is not a Category");
			}

			if (string.Equals("favorite", Name, StringComparison.OrdinalIgnoreCase))
			{
				return -1;
			}

			if (string.Equals("favorite", otherCategory.Name, StringComparison.OrdinalIgnoreCase))
			{
				return 1;
			}

			return string.Compare(Name, otherCategory.Name, StringComparison.OrdinalIgnoreCase);
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return Name;
		}

		#endregion
	}
}
