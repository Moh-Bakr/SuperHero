using Microsoft.AspNetCore.Mvc;
using SuperHero.BAL;
using SuperHero.BAL.Dtos;
using SuperHero.Helper;

namespace SuperHero.Core.Controllers;

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
   [Route("register")]
   public async Task<IActionResult> Register(RegisterDto registerDto)
   {
      var registrationResult = await _authService.RegisterAsync(registerDto);
      if (registrationResult.Contains("errors"))
      {
         return ResponseHelper.ContentResultErrorResponse(registrationResult);
      }

      return ResponseHelper.ContentResultSuccessResponse(registrationResult);
   }

   [HttpPost]
   [Route("login")]
   public async Task<IActionResult> Login(LoginDto loginDto)
   {
      var loginResult = await _authService.LoginAsync(loginDto);

      if (loginResult is null) return Unauthorized("Invalid Credentials");

      return ResponseHelper.ContentResultSuccessResponse(loginResult);
   }
}
