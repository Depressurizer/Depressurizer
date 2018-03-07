#region LICENSE

//     This file (CloseDialog.cs) is part of Depressurizer.
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
using System.Drawing;
using System.Globalization;

namespace Depressurizer.Dialogs
{
	public partial class CloseDialog : DepressurizerForm
	{
		#region Constructors and Destructors

		public CloseDialog(string message, string dialogTitle, Image image, bool showCancelButton, bool exportToSteam)
		{
			InitializeComponent();

			Text = string.Format(CultureInfo.InvariantCulture, "Depressurizer - {0}", dialogTitle);

			MessageLabel.Text = message;
			MessageIcon.Image = image;
			ButtonCancel.Enabled = ButtonCancel.Visible = showCancelButton;
			Export = CheckSaveToSteam.Checked = exportToSteam;
		}

		#endregion

		#region Public Properties

		public bool Export { get; private set; }

		public sealed override string Text
		{
			get => base.Text;
			set => base.Text = value;
		}

		#endregion

		#region Methods

		private void CheckSaveToSteam_CheckedChanged(object sender, EventArgs e)
		{
			Export = CheckSaveToSteam.Checked;
		}

		#endregion
	}
}
