using Raven.Client;
using Travellers.Core.Commands;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.ViewModelBuilders
{
	public class ChangeTravellerCountryBuilder : IViewModelBuilder<ChangeTravellerCountry>
	{
		private readonly IDocumentSession _session;

		public ChangeTravellerCountryBuilder(IDocumentSession session)
		{
			_session = session;
		}

		public void PersistViewModelFor(ChangeTravellerCountry cmd)
		{
			var model = _session.Load<TravellerModel>(cmd.TravellerId);
			model.Country = cmd.Country;
		}
	}
}