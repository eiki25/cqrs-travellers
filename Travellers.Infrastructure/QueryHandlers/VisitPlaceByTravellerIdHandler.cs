using System.Linq;
using Raven.Client;
using Travellers.Core.Queries;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.QueryHandlers
{
	public class VisitPlaceByTravellerIdHandler : IQueryHandler<VisitPlaceByTravellerId, VisitPlaceModel>
	{
		private readonly IDocumentSession _session;

		public VisitPlaceByTravellerIdHandler(IDocumentSession session)
		{
			_session = session;
		}

		public VisitPlaceModel Execute(VisitPlaceByTravellerId query)
		{
			var traveller = _session.Load<TravellerModel>(query.TravellerId);
			var allPlaces = _session.Query<PlaceModel>().Customize(x => x.WaitForNonStaleResults());

			return traveller != null
				       ? new VisitPlaceModel
					         {
						         TravellerId = traveller.Id,
						         TravellerName = traveller.Firstname + " " + traveller.Lastname,
						         Places = allPlaces.ToList()
					         }
				       : null;
		}
	}
}