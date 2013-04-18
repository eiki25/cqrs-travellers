using Simple.Data;
using Travellers.Core.Queries;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.QueryHandlers
{
	public class VisitPlaceByTravellerIdHandler : IQueryHandler<VisitPlaceByTravellerId, VisitPlaceModel>
	{
		public VisitPlaceModel Execute(VisitPlaceByTravellerId query)
		{
			var db = Database.Open();

			var traveller = db.Travellers.Get(query.TravellerId);

			return traveller != null
				       ? new VisitPlaceModel
					         {
						         TravellerId = traveller.Id,
						         TravellerName = traveller.Firstname + " " + traveller.Lastname,
						         Places = db.Places.All().Cast<PlaceModel>()
					         }
				       : null;
		}
	}
}