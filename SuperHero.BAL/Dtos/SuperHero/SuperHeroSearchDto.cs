using System.Text.Json;
using FluentValidation;
using SuperHero.Helper;

namespace SuperHero.BAL;

public class SuperHeroSearchDto
{
   public string Name { get; set; }
}

public class SuperHeroSearchDtoValidator : AbstractValidator<SuperHeroSearchDto>
{
   public SuperHeroSearchDtoValidator()
   {
      {
         RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
      }
   }
}

public static class SuperHeroSearchDtoValidatorExtensions
{
   public static async Task<string> ValidateToJsonAsync(this SuperHeroSearchDto model)
   {
      var validator = new SuperHeroSearchDtoValidator();
      var result = await validator.ValidateAsync(model);
      return SerializationUtility.SerializeErrors(result.Errors);
   }
}
