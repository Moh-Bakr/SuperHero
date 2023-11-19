using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SuperHero.Helper;

using FluentValidation.Results;

public static class ResponseHelper
{
   public static string FluentValidationErrorResponse(IEnumerable<ValidationFailure> errors)
   {
      var errorMessages = errors.Select(error => error.ErrorMessage).ToArray();
      var jsonResponse = new { IsSuccess = false, errors = errorMessages };
      return JsonConvert.SerializeObject(jsonResponse);
   }

   public static string FluentValidationSuccessResponse(string message)
   {
      var successResponse = new { IsSuccess = true, message = message };
      return JsonConvert.SerializeObject(successResponse);
   }

   public static IActionResult ContentResultCreatedResponse(string successContent)
   {
      return new ContentResult
      {
         Content = successContent,
         StatusCode = StatusCodes.Status201Created,
         ContentType = "application/json"
      };
   }

   public static string CustomErrorResponse(string message)
   {
      var jsonResponse = new { IsSuccess = false, errors = new[] { message } };
      return JsonConvert.SerializeObject(jsonResponse);
   }
}
