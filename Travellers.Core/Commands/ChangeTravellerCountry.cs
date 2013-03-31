using System;

namespace Travellers.Core.Commands
{
	public class ChangeTravellerCountry : ICommand
	{
		public Guid TravellerId { get; private set; }
		public string Country { get; private set; }

		public ChangeTravellerCountry(Guid travellerId, string country)
		{
			TravellerId = travellerId;
			Country = country;
		}
	}
}