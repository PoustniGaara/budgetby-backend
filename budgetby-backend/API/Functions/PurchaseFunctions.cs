using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Collections.Generic;
using API.DTOs;
using System.Linq;
using AutoMapper;
using Services.Local;

namespace API.Functions;

public class PurchaseFunctions
{
	private readonly IMapper _mapper;
	private readonly IPurchaseService _purchaseService;

	public PurchaseFunctions(IMapper mapper, IPurchaseService purchaseService)
	{
		_mapper = mapper;
		_purchaseService = purchaseService;
	}

	[FunctionName("InsertPurchases")]
	public async Task<IActionResult> InsertAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "purchases")] HttpRequestMessage req,
		ILogger log)
	{
		log.LogInformation("InsertPurchases function triggered.");
		try
		{
			//read request
			log.LogInformation("InsertPurchases request reading started.");
			IEnumerable<PurchasePostDTO> purchasesDto = await req.Content.ReadAsAsync<List<PurchasePostDTO>>();

			//map from dto to model
			log.LogInformation("InsertPurchases mapping started.");
			IEnumerable<Purchase> purchases = purchasesDto.Select(purchasePostDto => _mapper.Map<Purchase>(purchasePostDto));

			//run insert
			log.LogInformation("InsertPurchases service method started.");
			await _purchaseService.InsertAsync(purchases);

			log.LogInformation("InsertPurchases successfuly finished.");
			return new OkResult();
		}
		catch (Exception ex)
		{
			log.LogError(ex, "Error adding person.");
			return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
		}
	}

	[FunctionName("GetPurchasesByUser")]
	public async Task<IActionResult> GetByUserAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "purchases/{id}")] HttpRequestMessage req,
		ILogger log)
	{
		log.LogInformation("GetPurchasesByUser function triggered");

		try
		{
			//read request
			log.LogInformation("GetPurchasesByUser reading started.");
			int userId = await req.Content.ReadAsAsync<int>();

			//run service
			log.LogInformation("GetPurchasesByUser service method started.");
			IEnumerable<Purchase> purchases = await _purchaseService.GetByUserAsync(userId);

			//map model to DTO
			log.LogInformation("GetPurchasesByUser mapping started.");
			//IEnumerable<PurchasePostDto> purchases = purchasesDto.Select(purchasePostDto => _mapper.Map<Purchase>(purchasePostDto));

			/// Add mapping to DTO 
			return new OkObjectResult(purchases);
		}
		catch (Exception ex)
		{
			// Add proper exception handling
			log.LogError(ex, "Error fetching purchases.");
			return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
		}
	}

	//[FunctionName("UpdatePurchase")]
	//public async Task<IActionResult> UpdatePurchase(
	//[HttpTrigger(AuthorizationLevel.Function, "put", Route = "Purchases/{id}")] HttpRequestMessage req,
	//int id,
	//ILogger log)
	//{
	//	log.LogInformation($"UpdatePurchase function triggered with id: {id}");

	//	try
	//	{
	//		var Purchase = await req.Content.ReadAsAsync<Purchase>();
	//		Purchase.Id = id;
	//		var updatedPerson = await _PurchaseRepository.UpdateAsync(Purchase);

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

	[FunctionName("DeletePurchases")]
	public async Task<IActionResult> DeletePurchase(
	[HttpTrigger(AuthorizationLevel.Function, "delete", Route = "purchases/{id}")] HttpRequestMessage req,
	int id,
	ILogger log)
	{
		log.LogInformation($"DeletePurchase function triggered with id: {id}");

		try
		{
			var Purchase = await _purchaseRepository.GetByIdAsync(id);

			if (Purchase == null)
			{
				return new NotFoundResult();
			}

			await _purchaseRepository.DeleteAsync(id);
			return new NoContentResult();
		}
		catch (Exception ex)
		{
			log.LogError(ex, $"Error deleting person with id: {id}");
			return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
		}
	}


}
