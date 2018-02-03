#region LICENSE

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
using System.Text;
using System.Threading.Tasks;

namespace Depressurizer.Dialogs
{
	internal class ScrapeDialog : CancelableDialog
	{
		#region Fields

		private readonly List<int> _jobs;

		private readonly List<GameDBEntry> _results = new List<GameDBEntry>();

		private DateTime _start;

		private string _timeLeft;

		#endregion

		#region Constructors and Destructors

		public ScrapeDialog(List<int> jobs) : base("Scraping App info ...", true)
		{
			_jobs = jobs;
			TotalJobs = jobs.Count;
		}

		#endregion

		#region Methods

		protected override void CancelableDialog_Load(object sender, EventArgs e)
		{
			_start = DateTime.UtcNow;
			base.CancelableDialog_Load(sender, e);
		}

		protected override void OnFinish()
		{
			if (Canceled)
			{
				return;
			}

			SetText("Applying Data ...");

			lock (SyncRoot)
			{
				foreach (GameDBEntry g in _results)
				{
					if (Program.GameDB.Contains(g.Id))
					{
						Program.GameDB.Games[g.Id].MergeIn(g);
					}
					else
					{
						Program.GameDB.Games.Add(g.Id, g);
					}
				}
			}
		}

		protected override void OnStart()
		{
			Parallel.ForEach(_jobs, RunJob);
			Close();
		}

		protected override void UpdateText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine($"Scraping Apps: {jobsCompleted} / {TotalJobs} Completed");

			const string timeLeft = "Time left: {0}";
			if (jobsCompleted > 0)
			{
				if ((jobsCompleted > (TotalJobs / 4)) || ((jobsCompleted % 5) == 0))
				{
					TimeSpan timeRemaining = TimeSpan.FromTicks((DateTime.UtcNow.Subtract(_start).Ticks * (TotalJobs - (jobsCompleted + 1))) / (jobsCompleted + 1));
					if (timeRemaining.TotalSeconds >= 60)
					{
						_timeLeft = string.Format(timeLeft, timeRemaining.Minutes + ":" + (timeRemaining.Seconds < 10 ? "0" + timeRemaining.Seconds : timeRemaining.Seconds.ToString()));
					}
					else
					{
						_timeLeft = string.Format(timeLeft, timeRemaining.Seconds + "s");
					}
				}
			}
			else
			{
				_timeLeft = string.Format(timeLeft, "Unknown");
			}

			stringBuilder.AppendLine(_timeLeft);

			SetText(stringBuilder.ToString());
		}

		private void RunJob(int appId)
		{
			if (Stopped)
			{
				return;
			}

			GameDBEntry newGame = new GameDBEntry {Id = appId};
			newGame.ScrapeStore();

			lock (SyncRoot)
			{
				if (Stopped)
				{
					return;
				}

				_results.Add(newGame);
				OnJobCompletion();
			}
		}

		#endregion
	}
}