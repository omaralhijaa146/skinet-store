using skinet.Core.Entities.Identity;

namespace skinet.Core.Interfaces;

public interface ITokenService
{
    public string CreateToken(AppUser user);
}