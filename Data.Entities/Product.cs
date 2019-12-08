using System.Collections.Generic;

namespace Data.Entities
{
	public class Product
	{
		public int Id { get; set; }
		public string Title { get; set; }

		public virtual ICollection<Article> Articles { get; set; }
	}
}