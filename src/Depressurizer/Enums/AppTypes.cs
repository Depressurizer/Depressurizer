#region License

//     This file (AppTypes.cs) is part of Depressurizer.
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

namespace Depressurizer.Enums
{
	[Flags]
	public enum AppTypes
	{
		Application = 1,

		Demo = 1 << 1,

		DLC = 1 << 2,

		Game = 1 << 3,

		Media = 1 << 4,

		Tool = 1 << 5,

		Other = 1 << 6,

		Unknown = 1 << 7,

		InclusionNormal = Application | Game,

		InclusionUnknown = InclusionNormal | Unknown,

		InclusionAll = (1 << 8) - 1
	}
}