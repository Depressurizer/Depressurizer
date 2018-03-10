using System;
using System.Reflection;
using System.Windows.Forms;

namespace Depressurizer.Helpers
{
	/// <summary>
	/// Allows mouse wheel scrolling inside ToolStripDropDowns, e.g. all context menus.
	/// Adapted from: https://stackoverflow.com/a/27390000/383124
	/// </summary>
	internal static class DropdownMenuScrollWheelHandler
	{
		#region Fields

		private static MessageFilterImplementation _messageFilter;

		#endregion

		#region Methods

		/// <summary>
		/// Installs a global message filter to allow all ToolStripDropDowns to scroll via mouse wheel.
		/// <para/>
		/// No-op if already enabled.
		/// </summary>
		public static void Enable()
		{
			if (_messageFilter == null)
			{
				_messageFilter = new MessageFilterImplementation();
				Application.AddMessageFilter(_messageFilter);
			}
		}

		/// <summary>
		/// No-op if disabled/never enabled.
		/// </summary>
		public static void Disable()
		{
			if (_messageFilter != null)
			{
				Application.RemoveMessageFilter(_messageFilter);
				_messageFilter = null;
			}
		}

		#endregion

		#region Implementation

		private class MessageFilterImplementation : IMessageFilter
		{
			#region WM Constants

			private const int WM_MOUSEMOVE = 0x200;
			private const int WM_MOUSEWHEEL = 0x20A;

			#endregion

			#region Fields

			/// <summary>
			/// Helper to access non-public <a href="https://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/ToolStrip.cs">ScrollInternal</a> method.
			/// </summary>
			private static readonly Action<ToolStripDropDown, int> ScrollInternal =
				(Action<ToolStripDropDown, int>) Delegate.CreateDelegate(typeof(Action<ToolStripDropDown, int>),
					typeof(ToolStripDropDown).GetMethod("ScrollInternal",
						BindingFlags.NonPublic | BindingFlags.Instance));

			/// <summary>
			/// Window handle the mouse moved over last.
			/// </summary>
			private IntPtr _activeHwnd;

			/// <summary>
			/// ToolStripDropdown beneath mouse, or null if the mouse is over a different kind of control.
			/// </summary>
			private ToolStripDropDown _activeMenu;

			#endregion

			/// <summary>
			/// On mouse move, check whether the mouse moved to a different control. If it is a ToolStripDropDown, save the handle, otherwise, clear the handle. Then, the mouse move is processed as usual.
			/// <para/>
			/// On mouse wheel, scroll the drop down menu if the mouse is inside one. Then, filter out the message.
			/// </summary>
			/// <returns>Whether the message is filtered out, i.e. suppressed.</returns>
			public bool PreFilterMessage(ref Message m)
			{
				if (m.Msg == WM_MOUSEMOVE && _activeHwnd != m.HWnd)
				{
					_activeHwnd = m.HWnd;
					_activeMenu = Control.FromHandle(m.HWnd) as ToolStripDropDown;
				}
				else if (m.Msg == WM_MOUSEWHEEL && _activeMenu != null)
				{
					int delta = (short) (ushort) ((uint) (ulong) m.WParam >> 16);
					HandleDelta(_activeMenu, delta);
					return true;
				}

				return false;
			}

			private static void HandleDelta(ToolStripDropDown ts, int delta)
			{
				if (ts.Items.Count == 0) return;
				var firstItem = ts.Items[0];
				var lastItem = ts.Items[ts.Items.Count - 1];
				if (lastItem.Bounds.Bottom < ts.Height && firstItem.Bounds.Top > 0) return;
				// Controls the scroll speed and fix direction
				delta = delta / -2;
				if (delta < 0 && firstItem.Bounds.Top - delta > 9)
					delta = firstItem.Bounds.Top - 9;
				else if (delta > 0 && delta > lastItem.Bounds.Bottom - ts.Height + 9)
					delta = lastItem.Bounds.Bottom - ts.Height + 9;

				if (delta != 0) ScrollInternal(ts, delta);
			}
		}

		#endregion
	}
}