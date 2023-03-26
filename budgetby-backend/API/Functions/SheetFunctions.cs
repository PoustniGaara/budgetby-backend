using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DataAccess.Repositories;
using DataAccess.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace API.functions;

public class SheetFunctions
{

	private readonly IRepository<Sheet> _sheetRepository;

	public SheetFunctions(IRepository<Sheet> sheetRepository)
	{
		_sheetRepository = sheetRepository;
	}

	[FunctionName("GetSheets")]
	public async Task<IActionResult> GetAllSheets(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "sheets")] HttpRequest req,
		ILogger log)
	{
		log.LogInformation("Get all sheets function triggered");

		try
		{
			var sheets = await _sheetRepository.GetAllAsync();
			/// Add mapping to DTO 
			return new OkObjectResult(sheets);
		}
		catch (Exception ex)
		{
			// Add proper exception handling
			log.LogError(ex, "Error fetching sheets.");
			return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
		}
	}

	[FunctionName("GetSheet")]
	public async Task<IActionResult> GetSheet(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "sheets/{id}")] HttpRequest req,
		int id,
		ILogger log)
	{
		log.LogInformation("Get sheet by id function triggered");

		try
		{
			var sheet = await _sheetRepository.GetByIdAsync(id);

			if(sheet == null)
			{
				return new NotFoundResult();
			}
			/// Add mapping to DTO 
			return new OkObjectResult(sheet);
		}
		catch (Exception ex)
		{
			// Add proper exception handling
			log.LogError(ex, "Error fetching people.");
			return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
		}
	}

	//[FunctionName("AddSheet")]
	//public async Task<IActionResult> AddSheet(
	//		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "sheets")] HttpRequestMessage req,
	//		ILogger log)
	//{
	//	log.LogInformation("AddSheet function triggered.");

	//	try
	//	{
	//		var sheet = await req.Content.ReadAsAsync<Sheet>();
	//		var addedPerson = await _sheetRepository.InsertAsync(sheet);
	//		return new CreatedResult($"/api/people/{addedPerson.Id}", addedPerson);
	//	}
	//	catch (Exception ex)
	//	{
	//		log.LogError(ex, "Error adding person.");
	//		return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
	//	}
	//}

	//[FunctionName("UpdateSheet")]
	//public async Task<IActionResult> UpdateSheet(
	//[HttpTrigger(AuthorizationLevel.Function, "put", Route = "sheets/{id}")] HttpRequestMessage req,
	//int id,
	//ILogger log)
	//{
	//	log.LogInformation($"UpdateSheet function triggered with id: {id}");

	//	try
	//	{
	//		var sheet = await req.Content.ReadAsAsync<Sheet>();
	//		sheet.Id = id;
	//		var updatedPerson = await _sheetRepository.UpdateAsync(sheet);

	//		if (updatedPerson == null)
	//		{
	//			return new NotFoundResult();
	//		}

	//		return new OkObjectResult(updatedPerson);
	//	}
	//	catch (Exception ex)
	//	{
	//		log.LogError(ex, $"Error updating person with id: {id}");
	//		return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
	//	}
	//}

	[FunctionName("DeleteSheet")]
	public async Task<IActionResult> DeleteSheet(
	[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "sheets/{id}")] HttpRequestMessage req,
	int id,
	ILogger log)
	{
		log.LogInformation($"DeleteSheet function triggered with id: {id}");

		try
		{
			var sheet = await _sheetRepository.GetByIdAsync(id);

			if (sheet == null)
			{
				return new NotFoundResult();
			}

			await _sheetRepository.DeleteAsync(id);
			return new NoContentResult();
		}
		catch (Exception ex)
		{
			log.LogError(ex, $"Error deleting person with id: {id}");
			return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
		}
	}
}

