using System;

namespace Travellers.Core.Events
{
	public class PlaceNameChanged : IEvent
	{
		public Guid PlaceId { get; private set; }
		public string Name { get; private set; }

		public PlaceNameChanged(Guid placeId, string name)
		{
			PlaceId = placeId;
			Name = name;
		}
	}
}