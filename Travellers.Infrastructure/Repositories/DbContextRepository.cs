using System;
using System.Collections.Generic;
using System.Data.Entity;
using Travellers.Core.Repositories;

namespace Travellers.Infrastructure.Repositories
{
	public abstract class DbContextRepository<T> : IRepository<T> where T : class
	{
		private readonly DbContext _context;

		protected DbContextRepository(DbContext context)
		{
			_context = context;
		}

		protected DbContext Context
		{
			get { return _context; }
		}

		public void Add(T item)
		{
			_context.Set<T>().Add(item);
		}

		public T ById(Guid id)
		{
			return _context.Set<T>().Find(id);
		}

		public IEnumerable<T> All()
		{
			return _context.Set<T>();
		}

		public abstract IEnumerable<T> Search(string query);
	}
}