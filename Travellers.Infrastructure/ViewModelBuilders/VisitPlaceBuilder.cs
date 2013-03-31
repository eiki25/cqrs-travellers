using System.Linq;
using Raven.Client;
using Travellers.Core.Commands;
using Travellers.Core.Entities;
using Travellers.Core.Repositories;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.ViewModelBuilders
{
	public class VisitPlaceBuilder : IViewModelBuilder<VisitPlace>
	{
		private readonly IDocumentSession _session;
		private readonly IRepository<Traveller> _repository;

		public VisitPlaceBuilder(IDocumentSession session, IRepository<Traveller> repository)
		{
			_session = session;
			_repository = repository;
		}

		public void PersistViewModelFor(VisitPlace cmd)
		{
			var model = _session.Load<TravellerModel>(cmd.TravellerId);
			var place = _session.Load<PlaceModel>(cmd.PlaceId);

			model.VisitedPlaces = model.VisitedPlaces
				.Concat(new[]
					        {
						        new VisitModel
							        {
								        PlaceId = cmd.PlaceId,
								        PlaceName = place.Name,
								        Rating = cmd.Rating
							        }
					        });

			var traveller = _repository.ById(cmd.TravellerId);
			model.NumberOfVisits = traveller.NumberOfVisits;
			model.TotalPoints = traveller.TotalPoints;
			model.IsReallyCool = traveller.IsReallyCool;
		}
	}
}