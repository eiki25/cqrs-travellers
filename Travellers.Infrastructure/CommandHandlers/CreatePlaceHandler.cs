using Travellers.Core.Commands;
using Travellers.Core.Entities;
using Travellers.Core.Repositories;

namespace Travellers.Infrastructure.CommandHandlers
{
	public class CreatePlaceHandler : ICommandHandler<CreatePlace>
	{
		private readonly IRepository<Place> _repository;

		public CreatePlaceHandler(IRepository<Place> repository)
		{
			_repository = repository;
		}

		public void Handle(CreatePlace cmd)
		{
			_repository.Add(new Place(cmd.PlaceId, cmd.Name, cmd.Description, cmd.Points));
		}
	}
}