using Raven.Client;
using Travellers.Core.Queries;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.QueryHandlers
{
	public class PlaceByIdHandler : IQueryHandler<PlaceById, PlaceModel>
	{
		private readonly IDocumentSession _session;

		public PlaceByIdHandler(IDocumentSession session)
		{
			_session = session;
		}

		public PlaceModel Execute(PlaceById query)
		{
			return _session.Load<PlaceModel>(query.Id);
		}
	}
}
