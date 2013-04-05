using System;
using System.Collections.Generic;
using Travellers.Core.Events;

namespace Travellers.Core.Entities
{
	public interface IAggregateRoot
	{
		Guid Id { get; }
		int Version { get; }

		IEnumerable<IEvent> GetUncommittedEvents();
		void ClearUncommittedEvents();
	}
}