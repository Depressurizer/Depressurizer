#region License

//     This file (SteamTest.cs) is part of Depressurizer.
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
using Depressurizer.Core.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace Depressurizer.Test.Helpers
{
	[TestFixture]
	public class SteamTest
	{
		#region Public Methods and Operators

		[Test]
		public void GetStoreLanguage_Arabic()
		{
			// Arrange
			const string expected = "arabic";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Arabic);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Bulgarian()
		{
			// Arrange
			const string expected = "bulgarian";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Bulgarian);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Chinese_Simplified()
		{
			// Arrange
			const string expected = "schinese";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.ChineseSimplified);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Chinese_Traditional()
		{
			// Arrange
			const string expected = "tchinese";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.ChineseTraditional);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Czech()
		{
			// Arrange
			const string expected = "czech";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Czech);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Danish()
		{
			// Arrange
			const string expected = "danish";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Danish);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Dutch()
		{
			// Arrange
			const string expected = "dutch";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Dutch);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_English()
		{
			// Arrange
			const string expected = "english";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.English);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Finnish()
		{
			// Arrange
			const string expected = "finnish";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Finnish);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_French()
		{
			// Arrange
			const string expected = "french";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.French);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_German()
		{
			// Arrange
			const string expected = "german";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.German);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Greek()
		{
			// Arrange
			const string expected = "greek";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Greek);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Hungarian()
		{
			// Arrange
			const string expected = "hungarian";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Hungarian);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Italian()
		{
			// Arrange
			const string expected = "italian";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Italian);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Japanese()
		{
			// Arrange
			const string expected = "japanese";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Japanese);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Korean()
		{
			// Arrange
			const string expected = "koreana";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Korean);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Norwegian()
		{
			// Arrange
			const string expected = "norwegian";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Norwegian);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Polish()
		{
			// Arrange
			const string expected = "polish";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Polish);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Portuguese()
		{
			// Arrange
			const string expected = "portuguese";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Portuguese);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Portuguese_Brazil()
		{
			// Arrange
			const string expected = "brazilian";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.PortugueseBrasil);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Romanian()
		{
			// Arrange
			const string expected = "romanian";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Romanian);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Russian()
		{
			// Arrange
			const string expected = "russian";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Russian);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Spanish()
		{
			// Arrange
			const string expected = "spanish";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Spanish);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Swedish()
		{
			// Arrange
			const string expected = "swedish";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Swedish);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Thai()
		{
			// Arrange
			const string expected = "thai";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Thai);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Turkish()
		{
			// Arrange
			const string expected = "turkish";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Turkish);

			// Assert
			actual.Should().Be(expected);
		}

		[Test]
		public void GetStoreLanguage_Ukrainian()
		{
			// Arrange
			const string expected = "ukrainian";

			// Act
			string actual = Steam.GetStoreLanguage(StoreLanguage.Ukrainian);

			// Assert
			actual.Should().Be(expected);
		}

		#endregion
	}
}