using Raven.Client;
using Travellers.Core.Events;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.EventHandlers
{
	public class PlaceDescriptionChangedHandler : IEventHandler<PlaceDescriptionChanged>
	{
		private readonly IDocumentSession _session;

		public PlaceDescriptionChangedHandler(IDocumentSession session)
		{
			_session = session;
		}

		public void Handle(PlaceDescriptionChanged evt)
		{
			var model = _session.Load<PlaceModel>(evt.PlaceId);
			model.Description = evt.Description;
			_session.SaveChanges();
		}
	}
}