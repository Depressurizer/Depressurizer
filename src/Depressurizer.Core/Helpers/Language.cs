#region License

//     This file (Language.cs) is part of Depressurizer.
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
using System.Globalization;
using System.Linq;
using Depressurizer.Core.Enums;

namespace Depressurizer.Core.Helpers
{
	/// <summary>
	///     Helper functions for language related actions.
	/// </summary>
	public static class Language
	{
		#region Public Methods and Operators

		/// <summary>
		///     Returns the CultureInfo from the specified InterfaceLanguage.
		/// </summary>
		/// <param name="language">
		///     Language to get the matching culture from.
		/// </param>
		/// <returns>
		///     CultureInfo of the specified InterfaceLanguage.
		/// </returns>
		public static CultureInfo GetCultureInfo(InterfaceLanguage language)
		{
			CultureInfo culture;

			switch (language)
			{
				case InterfaceLanguage.Dutch:
					culture = new CultureInfo("nl");

					break;
				case InterfaceLanguage.English:
					culture = new CultureInfo("en");

					break;
				case InterfaceLanguage.Russian:
					culture = new CultureInfo("ru");

					break;
				case InterfaceLanguage.Spanish:
					culture = new CultureInfo("es");

					break;
				case InterfaceLanguage.Ukranian:
					culture = new CultureInfo("uk");

					break;
				default:

					throw new ArgumentOutOfRangeException(nameof(language), language, null);
			}

			return culture;
		}

		/// <summary>
		///     Returns the CultureInfo from the specified StoreLanguage.
		/// </summary>
		/// <param name="language">
		///     Language to get the matching culture from.
		/// </param>
		/// <returns>
		///     CultureInfo of the specified StoreLanguage.
		/// </returns>
		/// <remarks>
		///     Based on the "Web API language code" column:
		///     https://partner.steamgames.com/doc/store/localization
		/// </remarks>
		public static CultureInfo GetCultureInfo(StoreLanguage language)
		{
			CultureInfo cultureInfo;

			switch (language)
			{
				case StoreLanguage.ChineseSimplified:
					cultureInfo = new CultureInfo("zh-CN");

					break;
				case StoreLanguage.ChineseTraditional:
					cultureInfo = new CultureInfo("zh-TW");

					break;

				case StoreLanguage.PortugueseBrasil:
					cultureInfo = new CultureInfo("pt-BR");

					break;

				default:
					cultureInfo = CultureInfo.GetCultures(CultureTypes.AllCultures).First(c => c.EnglishName == language.ToString());

					break;
			}

			return cultureInfo;
		}

		#endregion
	}
}