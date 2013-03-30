using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Travellers.Core.Entities;
using Travellers.Core.Repositories;
using Travellers.Infrastructure;
using Travellers.Infrastructure.Persistence;
using Travellers.Infrastructure.Repositories;

namespace Travellers.Web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			ConfigureContainer();

			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}

		private void ConfigureContainer()
		{
			var builder = new ContainerBuilder();
			builder.RegisterControllers(typeof(MvcApplication).Assembly);
			builder.RegisterFilterProvider();

			builder.RegisterType<TravellersDbContext>().As<DbContext>().InstancePerHttpRequest();
			builder.RegisterType<PersistenceManager>().As<IPersistenceManager>().InstancePerHttpRequest();

			builder.RegisterType<DbContextTravellerRepository>().As<IRepository<Traveller>>();
			builder.RegisterType<DbContextPlaceRepository>().As<IRepository<Place>>();

			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}