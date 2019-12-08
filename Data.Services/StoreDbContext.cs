using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Services
{
	public class StoreDbContext : DbContext
	{
		private readonly ILogger<StoreDbContext> _logger;

		public StoreDbContext(ILogger<StoreDbContext> logger,
		                      DbContextOptions options) : base(options)
		{
			_logger = logger;
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Article> Articles { get; set; }
		public DbSet<Location> Locations { get; set; }
		public DbSet<Stock> Stock { get; set; }

		public Task Rollback()
		{
			_logger.LogDebug(ChangeTracker.HasChanges()
				                 ? $"Rollback was called on context with {ChangeTracker.Entries().Count()} pending changes."
				                 : "Rollback was called on context without pending changes.");

			return Task.CompletedTask;
		}

		public async Task Commit()
		{
			if (!ChangeTracker.HasChanges())
			{
				_logger.LogDebug("Commit was called on context without pending changes.");
				return;
			}

			_logger.LogInformation($"Commit was called on context with {ChangeTracker.Entries().Count()} pending changes.");

			await SaveChangesAsync();
		}
	}
}