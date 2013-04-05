using Raven.Client;
using Travellers.Core.Events;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.EventHandlers
{
	public class TravellerCountryChangedHandler : IEventHandler<TravellerCountryChanged>
	{
		private readonly IDocumentSession _session;

		public TravellerCountryChangedHandler(IDocumentSession session)
		{
			_session = session;
		}

		public void Handle(TravellerCountryChanged evt)
		{
			var model = _session.Load<TravellerModel>(evt.TravellerId);
			model.Country = evt.Country;
			_session.SaveChanges();
		}
	}
}