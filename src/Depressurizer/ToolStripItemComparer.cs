#region License

//     This file (ToolStripItemComparer.cs) is part of Depressurizer.
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
using System.Collections;
using System.Windows.Forms;

namespace Depressurizer
{
	public class ToolStripItemComparer : IComparer
	{
		#region Public Methods and Operators

		/// <inheritdoc />
		public int Compare(object x, object y)
		{
			ToolStripItem itemX = x as ToolStripItem;
			ToolStripItem itemY = y as ToolStripItem;

			if (itemX == null)
			{
				if (itemY == null)
				{
					return 0;
				}

				return -1;
			}

			if (itemY == null)
			{
				return 1;
			}

			return string.Compare(itemX.Text, itemY.Text, StringComparison.OrdinalIgnoreCase);
		}

		#endregion
	}
}