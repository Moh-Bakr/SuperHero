namespace SuperHero.BAL;

public interface IFavoriteListService
{
   Task<String> AddAsync(CreateFavoriteListDto createFavoriteListDto, string userId);
   Task<List<ReadFavoriteListDto>> GetAllByUserIdAsync(string userId, int pageNumber, int pageSize);
   Task<String> DeleteAsync(DeleteFavoriteListDto deleteFavoriteListDto, string userId);
}
