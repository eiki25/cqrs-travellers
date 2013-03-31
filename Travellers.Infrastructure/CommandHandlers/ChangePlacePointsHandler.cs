using Travellers.Core.Commands;
using Travellers.Core.Entities;
using Travellers.Core.Repositories;

namespace Travellers.Infrastructure.CommandHandlers
{
	public class ChangePlacePointsHandler : ICommandHandler<ChangePlacePoints>
	{
		private readonly IRepository<Place> _repository;

		public ChangePlacePointsHandler(IRepository<Place> repository)
		{
			_repository = repository;
		}

		public void Handle(ChangePlacePoints cmd)
		{
			var place = _repository.ById(cmd.PlaceId);

			if (place != null)
			{
				place.ChangePoints(cmd.Points);
			}
		}
	}
}