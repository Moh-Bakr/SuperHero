using Microsoft.AspNetCore.Identity;
using SuperHero.BAL.Dtos;

namespace SuperHero.BAL;

public interface IAuthService
{
   Task<String> RegisterAsync(RegisterDto registerDto);
   Task<String> LoginAsync(LoginDto loginDto);
}
