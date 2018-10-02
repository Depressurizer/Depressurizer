#region LICENSE

//     This file (CancelableDlg.cs) is part of Depressurizer.
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
using System.Threading;
using System.Windows.Forms;

namespace Rallion
{
    public partial class CancelableDlg : Form
    {
        #region Fields

        protected object abortLock = new object();

        protected int jobsCompleted;
        protected int runningThreads;

        protected int threadsToRun = 5;

        protected int totalJobs = 1;

        private bool _stopped;

        #endregion

        #region Constructors and Destructors

        public CancelableDlg(string title, bool stopButton)
        {
            InitializeComponent();
            Text = title;
            Canceled = false;

            cmdStop.Enabled = cmdStop.Visible = stopButton;
        }

        #endregion

        #region Delegates

        private delegate void EndProcDelegate(bool b);

        private delegate void SimpleDelegate();

        private delegate void TextUpdateDelegate(string s);

        #endregion

        #region Public Properties

        public Exception Error { get; protected set; }

        public int JobsCompleted => jobsCompleted;

        public int JobsTotal => totalJobs;

        #endregion

        #region Properties

        protected bool Canceled { get; private set; }

        protected bool Stopped
        {
            get
            {
                lock (abortLock)
                {
                    return _stopped;
                }
            }
            set
            {
                lock (abortLock)
                {
                    _stopped = value;
                }
            }
        }

        #endregion

        #region Methods

        protected void DisableAbort()
        {
            if (InvokeRequired)
            {
                Invoke(new SimpleDelegate(DisableAbort));
            }
            else
            {
                cmdStop.Enabled = cmdCancel.Enabled = false;
            }
        }

        protected virtual void Finish()
        {
        }

        protected void OnJobCompletion()
        {
            lock (abortLock)
            {
                jobsCompleted++;
            }

            UpdateText();
        }

        protected void OnThreadCompletion()
        {
            if (InvokeRequired)
            {
                Invoke(new SimpleDelegate(OnThreadCompletion));
            }
            else
            {
                runningThreads--;
                if (runningThreads <= 0)
                {
                    Close();
                }
            }
        }

        protected virtual void RunProcess()
        {
        }

        protected void SetText(string s)
        {
            if (InvokeRequired)
            {
                Invoke(new TextUpdateDelegate(SetText), s);
            }
            else
            {
                lblText.Text = s;
            }
        }

        protected virtual void UpdateForm_Load(object sender, EventArgs e)
        {
            threadsToRun = Math.Min(threadsToRun, totalJobs);
            for (int i = 0; i < threadsToRun; i++)
            {
                Thread t = new Thread(RunProcessChecked);
                t.Start();
                runningThreads++;
            }

            UpdateText();
        }

        protected virtual void UpdateText()
        {
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Stopped = true;
            Canceled = true;
            DisableAbort();
            Close();
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            Stopped = true;
            DisableAbort();
            Close();
        }

        private void RunProcessChecked()
        {
            try
            {
                RunProcess();
            }
            catch (Exception e)
            {
                lock (abortLock)
                {
                    Stopped = true;
                    Error = e;
                }

                if (IsHandleCreated)
                {
                    Invoke(new SimpleDelegate(Finish));
                    Invoke(new SimpleDelegate(Close));
                }
            }
        }

        private void UpdateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            lock (abortLock)
            {
                Stopped = true;
            }

            DisableAbort();
            //DialogResult = ( jobsCompleted >= totalJobs ) ? DialogResult.OK : DialogResult.Abort;
            Finish();
            if (jobsCompleted >= totalJobs)
            {
                DialogResult = DialogResult.OK;
            }
            else if (Canceled)
            {
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                DialogResult = DialogResult.Abort;
            }
        }

        #endregion
    }
}
