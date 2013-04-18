using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Travellers.Core.Commands;
using Travellers.Core.Queries;
using Travellers.Infrastructure;
using Travellers.Infrastructure.CommandDispatcher;
using Travellers.Infrastructure.CommandHandlers;
using Travellers.Infrastructure.Persistence;
using Travellers.Infrastructure.QueryHandlers;
using Travellers.Infrastructure.QueryService;
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

			builder.RegisterGeneric(typeof(DbContextRepository<>)).AsImplementedInterfaces();

			// Command handlers
			builder.RegisterAssemblyTypes(typeof(CreateTravellerHandler).Assembly)
				.InNamespaceOf<CreateTravellerHandler>()
				.AsImplementedInterfaces();

			// Query handlers
			builder.RegisterAssemblyTypes(typeof(TravellerByIdHandler).Assembly)
				.InNamespaceOf<TravellerByIdHandler>()
				.AsImplementedInterfaces();

			builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
			builder.RegisterType<QueryService>().As<IQueryService>();

			builder.RegisterType<MvcResolver>().As<IResolver>();

			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}
