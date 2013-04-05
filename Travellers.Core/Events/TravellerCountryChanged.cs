using System;

namespace Travellers.Core.Events
{
	public class TravellerCountryChanged : IEvent
	{
		public Guid TravellerId { get; private set; }
		public string Country { get; private set; }

		public TravellerCountryChanged(Guid travellerId, string country)
		{
			TravellerId = travellerId;
			Country = country;
		}
	}
}