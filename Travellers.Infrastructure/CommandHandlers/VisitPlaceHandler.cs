using Travellers.Core.Commands;
using Travellers.Core.Entities;
using Travellers.Core.Repositories;

namespace Travellers.Infrastructure.CommandHandlers
{
	public class VisitPlaceHandler : ICommandHandler<VisitPlace>
	{
		private readonly IRepository<Traveller> _travellerRepository;

		public VisitPlaceHandler(IRepository<Traveller> travellerRepository)
		{
			_travellerRepository = travellerRepository;
		}

		public void Handle(VisitPlace cmd)
		{
			var traveller = _travellerRepository.ById(cmd.TravellerId);

			if (traveller != null)
			{
				traveller.VisitPlace(cmd.PlaceId, cmd.Points, cmd.Rating);
			}
		}
	}
}