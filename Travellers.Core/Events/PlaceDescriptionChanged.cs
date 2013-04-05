using System;

namespace Travellers.Core.Events
{
	public class PlaceDescriptionChanged : IEvent
	{
		public Guid PlaceId { get; private set; }
		public string Description { get; private set; }

		public PlaceDescriptionChanged(Guid placeId, string description)
		{
			PlaceId = placeId;
			Description = description;
		}
	}
}