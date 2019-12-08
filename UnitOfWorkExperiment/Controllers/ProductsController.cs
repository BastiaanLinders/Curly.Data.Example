using System.Collections.Generic;
using System.Threading.Tasks;
using Dtos;
using Dtos.Commands;
using Microsoft.AspNetCore.Mvc;
using Store.Abstractions;

namespace UnitOfWorkExperiment.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
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
			await _productService.DeleteProduct(command);
		}
	}
}