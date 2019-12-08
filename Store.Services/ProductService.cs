using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Data.Abstractions;
using Data.Entities;
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
			                          .Select(p => new ProductSummaryDto
			                          {
				                          Id = p.Id,
				                          Title = p.Title
			                          })
			                          .ToListAsync();
		}

		public async Task<ProductDto> GetById(int id)
		{
			return await _storeContext.Products
			                          .Select(p => new ProductDto
			                          {
				                          Id = p.Id,
				                          Title = p.Title,
				                          Articles = p.Articles.Select(a => new ArticleSummaryDto
				                          {
					                          Id = a.Id,
					                          Title = a.Title,
					                          TotalStock = a.Stock.Sum(s => s.Count)
				                          })
			                          })
			                          .SingleOrDefaultAsync(p => p.Id == id);
		}

		public Task CreateProduct(CreateProductCommand command)
		{
			var product = new Product
			{
				Title = command.Title
			};

			_storeContext.Products.Add(product);

			//throw new TransactionInDoubtException("Bla bla bla!");

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