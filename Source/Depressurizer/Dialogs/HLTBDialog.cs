#region LICENSE

//     This file (HLTBDialog.cs) is part of Depressurizer.
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

using Depressurizer.Properties;
using DepressurizerCore;

namespace Depressurizer.Dialogs
{
	public sealed class HLTBDialog : CancelableDialog
	{
		#region Constructors and Destructors

		public HLTBDialog() : base(Resources.HLTBDialog_Title, false)
		{
			SetText(Resources.HLTBDialog_Status);
			Updated = 0;
		}

		#endregion

		#region Public Properties

		public int Updated { get; private set; }

		#endregion

		#region Methods

		protected override void OnFinish()
		{
			if (Canceled)
			{
				return;
			}

			if (Error != null)
			{
				return;
			}

			OnJobCompletion();
		}

		protected override void OnStart()
		{
			Updated = Database.Instance.UpdateFromHLTB(Settings.Instance.IncludeImputedTimes);
			Close();
		}

		#endregion
	}
}
