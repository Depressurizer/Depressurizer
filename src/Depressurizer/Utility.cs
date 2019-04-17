using System;
using System.ComponentModel;
using System.Drawing;
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

        public static void MoveItem(ListBox listBox, int direction)
        {
            // Checking selected item
            if (listBox?.SelectedItem == null)
            {
                return;
            }

            if (listBox.SelectedIndex < 0 || listBox.SelectedItems.Count > 1)
            {
                // No selected item or more than one item selected
                return;
            }

            // Calculate new index using move direction
            int newIndex = listBox.SelectedIndex + direction;

            // Checking bounds of the range
            if (newIndex < 0 || newIndex >= listBox.Items.Count)
            {
                // Index out of range, nothing to do.
                return;
            }

            object selected = listBox.SelectedItem;

            // Removing removable element
            listBox.Items.Remove(selected);

            // Insert it in new position
            listBox.Items.Insert(newIndex, selected);

            // Restore selection
            listBox.SetSelected(newIndex, true);
        }

        #endregion
    }
}
