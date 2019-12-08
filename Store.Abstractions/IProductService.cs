using System.Collections.Generic;
using System.Threading.Tasks;
using Dtos;
using Dtos.Commands;

namespace Store.Abstractions
{
	public interface IProductService
	{
		Task<IReadOnlyCollection<ProductSummaryDto>> GetAll();
		Task<ProductDto> GetById(int id);

		Task CreateProduct(CreateProductCommand command);
		Task DeleteProduct(DeleteProductCommand command);
	}
}