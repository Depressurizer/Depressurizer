using System.Xml;

namespace DPLib {
    public static class XmlUtil {
        public static string GetStringFromNode( XmlNode node, string defaultValue ) {
            string result;
            return TryGetStringFromNode( node, out result ) ? result : defaultValue;
        }

        public static bool TryGetStringFromNode( XmlNode node, out string value ) {
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

        public static int GetIntFromNode( XmlNode node, int defaultValue ) {
            int result;
            return TryGetIntFromNode( node, out result ) ? result : defaultValue;
        }

        public static bool TryGetIntFromNode( XmlNode node, out int value ) {
            if( node != null ) {
                XmlNode textNode = node.SelectSingleNode( "text()" );
                if( textNode != null && int.TryParse( textNode.InnerText, out value ) ) {
                    return true;
                }
            }
            value = 0;
            return false;
        }

        public static bool GetBoolFromNode( XmlNode node, bool defaultValue ) {
            bool result;
            return TryGetBoolFromNode( node, out result ) ? result : defaultValue;
        }

        public static bool TryGetBoolFromNode( XmlNode node, out bool value ) {
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

        public static bool GetBoolFromAttribute( XmlNode node, string attName, bool defaultValue ) {
            bool result;
            return TryGetBoolFromAttribute( node, attName, out result ) ? result : defaultValue;
        }

        public static bool TryGetBoolFromAttribute( XmlNode node, string attName, out bool value ) {
            string attText = GetAttributeText( node, attName );
            if( attText != null && bool.TryParse( attText, out value ) ) {
                return true;
            }
            value = false;
            return false;
        }
    }
}
