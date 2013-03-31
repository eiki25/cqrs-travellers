using Travellers.Core.Commands;
using Travellers.Core.Entities;
using Travellers.Core.Repositories;

namespace Travellers.Infrastructure.CommandHandlers
{
	public class CreateTravellerHandler : ICommandHandler<CreateTraveller>
	{
		private readonly IRepository<Traveller> _repository;

		public CreateTravellerHandler(IRepository<Traveller> repository)
		{
			_repository = repository;
		}

		public void Handle(CreateTraveller cmd)
		{
			_repository.Add(new Traveller(cmd.TravellerId, cmd.Firstname, cmd.Lastname, cmd.Country));
		}
	}
}