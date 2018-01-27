﻿/*
This file is part of Depressurizer.
Copyright 2011, 2012, 2013 Steve Labbe.

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
using System.Windows.Forms;
using Rallion;

namespace Depressurizer
{
    static class Program
    {
        public static AppLogger Logger;
        public static GameDB GameDB;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FatalError.InitializeHandler();

            Logger = new AppLogger();
            Logger.Level = LoggerLevel.None;
            Logger.DateFormat = "HH:mm:ss'.'ffffff";

            Logger.MaxFileSize = 2000000;
            Logger.MaxBackup = 1;
            Logger.FileNameTemplate = "Depressurizer.log";

            Settings.Instance.Load();

            Logger.Write(LoggerLevel.Info, GlobalStrings.Program_ProgramInitialized, Logger.Level);
            Logger.Write(LoggerLevel.Info, "Automatic mode not set, loading main form.");
            Application.Run(new FormMain());
            
            Settings.Instance.Save();

            Logger.Write(LoggerLevel.Info, GlobalStrings.Program_ProgramClosing);
            Logger.EndSession();
        }
    }
}