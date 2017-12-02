#region License

//     This file (Filter.cs) is part of Depressurizer.
//     Copyright (C) 2017  Martijn Vegter
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

namespace Depressurizer.Models
{
    /// <summary>
    ///     Depressurizer Filter
    /// </summary>
    public sealed class Filter : IComparable
    {
        private SortedSet<Category> _allow;

        private SortedSet<Category> _exclude;

        private SortedSet<Category> _require;

        /// <summary>
        ///     Filter Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Is Hidden
        /// </summary>
        public int Hidden { get; set; } = -1;

        /// <summary>
        ///     Is Virtual Reality
        /// </summary>
        public int VR { get; set; } = -1;


        /// <summary>
        ///     Is Uncategorized
        /// </summary>
        public int Uncategorized { get; set; } = -1;

        public SortedSet<Category> Allow
        {
            get => _allow ?? (_allow = new SortedSet<Category>());
            set => _allow = new SortedSet<Category>(value);
        }

        public SortedSet<Category> Require
        {
            get => _require ?? (_require = new SortedSet<Category>());
            set => _require = new SortedSet<Category>(value);
        }

        public SortedSet<Category> Exclude
        {
            get => _exclude ?? (_exclude = new SortedSet<Category>());
            set => _exclude = new SortedSet<Category>(value);
        }

        /// <summary>
        ///     Create Filter object
        /// </summary>
        /// <param name="name">Name of the filter</param>
        public Filter(string name)
        {
            Name = name;
        }

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is Filter otherFilter))
            {
                throw new ArgumentException("Obj is not a Filter");
            }

            return string.Compare(Name, otherFilter.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}