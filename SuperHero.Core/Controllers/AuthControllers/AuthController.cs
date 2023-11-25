using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SuperHero.BAL;
using SuperHero.BAL.Dtos;
using SuperHero.Helper;

namespace SuperHero.Core.Controllers.AuthControllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
   private readonly IAuthService _authService;

   public AuthController(IAuthService authService)
   {
      _authService = authService;
   }

   [HttpPost]
   [Route("Register")]
   public async Task<IActionResult> Register(RegisterDto registerDto)
   {
      var registrationResult = await _authService.RegisterAsync(registerDto);
      bool isSuccessResponse = ResponseType.IsSuccessResponse(registrationResult);

      if (isSuccessResponse) return ResponseHelper.ContentResultCreatedResponse(registrationResult);

      return BadRequest(registrationResult);
   }

   [HttpPost]
   [Route("Login")]
   public async Task<IActionResult> Login(LoginDto loginDto)
   {
      var loginResult = await _authService.LoginAsync(loginDto);
      bool isSuccess = ResponseType.IsSuccessResponse(loginResult);

      if (isSuccess) return Ok(loginResult);

      return BadRequest(loginResult);
   }
}
