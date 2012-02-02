using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Rallion;

namespace Depressurizer {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            FatalError.InitializeHandler();

            DepSettings settings = DepSettings.Instance();
            settings.Load();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new FormMain() );
        }
    }
}
