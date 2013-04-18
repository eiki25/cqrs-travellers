using Simple.Data;
using Travellers.Core.Queries;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.QueryHandlers
{
	public class TravellerByIdHandler : IQueryHandler<TravellerById, TravellerModel>
	{
		public TravellerModel Execute(TravellerById query)
		{
			var db = Database.Open();

			TravellerModel model = db.Travellers.Get(query.Id);

			if (model != null)
			{
				model.VisitedPlaces = db.Visits
					.FindAllByTravellerId(query.Id).Select(
						db.Visits.PlaceId,
						db.Visits.Places.Name.As("PlaceName"),
						db.Visits.Rating).Cast<VisitModel>();
			}

			return model;
		}
	}
}
