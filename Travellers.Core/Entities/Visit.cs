using System;

namespace Travellers.Core.Entities
{
	public class Visit
	{
		public Guid TravellerId { get; set; }
		public int VisitNumber { get; set; }
		public Guid PlaceId { get; set; }
		public int? Rating { get; set; }

		public virtual Place Place { get; set; }
		public virtual Traveller Traveller { get; set; }
	}
}