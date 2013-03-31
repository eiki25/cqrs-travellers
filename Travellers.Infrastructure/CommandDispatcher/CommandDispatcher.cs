using System;
using Travellers.Core.Commands;

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

			handler.Handle(cmd);
		}
	}
}
