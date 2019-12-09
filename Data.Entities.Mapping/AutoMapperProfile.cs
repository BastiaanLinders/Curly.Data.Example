using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Dtos;

namespace Data.Entities.Mapping
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			Map<Product, ProductDto>(product => new ProductDto
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

			Map<Product, ProductSummaryDto>(product => new ProductSummaryDto
			                                           {
				                                           Id = product.Id,
				                                           Title = product.Title
			                                           });

			Map<Article, ArticleDto>(article => new ArticleDto
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

			Map<Article, ArticleSummaryDto>(article => new ArticleSummaryDto
			                                           {
				                                           Id = article.Id,
				                                           Title = article.Title,
				                                           TotalStock = article.Stock.Sum(s => s.Count)
			                                           });

			Map<Stock, StockSummaryDto>(stock => new StockSummaryDto
			                                     {
				                                     Location = stock.Location.Name,
				                                     Count = stock.Count
			                                     });
		}

		private void Map<TFrom, TTo>(Expression<Func<TFrom, TTo>> projectionExpression)
		{
			CreateMap<TFrom, TTo>().ConvertUsing(projectionExpression);
		}
	}
}