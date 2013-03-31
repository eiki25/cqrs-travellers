using System;

namespace Travellers.Core.Commands
{
	public class ChangeTravellerName : ICommand
	{
		public Guid TravellerId { get; private set; }
		public string Firstname { get; private set; }
		public string Lastname { get; private set; }

		public ChangeTravellerName(Guid travellerId, string firstname, string lastname)
		{
			TravellerId = travellerId;
			Firstname = firstname;
			Lastname = lastname;
		}
	}
}