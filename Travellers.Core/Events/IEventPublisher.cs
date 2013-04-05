namespace Travellers.Core.Events
{
	public interface IEventPublisher
	{
		void Publish<T>(T evt) where T : IEvent;
	}
}