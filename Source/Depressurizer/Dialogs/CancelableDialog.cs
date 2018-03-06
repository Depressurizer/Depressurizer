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
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Depressurizer.Dialogs
{
	public partial class CancelableDialog : DepressurizerForm
	{
		#region Fields

		private bool _canceled;
		private bool _stopped;

		#endregion

		#region Constructors and Destructors

		public CancelableDialog(string formTitle, bool showStopButton)
		{
			InitializeComponent();

			Text = string.Format(CultureInfo.InvariantCulture, "Depressurizer - {0}", formTitle);

			ButtonStop.Enabled = ButtonStop.Visible = showStopButton;
		}

		#endregion

		#region Delegates

		private delegate void SetTextDelegate(string text);

		private delegate void SimpleDelegate();

		#endregion

		#region Public Properties

		public int CompletedJobs { get; protected set; }

		public Exception Error { get; protected set; }

		public sealed override string Text
		{
			get => base.Text;
			set => base.Text = value;
		}

		public int TotalJobs { get; protected set; }

		#endregion

		#region Properties

		protected static object SyncRoot => new object();

		protected bool Canceled
		{
			get
			{
				lock (SyncRoot)
				{
					return _canceled;
				}
			}

			set
			{
				lock (SyncRoot)
				{
					_canceled = value;
				}
			}
		}

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
			try
			{
				Task.Run(() => OnStart());
			}
			catch (Exception exception)
			{
				Error = exception;
			}
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
				CompletedJobs++;
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
				LabelText.Text = text;
			}
		}

		protected virtual void UpdateText() { }

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			lock (SyncRoot)
			{
				_stopped = true;
				_canceled = true;
			}

			Close();
		}

		private void ButtonStop_Click(object sender, EventArgs e)
		{
			lock (SyncRoot)
			{
				_stopped = true;
			}

			Close();
		}

		private void CancelableDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			lock (SyncRoot)
			{
				_stopped = true;
			}

			DisableButtons();
			OnFinish();

			if (CompletedJobs >= TotalJobs)
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
