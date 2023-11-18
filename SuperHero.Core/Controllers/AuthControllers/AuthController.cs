using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SuperHero.BAL;
using SuperHero.BAL.Dtos;

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

   [HttpPost("register")]
   public async Task<IActionResult> Register(RegisterDto registerDto)
   {
      var registrationResult = await _authService.RegisterAsync(registerDto);

      if (!registrationResult.Succeeded)
      {
         return BadRequest(registrationResult.Errors);
      }

      return new ContentResult
      {
         Content = "User Registered Successfully",
         StatusCode = StatusCodes.Status201Created
      };
   }

   [HttpPost("login")]
   public async Task<IActionResult> Login(LoginDto loginDto)
   {
      var loginResult = await _authService.LoginAsync(loginDto);

      if (loginResult is null) return Unauthorized("Invalid Credentials");


      return new ContentResult
      {
         Content = loginResult.Data,
         StatusCode = StatusCodes.Status200OK,
         ContentType = "application/json"
      };
   }
}
