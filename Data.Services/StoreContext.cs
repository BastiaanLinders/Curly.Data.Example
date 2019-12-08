using Data.Abstractions;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Services
{
	public class StoreContext : IStoreContext
	{
		private readonly StoreDbContext _dbContext;

		public StoreContext(StoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public DbSet<Product> Products => _dbContext.Products;
		public DbSet<Article> Articles => _dbContext.Articles;
		public DbSet<Location> Locations => _dbContext.Locations;
		public DbSet<Stock> Stock => _dbContext.Stock;
	}
}