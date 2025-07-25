using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using skinet.API.Extensions;
using skinet.API.Middleware;
using skinet.Core.Entities.Identity;
using skinet.Infrastructure.Data;
using skinet.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(opt =>
{ 
   
});
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithRedirects("/errors/{0}");


app.UseSwaggerDocumentation();


app.UseStaticFiles();

//app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope= app.Services.CreateScope();

var services= scope.ServiceProvider;
var context= services.GetRequiredService<StoreContext>();
var identityContext= services.GetRequiredService<AppIdentityDbContext>();
var userManager= services.GetRequiredService<UserManager<AppUser>>();
var logger= services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await identityContext.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
    await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
}
catch (Exception e)
{
    logger.LogError(e, "An error occurred while migrating the database.");
}

app.Run();