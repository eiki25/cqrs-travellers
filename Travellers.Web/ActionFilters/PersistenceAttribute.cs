using System.Web.Mvc;
using Travellers.Infrastructure.Persistence;

namespace Travellers.Web.ActionFilters
{
	public class PersistenceAttribute : ActionFilterAttribute
	{
		public IPersistenceManager PersistenceManager { get; set; }

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (filterContext.Exception == null)
			{
				PersistenceManager.SaveChanges();
			}
		}
	}
}