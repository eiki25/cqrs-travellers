using System;
using Travellers.Core.ViewModels;

namespace Travellers.Core.Queries
{
	public class PlaceById : IQuery<PlaceModel>
	{
		public Guid Id { get; set; }
	}
}