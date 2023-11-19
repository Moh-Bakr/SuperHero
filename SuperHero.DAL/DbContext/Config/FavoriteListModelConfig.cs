using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SuperHero.DAL
{
   public class FavoriteListModelConfig : IEntityTypeConfiguration<FavoriteListModel>
   {
      public void Configure(EntityTypeBuilder<FavoriteListModel> builder)
      {
         builder.Property(f => f.Id)
            .ValueGeneratedOnAdd();

         builder.HasOne(favorite => favorite.User)
            .WithMany(user => user.FavoriteSuperheroes)
            .HasForeignKey(favorite => favorite.UserId).IsRequired();

         builder.HasIndex(favorite => new { favorite.SuperHeroId, favorite.UserId })
            .IsUnique();
      }
   }
}
