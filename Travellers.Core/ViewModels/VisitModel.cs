using System;
using System.ComponentModel;

namespace Travellers.Core.ViewModels
{
	public class VisitModel
	{
		public Guid PlaceId { get; set; }
		[DisplayName("Place name")]
		public string PlaceName { get; set; }
		public int? Rating { get; set; }
	}
}