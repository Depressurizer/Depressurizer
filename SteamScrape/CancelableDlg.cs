/*
Copyright 2011, 2012 Steve Labbe.

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
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DPLib;

namespace SteamScrape {
    public abstract partial class CancelableDlg : Form {

        protected object abortLock = new object();

        int threadsToRun = 5;
        int runningThreads;

        protected int totalJobs = 1;
        protected int jobsCompleted = 0;

        bool _abort = false;
        protected bool Aborted {
            get {
                lock( abortLock ) {
                    return _abort;
                }
            }
            set {
                lock( abortLock ) {
                    _abort = value;
                }
            }
        }

        delegate void SimpleDelegate();
        delegate void TextUpdateDelegate( string s );

        public CancelableDlg( string title = "" ) {
            InitializeComponent();
            this.Text = title;

            DialogResult = DialogResult.OK;
        }

        protected virtual void UpdateForm_Load( object sender, EventArgs e ) {
            threadsToRun = Math.Min( threadsToRun, totalJobs );
            for( int i = 0; i < threadsToRun; i++ ) {
                Thread t = new Thread( new ThreadStart( RunProcess ) );
                t.Start();
                runningThreads++;
            }
            UpdateText();
        }

        protected abstract void RunProcess();

        protected void OnJobCompletion() {
            if( InvokeRequired ) {
                Invoke( new SimpleDelegate( OnJobCompletion ) );
            } else {
                jobsCompleted++;
                UpdateText();
            }
        }

        protected void OnThreadEnd() {
            if( InvokeRequired ) {
                Invoke( new SimpleDelegate( OnThreadEnd ) );
            } else {
                if( !Aborted ) {
                    runningThreads--;
                    if( runningThreads <= 0 ) {
                        this.Close();
                    }
                }
            }
        }

        private void cmdStop_Click( object sender, EventArgs e ) {
            DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void UpdateForm_FormClosing( object sender, FormClosingEventArgs e ) {
            lock( abortLock ) {
                Aborted = true;
            }
        }

        protected void SetText( string s ) {
            if( this.InvokeRequired ) {
                this.Invoke( new TextUpdateDelegate( SetText ), s );
            } else {
                lblText.Text = s;
            }
        }

        protected virtual void UpdateText() {}

        protected void DisableAbort() {
            if( InvokeRequired ) {
                Invoke( new SimpleDelegate( DisableAbort ) );
            } else {
                cmdStop.Enabled = false;
            }

        }
    }
}
