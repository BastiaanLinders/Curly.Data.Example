using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Abstractions;
using Data.Entities;
using Data.Entities.Mapping;
using Dtos;
using Dtos.Commands;
using Microsoft.EntityFrameworkCore;
using Store.Abstractions;

namespace Store.Services
{
	public class ProductService : IProductService
	{
		private readonly IStoreContext _storeContext;

		public ProductService(IStoreContext storeContext)
		{
			_storeContext = storeContext;
		}

		public async Task<IReadOnlyCollection<ProductSummaryDto>> GetAll()
		{
			return await _storeContext.Products
			                          .Project<Product, ProductSummaryDto>()
			                          .ToListAsync();
		}

		public async Task<ProductDto> GetById(int id)
		{
			return await _storeContext.Products
			                          .Project<Product, ProductDto>()
									  .SingleOrDefaultAsync(p => p.Id == id);
		}

		public Task CreateProduct(CreateProductCommand command)
		{
			var product = new Product
			              {
				              Title = command.Title
			              };

			_storeContext.Products.Add(product);

			return Task.CompletedTask;
		}


		public async Task DeleteProduct(DeleteProductCommand command)
		{
			var product = await _storeContext.Products.FindAsync(command.Id);
			if (product == null) return;

			_storeContext.Products.Remove(product);
		}
	}
}