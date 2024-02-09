using MinimalApi.Infrastructure;
using MinimalApi.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<AuthRepository>();
builder.Services.AddControllers();

builder.Services.AddAuthentication( x => {
  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
  x.TokenValidationParameters = new TokenValidationParameters {
    ValidIssuer = "admin",
    ValidAudience = "admin",
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("admin")),
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

// https://youtu.be/mgeuh8k3I4g?t=310