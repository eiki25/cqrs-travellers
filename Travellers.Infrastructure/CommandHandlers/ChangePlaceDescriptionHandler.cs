using Travellers.Core.Commands;
using Travellers.Core.Entities;
using Travellers.Core.Repositories;

namespace Travellers.Infrastructure.CommandHandlers
{
	public class ChangePlaceDescriptionHandler : ICommandHandler<ChangePlaceDescription>
	{
		private readonly IRepository<Place> _repository;

		public ChangePlaceDescriptionHandler(IRepository<Place> repository)
		{
			_repository = repository;
		}

		public void Handle(ChangePlaceDescription cmd)
		{
			var place = _repository.ById(cmd.PlaceId);

			if (place != null)
			{
				place.ChangeDescription(cmd.Description);
			}
		}
	}
}