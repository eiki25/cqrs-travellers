using System;

namespace Travellers.Core.Commands
{
	public class ChangePlaceName : ICommand
	{
		public Guid PlaceId { get; private set; }
		public string Name { get; private set; }

		public ChangePlaceName(Guid placeId, string name)
		{
			PlaceId = placeId;
			Name = name;
		}
	}
}