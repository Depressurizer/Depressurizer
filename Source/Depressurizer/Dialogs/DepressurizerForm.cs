#region LICENSE

//     This file (DepressurizerForm.cs) is part of Depressurizer.
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

using MaterialSkin;
using MaterialSkin.Controls;

namespace Depressurizer.Dialogs
{
	public partial class DepressurizerForm : MaterialForm
	{
		#region Constructors and Destructors

		public DepressurizerForm()
		{
			InitializeComponent();
			InitializeMaterialSkin();
		}

		#endregion

		#region Properties

		private static MaterialSkinManager MaterialSkinManager => MaterialSkinManager.Instance;

		#endregion

		#region Methods

		private void InitializeMaterialSkin()
		{
			MaterialSkinManager.AddFormToManage(this);
			MaterialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
			MaterialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
		}

		#endregion
	}
}
