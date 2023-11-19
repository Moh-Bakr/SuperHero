using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SuperHero.BAL.Dtos;
using SuperHero.DAL;

namespace SuperHero.BAL;

public class MappingProfile : Profile
{
   public MappingProfile()
   {
      CreateMap<RegisterDto, ApplicationUser>();
      CreateMap<CreateFavoriteListDto, FavoriteListModel>();
      CreateMap<FavoriteListModel, ReadFavoriteListDto>();
   }
}
