using System;
using System.ComponentModel.DataAnnotations;

namespace Travellers.Core.ViewModels
{
	public class PlaceModel
	{
		public Guid Id { get; set; }

		[Required]
		[StringLength(255, MinimumLength = 1)]
		public string Name { get; set; }

		[Required]
		[StringLength(255, MinimumLength = 1)]
		public string Description { get; set; }

		[Required]
		[Range(1, 500, ErrorMessage = "Must be a number between 1 and 500.")]
		public int Points { get; set; }
	}
}