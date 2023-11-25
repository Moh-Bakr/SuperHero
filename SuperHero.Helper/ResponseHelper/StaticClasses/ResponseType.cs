using Newtonsoft.Json;

namespace SuperHero.Helper;

public static class ResponseType
{
   public static bool IsSuccessResponse(string response)
   {
      var resultObject = JsonConvert.DeserializeObject<IsSuccessResultType>(response);
      return resultObject.IsSuccess;
   }

   private class IsSuccessResultType
   {
      public bool IsSuccess { get; set; } = true;
   }
}
