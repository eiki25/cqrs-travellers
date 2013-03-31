using Raven.Client;
using Travellers.Core.Commands;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.ViewModelBuilders
{
	public class ChangeTravellerNameBuilder : IViewModelBuilder<ChangeTravellerName>
	{
		private readonly IDocumentSession _session;

		public ChangeTravellerNameBuilder(IDocumentSession session)
		{
			_session = session;
		}

		public void PersistViewModelFor(ChangeTravellerName cmd)
		{
			var model = _session.Load<TravellerModel>(cmd.TravellerId);
			model.Firstname = cmd.Firstname;
			model.Lastname = cmd.Lastname;
		}
	}
}