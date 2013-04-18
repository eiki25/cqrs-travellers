using System;
using System.Data.Entity;
using Travellers.Core.Repositories;

namespace Travellers.Infrastructure.Repositories
{
	public class DbContextRepository<T> : IRepository<T> where T : class
	{
		private readonly DbContext _context;

		public DbContextRepository(DbContext context)
		{
			_context = context;
		}

		public void Add(T item)
		{
			_context.Set<T>().Add(item);
		}

		public T ById(Guid id)
		{
			return _context.Set<T>().Find(id);
		}
	}
}