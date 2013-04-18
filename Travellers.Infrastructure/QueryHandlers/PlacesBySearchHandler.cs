using Simple.Data;
using Travellers.Core.Queries;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.QueryHandlers
{
	public class PlacesBySearchHandler : IQueryHandler<PlacesBySearch, SearchPlaceModel>
	{
		public SearchPlaceModel Execute(PlacesBySearch query)
		{
			var db = Database.Open();

			return new SearchPlaceModel
					   {
						   Query = query.SearchString,
						   Places = string.IsNullOrWhiteSpace(query.SearchString)
										? db.Places.All().Cast<PlaceModel>()
										: db.Places.FindAll(
											db.Places.Name.Like("%" + query.SearchString + "%") ||
											db.Places.Description.Like("%" + query.SearchString + "%")
											  ).Cast<PlaceModel>()
					   };
		}
	}
}