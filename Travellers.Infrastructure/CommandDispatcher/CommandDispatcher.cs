using Travellers.Core.Commands;
using Travellers.Infrastructure.EventStore;

namespace Travellers.Infrastructure.CommandDispatcher
{
	public class CommandDispatcher : ICommandDispatcher
	{
		private readonly IResolver _resolver;
		private readonly IEventStoreContext _context;

		public CommandDispatcher(IResolver resolver, IEventStoreContext context)
		{
			_resolver = resolver;
			_context = context;
		}

		public void Send<T>(T cmd) where T : ICommand
		{
			var handler = _resolver.Resolve<ICommandHandler<T>>();
			var handled = false;

			while (handled == false)
			{
				try
				{
					handler.Handle(cmd);
					_context.SaveChanges();
					handled = true;
				}
				catch (ConcurrencyException)
				{
					// Just try the command again, if versions does not match the aggregate will be re-read from the repository with a new version.
				}
			}
		}
	}
}
