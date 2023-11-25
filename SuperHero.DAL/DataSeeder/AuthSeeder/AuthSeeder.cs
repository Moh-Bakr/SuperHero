using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace SuperHero.DAL
{
   public class AuthSeeder : IAuthSeeder
   {
      private readonly RoleManager<IdentityRole> _roleManager;

      public AuthSeeder(RoleManager<IdentityRole> roleManager)
      {
         _roleManager = roleManager;
      }

      public async Task SeedRolesAsync()
      {
         await SeedRoleAsync("Admin");
         await SeedRoleAsync("User");
      }

      private async Task SeedRoleAsync(string roleName)
      {
         if (!await _roleManager.RoleExistsAsync(roleName))
         {
            var role = new IdentityRole { Name = roleName };
            await _roleManager.CreateAsync(role);
         }
      }
   }
}
