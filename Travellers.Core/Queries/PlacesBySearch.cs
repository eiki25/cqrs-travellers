using Travellers.Core.ViewModels;

namespace Travellers.Core.Queries
{
	public class PlacesBySearch : IQuery<SearchPlaceModel>
	{
		public string SearchString { get; set; }
	}
}