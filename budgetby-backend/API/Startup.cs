using AutoMapper;
using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using DataAccess.UnitOfWork;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Services.Local;
using System.Data;
using System.Data.SqlClient;

[assembly: FunctionsStartup(typeof(API.Startup))]

namespace API
{
	internal class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			// Register AutoMapper with DI
			builder.Services.AddAutoMapper(typeof(Startup));

			//Services injections
			builder.Services.AddScoped<IPurchaseService, PurchaseService>();

			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<ISqlDataAccess, SqlDataAccess>(); 
			builder.Services.AddScoped<IRepository<Sheet>, SheetRepository>();

		}
	}
}
 