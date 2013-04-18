using Travellers.Core.Queries;

namespace Travellers.Infrastructure.QueryService
{
	public class QueryService : IQueryService
	{
		private readonly IResolver _resolver;

		public QueryService(IResolver resolver)
		{
			_resolver = resolver;
		}

		public TResult ExecuteQuery<TResult>(IQuery<TResult> query)
		{
			var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));			
			var handler = _resolver.Resolve(handlerType);

			return (TResult)((dynamic)handler).Execute((dynamic)query);
		}
	}
}
