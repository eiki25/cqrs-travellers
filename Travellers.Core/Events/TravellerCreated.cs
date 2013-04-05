using System;

namespace Travellers.Core.Events
{
	public class TravellerCreated : IEvent
	{
		public Guid TravellerId { get; private set; }
		public string Firstname { get; private set; }
		public string Lastname { get; private set; }
		public string Country { get; private set; }

		public TravellerCreated(Guid travellerId, string firstname, string lastname, string country)
		{
			TravellerId = travellerId;
			Firstname = firstname;
			Lastname = lastname;
			Country = country;
		}
	}
}