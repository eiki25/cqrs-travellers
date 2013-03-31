using Raven.Client;
using Travellers.Core.Commands;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.ViewModelBuilders
{
	public class CreateTravellerBuilder : IViewModelBuilder<CreateTraveller>
	{
		private readonly IDocumentSession _session;

		public CreateTravellerBuilder(IDocumentSession session)
		{
			_session = session;
		}

		public void PersistViewModelFor(CreateTraveller cmd)
		{
			_session.Store(new TravellerModel
							   {
								   Id = cmd.TravellerId,
								   Firstname = cmd.Firstname,
								   Lastname = cmd.Lastname,
								   Country = cmd.Country,
								   VisitedPlaces = new VisitModel[0]
							   });
		}
	}
}