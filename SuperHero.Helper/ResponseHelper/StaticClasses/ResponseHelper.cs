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
      var jsonResponse = new { errors = errorMessages };
      return JsonConvert.SerializeObject(jsonResponse);
   }

   public static string FluentValidationSuccessResponse(string message)
   {
      var successResponse = new { message = message };
      return JsonConvert.SerializeObject(successResponse);
   }

   public static IActionResult ContentResultErrorResponse(string errorContent)
   {
      return new ContentResult
      {
         Content = errorContent,
         StatusCode = StatusCodes.Status400BadRequest,
         ContentType = "application/json"
      };
   }

   public static IActionResult ContentResultSuccessResponse(string successContent)
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
      var jsonResponse = new { errors = new[] { message } };
      return JsonConvert.SerializeObject(jsonResponse);
   }
}
