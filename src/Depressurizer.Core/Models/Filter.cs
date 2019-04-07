using System;
using System.Collections.Generic;

namespace Depressurizer.Core.Models
{
    /// <summary>
    ///     Class representing a Filter.
    /// </summary>
    public class Filter : IComparable, IComparer<Filter>
    {
        #region Fields

        private SortedSet<Category> _allow;

        private SortedSet<Category> _exclude;

        private SortedSet<Category> _require;

        #endregion

        #region Constructors and Destructors

        public Filter(string name)
        {
            Name = name;
        }

        #endregion

        #region Public Properties

        public SortedSet<Category> Allow
        {
            get => _allow ?? (_allow = new SortedSet<Category>());
            set => _allow = new SortedSet<Category>(value);
        }

        public SortedSet<Category> Exclude
        {
            get => _exclude ?? (_exclude = new SortedSet<Category>());
            set => _exclude = new SortedSet<Category>(value);
        }

        public int Game { get; set; } = -1;

        public int Hidden { get; set; } = -1;

        /// <summary>
        ///     Name of the filter.
        /// </summary>
        public string Name { get; set; }

        public SortedSet<Category> Require
        {
            get => _require ?? (_require = new SortedSet<Category>());
            set => _require = new SortedSet<Category>(value);
        }

        public int Software { get; set; } = -1;

        public int Uncategorized { get; set; } = -1;

        public int VR { get; set; } = -1;

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
        public int Compare(Filter x, Filter y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }

                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            return x.CompareTo(y);
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
                throw new ArgumentException("Object is not a Filter!");
            }

            return string.Compare(Name, otherFilter.Name, StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
