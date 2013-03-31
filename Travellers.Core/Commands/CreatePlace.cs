using System;

namespace Travellers.Core.Commands
{
	public class CreatePlace : ICommand
	{
		public Guid PlaceId { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }
		public int Points { get; private set; }

		public CreatePlace(Guid placeId, string name, string description, int points)
		{
			PlaceId = placeId;
			Name = name;
			Description = description;
			Points = points;
		}
	}
}