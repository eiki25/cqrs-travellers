using System;
using Travellers.Core.ViewModels;

namespace Travellers.Core.Queries
{
	public class TravellerById : IQuery<TravellerModel>
	{
		public Guid Id { get; set; }
	}
}
