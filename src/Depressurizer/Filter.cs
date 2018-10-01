using System;
using System.Collections.Generic;
using System.Xml;
using Rallion;

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

        private SortedSet<Category> _allow;

        private SortedSet<Category> _exclude;

        private SortedSet<Category> _require;
        public int Hidden;

        public string Name;
        public int Uncategorized;
        public int VR;

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

        public SortedSet<Category> Allow
        {
            get => _allow;
            set => _allow = new SortedSet<Category>(value);
        }

        public SortedSet<Category> Require
        {
            get => _require;
            set => _require = new SortedSet<Category>(value);
        }

        public SortedSet<Category> Exclude
        {
            get => _exclude;
            set => _exclude = new SortedSet<Category>(value);
        }

        public int CompareTo(object o)
        {
            if (o == null) return 1;

            var otherFilter = o as Filter;
            if (o == null) throw new ArgumentException(GlobalStrings.Category_Exception_ObjectNotCategory);

            var comp = string.Compare(Name, otherFilter.Name, StringComparison.OrdinalIgnoreCase);

            if (comp == 0) return 0;

            return comp;
        }

        public override string ToString()
        {
            return Name;
        }

        public void WriteToXml(XmlWriter writer)
        {
            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.Filter_SavingFilter, Name);

            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(XmlName_Name, Name);
            writer.WriteElementString(XmlName_Uncategorized, Uncategorized.ToString());
            writer.WriteElementString(XmlName_Hidden, Hidden.ToString());
            writer.WriteElementString(XmlName_VR, VR.ToString());

            foreach (var c in Allow) writer.WriteElementString(XmlName_Allow, c.Name);

            foreach (var c in Require) writer.WriteElementString(XmlName_Require, c.Name);

            foreach (var c in Exclude) writer.WriteElementString(XmlName_Exclude, c.Name);

            writer.WriteEndElement(); // Filter

            Program.Logger.Write(LoggerLevel.Info, GlobalStrings.Filter_FilterSaveComplete);
        }
    }
}