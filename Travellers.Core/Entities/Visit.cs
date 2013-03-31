using System;

namespace Travellers.Core.Entities
{
	public class Visit
	{
		// Needed by EF
		protected Visit()
		{
		}

		public Visit(Guid travellerId, Guid placeId, int visitNumber, int? rating)
		{
			TravellerId = travellerId;
			PlaceId = placeId;
			VisitNumber = visitNumber;
			Rating = rating;
		}

		public Guid TravellerId { get; protected set; }
		public int VisitNumber { get; protected set; }
		public Guid PlaceId { get; protected set; }
		public int? Rating { get; protected set; }

		public virtual Place Place { get; protected set; }
		public virtual Traveller Traveller { get; protected set; }
	}
}