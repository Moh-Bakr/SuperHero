using FluentValidation;
using System.Text.Json;
using SuperHero.Helper;

namespace SuperHero.BAL;

public class SuperHeroDetailsDto
{
   public int CharacterId { get; set; }
}

public class SuperHeroDetailsDtoValidator : AbstractValidator<SuperHeroDetailsDto>
{
   public SuperHeroDetailsDtoValidator()
   {
      {
         RuleFor(x => x.CharacterId).NotEmpty().WithMessage("CharacterId is required.");
      }
   }
}

public static class SuperHeroDetailsDtoValidatorExtensions
{
   public static async Task<string> ValidateToJsonAsync(this SuperHeroDetailsDto model)
   {
      var validator = new SuperHeroDetailsDtoValidator();
      var result = await validator.ValidateAsync(model);
      return SerializationUtility.SerializeErrors(result.Errors);
   }
}
