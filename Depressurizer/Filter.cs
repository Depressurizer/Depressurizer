/*
    This file is part of Depressurizer.
    Original work Copyright 2011, 2012, 2013 Steve Labbe.
    Modified work Copyright 2017 Martijn Vegter.

    Depressurizer is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Depressurizer is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Depressurizer.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Xml;
using Depressurizer.Lib;

namespace Depressurizer
{
    public class Filter : IComparable
    {
        // Serialization strings
        private const string TypeIdString = "Filter";

        private const string
            XmlName_Name = "Name",
            XmlName_Uncategorized = "Uncategorized",
            XmlName_Hidden = "Hidden",
            XmlName_Allow = "Allow",
            XmlName_Require = "Require",
            XmlName_Exclude = "Exclude";

        public Filter(string name)
        {
            Name = name;
            Uncategorized = -1;
            Hidden = -1;
            Allow = new SortedSet<Category>();
            Require = new SortedSet<Category>();
            Exclude = new SortedSet<Category>();
        }

        public override string ToString()
        {
            return Name;
        }

        public string Name;
        public int Uncategorized;
        public int Hidden;

        private SortedSet<Category> _allow;

        public SortedSet<Category> Allow
        {
            get { return _allow; }
            set
            {
                _allow = new SortedSet<Category>(value);
                //foreach (Category c in value)
                //{
                //    _allow.Add(c);
                //}
            }
        }

        private SortedSet<Category> _require;

        public SortedSet<Category> Require
        {
            get { return _require; }
            set
            {
                _require = new SortedSet<Category>(value);
                //foreach (Category c in value)
                //{
                //    _require.Add(c);
                //}
            }
        }

        private SortedSet<Category> _exclude;

        public SortedSet<Category> Exclude
        {
            get { return _exclude; }
            set
            {
                _exclude = new SortedSet<Category>(value);
                //foreach (Category c in value)
                //{
                //    _exclude.Add(c);
                //}
            }
        }

        public int CompareTo(object o)
        {
            if (o == null) return 1;

            Filter otherFilter = o as Filter;
            if (o == null) throw new ArgumentException(GlobalStrings.Category_Exception_ObjectNotCategory);

            int comp = String.Compare(Name, otherFilter.Name, StringComparison.OrdinalIgnoreCase);

            if (comp == 0) return 0;

            return comp;
        }

        public void WriteToXml(XmlWriter writer)
        {
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.Filter_SavingFilter, Name);

            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(XmlName_Name, Name);
            writer.WriteElementString(XmlName_Uncategorized, Uncategorized.ToString());
            writer.WriteElementString(XmlName_Hidden, Hidden.ToString());

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

            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.Filter_FilterSaveComplete);
        }
    }
}