using Dapper;
using MySqlConnector;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccess.DbAccess;

public class SqlDataAccess : ISqlDataAccess
{
	private readonly IConfiguration _config;

	public SqlDataAccess(IConfiguration config)
	{
		_config = config;

	}

	public async Task<IEnumerable<T>> LoadData<T, U>(
		string storedProcedure,
		U parameters,
		IDbTransaction transaction = null,
		string connectionId = "Default")
	{
		using MySqlConnection connection = transaction
			== null ? new MySqlConnection(_config.GetConnectionString(connectionId)) : (MySqlConnection)transaction.Connection;
		return await connection.QueryAsync<T>(
			storedProcedure,
			parameters,
			transaction: transaction,
			commandType: CommandType.StoredProcedure);
	}

	public async Task SaveData<T>(
		string storedProcedure,
		T parameters,
		IDbTransaction transaction = null,
		string connectionId = "Default")
	{
		using MySqlConnection connection = transaction
			== null ? new MySqlConnection(_config.GetConnectionString(connectionId)) : (MySqlConnection)transaction.Connection;
		await connection.ExecuteAsync(
			storedProcedure,
			parameters,
			transaction: transaction,
			commandType: CommandType.StoredProcedure);
	}

	public IDbConnection GetDbConnection(string connectionId = "Default")
	{
		return new MySqlConnection(_config.GetConnectionString(connectionId));
	}
}

//NOTES:
//*1 using the 'using' keyword make resource to be released after the end of the scope
//it was defined. 'using' statement can be used with 'IDisposable' interface. 'using' is commonly
//	used with objects that access I/O
