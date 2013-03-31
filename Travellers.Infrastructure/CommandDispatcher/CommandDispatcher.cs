using Travellers.Core.Commands;
using Travellers.Infrastructure.ViewModelBuilders;

namespace Travellers.Infrastructure.CommandDispatcher
{
	public class CommandDispatcher : ICommandDispatcher
	{
		private readonly IResolver _resolver;

		public CommandDispatcher(IResolver resolver)
		{
			_resolver = resolver;
		}

		public void Send<T>(T cmd) where T : ICommand
		{
			var handler = _resolver.Resolve<ICommandHandler<T>>();
			var builder = _resolver.Resolve<IViewModelBuilder<T>>();

			handler.Handle(cmd);
			builder.PersistViewModelFor(cmd);
		}
	}
}
