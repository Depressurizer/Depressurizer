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
using DepressurizerCore.Models;

namespace Depressurizer.Forms
{
    public sealed class ScrapeDialog : CancelableDialog
    {
        #region Fields

        private readonly List<int> _jobs;

        private readonly List<DatabaseEntry> _result = new List<DatabaseEntry>();

        private DateTime _startTime;

        #endregion

        #region Constructors and Destructors

        public ScrapeDialog(List<int> jobs) : base("Scraping ...", true)
        {
            _jobs = jobs;
            base.TotalJobs = jobs.Count;
        }

        #endregion

        #region Public Properties

        public int CompletedJobs => base.CompletedJobs;

        public int TotalJobs => base.TotalJobs;

        #endregion

        #region Methods

        protected override void OnFinish()
        {
            if (Canceled)
            {
                return;
            }

            foreach (DatabaseEntry entry in _result)
            {
                if (Database.Instance.Contains(entry.Id))
                {
                    Database.Instance.Apps[entry.Id].MergeIn(entry);
                }
                else
                {
                    Database.Instance.Apps.TryAdd(entry.Id, entry);
                }
            }
        }

        protected override void OnStart()
        {
            _startTime = DateTime.UtcNow;
            Parallel.ForEach(_jobs, RunJob);
            CloseDialog();
        }

        protected override void UpdateText()
        {
            TimeSpan remainingTime = TimeSpan.Zero;
            if (CompletedJobs > 0)
            {
                double timeElapsed = (DateTime.UtcNow - _startTime).TotalMilliseconds;
                double timePerJob = timeElapsed / CompletedJobs;
                double timeRemaining = timePerJob * (TotalJobs - CompletedJobs);
                remainingTime = TimeSpan.FromMilliseconds(timeRemaining);
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Scraping {CompletedJobs} / {TotalJobs} completed.");
            stringBuilder.Append("Time left: ");

            if (remainingTime.TotalSeconds <= 60)
            {
                stringBuilder.Append($"{remainingTime.Seconds}s");
            }
            else
            {
                stringBuilder.Append(remainingTime.Hours > 0 ? $"{remainingTime.TotalHours}h" : $"{remainingTime.Minutes}:{remainingTime.Seconds}m");
            }

            SetText(stringBuilder.ToString());
        }

        private void RunJob(int appId)
        {
            if (Stopped)
            {
                return;
            }

            DatabaseEntry newApp = new DatabaseEntry(appId);
            newApp.ScrapeStore();

            if (Stopped)
            {
                return;
            }

            _result.Add(newApp);
            OnJobCompletion();
        }

        #endregion
    }
}