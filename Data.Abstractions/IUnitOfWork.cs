using System.Threading.Tasks;

namespace Data.Abstractions
{
	public interface IUnitOfWork
	{
		Task Rollback();
		Task Commit();
	}
}