using System;

namespace Travellers.Core.Events
{
	public class TravellerGotReallyCool : IEvent
	{
		public Guid TravellerId { get; private set; }

		public TravellerGotReallyCool(Guid travellerId)
		{
			TravellerId = travellerId;
		}
	}
}