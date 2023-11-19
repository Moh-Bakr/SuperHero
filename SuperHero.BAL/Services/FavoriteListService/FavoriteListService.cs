using AutoMapper;
using FluentValidation;
using SuperHero.DAL;
using SuperHero.Helper;

namespace SuperHero.BAL;

public class FavoriteListService : IFavoriteListService
{
   private readonly ICrudRepository<FavoriteListModel> _crudRepository;
   private readonly IValidator<CreateFavoriteListDto> _validator;
   private readonly IMapper _mapper;

   public FavoriteListService(ICrudRepository<FavoriteListModel> crudRepository,
      IValidator<CreateFavoriteListDto> validator,
      IMapper mapper)
   {
      _crudRepository = crudRepository;
      _validator = validator;
      _mapper = mapper;
   }

   public async Task<String> AddAsync(CreateFavoriteListDto createFavoriteListDto, string userId)
   {
      var validationResult = await _validator.ValidateAsync(createFavoriteListDto);
      if (!validationResult.IsValid)
      {
         var validationErrorJson = ResponseHelper.FluentValidationErrorResponse(validationResult.Errors);
         return validationErrorJson;
      }

      var isExistFavoriteList = _crudRepository.FindValuesByExpressionAsync(e =>
         e.SuperHeroId == createFavoriteListDto.SuperHeroId && e.UserId == userId);

      if (isExistFavoriteList)
      {
         var validationErrorJson = ResponseHelper.CustomErrorResponse("SuperHero already exist in your favorite list");
         return validationErrorJson;
      }

      createFavoriteListDto.UserId = userId;
      var favoriteList = _mapper.Map<FavoriteListModel>(createFavoriteListDto);
      await _crudRepository.AddAsync(favoriteList);

      var successResponseJson = ResponseHelper.FluentValidationSuccessResponse("SuperHero Created Successful");
      return successResponseJson;
   }

   public async Task<List<ReadFavoriteListDto>> GetAllByUserIdAsync(string userId, int pageNumber, int pageSize)
   {
      var favoriteLists = await _crudRepository.GetByIdAsync(userId, "UserId", pageNumber, pageSize, x => x.Id);

      var results = _mapper.Map<List<ReadFavoriteListDto>>(favoriteLists);

      return results;
   }


   public async Task<String> DeleteAsync(DeleteFavoriteListDto deleteFavoriteListDto, string userId)
   {
      var find = await _crudRepository.FindByIdAsync(deleteFavoriteListDto.Id);
      if (find is not null && find.UserId != userId)
      {
         var unAuthorized = ResponseHelper.CustomErrorResponse("You can not delete this SuperHero");
         return unAuthorized;
      }

      bool isDeleted = await _crudRepository.DeleteAsync(deleteFavoriteListDto.Id);
      if (isDeleted)
      {
         var successResponseJson = ResponseHelper.FluentValidationSuccessResponse("SuperHero Deleted Successful");
         return successResponseJson;
      }

      var errorResponseJson = ResponseHelper.CustomErrorResponse("SuperHero not found");
      return errorResponseJson;
   }
}
