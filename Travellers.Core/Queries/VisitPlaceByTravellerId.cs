using System;
using Travellers.Core.ViewModels;

namespace Travellers.Core.Queries
{
	public class VisitPlaceByTravellerId : IQuery<VisitPlaceModel>
	{
		public Guid TravellerId { get; set; }
	}
}