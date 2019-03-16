using System.Xml.Serialization;

namespace Depressurizer.Core.AutoCats
{
    public class AutoCatRule
    {
        #region Public Properties

        [XmlElement("Text")]
        public string Name { get; set; }

        #endregion
    }
}
