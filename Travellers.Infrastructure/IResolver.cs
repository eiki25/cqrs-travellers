using System;

namespace Travellers.Infrastructure
{
	public interface IResolver
	{
		T Resolve<T>();
	}
}