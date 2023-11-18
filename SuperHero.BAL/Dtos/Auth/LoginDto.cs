using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SuperHero.Helper;

namespace SuperHero.BAL;

public class LoginDto
{
   public string UserName { get; set; }

   public string Password { get; set; }
}

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
   private readonly UserManager<IdentityUser> _userManager;

   public LoginDtoValidator(UserManager<IdentityUser> userManager)
   {
      _userManager = userManager;
      {
         RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required.");
         RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
      }
   }
}

public static class LoginDtoExtensions
{
   public static async Task<string> ValidateToJsonAsync(this LoginDto model,
      UserManager<IdentityUser> userManager)
   {
      var validator = new LoginDtoValidator(userManager);
      var result = await validator.ValidateAsync(model);
      return SerializationUtility.SerializeErrors(result.Errors);
   }
}
