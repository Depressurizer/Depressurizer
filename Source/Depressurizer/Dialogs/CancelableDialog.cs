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

namespace Depressurizer.Dialogs
{
	public partial class CancelableDialog : DepressurizerDialog
	{
		#region Static Fields

		protected static object SyncRoot = new object();

		#endregion

		#region Fields

		protected int jobsCompleted;

		protected int TotalJobs = 1;

		private bool _stopped;

		#endregion

		#region Constructors and Destructors

		public CancelableDialog(string title, bool stopButton) : base(title)
		{
			InitializeComponent();

			Canceled = false;
			ButtonStop.Enabled = ButtonStop.Visible = stopButton;
		}

		#endregion

		#region Delegates

		private delegate void SetTextDelegate(string s);

		private delegate void SimpleDelegate();

		#endregion

		#region Public Properties

		public Exception Error { get; protected set; }

		public int JobsCompleted => jobsCompleted;

		public int JobsTotal => TotalJobs;

		#endregion

		#region Properties

		protected bool Canceled { get; private set; }

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

		protected virtual void CancelableDialog_Load(object sender, EventArgs e)
		{
			// TODO: Error
			Task.Run(() => OnStart());

			UpdateText();
		}

		protected new void Close()
		{
			if (InvokeRequired)
			{
				Invoke(new SimpleDelegate(Close));
			}
			else
			{
				base.Close();
			}
		}

		protected void DisableButtons()
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

		protected virtual void OnFinish() { }

		protected void OnJobCompletion()
		{
			lock (SyncRoot)
			{
				jobsCompleted++;
			}

			UpdateText();
		}

		protected virtual void OnStart() { }

		protected void SetText(string text)
		{
			if (InvokeRequired)
			{
				Invoke(new SetTextDelegate(SetText), text);
			}
			else
			{
				lblText.Text = text;
			}
		}

		protected virtual void UpdateText() { }

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			Stopped = true;
			Canceled = true;

			DisableButtons();
			Close();
		}

		private void ButtonStop_Click(object sender, EventArgs e)
		{
			Stopped = true;

			DisableButtons();
			Close();
		}

		private void CancelableDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			lock (SyncRoot)
			{
				Stopped = true;
			}

			DisableButtons();
			OnFinish();

			if (jobsCompleted >= TotalJobs)
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
