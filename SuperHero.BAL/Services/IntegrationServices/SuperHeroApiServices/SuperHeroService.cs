using FluentValidation;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SuperHero.Helper;

namespace SuperHero.BAL;

public class SuperHeroService : ISuperHeroService
{
   private readonly IHttpClientFactory _httpClientFactory;
   private readonly IConfiguration _configuration;
   private readonly IValidator<SuperHeroSearchDto> _searchDtoValidator;
   private readonly IValidator<SuperHeroDetailsDto> _detailsDtoValidator;
   private readonly ISuperHeroServiceHelper _superHeroServiceHelper;

   public SuperHeroService(IHttpClientFactory httpClientFactory, IConfiguration configuration,
      ISuperHeroServiceHelper superHeroServiceHelper,
      IValidator<SuperHeroSearchDto> searchDtoValidator, IValidator<SuperHeroDetailsDto> detailsDtoValidator)
   {
      _httpClientFactory = httpClientFactory;
      _configuration = configuration;
      _superHeroServiceHelper = superHeroServiceHelper;
      _searchDtoValidator = searchDtoValidator;
      _detailsDtoValidator = detailsDtoValidator;
   }

   public async Task<string> SearchAsync(SuperHeroSearchDto searchDto)
   {
      var validationResult = await _searchDtoValidator.ValidateAsync(searchDto);
      if (!validationResult.IsValid)
      {
         var validationErrorJson = ResponseHelper.FluentValidationErrorResponse(validationResult.Errors);
         return validationErrorJson;
      }

      var accessToken = _configuration["SuperHeroApi:Api_Key"];
      var url = _configuration["SuperHeroApi:Api_Url"];
      string requestUri = $"{accessToken}/search/{searchDto.Name}";

      var result = await _superHeroServiceHelper.HttpRequestHelperAsync(url, accessToken, requestUri, searchDto);
      return result;
   }


   public async Task<string> DetailsAsync(SuperHeroDetailsDto detailsDto)
   {
      var validationResult = await _detailsDtoValidator.ValidateAsync(detailsDto);
      if (!validationResult.IsValid)
      {
         var validationErrorJson = ResponseHelper.FluentValidationErrorResponse(validationResult.Errors);
         return validationErrorJson;
      }

      var accessToken = _configuration["SuperHeroApi:Api_Key"];
      var url = _configuration["SuperHeroApi:Api_Url"];
      string requestUri = $"{accessToken}/{detailsDto.CharacterId}";

      var result = await _superHeroServiceHelper.HttpRequestHelperAsync(url, accessToken, requestUri, detailsDto);
      return result;
   }
}
