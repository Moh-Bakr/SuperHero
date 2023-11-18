using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SuperHero.BAL.Dtos;
using SuperHero.Helper;

namespace SuperHero.BAL;

public class AuthService : IAuthService
{
   private readonly UserManager<IdentityUser> _userManager;
   private readonly RoleManager<IdentityRole> _roleManager;
   private readonly IValidator<RegisterDto> _registerDtoValidator;
   private readonly IValidator<LoginDto> _loginDtoValidator;
   private readonly IAuthToken _authToken;
   private readonly IConfiguration _configuration;
   private readonly IMapper _mapper;

   public AuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
      IConfiguration configuration, IMapper mapper, IValidator<RegisterDto> registerDtoValidator,
      IValidator<LoginDto> loginDtoValidator, IAuthToken authToken)
   {
      _registerDtoValidator = registerDtoValidator;
      _loginDtoValidator = loginDtoValidator;
      _userManager = userManager;
      _roleManager = roleManager;
      _configuration = configuration;
      _mapper = mapper;
      _authToken = authToken;
   }

   public async Task<String> RegisterAsync(RegisterDto registerDto)
   {
      var validationResult = await _registerDtoValidator.ValidateAsync(registerDto);
      if (!validationResult.IsValid)
      {
         var validationErrorJson = ResponseHelper.FluentValidationErrorResponse(validationResult.Errors);
         return validationErrorJson;
      }

      var user = _mapper.Map<IdentityUser>(registerDto);

      await _userManager.CreateAsync(user, registerDto.Password);
      await _userManager.AddToRoleAsync(user, StaticUserRoles.User);

      var successResponseJson = ResponseHelper.FluentValidationSuccessResponse("Registration successful");
      return successResponseJson;
   }

   public async Task<String> LoginAsync(LoginDto loginDto)
   {
      var validationResult = await _loginDtoValidator.ValidateAsync(loginDto);
      if (!validationResult.IsValid)
      {
         var validationErrorJson = ResponseHelper.FluentValidationErrorResponse(validationResult.Errors);
         return validationErrorJson;
      }

      var user = await _userManager.FindByNameAsync(loginDto.UserName);
      if (user is null) return null;

      var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
      if (!result) return null;

      var roles = await _userManager.GetRolesAsync(user);

      var authClaims = _authToken.GetAuthClaims(user.UserName, user.Id, roles);
      var token = _authToken.GenerateNewJsonWebTokenToken(authClaims);

      var successResponseJson = ResponseHelper.FluentValidationSuccessResponse(token);
      return successResponseJson;
   }
}
