#region License

//     This file (Steam.cs) is part of Depressurizer.
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

using Depressurizer.Core.Enums;

namespace Depressurizer.Core.Helpers
{
	/// <summary>
	///     Helper functions for Steam related actions
	/// </summary>
	public static class Steam
	{
		#region Public Methods and Operators

		/// <summary>
		///     Converts a StoreLanguage to a Steam accepted API language code
		/// </summary>
		/// <param name="language">Language to convert</param>
		/// <returns>Steam Store API language code</returns>
		public static string GetStoreLanguage(StoreLanguage language)
		{
			string storeLanguage;

			switch (language)
			{
				case StoreLanguage.ChineseSimplified:
					storeLanguage = "schinese";

					break;

				case StoreLanguage.ChineseTraditional:
					storeLanguage = "tchinese";

					break;

				case StoreLanguage.Korean:
					storeLanguage = "koreana";

					break;

				case StoreLanguage.PortugueseBrasil:
					storeLanguage = "brazilian";

					break;

				default:
					storeLanguage = language.ToString();

					break;
			}

			return storeLanguage.ToLower();
		}

		#endregion
	}
}