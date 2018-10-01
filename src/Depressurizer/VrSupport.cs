using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Depressurizer
{
    public struct VrSupport
    {
        [DefaultValue(null)] [XmlElement("Headset")]
        public List<string> Headsets;

        [DefaultValue(null)] [XmlElement("Input")]
        public List<string> Input;

        [DefaultValue(null)] [XmlElement("PlayArea")]
        public List<string> PlayArea;
    }
}
