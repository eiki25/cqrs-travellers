using System;

namespace Travellers.Core.Commands
{
	public class ChangePlacePoints : ICommand
	{
		public Guid PlaceId { get; private set; }
		public int Points { get; private set; }

		public ChangePlacePoints(Guid placeId, int points)
		{
			PlaceId = placeId;
			Points = points;
		}
	}
}