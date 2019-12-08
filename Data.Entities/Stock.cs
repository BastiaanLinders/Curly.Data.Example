namespace Data.Entities
{
	public class Stock
	{
		public int Id { get; set; }

		public int LocationId { get; set; }
		public int ArticleId { get; set; }

		public int Count { get; set; }

		public virtual Location Location { get; set; }
		public virtual Article Article { get; set; }
	}
}