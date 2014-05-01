/*
Copyright 2011, 2012, 2013 Steve Labbe.

This file is part of Depressurizer.

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
using System.Threading;
using System.Windows.Forms;

namespace Rallion {
    public partial class CancelableDlg : Form {
        #region Fields
        protected object abortLock = new object();

        protected int threadsToRun = 5;
        protected int runningThreads;

        protected int totalJobs = 1;
        public int JobsTotal {
            get {
                return totalJobs;
            }
        }
        protected int jobsCompleted = 0;
        public int JobsCompleted {
            get {
                return jobsCompleted;
            }
        }

        private bool _stopped = false;
        protected bool Stopped {
            get {
                lock( abortLock ) {
                    return _stopped;
                }
            }
            set {
                lock( abortLock ) {
                    _stopped = value;
                }
            }
        }

        protected bool Canceled { get; private set; }

        public Exception Error { get; protected set; }

        delegate void SimpleDelegate();
        delegate void TextUpdateDelegate( string s );
        delegate void EndProcDelegate( bool b );
        #endregion

        #region Initialization
        public CancelableDlg( string title, bool stopButton ) {
            InitializeComponent();
            this.Text = title;
            Canceled = false;

            cmdStop.Enabled = cmdStop.Visible = stopButton;
        }

        protected virtual void UpdateForm_Load( object sender, EventArgs e ) {
            threadsToRun = Math.Min( threadsToRun, totalJobs );
            for( int i = 0; i < threadsToRun; i++ ) {
                Thread t = new Thread( new ThreadStart( RunProcessChecked ) );
                t.Start();
                runningThreads++;
            }
            UpdateText();
        }

        private void RunProcessChecked() {
            try {
                RunProcess();
            } catch( Exception e ) {
                lock( abortLock ) {
                    Stopped = true;
                    Error = e;
                }
                Invoke( new SimpleDelegate( Close ) );
            }
        }
        #endregion

        #region Methods to override
        protected virtual void RunProcess() { }

        protected virtual void UpdateText() { }

        protected virtual void Finish() { }
        #endregion

        #region Status Updaters
        protected void OnJobCompletion() {
            lock( abortLock ) {
                jobsCompleted++;
            }
            UpdateText();
        }

        protected void OnThreadCompletion() {
            if( InvokeRequired ) {
                Invoke( new SimpleDelegate( OnThreadCompletion ) );
            } else {
                runningThreads--;
                if( runningThreads <= 0 ) {
                    this.Close();
                }
            }
        }
        #endregion

        #region Event Handlers
        private void cmdStop_Click( object sender, EventArgs e ) {
            Stopped = true;
            DisableAbort();
            this.Close();
        }

        private void UpdateForm_FormClosing( object sender, FormClosingEventArgs e ) {
            lock( abortLock ) {
                Stopped = true;
            }
            DisableAbort();
            DialogResult = ( jobsCompleted >= totalJobs ) ? DialogResult.OK : DialogResult.Abort;
            Finish();
            if( jobsCompleted >= totalJobs ) {
                DialogResult = DialogResult.OK;
            } else if( Canceled ) {
                DialogResult = DialogResult.Cancel;
            } else {
                DialogResult = DialogResult.Abort;
            }
        }
        #endregion

        #region UI Updaters
        protected void SetText( string s ) {
            if( this.InvokeRequired ) {
                this.Invoke( new TextUpdateDelegate( SetText ), s );
            } else {
                lblText.Text = s;
            }
        }

        protected void DisableAbort() {
            if( InvokeRequired ) {
                Invoke( new SimpleDelegate( DisableAbort ) );
            } else {
                cmdStop.Enabled = cmdCancel.Enabled = false;
            }
        }
        #endregion

        private void cmdCancel_Click( object sender, EventArgs e ) {
            Stopped = true;
            Canceled = true;
            DisableAbort();
            this.Close();
        }
    }
}
