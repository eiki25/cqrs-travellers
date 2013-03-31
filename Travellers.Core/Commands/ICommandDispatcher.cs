namespace Travellers.Core.Commands
{
	public interface ICommandDispatcher
	{
		void Send<T>(T cmd) where T : ICommand;
	}
}