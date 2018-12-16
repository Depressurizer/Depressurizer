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

        private const string XmlNameAllow = "Allow";

        private const string XmlNameExclude = "Exclude";

        private const string XmlNameHidden = "Hidden";

        private const string XmlNameName = "Name";

        private const string XmlNameRequire = "Require";

        private const string XmlNameUncategorized = "Uncategorized";

        private const string XmlNameVR = "VR";

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

            writer.WriteElementString(XmlNameName, Name);
            writer.WriteElementString(XmlNameUncategorized, Uncategorized.ToString());
            writer.WriteElementString(XmlNameHidden, Hidden.ToString());
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

            writer.WriteEndElement(); // Filter

            Logger.Info(GlobalStrings.Filter_FilterSaveComplete);
        }

        #endregion
    }
}
