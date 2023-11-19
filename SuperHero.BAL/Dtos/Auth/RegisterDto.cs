using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SuperHero.DAL;
using SuperHero.Helper;

namespace SuperHero.BAL.Dtos;

public class RegisterDto
{
   public string UserName { get; set; }
   public string Email { get; set; }
   public string Password { get; set; }
   public string ConfirmPassword { get; set; }
}

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
   private readonly UserManager<ApplicationUser> _userManager;

   public RegisterDtoValidator(UserManager<ApplicationUser> userManager)
   {
      _userManager = userManager;
      RuleFor(x => x.UserName)
         .NotEmpty().WithMessage("Username is required.")
         .Matches(@"^[a-zA-Z0-9_-]{4,16}$")
         .WithMessage(
            "Username must be 4-16 characters and can only contain letters, numbers, underscores, and dashes.")
         .MustAsync(IsUsernameUnique).WithMessage("{PropertyName} is already taken.");

      RuleFor(x => x.Email)
         .NotEmpty().WithMessage("Email is required.")
         .EmailAddress().WithMessage("Invalid email format.")
         .MustAsync(IsEmailUnique).WithMessage("Email is already registered.");

      RuleFor(x => x.Password)
         .NotEmpty().WithMessage("Password is required.")
         .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
         .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
         .WithMessage(
            "Password must contain at least 1 uppercase letter, 1 lowercase letter, 1 number, and 1 special character.")
         .NotEqual(x => x.UserName).WithMessage("Password must not be the same as username.");

      RuleFor(dto => dto.ConfirmPassword).Equal(dto => dto.Password)
         .WithMessage("Password and confirmation password must match");
   }

   private async Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken)
   {
      var user = await _userManager.FindByEmailAsync(email);
      return user == null;
   }

   private async Task<bool> IsUsernameUnique(string username, CancellationToken cancellationToken)
   {
      var user = await _userManager.FindByNameAsync(username);
      return user == null;
   }
}

public static class RegisterDtoModelExtensions
{
   public static async Task<string> ValidateToJsonAsync(this RegisterDto model,
      UserManager<ApplicationUser> userManager)
   {
      var validator = new RegisterDtoValidator(userManager);
      var result = await validator.ValidateAsync(model);
      return SerializationUtility.SerializeErrors(result.Errors);
   }
}
