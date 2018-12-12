﻿#region LICENSE

//     This file (DlgFatalError.cs) is part of Depressurizer.
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
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Depressurizer;
using Depressurizer.Helpers;
using Sentry;

namespace Rallion
{
    internal delegate DialogResult DLG_MessageBox(string text);

    public partial class FatalError : Form
    {
        #region Constants

        /// <summary>
        ///     The default height of the Info section
        /// </summary>
        private const int DEFAULT_INFO_HEIGHT = 250;

        private const int MAX_HEIGHT = 2000;

        private const int MAX_WIDTH = 2000;

        private const int MIN_HEIGHT = 300;

        private const int MIN_WIDTH = 300;

        #endregion

        #region Fields

        /// <summary>
        ///     The exception being displayed
        /// </summary>
        private readonly Exception ex;

        /// <summary>
        ///     The current height of the info section.
        /// </summary>
        private int currentInfoHeight = DEFAULT_INFO_HEIGHT;

        /// <summary>
        ///     Stores whether or not the extra info is being shown
        /// </summary>
        private bool ShowingInfo;

        #endregion

        #region Constructors and Destructors

        private FatalError(Exception e)
        {
            SentrySdk.CaptureException(e);

            InitializeComponent();
            ex = e;
            ShowingInfo = true;
            TopMost = true;
            TopLevel = true;
        }

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        /// <summary>
        ///     The minimum height of the form, without info showing.
        /// </summary>
        private int ShortHeight => Height - ClientSize.Height + cmdClose.Bottom + 10;

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
            Logger.Exception("Fatal Error: ", e);
            FatalError errForm = new FatalError(e);
            errForm.ShowDialog();
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
            txtErrType.Text = ex.GetType().Name;
            txtErrMsg.Text = ex.Message;
            txtTrace.Text = ex.StackTrace;
            Exception innerEx = ex.InnerException;
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
            if (!ShowingInfo)
            {
                return;
            }

            // Save the current info height in case we toggle back
            currentInfoHeight = Height - ShortHeight;
            // Resize and disable user resizing
            int newHeight = ShortHeight;
            MinimumSize = new Size(MIN_WIDTH, newHeight);
            MaximumSize = new Size(MAX_WIDTH, newHeight);
            Height = newHeight;
            // Hide extra components
            grpMoreInfo.Visible = grpMoreInfo.Enabled = ShowingInfo = false;
        }

        /// <summary>
        ///     Saves the exception data to a file in the application directory
        /// </summary>
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
                    dialog.FileName = "dError_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".log";

                    DialogResult result = dialog.ShowDialog();
                    if (result != DialogResult.OK)
                    {
                        return;
                    }

                    using (StreamWriter streamWriter = new StreamWriter(dialog.FileName))
                    {
                        streamWriter.Write(ex.ToString());
                    }

                    MessageBox.Show(GlobalStrings.DlgFatalError_ErrorInformationSaved);
                }
            }
            catch
            {
                MessageBox.Show(GlobalStrings.DlgFatalError_CouldNotSaveErrorInformation);
            }
        }

        /// <summary>
        ///     Copies the exception data to the clipboard
        /// </summary>
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
                    Clipboard.SetText(ex.ToString());
                    dMsg = GlobalStrings.DlgFatalError_ClipboardUpdated;
                }
                finally
                {
                    if (InvokeRequired)
                    {
                        Invoke(new DLG_MessageBox(MessageBox.Show), dMsg);
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
            if (ShowingInfo)
            {
                return;
            }

            // Increase the form height and allow resizing
            MinimumSize = new Size(MIN_WIDTH, MIN_HEIGHT);
            MaximumSize = new Size(MAX_WIDTH, MAX_HEIGHT);
            Height = ShortHeight + currentInfoHeight;
            // Show extra components
            grpMoreInfo.Visible = grpMoreInfo.Enabled = ShowingInfo = true;
        }

        /// <summary>
        ///     Toggles the extra info
        /// </summary>
        private void ToggleInfo()
        {
            if (ShowingInfo)
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
