using System;

namespace Depressurizer
{
    /// <summary>
    /// Represents a single game category
    /// </summary>
    /// 
    public class Category : IComparable
    {
        public string Name;
        public int Count;

        public Category(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(object o)
        {
            if (o == null) return 1;

            Category otherCat = o as Category;
            if (o == null) throw new ArgumentException(GlobalStrings.Category_Exception_ObjectNotCategory);

            int comp = String.Compare(Name, otherCat.Name, StringComparison.OrdinalIgnoreCase);

            if (comp == 0) return 0;
            if (String.Equals(Name, "favorite", StringComparison.OrdinalIgnoreCase)) return -1;
            if (String.Equals(otherCat.Name, "favorite", StringComparison.OrdinalIgnoreCase)) return 1;

            return comp;
        }
    }
}