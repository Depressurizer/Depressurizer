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
using System.Threading;
using System.Windows.Forms;
using Depressurizer.Core;
using Depressurizer.Core.Helpers;
using Depressurizer.Dialogs;

namespace Depressurizer
{
	internal static class Program
	{
		#region Properties

		private static Logger Logger => Logger.Instance;

		private static Settings Settings => Settings.Instance;

		#endregion

		#region Methods

		/// <summary>
		///     The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.ApplicationExit += OnApplicationExit;

			FatalErrorDialog.InitializeHandler();

			Settings.SetThread(Thread.CurrentThread);
			Settings.Load();

			if (string.IsNullOrWhiteSpace(Settings.SteamPath))
			{
				using (SteamPathDialog dialog = new SteamPathDialog())
				{
					dialog.ShowDialog();

					Settings.SteamPath = dialog.Path;
					Settings.Save();
				}
			}

			Application.Run(new FormMain());
		}

		private static void OnApplicationExit(object sender, EventArgs e)
		{
			Settings.Save();
			Logger.Dispose();
		}

		#endregion
	}
}
