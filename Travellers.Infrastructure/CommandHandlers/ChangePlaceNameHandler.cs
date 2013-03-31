using Travellers.Core.Commands;
using Travellers.Core.Entities;
using Travellers.Core.Repositories;

namespace Travellers.Infrastructure.CommandHandlers
{
	public class ChangePlaceNameHandler : ICommandHandler<ChangePlaceName>
	{
		private readonly IRepository<Place> _repository;

		public ChangePlaceNameHandler(IRepository<Place> repository)
		{
			_repository = repository;
		}

		public void Handle(ChangePlaceName cmd)
		{
			var place = _repository.ById(cmd.PlaceId);

			if (place != null)
			{
				place.ChangeName(cmd.Name);
			}
		}
	}
}