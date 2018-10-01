using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Depressurizer
{
    public struct LanguageSupport
    {
        [DefaultValue(null)] [XmlElement("Interface")]
        public List<string> Interface;

        [DefaultValue(null)] [XmlElement("FullAudio")]
        public List<string> FullAudio;

        [DefaultValue(null)] [XmlElement("Subtitles")]
        public List<string> Subtitles;
    }
}
