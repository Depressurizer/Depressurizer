#region License

//     This file (ScrapeDialog.cs) is part of Depressurizer.
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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Depressurizer.Models;
using MaterialSkin;
using MaterialSkin.Controls;

namespace Depressurizer.Dialogs
{
	internal partial class ScrapeDialog : MaterialForm
	{
		#region Static Fields

		private static readonly object SyncRoot = new object();

		#endregion

		#region Fields

		private readonly List<int> _jobs;

		private readonly List<DatabaseEntry> _results = new List<DatabaseEntry>();

		private bool _canceled = false;

		private DateTime _start;

		private bool _stopped = false;

		private string _timeLeft;

		#endregion

		#region Constructors and Destructors

		public ScrapeDialog(IEnumerable<int> appids)
		{
			InitializeComponent();
			MaterialSkinManager.AddFormToManage(this);

			_jobs = appids.Distinct().Where(i => i > 0).ToList();
			TotalJobs = _jobs.Count;
		}

		#endregion

		#region Delegates

		private delegate void SimpleDelegate();

		private delegate void TextUpdateDelegate(string text);

		#endregion

		#region Public Properties

		public int CompletedJobs { get; private set; }

		public Exception Error { get; private set; }

		public int TotalJobs { get; }

		#endregion

		#region Properties

		private static Database Database => Database.Instance;

		private static MaterialSkinManager MaterialSkinManager => MaterialSkinManager.Instance;

		#endregion

		#region Methods

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

		private new void Close()
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

		private void Finish()
		{
			lock (SyncRoot)
			{
				_stopped = true;
				if (_canceled)
				{
					return;
				}
			}

			SetText("Applying data...");

			foreach (DatabaseEntry entry in _results)
			{
				if (Database.Contains(entry.Id, out DatabaseEntry databaseEntry))
				{
					databaseEntry.MergeIn(entry);
				}
				else
				{
					Database.Add(entry);
				}
			}
		}

		private void OnJobCompletion()
		{
			lock (SyncRoot)
			{
				CompletedJobs++;
			}

			UpdateText();
		}

		private void RunJob(int appId)
		{
			lock (SyncRoot)
			{
				if (_stopped)
				{
					return;
				}
			}

			DatabaseEntry entry = new DatabaseEntry(appId);
			entry.ScrapeStore();

			if (entry.LastStoreScrape == 0)
			{
				entry.ScrapeTrueSteamAchievements();
			}

			lock (SyncRoot)
			{
				if (_stopped)
				{
					return;
				}

				_results.Add(entry);
				OnJobCompletion();
			}
		}

		private void ScrapeDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			lock (SyncRoot)
			{
				_stopped = true;
			}

			DisableButtons();
			Finish();

			if (CompletedJobs >= TotalJobs)
			{
				DialogResult = DialogResult.OK;
			}
			else if (_canceled)
			{
				DialogResult = DialogResult.Cancel;
			}
			else
			{
				DialogResult = DialogResult.Abort;
			}
		}

		private void ScrapeDialog_Load(object sender, EventArgs e)
		{
			_start = DateTime.UtcNow;
			Start();
		}

		private void SetText(string text)
		{
			if (InvokeRequired)
			{
				Invoke(new TextUpdateDelegate(SetText), text);
			}
			else
			{
				LabelStatus.Text = text;
			}
		}

		private void Start()
		{
			try
			{
				Task.Run(() =>
				{
					Parallel.ForEach(_jobs, RunJob);
					Close();
				});
			}
			catch (Exception e)
			{
				lock (SyncRoot)
				{
					_stopped = true;
					Error = e;
				}

				if (IsHandleCreated)
				{
					Invoke(new SimpleDelegate(Finish));
					Invoke(new SimpleDelegate(Close));
				}
			}
		}

		private void UpdateText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format(CultureInfo.CurrentUICulture, "Scraped {0}/{1}", CompletedJobs, TotalJobs));

			string timeLeft = string.Format(CultureInfo.CurrentUICulture, "{0}: ", "Time left") + "{0}";
			if (CompletedJobs > 0)
			{
				TimeSpan timeRemaining = TimeSpan.FromTicks((DateTime.UtcNow.Subtract(_start).Ticks * (TotalJobs - (CompletedJobs + 1))) / (CompletedJobs + 1));
				if (timeRemaining.TotalHours >= 1)
				{
					_timeLeft = string.Format(CultureInfo.InvariantCulture, timeLeft, timeRemaining.Hours + ":" + (timeRemaining.Minutes < 10 ? "0" + timeRemaining.Minutes : timeRemaining.Minutes.ToString()) + ":" + (timeRemaining.Seconds < 10 ? "0" + timeRemaining.Seconds : timeRemaining.Seconds.ToString()));
				}
				else if (timeRemaining.TotalSeconds >= 60)
				{
					_timeLeft = string.Format(CultureInfo.InvariantCulture, timeLeft, (timeRemaining.Minutes < 10 ? "0" + timeRemaining.Minutes : timeRemaining.Minutes.ToString()) + ":" + (timeRemaining.Seconds < 10 ? "0" + timeRemaining.Seconds : timeRemaining.Seconds.ToString()));
				}
				else
				{
					_timeLeft = string.Format(CultureInfo.InvariantCulture, timeLeft, timeRemaining.Seconds + "s");
				}
			}
			else
			{
				_timeLeft = string.Format(CultureInfo.CurrentUICulture, timeLeft, "Unknown");
			}

			stringBuilder.AppendLine(_timeLeft);
			SetText(stringBuilder.ToString());
		}

		#endregion
	}
}
