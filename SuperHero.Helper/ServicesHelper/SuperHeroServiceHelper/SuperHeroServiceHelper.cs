using Microsoft.Extensions.Configuration;

namespace SuperHero.Helper
{
   public class SuperHeroServiceHelper : ISuperHeroServiceHelper
   {
      private readonly IHttpClientFactory _httpClientFactory;
      private readonly IConfiguration _configuration;

      public SuperHeroServiceHelper(IHttpClientFactory httpClientFactory, IConfiguration configuration)
      {
         _httpClientFactory = httpClientFactory;
         _configuration = configuration;
      }

      public async Task<string> RequestHelperAsync<T>(string url, string accessToken, string requestUri, T entity)
      {
         if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(url))
         {
            var validationErrorJson = ResponseHelper.CustomErrorResponse("Invalid Api Configuration");
            return validationErrorJson;
         }

         var client = _httpClientFactory.CreateClient();
         client.BaseAddress = new Uri(url);

         try
         {
            HttpResponseMessage response = await GetAsyncWithTimeout(client, requestUri, TimeSpan.FromSeconds(5));
            var result = await response.Content.ReadAsStringAsync();

            if (result.Contains("error"))
            {
               var validationErrorJson = ResponseHelper.CustomErrorResponse("character not found");
               return validationErrorJson;
            }

            return result;
         }
         catch (TimeoutException ex)
         {
            var timeoutErrorJson = ResponseHelper.CustomErrorResponse($"Timeout: Cannot connect to the server.");
            return timeoutErrorJson;
         }
      }

      private static async Task<HttpResponseMessage> GetAsyncWithTimeout(HttpClient client, string requestUri,
         TimeSpan timeout)
      {
         using (var cts = new CancellationTokenSource())
         {
            var timeoutTask = Task.Delay(timeout, cts.Token);

            var responseTask = client.GetAsync(requestUri);

            var completedTask = await Task.WhenAny(responseTask, timeoutTask);

            if (completedTask == timeoutTask)
            {
               cts.Cancel();
            }

            return await responseTask;
         }
      }
   }
}
