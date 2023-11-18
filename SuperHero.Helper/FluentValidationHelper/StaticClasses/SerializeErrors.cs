using System.Text.Json;
using FluentValidation.Results;

namespace SuperHero.Helper;

public static class SerializationUtility
{
   public static string SerializeErrors(IEnumerable<ValidationFailure> errors)
   {
      var errorMessages = errors.Select(x => x.ErrorMessage);
      return JsonSerializer.Serialize(errorMessages, new JsonSerializerOptions
      {
         WriteIndented = true,
      }).Replace("\r\n", "\n");
   }
}
