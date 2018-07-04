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

		bool Contains(int id);

		bool Contains(int id, out T entity);

		#endregion
	}
}
