using Travellers.Core.Events;

namespace Travellers.Infrastructure.EventPublisher
{
	public class EventPublisher : IEventPublisher
	{
		private readonly IResolver _resolver;

		public EventPublisher(IResolver resolver)
		{
			_resolver = resolver;
		}

		public void Publish<T>(T evt) where T : IEvent
		{
			var handlerType = typeof(IEventHandler<>).MakeGenericType(evt.GetType());
			var eventHandlers = _resolver.ResolveAll(handlerType);

			foreach (var eventHandler in eventHandlers)
			{
				handlerType.GetMethod("Handle").Invoke(eventHandler, new object[] {evt});
			}
		}
	}
}
