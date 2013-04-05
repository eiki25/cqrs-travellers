namespace Travellers.Infrastructure.EventStore
{
	public interface IEventStoreContext
	{
		void SaveChanges();
	}
}