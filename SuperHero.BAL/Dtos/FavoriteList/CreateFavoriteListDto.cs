using FluentValidation;
using SuperHero.Helper;

namespace SuperHero.BAL;

public class CreateFavoriteListDto
{
   public int SuperHeroId { get; set; }
   public string? UserId { get; set; }
   public string FullName { get; set; }
   public string PlaceOfBirth { get; set; }
   public string ImageUrl { get; set; }
}

public class FavoriteListDtoValidator : AbstractValidator<CreateFavoriteListDto>
{
   public FavoriteListDtoValidator()
   {
      RuleFor(x => x.SuperHeroId).NotEmpty().WithMessage("SuperHeroId is required.");
      RuleFor(x => x.FullName).NotEmpty().WithMessage("FullName is required.");
      RuleFor(x => x.PlaceOfBirth).NotEmpty().WithMessage("PlaceOfBirth is required.");
      RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("ImageUrl is required.");
   }
}

public static class FavoriteListDtoExtensions
{
   public static async Task<string> ValidateToJsonAsync(this CreateFavoriteListDto model)
   {
      var validator = new FavoriteListDtoValidator();
      var result = await validator.ValidateAsync(model);
      return SerializationUtility.SerializeErrors(result.Errors);
   }
}
