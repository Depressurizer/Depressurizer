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
using System.IO;
using System.Reflection;
using System.Xml;

namespace Rallion
{
    /// <summary>
    /// Base class for a settings object. Capable of loading and saving values of all public properties.
    /// </summary>
    abstract class AppSettings
    {
        protected readonly object threadLock = new object();

        protected bool outOfDate;

        public string FilePath;

        protected AppSettings()
        {
            FilePath = "Settings.xml";
        }

        /// <summary>
        /// Saves the contents of this instance to the defined config file.
        /// </summary>
        /// <param name="force">If false, will only save if the flag indicates that changes have been made. If true, always saves.</param>
        public void Save(bool force = false)
        {
            if (force || outOfDate)
            {
                Type t = GetType();

                PropertyInfo[] properties = t.GetProperties();
                XmlDocument doc = new XmlDocument();
                XmlElement config = doc.CreateElement("config");
                lock (threadLock)
                {
                    foreach (PropertyInfo pi in properties)
                    {
                        object val = pi.GetValue(this, null);
                        if (val != null)
                        {
                            XmlElement element = doc.CreateElement(pi.Name);
                            element.InnerText = val.ToString();
                            config.AppendChild(element);
                        }
                    }
                }
                doc.AppendChild(config);
                try
                {
                    doc.Save(FilePath);
                }
                catch (IOException) { }
                outOfDate = false;
            }
        }

        /// <summary>
        /// Loads settings from the defined config file.
        /// </summary>
        public virtual void Load()
        {
            Type type = GetType();
            if (File.Exists(FilePath))
            {
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(FilePath);
                    XmlNode configNode = doc.SelectSingleNode("/config");
                    lock (threadLock)
                    {
                        foreach (XmlNode node in configNode.ChildNodes)
                        {
                            string name = node.Name;
                            string value = node.InnerText;
                            PropertyInfo pi = type.GetProperty(name);
                            if (pi != null)
                            {
                                SetProperty(pi, value);
                            }
                        }
                    }
                }
                catch (XmlException) { }
                catch (IOException) { }
                outOfDate = false;
            }
        }

        private void SetProperty(PropertyInfo propertyInfo, string value)
        {
            try
            {
                if (propertyInfo.PropertyType.IsEnum)
                {
                    object eVal = Enum.Parse(propertyInfo.PropertyType, value, true);
                    propertyInfo.SetValue(this, eVal, null);
                }
                else if (propertyInfo.PropertyType == typeof(string))
                {
                    propertyInfo.SetValue(this, value, null);
                }
                else if (propertyInfo.PropertyType == typeof(bool))
                {
                    propertyInfo.SetValue(this, bool.Parse(value), null);
                }
                else if (propertyInfo.PropertyType == typeof(int))
                {
                    propertyInfo.SetValue(this, int.Parse(value), null);
                }
            }
            catch { }
        }
    }
}