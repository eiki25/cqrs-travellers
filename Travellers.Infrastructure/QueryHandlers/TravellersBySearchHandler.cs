
using Simple.Data;
using Travellers.Core.Queries;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.QueryHandlers
{
	public class TravellersBySearchHandler : IQueryHandler<TravellersBySearch, SearchTravellerModel>
	{
		public SearchTravellerModel Execute(TravellersBySearch query)
		{
			var db = Database.Open();

			return new SearchTravellerModel
				       {
					       Query = query.SearchString,
					       Travellers = string.IsNullOrWhiteSpace(query.SearchString)
						                    ? db.Travellers.All().Cast<TravellerModel>()
						                    : db.Travellers.FindAll(
							                    db.Travellers.Firstname.Like("%" + query.SearchString + "%") ||
							                    db.Travellers.Lastname.Like("%" + query.SearchString + "%") ||
							                    db.Travellers.Country.Like("%" + query.SearchString + "%")
							                      ).Cast<TravellerModel>()
				       };
		}
	}
}