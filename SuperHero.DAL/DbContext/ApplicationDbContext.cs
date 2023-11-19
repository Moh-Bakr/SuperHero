using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SuperHero.DAL;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
   public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
   {
   }

   public DbSet<FavoriteListModel> FavoriteList { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      base.OnModelCreating(modelBuilder);

      #region Configure primary key for IdentityUserLogin<string>

      modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
      {
         entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
      });

      #endregion

      modelBuilder.ApplyConfiguration(new FavoriteListModelConfig());
   }
}
