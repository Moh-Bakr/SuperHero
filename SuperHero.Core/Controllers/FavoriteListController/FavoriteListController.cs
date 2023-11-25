using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BAL;
using SuperHero.Helper;

namespace SuperHero.Core.Controllers.FavoriteListController;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class FavoriteListController : ControllerBase
{
   private readonly IFavoriteListService _favoriteListService;

   public FavoriteListController(IFavoriteListService favoriteListService)
   {
      _favoriteListService = favoriteListService;
   }

   [HttpPost]
   [Route("Create")]
   public async Task<IActionResult> AddAsync([FromBody] CreateFavoriteListDto createFavoriteListDto)
   {
      string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (userId is null) return Unauthorized();


      var response = await _favoriteListService.AddAsync(createFavoriteListDto, userId);

      bool isSuccessResponse = ResponseType.IsSuccessResponse(response);
      if (isSuccessResponse) return ResponseHelper.ContentResultCreatedResponse(response);

      return BadRequest(response);
   }

   [HttpGet]
   [Route("GetByUserId")]
   public async Task<IActionResult> GetAllByUserIdAsync([FromQuery] int pageNumber, int pageSize)
   {
      string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (userId is null) return Unauthorized();


      var favoriteLists = await _favoriteListService.GetAllByUserIdAsync(userId, pageNumber, pageSize);
      return Ok(favoriteLists);
   }

   [HttpDelete]
   [Route("Delete")]
   public async Task<IActionResult> DeleteAsync([FromQuery] DeleteFavoriteListDto deleteFavoriteListDto)
   {
      string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (userId is null) return Unauthorized();

      var response = await _favoriteListService.DeleteAsync(deleteFavoriteListDto, userId);

      bool isSuccessResponse = ResponseType.IsSuccessResponse(response);
      if (isSuccessResponse) return Ok(response);

      return BadRequest(response);
   }
}
