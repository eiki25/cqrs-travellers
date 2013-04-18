using Raven.Client;
using Travellers.Core.Queries;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.QueryHandlers
{
	public class TravellerByIdHandler : IQueryHandler<TravellerById, TravellerModel>
	{
		private readonly IDocumentSession _session;

		public TravellerByIdHandler(IDocumentSession session)
		{
			_session = session;
		}

		public TravellerModel Execute(TravellerById query)
		{
			return _session.Load<TravellerModel>(query.Id);
		}
	}
}
