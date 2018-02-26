#region LICENSE

//     This file (Filter.cs) is part of DepressurizerCore.
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
using System.Collections.Generic;

namespace DepressurizerCore.Models
{
	public enum AdvancedFilterState
	{
		None = -1,

		Allow = 0,

		Require = 1,

		Exclude = 2
	}

	public sealed class Filter : IComparable
	{
		#region Constructors and Destructors

		public Filter(string name)
		{
			Name = name;
		}

		#endregion

		#region Public Properties

		public SortedSet<Category> Allow { get; set; } = new SortedSet<Category>();

		public SortedSet<Category> Exclude { get; set; } = new SortedSet<Category>();

		public int Game { get; set; } = -1;

		public int Hidden { get; set; } = -1;

		/// <summary>
		///     Filter Name
		/// </summary>
		public string Name { get; set; } = null;

		public SortedSet<Category> Require { get; set; } = new SortedSet<Category>();

		public int Software { get; set; } = -1;

		public int Uncategorized { get; set; } = -1;

		public int VR { get; set; } = -1;

		#endregion

		#region Public Methods and Operators

		/// <inheritdoc />
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}

			if (!(obj is Filter otherFilter))
			{
				throw new ArgumentException("Object is not a Filter");
			}

			return string.Compare(Name, otherFilter.Name, StringComparison.InvariantCulture);
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return Name;
		}

		#endregion
	}
}
