#region License

//     This file (Program.cs) is part of Depressurizer.
//     Copyright (C) 2017  Martijn Vegter
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
using System.Windows.Forms;
using Depressurizer.Helpers;
using Newtonsoft.Json;
using Rallion;

namespace Depressurizer
{
    internal static class Program
    {
        public static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Auto
        };

        public static GameDB GameDB;

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Logger.Instance.Info("Started Depressurizer, current loglevel is " + Logger.Level);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += OnApplicationExit;
            FatalError.InitializeHandler();
            Settings.Instance.Load();

            Logger.Instance.Info("Loading main form.");
            Application.Run(new FormMain());
        }

        private static void OnApplicationExit(object sender, EventArgs eventArgs)
        {
            Logger.Instance.Info("Shutting down Depressurizer");

            Settings.Instance.Save();
            Logger.Instance.Dispose();
        }
    }
}