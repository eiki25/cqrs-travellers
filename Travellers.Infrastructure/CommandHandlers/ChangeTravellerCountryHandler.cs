using Travellers.Core.Commands;
using Travellers.Core.Entities;
using Travellers.Core.Repositories;

namespace Travellers.Infrastructure.CommandHandlers
{
	public class ChangeTravellerCountryHandler : ICommandHandler<ChangeTravellerCountry>
	{
		private readonly IRepository<Traveller> _repository;

		public ChangeTravellerCountryHandler(IRepository<Traveller> repository)
		{
			_repository = repository;
		}

		public void Handle(ChangeTravellerCountry cmd)
		{
			var traveller = _repository.ById(cmd.TravellerId);

			if (traveller != null)
			{
				traveller.ChangeCountry(cmd.Country);
			}
		}
	}
}