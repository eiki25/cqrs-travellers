using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Travellers.Core.Queries;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.QueryHandlers
{
	public class TravellersBySearchHandler : IQueryHandler<TravellersBySearch, SearchTravellerModel>
	{
		private readonly IDocumentSession _session;

		public TravellersBySearchHandler(IDocumentSession session)
		{
			_session = session;
		}

		public SearchTravellerModel Execute(TravellersBySearch query)
		{
			var travellers = _session.Query<TravellerModel>()
				.Customize(x => x.WaitForNonStaleResults())
				.Where(x =>
				       x.Firstname.StartsWith(query.SearchString) ||
				       x.Lastname.StartsWith(query.SearchString) ||
				       x.Country.StartsWith(query.SearchString)
				);

			return new SearchTravellerModel
				       {
					       Query = query.SearchString,
					       Travellers = travellers.ToList()
				       };
		}
	}
}