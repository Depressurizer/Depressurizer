using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Depressurizer
{
    /// <summary>
    ///     This allows drawing sorting arrows on the columns in the ListView.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ListViewExtensions
    {
        #region Constants

        public const int HDM_FIRST = 0x1200;

        public const int HDM_GETITEM = HDM_FIRST + 11;

        public const int HDM_SETITEM = HDM_FIRST + 12;

        public const int LVM_FIRST = 0x1000;

        public const int LVM_GETHEADER = LVM_FIRST + 31;

        #endregion

        #region Public Methods and Operators

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, ref HDITEM lParam);

        public static void SetSortIcon(this ListView listViewControl, int columnIndex, SortOrder order)
        {
            IntPtr columnHeader = SendMessage(listViewControl.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);
            for (int columnNumber = 0; columnNumber <= listViewControl.Columns.Count - 1; columnNumber++)
            {
                IntPtr columnPtr = new IntPtr(columnNumber);
                HDITEM item = new HDITEM
                {
                    mask = HDITEM.Mask.Format
                };

                if (SendMessage(columnHeader, HDM_GETITEM, columnPtr, ref item) == IntPtr.Zero)
                {
                    throw new Win32Exception();
                }

                if (order != SortOrder.None && columnNumber == columnIndex)
                {
                    switch (order)
                    {
                        case SortOrder.Ascending:
                            item.fmt &= ~HDITEM.Format.SortDown;
                            item.fmt |= HDITEM.Format.SortUp;
                            break;
                        case SortOrder.Descending:
                            item.fmt &= ~HDITEM.Format.SortUp;
                            item.fmt |= HDITEM.Format.SortDown;
                            break;
                    }
                }
                else
                {
                    item.fmt &= ~HDITEM.Format.SortDown & ~HDITEM.Format.SortUp;
                }

                if (SendMessage(columnHeader, HDM_SETITEM, columnPtr, ref item) == IntPtr.Zero)
                {
                    throw new Win32Exception();
                }
            }
        }

        #endregion

        [StructLayout(LayoutKind.Sequential)]
        public struct HDITEM
        {
            #region Fields

            public int cchTextMax;

            public int cxy;

            public Format fmt;

            public IntPtr hbm;

            // _WIN32_IE >= 0x0300 
            public int iImage;

            public int iOrder;

            public IntPtr lParam;

            public Mask mask;

            [MarshalAs(UnmanagedType.LPTStr)]
            public string pszText;

            public IntPtr pvFilter;

            // _WIN32_WINNT >= 0x0600
            public uint state;

            // _WIN32_IE >= 0x0500
            public uint type;

            #endregion

            #region Enums

            [Flags]
            public enum Format
            {
                SortDown = 0x200, // HDF_SORTDOWN

                SortUp = 0x400 // HDF_SORTUP
            }

            [Flags]
            public enum Mask
            {
                Format = 0x4 // HDI_FORMAT
            }

            #endregion
        }
    }
}
