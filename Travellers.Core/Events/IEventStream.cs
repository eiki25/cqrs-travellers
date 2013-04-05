using System;
using System.Collections.Generic;

namespace Travellers.Core.Events
{
	public interface IEventStream
	{
		Guid AggregateId { get; }
		int Version { get; }
		IEnumerable<IEvent> Events { get; }
	}
}