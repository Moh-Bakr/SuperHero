using Microsoft.AspNetCore.Identity;

namespace SuperHero.DAL;

public class ApplicationUser : IdentityUser
{
   public ICollection<FavoriteListModel> FavoriteSuperheroes { get; set; }
}
