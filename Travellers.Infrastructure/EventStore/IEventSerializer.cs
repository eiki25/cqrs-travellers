using System.Collections.Generic;
using Travellers.Core.Events;

namespace Travellers.Infrastructure.EventStore
{
	public interface IEventSerializer
	{
		string Serialize(IEnumerable<IEvent> events);
		IEnumerable<IEvent> Deserialize(string data);
	}
}