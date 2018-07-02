#region License

//     This file (LanguageTest.cs) is part of Depressurizer.
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

using System.Globalization;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace Depressurizer.Tests.Helpers
{
	[TestFixture]
	public class LanguageTest
	{
		#region Public Methods and Operators

		[Test]
		public void GetCultureInfo_InterfaceLanguage_Dutch()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("nl");

			// Act
			CultureInfo actual = Language.GetCultureInfo(InterfaceLanguage.Dutch);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_InterfaceLanguage_English()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("en");

			// Act
			CultureInfo actual = Language.GetCultureInfo(InterfaceLanguage.English);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_InterfaceLanguage_Russian()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("ru");

			// Act
			CultureInfo actual = Language.GetCultureInfo(InterfaceLanguage.Russian);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_InterfaceLanguage_Spanish()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("es");

			// Act
			CultureInfo actual = Language.GetCultureInfo(InterfaceLanguage.Spanish);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_InterfaceLanguage_Ukranian()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("uk");

			// Act
			CultureInfo actual = Language.GetCultureInfo(InterfaceLanguage.Ukranian);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Arabic()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("ar");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Arabic);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Bulgarian()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("bg");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Bulgarian);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Chinese_Simplified()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("zh-CN");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.ChineseSimplified);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Chinese_Traditional()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("zh-TW");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.ChineseTraditional);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Czech()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("cs");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Czech);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Danish()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("da");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Danish);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Dutch()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("nl");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Dutch);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_English()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("en");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.English);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Finnish()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("fi");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Finnish);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_French()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("fr");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.French);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_German()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("de");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.German);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Greek()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("el");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Greek);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Hungarian()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("hu");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Hungarian);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Italian()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("it");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Italian);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Japanese()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("ja");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Japanese);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Korean()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("ko");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Korean);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Norwegian()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("no");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Norwegian);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Polish()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("pl");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Polish);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Portuguese()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("pt");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Portuguese);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Portuguese_Brazil()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("pt-BR");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.PortugueseBrasil);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Romanian()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("ro");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Romanian);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Russian()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("ru");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Russian);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Spanish()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("es");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Spanish);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Swedish()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("sv");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Swedish);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Thai()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("th");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Thai);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Turkish()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("tr");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Turkish);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetCultureInfo_StoreLanguage_Ukrainian()
		{
			// Arrange
			CultureInfo expected = new CultureInfo("uk");

			// Act
			CultureInfo actual = Language.GetCultureInfo(StoreLanguage.Ukrainian);

			// Assert
			actual.Should().Be(expected);
		}

		#endregion
	}
}
