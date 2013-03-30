using System.Collections.Generic;

namespace Travellers.Core.ViewModels
{
	public class SearchPlaceModel
	{
		public string Query { get; set; }

		public IEnumerable<PlaceModel> Places { get; set; }
	}
}