using System;
using System.Collections.Generic;

namespace Travellers.Core.Repositories
{
	public interface IRepository<T>
	{
		void Add(T item);
		T ById(Guid id);
		IEnumerable<T> All();
		IEnumerable<T> Search(string query);
	}
}