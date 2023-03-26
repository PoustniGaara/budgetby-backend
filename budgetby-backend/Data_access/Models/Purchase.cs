namespace DataAccess.Models;

public class Purchase
{
	public int Id { get; set; }
	public int Amount { get; set; }
	public decimal Price { get; set; }
	public DateTime Date { get; set; } //The Date is to be mapped to id. Mapping will be done by cache at repository layer.
	public int SheetID { get; set; }
	public Supplier Supplier { get; set; }
	public Product Product { get; set; }

}
