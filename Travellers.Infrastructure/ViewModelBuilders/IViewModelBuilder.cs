using Travellers.Core.Commands;

namespace Travellers.Infrastructure.ViewModelBuilders
{
	public interface IViewModelBuilder<in T> where T : ICommand
	{
		void PersistViewModelFor(T cmd);
	}
}