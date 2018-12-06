#region LICENSE

//     This file (Filter.cs) is part of Depressurizer.
//     Copyright (C) 2011 Steve Labbe
//     Copyright (C) 2017 Theodoros Dimos
//     Copyright (C) 2017 Martijn Vegter
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
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Generic;
using System.Xml;
using Depressurizer.Core.Models;
using Depressurizer.Helpers;

namespace Depressurizer.Models
{
    /// <summary>
    ///     Class representing a Filter.
    /// </summary>
    /// <inheritdoc />
    public class Filter : IComparable
    {
        #region Constants

        private const string TypeIdString = "Filter";

        private const string XmlName_Allow = "Allow";

        private const string XmlName_Exclude = "Exclude";

        private const string XmlName_Hidden = "Hidden";

        private const string XmlName_Name = "Name";

        private const string XmlName_Require = "Require";

        private const string XmlName_Uncategorized = "Uncategorized";

        private const string XmlName_VR = "VR";

        #endregion

        #region Fields

        public int Hidden;

        public int Uncategorized;

        public int VR;

        private SortedSet<Category> _allow;

        private SortedSet<Category> _exclude;

        private SortedSet<Category> _require;

        #endregion

        #region Constructors and Destructors

        public Filter(string name)
        {
            Name = name;
            Uncategorized = -1;
            Hidden = -1;
            VR = -1;
            Allow = new SortedSet<Category>();
            Require = new SortedSet<Category>();
            Exclude = new SortedSet<Category>();
        }

        #endregion

        #region Public Properties

        public SortedSet<Category> Allow
        {
            get => _allow;
            set => _allow = new SortedSet<Category>(value);
        }

        public SortedSet<Category> Exclude
        {
            get => _exclude;
            set => _exclude = new SortedSet<Category>(value);
        }

        /// <summary>
        ///     Name of the filter.
        /// </summary>
        public string Name { get; set; }

        public SortedSet<Category> Require
        {
            get => _require;
            set => _require = new SortedSet<Category>(value);
        }

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
                throw new ArgumentException("Object is not a Filter!");
            }

            return string.Compare(Name, otherFilter.Name, StringComparison.OrdinalIgnoreCase);
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

            writer.WriteElementString(XmlName_Name, Name);
            writer.WriteElementString(XmlName_Uncategorized, Uncategorized.ToString());
            writer.WriteElementString(XmlName_Hidden, Hidden.ToString());
            writer.WriteElementString(XmlName_VR, VR.ToString());

            foreach (Category c in Allow)
            {
                writer.WriteElementString(XmlName_Allow, c.Name);
            }

            foreach (Category c in Require)
            {
                writer.WriteElementString(XmlName_Require, c.Name);
            }

            foreach (Category c in Exclude)
            {
                writer.WriteElementString(XmlName_Exclude, c.Name);
            }

            writer.WriteEndElement(); // Filter

            Logger.Info(GlobalStrings.Filter_FilterSaveComplete);
        }

        #endregion
    }
}
