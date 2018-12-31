using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Depressurizer.Models;
using Rallion;

namespace Depressurizer
{
    internal class DbScrapeDlg : CancelableDlg
    {
        #region Fields

        private readonly Queue<int> _queue;

        private readonly List<DatabaseEntry> _results = new List<DatabaseEntry>();

        private DateTime _start;

        #endregion

        #region Constructors and Destructors

        public DbScrapeDlg(IEnumerable<int> appIds) : base(GlobalStrings.CDlgScrape_ScrapingGameInfo, true)
        {
            _queue = new Queue<int>(appIds.Distinct().Where(id => id > 0));
            totalJobs = _queue.Count;
        }

        #endregion

        #region Properties

        private static Database Database => Database.Instance;

        #endregion

        #region Methods

        protected override void Finish()
        {
            if (Canceled)
            {
                return;
            }

            SetText(GlobalStrings.CDlgScrape_ApplyingData);

            if (_results == null)
            {
                return;
            }

            foreach (DatabaseEntry g in _results)
            {
                if (Database.Contains(g.Id, out DatabaseEntry entry))
                {
                    entry.MergeIn(g);
                }
                else
                {
                    Database.Add(g);
                }
            }
        }

        protected override void RunProcess()
        {
            bool stillRunning = true;
            while (!Stopped && stillRunning)
            {
                stillRunning = RunNextJob();
            }

            OnThreadCompletion();
        }

        protected override void UpdateForm_Load(object sender, EventArgs e)
        {
            _start = DateTime.Now;
            base.UpdateForm_Load(sender, e);
        }

        protected override void UpdateText()
        {
            TimeSpan timeRemaining = TimeSpan.Zero;
            if (jobsCompleted > 0)
            {
                double msElapsed = (DateTime.Now - _start).TotalMilliseconds;
                double msPerItem = msElapsed / jobsCompleted;
                double msRemaining = msPerItem * (totalJobs - jobsCompleted);
                timeRemaining = TimeSpan.FromMilliseconds(msRemaining);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(GlobalStrings.CDlgDataScrape_UpdatingComplete, jobsCompleted, totalJobs));

            sb.Append(GlobalStrings.CDlgDataScrape_TimeRemaining);
            if (timeRemaining == TimeSpan.Zero)
            {
                sb.Append(GlobalStrings.CDlgScrape_Unknown);
            }
            else if (timeRemaining.TotalMinutes < 1.0)
            {
                sb.Append(GlobalStrings.CDlgScrape_1minute);
            }
            else
            {
                double hours = timeRemaining.TotalHours;
                if (hours >= 1.0)
                {
                    sb.Append($"{hours:F0}h");
                }

                sb.Append($"{timeRemaining.Minutes:D2}m");
            }

            SetText(sb.ToString());
        }

        private int GetNextGameId()
        {
            lock (_queue)
            {
                if (_queue.Count > 0)
                {
                    return _queue.Dequeue();
                }

                return 0;
            }
        }

        private bool RunNextJob()
        {
            int id = GetNextGameId();
            if (id == 0)
            {
                return false;
            }

            if (Stopped)
            {
                return false;
            }

            DatabaseEntry newGame = new DatabaseEntry(id);
            newGame.ScrapeStore();

            // This lock is critical, as it makes sure that the abort check and the actual game update funtion essentially atomically with reference to form-closing.
            // If this isn't the case, the form could successfully close before this happens, but then it could still go through, and that's no good.
            lock (abortLock)
            {
                if (Stopped)
                {
                    return false;
                }

                _results.Add(newGame);
                OnJobCompletion();
                return true;
            }
        }

        #endregion
    }
}
