using System.Linq;
using System.Threading.Tasks;
using Data.Abstractions;
using Dtos.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
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

	public class CommandPopulatorFilter : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (context.ActionDescriptor.Parameters.Any(p => typeof(IObjectCommand).IsAssignableFrom(p.ParameterType)))
			{
				var idFromUri = int.Parse((string) context.RouteData.Values["id"]);
				var objectCommands = context.ActionDescriptor.Parameters.Where(p => typeof(IObjectCommand).IsAssignableFrom(p.ParameterType));
				foreach (ParameterDescriptor parameterDescriptor in objectCommands)
				{
					var command = (IObjectCommand) context.ActionArguments[parameterDescriptor.Name];
					if (command == null)
					{
						context.Result = new BadRequestResult();
						return;
					}

					if (command.Id != default && command.Id != idFromUri)
					{
						context.Result = new BadRequestResult();
						return;
					}

					if (command.Id == default)
					{
						command.Id = idFromUri;
					}
				}
			}

			await next();
		}
	}
}