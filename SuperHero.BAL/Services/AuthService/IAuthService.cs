using Microsoft.AspNetCore.Identity;
using SuperHero.BAL.Dtos;

namespace SuperHero.BAL;

public interface IAuthService
{
   Task<ResponseResult<IdentityUser>> RegisterAsync(RegisterDto registerDto);
   Task<ResponseResult<String>> LoginAsync(LoginDto loginDto);
}
