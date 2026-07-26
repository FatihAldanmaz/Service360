using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service360.Application.DTOs.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; } = null!;

        public DateTime Expiration { get; set; }
    }
}
