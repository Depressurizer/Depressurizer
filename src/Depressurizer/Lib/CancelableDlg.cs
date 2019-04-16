using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Depressurizer.Core.Helpers;

namespace Rallion
{
    public partial class CancelableDlg : Form
    {
        #region Fields

        protected object abortLock = new object();

        protected int jobsCompleted;

        protected int runningThreads;

        protected int ThreadsToRun = Environment.ProcessorCount;

        protected int totalJobs = 1;

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

        private delegate void SimpleDelegate();

        private delegate void TextUpdateDelegate(string s);

        #endregion

        #region Public Properties

        public Exception Error { get; protected set; }

        public int JobsCompleted => jobsCompleted;

        public int JobsTotal => totalJobs;

        public List<Thread> Threads { get; set; }

        #endregion

        #region Properties

        protected static Logger Logger => Logger.Instance;

        protected bool Canceled { get; private set; }

        protected bool Stopped { get; set; }

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

        protected virtual void Finish() { }

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
                Logger.Info("CancelableDlg:{0} | Thread completed, still running {1}.", Text, runningThreads);
            }
        }

        protected virtual void RunProcess() { }

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
            ThreadsToRun = Math.Min(ThreadsToRun, totalJobs);
            Threads = new List<Thread>(ThreadsToRun);

            for (int i = 0; i < ThreadsToRun; i++)
            {
                Thread t = new Thread(RunProcessChecked);
                Threads.Add(t);

                t.Start();
                runningThreads++;
            }

            Thread thread = new Thread(CheckClose)
            {
                IsBackground = true
            };
            thread.Start();

            UpdateText();
        }

        protected virtual void UpdateText() { }

        private void CheckClose()
        {
            const int delay = 500;

            while (runningThreads != 0)
            {
                Thread.Sleep(delay);
            }

            if (InvokeRequired)
            {
                Invoke(new SimpleDelegate(Close));
            }
            else
            {
                Close();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            lock (abortLock)
            {
                Stopped = true;
                Canceled = true;
            }

            DisableAbort();
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            lock (abortLock)
            {
                Stopped = true;
            }

            DisableAbort();
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

            Logger.Info("Waiting on threads to exit...");
            foreach (Thread t in Threads)
            {
                Logger.Info("Joining thread {0}...", t.ManagedThreadId);
                t.Join();
                Logger.Info("Thread {0} joined...", t.ManagedThreadId);
            }
            Logger.Info("All threads have exited...");

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
