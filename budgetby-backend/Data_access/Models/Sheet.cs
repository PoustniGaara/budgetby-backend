namespace DataAccess.Models
{
	public class Sheet
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public decimal Total { get; set; }
		public IEnumerable<Purchase> Purchases { get; set; } 
		public Sheet()
		{

		}
	}
}
