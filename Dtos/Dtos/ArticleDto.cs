using System.Collections.Generic;

namespace Dtos
{
	public class ArticleDto
	{
		public int Id { get; set; }
		public int ProductId { get; set; }

		public string ProductTitle { get; set; }
		public string Title { get; set; }

		public int TotalStock { get; set; }

		public IEnumerable<StockSummaryDto> Stock { get; set; }
	}
}