using System.Linq;
using Raven.Client;
using Travellers.Core.Events;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.EventHandlers
{
	public class TravellerVisitedPlaceHandler : IEventHandler<TravellerVisitedPlace>
	{
		private readonly IDocumentSession _session;

		public TravellerVisitedPlaceHandler(IDocumentSession session)
		{
			_session = session;
		}

		public void Handle(TravellerVisitedPlace evt)
		{
			var model = _session.Load<TravellerModel>(evt.TravellerId);
			var place = _session.Load<PlaceModel>(evt.PlaceId);

			var visitedPlaces = model.VisitedPlaces.ToList();
			visitedPlaces.Add(new VisitModel {PlaceId = evt.PlaceId, PlaceName = place.Name, Rating = evt.Rating});
			model.VisitedPlaces = visitedPlaces;

			model.NumberOfVisits++;
			model.TotalPoints += evt.Points;
			_session.SaveChanges();
		}
	}
}