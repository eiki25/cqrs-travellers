using System.Data.Entity;

namespace Travellers.Infrastructure.Persistence
{
	public class PersistenceManager : IPersistenceManager
	{
		private readonly DbContext _context;

		public PersistenceManager(DbContext context)
		{
			_context = context;
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}
	}
}