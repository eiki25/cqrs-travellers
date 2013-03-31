using Travellers.Core.Commands;
using Travellers.Core.Entities;
using Travellers.Core.Repositories;

namespace Travellers.Infrastructure.CommandHandlers
{
	public class ChangeTravellerNameHandler : ICommandHandler<ChangeTravellerName>
	{
		private readonly IRepository<Traveller> _repository;

		public ChangeTravellerNameHandler(IRepository<Traveller> repository)
		{
			_repository = repository;
		}

		public void Handle(ChangeTravellerName cmd)
		{
			var traveller = _repository.ById(cmd.TravellerId);

			if (traveller != null)
			{
				traveller.ChangeName(cmd.Firstname, cmd.Lastname);
			}
		}
	}
}