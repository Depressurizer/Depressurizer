#region LICENSE

//     This file (CancelableDialog.cs) is part of Depressurizer.
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
using System.Threading.Tasks;
using System.Windows.Forms;
using DepressurizerCore.Helpers;

namespace Depressurizer.Forms
{
    public partial class CancelableDialog : DepressurizerDialog
    {
        #region Static Fields

        protected static readonly object SyncRoot = new object();

        #endregion

        #region Fields

        protected int CompletedJobs = 0;

        protected int TotalJobs = 0;

        private bool _stopped = false;

        #endregion

        #region Constructors and Destructors

        public CancelableDialog(string title, bool showStopButton)
        {
            InitializeComponent();

            Text = title;
            ButtonStop.Enabled = ButtonStop.Visible = showStopButton;
        }

        #endregion

        #region Delegates

        private delegate void SetTextDelegate(string s);

        private delegate void SimpleDelegate();

        #endregion

        #region Public Properties

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        #endregion

        #region Properties

        protected bool Canceled { get; set; } = false;

        protected bool Stopped
        {
            get
            {
                lock (SyncRoot)
                {
                    return _stopped;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    _stopped = value;
                }
            }
        }

        #endregion

        #region Methods

        protected void CloseDialog()
        {
            if (InvokeRequired)
            {
                Invoke(new SimpleDelegate(CloseDialog));
            }
            else
            {
                Close();
            }
        }

        protected virtual void OnFinish() { }

        protected virtual void OnJobCompletion()
        {
            CompletedJobs++;

            try
            {
                UpdateText();
            }
            catch (Exception e)
            {
                SentryLogger.LogException(e);

                throw;
            }
        }

        protected virtual void OnStart() { }

        protected void SetText(string s)
        {
            if (InvokeRequired)
            {
                Invoke(new SetTextDelegate(SetText), s);
            }
            else
            {
                LabelStatus.Text = s;
            }
        }

        protected virtual void UpdateText() { }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            lock (SyncRoot)
            {
                Stopped = true;
            }

            Canceled = true;

            DisableButtons();
            Close();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            lock (SyncRoot)
            {
                Stopped = true;
            }

            DisableButtons();
            Close();
        }

        private void CancelableDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            lock (SyncRoot)
            {
                DialogResult = DialogResult.OK;
                if (Stopped)
                {
                    DialogResult = DialogResult.Abort;
                }
                else if (Canceled)
                {
                    DialogResult = DialogResult.Cancel;
                }

                Stopped = true;
            }

            DisableButtons();

            try
            {
                OnFinish();
            }
            catch (Exception exception)
            {
                SentryLogger.LogException(exception);

                throw;
            }
        }

        private void CancelableDialog_Load(object sender, EventArgs e)
        {
            try
            {
                Task.Run(() => OnStart());
            }
            catch (Exception exception)
            {
                SentryLogger.LogException(exception);

                throw;
            }
        }

        private void DisableButtons()
        {
            if (InvokeRequired)
            {
                Invoke(new SimpleDelegate(DisableButtons));
            }
            else
            {
                ButtonStop.Enabled = ButtonCancel.Enabled = false;
            }
        }

        #endregion
    }
}