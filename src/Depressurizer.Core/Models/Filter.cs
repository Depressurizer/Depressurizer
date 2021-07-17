using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Interfaces;
using JetBrains.Annotations;

namespace Depressurizer.Core.Models
{
    /// <summary>
    ///     Class representing a Filter.
    /// </summary>
    public class Filter : IComparable, IComparer<Filter>
    {
        #region Constants

        private const string XmlNameFilterAllow = "Allow";

        private const string XmlNameFilterExclude = "Exclude";

        private const string XmlNameFilterGame = "Game";

        private const string XmlNameFilterMod = "Mod";

        private const string XmlNameFilterHidden = "Hidden";

        private const string XmlNameFilterRequire = "Require";

        private const string XmlNameFilterSoftware = "Software";

        private const string XmlNameFilterUncategorized = "Uncategorized";

        private const string XmlNameFilterVR = "VR";

        #endregion

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

        [NotNull]
        public SortedSet<Category> Allow
        {
            get => _allow ?? (_allow = new SortedSet<Category>());
            set => _allow = new SortedSet<Category>(value);
        }

        [NotNull]
        public SortedSet<Category> Exclude
        {
            get => _exclude ?? (_exclude = new SortedSet<Category>());
            set => _exclude = new SortedSet<Category>(value);
        }

        public int Game { get; set; } = -1;

        public int Mod { get; set; } = -1;

        public int Hidden { get; set; } = -1;

        /// <summary>
        ///     Name of the filter.
        /// </summary>
        public string Name { get; set; }

        [NotNull]
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

        public static void AddFromNode(XmlNode node, IProfile profile)
        {
            if (!XmlUtil.TryGetStringFromNode(node[Serialization.Constants.Name], out string name))
            {
                return;
            }

            Filter filter = profile.AddFilter(name);
            if (XmlUtil.TryGetIntFromNode(node[XmlNameFilterGame], out int game))
            {
                filter.Game = game;
            }

            if (XmlUtil.TryGetIntFromNode(node[XmlNameFilterMod], out int mod))
            {
                filter.Mod = mod;
            }

            if (XmlUtil.TryGetIntFromNode(node[XmlNameFilterSoftware], out int software))
            {
                filter.Software = software;
            }

            if (XmlUtil.TryGetIntFromNode(node[XmlNameFilterUncategorized], out int uncategorized))
            {
                filter.Uncategorized = uncategorized;
            }

            if (XmlUtil.TryGetIntFromNode(node[XmlNameFilterHidden], out int hidden))
            {
                filter.Hidden = hidden;
            }

            if (XmlUtil.TryGetIntFromNode(node[XmlNameFilterVR], out int vr))
            {
                filter.VR = vr;
            }

            XmlNodeList filterNodes = node.SelectNodes(XmlNameFilterAllow);
            if (filterNodes != null)
            {
                foreach (XmlNode fNode in filterNodes)
                {
                    if (XmlUtil.TryGetStringFromNode(fNode, out string catName))
                    {
                        filter.Allow.Add(profile.GetCategory(catName));
                    }
                }
            }

            filterNodes = node.SelectNodes(XmlNameFilterRequire);
            if (filterNodes != null)
            {
                foreach (XmlNode fNode in filterNodes)
                {
                    if (XmlUtil.TryGetStringFromNode(fNode, out string catName))
                    {
                        filter.Require.Add(profile.GetCategory(catName));
                    }
                }
            }

            filterNodes = node.SelectNodes(XmlNameFilterExclude);
            if (filterNodes != null)
            {
                foreach (XmlNode fNode in filterNodes)
                {
                    if (XmlUtil.TryGetStringFromNode(fNode, out string catName))
                    {
                        filter.Exclude.Add(profile.GetCategory(catName));
                    }
                }
            }
        }

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

        public void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement(Serialization.Constants.Filter);

            writer.WriteElementString(Serialization.Constants.Name, Name);
            writer.WriteElementString(XmlNameFilterGame, Game.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString(XmlNameFilterMod, Mod.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString(XmlNameFilterSoftware, Software.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString(XmlNameFilterUncategorized, Uncategorized.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString(XmlNameFilterHidden, Hidden.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString(XmlNameFilterVR, VR.ToString(CultureInfo.InvariantCulture));

            foreach (Category c in Allow)
            {
                writer.WriteElementString(XmlNameFilterAllow, c.Name);
            }

            foreach (Category c in Require)
            {
                writer.WriteElementString(XmlNameFilterRequire, c.Name);
            }

            foreach (Category c in Exclude)
            {
                writer.WriteElementString(XmlNameFilterExclude, c.Name);
            }

            writer.WriteEndElement();
        }

        #endregion
    }
}
