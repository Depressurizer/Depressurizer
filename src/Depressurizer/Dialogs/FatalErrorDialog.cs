#region License

//     This file (FatalErrorDialog.cs) is part of Depressurizer.
//     Copyright (C) 2011  Steve Labbe
//     Copyright (C) 2018  Martijn Vegter
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
//     along with this program.  If not, see <https://www.gnu.org/licenses/>.

#endregion

using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Depressurizer.Helpers;

namespace Depressurizer.Dialogs
{
	internal delegate DialogResult DlgMessageBox(string text);

	public partial class FatalErrorDialog : Form
	{
		#region Constants

		private const int DefaultInfoHeight = 250;

		private const int MaxHeight = 2000;

		private const int MaxWidth = 2000;

		private const int MinHeight = 300;

		private const int MinWidth = 300;

		#endregion

		#region Fields

		private readonly Exception _exception;

		private int _currentInfoHeight = DefaultInfoHeight;

		private bool _showingInfo;

		#endregion

		#region Constructors and Destructors

		private FatalErrorDialog(Exception e)
		{
			InitializeComponent();
			_exception = e;
			_showingInfo = true;
			TopMost = true;
			TopLevel = true;
		}

		#endregion

		#region Properties

		/// <summary>
		///     The minimum height of the form, without info showing.
		/// </summary>
		private int ShortHeight => (Height - ClientSize.Height) + cmdClose.Bottom + 10;

		#endregion

		#region Public Methods and Operators

		/// <summary>
		///     Starts catching all unhandled exceptions for processing.
		/// </summary>
		public static void InitializeHandler()
		{
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			Application.ThreadException += Application_ThreadException;
		}

		#endregion

		#region Methods

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			HandleUnhandledException(e.Exception);
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			HandleUnhandledException((Exception) e.ExceptionObject);
		}

		/// <summary>
		///     Shows the form and ends the program after an exception makes it to the top level.
		/// </summary>
		/// <param name="e">The unhandled exception.</param>
		private static void HandleUnhandledException(Exception e)
		{
			Program.Logger.WriteException("Fatal Error: ", e);
			SentryLogger.Log(e);

			using (FatalErrorDialog dialog = new FatalErrorDialog(e))
			{
				dialog.ShowDialog();
			}

			Application.Exit();
		}

		private void cmdCopy_Click(object sender, EventArgs e)
		{
			SetClipboardText();
		}

		private void cmdSave_Click(object sender, EventArgs e)
		{
			SaveToFile();
		}

		private void cmdShow_Click(object sender, EventArgs e)
		{
			ToggleInfo();
		}

		private void FatalError_Load(object sender, EventArgs e)
		{
			HideInfo();

			string appName = Application.ProductName;

			Text = string.Format(GlobalStrings.DlgFatalError_FatalError, appName);
			lblMessage.Text = string.Format(GlobalStrings.DlgFatalError_FatalErrorOcurred, appName);

			FillFields();

			Activate();
		}

		private void FillFields()
		{
			string innerExStackTraceSep = "---End of inner exception stack trace---" + Environment.NewLine;
			txtErrType.Text = _exception.GetType().Name;
			txtErrMsg.Text = _exception.Message;
			txtTrace.Text = _exception.StackTrace;
			Exception innerEx = _exception.InnerException;
			while (innerEx != null)
			{
				txtErrMsg.Text += Environment.NewLine + innerEx.GetType().Name + @": " + innerEx.Message;
				txtTrace.Text = innerEx.StackTrace + innerExStackTraceSep + txtTrace.Text;
				innerEx = innerEx.InnerException;
			}
		}

		/// <summary>
		///     Hides the extra info
		/// </summary>
		private void HideInfo()
		{
			if (!_showingInfo)
			{
				return;
			}

			// Save the current info height in case we toggle back
			_currentInfoHeight = Height - ShortHeight;

			// Resize and disable user resizing
			int newHeight = ShortHeight;
			MinimumSize = new Size(MinWidth, newHeight);
			MaximumSize = new Size(MaxWidth, newHeight);
			Height = newHeight;

			// Hide extra components
			grpMoreInfo.Visible = grpMoreInfo.Enabled = _showingInfo = false;
		}

		private void SaveToFile()
		{
			try
			{
				using (SaveFileDialog dialog = new SaveFileDialog())
				{
					dialog.CreatePrompt = false;
					dialog.AddExtension = false;
					dialog.AutoUpgradeEnabled = true;
					dialog.InitialDirectory = Environment.CurrentDirectory;
					dialog.FileName = "error_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".log";

					DialogResult result = dialog.ShowDialog();
					if (result != DialogResult.OK)
					{
						return;
					}

					using (StreamWriter fstr = new StreamWriter(dialog.FileName))
					{
						fstr.Write(_exception.ToString());
					}

					MessageBox.Show(GlobalStrings.DlgFatalError_ErrorInformationSaved);
				}
			}
			catch
			{
				MessageBox.Show(GlobalStrings.DlgFatalError_CouldNotSaveErrorInformation);
			}
		}

		private void SetClipboardText()
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				Thread t = new Thread(SetClipboardText);
				t.SetApartmentState(ApartmentState.STA);
				t.Start();
			}
			else
			{
				string dMsg = GlobalStrings.DlgFatalError_CouldNotCopyClipboard;
				try
				{
					Clipboard.SetText(_exception.ToString());
					dMsg = GlobalStrings.DlgFatalError_ClipboardUpdated;
				}
				finally
				{
					if (InvokeRequired)
					{
						Invoke(new DlgMessageBox(MessageBox.Show), dMsg);
					}
					else
					{
						MessageBox.Show(dMsg);
					}
				}
			}
		}

		/// <summary>
		///     Displays the extra info
		/// </summary>
		private void ShowInfo()
		{
			if (_showingInfo)
			{
				return;
			}

			// Increase the form height and allow resizing
			MinimumSize = new Size(MinWidth, MinHeight);
			MaximumSize = new Size(MaxWidth, MaxHeight);
			Height = ShortHeight + _currentInfoHeight;
			// Show extra components
			grpMoreInfo.Visible = grpMoreInfo.Enabled = _showingInfo = true;
		}

		/// <summary>
		///     Toggles the extra info
		/// </summary>
		private void ToggleInfo()
		{
			if (_showingInfo)
			{
				HideInfo();
			}
			else
			{
				ShowInfo();
			}
		}

		#endregion
	}
}