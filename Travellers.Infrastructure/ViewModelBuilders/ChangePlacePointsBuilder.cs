using Raven.Client;
using Travellers.Core.Commands;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.ViewModelBuilders
{
	public class ChangePlacePointsBuilder : IViewModelBuilder<ChangePlacePoints>
	{
		private readonly IDocumentSession _session;

		public ChangePlacePointsBuilder(IDocumentSession session)
		{
			_session = session;
		}

		public void PersistViewModelFor(ChangePlacePoints cmd)
		{
			var model = _session.Load<PlaceModel>(cmd.PlaceId);
			model.Points = cmd.Points;
		}
	}
}