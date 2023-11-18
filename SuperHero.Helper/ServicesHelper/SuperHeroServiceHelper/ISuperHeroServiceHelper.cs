namespace SuperHero.Helper;

public interface ISuperHeroServiceHelper
{
   Task<string> RequestHelperAsync<T>(string url, string accessToken, string requestUri, T entity);
}
