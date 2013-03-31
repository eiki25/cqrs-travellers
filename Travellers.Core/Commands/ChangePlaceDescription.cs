using System;

namespace Travellers.Core.Commands
{
	public class ChangePlaceDescription : ICommand
	{
		public Guid PlaceId { get; private set; }
		public string Description { get; private set; }

		public ChangePlaceDescription(Guid placeId, string description)
		{
			PlaceId = placeId;
			Description = description;
		}
	}
}