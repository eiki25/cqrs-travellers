using System;

namespace Travellers.Core.Commands
{
	public class CreateTraveller : ICommand
	{
		public Guid TravellerId { get; private set; }
		public string Firstname { get; private set; }
		public string Lastname { get; private set; }
		public string Country { get; private set; }

		public CreateTraveller(Guid travellerId, string firstname, string lastname, string country)
		{
			TravellerId = travellerId;
			Firstname = firstname;
			Lastname = lastname;
			Country = country;
		}
	}
}