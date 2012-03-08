using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DPLib;

namespace SteamScrape {
    class ScrapeProcDlg : CancelableDlg {
        Queue<int> jobs;
        GameDB games;

        System.DateTime start;

        public ScrapeProcDlg( GameDB games, Queue<int> jobs )
            : base( "Scraping game info" ) {
            this.jobs = jobs;
            this.games = games;

            totalJobs = jobs.Count;
        }

        protected override void UpdateForm_Load( object sender, EventArgs e ) {
            start = DateTime.Now;
            base.UpdateForm_Load( sender, e );
        }

        private GameDBEntry GetNextGame() {
            int gameId;
            lock( jobs ) {
                if( jobs.Count > 0 ) {
                    gameId = jobs.Dequeue();
                } else {
                    return null;
                }
            }
            GameDBEntry res = null;
            lock( games ) {
                res = games.Games[gameId];
            }
            return res;
        }

        protected override void RunProcess() {
            bool running = true;
            while( !Aborted && running ) {
                running = RunNextJob();
                if( !Aborted && running ) {
                    OnJobCompletion();
                }
            }
            if( !Aborted ) {
                OnThreadEnd();
            }
        }

        /// <summary>
        /// Runs the next job in the queue, in a thread-safe manner. Aborts ASAP if the form is closed.
        /// </summary>
        /// <returns>True if a job was run, false otherwise</returns>
        private bool RunNextJob() {
            if( Aborted ) return false;

            GameDBEntry game = GetNextGame();
            if( game == null ) {
                return false;
            }
            if( Aborted ) return false;

            string genre = null;
            AppType type = GameDBEntry.ScrapeStore( game.Id, out genre );

            // This lock is critical, as it makes sure that the abort check and the actual game update funtion essentially atomically with reference to form-closing.
            // If this isn't the case, the form could successfully close before this happens, but then it could still go through, and that's no good.
            lock( abortLock ) {
                if( !Aborted ) {
                    game.Type = type;
                    if( type == AppType.Game || type == AppType.DLC || type == AppType.IdRedirect ) {
                        game.Genre = genre;
                    }
                    return true;
                } else {
                    return false;
                }
            }
        }

        protected override void UpdateText() {
            double msElapsed = ( DateTime.Now - start ).TotalMilliseconds;
            double msPerItem = msElapsed / (double)jobsCompleted;
            double msRemaining = msPerItem * ( totalJobs - jobsCompleted );
            TimeSpan timeRemaining = TimeSpan.FromMilliseconds( msRemaining );

            StringBuilder sb = new StringBuilder();
            sb.Append( string.Format( "Updating...{0}/{1} complete.", jobsCompleted, totalJobs ) );

            sb.Append( "\nTime Remaining: " );
            if( timeRemaining.TotalMinutes < 1.0 ) {
                sb.Append( "< 1 minute" );
            } else {
                double hours = timeRemaining.TotalHours;
                if( hours >= 1.0 ) {
                    sb.Append( string.Format( "{0:F0}h", hours ) );
                }
                sb.Append( string.Format( "{0:D2}m", timeRemaining.Minutes ) );
            }
            SetText( sb.ToString() );
        }
    }
}
