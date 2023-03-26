using System;

namespace API.DTOs;

public class PurchasePostDTO
{
	//Sheet properties
	public int SheetID { get; set; }

	//Supplier properties
	public string? SupplierName { get; set; }
	public string? Town { get; set; }
	public int? Zipcode { get; set; }
	public string? Street { get; set; }

	//Purchase properties
	public decimal Price { get; set; }
	public int Amount { get; set; }
	public DateTime Date { get; set; }

	//Product properties
	public string ProductName { get; set; }





}
