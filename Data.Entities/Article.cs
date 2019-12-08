using System.Collections.Generic;

namespace Data.Entities
{
	public class Article
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string Title { get; set; }

		public virtual Product Product { get; set; }
		public virtual ICollection<Stock> Stock { get; set; }
	}
}