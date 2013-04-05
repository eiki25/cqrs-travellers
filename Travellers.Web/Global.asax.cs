﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Raven.Client;
using Raven.Client.Document;
using Travellers.Core.Commands;
using Travellers.Core.Events;
using Travellers.Core.Queries;
using Travellers.Infrastructure;
using Travellers.Infrastructure.CommandDispatcher;
using Travellers.Infrastructure.CommandHandlers;
using Travellers.Infrastructure.EventHandlers;
using Travellers.Infrastructure.EventPublisher;
using Travellers.Infrastructure.EventStore;
using Travellers.Infrastructure.QueryHandlers;
using Travellers.Infrastructure.QueryService;
using Travellers.Infrastructure.Repositories;

namespace Travellers.Web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : System.Web.HttpApplication
	{
		private static DocumentStore _documentStore;

		protected void Application_Start()
		{
			ConfigureRavenDB();
			ConfigureContainer();

			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}

		private void ConfigureRavenDB()
		{
			_documentStore = new DocumentStore { ConnectionStringName = "RavenDB" };
			_documentStore.Initialize();
		}

		private void ConfigureContainer()
		{
			var builder = new ContainerBuilder();
			builder.RegisterControllers(typeof(MvcApplication).Assembly);

			builder.RegisterType<SqlEventStore>().As<IEventStore>();
			builder.RegisterType<JsonEventSerializer>().As<IEventSerializer>();
			builder.RegisterType<EventStoreContext>().As<IEventStoreContext>().InstancePerHttpRequest();
			builder.Register(c => CreateDocumentSession()).As<IDocumentSession>().InstancePerHttpRequest();

			builder.RegisterGeneric(typeof(EventSourcedRepository<>)).AsImplementedInterfaces();

			// Command handlers
			builder.RegisterAssemblyTypes(typeof(CreateTravellerHandler).Assembly)
				.InNamespaceOf<CreateTravellerHandler>()
				.AsImplementedInterfaces();

			// Query handlers
			builder.RegisterAssemblyTypes(typeof(TravellerByIdHandler).Assembly)
				.InNamespaceOf<TravellerByIdHandler>()
				.AsImplementedInterfaces();

			// Event handlers (previously ViewModelBuilders)
			builder.RegisterAssemblyTypes(typeof(TravellerCreatedHandler).Assembly)
				.InNamespaceOf<TravellerCreatedHandler>()
				.AsImplementedInterfaces();

			builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
			builder.RegisterType<QueryService>().As<IQueryService>();
			builder.RegisterType<EventPublisher>().As<IEventPublisher>();

			builder.RegisterType<MvcResolver>().As<IResolver>();
			builder.RegisterType<MvcContext>().As<IContext>().InstancePerHttpRequest();

			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}

		private static IDocumentSession CreateDocumentSession()
		{
			var session = _documentStore.OpenSession();
			session.Advanced.AllowNonAuthoritativeInformation = false;
			return session;
		}
	}
}
