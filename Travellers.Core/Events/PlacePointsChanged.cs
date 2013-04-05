using System;

namespace Travellers.Core.Events
{
	public class PlacePointsChanged : IEvent
	{
		public Guid PlaceId { get; private set; }
		public int Points { get; private set; }

		public PlacePointsChanged(Guid placeId, int points)
		{
			PlaceId = placeId;
			Points = points;
		}
	}
}