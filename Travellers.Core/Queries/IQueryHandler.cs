﻿namespace Travellers.Core.Queries
{
	public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult>
	{
		TResult Execute(TQuery query);
	}
}
