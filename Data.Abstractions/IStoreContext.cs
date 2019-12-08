using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Abstractions
{
	public interface IStoreContext
	{
		DbSet<Product> Products { get; }
		DbSet<Article> Articles { get; }
		DbSet<Location> Locations { get; }
		DbSet<Stock> Stock { get; }
	}
}