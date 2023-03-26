using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.UnitOfWork;

namespace DataAccess.Repositories;

public class ProductRepository : IRepository<Product>
{
	private readonly ISqlDataAccess _db;
	private readonly IUnitOfWork _unitOfWork;

	public ProductRepository(ISqlDataAccess db, IUnitOfWork unitOfWork)
	{
		_db = db;
		_unitOfWork = unitOfWork;
	}

	public async Task<IEnumerable<Product>> GetAllAsync() =>
		await _db.LoadData<Product, dynamic>(storedProcedure: "spProduct_ReadAll", new { }); //*1

	public async Task<Product?> GetByIdAsync(int id)
	{
		var results = await _db.LoadData<Product, dynamic>(
			storedProcedure: "spProduct_Read",
			new { Id = id });
		return results.FirstOrDefault(); //*2
	}

	public async Task InsertAsync(Product spendProduct)
	{
		//await _db.SaveData(storedProcedure: "", new { spendProduct.Date, spendProduct.Products }); // TO FINISH
		await _unitOfWork.SaveChangesAsync();
	}
		

	public async Task UpdateAsync(Product spendProduct)
	{
		await _unitOfWork.SaveChangesAsync();
		await _db.SaveData(storedProcedure: "spProduct_Update", spendProduct);
	}
		

	public async Task DeleteAsync(int id)
	{
		await _unitOfWork.SaveChangesAsync();
		await _db.SaveData(storedProcedure: "spProduct_Delete", new { Id = id });
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
