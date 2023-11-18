using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SuperHero.BAL.Dtos;

namespace SuperHero.BAL;

public class MappingProfile : Profile
{
   public MappingProfile()
   {
      CreateMap<RegisterDto, IdentityUser>();
   }
}
