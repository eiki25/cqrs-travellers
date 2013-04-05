using System;

namespace Travellers.Core.Events
{
	public class TravellerVisitedPlace : IEvent
	{
		public Guid TravellerId { get; private set; }
		public Guid PlaceId { get; private set; }
		public int Points { get; private set; }
		public int? Rating { get; private set; }

		public TravellerVisitedPlace(Guid travellerId, Guid placeId, int points, int? rating)
		{
			TravellerId = travellerId;
			PlaceId = placeId;
			Points = points;
			Rating = rating;
		}
	}
}