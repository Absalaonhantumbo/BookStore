using Aplication.Dtos;
using Application.Dtos;
using AutoMapper;
using Domain;

namespace Application.Helpers.MappingProfiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
      
      CreateMap<User,UserDto>();
      CreateMap<Supplier, SuppliersDto>()
          .ForMember(dest => dest.CompanyType, opt =>
              opt.MapFrom(src => src.CompanyType.Description))
          .ForMember(dest => dest.Country, opt => 
              opt.MapFrom(src => src.Country.Name))
          .ForMember(dest => dest.SupplierType, opt =>
              opt.MapFrom(src => src.SupplierType.Description));
    }
}