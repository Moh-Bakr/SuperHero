using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BAL;
using SuperHero.Helper;

namespace SuperHero.Core.Controllers.IntegrationControllers.SuperHero;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class SuperHeroController : ControllerBase
{
   private readonly ISuperHeroService _superHeroService;

   public SuperHeroController(ISuperHeroService superHeroService)
   {
      _superHeroService = superHeroService;
   }

   [HttpGet]
   [Route("search")]
   public async Task<IActionResult> SearchAsync([FromQuery] SuperHeroSearchDto searchDto)
   {
      var searchResult = await _superHeroService.SearchAsync(searchDto);
      if (searchResult.Contains("error"))
      {
         return ResponseHelper.ContentResultErrorResponse(searchResult);
      }

      return ResponseHelper.ContentResultSuccessResponse(searchResult);
   }

   [HttpGet]
   [Route("details")]
   public async Task<IActionResult> DetailsAsync([FromQuery] SuperHeroDetailsDto detailsDto)
   {
      var detailsResult = await _superHeroService.DetailsAsync(detailsDto);
      if (detailsResult.Contains("error"))
      {
         return ResponseHelper.ContentResultErrorResponse(detailsResult);
      }

      return ResponseHelper.ContentResultSuccessResponse(detailsResult);
   }
}
