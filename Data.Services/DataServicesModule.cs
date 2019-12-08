using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utilities;

namespace Data.Services
{
	public class DataServicesModule : ConfiguredServiceModuleBase
	{
		public override void RegisterServices(IServiceCollection serviceCollection, IConfiguration configuration)
		{
			serviceCollection.AddDbContext<StoreDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("StoreContext")));

			serviceCollection.AddScopedForAllImplementedInterfaces<UnitOfWork>();
			serviceCollection.AddScopedForAllImplementedInterfaces<StoreContext>();
		}
	}
}