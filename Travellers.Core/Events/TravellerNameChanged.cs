using System;

namespace Travellers.Core.Events
{
	public class TravellerNameChanged : IEvent
	{
		public Guid TravellerId { get; private set; }
		public string Firstname { get; private set; }
		public string Lastname { get; private set; }

		public TravellerNameChanged(Guid travellerId, string firstname, string lastname)
		{
			TravellerId = travellerId;
			Firstname = firstname;
			Lastname = lastname;
		}
	}
}