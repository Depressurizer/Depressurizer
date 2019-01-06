using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using Depressurizer.Core.Helpers;

namespace Depressurizer
{
    public static class Utility
    {
        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Moves a file to back it up in anticipation of a save. Maintains a certain number of old versions of the file.
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
        ///     Clamp the value of an item to be between two values.
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

        /// <summary>
        ///     Compares two lists of strings for equality / sorting purposes.
        /// </summary>
        /// <param name="a">First list</param>
        /// <param name="b">Second list</param>
        /// <returns>0 if equal, negative if a is greater, positive if b is greater</returns>
        public static int CompareLists(IList<string> a, IList<string> b)
        {
            if (a == null)
            {
                return b == null ? 0 : 1;
            }

            if (b == null)
            {
                return -1;
            }

            for (int i = 0; i < a.Count && i < b.Count; i++)
            {
                int res = string.CompareOrdinal(a[i], b[i]);
                if (res != 0)
                {
                    return res;
                }
            }

            return b.Count - a.Count;
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return value.ToString();
        }

        public static Image GetImage(string url)
        {
            try
            {
                return Image.FromStream(GetRemoteImageStream(url));
            }
            catch (Exception e)
            {
                Logger.Warn("GetImage: Error getting image from {0}, error: {1}.", url, e.Message);
            }

            return null;
        }

        public static Stream GetRemoteImageStream(string url)
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
                if ((response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Moved || response.StatusCode == HttpStatusCode.Redirect) && response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
                {
                    return response.GetResponseStream();
                }
            }
            catch (Exception e)
            {
                Logger.Warn("GetRemoteImageStream: Error getting image stream from {0}, error: {1}.", url, e.Message);
            }

            return null;
        }

        public static bool IsOnScreen(Control form)
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                Point formTopLeft = new Point(form.Left, form.Top);

                if (screen.WorkingArea.Contains(formTopLeft))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Opens the store page for the specified app in the default browser.
        /// </summary>
        /// <param name="appId"></param>
        public static void LaunchStorePage(int appId)
        {
            Process.Start(string.Format(CultureInfo.InvariantCulture, Constants.SteamStoreApp, appId));
        }

        public static void MoveItem(ListBox lb, int direction)
        {
            // Checking selected item
            if (lb.SelectedItem == null || lb.SelectedIndex < 0 || lb.SelectedItems.Count > 1)
            {
                return; // No selected item or more than one item selected - nothing to do
            }

            // Calculate new index using move direction
            int newIndex = lb.SelectedIndex + direction;

            // Checking bounds of the range
            if (newIndex < 0 || newIndex >= lb.Items.Count)
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

        #region Methods

        /// <summary>
        ///     Clears a slot for a file to be moved into as part of a backup.
        /// </summary>
        /// <param name="basePath">Path of the main file that's being backed up.</param>
        /// <param name="maxBackups">The number of backups that we're looking to keep</param>
        /// <param name="current">
        ///     The number of the backup file to process. For example, if 1, this is clearing a spot for the most
        ///     recent backup.
        /// </param>
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
        ///     Gets the name for a certain backup slot.
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

            return $"{baseName}.bak_{slotNum}";
        }

        #endregion
    }
}
