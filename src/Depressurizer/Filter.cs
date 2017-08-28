using System;
using System.Collections.Generic;
using System.Xml;

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
            XmlName_VR = "VR",
            XmlName_Allow = "Allow",
            XmlName_Require = "Require",
            XmlName_Exclude = "Exclude";

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

        public override string ToString()
        {
            return Name;
        }

        public string Name;
        public int Uncategorized;
        public int Hidden;
        public int VR;

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
            Program.Logger.WriteInfo(GlobalStrings.Filter_SavingFilter, Name);

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

            Program.Logger.WriteInfo(GlobalStrings.Filter_FilterSaveComplete);
        }
    }
}