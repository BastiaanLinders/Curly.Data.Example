using Microsoft.Extensions.DependencyInjection;
using Utilities;

namespace Store.Services
{
	public class StoreServicesModule : ServiceModuleBase
	{
		public override void RegisterServices(IServiceCollection serviceCollection)
		{
			serviceCollection.AddTransientForAllImplementedInterfaces<ProductService>();
		}
	}
}