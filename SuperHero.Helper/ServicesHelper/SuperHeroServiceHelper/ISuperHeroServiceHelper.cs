namespace SuperHero.Helper;

public interface ISuperHeroServiceHelper
{
   Task<string> HttpRequestHelperAsync<T>(string url, string accessToken, string requestUri, T entity);
}
