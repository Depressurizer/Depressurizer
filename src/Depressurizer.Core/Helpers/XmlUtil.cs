﻿using System;
using System.Collections.Generic;
using System.Xml;

namespace Depressurizer.Core.Helpers
{
    public static class XmlUtil
    {
        #region Public Methods and Operators

        public static bool GetBoolFromNode(XmlNode node, bool defaultValue)
        {
            return TryGetBoolFromNode(node, out bool result) ? result : defaultValue;
        }

        public static double GetDoubleFromNode(XmlNode node, double defaultValue)
        {
            return TryGetDoubleFromNode(node, out double result) ? result : defaultValue;
        }

        public static TEnum GetEnumFromNode<TEnum>(XmlNode node, TEnum defaultValue) where TEnum : struct, IComparable, IConvertible, IFormattable
        {
            if (node == null)
            {
                return defaultValue;
            }

            XmlNode textNode = node.SelectSingleNode("text()");
            if (textNode == null)
            {
                return defaultValue;
            }

            string str = textNode.InnerText;
            if (Enum.TryParse(str, out TEnum res))
            {
                return res;
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

        public static bool TryGetBoolFromNode(XmlNode node, out bool value)
        {
            value = false;

            XmlNode textNode = node?.SelectSingleNode("text()");
            if (textNode != null && bool.TryParse(textNode.InnerText, out value))
            {
                return true;
            }

            return false;
        }

        public static bool TryGetDoubleFromNode(XmlNode node, out double value)
        {
            value = 0;

            XmlNode textNode = node?.SelectSingleNode("text()");
            if (textNode != null && double.TryParse(textNode.InnerText, out value))
            {
                return true;
            }

            return false;
        }

        public static bool TryGetFloatFromNode(XmlNode node, out float value)
        {
            value = 0;
            XmlNode textNode = node?.SelectSingleNode("text()");
            if (textNode != null && float.TryParse(textNode.InnerText, out value))
            {
                return true;
            }

            return false;
        }

        public static bool TryGetInt64FromNode(XmlNode node, out long value)
        {
            value = 0;

            XmlNode textNode = node?.SelectSingleNode("text()");
            if (textNode != null && long.TryParse(textNode.InnerText, out value))
            {
                return true;
            }

            return false;
        }

        public static bool TryGetIntFromNode(XmlNode node, out int value)
        {
            value = 0;

            XmlNode textNode = node?.SelectSingleNode("text()");
            if (textNode != null && int.TryParse(textNode.InnerText, out value))
            {
                return true;
            }

            return false;
        }

        public static bool TryGetStringFromNode(XmlNode node, out string value)
        {
            value = string.Empty;

            XmlNode textNode = node?.SelectSingleNode("text()");
            if (textNode == null)
            {
                return false;
            }

            value = node.InnerText;
            return true;
        }

        #endregion
    }
}
