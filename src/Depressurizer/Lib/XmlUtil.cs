#region LICENSE

//     This file (XmlUtil.cs) is part of Depressurizer.
//     Copyright (C) 2011 Steve Labbe
//     Copyright (C) 2017 Theodoros Dimos
//     Copyright (C) 2017 Martijn Vegter
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

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
            bool result;
            return TryGetBoolFromAttribute(node, attName, out result) ? result : defaultValue;
        }

        public static bool GetBoolFromNode(XmlNode node, bool defaultValue)
        {
            bool result;
            return TryGetBoolFromNode(node, out result) ? result : defaultValue;
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
            float result;
            return TryGetFloatFromNode(node, out result) ? result : defaultValue;
        }

        public static long GetInt64FromNode(XmlNode node, long defaultValue)
        {
            long result;
            return TryGetInt64FromNode(node, out result) ? result : defaultValue;
        }

        public static int GetIntFromNode(XmlNode node, int defaultValue)
        {
            int result;
            return TryGetIntFromNode(node, out result) ? result : defaultValue;
        }

        public static string GetStringFromNode(XmlNode node, string defaultValue)
        {
            string result;
            return TryGetStringFromNode(node, out result) ? result : defaultValue;
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
