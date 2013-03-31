using System;

namespace Travellers.Core.Commands
{
	public class VisitPlace : ICommand
	{
		public Guid TravellerId { get; private set; }
		public Guid PlaceId { get; private set; }
		public int Points { get; private set; }
		public int? Rating { get; private set; }

		public VisitPlace(Guid travellerId, Guid placeId, int points, int? rating)
		{
			TravellerId = travellerId;
			PlaceId = placeId;
			Points = points;
			Rating = rating;
		}
	}
}