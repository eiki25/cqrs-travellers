using System.Collections.Generic;

namespace Travellers.Core.ViewModels
{
	public class SearchTravellerModel
	{
		public string Query { get; set; }

		public IEnumerable<TravellerModel> Travellers { get; set; }
	}
}