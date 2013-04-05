using Raven.Client;
using Travellers.Core.Events;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.EventHandlers
{
	public class TravellerCreatedHandler : IEventHandler<TravellerCreated>
	{
		private readonly IDocumentSession _session;

		public TravellerCreatedHandler(IDocumentSession session)
		{
			_session = session;
		}

		public void Handle(TravellerCreated evt)
		{
			_session.Store(new TravellerModel
				               {
					               Id = evt.TravellerId,
					               Firstname = evt.Firstname,
					               Lastname = evt.Lastname,
					               Country = evt.Country,
					               VisitedPlaces = new VisitModel[0]
				               });
			_session.SaveChanges();
		}
	}

}