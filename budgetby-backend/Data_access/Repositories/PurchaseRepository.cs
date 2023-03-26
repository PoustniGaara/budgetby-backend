using DataAccess.DbAccess;
using DataAccess.Models;
using System.Data;

namespace DataAccess.Repositories;

public class PurchaseRepository : IRepository<Purchase>
{
	private readonly ISqlDataAccess _db;
	private readonly Func<IDbTransaction> _getTransaction;

	public PurchaseRepository(ISqlDataAccess db, Func<IDbTransaction> getTransaction)
	{
		_db = db;
		_getTransaction = getTransaction;

	}

	public async Task InsertAsync(Purchase purchase)
	{
		await _db.SaveData(
			storedProcedure: "spPurchase_Insert",
			new { purchase.Date, purchase.Price },
			transaction: _getTransaction());
	}

	public async Task<IEnumerable<Purchase>> GetAllAsync() =>
		await _db.LoadData<Purchase, dynamic>(storedProcedure: "spPurchase_ReadAll", new { }); //*1

	public async Task<Purchase?> GetByIdAsync(int id)
	{
		var results = await _db.LoadData<Purchase, dynamic>(
			storedProcedure: "spPurchase_Read",
			new { Id = id });
		return results.FirstOrDefault(); //*2
	}

	public async Task UpdateAsync(Purchase spendPurchase)
	{
		await _db.SaveData(storedProcedure: "spPurchase_Update", spendPurchase);
	}
		

	public async Task DeleteAsync(int id)
	{
		await _db.SaveData(storedProcedure: "spPurchase_Delete", new { Id = id });
	}

}


//NOTES:
//	*1 When a variable is declared as dynamic, the C# compiler does not perform any compile-time type checking on that variable. 
//	Instead, the type of the variable is determined at runtime based on the type of the object that is assigned to it.
//	dynamic variable = "Hello, world!";
//	Console.WriteLine(variable);
//	variable = 42;
//	Console.WriteLine(variable);

//*2 FirstOrDefault() will return the value or default value and in this case it is a null
