using System;
using System.Collections.Generic;

namespace Travellers.Infrastructure
{
	public interface IResolver
	{
		T Resolve<T>();
		object Resolve(Type type);
		IEnumerable<object> ResolveAll(Type type);
	}
}