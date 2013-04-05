using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;
using Travellers.Core.Events;
using Travellers.Infrastructure;
using Autofac;
using Travellers.Infrastructure.EventHandlers;
using Travellers.Infrastructure.EventPublisher;
using Travellers.Infrastructure.EventStore;

namespace Travellers.EventReplayer
{
	class Program
	{
		private static DocumentStore _documentStore;

		static void Main(string[] args)
		{
			_documentStore = new DocumentStore {ConnectionStringName = "RavenDB"};
			_documentStore.Initialize();

			var container = ConfigureContainer();

			var eventStore = container.Resolve<IEventStore>();
			var eventPublisher = container.Resolve<IEventPublisher>();

			Console.WriteLine("Press ENTER to replay all events...");
			Console.ReadLine();

			foreach (var evt in eventStore.LoadAllEvents())
			{
				eventPublisher.Publish(evt);
				Console.WriteLine("Published {0}", evt.GetType().Name);
			}

			Console.WriteLine("Done...");
			Console.ReadLine();
		}

		private static IContainer ConfigureContainer()
		{
			var builder = new ContainerBuilder();

			builder.Register(c => _documentStore.OpenSession()).As<IDocumentSession>();
			builder.RegisterType<AutofacResolver>().As<IResolver>();
			builder.RegisterType<SqlEventStore>().As<IEventStore>();
			builder.RegisterType<EventPublisher>().As<IEventPublisher>();
			builder.RegisterType<JsonEventSerializer>().As<IEventSerializer>();

			builder.RegisterAssemblyTypes(typeof (TravellerCreatedHandler).Assembly)
				.InNamespaceOf<TravellerCreatedHandler>()
				.AsImplementedInterfaces();

			return builder.Build();
		}
	}

	public class AutofacResolver : IResolver
	{
		private readonly IComponentContext _context;

		public AutofacResolver(IComponentContext context)
		{
			_context = context;
		}

		public object Resolve(Type type)
		{
			return _context.Resolve(type);
		}

		public T Resolve<T>()
		{
			return _context.Resolve<T>();
		}

		public IEnumerable<object> ResolveAll(Type type)
		{
			var enumerableType = typeof(IEnumerable<>).MakeGenericType(type);
			var instance = _context.Resolve(enumerableType);
			return ((IEnumerable)instance).Cast<object>();
		}
	}
}
