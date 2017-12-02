/*
This file is part of Depressurizer.
Copyright 2011, 2012, 2013 Steve Labbe.

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

namespace Depressurizer
{
    public static class XmlUtil
    {
        public static string GetStringFromNode(XmlNode node, string defaultValue)
        {
            string result;
            return TryGetStringFromNode(node, out result) ? result : defaultValue;
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

        public static int GetIntFromNode(XmlNode node, int defaultValue)
        {
            int result;
            return TryGetIntFromNode(node, out result) ? result : defaultValue;
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

        public static float GetFloatFromNode(XmlNode node, float defaultValue)
        {
            float result;
            return TryGetFloatFromNode(node, out result) ? result : defaultValue;
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

        public static Int64 GetInt64FromNode(XmlNode node, Int64 defaultValue)
        {
            Int64 result;
            return TryGetInt64FromNode(node, out result) ? result : defaultValue;
        }

        public static bool TryGetInt64FromNode(XmlNode node, out Int64 value)
        {
            if (node != null)
            {
                XmlNode textNode = node.SelectSingleNode("text()");
                if (textNode != null && Int64.TryParse(textNode.InnerText, out value))
                {
                    return true;
                }
            }
            value = 0;
            return false;
        }

        public static bool GetBoolFromNode(XmlNode node, bool defaultValue)
        {
            bool result;
            return TryGetBoolFromNode(node, out result) ? result : defaultValue;
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
            bool result;
            return TryGetBoolFromAttribute(node, attName, out result) ? result : defaultValue;
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

        public static TEnum GetEnumFromNode<TEnum>(XmlNode node, TEnum defaultValue)
            where TEnum : struct, IComparable, IConvertible, IFormattable
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
            if (result.Count == 0) return null;
            return result;
        }
    }
}