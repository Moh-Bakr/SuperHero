using System.Text;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SuperHero.BAL;
using SuperHero.BAL.Dtos;
using SuperHero.DAL;
using SuperHero.Helper;
using SuperHero.Helper.AuthHelper.TokenHelper;

var builder = WebApplication.CreateBuilder(args);

#region DbContext and Identity

builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
   .AddEntityFrameworkStores<ApplicationDbContext>()
   .AddDefaultTokenProviders();

#endregion

#region JWT Authentication and Authorization Configuration

builder.Services.AddAuthentication(options =>
{
   options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
   options.SaveToken = true;
   options.RequireHttpsMetadata = true;
   options.TokenValidationParameters = new TokenValidationParameters
   {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = builder.Configuration["Jwt:Issuer"],
      ValidAudience = builder.Configuration["Jwt:Audience"],
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
   };
});

#endregion

#region GenericRepositories

builder.Services.AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>));

#endregion

#region Services

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthToken, AuthToken>();
builder.Services.AddScoped<ISuperHeroService, SuperHeroService>();
builder.Services.AddScoped<ISuperHeroServiceHelper, SuperHeroServiceHelper>();
builder.Services.AddScoped<IFavoriteListService, FavoriteListService>();

#endregion

#region AutoMapper Configuration

var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion

#region FluentValidation

builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
builder.Services.AddTransient<IValidator<RegisterDto>, RegisterDtoValidator>();
builder.Services.AddTransient<IValidator<LoginDto>, LoginDtoValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<SuperHeroSearchDto>();
builder.Services.AddTransient<IValidator<SuperHeroSearchDto>, SuperHeroSearchDtoValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<SuperHeroDetailsDto>();
builder.Services.AddTransient<IValidator<SuperHeroDetailsDto>, SuperHeroDetailsDtoValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateFavoriteListDto>();
builder.Services.AddTransient<IValidator<CreateFavoriteListDto>, FavoriteListDtoValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<DeleteFavoriteListDto>();
builder.Services.AddTransient<IValidator<DeleteFavoriteListDto>, DeleteFavoriteListDtoDtoValidator>();

#endregion

#region Database Seeder

builder.Services.AddTransient<DataSeeder>();
builder.Services.AddScoped<IAuthSeeder, AuthSeeder>();

#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

#region SeedData Command

if (args.Length == 1 && args[0].ToLower() == "seeddata")
      SeedData(app);

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#region SeedData Function

void SeedData(WebApplication app)
{
   using (var scope = app.Services.CreateScope())
   {
      var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
      dataSeeder.SeedDataAsync().Wait();
   }
}

#endregion

app.Run();
