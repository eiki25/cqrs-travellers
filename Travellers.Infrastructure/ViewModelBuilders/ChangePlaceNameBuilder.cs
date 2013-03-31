using Raven.Client;
using Travellers.Core.Commands;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.ViewModelBuilders
{
	public class ChangePlaceNameBuilder : IViewModelBuilder<ChangePlaceName>
	{
		private readonly IDocumentSession _session;

		public ChangePlaceNameBuilder(IDocumentSession session)
		{
			_session = session;
		}

		public void PersistViewModelFor(ChangePlaceName cmd)
		{
			var model = _session.Load<PlaceModel>(cmd.PlaceId);
			model.Name = cmd.Name;
		}
	}
}