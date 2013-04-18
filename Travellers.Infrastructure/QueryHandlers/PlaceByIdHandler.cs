using Simple.Data;
using Travellers.Core.Queries;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.QueryHandlers
{
	public class PlaceByIdHandler : IQueryHandler<PlaceById, PlaceModel>
	{
		public PlaceModel Execute(PlaceById query)
		{
			return Database.Open().Places.Get(query.Id);
		}
	}
}