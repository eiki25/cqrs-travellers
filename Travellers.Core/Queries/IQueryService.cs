namespace Travellers.Core.Queries
{
	public interface IQueryService
	{
		TResult ExecuteQuery<TResult>(IQuery<TResult> query);
	}
}