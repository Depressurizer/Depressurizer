#region License

//     This file (CategoryTest.cs) is part of Depressurizer.
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
using Depressurizer.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Depressurizer.Test.Models
{
	[TestFixture]
	public class CategoryTest
	{
		#region Public Methods and Operators

		[Test]
		public void CompareTo_Alphabetically()
		{
			// Arrange
			Category firstCategory = new Category("Action");
			Category secondCategory = new Category("Sports");

			// Act
			int firstCompareTo = firstCategory.CompareTo(secondCategory);
			int secondCompareTo = secondCategory.CompareTo(firstCategory);

			// Assert
			firstCompareTo.Should().BeLessThan(0);
			secondCompareTo.Should().BeGreaterThan(0);
		}

		[Test]
		public void CompareTo_CaseInsensitive()
		{
			// Arrange
			Category firstCategory = new Category("Action");
			Category secondCategory = new Category("action");

			// Act
			int firstCompareTo = firstCategory.CompareTo(secondCategory);
			int secondCompareTo = secondCategory.CompareTo(firstCategory);

			// Assert
			firstCompareTo.Should().Be(0);
			secondCompareTo.Should().Be(0);
		}

		[Test]
		public void CompareTo_InvalidObject()
		{
			// Arrange
			Category baseCategory = new Category("test");

			// Act
			try
			{
				// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
				baseCategory.CompareTo("invalid");
			}
			catch (Exception e)
			{
				// Assert
				e.Should().BeOfType<ArgumentException>();
				e.Message.Should().Be("Object is not a Category");
			}
		}

		[Test]
		public void CompareTo_Null()
		{
			// Arrange
			Category baseCategory = new Category("test");

			// Act
			int compareTo = baseCategory.CompareTo(null);

			// Assert
			compareTo.Should().Be(1);
		}

		[Test]
		public void ToString_Valid()
		{
			// Arrange
			const string expected = "Action";

			// Act
			string actual = new Category("Action").ToString();

			// Assert
			actual.Should().Be(expected);
		}

		#endregion
	}
}