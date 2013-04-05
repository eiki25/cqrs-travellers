using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Travellers.Infrastructure;

namespace Travellers.Web
{
	public class MvcResolver : IResolver
	{
		public T Resolve<T>()
		{
			return DependencyResolver.Current.GetService<T>();
		}

		public object Resolve(Type type)
		{
			return DependencyResolver.Current.GetService(type);
		}

		public IEnumerable<object> ResolveAll(Type type)
		{
			return DependencyResolver.Current.GetServices(type);
		}
	}
}