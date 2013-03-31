namespace Travellers.Core.Commands
{
	public interface ICommandHandler<in T> where T : ICommand
	{
		void Handle(T cmd);
	}
}