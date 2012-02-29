using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SteamScrape {
    public partial class UpdateForm : Form {

        const int THREADS = 3;
        int runningThreads;

        Queue<int> jobs;

        bool abort = false;

        public UpdateForm( Queue<int> jobs ) {
            InitializeComponent();
            this.jobs = jobs;
        }
    }
}
