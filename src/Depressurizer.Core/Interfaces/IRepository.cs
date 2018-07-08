#region License

//     This file (IRepository.cs) is part of Depressurizer.
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

using System.Linq;

namespace Depressurizer.Core.Interfaces
{
	public abstract class EntityBase
	{
		#region Public Properties

		public int Id { get; protected set; }

		#endregion
	}

	public interface IRepository<T> where T : EntityBase
	{
		#region Public Methods and Operators

		void Add(T entity);

		bool Contains(int id);

		bool Contains(int id, out T entity);

		IQueryable<T> GetAll();

		void Remove(T entity);

		#endregion
	}
}
