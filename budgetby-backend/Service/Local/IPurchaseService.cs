using DataAccess.Models;

namespace Services.Local
{
	public interface IPurchaseService
	{
		Task DeleteAsync(IEnumerable<int> purchasesIds);
		Task<IEnumerable<Purchase>> GetByUserAsync(int userId);
		Task InsertAsync(IEnumerable<Purchase> purchases);
		Task UpdateAsync(IEnumerable<Purchase> purchases);
	}
}