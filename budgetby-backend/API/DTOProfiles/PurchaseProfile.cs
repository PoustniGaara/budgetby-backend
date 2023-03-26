using API.DTOs;
using AutoMapper;
using DataAccess.Models;

namespace API.DTOProfiles;

public class PurchaseProfile : Profile
{
    public PurchaseProfile()
    {
		CreateMap<Purchase, PurchasePostDTO>()
				.ForMember(dest => dest.ProductName, act => act.MapFrom(src => src.Product.Name))
				.ForMember(dest => dest.SupplierName, act => act.MapFrom(src => src.Supplier.Name))
				.ForMember(dest => dest.Town, act => act.MapFrom(src => src.Supplier.Address.Town))
				.ForMember(dest => dest.Zipcode, act => act.MapFrom(src => src.Supplier.Address.Zipcode))
				.ForMember(dest => dest.Street, act => act.MapFrom(src => src.Supplier.Address.Street))
				.ReverseMap();
	}
}
