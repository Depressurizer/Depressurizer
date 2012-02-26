using System.Xml;

namespace DPLib {
    public static class XmlUtil {
        public static bool GetIntFromNode( XmlNode node, out int value ) {
            if( node != null ) {
                XmlNode textNode = node.SelectSingleNode( "text()" );
                if( textNode != null && int.TryParse( textNode.InnerText, out value ) ) {
                    return true;
                }
            }
            value = 0;
            return false;
        }

        public static bool GetStringFromNode( XmlNode node, out string value ) {
            if( node != null ) {
                XmlNode textNode = node.SelectSingleNode( "text()" );
                if( textNode != null ) {
                    value = node.InnerText;
                    return true;
                }
            }
            value = string.Empty;
            return false;
        }

        public static bool GetBoolFromNode( XmlNode node, out bool value ) {
            if( node != null ) {
                XmlNode textNode = node.SelectSingleNode( "text()" );
                if( textNode != null && bool.TryParse( textNode.InnerText, out value ) ) {
                    return true;
                }
            }
            value = false;
            return false;
        }

        public static string GetAttributeText( XmlNode node, string attName ) {
            if( node != null ) {
                XmlAttribute att = node.Attributes["attName"];
                if( att != null ) {
                    return att.Value;
                }
            }
            return null;
        }

        public static bool GetBoolFromAttribute( XmlNode node, string attName, out bool value ) {
            string attText = GetAttributeText( node, attName );
            if( attText != null && bool.TryParse( attText, out value ) ) {
                return true;
            }
            value = false;
            return false;
        }
    }
}
