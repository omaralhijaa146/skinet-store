using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using skinet.Core.Entities.Identity;

namespace skinet.API.Extensions;

public static class UserManagerExtensions
{
    public static async Task<AppUser> FindUserByClaimsPrincipleWithAddress(this UserManager<AppUser> userManager,ClaimsPrincipal user)
    {
        var email = user.FindFirstValue(ClaimTypes.Email);

        return await userManager.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email);
    }

    public static async Task<AppUser> FindByEmailFromClaimsPrinciple(this UserManager<AppUser> userManager,
        ClaimsPrincipal user)
    {
        return await userManager.Users.FirstOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));
    }
}