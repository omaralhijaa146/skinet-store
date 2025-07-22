using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using skinet.Core.Entities.Identity;
using skinet.Infrastructure.Identity;

namespace skinet.API.Extensions;

public static class IdentityServiceExtensions
{

    public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<AppIdentityDbContext>(opt =>
        {
            opt.UseSqlite(configuration.GetConnectionString("IdentityConnection"));
        });

        services.AddIdentityCore<AppUser>(opt =>
        {
            
        }).AddEntityFrameworkStores<AppIdentityDbContext>()
        .AddSignInManager<SignInManager<AppUser>>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                ValidIssuer = configuration["Token:Issuer"],
                ValidateIssuer = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateAudience = false,
            };
        });
        services.AddAuthorization();
        return services;
    }

}