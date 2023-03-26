using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.UnitOfWork;

namespace DataAccess.Repositories;

public class SupplierRepository : IRepository<Supplier>
{
	private readonly ISqlDataAccess _db;
	private readonly IUnitOfWork _unitOfWork;

	public SupplierRepository(ISqlDataAccess db, IUnitOfWork unitOfWork)
	{
		_db = db;
		_unitOfWork = unitOfWork;
	}

	public async Task<IEnumerable<Supplier>> GetAllAsync() =>
		await _db.LoadData<Supplier, dynamic>(storedProcedure: "spSupplier_ReadAll", new { }); //*1

	public async Task<Supplier?> GetByIdAsync(int id)
	{
		var results = await _db.LoadData<Supplier, dynamic>(
			storedProcedure: "spSupplier_Read",
			new { Id = id });
		return results.FirstOrDefault(); //*2
	}

	public async Task InsertAsync(Supplier spendSupplier)
	{
		//await _db.SaveData(storedProcedure: "", new { spendSupplier.Date, spendSupplier.Suppliers }); // To finish
		await _unitOfWork.SaveChangesAsync();
	}
		

	public async Task UpdateAsync(Supplier spendSupplier)
	{
		await _unitOfWork.SaveChangesAsync();
		await _db.SaveData(storedProcedure: "spSupplier_Update", spendSupplier);
	}
		

	public async Task DeleteAsync(int id)
	{
		await _unitOfWork.SaveChangesAsync();
		await _db.SaveData(storedProcedure: "spSupplier_Delete", new { Id = id });
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
