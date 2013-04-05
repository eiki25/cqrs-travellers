namespace Travellers.Infrastructure
{
	public interface IContext
	{
		object this[object key] { get; set; }
	}
}