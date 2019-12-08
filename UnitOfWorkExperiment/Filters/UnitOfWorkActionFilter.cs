using System.Threading.Tasks;
using Data.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UnitOfWorkExperiment.Filters
{
	public class UnitOfWorkActionFilter : IAsyncActionFilter
	{
		private readonly IUnitOfWork _unitOfWork;

		public UnitOfWorkActionFilter(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var resultContext = await next();

			if (resultContext.Exception != null && !resultContext.ExceptionHandled)
			{
				await _unitOfWork.Rollback();
			}
			else
			{
				await _unitOfWork.Commit();
			}
		}
	}
}