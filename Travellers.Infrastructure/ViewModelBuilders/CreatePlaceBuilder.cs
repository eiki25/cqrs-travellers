using Raven.Client;
using Travellers.Core.Commands;
using Travellers.Core.ViewModels;

namespace Travellers.Infrastructure.ViewModelBuilders
{
	public class CreatePlaceBuilder : IViewModelBuilder<CreatePlace>
	{
		private readonly IDocumentSession _session;

		public CreatePlaceBuilder(IDocumentSession session)
		{
			_session = session;
		}

		public void PersistViewModelFor(CreatePlace cmd)
		{
			_session.Store(new PlaceModel { Id = cmd.PlaceId, Name = cmd.Name, Description = cmd.Description, Points = cmd.Points });
		}
	}
}
