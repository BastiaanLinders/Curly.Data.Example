using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Utilities
{
	public static class ServiceRegistrationExtensions
	{
		public static void AddModule<T>(this IServiceCollection serviceCollection) where T : ServiceModuleBase, new()
		{
			new T().RegisterServices(serviceCollection);
		}

		public static void AddModule<T>(this IServiceCollection serviceCollection, IConfiguration configuration) where T : ConfiguredServiceModuleBase, new()
		{
			new T().RegisterServices(serviceCollection, configuration);
		}

		public static IServiceCollection AddTransientForAllImplementedInterfaces<T>(this IServiceCollection serviceCollection)
		{
			RegisterForAllImplementedInterfaces(typeof(T), serviceCollection.AddTransient);
			return serviceCollection;
		}

		public static IServiceCollection AddSingletonForAllImplementedInterfaces<T>(this IServiceCollection serviceCollection)
		{
			RegisterForAllImplementedInterfaces(typeof(T), serviceCollection.AddSingleton);
			return serviceCollection;
		}

		public static IServiceCollection AddScopedForAllImplementedInterfaces<T>(this IServiceCollection serviceCollection)
		{
			RegisterForAllImplementedInterfaces(typeof(T), serviceCollection.AddScoped);
			return serviceCollection;
		}

		private static void RegisterForAllImplementedInterfaces(Type typeToRegister, Func<Type, Type, IServiceCollection> scopedRegistration)
		{
			var implementedInterfaces = typeToRegister.GetInterfaces();
			foreach (var implementedInterface in implementedInterfaces)
				scopedRegistration.Invoke(implementedInterface, typeToRegister);
		}
	}

	public abstract class ServiceModuleBase
	{
		public abstract void RegisterServices(IServiceCollection serviceCollection);
	}

	public abstract class ConfiguredServiceModuleBase
	{
		public abstract void RegisterServices(IServiceCollection serviceCollection, IConfiguration configuration);
	}
}