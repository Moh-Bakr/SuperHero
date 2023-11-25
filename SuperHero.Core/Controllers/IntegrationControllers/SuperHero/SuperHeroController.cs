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
   [Route("Search")]
   public async Task<IActionResult> SearchAsync([FromQuery] SuperHeroSearchDto searchDto)
   {
      var searchResult = await _superHeroService.SearchAsync(searchDto);

      bool isSuccessResponse = ResponseType.IsSuccessResponse(searchResult);
      if (isSuccessResponse) return Ok(searchResult);

      return BadRequest(searchResult);
   }

   [HttpGet]
   [Route("Details")]
   public async Task<IActionResult> DetailsAsync([FromQuery] SuperHeroDetailsDto detailsDto)
   {
      var detailsResult = await _superHeroService.DetailsAsync(detailsDto);

      bool isSuccessResponse = ResponseType.IsSuccessResponse(detailsResult);
      if (isSuccessResponse) return Ok(detailsResult);

      return BadRequest(detailsResult);
   }
}
