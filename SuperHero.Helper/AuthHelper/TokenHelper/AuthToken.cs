using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace SuperHero.Helper.AuthHelper.TokenHelper
{
   public class AuthToken : IAuthToken
   {
      private readonly IConfiguration _configuration;

      public AuthToken(IConfiguration configuration)
      {
         _configuration = configuration;
      }

      public List<Claim> GetAuthClaims(string userName, string userId, IEnumerable<string> roles)
      {
         var authClaims = new List<Claim>
         {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim("JWTD", Guid.NewGuid().ToString())
         };

         foreach (var role in roles)
         {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
         }

         return authClaims;
      }
      public string GenerateNewJsonWebTokenToken(List<Claim> claims)
      {
         var jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
         var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            expires: DateTime.Now.AddHours(12),
            claims: claims,
            signingCredentials: new SigningCredentials(
               jwtKey, SecurityAlgorithms.HmacSha256)
         );
         return new JwtSecurityTokenHandler().WriteToken(token);
      }

   }
}
