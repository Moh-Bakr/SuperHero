namespace SuperHero.BAL;

public class ReadFavoriteListDto
{
   public int Id { get; set; }
   public int SuperHeroId { get; set; }
   public string FullName { get; set; }
   public string PlaceOfBirth { get; set; }
   public string ImageUrl { get; set; }
}
