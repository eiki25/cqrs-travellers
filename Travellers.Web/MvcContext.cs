using System.Web;
using Travellers.Infrastructure;

namespace Travellers.Web
{
	public class MvcContext : IContext
	{
		public object this[object key]
		{
			get
			{
				return HttpContext.Current.Items[key];
			}
			set
			{
				HttpContext.Current.Items[key] = value;
			}
		}
	}
}