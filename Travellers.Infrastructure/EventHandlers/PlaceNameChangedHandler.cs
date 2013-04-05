using Raven.Client;
using Travellers.Core.Events;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.EventHandlers
{
	public class PlaceNameChangedHandler : IEventHandler<PlaceNameChanged>
	{
		private readonly IDocumentSession _session;

		public PlaceNameChangedHandler(IDocumentSession session)
		{
			_session = session;
		}

		public void Handle(PlaceNameChanged evt)
		{
			var model = _session.Load<PlaceModel>(evt.PlaceId);
			model.Name = evt.Name;
			_session.SaveChanges();
		}
	}
}