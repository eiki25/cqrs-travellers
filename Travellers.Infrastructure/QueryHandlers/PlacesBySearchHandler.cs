using System.Linq;
using Raven.Client;
using Travellers.Core.Queries;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.QueryHandlers
{
	public class PlacesBySearchHandler : IQueryHandler<PlacesBySearch, SearchPlaceModel>
	{
		private readonly IDocumentSession _session;

		public PlacesBySearchHandler(IDocumentSession session)
		{
			_session = session;
		}

		public SearchPlaceModel Execute(PlacesBySearch query)
		{
			var places = _session.Query<PlaceModel>()
				.Customize(x => x.WaitForNonStaleResults())
				.Where(
					x => x.Name.StartsWith(query.SearchString) ||
					     x.Description.StartsWith(query.SearchString));

			return new SearchPlaceModel
					   {
						   Query = query.SearchString,
						   Places = places.ToList()
					   };
		}
	}
}