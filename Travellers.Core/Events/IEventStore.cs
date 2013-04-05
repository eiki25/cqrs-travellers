using System;
using System.Collections.Generic;
using Travellers.Core.Entities;

namespace Travellers.Core.Events
{
	public interface IEventStore
	{
		void PersistEvents(IAggregateRoot aggregate);
		IEventStream LoadEvents(Guid aggregateId);
		IEnumerable<IEvent> LoadAllEvents();
	}
}