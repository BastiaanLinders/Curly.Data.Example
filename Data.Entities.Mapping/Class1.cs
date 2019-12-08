using System.Linq;
using AutoMapper;
using Dtos;

namespace Data.Entities.Mapping
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Product, ProductDto>()
				.ConvertUsing(product => new ProductDto
				                         {
					                         Id = product.Id,
					                         Title = product.Title,
					                         Articles = product.Articles.Select(article => new ArticleSummaryDto
					                                                                       {
						                                                                       Id = article.Id,
						                                                                       Title = article.Title,
						                                                                       TotalStock = article.Stock.Sum(s => s.Count)
					                                                                       })
				                         });

			CreateMap<Product, ProductSummaryDto>()
				.ConvertUsing(product => new ProductSummaryDto
				                         {
					                         Id = product.Id,
					                         Title = product.Title
				                         });

			CreateMap<Article, ArticleDto>()
				.ConvertUsing(article => new ArticleDto
				                         {
					                         Id = article.Id,
					                         ProductId = article.ProductId,
					                         Title = article.Title,
					                         ProductTitle = article.Product.Title,
					                         TotalStock = article.Stock.Sum(stock => stock.Count),
					                         Stock = article.Stock.Select(stock => new StockSummaryDto
					                                                               {
						                                                               Location = stock.Location.Name,
						                                                               Count = stock.Count
					                                                               })
				                         });

			CreateMap<Article, ArticleSummaryDto>()
				.ConvertUsing(article => new ArticleSummaryDto
				                         {
					                         Id = article.Id,
					                         Title = article.Title,
					                         TotalStock = article.Stock.Sum(s => s.Count)
				                         });

			CreateMap<Stock, StockSummaryDto>()
				.ConvertUsing(stock => new StockSummaryDto
				                       {
					                       Location = stock.Location.Name,
					                       Count = stock.Count
				                       });
		}
	}
}