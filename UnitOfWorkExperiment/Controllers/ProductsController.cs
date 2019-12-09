using System.Collections.Generic;
using System.Threading.Tasks;
using Dtos;
using Dtos.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Abstractions;

namespace UnitOfWorkExperiment.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly ILogger<ProductsController> _logger;
		private readonly IProductService _productService;

		public ProductsController(ILogger<ProductsController> logger,
		                          IProductService productService)
		{
			_logger = logger;
			_productService = productService;
		}

		[HttpGet]
		public async Task<IEnumerable<ProductSummaryDto>> Get()
		{
			return await _productService.GetAll();
		}

		[HttpGet("{id}")]
		public async Task<ProductDto> Get(int id)
		{
			return await _productService.GetById(id);
		}

		[HttpPost]
		public async Task Add(CreateProductCommand command)
		{
			await _productService.CreateProduct(command);
		}

		[HttpDelete("{id}")]
		public async Task Remove(int id, DeleteProductCommand command)
		{
			_logger.LogInformation($"id = {id} || command.id = {command.Id}");
			await _productService.DeleteProduct(command);
		}
	}
}