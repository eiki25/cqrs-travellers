using System;

namespace Travellers.Core.Repositories
{
	public interface IRepository<T>
	{
		void Add(T item);
		T ById(Guid id);
	}
}