using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skinet.API.Errors;
using skinet.Core.Interfaces;
using skinet.Infrastructure.Data;
using skinet.Infrastructure.Services;
using StackExchange.Redis;

namespace skinet.API.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<StoreContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddSingleton<IConnectionMultiplexer>(opt =>
        {
            var options = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"));
            return ConnectionMultiplexer.Connect(options);
        });

        services.AddScoped<ITokenService,TokenService>();
        services.AddScoped<IBaskeRepository,BasketRepository>();
        
        services .AddScoped<IProductRepository, ProductRepository>();
        services .AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
        services .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services .Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory= actionContext =>
            {
                var errors = actionContext.ModelState.Where(e=>e.Value.Errors.Count>0).SelectMany(x=>x.Value.Errors).Select(x=>x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(errorResponse);
            };
        });

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",policy=>
            {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
            });
        });
        return services;
    }
}