using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Simple.Data;
using Travellers.Core.Entities;
using Travellers.Core.Events;

namespace Travellers.Infrastructure.EventStore
{
	public class SqlEventStore : IEventStore
	{
		private readonly IEventSerializer _serializer;
		private const int UniqueKeyViolation = 2627;

		public SqlEventStore(IEventSerializer serializer)
		{
			_serializer = serializer;
		}

		public void PersistEvents(IAggregateRoot aggregate)
		{
			try
			{
				var events = aggregate.GetUncommittedEvents();

				if (!events.Any()) return;

				Database.Open().Events.Insert(
					Id: Guid.NewGuid(),
					AggregateId: aggregate.Id,
					Version: aggregate.Version + 1,
					Events: _serializer.Serialize(events),
					Created: DateTime.UtcNow
					);
			}
			catch (SqlException ex)
			{
				if (ex.Number == UniqueKeyViolation)
				{
					throw new ConcurrencyException();
				}

				throw;
			}
		}

		public IEventStream LoadEvents(Guid aggregateId)
		{
			int version = 0;
			var events = new List<IEvent>();

			var eventHistory = Database.Open().Events
				.FindAllByAggregateId(aggregateId)
				.OrderByVersion();

			foreach (var evt in eventHistory)
			{
				version = evt.Version;
				events.AddRange(_serializer.Deserialize(evt.Events));
			}

			return new EventStream { AggregateId = aggregateId, Version = version, Events = events };
		}

		public IEnumerable<IEvent> LoadAllEvents()
		{
			foreach (var evt in Database.Open().Events.All().OrderByCreated())
			{
				foreach (var @event in _serializer.Deserialize(evt.Events))
				{
					yield return @event;
				}
			}
		}

		private class EventStream : IEventStream
		{
			public IEnumerable<IEvent> Events { get; set; }
			public int Version { get; set; }
			public Guid AggregateId { get; set; }
		}
	}
}
