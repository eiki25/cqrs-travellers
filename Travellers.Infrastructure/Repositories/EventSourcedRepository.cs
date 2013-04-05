using System;
using System.Collections.Generic;
using System.Reflection;
using Travellers.Core.Entities;
using Travellers.Core.Events;
using Travellers.Core.Repositories;
using Travellers.Infrastructure.EventStore;

namespace Travellers.Infrastructure.Repositories
{
	public class EventSourcedRepository<T> : IRepository<T> where T : AggregateRoot
	{
		private readonly IEventStore _eventStore;
		private readonly IContext _context;

		private readonly ConstructorInfo _constructor;

		public EventSourcedRepository(IEventStore eventStore, IContext context)
		{
			_eventStore = eventStore;
			_context = context;

			_constructor = typeof(T).GetConstructor(new[] { typeof(IEventStream) });
		}

		public T ById(Guid id)
		{
			var events = _eventStore.LoadEvents(id);
			var aggregate = (T)_constructor.Invoke(new object[] { events });

			AddToContext(aggregate);

			return aggregate;
		}

		public void Add(T toAdd)
		{
			AddToContext(toAdd);
		}

		private void AddToContext(T toAdd)
		{
			var aggregates = _context[EventStoreContext.AGGREGATE_KEY] as HashSet<IAggregateRoot>;

			if (aggregates == null)
			{
				aggregates = new HashSet<IAggregateRoot>();
				_context[EventStoreContext.AGGREGATE_KEY] = aggregates;
			}

			aggregates.Add(toAdd);
		}
	}
}