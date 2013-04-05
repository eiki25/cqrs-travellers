using System;
using System.Collections.Generic;
using Travellers.Core.Events;

namespace Travellers.Core.Entities
{
	public abstract class AggregateRoot : IAggregateRoot
	{
		private readonly List<IEvent> _uncommittedEvents = new List<IEvent>();

		protected AggregateRoot(Guid id)
		{
			Id = id;
			Version = 0;
		}

		protected AggregateRoot(IEventStream events) : this(events.AggregateId)
		{
			Version = events.Version;

			foreach (var evt in events.Events)
			{
				Apply(evt);
			}
		}

		public Guid Id { get; protected set; }
		public int Version { get; protected set; }

		public IEnumerable<IEvent> GetUncommittedEvents()
		{
			return _uncommittedEvents;
		}

		public void ClearUncommittedEvents()
		{
			_uncommittedEvents.Clear();
		}

		protected void AddEvent<T>(T evt) where T : IEvent
		{
			Apply(evt);

			_uncommittedEvents.Add(evt);
		}

		protected abstract void Apply(IEvent evt);
	}
}