using MinimalApi.Infrastructure;
using MinimalApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using MinimalApi.Application.Services;
using MinimalApi.Configurations;

var builder = WebApplication.CreateBuilder(args);
var configuration = new Configuration();

builder.Services.Configure<MongoDBSettings>(options => {
    options.ConnectionURI = configuration.GetValue("MONGODB_URI");
});

builder.Services.AddSingleton<JwtSecurityTokenHandler>();
builder.Services.AddSingleton<TokenService>();
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<AuthRepository>();
builder.Services.AddControllers();

builder.Services.AddAuthentication( x => {
  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
  x.TokenValidationParameters = new TokenValidationParameters {
    ValidIssuer = configuration.GetValue("JWT_ISSUER"),
    ValidAudience = configuration.GetValue("JWT_AUDIENCE"),
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue("JWT_SECRET"))),
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
  };
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();