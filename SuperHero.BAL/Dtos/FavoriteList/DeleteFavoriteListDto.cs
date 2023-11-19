using FluentValidation;
using SuperHero.Helper;

namespace SuperHero.BAL;

public class DeleteFavoriteListDto
{
   public int Id { get; set; }
}

public class DeleteFavoriteListDtoDtoValidator : AbstractValidator<DeleteFavoriteListDto>
{
   public DeleteFavoriteListDtoDtoValidator()
   {
      RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
   }
}

public static class DeleteFavoriteListDtoExtensions
{
   public static async Task<string> ValidateToJsonAsync(this DeleteFavoriteListDto model)
   {
      var validator = new DeleteFavoriteListDtoDtoValidator();
      var result = await validator.ValidateAsync(model);
      return SerializationUtility.SerializeErrors(result.Errors);
   }
}
