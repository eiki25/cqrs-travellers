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
	}
}