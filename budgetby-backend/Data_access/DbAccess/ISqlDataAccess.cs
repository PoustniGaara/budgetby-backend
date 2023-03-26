using System.Data;

namespace DataAccess.DbAccess
{
	public interface ISqlDataAccess
	{
		Task<IEnumerable<T>> LoadData<T, U>(
			string storedProcedure,
			U parameters,
			IDbTransaction transaction = null,
			string connectionId = "Default");
		Task SaveData<T>(
			string storedProcedure,
			T parameters,
			IDbTransaction transaction = null,
			string connectionId = "Default");
		IDbConnection GetDbConnection(string connectionId = "Default");

	}
}