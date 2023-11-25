using Microsoft.Extensions.DependencyInjection;

namespace SuperHero.DAL
{
   public class DataSeeder
   {
      private readonly IServiceProvider _serviceProvider;

      public DataSeeder(IServiceProvider serviceProvider)
      {
         _serviceProvider = serviceProvider;
      }

      public async Task SeedDataAsync()
      {
         using var scope = _serviceProvider.CreateScope();
         var roleSeeder = scope.ServiceProvider.GetRequiredService<IAuthSeeder>();
         await roleSeeder.SeedRolesAsync();
      }
   }
}
