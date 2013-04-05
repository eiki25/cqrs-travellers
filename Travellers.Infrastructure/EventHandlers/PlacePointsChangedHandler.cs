using Raven.Client;
using Travellers.Core.Events;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.EventHandlers
{
	public class PlacePointsChangedHandler : IEventHandler<PlacePointsChanged>
	{
		private readonly IDocumentSession _session;

		public PlacePointsChangedHandler(IDocumentSession session)
		{
			_session = session;
		}

		public void Handle(PlacePointsChanged evt)
		{
			var model = _session.Load<PlaceModel>(evt.PlaceId);
			model.Points = evt.Points;
			_session.SaveChanges();
		}
	}
}