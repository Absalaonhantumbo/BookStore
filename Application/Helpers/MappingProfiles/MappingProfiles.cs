using Aplication.Dtos;
using AutoMapper;
using Domain;

namespace Aplication.Helpers.MappingProfiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
      
      CreateMap<User,UserDto>();

    }
}