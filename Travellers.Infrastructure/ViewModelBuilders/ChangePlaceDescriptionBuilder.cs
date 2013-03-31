using Raven.Client;
using Travellers.Core.Commands;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.ViewModelBuilders
{
	public class ChangePlaceDescriptionBuilder : IViewModelBuilder<ChangePlaceDescription>
	{
		private readonly IDocumentSession _session;

		public ChangePlaceDescriptionBuilder(IDocumentSession session)
		{
			_session = session;
		}

		public void PersistViewModelFor(ChangePlaceDescription cmd)
		{
			var model = _session.Load<PlaceModel>(cmd.PlaceId);
			model.Description = cmd.Description;
		}
	}
}