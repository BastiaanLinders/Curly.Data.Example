using System.Collections.Generic;

namespace Dtos
{
	public class ProductDto
	{
		public int Id { get; set; }
		public string Title { get; set; }

		public virtual IEnumerable<ArticleSummaryDto> Articles { get; set; }
	}
}