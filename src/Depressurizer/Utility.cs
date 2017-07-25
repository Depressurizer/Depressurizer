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
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace Depressurizer
{
    public static class Utility
    {
        #region File Backups

        /// <summary>
        /// Moves a file to back it up in anticipation of a save. Maintains a certain number of old versions of the file.
        /// </summary>
        /// <param name="filePath">File to move</param>
        /// <param name="maxBackups">The number of old versions to maintain</param>
        public static void BackupFile(string filePath, int maxBackups)
        {
            if (maxBackups < 1)
            {
                return;
            }

            string targetPath = BackupFile_ClearSlot(filePath, maxBackups, 1);
            File.Copy(filePath, targetPath);
        }

        /// <summary>
        /// Clears a slot for a file to be moved into as part of a backup.
        /// </summary>
        /// <param name="basePath">Path of the main file that's being backed up.</param>
        /// <param name="maxBackups">The number of backups that we're looking to keep</param>
        /// <param name="current">The number of the backup file to process. For example, if 1, this is clearing a spot for the most recent backup.</param>
        /// <returns>The path of the cleared slot</returns>
        private static string BackupFile_ClearSlot(string basePath, int maxBackups, int current)
        {
            string thisPath = BackupFile_GetName(basePath, current);
            if (!File.Exists(thisPath))
            {
                return thisPath;
            }
            if (current >= maxBackups)
            {
                File.Delete(thisPath);
                return thisPath;
            }
            string moveTarget = BackupFile_ClearSlot(basePath, maxBackups, current + 1);
            File.Move(thisPath, moveTarget);
            return thisPath;
        }

        /// <summary>
        /// Gets the name for a certain backup slot.
        /// </summary>
        /// <param name="baseName">Name of the current version of the file</param>
        /// <param name="slotNum">Slot number to get the name for</param>
        /// <returns>The name</returns>
        private static string BackupFile_GetName(string baseName, int slotNum)
        {
            if (slotNum == 0)
            {
                return baseName;
            }

            return string.Format("{0}.bak_{1}", baseName, slotNum);
        }

        #endregion

        #region Date and time

        /// <summary>
        /// Unix epoch
        /// </summary>
        static DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        ///  Steam Games without banners, ignore 404 warning
        /// </summary>
        static List<int> ignoreWarning = new List<int>
        {
            2430,
            17340,
            39530,
            44630,
            208610,
            215060,
            219540,
            228020,
            228040,
            228060,
            228080,
            228100,
            404730
        };

        /// <summary>
        /// Gets the current time as Unix timestamp
        /// </summary>
        /// <returns>Int containing Unix time</returns>
        public static int GetCurrentUTime()
        {
            return GetUTime(DateTime.UtcNow);
        }

        /// <summary>
        /// Converts a given DateTime to unix time
        /// </summary>
        /// <param name="dt">DateTime to convert</param>
        /// <returns>int containing unix time</returns>
        public static int GetUTime(DateTime dt)
        {
            double tSecs = (dt - epoch).TotalSeconds;
            if (tSecs > int.MaxValue)
            {
                return int.MaxValue;
            }
            if (tSecs < 0)
            {
                return 0;
            }

            return (int) tSecs;
        }

        /// <summary>
        /// Converts unix time to a DateTime object
        /// </summary>
        /// <param name="uTime">Unix time to convert</param>
        /// <returns>DateTime representation</returns>
        public static DateTime GetDTFromUTime(int uTime)
        {
            return epoch.AddSeconds(uTime);
        }

        #endregion

        #region General

        /// <summary>
        /// Compares two lists of strings for equality / sorting purposes.
        /// </summary>
        /// <param name="a">First list</param>
        /// <param name="b">Second list</param>
        /// <returns>0 if equal, negative if a is greater, positive if b is greater</returns>
        public static int CompareLists(List<string> a, List<string> b)
        {
            if (a == null)
            {
                return (b == null) ? 0 : 1;
            }
            if (b == null)
            {
                return -1;
            }
            for (int i = 0; (i < a.Count) && (i < b.Count); i++)
            {
                int res = string.Compare(a[i], b[i]);
                if (res != 0)
                {
                    return res;
                }
            }
            return b.Count - a.Count;
        }

        /// <summary>
        /// Clamp the value of an item to be between two values.
        /// </summary>
        /// <typeparam name="T">Type of the clamped object</typeparam>
        /// <param name="val">Value to clamp</param>
        /// <param name="min">Minimum return value</param>
        /// <param name="max">Maximum return value</param>
        /// <returns>If val is between min and max, return val. If greater than max, return max. If less than min, return min.</returns>
        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0)
            {
                return min;
            }
            if (val.CompareTo(max) > 0)
            {
                return max;
            }

            return val;
        }

        public static Image GetImage(string url, RequestCacheLevel cache, int id = 0)
        {
            try
            {
                return Image.FromStream(GetRemoteImageStream(url, id));
            }
            catch
            {
                if (!ignoreWarning.Contains(id))
                {
                    Program.Logger.Write(Rallion.LoggerLevel.Warning,
                        string.Format(GlobalStrings.Utility_GetImage, url));
                }
            }
            return null;
        }

        public static Stream GetRemoteImageStream(string url, int id = 0)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();

                // Check that the remote file was found. The ContentType
                // check is performed since a request for a non-existent
                // image file might be redirected to a 404-page, which would
                // yield the StatusCode "OK", even though the image was not
                // found.
                if (((response.StatusCode == HttpStatusCode.OK) ||
                     (response.StatusCode == HttpStatusCode.Moved) ||
                     (response.StatusCode == HttpStatusCode.Redirect)) &&
                    response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
                {
                    return response.GetResponseStream();
                }
            }
            catch
            {
                if (!ignoreWarning.Contains(id))
                {
                    Program.Logger.Write(Rallion.LoggerLevel.Warning,
                        string.Format(GlobalStrings.Utility_GetImage, url));
                }
            }
            return null;
        }

        public static bool SaveRemoteImageToFile(string url, string localPath, int id = 0)
        {
            try
            {
                using (Stream inputStream = GetRemoteImageStream(url, id))
                {
                    if (inputStream == null)
                    {
                        return false;
                    }

                    using (Stream outputStream = File.OpenWrite(localPath))
                    {
                        byte[] buffer = new byte[4096];
                        int bytesRead;
                        do
                        {
                            bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                            outputStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead != 0);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Program.Logger.WriteException(string.Format(GlobalStrings.Utility_SaveBanner, localPath), e);
                return false;
            }
        }

        public static bool GrabBanner(int id)
        {
            string bannerURL = string.Format(Properties.Resources.UrlGameBanner, id);
            string bannerPath = string.Format(Properties.Resources.GameBannerPath,
                Path.GetDirectoryName(Application.ExecutablePath), id);

            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(bannerPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(bannerPath));
                }

                return SaveRemoteImageToFile(bannerURL, bannerPath, id);
            }
            catch
            {
                Program.Logger.Write(Rallion.LoggerLevel.Warning,
                    string.Format(GlobalStrings.GameData_GetBanner, bannerURL));
                return false;
            }
        }

        public static bool IsOnScreen(MaterialForm form)
        {
            Screen[] screens = Screen.AllScreens;
            foreach (Screen screen in screens)
            {
                Point formTopLeft = new Point(form.Left, form.Top);

                if (screen.WorkingArea.Contains(formTopLeft))
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if ((attributes != null) && (attributes.Length > 0))
            {
                return attributes[0].Description;
            }

            return value.ToString();
        }

        public static void MoveItem(ListBox lb, int direction)
        {
            // Checking selected item
            if ((lb.SelectedItem == null) || (lb.SelectedIndex < 0) || (lb.SelectedItems.Count > 1))
            {
                return; // No selected item or more than one item selected - nothing to do
            }

            // Calculate new index using move direction
            int newIndex = lb.SelectedIndex + direction;

            // Checking bounds of the range
            if ((newIndex < 0) || (newIndex >= lb.Items.Count))
            {
                return; // Index out of range - nothing to do
            }

            object selected = lb.SelectedItem;

            // Removing removable element
            lb.Items.Remove(selected);
            // Insert it in new position
            lb.Items.Insert(newIndex, selected);
            // Restore selection
            lb.SetSelected(newIndex, true);
        }

        #endregion

        #region Language

        public static CultureInfo GetCultureInfoFromStoreLanguage(StoreLanguage dbLanguage)
        {
            string l = Enum.GetName(typeof(StoreLanguage), dbLanguage);
            CultureInfo culture = CultureInfo.GetCultureInfo("en");
            if (l == "zh_Hans")
            {
                culture = CultureInfo.GetCultureInfo("zh-Hans");
            }
            else if (l == "zh_Hant")
            {
                culture = CultureInfo.GetCultureInfo("zh-Hant");
            }
            else if (l == "pt-BR")
            {
                culture = CultureInfo.GetCultureInfo("pt-BR");
            }
            else if (l == "windows")
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                if (Enum.GetNames(typeof(StoreLanguage)).ToList().Contains(currentCulture.TwoLetterISOLanguageName))
                {
                    culture = currentCulture;
                }
                else
                {
                    if ((currentCulture.Name == "zh-Hans") || (currentCulture.Parent.Name == "zh-Hans"))
                    {
                        culture = CultureInfo.GetCultureInfo("zh-Hans");
                    }
                    else if ((currentCulture.Name == "zh-Hant") || (currentCulture.Parent.Name == "zh-Hant"))
                    {
                        culture = CultureInfo.GetCultureInfo("zh-Hant");
                    }
                    else if ((currentCulture.Name == "pt-BR") || (currentCulture.Parent.Name == "pt-BR"))
                    {
                        culture = CultureInfo.GetCultureInfo("pt-BR");
                    }
                }
            }
            else
            {
                culture = CultureInfo.GetCultureInfo(l);
            }
            return culture;
        }

        #endregion
    }
}