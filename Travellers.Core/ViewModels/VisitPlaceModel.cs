using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Travellers.Core.ViewModels
{
	public class VisitPlaceModel
	{
		public Guid TravellerId { get; set; }

		[DisplayName("Traveller name")]
		public string TravellerName { get; set; }

		public Guid SelectedPlaceId { get; set; }
		public int? Rating { get; set; }

		public IEnumerable<PlaceModel> Places { get; set; }
	}
}