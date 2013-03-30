using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Travellers.Core.ViewModels
{
	public class TravellerModel
	{
		public Guid Id { get; set; }

		[Required]
		[StringLength(255, MinimumLength = 1)]
		public string Firstname { get; set; }

		[Required]
		[StringLength(255, MinimumLength = 1)]
		public string Lastname { get; set; }

		[Required]
		[StringLength(255, MinimumLength = 1)]
		public string Country { get; set; }

		[ReadOnly(true)]
		[DisplayName("Total points")]
		public int TotalPoints { get; set; }

		[ReadOnly(true)]
		[DisplayName("Number of visited places")]
		public int NumberOfVisits { get; set; }

		[ReadOnly(true)]
		public bool IsReallyCool { get; set; }

		[ReadOnly(true)]
		[DisplayName("Visited places")]
		public IEnumerable<VisitModel> VisitedPlaces { get; set; }
	}
}