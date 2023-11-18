namespace SuperHero.BAL;

public interface ISuperHeroService
{
   Task<string> SearchAsync(SuperHeroSearchDto searchDto);
   Task<String> DetailsAsync(SuperHeroDetailsDto detailsDto);
}
