#region License

//     This file (Filter.cs) is part of Depressurizer.
//     Copyright (C) 2011  Steve Labbe
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
using System.Xml;
using Depressurizer.Core.Models;
using Depressurizer.Helpers;

namespace Depressurizer.Models
{
	public sealed class Filter : IComparable
	{
		#region Constants

		private const string TypeIdString = "Filter";

		private const string XmlNameAllow = "Allow";

		private const string XmlNameExclude = "Exclude";

		private const string XmlNameGame = "Game";

		private const string XmlNameHidden = "Hidden";

		private const string XmlNameName = "Name";

		private const string XmlNameRequire = "Require";

		private const string XmlNameSoftware = "Software";

		private const string XmlNameUncategorized = "Uncategorized";

		private const string XmlNameVR = "VR";

		#endregion

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

		#region Properties

		private static Logger Logger => Logger.Instance;

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

			return string.Compare(Name, otherFilter.Name, StringComparison.Ordinal);
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return Name;
		}

		public void WriteToXml(XmlWriter writer)
		{
			Logger.Info(GlobalStrings.Filter_SavingFilter, Name);

			writer.WriteStartElement(TypeIdString);

			writer.WriteElementString(XmlNameName, Name);

			writer.WriteElementString(XmlNameGame, Game.ToString());
			writer.WriteElementString(XmlNameHidden, Hidden.ToString());
			writer.WriteElementString(XmlNameSoftware, Software.ToString());
			writer.WriteElementString(XmlNameUncategorized, Uncategorized.ToString());
			writer.WriteElementString(XmlNameVR, VR.ToString());

			foreach (Category c in Allow)
			{
				writer.WriteElementString(XmlNameAllow, c.Name);
			}

			foreach (Category c in Require)
			{
				writer.WriteElementString(XmlNameRequire, c.Name);
			}

			foreach (Category c in Exclude)
			{
				writer.WriteElementString(XmlNameExclude, c.Name);
			}

			writer.WriteEndElement();

			Logger.Info(GlobalStrings.Filter_FilterSaveComplete);
		}

		#endregion
	}
}