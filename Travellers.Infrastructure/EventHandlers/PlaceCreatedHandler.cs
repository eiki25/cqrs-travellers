using Raven.Client;
using Travellers.Core.Events;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.EventHandlers
{
	public class PlaceCreatedHandler : IEventHandler<PlaceCreated>
	{
		private readonly IDocumentSession _session;

		public PlaceCreatedHandler(IDocumentSession session)
		{
			_session = session;
		}

		public void Handle(PlaceCreated evt)
		{
			_session.Store(new PlaceModel {Id = evt.PlaceId, Name = evt.Name, Description = evt.Description, Points = evt.Points});
			_session.SaveChanges();
		}
	}
}
