using System;
using System.Collections.Generic;
using System.Xml;

namespace Depressurizer
{
    public static class XmlUtil
    {
        #region Public Methods and Operators

        public static string GetAttributeText(XmlNode node, string attName)
        {
            if (node != null)
            {
                XmlAttribute att = node.Attributes["attName"];
                if (att != null)
                {
                    return att.Value;
                }
            }

            return null;
        }

        public static bool GetBoolFromAttribute(XmlNode node, string attName, bool defaultValue)
        {
            return TryGetBoolFromAttribute(node, attName, out bool result) ? result : defaultValue;
        }

        public static bool GetBoolFromNode(XmlNode node, bool defaultValue)
        {
            return TryGetBoolFromNode(node, out bool result) ? result : defaultValue;
        }

        public static TEnum GetEnumFromNode<TEnum>(XmlNode node, TEnum defaultValue) where TEnum : struct, IComparable, IConvertible, IFormattable
        {
            if (node != null)
            {
                XmlNode textNode = node.SelectSingleNode("text()");
                if (textNode != null)
                {
                    string str = textNode.InnerText;
                    TEnum res = defaultValue;
                    if (Enum.TryParse(str, out res))
                    {
                        return res;
                    }
                }
            }

            return defaultValue;
        }

        public static float GetFloatFromNode(XmlNode node, float defaultValue)
        {
            return TryGetFloatFromNode(node, out float result) ? result : defaultValue;
        }

        public static long GetInt64FromNode(XmlNode node, long defaultValue)
        {
            return TryGetInt64FromNode(node, out long result) ? result : defaultValue;
        }

        public static int GetIntFromNode(XmlNode node, int defaultValue)
        {
            return TryGetIntFromNode(node, out int result) ? result : defaultValue;
        }

        public static string GetStringFromNode(XmlNode node, string defaultValue)
        {
            return TryGetStringFromNode(node, out string result) ? result : defaultValue;
        }

        public static List<string> GetStringsFromNodeList(XmlNodeList nodeList)
        {
            List<string> result = new List<string>();
            foreach (XmlNode node in nodeList)
            {
                string s = GetStringFromNode(node, null);
                if (s != null)
                {
                    result.Add(s);
                }
            }

            if (result.Count == 0)
            {
                return null;
            }

            return result;
        }

        public static bool TryGetBoolFromAttribute(XmlNode node, string attName, out bool value)
        {
            string attText = GetAttributeText(node, attName);
            if (attText != null && bool.TryParse(attText, out value))
            {
                return true;
            }

            value = false;
            return false;
        }

        public static bool TryGetBoolFromNode(XmlNode node, out bool value)
        {
            if (node != null)
            {
                XmlNode textNode = node.SelectSingleNode("text()");
                if (textNode != null && bool.TryParse(textNode.InnerText, out value))
                {
                    return true;
                }
            }

            value = false;
            return false;
        }

        public static bool TryGetFloatFromNode(XmlNode node, out float value)
        {
            if (node != null)
            {
                XmlNode textNode = node.SelectSingleNode("text()");
                if (textNode != null && float.TryParse(textNode.InnerText, out value))
                {
                    return true;
                }
            }

            value = 0;
            return false;
        }

        public static bool TryGetInt64FromNode(XmlNode node, out long value)
        {
            if (node != null)
            {
                XmlNode textNode = node.SelectSingleNode("text()");
                if (textNode != null && long.TryParse(textNode.InnerText, out value))
                {
                    return true;
                }
            }

            value = 0;
            return false;
        }

        public static bool TryGetIntFromNode(XmlNode node, out int value)
        {
            if (node != null)
            {
                XmlNode textNode = node.SelectSingleNode("text()");
                if (textNode != null && int.TryParse(textNode.InnerText, out value))
                {
                    return true;
                }
            }

            value = 0;
            return false;
        }

        public static bool TryGetStringFromNode(XmlNode node, out string value)
        {
            if (node != null)
            {
                XmlNode textNode = node.SelectSingleNode("text()");
                if (textNode != null)
                {
                    value = node.InnerText;
                    return true;
                }
            }

            value = string.Empty;
            return false;
        }

        #endregion
    }
}
