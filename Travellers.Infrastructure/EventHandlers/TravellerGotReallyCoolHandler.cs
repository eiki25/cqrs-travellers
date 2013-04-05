using Raven.Client;
using Travellers.Core.Events;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.EventHandlers
{
	public class TravellerGotReallyCoolHandler : IEventHandler<TravellerGotReallyCool>
	{
		private readonly IDocumentSession _session;

		public TravellerGotReallyCoolHandler(IDocumentSession session)
		{
			_session = session;
		}

		public void Handle(TravellerGotReallyCool evt)
		{
			var model = _session.Load<TravellerModel>(evt.TravellerId);
			model.IsReallyCool = true;
			_session.SaveChanges();
		}
	}
}