using System.Data.Entity;
using System.Transactions;
using Raven.Client;

namespace Travellers.Infrastructure.Persistence
{
	public class PersistenceManager : IPersistenceManager
	{
		private readonly DbContext _context;
		private readonly IDocumentSession _session;

		public PersistenceManager(DbContext context, IDocumentSession session)
		{
			_context = context;
			_session = session;
		}

		public void SaveChanges()
		{
			using (var tx = new TransactionScope(TransactionScopeOption.Required))
			{
				_context.SaveChanges();
				_session.SaveChanges();

				tx.Complete();
			}
		}
	}
}