using System;

namespace Travellers.Core.Events
{
	public class PlaceCreated : IEvent
	{
		public Guid PlaceId { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }
		public int Points { get; private set; }

		public PlaceCreated(Guid placeId, string name, string description, int points)
		{
			PlaceId = placeId;
			Name = name;
			Description = description;
			Points = points;
		}
	}
}
