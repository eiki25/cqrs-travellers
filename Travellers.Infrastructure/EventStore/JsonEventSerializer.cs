using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Travellers.Core.Events;

namespace Travellers.Infrastructure.EventStore
{
	public class JsonEventSerializer : IEventSerializer
	{
		private readonly JsonSerializerSettings _settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

		public string Serialize(IEnumerable<IEvent> events)
		{
			return JsonConvert.SerializeObject(events.ToArray(), _settings);
		}

		public IEnumerable<IEvent> Deserialize(string data)
		{
			return JsonConvert.DeserializeObject<IEvent[]>(data, _settings);
		}
	}
}