using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service360.Application.DTOs.Auth
{
    public class CompanyRegisterRequest
    {

        public string CompanyName { get; set; } = string.Empty;

        public string? TaxNumber { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
