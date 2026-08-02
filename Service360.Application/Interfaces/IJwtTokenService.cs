using Service360.Application.DTOs.Auth;
using Service360.Domain.Entities;


namespace Service360.Application.Interfaces
{
    public interface IJwtTokenService
    {
        AuthResponse GenerateToken(AppUser user,IList<string> roles);
    }
}
