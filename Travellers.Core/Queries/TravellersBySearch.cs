using Travellers.Core.ViewModels;

namespace Travellers.Core.Queries
{
	public class TravellersBySearch : IQuery<SearchTravellerModel>
	{
		public string SearchString { get; set; }
	}
}