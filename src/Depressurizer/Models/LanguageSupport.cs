﻿#region License

//     This file (LanguageSupport.cs) is part of Depressurizer.
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
using System.ComponentModel;
using System.Xml.Serialization;

namespace Depressurizer.Models
{
	public struct LanguageSupport
	{
		#region Fields

		[DefaultValue(null)]
		[XmlElement("Interface")]
		public List<string> Interface;

		[DefaultValue(null)]
		[XmlElement("FullAudio")]
		public List<string> FullAudio;

		[DefaultValue(null)]
		[XmlElement("Subtitles")]
		public List<string> Subtitles;

		#endregion
	}
}
