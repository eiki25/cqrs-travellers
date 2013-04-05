using Raven.Client;
using Travellers.Core.Events;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.EventHandlers
{
	public class TravellerNameChangedHandler : IEventHandler<TravellerNameChanged>
	{
		private readonly IDocumentSession _session;

		public TravellerNameChangedHandler(IDocumentSession session)
		{
			_session = session;
		}

		public void Handle(TravellerNameChanged evt)
		{
			var model = _session.Load<TravellerModel>(evt.TravellerId);
			model.Firstname = evt.Firstname;
			model.Lastname = evt.Lastname;
			_session.SaveChanges();
		}
	}
}