using DataAccess.Models;
using DataAccess.Repositories;
using System.Data;
using System.Data.Common;

namespace DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
	private readonly IDbConnection _db;
	private IDbTransaction _transaction;

	public IRepository<Purchase> PurchaseRepository { get; private set; }
	public IRepository<Product> ProductRepository { get; private set; }
	public IRepository<Supplier> SupplierRepository { get; private set; }

	public UnitOfWork(
		IDbConnection connection,
		IRepository<Purchase> purchaseRepository,
		IRepository<Product> productRepository,
		IRepository<Supplier> supplierRepository)
	{
		_db = connection;
		PurchaseRepository = purchaseRepository;
		ProductRepository = productRepository;
		SupplierRepository = supplierRepository;
	}

	public IDbTransaction BeginTransaction()
	{
		_transaction = _db.BeginTransaction();
		return _transaction;
	}

	public void CommitTransaction()
	{
		_transaction.Commit();
		_transaction = null;
	}

	public void RollbackTransaction()
	{
		_transaction.Rollback();
		_transaction = null;
	}
	public void Dispose()
	{
		_db.Dispose();
	}
}
