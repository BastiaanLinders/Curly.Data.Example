using System.Threading.Tasks;
using Data.Abstractions;

namespace Data.Services
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreDbContext _dbContext;

		public UnitOfWork(StoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Rollback()
		{
			await _dbContext.Rollback();
		}

		public async Task Commit()
		{
			await _dbContext.Commit();
		}
	}
}