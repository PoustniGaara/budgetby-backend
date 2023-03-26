using DataAccess.Models;
using DataAccess.Repositories;
using System.Data;

namespace DataAccess.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
	IRepository<Purchase> PurchaseRepository { get; }
	IRepository<Product> ProductRepository { get; }
	IRepository<Supplier> SupplierRepository { get; }

	IDbTransaction BeginTransaction();
	void CommitTransaction();
	void RollbackTransaction();
}