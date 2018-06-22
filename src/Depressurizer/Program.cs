#region License

//     This file (Program.cs) is part of Depressurizer.
//     Copyright (C) 2011  Steve Labbe
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
using System.Windows.Forms;
using Depressurizer.Dialogs;
using Depressurizer.Helpers;

namespace Depressurizer
{
	internal static class Program
	{
		#region Static Fields

		public static Database Database;

		#endregion

		#region Properties

		private static Logger Logger => Logger.Instance;

		#endregion

		#region Methods

		/// <summary>
		///     The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.ApplicationExit += OnApplicationExit;

			FatalErrorDialog.InitializeHandler();

			Settings.Instance.Load();

			Logger.Info(GlobalStrings.Program_ProgramInitialized);
			Application.Run(new FormMain());
		}

		private static void OnApplicationExit(object sender, EventArgs e)
		{
			Settings.Instance.Save();
			Logger.Instance.Dispose();
		}

		#endregion
	}
}