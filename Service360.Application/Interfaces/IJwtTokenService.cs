using Service360.Application.DTOs.Auth;
using Service360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service360.Application.Interfaces
{
    public interface IJwtTokenService
    {
        AuthResponse GenerateToken(AppUser user);
    }
}
