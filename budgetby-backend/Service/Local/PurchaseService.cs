using DataAccess.Models;
using DataAccess.Repositories;

namespace Services.Local;

public class PurchaseService : IPurchaseService
{
	private readonly IRepository<Purchase> _purchaseRepository;

	public PurchaseService(IRepository<Purchase> purchaseRepository)
	{
		_purchaseRepository = purchaseRepository;
	}

	public async Task InsertAsync(IEnumerable<Purchase> purchases)
	{
		//insert new Supplier or Get Supplier ID  
		//insert new Product or Get Product ID  
		//Insert purchases 
	}

	public async Task<IEnumerable<Purchase>> GetByUserAsync(int userId)
	{
		return null;
	}

	public async Task UpdateAsync(IEnumerable<Purchase> purchases)
	{

	}

	public async Task DeleteAsync(IEnumerable<int> purchasesIds)
	{

	}
}
