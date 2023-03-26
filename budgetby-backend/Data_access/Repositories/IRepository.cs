using DataAccess.Models;

namespace DataAccess.Repositories
{
	public interface IRepository<T>
	{
		Task DeleteAsync(int id);
		Task<T?> GetByIdAsync(int id);
		Task<IEnumerable<T>> GetAllAsync();
		Task InsertAsync(T spendSheet); // I need to return the ID 
		Task UpdateAsync(T spendSheet);
	}
}