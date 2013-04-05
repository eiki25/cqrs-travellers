using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Travellers.Core.Entities;
using Travellers.Core.Events;

namespace Travellers.Infrastructure.EventStore
{
	public class EventStoreContext : IEventStoreContext
	{
		internal const string AGGREGATE_KEY = "EventStoreContext.Aggregates";

		private readonly IContext _context;
		private readonly IEventPublisher _eventPublisher;
		private readonly IEventStore _eventStore;

		public EventStoreContext(IContext context, IEventPublisher eventPublisher, IEventStore eventStore)
		{
			_context = context;
			_eventPublisher = eventPublisher;
			_eventStore = eventStore;
		}

		public void SaveChanges()
		{
			var aggregates = _context[AGGREGATE_KEY] as HashSet<IAggregateRoot>;

			if (aggregates == null || !aggregates.Any())
			{
				return;
			}

			using (var tx = new TransactionScope(TransactionScopeOption.Required))
			{
				foreach (var aggregate in aggregates)
				{
					_eventStore.PersistEvents(aggregate);
				}

				foreach (var evt in aggregates.SelectMany(ar => ar.GetUncommittedEvents()))
				{
					_eventPublisher.Publish(evt);
				}

				tx.Complete();
			}

			aggregates.ToList().ForEach(x => x.ClearUncommittedEvents());

			_context[AGGREGATE_KEY] = null;
		}
	}
}