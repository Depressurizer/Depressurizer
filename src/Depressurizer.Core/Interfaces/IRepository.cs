using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depressurizer.Core.Interfaces
{
	public abstract class EntityBase
	{
		public int Id { get; protected set; }
	}

	public interface IRepository<T> where T : EntityBase
	{

	}
}
