using System.Security.Claims;

namespace SuperHero.Helper;

public interface IAuthToken
{
   List<Claim> GetAuthClaims(string userName, string userId, IEnumerable<string> roles);
   string GenerateNewJsonWebTokenToken(List<Claim> claims);
}
