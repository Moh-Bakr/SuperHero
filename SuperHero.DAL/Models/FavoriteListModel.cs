using Microsoft.AspNetCore.Identity;

namespace SuperHero.DAL;

public class FavoriteListModel : BaseModel
{
   public int SuperHeroId { get; set; }
   public string UserId { get; set; }
   public ApplicationUser User { get; set; }
   public string FullName { get; set; }
   public string PlaceOfBirth { get; set; }
   public string ImageUrl { get; set; }
}
